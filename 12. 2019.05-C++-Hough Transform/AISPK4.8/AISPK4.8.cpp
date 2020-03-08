// AISPK4.7.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//12.05.19
#include <iostream>
#include"FastHT.h"
//print the data
template< int height, int width, class T>
void Print(T a) {
	std::cout << std::endl;
	for (int x = 0; x < height; x += height / 55) {
		for (int y = 0; y < width; y += width / 55) {
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
//draw circle
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
//test
void Test() {
	static unsigned char Rt[512][512] = { 0 };
	static unsigned char Gt[512][512] = { 0 };
	static unsigned char Bt[512][512] = { 0 };
	CreateCircle<512, 512>(190, 98, 15, Rt, Gt, Bt);
	FastHT<512, 512> fht;
	//R	,	G	,	B	,	color_threshold	=	80	,	edge_threshold	=	4	,	r_min	=	2	, r_max	=	32	,	r_step	=	2
	fht.FindBall(Rt, Gt, Bt, 80, 3, 2, 32, 2);
	//output
	std::cout << "检测到的圆心和半径: " << std::endl << std::endl;
	std::cout << "x:	" << fht.Result.x_found << "	y:	" << fht.Result.y_found << "	radius:	" << fht.Result.r_found << std::endl;
	std::cout << std::endl << std::endl;
	//image Output
	std::cout << "R_filt - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(fht.R_filt);
	std::cout << "G_filt - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(fht.G_filt);
	std::cout << "B_filt - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(fht.B_filt);
	std::cout << "ColorMask - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(fht.ColorMask);
	std::cout << "EdgeMask - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(fht.EdgeMask);
}
//main
int main()
{
	Test();
	std::cout << "endl.....";
}