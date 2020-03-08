// AISPK4.3.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include<cmath>
#include <cstring>

//定义圆周率
constexpr auto M_PI = 3.141592653589793238462643383279502884L/* pi */;

//霍夫变换参数
struct HTParameter
{
	int r_found;
	int x_found;
	int y_found;
	int max_accu_value;
};

//图像处理类
template<int image_height, int image_width>
class CVX {
private:
	//定义临时数据
	struct TempImageData
	{
		//霍夫空间累加数据
		unsigned char AccuGrid[image_height][image_width] = { 0 };
		unsigned char AccuGrid_filt[image_height][image_width] = { 0 };
	};

public:
	//定义各个图层数据，数组+指针，双重变量输出
	struct ImageData
	{
		//初始化双重指针
		ImageData() 
		{
			Array2Ptr(R, ptr_R);
			Array2Ptr(G, ptr_G);
			Array2Ptr(B, ptr_B);
			Array2Ptr(R_filt, ptr_R_filt);
			Array2Ptr(G_filt, ptr_G_filt);
			Array2Ptr(B_filt, ptr_B_filt);
			Array2Ptr(ColorMask, ptr_ColorMask);
			Array2Ptr(EdgeMask, ptr_EdgeMask);
		}
		//定义双重指针
		unsigned char** ptr_R, ** ptr_G, ** ptr_B, ** ptr_R_filt, ** ptr_G_filt, ** ptr_B_filt, ** ptr_ColorMask, ** ptr_EdgeMask;
		//原始RGB数据（目前未使用，供意外情况）
		unsigned char R[image_height][image_width] = { 0 };
		unsigned char G[image_height][image_width] = { 0 };
		unsigned char B[image_height][image_width] = { 0 };
		//过滤后RGB数据
		unsigned char R_filt[image_height][image_width] = { 0 };
		unsigned char G_filt[image_height][image_width] = { 0 };
		unsigned char B_filt[image_height][image_width] = { 0 };
		//颜色过滤后数据
		unsigned char ColorMask[image_height][image_width] = { 0 };
		//边缘检测后的数据
		unsigned char EdgeMask[image_height][image_width] = { 0 };
		//数组转指针
		void Array2Ptr(unsigned char Array[image_height][image_width], unsigned char** &ptr)
		{
			ptr = new unsigned char* [image_height];
			for (int x = 0; x < image_height; x++)ptr[x] = Array[x];
		}
		//释放指针
		void DeletePtr(unsigned char** ptr)
		{
			delete ptr;
			ptr = NULL;
		}
		//调用结束后释放指针
		~ImageData()
		{
			DeletePtr(ptr_R);
			DeletePtr(ptr_G);
			DeletePtr(ptr_B);
			DeletePtr(ptr_R_filt);
			DeletePtr(ptr_G_filt);
			DeletePtr(ptr_B_filt);
		}
	};

	//定义高度宽度
	int height = image_height;
	int width = image_width;
	//卷积使用的Kernel
	int kernel_gauss[3][3] = { {1,1,1},	{1,1,1},	{1,1,1} }; //低通滤波CNN
	int kernel_edge_h[3][3] = { {1,0,-1},	{2,0,-2},	{1,0,-1} };//水平边缘检测CNN
	int kernel_edge_v[3][3] = { {1,2,1},	{0,0,0},	{-1,-2,-1} };//垂直边缘检查CNN

	//定义储存数据
	ImageData* Data;
	TempImageData* TempData;
	//定义结果
	HTParameter Result = { 0 };

	//初始化函数，用于分配内存
	CVX()
	{
		Data = new ImageData;
		TempData = new TempImageData;
	}

	//图像初始化，将数组置零
	template<class T>
	void InitImage(T image, unsigned char value = 0)
	{
		for (int x = 0; x < image_height; x++)
		{
			for (int y = 0; y < image_width; y++)
			{
				image[x][y] = value;
			}
		}
	}

	//3*3卷积计算
	template<class T1, class T2>
	void Convolve3_3(T1 image,T2 image_filt, int kernel[3][3])
	{
		for (int x = 0; x < image_height - 2; x++)
		{
			for (int y = 0; y < image_width - 2; y++)
			{
				unsigned int sum = 0;
				for (int k = 0; k < 3; k++) {
					for (int l = 0; l < 3; l++) {
						sum += (unsigned int)image[x + k][y + l] * (unsigned int)kernel[k][l];
					}
				}
				image_filt[x + 1][y + 1] = (unsigned int)(sum / 9.0 + 0.5);
			}
		}
	}

