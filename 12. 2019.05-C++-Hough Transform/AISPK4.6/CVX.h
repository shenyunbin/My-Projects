//AISPK4.6

#pragma once

#include <iostream>
#include<cmath>
#include <cstring>

//define Pi
constexpr double M_2PI = 6.28318530717;
constexpr double M_1_4PI = 0.7853981633;

//houghtransform parameter
struct HTParameter
{
	int r_found;
	int x_found;
	int y_found;
	int max_accu_value;
};

//class cvx
template<int image_height, int image_width>
class CVX {
private:
	//define temporary image for accumulation grid in the step houghtransform
	struct TempImageData
	{
		//houghtransform outputed data
		unsigned char AccuGrid[image_height][image_width] = { 0 };
		unsigned char AccuGrid_filt[image_height][image_width] = { 0 };
	};

public:
	//define each image data
	struct ImageData
	{
		//define double pointer
		unsigned char** R, ** G, ** B, ** R_filt, ** G_filt, ** B_filt, ** ColorMask, ** EdgeMask;
		//initialize the double pointer
		ImageData()
		{
			MallocPtr(R);
			MallocPtr(G);
			MallocPtr(B);
			MallocPtr(R_filt);
			MallocPtr(G_filt);
			MallocPtr(B_filt);
			MallocPtr(ColorMask);
			MallocPtr(EdgeMask);
		}
		//get the memory for pointer
		void MallocPtr(unsigned char**& ptr) {
			ptr = new unsigned char* [image_height];
			for (int x = 0; x < image_height; x++)ptr[x] = new unsigned char [image_width] ();
		}
		//delete the double pointer
		void DeletePtr(unsigned char**& ptr)
		{
			for (int x = 0; x < image_height; x++)delete ptr[x];
			delete ptr;
			ptr = NULL;
		}
		//delete the double pointer
		~ImageData()
		{
			DeletePtr(R);
			DeletePtr(G);
			DeletePtr(B);
			DeletePtr(R_filt);
			DeletePtr(G_filt);
			DeletePtr(B_filt);
			DeletePtr(ColorMask);
			DeletePtr(EdgeMask);
		}
	};

	//define height and width
	int height = image_height;
	int width = image_width;
	//Kernel for convolve
	const int kernel_averange[3][3] = { {1,1,1}, {1,1,1}, {1,1,1} };
	const int kernel_gauss[3][3] = { {1,2,1}, {2,4,2}, {1,2,1} };
	const int kernel_edge_h[3][3] = { {1,0,-1}, {2,0,-2}, {1,0,-1} };
	const int kernel_edge_v[3][3] = { {1,2,1}, {0,0,0}, {-1,-2,-1} };

	//define the image data and the temporary data
	ImageData* Data;
	TempImageData* TempData;
	//define the result of the ball
	HTParameter Result = { 0 };

	//initialize the memory
	CVX()
	{
		Data = new ImageData;
		TempData = new TempImageData;
	}

	//initialize the image
	template<class T>
	void InitImage(T& image, const unsigned char value = 0)
	{
		for (int x = 0; x < image_height; x++)
		{
			for (int y = 0; y < image_width; y++)
			{
				image[x][y] = value;
			}
		}
	}

	//3*3convolve calculation
	template<class T1, class T2>
	void ConvolveFilter3_3(T1& image, T2& image_filt, const int kernel[3][3], const int &boundary_condition)
	{
		int divisor=0;
		for (int k = 0; k < 3; k++)
			for (int l = 0; l < 3; l++)
				divisor += (int)kernel[k][l];
		double multiple = 1.0 / divisor;

		int start, x_end, y_end;
		if (boundary_condition == -2) 
		{
			start = 0;
			x_end = image_height -2;
			y_end = image_width - 2;
		}
		else
		{
			start = -1;
			x_end = image_height - 1;
			y_end = image_width - 1;
		}
		int sum;
		for (int x = start; x < x_end; x++)
		{
			for (int y = start; y < y_end; y++)
			{
				SubConvolve3_3(image, x, y, boundary_condition,kernel, sum);
				image_filt[x + 1][y + 1] = (int)(sum * multiple + 0.5);
			}
		}
	}

