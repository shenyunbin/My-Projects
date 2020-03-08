#include "Wire.h"
#include "I2Cdev.h"
#include "ADXL345.h"
#define TimeInterval 0.020
ADXL345 adxl; //variable adxl is an instance of the ADXL345 library
const double xratio=0.99;
float Ax, Ay, Az;
int ax, ay, az, vx,vy,vz,xx, xy, xz;
int Xt[101], Yt[101], Zt[101], AXt[4], AYt[4], AZt[4], VXt[20], VYt[20], VZt[20];
int ax0,ay0,az0;
//整型数据发送
void INT16_SEND(int int16)  {  
  unsigned int uint16;
  uint16 = int16 + 32767;
  Serial.write(uint16 >> 8);
  Serial.write(uint16);
}
//数据发送
void DATA_SEND()  {  
  Serial.write(255);
  INT16_SEND(ax0);
  INT16_SEND(ay0);
  INT16_SEND(az0);
  INT16_SEND(xx);
  INT16_SEND(xy);
  INT16_SEND(xz);
  Serial.print(254);
}
//Test Function
void TEST() {
  Serial.print(ax);
  Serial.print("\t");
  Serial.print(ay);
  Serial.print("\t");
  Serial.print(az);
  Serial.print("\t");
  Serial.print(vx);
  Serial.print("\t");
  Serial.print(vy);
  Serial.print("\t");
  Serial.print(vz);
  Serial.print("\t");
  Serial.print(xx);
  Serial.print("\t");
  Serial.print(xy);
  Serial.print("\t");
  Serial.println(xz);
}
//15点取平均值
int  F15(int in[])  {
  int averange;
  for(int i=43; i<58;i++){
    averange+=in[i];
  }
  averange/=15;
return averange;
}
//101点取平均值
int F101(int in[])  {
  int averange;
  for(int i=0; i<101;i++){
    averange+=in[i];
  }
  averange/=101;
return averange;
}
//加速度滤波+平滑处理
int AProcess(int in[]){
  int Apro;
  Apro=F15(in)-F101(in);
  if(abs(Apro)>20)return Apro;
  else return 0;
}
//速度置0处理
int VProcess(int vi,int in[],int vin[]){
  if(in[3]==in[2]&&in[2]==in[1]&&in[1]==in[0])return 0;
  else if(vin[0]>10&&vi<0)return 0;
  else if(vin[0]<-10&&vi>0)return 0;
  else return vi;//if(abs(vi)>10) 
}
int XProcess(int xi){
  int bond=1200;
  if(xi>bond)return bond;
  else if(xi<-bond)return -bond;
  else return xi;
}
//数组数据更新
void cir(int a[],int num, int temp) {  
  int i;
  for(i=0;i<num-1;i++){
    a[i]=a[i+1];
  }
  a[i] = temp;
}
//获取经过加速度滤波+平滑处理算法后的数据
void GetA() {  
  adxl.readAccel(&ax0, &ay0, &az0);//读取原始3个数据
  cir(Xt,101,ax0);
  cir(Yt,101,ay0);
  cir(Zt,101,az0);
  ax = AProcess(Xt);
  ay = AProcess(Yt);
  az = AProcess(Zt);
  cir(AXt,4,ax);
  cir(AYt,4,ay);
  cir(AZt,4,az);
}
//
void GetV() {
  vx=vx+ax;
  vy=vy+ay;
  vz=vz+az; 
  vx = VProcess(vx,AXt,VXt);
  vy = VProcess(vy,AYt,VXt);
  vz = VProcess(vz,AZt,VXt);   
  cir(VXt,20,vx);
  cir(VYt,20,vy);
  cir(VZt,20,vz);
}
//
void GetX() {
  xx=xx+vx*0.1;//xx*0.99+vx*0.1
  xy=xy+vy*0.1;
  xz=xz+vz*0.1;
  xx=XProcess(xx);
  xy=XProcess(xy);
  xz=XProcess(xz);
}

void setup() {
  Serial.begin(115200);//初始化
  adxl.powerOn();
}

void loop() {
  delay(18);
  GetA();
  GetV();
  GetX();
  //DATA_SEND();
  TEST();
}


