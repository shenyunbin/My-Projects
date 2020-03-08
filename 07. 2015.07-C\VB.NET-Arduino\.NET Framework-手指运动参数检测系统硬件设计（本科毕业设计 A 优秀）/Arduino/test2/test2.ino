#include "Wire.h"
#include "I2Cdev.h"
#include "MPU6050.h"
#include "MsTimer2.h"//时间操作系统头文件  本程序用作timeChange时间采集并处理一次数据



MPU6050 accelgyro;//陀螺仪类
int16_t ax, ay, az, gx, gy, gz;//陀螺仪原始数据 3个加速度+3个角速度
float angleAx,gyroGy;
	float integral, basic;//角度和角速度
	float P[2][2] = {{ 1, 0 },
              { 0, 1 }};
	float Pdot[4] ={ 0,0,0,0};
	float Q_angle=0.001, Q_gyro=0.005; //角度数据置信度,角速度数据置信度
	float R_angle=0.5 ,C_0 = 1;
	float q_bias, angle_err, PCt_0, PCt_1, E, K_0, K_1, t_0, t_1;
	void cal(double angle_m,double gyro_m,float dt);



void setup() {
    Wire.begin();//初始化
    Serial.begin(9600);//初始化
    accelgyro.initialize();//初始化

}
void loop() {
  
    accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);//读取原始6个数据
    
    angleAx=atan2(ax,az)*180/PI;//计算与x轴夹角
    gyroGy=-gy/131.00;//计算角速度
    
   cal(angleAx,gyroGy,20);
    Serial.print(integral);
    Serial.print(',');
    Serial.println(basic);//Serial.print(',');
}


void cal(double angle_m,double gyro_m,float dt)
{
integral+=(gyro_m-q_bias) * dt;
angle_err = angle_m - integral;
Pdot[0]=Q_angle - P[0][1] - P[1][0];
Pdot[1]=- P[1][1];
Pdot[2]=- P[1][1];
Pdot[3]=Q_gyro;
P[0][0] += Pdot[0] * dt;
P[0][1] += Pdot[1] * dt;
P[1][0] += Pdot[2] * dt;
P[1][1] += Pdot[3] * dt;
PCt_0 = C_0 * P[0][0];
PCt_1 = C_0 * P[1][0];
E = R_angle + C_0 * PCt_0;
K_0 = PCt_0 / E;
K_1 = PCt_1 / E;
t_0 = PCt_0;
t_1 = C_0 * P[0][1];
P[0][0] -= K_0 * t_0;
P[0][1] -= K_0 * t_1;
P[1][0] -= K_1 * t_0;
P[1][1] -= K_1 * t_1;
integral += K_0 * angle_err; //最优角度
q_bias += K_1 * angle_err;
basic = gyro_m-q_bias;//最优角速度
}

