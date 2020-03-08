// AISPK4.1.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include<cmath>
#include <cstring>

template<int width, int heigtht>
void Print(unsigned char a[width][heigtht]) {
	std::cout << std::endl;
	for (int x = 0; x < heigtht; x += heigtht / 255) {
		for (int y = 0; y < width; y += width / 256) {
			if (a[x][y] != 0) {
				std::cout << "1";
			}
			else {
				std::cout << "0";
			}
		}
		std::cout << std::endl;
	}
}



constexpr auto M_PI = 3.141592653589793238462643383279502884L/* pi */;

//霍夫变换参数
struct HTParameter {
	int r_found;
	int x_found;
	int y_found;
	int max_accu_value;
};


template<int image_width, int image_height>
struct ImageData {
	//定义各个图像矩阵
	//static unsigned char R_filt[image_height][image_width] = { 0 };
	//static unsigned char G_filt[image_height][image_width] = { 0 };
	//static unsigned char B_filt[image_height][image_width] = { 0 };
	//static unsigned char ColorMask[image_height][image_width] = { 0 };
	//static unsigned char EdgeMask[image_height][image_width] = { 0 };
	unsigned char R_filt[image_height][image_width] = { 0 };
	unsigned char G_filt[image_height][image_width] = { 0 };
	unsigned char B_filt[image_height][image_width] = { 0 };
	unsigned char ColorMask[image_height][image_width] = { 0 };
	unsigned char EdgeMask[image_height][image_width] = { 0 };
};

template<int image_width,int image_height>
class CVX {
public:
	//定义高度宽度
	int width = image_width;
	int height = image_height;

	//卷积使用的Kernel
	int kernel_gauss[3][3] = { {1,1,1},	{1,1,1},	{1,1,1} }; //低通滤波CNN
	int kernel_edge_h[3][3] = { {1,0,-1},	{2,0,-2},	{1,0,-1} };//水平边缘检测CNN
	int kernel_edge_v[3][3] = { {1,2,1},	{0,0,0},	{-1,-2,-1} };//垂直边缘检查CNN

	//定义结果
	HTParameter Result = {0};

	//定义储存数据
	ImageData<image_width, image_height>* Data = new ImageData<image_width, image_height>;

	//初始化函数，用于分配内存
	CVX()
	{

	}

	//3*3卷积计算
	void Convolve3_3(unsigned char image[image_height][image_width], 
		unsigned char image_filt[image_height][image_width], int kernel[3][3]) 
	{
		for (int x = 0; x < image_height - 2; x++) {
			for (int y = 0; y < image_width - 2; y++) {
				unsigned int sum = 0;
				for (int k = 0; k < 3; k++) {
					for (int l = 0; l < 3; l++) {
						sum += (unsigned int)image[x + k + 1][y + l + 1] * (unsigned int)kernel[k][l];
					}
				}
				image_filt[x + 1][y + 1] = (unsigned int)(sum/9);
			}
		}
	}

	//CNN低通滤波，高斯滤波
	void GaussFilter(unsigned char R[image_height][image_width], unsigned char G[image_height][image_width], 
		unsigned char B[image_height][image_width],unsigned char R_filt[image_height][image_width], 
		unsigned char G_filt[image_height][image_width], unsigned char B_filt[image_height][image_width]) 
	{
		Convolve3_3(R, R_filt, kernel_gauss);
		Convolve3_3(G, G_filt, kernel_gauss);
		Convolve3_3(B, B_filt, kernel_gauss);
	}