	//CNN低通滤波，高斯滤波
	template<class T1,class T2>
	void GaussFilter(T1 R, T1 G, T1 B, T2 R_filt, T2 G_filt, T2 B_filt)
	{
		Convolve3_3(R, R_filt, kernel_gauss);
		Convolve3_3(G, G_filt, kernel_gauss);
		Convolve3_3(B, B_filt, kernel_gauss);
	}

	//颜色过滤函数
	template<class T1,class T2>
	void ColorFilter(T1 R_filt, T1 G_filt, T1 B_filt, T2 ColorMask, unsigned char color_threshold)
	{
		for (int x = 0; x < image_height; x++)
		{
			for (int y = 0; y < image_width; y++)
			{
				ColorMask[x][y] = ColorCompare(R_filt[x][y], G_filt[x][y], B_filt[x][y], color_threshold);
			}
		}
	}

	//三种颜色比较并得出结果
	unsigned char ColorCompare(unsigned char R, unsigned char G, unsigned char B, unsigned char color_threshold)
	{
		return ((abs(R - G) > color_threshold) || (abs(B - G) > color_threshold) || (abs(R - B) > color_threshold)) ? 0 : 1;
	}

	//边缘检测
	template<class T1,class T2>
	void EdgeSobel(T1 ColorMask, T2 EdgeMask, double edge_threshold)
	{
		for (int x = 0; x < image_height - 2; x++)
		{
			for (int y = 0; y < image_width - 2; y++)
			{
				double value = 0;
				int value_h = 0;
				int value_v = 0;
				for (int k = 0; k < 3; k++) {
					for (int l = 0; l < 3; l++) {
						value_h += ((int)ColorMask[x + k][y + l] * kernel_edge_h[k][l]);//水平边缘检测
						value_v += ((int)ColorMask[x + k][y + l] * kernel_edge_v[k][l]);//垂直边缘检测
					}
				}
				value += sqrt(pow(value_h, 2) + pow(value_v, 2));
				if (value > edge_threshold) {
					EdgeMask[x + 1][y + 1] = 1;
				}
			}
		}
	}

	//寻找圆心和半径，霍夫变换遍历各个半径
	template<class T>
	HTParameter HoughTransform(T EdgeMask, int r_min, int r_max, int r_step, bool ht_gauss_filter)
	{
		HTParameter _HTP, _HTP_MAX;
		_HTP_MAX.max_accu_value = 1;
		for (int r = r_min; r < r_max; r += r_step)
		{
			_HTP = SubHoughTransform(EdgeMask, r, ht_gauss_filter);
			if (_HTP.max_accu_value > _HTP_MAX.max_accu_value)
			{
				_HTP_MAX = _HTP;
				_HTP_MAX.r_found = r;
			}
		}
		return _HTP_MAX;
	}

	//确定半径的霍夫变换
	template<class T>
	HTParameter SubHoughTransform(T EdgeMask, int radius, bool ht_gauss_filter)
	{
		//将AccuGrid置零
		InitImage(TempData->AccuGrid);
		//初始化ab
		double a, b;
		//设置角度步数
		int step_num = 100;
		//设置初始a,b
		int pre_a = -1;
		int pre_b = -1;
		//设置角度增加步长
		double d_theta = 0.25 * M_PI / radius;// 2 * M_PI / 400
		for (int x = 0; x < image_height; x++)
		{
			for (int y = 0; y < image_width; y++)
			{
				if (EdgeMask[x][y] > 0)
				{
					for (double theta = 0; theta < 2 * M_PI; theta += d_theta)
					{
						a = x - radius * cos(theta);
						b = y - radius * sin(theta);
						HTAccumulate(TempData->AccuGrid, a, b, pre_a, pre_b);
						theta += d_theta;
					}
				}

			}
		}
		HTParameter _HTP = HTGetMaxAccu(TempData->AccuGrid, ht_gauss_filter);
		return _HTP;
	}

	//自动在霍夫空间叠加相应点的值，自动投票
	template<class T>
	void HTAccumulate(T AccuGrid, double a, double b, int& pre_a, int& pre_b)
	{
		int round_a = int(a + 0.5);//四舍五入
		int round_b = int(b + 0.5);
		if ((round_a == pre_a) && (round_b == pre_b))
			return;
		if ((round_a >= image_height) || (round_b >= image_width))
			return;
		AccuGrid[round_a][round_b] += 1;
		pre_a = round_a;
		pre_b = round_b;
	}

