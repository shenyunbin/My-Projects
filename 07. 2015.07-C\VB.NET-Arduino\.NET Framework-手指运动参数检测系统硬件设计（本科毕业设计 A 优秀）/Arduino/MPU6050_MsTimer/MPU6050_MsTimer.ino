#include <MsTimer2.h>               //定时器库的 头文件

#include "Wire.h"

#include "I2Cdev.h"
#include "MPU6050.h"


MPU6050 accelgyro;
#define Ax_offset 0
#define Ay_offset 0
#define Az_offset 0
#define Gx_offset -2.73
#define Gy_offset -0.21
#define Gz_offset 0.63

int16_t ax, ay, az;
int16_t gx, gy, gz; //存储原始数据
float aax, aay, aaz, ggx, ggy, ggz; //存储量化后的数据
float Ax, Ay, Az; //单位 g(9.8m/s^2)
float Ax0, Ay0, Az0; //单位 g(9.8m/s^2)
float Ax1, Ay1, Az1; //单位 g(9.8m/s^2)
float Gx, Gy, Gz; //单位 °/s
float Gx1, Gy1, Gz1; //单位 °/s
float AG[6];
float Xconst, Yconst, Zconst, G0;

float Angel_accX, Angel_accY, Angel_accZ; //存储加速度计算出的角度

long LastTime, NowTime, TimeSpan = 100; //用来对角速度积分的
int count;

void flash()                        //中断处理函数，改变灯的状态
{

  //  NowTime=millis();//获取当前程序运行的毫秒数
  //  TimeSpan=NowTime-LastTime;//积分时间这样算不是很严谨
  //下面三行就是通过对角速度积分实现各个轴的角度测量，当然假设各轴的起始角度都是0
  Ax0 = (AG[0] / count);
  Ay0 = (AG[1] / count);
  Az0 = (AG[2] / count);
  if ((fabs(Ax0 - Ax1) + fabs(Ay0 - Ay1) + fabs(Ay0 - Ay1)) < 50)
  {
    Gx = 180-(float)(((atan2(sqrt(Ay * Ay + Az * Az), Ax) * 180) / 3.14159265)); //X轴角度值
    Gy = 180-(float)(((atan2(sqrt(Ax * Ax + Az * Az), Ay) * 180) / 3.14159265)); //Y轴角度值
    Gz = (float)(((atan2(sqrt(Ax * Ax + Ay * Ay), Az) * 180) / 3.14159265)); //Z轴角度值
    G0 = sqrt(Ax * Ax + Ay * Ay + Az * Az);
    Ax=Ay=Az=0;
  }
  Ax1 = Ax0;
  Ay1 = Ay0;
  Az1 = Az0;
  Gx1 = ((AG[3] / count) - Gx_offset) * TimeSpan / 1000.00;
  Gy1 = ((AG[4] / count)- Gy_offset) * TimeSpan / 1000.00;
  Gz1 = ((AG[5] / count)- Gz_offset) * TimeSpan / 1000.00;
  
  Gx=acos(cos(Gy1+)*cos(Gz1));
  Gy=acos(cos(Gy)*cos(Gy1));
  Gz=;


  Ax = Ax + Ax1* TimeSpan / 1000.00;
  Ay = Ay + Ay1* TimeSpan / 1000.00;
  Az = Az + Az1* TimeSpan / 1000.00;

  //Gz = Gz + (Gz1- Gz_offset) * TimeSpan / 1000.00;

  count = 0;
  for (int i = 0; i < 6; i++)AG[i] = 0;






  Serial.print("a/g:\t");
  Serial.print(Ax); Serial.print("\t");
  Serial.print(Ay); Serial.print("\t");
  Serial.print(Az); Serial.print("\t");
  Serial.print(Xconst); Serial.print("\t");
  Serial.print(Yconst); Serial.print("\t");
  Serial.print(Zconst); Serial.print("\t");


  Serial.print(Gx); Serial.print("\t");
  Serial.print(Gy); Serial.print("\t");
  Serial.println(Gz); Serial.print("\t");

  //    LastTime=NowTime;
}

void setup() {
  Wire.begin();
  Serial.begin(38400);
  Serial.println("Initializing...");
  accelgyro.initialize();
  MsTimer2::set(TimeSpan, flash);        // 中断设置函数，每 500ms 进入一次中断
  MsTimer2::start();                //开始计时

}

void loop() {
  accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);
  aax = ax / 16384.00;
  aay = ay / 16384.00;
  aaz = az / 16384.00;
  ggx = gx / 131.00;
  ggy = gy / 131.00;
  ggz = gz / 131.00;
  AG[0] += aax;
  AG[1] += aay;
  AG[2] += aaz;
  AG[3] += ggx;
  AG[4] += ggy;
  AG[5] += ggz;
  // delay(10);

  //Serial.print(AG[0]);
  count++;
}


