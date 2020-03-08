// AISPK4.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include<cmath>

#define M_PI 3.141592653589793238462643383279502884L/* pi */

//霍夫变换参数
struct HTParameter {
	int r_found;
	int x_found;
	int y_found;
	int max_accu_value;
};


class CVX {
public:
	unsigned char **R_filt, **G_filt, **B_filt, **ColorMask, **EdgeMask;

	//设置图片宽度和高度
	int image_width=256;
	int image_height=256;

	//卷积使用的Kernel
	int kernel_gauss[3][3] = { {1,1,1},	{1,1,1},	{1,1,1} }; //低通滤波CNN
	int kernel_edge_h[3][3] = { {1,0,-1},	{2,0,-2},	{1,0,-1} };//水平边缘检测CNN
	int kernel_edge_v[3][3] = { {1,2,1},	{0,0,0},	{-1,-2,-1} };//垂直边缘检查CNN

	//定义结果
	HTParameter Result;

	//初始化函数，用于分配内存
	CVX(int _image_width = 256, int _image_height = 256) {
		image_width = _image_width;
		image_height = _image_height;
		R_filt = GetMemory();
		G_filt = GetMemory();
		B_filt = GetMemory();
		ColorMask = GetMemory();
		EdgeMask = GetMemory();
	}

	//动态分配内存
	unsigned char** GetMemory(unsigned char initial_value=0,bool initial = true) {
		unsigned char** p;
		p = (unsigned char**)calloc(initial_value,image_height *8*16);//因为双重指针，储存地址 sizeof(unsigned char*)
		for (int i = 0; i < image_height; i++) {
			p[i] = (unsigned char*)calloc(initial_value,image_width*8);// * sizeof(unsigned char) sizeof(unsigned char)
		}
		/*
		if (initial == true) {
			for (int x = 0; x < image_height; x++) {
				for (int y = 0; y < image_width; y++) {
					p[x][y] = (unsigned char)initial_value;
				}
			}
		}
		*/
		return p;
	}

	//删除指针，释放内存
	void Free(unsigned char** p) {
		for (int i = 0; i < image_height; i++) {
			delete(p[i]);
		}
		delete(p);
	}

	//3*3卷积计算
	void Convolve3_3(unsigned char** image, unsigned char** image_filt, int kernel[3][3]) {
		for (int x = 0; x < image_height-2; x++) {
			for (int y = 0; y < image_width-2; y++) {
				unsigned char sum = 0;
				for (int k = 0; k < 3; k++) {
					for (int l = 0; l < 3; l++) {
						sum+= image[x + k+1][y + l+1]* kernel[k][l];
					}
				}
				image_filt[x+1][y+1] = 1 / 9 * sum;
			}
		}
	}

	//CNN低通滤波，高斯滤波
	void GaussFilter(unsigned char** R, unsigned char** G, unsigned char** B,
		unsigned char** R_filt, unsigned char** G_filt, unsigned char** B_filt) {
		Convolve3_3(R,R_filt, kernel_gauss);
		Convolve3_3(G, G_filt, kernel_gauss);
		Convolve3_3(B, B_filt, kernel_gauss);
	}

	//颜色过滤函数
	void ColorFilter(unsigned char** R_filt, unsigned char** G_filt, unsigned char** B_filt,
		unsigned char ** ColorMask, unsigned char color_threshold = 80) {
		for (int x = 0; x < image_height; x++) {
			for (int y = 0; y < image_width; y++) {
				ColorMask[x][y]=ColorCompare(R_filt[x][y], G_filt[x][y], B_filt[x][y], color_threshold);
			}
		}
	}

	//三种颜色比较并得出结果
	unsigned char ColorCompare(unsigned char R, unsigned char G,unsigned char B, unsigned char color_threshold) {
		return ((abs(R - G) > color_threshold) || (abs(B - G) > color_threshold) || (abs(R - B) > color_threshold)) ? 0 : 1;
	}

	//边缘检测
	void EdgeSobel(unsigned char** ColorMask, unsigned char** EdgeMask, double edge_threshold=800) {
		for (int x = 0; x < image_height - 2; x++) {
			for (int y = 0; y < image_width - 2; y++) {
				double value = 0;
				int value_h = 0;
				int value_v = 0;
				for (int k = 0; k < 3; k++) {
					for (int l = 0; l < 3; l++) {
						value_h += (ColorMask[x + k + 1][y + l + 1] * kernel_edge_h[k][l]);//水平边缘检测
						value_v += (ColorMask[x + k + 1][y + l + 1] * kernel_edge_v[k][l]);//垂直边缘检测
					}
				}
				value += sqrt(value_h ^ 2	 + value_v ^ 2);
				EdgeMask[x + 1][y + 1] =  value > edge_threshold ?  1 :  0;//语法不是很确定，还有01输出是否要取反
			}
		}
	}

