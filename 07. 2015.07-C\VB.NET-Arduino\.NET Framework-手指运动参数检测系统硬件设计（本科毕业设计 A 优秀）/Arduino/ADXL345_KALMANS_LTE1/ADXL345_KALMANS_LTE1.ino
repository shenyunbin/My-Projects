#include "Wire.h"
#include "I2Cdev.h"
#include "KALMANS.h"
#include "ADXL345.h"

ADXL345 adxl; //variable adxl is an instance of the ADXL345 library

KALMANS kal1;
KALMANS kal2;
KALMANS kal3;

#define isize 100
int16_t ax, ay, az, gx, gy, gz;//陀螺仪原始数据 3个加速度+3个角速度
float Ax, Ay, Az;
int  xx, xy, xz;
void INT16_SEND(int int16)
{
  unsigned int uint16;
  uint16 = int16 + 32767;
  Serial.write(uint16 >> 8);
  Serial.write(uint16);
}

void DATA_SEND()
{
  Serial.write(255);
  INT16_SEND(ax);
  INT16_SEND(ay);
  INT16_SEND(az);
  INT16_SEND(xx);
  INT16_SEND(xy);
  INT16_SEND(xz);
  Serial.print(254);
}

void TEST()
{
  Serial.print(xx);
  Serial.print("\t");
  Serial.print(xy);
  Serial.print("\t");
  Serial.println(xz);
}

void setup() {
  Serial.begin(115200);//初始化
  adxl.powerOn();
}
void loop() {
  delay(18);
  adxl.readAccel(&ax, &ay, &az);//读取原始6个数据
  Ax = ax;
  Ay = ay;
  Az = az;
  kal1.cal(0, Ax, 0.020);
  kal2.cal(0, Ay, 0.020);
  kal3.cal(0, Az, 0.020);
  xx = kal1.integral * 10;
  xy = kal2.integral * 10;
  xz = kal3.integral * 10;
  DATA_SEND();
  //.TEST();
}


