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
float Vx, Vy, Vz; //单位 g(9.8m/s^2)
float Gx, Gy, Gz; //单位 °/s
float Gx1, Gy1, Gz1; //单位 °/s
float Gx0, Gy0, Gz0; //单位 °/s
float AG[6];
float Xconst, Yconst, Zconst, G0;

float Angel_accX, Angel_accY, Angel_accZ; //存储加速度计算出的角度

long LastTime, NowTime, TimeSpan; //用来对角速度积分的
float count;


void setup() 
{
  Wire.begin();
  Serial.begin(115200);
  Serial.println("Initializing...");
  accelgyro.initialize();

}

void loop() {
 // delay(50);


  accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);
    aax=ax/16.38400-Ax_offset;
    aay=ay/16.38400-Ay_offset;
    aaz=az/16.38400-Az_offset;
    ggx=gx/131.00-Gx_offset;
    ggy=gy/131.00-Gy_offset;
    ggz=gz/131.00-Gz_offset;

//下面三行就是通过对角速度积分实现各个轴的角度测量，当然假设各轴的起始角度都是0
  AG[0] = aax;
  AG[1] = aay;
  AG[2] = aaz;
  AG[3] = ggx;
  AG[4] = ggy;
  AG[5] = ggz;
  count=1.00;
  Ax0 = ((AG[0]) * 1.000 / count);
  Ay0 = ((AG[1]) * 1.000 / count);
  Az0 = ((AG[2]) * 1.000 / count);
  
  NowTime=millis();//获取当前程序运行的毫秒数
  TimeSpan=NowTime-LastTime;//积分时间这样算不是很严谨
  LastTime=NowTime;
  
  if ((fabs(Ax0 - Ax1) + fabs(Ay0 - Ay1) ) < 20)
  {
    Gx0 = (float)(atan2(sqrt(Ay0 * Ay0 + Az0 * Az0), Ax0)); //X轴角度值
    Gy0 = (float)(atan2(sqrt(Ax0 * Ax0 + Az0 * Az0), Ay0)); //Y轴角度值
    Gz0 = (float)(atan2(sqrt(Ax0 * Ax0 + Ay0 * Ay0), Az0)); //Z轴角度值
    Gx = (float)(atan(cos(Gy0) / cos(Gz0)) * 180 / 3.14159265);
    Gy = (float)(-atan(cos(Gx0) / cos(Gz0)) * 180 / 3.14159265);
    G0 = sqrt(Ax0 * Ax0 + Ay0 * Ay0 + Az0 * Az0);
    Vx = Vy = Vz = 0;
    Serial.println("!!!!!!!!!!!!!!!!!!!!!!!!");//* 180 / 3.14159265
  }
  else
  {
    Gx1 = (AG[3] / count) * TimeSpan / 1000.00;
    Gy1 = (AG[4] / count) * TimeSpan / 1000.00;
    //Gz1 = ((AG[5] / count)- Gz_offset) * TimeSpan / 1000.00;
    Gx = Gx + Gy1;
    Gy = Gy + Gx1;
  }
  Ax1 = Ax0;
  Ay1 = Ay0;
  Az1 = Az0;

  Gx1 = Gx * 3.14159265 / 180.00;
  Gy1 = Gy * 3.14159265 / 180.00;
  Az = Az0 * cos(Gx1) + Ay0 * sin(Gx1);
  Ay = -Az0 * sin(Gx1) + Ay0 * cos(Gx1);
  Ax = Ax0 * cos(Gy1) + Az0 * sin(Gy1);
  Az = -Ax0 * sin(Gy1) + Az0 * cos(Gy1) - G0;

  //  Gz=acos(cos(Gy)*cos(Gy1));
  Vx = Vx + Ax * TimeSpan / 1000.00;
  Vy = Vy + Ay * TimeSpan / 1000.00;
  Vz = Vz + Az * TimeSpan / 1000.00;

  for (int i = 0; i < 6; i++)AG[i] = 0;

  Serial.print("a/g:\t");
  Serial.print(Ax); Serial.print("\t");
  Serial.print(Ay); Serial.print("\t");
  Serial.print(Az); Serial.print("\t");Serial.print("\t");
  Serial.print(Vx); Serial.print("\t");
  Serial.print(Vy); Serial.print("\t");
  Serial.print(Vz); Serial.print("\t"); Serial.print("\t");


  Serial.print(Gx); Serial.print("\t");
  Serial.print(Gy); Serial.print("\t");
  Serial.println(TimeSpan); Serial.print("\t");
  //Serial.println(Gz); Serial.print("\t");

  //    LastTime=NowTime;
  
  
  
  

}


