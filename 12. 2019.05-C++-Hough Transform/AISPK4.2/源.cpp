// AISPK4.2.cpp : ���ļ����� "main" ����������ִ�н��ڴ˴���ʼ��������
//

#include <iostream>
#include<cmath>
#include <cstring>



constexpr auto M_PI = 3.141592653589793238462643383279502884L/* pi */;

//����任����
struct HTParameter 
{
	int r_found;
	int x_found;
	int y_found;
	int max_accu_value;
};

template<int image_width, int image_height>
class CVX {
private:
	//������ʱ����
	struct TempImageData
	{
		//����ռ��ۼ�����
		unsigned char AccuGrid[image_height][image_width] = { 0 };
		unsigned char AccuGrid_filt[image_height][image_width] = { 0 };
	};

public:
	//�������ͼ������
	struct ImageData
	{
		//ԭʼRGB����
		unsigned char R[image_height][image_width] = { 0 };
		unsigned char G[image_height][image_width] = { 0 };
		unsigned char B[image_height][image_width] = { 0 };
		//���˺�RGB����
		unsigned char R_filt[image_height][image_width] = { 0 };
		unsigned char G_filt[image_height][image_width] = { 0 };
		unsigned char B_filt[image_height][image_width] = { 0 };
		//��ɫ���˺�����
		unsigned char ColorMask[image_height][image_width] = { 0 };
		//��Ե���������
		unsigned char EdgeMask[image_height][image_width] = { 0 };
	};

	//����߶ȿ��
	int width = image_width;
	int height = image_height;
	//���ʹ�õ�Kernel
	int kernel_gauss[3][3] = { {1,1,1},	{1,1,1},	{1,1,1} }; //��ͨ�˲�CNN
	int kernel_edge_h[3][3] = { {1,0,-1},	{2,0,-2},	{1,0,-1} };//ˮƽ��Ե���CNN
	int kernel_edge_v[3][3] = { {1,2,1},	{0,0,0},	{-1,-2,-1} };//��ֱ��Ե���CNN

	//���崢������
	ImageData* Data;
	TempImageData* TempData;
	//������
	HTParameter Result = { 0 };

	//��ʼ�����������ڷ����ڴ�
	CVX()
	{
		Data = new ImageData;
		TempData = new TempImageData;
	}

	//ͼ���ʼ��������������
	void InitImage(unsigned char image[image_height][image_width], unsigned char value = 0)
	{
		for (int x = 0; x < image_height; x++)
		{
			for (int y = 0; y < image_width; y++)
			{
				image[x][y] = value;
			}
		}
	}

	//����ͼ��1�����أ���˫������ת����
	void InputRGB(unsigned char R[image_height][image_width],
		unsigned char G[image_height][image_width], unsigned char B[image_height][image_width], ImageData* Dt)
	{
		for (int x = 0; x < image_height; x++)
		{
			for (int y = 0; y < image_width; y++)
			{
				Dt->R[x][y] = R[x][y];
				Dt->G[x][y] = G[x][y];
				Dt->B[x][y] = B[x][y];
			}
		}
	}

	//����ͼ��2�����أ���˫��ָ��ת����
	void InputRGB(unsigned char **R,unsigned char **G, unsigned char **B, ImageData* Dt)
	{
		for (int x = 0; x < image_height; x++)
		{
			for (int y = 0; y < image_width; y++)
			{
				Dt->R[x][y] = R[x][y];
				Dt->G[x][y] = G[x][y];
				Dt->B[x][y] = B[x][y];
			}
		}
	}

	//3*3�������
	void Convolve3_3(unsigned char image[image_height][image_width],
		unsigned char image_filt[image_height][image_width], int kernel[3][3])
	{
		for (int x = 0; x < image_height - 2; x++)
		{
			for (int y = 0; y < image_width - 2; y++)
			{
				unsigned int sum = 0;
				for (int k = 0; k < 3; k++) {
					for (int l = 0; l < 3; l++) {
						sum += (unsigned int)image[x + k ][y + l ] * (unsigned int)kernel[k][l];
					}
				}
				image_filt[x + 1][y + 1] = (unsigned int)(sum / 9.0+0.5);
			}
		}
	}

