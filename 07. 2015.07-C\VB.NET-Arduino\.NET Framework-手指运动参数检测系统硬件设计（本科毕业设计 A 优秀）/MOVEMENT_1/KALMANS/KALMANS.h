#ifndef _KALMANS_H_
#define _KALMANS_H_

class KALMANS
{
public:
	float integral, basic;//角度和角速度
	float P[2][2] = {{ 1, 0 },
              { 0, 1 }};
	float Pdot[4] ={ 0,0,0,0};
	float Q_angle=0.001, Q_gyro=0.005; //角度数据置信度,角速度数据置信度
	float R_angle=0.5 ,C_0 = 1;
	float q_bias, angle_err, PCt_0, PCt_1, E, K_0, K_1, t_0, t_1;
	void cal(double angle_m,double gyro_m,float dt);
};


#endif