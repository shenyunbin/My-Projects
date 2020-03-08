#include "Wire.h"
#include "I2Cdev.h"
#include "MPU6050.h"
MPU6050 accelgyro;

int16_t ax, ay, az;
int16_t gx, gy, gz;
int AG[3],Ax0,Ax1,Ay0,Ay1,Az0,Az1;
int iAG=0,Offset_Ax,Offset_Ay,Offset_Az,Vx,Vy,Vz,Xx,Xy,Xz,Xx0,Xy0,Xz0;
long LastTime, NowTime,TimeSpan; //用来对角速度积分的
#define LED_PIN 13
bool blinkState = false;

void INT16_SEND(int int16)
{
   Serial.write(int16>>8);
   Serial.write(int16);  
}
 void DATA_SEND()
 {
 Serial.write(255);
 INT16_SEND(120);
 INT16_SEND(120);
 INT16_SEND(0);
 INT16_SEND(120);
 INT16_SEND(0);
 INT16_SEND(120);
  INT16_SEND(0);
 INT16_SEND(0);
 INT16_SEND(120);
 Serial.println();
 }




void setup() {
    Wire.begin();
    Serial.begin(115200);
    Serial.println("Initializing I2C devices...");
    accelgyro.initialize();

}

void loop() {
    accelgyro.getAcceleration(&ax, &ay, &az);    
    Ax1 += ax/1638;
    Ay1 += ay/1638;
    Az1 += az/1638;
    iAG++;
    if(iAG>99)
    {
    iAG=0;
  if ((fabs(Ax0 - Ax1) + fabs(Ay0 - Ay1) + fabs(Ay0 - Ay1)) < 50)//(fabs(Ax0 - Ax1) + fabs(Ay0 - Ay1) + fabs(Ay0 - Ay1)) < 500
  {
 //   Gx = 180-(float)(((atan2(sqrt(Ay1 * Ay1 + Az1 * Az1), Ax1) * 180) / 3.14159265)); //X轴角度值
 //   Gy = 180-(float)(((atan2(sqrt(Ax1 * Ax1 + Az1 * Az1), Ay1) * 180) / 3.14159265)); //Y轴角度值
 //   Gz = (float)(((atan2(sqrt(Ax1 * Ax1 + Ay1 * Ay1), Az1) * 180) / 3.14159265)); //Z轴角度值
 //   G0 = sqrt(Ax1 * Ax1 + Ay1 * Ay1 + Az1 * Az1);
   Ax0=Ax1;
   Ay0=Ay1;
   Az0=Az1;
    Offset_Ax = Ax1;
    Offset_Ay = Ay1;
    Offset_Az = Az1;   
    Vx=Vy=Vz=0;
  }
  else
  {
  Ax0=Ax1;
  Ay0=Ay1;
  Az0=Az1;
  
  NowTime=millis();//获取当前程序运行的毫秒数
  TimeSpan=min(NowTime-LastTime,255);//积分时间这样算不是很严谨
  LastTime=NowTime;
  
  Ax1=(Ax1-Offset_Ax);
  Ay1=(Ay1-Offset_Ay);
  Az1=(Az1-Offset_Az);
  
  Vx=Vx+Ax1*TimeSpan/1000;
  Vy=Vy+Ay1*TimeSpan/1000;
  Vz=Vz+Az1*TimeSpan/1000; 
  
  Xx0=Xx+Vx*TimeSpan/1000;
  Xy0=Xy+Vy*TimeSpan/1000;
  Xz0=Xz+Vz*TimeSpan/1000;
   
  if(Xx0<-300)Xx=-300;
  else if(Xx0>300)Xx=300;
  else Xx=Xx0;
  if(Xy0<-300)Xy=-300;
  else if(Xy0>300)Xy=300;
  else Xy=Xy0;
  if(Xz0<-300)Xz=-300;
  else if(Xz0>300)Xz=300;
  else Xz=Xz0;
 /*

  */
  }
  
  
  
  DATA_SEND();
  Ax1 = 0;
  Ay1 = 0;
  Az1 = 0;
    }
    
    
    


}