	//get the image pixel with boundary condition
	template<class T>
	void SubConvolve3_3(T& image, const int &x,const int &y,
		const int &boundary_condition, const int kernel[3][3], int& sum)
	{
		int temp_x, temp_y;
		int _sum = 0;
		switch (boundary_condition)
		{
		case -2:	//do nothing
			for (int k = 0; k < 3; k++) 
				for (int l = 0; l < 3; l++) 
					_sum+=(int)image[x+k][y+l] * (int)kernel[k][l];
			sum = _sum;
			return;
			break;
		case -1:	//boundary condition, copy the boundary value
			for (int k = 0; k < 3; k++)
			{
				for (int l = 0; l < 3; l++)
				{
					temp_x = x + k;
					temp_y = y + l;
					//when x is out of the boundary, and return the boundary value of x
					if (x >= image_height)
						temp_x = image_height - 1;
					else if (x < 0)
						temp_x = 0;
					else
						temp_x = x;
					//when y is out of the boundary, return the boundary value of y
					if (y >= image_width)
						temp_y = image_width - 1;
					else if (y < 0)
						temp_y = 0;
					else
						temp_y = y;
					_sum += (int)image[temp_x][temp_y] * (int)kernel[k][l];
				}
			}
			sum = _sum;
			return;
			break;
		default:	//boundary condition, return the determined value 
			for (int k = 0; k < 3; k++)
			{
				for (int l = 0; l < 3; l++)
				{
					temp_x = x + k;
					temp_y = y + l;
					if ((temp_x >= image_height) || (temp_x < 0) || (temp_y >= image_width) || (temp_y < 0))
						_sum += boundary_condition * (int)kernel[k][l];
					else
						_sum += (int)image[temp_x][temp_y] * (int)kernel[k][l];
				}
			}
			sum = _sum;
			return;
			break;
		}
	}

	//CNN low pass filter, gauss filter
	template<class T1, class T2>
	void GaussFilter(T1& R, T1& G, T1& B, T2& R_filt, T2& G_filt, T2& B_filt, 
		const int kernel[3][3], const int boundary_condition)
	{
		ConvolveFilter3_3(R, R_filt, kernel, boundary_condition);
		ConvolveFilter3_3(G, G_filt, kernel, boundary_condition);
		ConvolveFilter3_3(B, B_filt, kernel, boundary_condition);
	}

	//color filter
	template<class T1, class T2>
	void ColorFilter(T1& R_filt, T1& G_filt, T1& B_filt, T2& ColorMask, const unsigned char color_threshold)
	{
		for (int x = 0; x < image_height; x++)
		{
			for (int y = 0; y < image_width; y++)
			{
				ColorMask[x][y] = ColorCompare(R_filt[x][y], G_filt[x][y], B_filt[x][y], color_threshold);
			}
		}
	}

	//color extractor for color filter
	unsigned char ColorCompare(unsigned char& R, unsigned char& G, unsigned char& B, 
		const unsigned char color_threshold)
	{
		return ((abs(R - G) > color_threshold) || (abs(B - G) > color_threshold) || (abs(R - B) > color_threshold)) ? 0 : 1;
	}

	//edgesobel
	template<class T1, class T2>
	void EdgeSobel(T1& ColorMask, T2& EdgeMask, const double edge_threshold, 
		const int boundary_condition)
	{
		int start, x_end, y_end;
		if (boundary_condition == -2)
		{
			start = 0;
			x_end = image_height - 2;
			y_end = image_width - 2;
		}
		else
		{
			start = -1;
			x_end = image_height - 1;
			y_end = image_width - 1;
		}
		double value;
		int value_h,value_v, pixel_value;
		for (int x = start; x < x_end; x++)
		{
			for (int y = start; y < y_end; y++)
			{
				value = 0;
				SubConvolve3_3(ColorMask, x, y, boundary_condition, kernel_edge_h, value_h);
				SubConvolve3_3(ColorMask, x, y, boundary_condition, kernel_edge_v, value_v);
				value = sqrt(pow(value_h, 2) + pow(value_v, 2));
				if (value > edge_threshold) 
				{
					EdgeMask[x + 1][y + 1] = 1;
				}
			}
		}
	}