	//CNN��ͨ�˲�����˹�˲�
	void GaussFilter(unsigned char R[image_height][image_width], unsigned char G[image_height][image_width],
		unsigned char B[image_height][image_width], unsigned char R_filt[image_height][image_width],
		unsigned char G_filt[image_height][image_width], unsigned char B_filt[image_height][image_width])
	{
		Convolve3_3(R, R_filt, kernel_gauss);
		Convolve3_3(G, G_filt, kernel_gauss);
		Convolve3_3(B, B_filt, kernel_gauss);
	}

	//��ɫ���˺���
	void ColorFilter(unsigned char R_filt[image_height][image_width], unsigned char G_filt[image_height][image_width],
		unsigned char B_filt[image_height][image_width], unsigned char ColorMask[image_height][image_width],
		unsigned char color_threshold)
	{
		for (int x = 0; x < image_height; x++)
		{
			for (int y = 0; y < image_width; y++)
			{
				ColorMask[x][y] = ColorCompare(R_filt[x][y], G_filt[x][y], B_filt[x][y], color_threshold);
			}
		}
	}

	//������ɫ�Ƚϲ��ó����
	unsigned char ColorCompare(unsigned char R, unsigned char G, unsigned char B, unsigned char color_threshold)
	{
		return ((abs(R - G) > color_threshold) || (abs(B - G) > color_threshold) || (abs(R - B) > color_threshold)) ? 0 : 1;
	}

