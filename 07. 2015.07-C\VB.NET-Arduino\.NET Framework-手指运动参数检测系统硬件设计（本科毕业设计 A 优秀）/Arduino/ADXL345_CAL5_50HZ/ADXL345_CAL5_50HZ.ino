#include "Wire.h"
#include "I2Cdev.h"
#include "ADXL345.h"
#define TimeInterval 0.020
ADXL345 adxl; //variable adxl is an instance of the ADXL345 library
const double xratio=0.99;
float Ax, Ay, Az;
int ax, ay, az, vx,vy,vz,xx, xy, xz;
int Xt[101], Yt[101], Zt[101], AXt[15], AYt[15], AZt[15];
int Tx,Ty,Tz;
int ax0,ay0,az0,v_flag;
int boundary=5;
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
void TEST1() {
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
//Test Function
void TEST2() {
  Serial.print(ay);
  Serial.print("\t");
  Serial.print(vy);
  Serial.print("\t");
  Serial.println(xy);
}
//15点取平均值
int  F15(int in[])  {
  int averange=0;
  for(int i=43; i<58;i++){
    averange+=in[i];
  }
  averange/=15.0;
return averange;
}
//101点取平均值
int F101(int in[],int *Tin)  {
  int averange=0;
  for(int i=0; i<101;i++){
    averange+=in[i];
  }
  averange/=101.0;
  if(*Tin==0)return averange;
  else *Tin=0.05*averange+0.95*(*Tin);
  return *Tin;
}
//加速度滤波+平滑处理
int AProcess(int in[],int *Tin){
  int Apro;
  Apro=F15(in)-F101(in,Tin);
  if(abs(Apro)>5)return Apro;
  else return 0;
}
//
int VProcess1(int vi,int ai,int in[]) {
  if(abs(ai)>boundary)return vi+ai*fabs(ai/18);
  else return vi;
}
//速度置0处理
int VProcess2(int vi,int *xi,int in[]){
  if((in[14]==0)&&(in[12]==0)&&(in[11]==0)){
    *xi-=(vi*0.4);
    return 0;
  }
  else return vi;
}
int XProcess(int xi){
  int bond=12000;
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
  ax = AProcess(Xt,&Tx);
  ay = AProcess(Yt,&Ty);
  az = AProcess(Zt,&Tz);
  cir(AXt,15,ax);
  cir(AYt,15,ay);
  cir(AZt,15,az);
}
//
void GetV() {
  vx=VProcess1(vx,ax,AXt);
  vy=VProcess1(vy,ay,AYt);
  vz=VProcess1(vz,az,AZt); 
  vx = VProcess2(vx,&xx,AXt);
  vy = VProcess2(vy,&xy,AYt);
  vz = VProcess2(vz,&xz,AZt);   
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
  delay(20);
}

void loop() {
  for(int i=0;i<100;i++)
  {
  adxl.readAccel(&ax0, &ay0, &az0);//读取原始3个数据
  cir(Xt,15,ax0);
  cir(Yt,15,ay0);
  cir(Zt,15,az0);
  delay(18);
  }
  while(1)
  {
  delay(18);
  GetA();
  GetV();
  GetX();
  DATA_SEND();
  //TEST2();
  }
}