	//寻找圆心和半径
	HTParameter HoughTransform(unsigned char** EdgeMask, int r_min=2,int r_max=30,int r_step=2, bool ht_gauss_filter = true) {
		HTParameter _HTP,_HTP_MAX;
		_HTP_MAX.max_accu_value = 1;
		for (int r = r_min; r < r_max;r=+r_step) {
			_HTP = SubHoughTransform(EdgeMask,r, ht_gauss_filter);
			if (_HTP.max_accu_value > _HTP_MAX.max_accu_value) {
				_HTP_MAX = _HTP;
				_HTP_MAX.r_found = r;
			}
		}
		return _HTP_MAX;
	}

	//霍夫变换
	HTParameter SubHoughTransform(unsigned char** EdgeMask, int radius, bool ht_gauss_filter = true) {
		//开辟空间
		unsigned char** AccuGrid;
		AccuGrid = GetMemory(0,true);
		//初始化ab
		double a, b;
		//设置角度步数
		int step_num = 100;
		//设置初始角度
		double theta = 0;
		//设置角度增加步长
		double d_theta = 2 * M_PI / 100;
		for (int x = 0; x < image_height; x++) {
			for (int y = 0; y < image_width; y++) {
				if (EdgeMask[x][y] > 0) {
					for (int i = 0; i < step_num; i++) {
						a = x - radius * cos(theta);
						b = y - radius * sin(theta);
						HTAccumulate(AccuGrid, a, b);
						theta += d_theta;
					}
				}

			}
		}
		HTParameter _HTP = HTGetMaxAccu(AccuGrid, ht_gauss_filter);
		Free(AccuGrid);
		return _HTP;
	}

	//自动在霍夫空间叠加相应点的值
	void HTAccumulate(unsigned char**AccuGrid,double a,double b) {
		 int round_a = int(a+0.5);//四舍五入
		 int round_b = int(b+0.5);
		if ((round_a >= image_height) || (round_b >= image_width))return;
		else {
			AccuGrid[round_a][round_b] += 1;
		}
	}

	//在霍夫空间找到最大的值
	HTParameter HTGetMaxAccu(unsigned char** AccuGrid, bool ht_gauss_filter=true) {
		unsigned char** AccuGrid_filt;
		//低通滤波，高斯滤波
		if (ht_gauss_filter) {
			//开辟空间
			AccuGrid_filt = GetMemory(0, true);
			Convolve3_3(AccuGrid, AccuGrid_filt, kernel_gauss);
		}
		//不进行低通滤波
		else {
			AccuGrid_filt=AccuGrid;
		}
		//定义霍夫变换参数
		HTParameter _HTP = {0};
		//定义临时值
		int temp;
		//定义初始最大值
		int max=1;
		for (int x = 0; x < image_height; x++) {
			for (int y = 0; y < image_width; y++) {
				temp = AccuGrid_filt[x][y];
				if (temp > max) {
					max = temp;
					//储存数据到_HTP
					_HTP.x_found = x;
					_HTP.y_found = y;
					_HTP.max_accu_value = max;
				}
			}
		}
		//释放内存
		if (ht_gauss_filter) Free(AccuGrid_filt);
		return _HTP;
	}

	//综合处理函数
	HTParameter GetCircle(unsigned char** R, unsigned char** G, unsigned char** B, 
		unsigned char color_threshold = 80,
		double edge_threshold = 800,  bool ht_gauss_filter = true,
		int r_min = 2, int r_max = 30, int r_step = 2) {
		GaussFilter(R, G, B, R_filt, G_filt, B_filt);
		ColorFilter(R_filt, G_filt, B_filt, ColorMask, color_threshold);
		EdgeSobel(ColorMask, EdgeMask, edge_threshold);
		return Result = HoughTransform(EdgeMask, r_min, r_max, r_step, ht_gauss_filter);
	}
};



class TEST {
public:
	CVX cvx;
	unsigned char** Rt, ** Gt, ** Bt;

	void CreateCircle(int a,int b,int r, unsigned char** Rt, unsigned char**Gt, unsigned char**Bt) {
		Rt = cvx.GetMemory();
		Gt = cvx.GetMemory();
		Bt = cvx.GetMemory();
		for (int x = 0; x < cvx.image_height; x++) {
			for (int y = 0; y < cvx.image_width; y++) {
				if (((a -x)^ 2 + (b-y) ^ 2)<=(r^2)) {
					Rt[x][y] = 255;
					Gt[x][y] = 255;
				}
			}
		}
	}

	void Run(int a, int b, int r) {
		CreateCircle(a, b, r, Rt, Gt, Bt);
		cvx.GetCircle(Rt, Gt, Bt,80,800,  true,2,30,2);
		std::cout<<"x:	" << cvx.Result.x_found << "	y:	" << cvx.Result.y_found << "	z:	" << cvx.Result.r_found<<std::endl;
	}

};



void Print(unsigned char **a) {
	for (int i = 0; i < 100; i++) {
		std::cout << a[i][i];
	}
}



int main()
{
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

*/