	//HoughTransform, find the radius and position of the ball
	template<class T>
	HTParameter HoughTransform(T &EdgeMask, const int r_min, const int r_max, const int r_step,
		const int kernel[3][3]=kernel_gauss, const int boundary_condition=-1)
	{
		HTParameter _HTP, _HTP_MAX;
		_HTP_MAX.max_accu_value = 1;
		for (int r = r_min; r < r_max; r += r_step)
		{
			_HTP = SubHoughTransform(EdgeMask, r, kernel, boundary_condition);
			if (_HTP.max_accu_value > _HTP_MAX.max_accu_value)
			{
				_HTP_MAX = _HTP;
				_HTP_MAX.r_found = r;
			}
		}
		return _HTP_MAX;
	}

	//HoughTransform, for determined radius
	template<class T>
	HTParameter SubHoughTransform(T &EdgeMask, 
		int radius, const int kernel[3][3], const int boundary_condition)
	{
		//initialize the image AccuGrid
		InitImage(TempData->AccuGrid);
		//initialize the a, b
		double a, b;
		int pre_a = -1;
		int pre_b = -1;
		//set the d_theta
		double d_theta = M_1_4PI / radius;
		for (int x = 0; x < image_height; x++)
		{
			for (int y = 0; y < image_width; y++)
			{
				if (EdgeMask[x][y] > 0)
				{
					for (double theta = 0; theta < M_2PI; theta += d_theta)
					{
						a = x - radius * cos(theta);
						b = y - radius * sin(theta);
						HTAccumulate(TempData->AccuGrid, a, b, pre_a, pre_b);
						theta += d_theta;
					}
				}

			}
		}
		HTParameter _HTP = HTGetMaxAccu(TempData->AccuGrid, kernel, boundary_condition);
		return _HTP;
	}

	//accmulate in the ht zone
	template<class T>
	void HTAccumulate(T &AccuGrid, double a, double b, int& pre_a, int& pre_b)
	{
		int round_a = int(a + 0.5);
		int round_b = int(b + 0.5);
		if ((round_a == pre_a) && (round_b == pre_b))
			return;
		if ((round_a >= image_height) || (round_b >= image_width))
			return;
		AccuGrid[round_a][round_b] += 1;
		pre_a = round_a;
		pre_b = round_b;
	}

	//get the maximal value in the ht zone
	template<class T>
	HTParameter HTGetMaxAccu(T &AccuGrid, const int kernel[3][3], const int boundary_condition)
	{
		//initialize the image AccuGrid_filt
		InitImage(TempData->AccuGrid_filt);
		//filter
		ConvolveFilter3_3(AccuGrid, TempData->AccuGrid_filt, kernel, boundary_condition);
		//define ht parameter
		HTParameter _HTP = { 0 };
		//define the temporary maximal value
		int temp;
		//define the maximal value
		int max = 2;
		int x_sum = 0;
		int y_sum = 0;
		int max_count = 1;
		for (int x = 0; x < image_height; x++)
		{
			for (int y = 0; y < image_width; y++)
			{
				temp = TempData->AccuGrid_filt[x][y];
				if (temp < max)
					continue;
				if (temp > max) 
				{
					max = temp;
					max_count = 1;
					x_sum = x;
					y_sum = y;
				}
				else// if (temp == max)
				{
					max_count++;
					x_sum += x;
					y_sum += y;
				}
			}
		}
		//storage the parameter in _HTP
		_HTP.max_accu_value = max;
		_HTP.x_found = int(1.0 * x_sum / max_count + 0.5);
		_HTP.y_found = int(1.0 * y_sum / max_count + 0.5);
		return _HTP;
	}

