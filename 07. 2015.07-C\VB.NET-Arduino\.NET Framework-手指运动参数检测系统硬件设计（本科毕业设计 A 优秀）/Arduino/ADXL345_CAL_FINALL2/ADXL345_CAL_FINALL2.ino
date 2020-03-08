#include "Wire.h"
#include "I2Cdev.h"
#include "ADXL345.h"
#define TimeInterval 0.020
ADXL345 adxl; //variable adxl is an instance of the ADXL345 library
const double xratio = 0.99;
float Ax, Ay, Az;
int ax, ay, az, vx, vy, vz, xx, xy, xz;
int Xt[101], Yt[101], Zt[101], AXt[15], AYt[15], AZt[15];
int XtS,YtS,ZtS;
int Tx, Ty, Tz;
int ax0, ay0, az0, v_flag, Xchange;
int boundary = 5;
long int NowTime,LastTime,TimeSpan;
int Smax;
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
   NowTime=millis();//获取当前程序运行的毫秒数
  TimeSpan=min(NowTime-LastTime,255);//积分时间这样算不是很严谨
  LastTime=NowTime;
  Serial.print(XtS);
  Serial.print("\t");
  Serial.print(ay);
  Serial.print("\t");
  Serial.print(vy);
  Serial.print("\t");
  Serial.println(xy);
}
//15点取平均值
int  F15(int in[])  {
  int averange = 0;
  for (int i = 43; i < 58; i++) {
    averange += in[i];
  }
  averange /= 15.0;
  return averange;
}
//101点取平均值
int F101(int in[], int *Tin )  {
  long int averange = 0;
  for (int i = 0; i < 101; i++) {
    averange += in[i];
  }
  averange /= 101.0;
  if (*Tin == 0)return averange;
  else *Tin = 0.05 * averange + 0.95 * (*Tin);
  return *Tin;
}
//
void iABS(int *Trend,int in){
  if((abs(*Trend)>320)&&(in!=0)) *Trend=320;
  else if(in!=0)*Trend+=2; 
  else if(abs(*Trend)>10) *Trend-=10;
}
//加速度滤波+平滑处理
void AProcess( int *ai1, int *ai2, int *ai3,
               int in1[], int in2[], int in3[],
               int *Tin1, int *Tin2, int *Tin3 ) {
  int Apro1, Apro2, Apro3;
  Apro1 = F15(in1) - F101(in1, Tin1);
  Apro2 = F15(in2) - F101(in2, Tin2);
  Apro3 = F15(in3) - F101(in3, Tin3);
  if ((abs(Apro1) + abs(Apro2) + abs(Apro3)) > 10) {
    *ai1 = Apro1;
    *ai2 = Apro2;
    *ai3 = Apro3;
  }
  else {
    *ai1 = 0;
    *ai2 = 0;
    *ai3 = 0;
  }   
}
//
int VProcess1( int vi, int ai ,int ts) {
  if(ts>300){return vi*0.9 + ai*5;}
  else if (abs(ai) > boundary)return vi + ai*fabs(ai/18);// * fabs(ai / 18);
  else return vi;
}
/*
int VProcess1( int vi,int ai,int in[],
               int *xi,int *temp ) {
  if(fabs(vi)<10&&fabs(ai)>5){
    Xchange=1;
    *temp=*xi;
  }
  else if(Xchange>0)Xchange++;
  if(abs(ai)>boundary)return vi+ai*fabs(ai/18);
  else return vi;
}
*/
//速度置0处理
void VProcess2( int *vi1, int *vi2, int *vi3,
                int *xi1, int *xi2, int *xi3,
                int in1[], int in2[], int in3[] ) {
  if ( (in1[14] == 0) && (in1[12] == 0) && (in1[11] == 0) &&
       (in2[14] == 0) && (in2[12] == 0) && (in2[11] == 0) &&
       (in3[14] == 0) && (in3[12] == 0) && (in3[11] == 0) ) {
    Xchange = 0;
    *xi1 -= 0.4 * (*vi1);
    *xi2 -= 0.4 * (*vi2);
    *xi3 -= 0.4 * (*vi3);
    *vi1 = 0;
    *vi2 = 0;
    *vi3 = 0;
  }
}
int XProcess1(int *xi,int vi,int ts) {
  if(ts>300){*xi = (*xi)*0.9 + vx * 0.1;}
  else *xi = *xi + vx * 0.1;  
}
int XProcess2(int xi) {
  int bond = 12000;
  if (xi > bond)return bond;
  else if (xi < -bond)return -bond;
  else return xi;
}
//数组数据更新
void cir(int a[], int num, int temp) {
  int i;
  for (i = 0; i < num - 1; i++) {
    a[i] = a[i + 1];
  }
  a[i] = temp;
}
//获取经过加速度滤波+平滑处理算法后的数据
void GetA() {
  adxl.readAccel(&ax0, &ay0, &az0);//读取原始3个数据
  cir(Xt, 101, ax0);
  cir(Yt, 101, ay0);
  cir(Zt, 101, az0);
  AProcess( &ax, &ay, &az,
            Xt,  Yt, Zt,
            &Tx, &Ty, &Tz );
  cir(AXt, 15, ax);
  cir(AYt, 15, ay);
  cir(AZt, 15, az);
  iABS(&XtS,ax);
  iABS(&YtS,ay);
  iABS(&ZtS,az);
}
//
void GetV() {
  //Smax=max(XtS, YtS);
  //Smax=max(Smax,ZtS);
  vx = VProcess1(vx, ax, Smax);
  vy = VProcess1(vy, ay, Smax);
  vz = VProcess1(vz, az, Smax);
  VProcess2( &vx, &vy, &vz,
             &xx, &xy, &xz,
             AXt, AYt, AZt );
}
//
void GetX() {
  XProcess1(&xx,vx,Smax);
  XProcess1(&xy,vy,Smax);
  XProcess1(&xz,vz,Smax);
  xx = XProcess2(xx);
  xy = XProcess2(xy);
  xz = XProcess2(xz);
}

void setup() {
  Serial.begin(115200);//初始化
  adxl.powerOn();
  delay(20);
}

void loop() {
  for (int i = 0; i < 100; i++)
  {
    adxl.readAccel(&ax0, &ay0, &az0);//读取原始3个数据
    cir(Xt, 101, ax0);
    cir(Yt, 101, ay0);
    cir(Zt, 101, az0);
    delay(18);
  }
  while (1)
  {
    delay(17);
    GetA();
    GetV();
    GetX();
    DATA_SEND();
    //TEST2();
  }
}


