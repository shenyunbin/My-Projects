//AISPK4.8.6	
constexpr double M_2PI = 6.28318530717;
constexpr double M_Theta = 1.5707963268; //0.7853981633
struct HTParameter{ int r_found; int x_found; int y_found; int max_accu_value; };
template<int image_height, int image_width>
class FastHT 
{
public:
	//PARAMETERS////////////////////////////////////////////////////////////
	unsigned char(*R_filt)[image_width] = new unsigned char[image_height][image_width]();
	unsigned char(*G_filt)[image_width] = new unsigned char[image_height][image_width]();
	unsigned char(*B_filt)[image_width] = new unsigned char[image_height][image_width]();
	unsigned char(*ColorMask)[image_width] = new unsigned char[image_height][image_width]();
	unsigned char(*EdgeMask)[image_width] = new unsigned char[image_height][image_width]();
	unsigned char(*AccuGrid)[image_width] = new unsigned char[image_height][image_width]();
	const double kernel_gauss_multiple = 1.0 / 16;
	const int kernel_gauss[3][3] = { {1,2,1}, {2,4,2}, {1,2,1} };
	const double kernel_edge_multiple = 1.0;
	const int kernel_edge_h[3][3] = { {1,0,-1}, {2,0,-2}, {1,0,-1} };
	const int kernel_edge_v[3][3] = { {1,2,1}, {0,0,0}, {-1,-2,-1} };
	const int convolve_start = 1;
	const int convolve_end_x = image_height - 1;
	const int convolve_end_y = image_width - 1;
	HTParameter Result = { 0 };
	//SUB CONVOLVE//////////////////////////////////////////////////////////
	template<class T>
	unsigned char SubConvolve3_3(T& image, const int &x,const int &y, const int kernel[3][3], const double& multiple)
	{
		int sum=0;
		for (int k = 0; k < 3; k++)
			for (int l = 0; l < 3; l++)
				sum += (int)image[x + k-1][y + l-1] * (int)kernel[k][l];
		return (unsigned char)(sum * multiple);
	}
	//GAUSS FILTER/////////////////////////////////////////////////////////////
	template<class T1, class T2>
	void GaussFilter(T1& R, T1& G, T1& B, T2& R_filt, T2& G_filt, T2& B_filt)
	{
		for (int x = convolve_start; x < convolve_end_x; x++)
			for (int y = convolve_start; y < convolve_end_y; y++)
			{
				R_filt[x][y]=SubConvolve3_3(R, x, y, kernel_gauss, kernel_gauss_multiple);
				G_filt[x][y]=SubConvolve3_3(G, x, y, kernel_gauss, kernel_gauss_multiple);
				B_filt[x][y]=SubConvolve3_3(B, x, y, kernel_gauss, kernel_gauss_multiple);
			}
	}
	//COLOR FILTER/////////////////////////////////////////////////////////////
	template<class T1, class T2>
	void ColorFilter(T1& R_filt, T1& G_filt, T1& B_filt, T2& ColorMask, const unsigned char &color_threshold)
	{
		for (int x = 0; x < image_height; x++)
			for (int y = 0; y < image_width; y++)
				ColorMask[x][y] =
					sqrt( pow(R_filt[x][y] - 255, 2) + pow(G_filt[x][y] - 255, 2) + pow(B_filt[x][y] - 0, 2) )  > color_threshold	?	0	:	1;
	}
	//EDGE SOBEL//////////////////////////////////////////////////////////////
	template<class T1, class T2>
	void EdgeSobel(T1& ColorMask, T2& EdgeMask, const double &edge_threshold)
	{
		for (int x = convolve_start; x < convolve_end_x; x++)
			for (int y = convolve_start; y < convolve_end_y; y++)
				EdgeMask[x][y] =
					sqrt(    pow(SubConvolve3_3(ColorMask, x, y, kernel_edge_h, kernel_edge_multiple), 2) + 
					pow(SubConvolve3_3(ColorMask, x, y, kernel_edge_v, kernel_edge_multiple), 2)    )	>	edge_threshold	?	1	:	0;
	}
	//HOUGH TRANSFORM////////////////////////////////////////////////////
	template<class T>
	HTParameter HoughTransform(T &EdgeMask, const int r_min, const int r_max, const int r_step)
	{
		HTParameter _HTP, _HTP_MAX;
		_HTP_MAX.max_accu_value = 1;
		for (int r = r_min; r < r_max; r += r_step)
			if ((_HTP = SubHoughTransform(EdgeMask, r)).max_accu_value > _HTP_MAX.max_accu_value)
				_HTP_MAX = _HTP;
		return _HTP_MAX;
	}
	template<class T>
	HTParameter SubHoughTransform(T &EdgeMask, const int radius)
	{
		memset(AccuGrid, NULL, image_height * image_width * sizeof(unsigned char));
		auto AccuGridAddPt = [](T& AccuGrid, const int a, const int b)
		{ if ((a > 0) && (b > 0) && (a < image_height) && (b < image_width))AccuGrid[a][b]++; };
		double d_theta = M_Theta / radius;
		for (int x = 0; x < image_height; x++)
			for (int y = 0; y < image_width; y++)
				if (EdgeMask[x][y])
					for (double theta = 0; theta < M_2PI; theta += d_theta)
						AccuGridAddPt(AccuGrid, x - radius * cos(theta) +0.5, y - radius * sin(theta) +0.5);
		HTParameter _HTP = { 0 };
		for (int x = 0; x < image_height; x++)
			for (int y = 0; y < image_width; y++)
				if (AccuGrid[x][y] > _HTP.max_accu_value)
					_HTP = { radius, x, y, AccuGrid[x][y] };
		return _HTP;
	}
	//FIND BALL/////////////////////////////////////////////////////////////
	template<class T>
	HTParameter FindBall(T &R, T &G, T &B, const unsigned char color_threshold = 80, 
		const double edge_threshold = 2, const int r_min = 2, const int r_max = 32, const int r_step = 2)
	{
		GaussFilter(R, G, B, R_filt, G_filt, B_filt);
		ColorFilter(R_filt, G_filt, B_filt, ColorMask, color_threshold);
		EdgeSobel(ColorMask, EdgeMask, edge_threshold);
		return Result = HoughTransform(EdgeMask, r_min, r_max, r_step);
	}
	~FastHT()
	{
		delete[]R_filt;
		delete[]G_filt;
		delete[]B_filt;
		delete[]ColorMask;
		delete[]EdgeMask;
		delete[]AccuGrid;
	}
};