	//find the ball
	template<class T>
	HTParameter HTFindBall(T &R, T &G, T &B,
		/*main parameters*/
		const unsigned char color_threshold = 80, const double edge_threshold = 2,
		const int r_min = 2, const int r_max = 30, const int r_step = 2,
		/*other parameters, filter parameters*/
		const int gaussfilter_kernel[3][3]=kernel_gauss, const int gaussfilter_boundary_condition=-1,
		const int edgesobel_boundary_condition = -1,
		const int ht_gaussfilter_kernel[3][3]= kernel_gauss, const int ht_boundary_condition = -1)
	{
		GaussFilter(R, G, B, Data->R_filt, Data->G_filt, Data->B_filt, gaussfilter_kernel, gaussfilter_boundary_condition);
		ColorFilter(Data->R_filt, Data->G_filt, Data->B_filt, Data->ColorMask, color_threshold);
		EdgeSobel(Data->ColorMask, Data->EdgeMask, edge_threshold, edgesobel_boundary_condition);
		Result = HoughTransform(Data->EdgeMask, r_min, r_max, r_step, ht_gaussfilter_kernel, ht_boundary_condition);
		return Result;
	}

	//free the memory
	~CVX()
	{
		delete Data;
		delete TempData;
	}
};






/*
	//get the image pixel with boundary condition
	template<class T>
	void SubConvolve3_3(T& image, const int& x, const int& y,
		const int& boundary_condition,
		const int kernel1[3][3], int& sum1, const int kernel2[3][3], int& sum2)
	{
		int temp_x, temp_y,temp_pixel;
		int _sum1 = 0;
		int _sum2 = 0;
		switch (boundary_condition)
		{
		case -2:	//do nothing
			for (int k = 0; k < 3; k++)
			{
				for (int l = 0; l < 3; l++)
				{
					temp_pixel = image[x + k][y + l];
					_sum1 += temp_pixel * (int)kernel1[k][l];
					_sum2 += temp_pixel * (int)kernel2[k][l];
				}
			}
			sum1 = _sum1;
			sum2 = _sum2;
			return;
			break;
		case -1:	//boundary condition, copy the boundary value
			for (int k = 0; k < 3; k++)
			{
				for (int l = 0; l < 3; l++)
				{
					temp_x = x + k;
					temp_y = y + l;
					//when x is out of the boundary, and return the boundary value of x
					if (x >= image_height)
						temp_x = image_height - 1;
					else if (x < 0)
						temp_x = 0;
					else
						temp_x = x;
					//when y is out of the boundary, return the boundary value of y
					if (y >= image_width)
						temp_y = image_width - 1;
					else if (y < 0)
						temp_y = 0;
					else
						temp_y = y;
					_sum1 += (int)image[temp_x][temp_y] * (int)kernel1[k][l];
					_sum2 += (int)image[temp_x][temp_y] * (int)kernel2[k][l];

				}
			}
			sum1 = _sum1;
			sum2 = _sum2;
			return;
			break;
		default:	//boundary condition, return the determined value
			for (int k = 0; k < 3; k++)
			{
				for (int l = 0; l < 3; l++)
				{
					temp_x = x + k;
					temp_y = y + l;
					if ((temp_x >= image_height) || (temp_x < 0) || (temp_y >= image_width) || (temp_y < 0))
					{
						_sum1 += boundary_condition * (int)kernel1[k][l];
						_sum2 += boundary_condition * (int)kernel2[k][l];
					}
					else
					{
						_sum1 += (int)image[temp_x][temp_y] * (int)kernel1[k][l];
						_sum2 += (int)image[temp_x][temp_y] * (int)kernel2[k][l];
					}

				}
			}
			sum1 = _sum1;
			sum2 = _sum2;
			return;
			break;
		}
	}

					SubConvolve3_3(ColorMask, x, y, boundary_condition,
					kernel_edge_h, value_h, kernel_edge_v, value_v);

*/