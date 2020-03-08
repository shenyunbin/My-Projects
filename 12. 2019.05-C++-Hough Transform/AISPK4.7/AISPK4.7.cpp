// AISPK4.7.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//11.05.19

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


void Test() {
	//模拟输入数据
	static unsigned char Rt[512][512] = { 0 };
	static unsigned char Gt[512][512] = { 0 };
	static unsigned char Bt[512][512] = { 0 };
	//画圆，模拟小球
	CreateCircle<512, 512>(10, 20, 25, Rt, Gt, Bt);



	//initialize the CVX
	CVX<512, 512> cvx;

	/*
	==============================
	Find the loacation and the radius of the Ball
	==============================


	Note:
	---------------------------------------------------
	filter_kernel	=	[	kernel_gauss,	kernel_averange	]
	boundary_condition	=	[	-2 none boundary condition	,		-1	copy boundary	,	>=0 determined value	]
	

	Parameters:
	---------------------------------------------------
	R	,	G	,	B	,	
	color_threshold	=	80	,	edge_threshold	=	4	,	r_min	=	2	, r_max	=	32	,	r_step	=	2	,	

	gaussfilter_kernel	,	gaussfilter_boundary_condition	=	-1	,
	edgesobel_boundary_condition	=	-1	,
	ht_gaussfilter	=	false	, ht_gaussfilter_kernel	,	edgesobel_boundary_condition	=	-1	,
	*/
	cvx.HTFindBall(
		Rt, Gt, Bt, 80, 3, 12, 32, 2,
		cvx.kernel_gauss, -1, -1, 
		false, cvx.kernel_gauss, -1);

	//输出结果
	std::cout << "检测到的圆心和半径: " << std::endl << std::endl;
	std::cout << "x:	" << cvx.Result.x_found << "	y:	" << cvx.Result.y_found << "	radius:	" << cvx.Result.r_found << std::endl;
	std::cout << std::endl << std::endl;

	//输出各个处理后的图像，图方便，用字符输出了
	std::cout << "R_filt - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->R_filt);
	std::cout << "G_filt - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->G_filt);
	std::cout << "B_filt - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->B_filt);
	std::cout << "ColorMask - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ColorMask);
	std::cout << "EdgeMask - 指针	图像:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->EdgeMask);

}

//主函数
int main()
{
	Test();

	std::cout << "endl.....";

}