#include "Wire.h"
#include "I2Cdev.h"
#include "MPU6050.h"
#include "KALMANS.h"

KALMANS kal1;
KALMANS kal2;
KALMANS kal3;

MPU6050 accelgyro;//陀螺仪类
int16_t ax, ay, az, gx, gy, gz;//陀螺仪原始数据 3个加速度+3个角速度
float angleAx,angleAy,Ax,Ay,Az,Vx,Vy,Vz,Xx,Xy,Xz,axn,ayn,azn,vxn,vyn,vzn;
float Vxbasic[50],Vybasic[50],Vzbasic[50],averx,avery,averz;
int count;
void setup() {
    Wire.begin();//初始化
    Serial.begin(115200);//初始化
    accelgyro.initialize();//初始化

}
void loop() {  
    delay(18);
    accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);//读取原始6个数据
    angleAy=atan2(ax,az)*180/PI;//计算与x轴夹角
    angleAx=atan2(ay,az)*180/PI;//计算与y轴夹角

    Ax=ax/1638.400;
    Ay=ay/1638.400;
    Az=az/1638.400;
    
    kal1.cal(0,Ax,0.020);
    kal2.cal(0,Ay,0.020);
    kal3.cal(0,Az,0.020);
    
    Vxbasic[(count+1)%50]=Vxbasic[count%50]+(kal1.integral+axn)/100.00;
    Vybasic[(count+1)%50]=Vybasic[count%50]+(kal2.integral+ayn)/100.00;
    Vzbasic[(count+1)%50]=Vzbasic[count%50]+(kal3.integral+azn)/100.00;           
    axn=kal1.integral;
    ayn=kal2.integral;
    azn=kal3.integral;   
    for(int i=0;i<50;i++)
    {
      averx=avery=averz=0;
      averx+=Vxbasic[i]/50.00;
      avery+=Vybasic[i]/50.00;
      averz+=Vzbasic[i]/50.00;    
    }
    if(count>50)
    {
      Vx=Vxbasic[25]-averx;
      Vy=Vybasic[25]-avery;
      Vz=Vzbasic[25]-averz;    
      Xx+=(Vx+vxn)/100.00;
      Xy+=(Vy+vyn)/100.00;
      Xz+=(Vz+vzn)/100.00;
      vxn=Vx;
      vyn=Vy;
      vzn=Vz;      
      
    }
    
    Serial.print(kal1.basic);
    Serial.print("\t");
    Serial.print(kal2.basic);
    Serial.print("\t");
    Serial.print(kal3.basic);
    Serial.print("\t");
    
   Serial.print(Vx);
    Serial.print("\t");
    Serial.print(Vy);
    Serial.print("\t");
    Serial.print(Vz);
    Serial.print("\t");
    
    
     Serial.print(Xx);
    Serial.print("\t");
    Serial.print(Xy);
    Serial.print("\t");
    Serial.println(Xz);

     count++;
     if(count>300)count-=100;
}