	//��Ե���
	void EdgeSobel(unsigned char ColorMask[image_height][image_width],
		unsigned char EdgeMask[image_height][image_width], double edge_threshold)
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
						value_h += ((int)ColorMask[x + k ][y + l ] * kernel_edge_h[k][l]);//ˮƽ��Ե���
						value_v += ((int)ColorMask[x + k ][y + l ] * kernel_edge_v[k][l]);//��ֱ��Ե���
					}
				}
				value += sqrt(pow(value_h, 2) + pow(value_v, 2));
				if (value > edge_threshold) {
					EdgeMask[x + 1][y + 1] = 1;
				}
			}
		}
	}

	//Ѱ��Բ�ĺͰ뾶������任���������뾶
	HTParameter HoughTransform(unsigned char EdgeMask[image_height][image_width],
		int r_min, int r_max, int r_step, bool ht_gauss_filter)
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
	
	//ȷ���뾶�Ļ���任
	HTParameter SubHoughTransform(unsigned char EdgeMask[image_height][image_width],
		int radius, bool ht_gauss_filter)
	{
		//��AccuGrid����
		InitImage(TempData->AccuGrid);
		//��ʼ��ab
		double a, b;
		//���ýǶȲ���
		int step_num = 100;
		//���ó�ʼa,b
		int pre_a = -1;
		int pre_b = -1;
		//���ýǶ����Ӳ���
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

	//�Զ��ڻ���ռ������Ӧ���ֵ���Զ�ͶƱ
	void HTAccumulate(unsigned char AccuGrid[image_height][image_width], double a, double b, int& pre_a, int& pre_b)
	{
		int round_a = int(a + 0.5);//��������
		int round_b = int(b + 0.5);
		if ((round_a == pre_a) && (round_b == pre_b))
			return;
		if ((round_a >= image_height) || (round_b >= image_width))
			return;
		AccuGrid[round_a][round_b] += 1;
		pre_a = round_a;
		pre_b = round_b;
	}

	//�ڻ���ռ��ҵ�����ֵ���ҵ�ͶƱ���
	HTParameter HTGetMaxAccu(unsigned char AccuGrid[image_height][image_width], bool ht_gauss_filter) 
	{
		//��AccuGrid_filt����
		InitImage(TempData->AccuGrid_filt);
		//��ͨ�˲�����˹�˲�
		if (ht_gauss_filter) 
		{
			//���ٿռ�
			Convolve3_3(AccuGrid, TempData->AccuGrid_filt, kernel_gauss);
		}
		//�����е�ͨ�˲�
		else
		{
			memcpy(TempData->AccuGrid_filt, AccuGrid, sizeof(AccuGrid));
		}
		//�������任����
		HTParameter _HTP = { 0 };
		//������ʱֵ
		int temp;
		//�����ʼ���ֵ
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
				if (temp ==max) {
					max_count++;
					x_sum += x;
					y_sum += y;
				}
				else if(temp > max)
				{
					max = temp;
					max_count = 1;
					x_sum = x;
					y_sum = y;
				}
			}
		}
		//�������ݵ�_HTP
		_HTP.max_accu_value = max;
		_HTP.x_found = int(1.0 * x_sum / max_count + 0.5);
		_HTP.y_found = int(1.0 * y_sum / max_count + 0.5);
		return _HTP;
	}

	//�ۺϴ�����1-���أ���ά��������
	HTParameter GetCircle(unsigned char R[image_height][image_width],
		unsigned char G[image_height][image_width], unsigned char B[image_height][image_width],
		unsigned char color_threshold = 80,
		double edge_threshold = 2, bool ht_gauss_filter = true,
		int r_min = 2, int r_max = 30, int r_step = 2)
	{
		//����ͼ��Data������ת��
		InputRGB(R,G,B,Data);
		//��˹�˲�
		GaussFilter(R,G, B, Data->R_filt, Data->G_filt, Data->B_filt);
		//��ɫ�˲�
		ColorFilter(Data->R_filt, Data->G_filt, Data->B_filt, Data->ColorMask, color_threshold);
		//��Ե���
		EdgeSobel(Data->ColorMask, Data->EdgeMask, edge_threshold);
		//����任
		Result = HoughTransform(Data->EdgeMask, r_min, r_max, r_step, ht_gauss_filter);
		return Result;
	}


	//�ۺϴ�����2-���أ�˫��ָ������	template<class T>
	HTParameter GetCircle(unsigned char **R,unsigned char **G, unsigned char **B,
		unsigned char color_threshold = 80,
		double edge_threshold = 2, bool ht_gauss_filter = true,
		int r_min = 2, int r_max = 30, int r_step = 2)
	{
		//����ͼ��Data������ת��
		InputRGB(R, G, B, Data);
		//��˹�˲�
		GaussFilter(Data->R, Data->G, Data->B, Data->R_filt, Data->G_filt, Data->B_filt);
		//��ɫ�˲�
		ColorFilter(Data->R_filt, Data->G_filt, Data->B_filt, Data->ColorMask, color_threshold);
		//��Ե���
		EdgeSobel(Data->ColorMask, Data->EdgeMask, edge_threshold);
		//����任
		Result = HoughTransform(Data->EdgeMask, r_min, r_max, r_step, ht_gauss_filter);
		return Result;
	}


	//�����������ͷ��ڴ�
	~CVX()
	{
		delete Data;
		delete TempData;
	}
};






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

class TEST {
public:
	CVX<256, 256> cvx;

	void CreateCircle(int a, int b, int r, unsigned char Rt[256][256],
		unsigned char  Gt[256][256], unsigned char Bt[256][256])
	{
		for (int x = 0; x < cvx.height; x++) 
		{
			for (int y = 0; y < cvx.width; y++) 
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
	test.Run(90, 140, 25, Rt, Gt, Bt);
	//Print<256, 256>(Rt);
			/*Print<256, 256>(R_filt);
		Print<256, 256>(G_filt);
		Print<256, 256>(B_filt);
		Print<256, 256>(ColorMask);
		Print<256, 256>(EdgeMask);*/
}



