// AISPK4.5.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//10.05.19

#include <iostream>
#include"CVX.h"


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
		std::cout << std::endl;
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


//主函数
int main()
{
	//模拟输入数据
	static unsigned char Rt[256][256] = { 0 };
	static unsigned char Gt[256][256] = { 0 };
	static unsigned char Bt[256][256] = { 0 };
	//画圆，模拟小球
	CreateCircle<256, 256>(90, 140, 25, Rt, Gt, Bt);



	//初始化CVX
	CVX<256, 256> cvx;
	//进行霍夫变换获小球的位置和半径，具体参数如下
	//R，G，B，unsigned char color_threshold = 80, double edge_threshold = 4,
	//ht_gauss_filter = true，int r_min = 2, int r_max = 32, int r_step = 2
	cvx.HTFindBall(Rt, Gt, Bt, 80, 4, true, 12, 32, 2);

	//输出结果
	std::cout << "检测到的圆心和半径: " << std::endl << std::endl;
	std::cout << "x:	" << cvx.Result.x_found << "	y:	" << cvx.Result.y_found << "	radius:	" << cvx.Result.r_found << std::endl;
	std::cout << std::endl << std::endl;

	//输出各个处理后的图像，图方便，用字符输出了
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