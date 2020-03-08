// AISPK4.5.cpp : ���ļ����� "main" ����������ִ�н��ڴ˴���ʼ��������
//10.05.19

#include <iostream>
#include"CVX.h"


//���º�����������ʹ��

//�򵥴�ӡ����
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

//��Բ
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


//������
int main()
{
	//ģ����������
	static unsigned char Rt[256][256] = { 0 };
	static unsigned char Gt[256][256] = { 0 };
	static unsigned char Bt[256][256] = { 0 };
	//��Բ��ģ��С��
	CreateCircle<256, 256>(90, 140, 25, Rt, Gt, Bt);



	//��ʼ��CVX
	CVX<256, 256> cvx;
	//���л���任��С���λ�úͰ뾶�������������
	//R��G��B��unsigned char color_threshold = 80, double edge_threshold = 4,
	//ht_gauss_filter = true��int r_min = 2, int r_max = 32, int r_step = 2
	cvx.HTFindBall(Rt, Gt, Bt, 80, 4, true, 12, 32, 2);

	//������
	std::cout << "��⵽��Բ�ĺͰ뾶: " << std::endl << std::endl;
	std::cout << "x:	" << cvx.Result.x_found << "	y:	" << cvx.Result.y_found << "	radius:	" << cvx.Result.r_found << std::endl;
	std::cout << std::endl << std::endl;

	//�������������ͼ��ͼ���㣬���ַ������
	std::cout << "ptr_R_filt - ָ��	ͼ��:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ptr_R_filt);
	std::cout << "R_filt - ����	ͼ��:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->R_filt);
	std::cout << "ptr_G_filt - ָ��	ͼ��:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ptr_G_filt);
	std::cout << "G_filt - ����	ͼ��:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->G_filt);
	std::cout << "ptr_B_filt - ָ��	ͼ��:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ptr_B_filt);
	std::cout << "B_filt - ����	ͼ��:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->B_filt);
	std::cout << "ptr_ColorMask - ָ��	ͼ��:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ptr_ColorMask);
	std::cout << "ColorMask - ����	ͼ��:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ColorMask);
	std::cout << "ptr_EdgeMask - ָ��	ͼ��:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->ptr_EdgeMask);
	std::cout << "EdgeMask - ����	ͼ��:	" << std::endl << std::endl;
	Print<256, 256>(cvx.Data->EdgeMask);
}