	//颜色过滤函数
	void ColorFilter(unsigned char R_filt[image_height][image_width], unsigned char G_filt[image_height][image_width], 
		unsigned char B_filt[image_height][image_width],unsigned char ColorMask[image_height][image_width], 
		unsigned char color_threshold)
	{
		for (int x = 0; x < image_height; x++) {
			for (int y = 0; y < image_width; y++) {
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
	void EdgeSobel(unsigned char ColorMask[image_height][image_width], 
		unsigned char EdgeMask[image_height][image_width], double edge_threshold) 
	{
		for (int x = 0; x < image_height - 2; x++) {
			for (int y = 0; y < image_width - 2; y++) {
				double value = 0;
				int value_h = 0;
				int value_v = 0;
				for (int k = 0; k < 3; k++) {
					for (int l = 0; l < 3; l++) {
						value_h += ((int)ColorMask[x + k + 1][y + l + 1] * kernel_edge_h[k][l]);//水平边缘检测
						value_v += ((int)ColorMask[x + k + 1][y + l + 1] * kernel_edge_v[k][l]);//垂直边缘检测
					}
				}
				value += sqrt(pow(value_h ,2) + pow(value_v , 2));
				if (value > edge_threshold) {
					EdgeMask[x + 1][y + 1] = 1;
				}
			}
		}
	}

	//寻找圆心和半径
	HTParameter HoughTransform(unsigned char EdgeMask[image_height][image_width],
		int r_min, int r_max, int r_step, bool ht_gauss_filter) 
	{
		HTParameter _HTP, _HTP_MAX;
		_HTP_MAX.max_accu_value = 1;
		for (int r = r_min; r < r_max; r +=r_step) {
			//std::cout << r<< " " << r_step << std::endl;
			_HTP = SubHoughTransform(EdgeMask, r, ht_gauss_filter);
			if (_HTP.max_accu_value > _HTP_MAX.max_accu_value) {
				_HTP_MAX = _HTP;
				_HTP_MAX.r_found = r;
			}
		}
		return _HTP_MAX;
	}

	//霍夫变换
	HTParameter SubHoughTransform(unsigned char EdgeMask[image_height][image_width], 
		int radius, bool ht_gauss_filter)
	{
		//开辟空间
		static unsigned char AccuGrid[image_height][image_width] = {0};
		//初始化ab
		double a, b;
		//设置角度步数
		int step_num = 100;
		//设置初始a,b
		int pre_a = -1;
		int pre_b = -1;
		//设置角度增加步长
		double d_theta =0.20* M_PI / radius;// 0.1 / radius;// 2 * M_PI / (2 * M_PI * radius)  2 * M_PI / 400
		for (int x = 0; x < image_height; x++) {
			for (int y = 0; y < image_width; y++) {
				if (EdgeMask[x][y] > 0) {
					for (double theta = 0; theta < 2 * M_PI; theta+=d_theta) {
						a = x - radius * cos(theta);
						b = y - radius * sin(theta);
						HTAccumulate(AccuGrid, a, b,pre_a, pre_b);
						theta += d_theta;
					}
				}

			}
		}
		HTParameter _HTP = HTGetMaxAccu(AccuGrid, ht_gauss_filter);
		return _HTP;
	}

	//自动在霍夫空间叠加相应点的值
	void HTAccumulate(unsigned char AccuGrid[image_height][image_width], double a, double b,int& pre_a,int& pre_b)
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

	//在霍夫空间找到最大的值
	HTParameter HTGetMaxAccu(unsigned char AccuGrid[image_height][image_width], bool ht_gauss_filter) {
		static unsigned char AccuGrid_filt[image_height][image_width] = { 0 };
		//低通滤波，高斯滤波
		if (ht_gauss_filter) {
			//开辟空间
			Convolve3_3(AccuGrid, AccuGrid_filt, kernel_gauss);
		}
		//不进行低通滤波
		else {
			memcpy(AccuGrid_filt, AccuGrid, sizeof(AccuGrid));
		}
		//定义霍夫变换参数
		HTParameter _HTP = { 0 };
		//定义临时值
		int temp;
		//定义初始最大值
		int max = 1;
		/*		int max_repeat = 1;
		int offset_x = 0;
		int offset_y = 0;*/


		for (int x = 0; x < image_height; x++) {
			for (int y = 0; y < image_width; y++) {
				temp = AccuGrid_filt[x][y];
				if (temp < 2)continue;
				/*				if (temp == max)
				{
					max_repeat++;
					offset_x += (x - _HTP.x_found);
					offset_y += (y - _HTP.y_found);
				}
				else
				{
					max_repeat = 1;
					offset_x = 0;
					offset_y = 0;
				}*/
				if (temp > max) {
					max = temp;
					//储存数据到_HTP
					_HTP.x_found = x;
					_HTP.y_found = y;
					_HTP.max_accu_value = max;
				}
			}
			/*		if (max_repeat>1)
			{
				_HTP.x_found += offset_x / max_repeat;
				_HTP.y_found += offset_y / max_repeat;
			}*/
		}
		return _HTP;
	}

	//综合处理函数
	HTParameter GetCircle(unsigned char R[image_height][image_width],
		unsigned char G[image_height][image_width], unsigned char B[image_height][image_width],
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

		/*Print<256, 256>(R_filt);
		Print<256, 256>(G_filt);
		Print<256, 256>(B_filt);
		Print<256, 256>(ColorMask);
		Print<256, 256>(EdgeMask);*/
		return Result;
	}
};



class TEST {
public:
	CVX<256, 256> cvx;

	void CreateCircle(int a, int b, int r, unsigned char Rt[256][256], 
		unsigned char  Gt[256][256], unsigned char Bt[256][256]) 
	{
		for (int x = 0; x < cvx.height; x++) {
			for (int y = 0; y < cvx.width; y++) {
				double _r = pow(r, 2) -(pow(a-x, 2) + pow(b-y, 2)) ;
				if (_r>0) {
					Rt[x][y] = 255;
					Gt[x][y] = 255;
				}
			}
		}
	}

	void Run(int a, int b, int r, unsigned char Rt[256][256],
		unsigned char  Gt[256][256], unsigned char Bt[256][256])
	{
		CreateCircle(a, b, r, Rt, Gt, Bt);
		cvx.GetCircle(Rt, Gt, Bt, 80, 4, true, 12, 32, 2);
		std::cout << "x:	" << cvx.Result.x_found << "	y:	" << cvx.Result.y_found << "	z:	" << cvx.Result.r_found << std::endl;
	}

};


int main()
{
	static unsigned char Rt[256][256] = { 0 };
	static unsigned char Gt[256][256] = { 0 };
	static unsigned char Bt[256][256] = { 0 };
	TEST test;
	test.Run(60,190,17,Rt,Gt,Bt);
	//Print<256, 256>(Rt);

}




















// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门提示: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件


	/*	void Input(unsigned char* input_image) {
		for (int x = 0; x < image_height; x++) {
			for (int y = 0; y < image_width; y++) {
				R[x][y] = input_image[x];
			}
		}

		unsigned char** _R, unsigned char** _G, unsigned char** _B,
		unsigned char** R_filt, unsigned char** G_filt, unsigned char** B_filt, unsigned char kernel_gauss[3][3]
	}*/


	/*
		unsigned char** a, ** b, ** c;
		a = NULL;
		b = NULL;
		c = NULL;


		CVX cvx(256,256);
		//int b[10][10] = {0};

		int** p;
		p = (int**)malloc(100 * sizeof(int*));
		for (int i = 0; i < 100; i++) {
			p[i]= (int*)malloc(100 * sizeof(int));
		}
		for (int i = 0; i < 100;i++) {
			for (int ii = 0; ii < 100; ii++) {
				p[i][ii] = (int)(i * 10 + ii);
			}
		}

		Print(cvx.R_filt);

		std::cout << "Hello World!\n";




			CVX cvx;
	unsigned char** Rt, ** Gt, ** Bt;
	int a = 60;
	int b = 60;
	int r = 24;
	Rt = cvx.GetMemory();
	Gt = cvx.GetMemory();
	Bt = cvx.GetMemory();
	for (int x = 0; x < cvx.image_height; x++) {
		for (int y = 0; y < cvx.image_width; y++) {
			if (((a - x) ^ 2 + (b - y) ^ 2) <= (r ^ 2)) {
				Rt[x][y] = 255;
				Gt[x][y] = 255;
			}
		}
	}
	cvx.GetCircle(Rt, Gt, Bt, 80, 800, true, 2, 30, 2);
	std::cout << "x:	" << cvx.Result.x_found << "	y:	" << cvx.Result.y_found << "	z:	" << cvx.Result.r_found << std::endl;

	*/