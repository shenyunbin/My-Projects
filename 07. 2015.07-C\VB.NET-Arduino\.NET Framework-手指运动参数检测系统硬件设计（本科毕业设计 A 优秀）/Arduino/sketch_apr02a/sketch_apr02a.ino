#include "Wire.h"
#include "I2Cdev.h"
#include "MPU6050.h"
#include "KALMANS.h"

KALMANS kal;

MPU6050 accelgyro;//陀螺仪类
int16_t ax, ay, az, gx, gy, gz;//陀螺仪原始数据 3个加速度+3个角速度
float angleAx,gyroGy;
void setup() {
    Wire.begin();//初始化
    Serial.begin(9600);//初始化
    accelgyro.initialize();//初始化

}
void loop() {
  
    accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);//读取原始6个数据
    
    angleAx=atan2(ax,az)*180/PI;//计算与x轴夹角
    gyroGy=-gy/131.00;//计算角速度
    
    kal.cal(angleAx,gyroGy,0.020);
    Serial.print(kal.integral);
    Serial.print(',');
    Serial.println(kal.basic);//Serial.print(',');
}




