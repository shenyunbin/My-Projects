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
float angleAx,angleAy,Ax,Ay,Az,Vx,Vy,Vz,Xx,Xy,Xz,axn,ayn,azn,vxn,vyn,vzn,vxx,vyy,vzz;
//int Vxbasic[isize],Vybasic[isize],Vzbasic[isize];
//int averx,avery,averz;
//int count;
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

    Ax=ax/163.8400;
    Ay=ay/163.8400;
    Az=az/163.8400;
    
    kal1.cal(0,Ax,0.020);
    kal2.cal(0,Ay,0.020);
    kal3.cal(0,Az,0.020);
    
    vxx+=(kal1.integral+axn)/100.00;
    vyy+=(kal2.integral+ayn)/100.00;
    vzz+=(kal3.integral+azn)/100.00;
    axn=kal1.integral;
    ayn=kal2.integral;
    azn=kal3.integral;   
    /*
    Vxbasic[(count)%isize]=vxx;
    Vybasic[(count)%isize]=vyy;
    Vzbasic[(count)%isize]=vzz;              

    Vxbasic[(count+1)%isize]=Vxbasic[count%isize]+(kal1.integral+axn)/100.00;
    Vybasic[(count+1)%isize]=Vybasic[count%isize]+(kal2.integral+ayn)/100.00;
    Vzbasic[(count+1)%isize]=Vzbasic[count%isize]+(kal3.integral+azn)/100.00;           
    */
    

    /*
    for(int i=0;i<isize;i++)
    {
      averx=avery=averz=0;
      averx+=Vxbasic[i]/isize;
      avery+=Vybasic[i]/isize;
      averz+=Vzbasic[i]/isize;    
      Vxbasic[i]-=averx;
      Vxbasic[i]-=avery;
      Vxbasic[i]-=averz;  
    }
    if(count>isize)
    {
      Vx=Vxbasic[(count-isize/2)%isize];
      Vy=Vybasic[(count-isize/2)%isize];
      Vz=Vzbasic[(count-isize/2)%isize];    
      Xx+=(Vx+vxn)/100.00;
      Xy+=(Vy+vyn)/100.00;
      Xz+=(Vz+vzn)/100.00;
      vxn=Vx;
      vyn=Vy;
      vzn=Vz;            
    }
    */
    
    Serial.print(kal1.basic);
    Serial.print("\t");
    Serial.print(kal2.basic);
    Serial.print("\t");
    Serial.print(kal3.basic);
    Serial.print("\t");

    
    Serial.print(kal1.integral);
    Serial.print("\t");
    Serial.print(kal2.integral);
    Serial.print("\t");
    Serial.print(kal3.integral);
    Serial.print("\t");
    
   Serial.print(kal4.integral);
    Serial.print("\t");
    Serial.print(kal5.integral);
    Serial.print("\t");
    Serial.print(kal6.integral);
    Serial.println("\t");
    
/*    
     Serial.print(Xx);
    Serial.print("\t");
    Serial.print(Xy);
    Serial.print("\t");
    Serial.println(Xz);

     count++;
     if(count>20000)
     {count=200;
     //Vx=Vy=Vz=0;
     }
     */
}




