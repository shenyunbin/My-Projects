#include "Wire.h"
#include "I2Cdev.h"
#include "MPU6050.h"
#include "KALMANS.h"

KALMANS kal1;
KALMANS kal2;
KALMANS kal3;

#define isize 100
MPU6050 accelgyro;//陀螺仪类
int16_t ax, ay, az, gx, gy, gz;//陀螺仪原始数据 3个加速度+3个角速度
float Ax, Ay, Az;

void INT16_SEND(int int16)
{
  unsigned int uint16;
  uint16=int16+32767;
  Serial.write(uint16 >> 8);
  Serial.write(uint16);

}


void setup() {
  Wire.begin();//初始化
  Serial.begin(115200);//初始化
  accelgyro.initialize();//初始化

}
void loop() {
  delay(18);
  accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);//读取原始6个数据

  Ax = ax / 163.8400;
  Ay = ay / 163.8400;
  Az = az / 163.8400;

  kal1.cal(0, Ax, 0.020);
  kal2.cal(0, Ay, 0.020);
  kal3.cal(0, Az, 0.020);

  int ax, ay, az, xx, xy, xz;
  ax = Ax;
  ay = Ay;
  az = Az;
  xx = kal1.integral * 100;
  xy = kal2.integral * 100;
  xz = kal3.integral * 100;

  Serial.write(255);
  INT16_SEND(ax);
  INT16_SEND(ay);
  INT16_SEND(az);
  INT16_SEND(xx);
  INT16_SEND(xy);
  INT16_SEND(xz);
  Serial.print(254);
}


/*
    Serial.print(ax);
    Serial.print("\t");
    Serial.print(ay);
    Serial.print("\t");
    Serial.print(az);
    Serial.print("\t");


    Serial.print(xx);
    Serial.print("\t");
    Serial.print(xy);
    Serial.print("\t");
    Serial.println(xz);
    
    
    
    
    
        
  Serial.write(255);
  INT16_SEND(ax);
  INT16_SEND(ay);
  INT16_SEND(az);
  INT16_SEND(xx);
  INT16_SEND(xy);
  INT16_SEND(xz);
  Serial.println();
    
      Serial.write(255);
  Serial.write(255);
  INT16_SEND(ax);
  INT16_SEND(ay);
  INT16_SEND(az);
  INT16_SEND(xx);
  INT16_SEND(xy);
  INT16_SEND(xz);
  Serial.print(254);
    
    
*/