	//在霍夫空间找到最大的值，找到投票结果
	template<class T>
	HTParameter HTGetMaxAccu(T AccuGrid, bool ht_gauss_filter)
	{
		//将AccuGrid_filt置零
		InitImage(TempData->AccuGrid_filt);
		//低通滤波，高斯滤波
		if (ht_gauss_filter)
		{
			//开辟空间
			Convolve3_3(AccuGrid, TempData->AccuGrid_filt, kernel_gauss);
		}
		//不进行低通滤波
		else
		{
			memcpy(TempData->AccuGrid_filt, AccuGrid, sizeof(AccuGrid));
		}
		//定义霍夫变换参数
		HTParameter _HTP = { 0 };
		//定义临时值
		int temp;
		//定义初始最大值
		int max = 1;
		int x_sum = 0;
		int y_sum = 0;
		int max_count = 1;
		for (int x = 0; x < image_height; x++)
		{
			for (int y = 0; y < image_width; y++)
			{
				temp = TempData->AccuGrid_filt[x][y];
				if (temp < 2)continue;
				if (temp == max) {
					max_count++;
					x_sum += x;
					y_sum += y;
				}
				else if (temp > max)
				{
					max = temp;
					max_count = 1;
					x_sum = x;
					y_sum = y;
				}
			}
		}
		//储存数据到_HTP
		_HTP.max_accu_value = max;
		_HTP.x_found = int(1.0 * x_sum / max_count + 0.5);
		_HTP.y_found = int(1.0 * y_sum / max_count + 0.5);
		return _HTP;
	}

	//综合处理函数
	template<class T>
	HTParameter GetCircle(T R, T G, T B,
		unsigned char color_threshold = 80,
		double edge_threshold = 2, bool ht_gauss_filter = true,
		int r_min = 2, int r_max = 30, int r_step = 2)
	{
		//高斯滤波
		GaussFilter(R, G, B, Data->R_filt, Data->G_filt, Data->B_filt);
		//颜色滤波
		ColorFilter(Data->R_filt, Data->G_filt, Data->B_filt, Data->ColorMask, color_threshold);
		//边缘检测
		EdgeSobel(Data->ColorMask, Data->EdgeMask, edge_threshold);
		//霍夫变换
		Result = HoughTransform(Data->EdgeMask, r_min, r_max, r_step, ht_gauss_filter);
		return Result;
	}

	//结束函数，释放内存
	~CVX()
	{
		delete Data;
		delete TempData;
	}
};



//以下函数仅供测试使用

//简单打印数据
template< int height, int width, class T>
void Print(T a) {
	std::cout << std::endl;
	for (int x = 0; x < height; x += height / 40) {
		for (int y = 0; y < width; y += width / 80) {
			if (a[x][y] != 0) {
				std::cout << "1";
			}
			else {
				std::cout << "0";
			}
		}
		std::cout <<  std::endl;
	}
	std::cout << std::endl << std::endl << std::endl;
}

//画圆
template< int height, int width, class T>
void CreateCircle(int a, int b, int r, T Rt, T Gt, T Bt)
{
	for (int x = 0; x < height; x++)
	{
		for (int y = 0; y < width; y++)
		{
			double _r = pow(r, 2) - (pow(a - x, 2) + pow(b - y, 2));
			if (_r >= 0)
			{
				Rt[x][y] = 255;
				Gt[x][y] = 255;
			}
		}
	}
}



int main()
{
	static unsigned char Rt[256][256] = { 0 };
	static unsigned char Gt[256][256] = { 0 };
	static unsigned char Bt[256][256] = { 0 };

	CVX<256, 256> cvx;
	CreateCircle<256,256>(90, 140, 25, Rt, Gt, Bt);

	cvx.GetCircle(Rt, Gt, Bt, 80, 4, true, 12, 32, 2);

	std::cout << "检测到的圆心和半径: " << std::endl << std::endl;
	std::cout << "x:	" << cvx.Result.x_found << "	y:	" << cvx.Result.y_found << "	radius:	" << cvx.Result.r_found << std::endl;
	std::cout << std::endl << std::endl;
	std::cout << "ptr_R_filt - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ptr_R_filt);
	std::cout << "R_filt - 数组	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->R_filt);
	std::cout << "ptr_G_filt - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ptr_G_filt);
	std::cout << "G_filt - 数组	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->G_filt);
	std::cout << "ptr_B_filt - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ptr_B_filt);
	std::cout << "B_filt - 数组	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->B_filt);
	std::cout << "ptr_ColorMask - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ptr_ColorMask);
	std::cout << "ColorMask - 数组	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ColorMask);
	std::cout << "ptr_EdgeMask - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ptr_EdgeMask);
	std::cout << "EdgeMask - 数组	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->EdgeMask);
}