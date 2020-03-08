#include "Wire.h"
#include "I2Cdev.h"
#include "ADXL345.h"
ADXL345 adxl; 
int ax, ay, az, vx, vy, vz, xx, xy, xz;
int ax0, ay0, az0;
int Tx, Ty, Tz;
int Xt[101], Yt[101], Zt[101], AXt[15], AYt[15], AZt[15];
int boundary = 0;
int FS;
//整型数据发送
void INT16_SEND(int int16)  {
  unsigned int uint16;
  uint16 = int16 + 32767;
  Serial.write(uint16 >> 8);
  Serial.write(uint16);
}
//数据发送
void DATA_SEND()  {
  Serial.write(253);
  INT16_SEND(ax0);
  INT16_SEND(ay0);
  INT16_SEND(az0);
  INT16_SEND(ax);
  INT16_SEND(ay);
  INT16_SEND(az);
  INT16_SEND(vx);
  INT16_SEND(vy);
  INT16_SEND(vz);
  INT16_SEND(xx);
  INT16_SEND(xy);
  INT16_SEND(xz);
  Serial.write(254);
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
//15点取平均值
int  F15(int in[])  {
  int averange = 0;
  for (int i = 85; i < 100; i++) {
    averange += in[i];
  }
  averange /= 15.0;
  return averange;
}
//101点取平均值
int F101B(int in[])  {
  int averange;
  for(int i=0; i<101;i++){
    averange+=in[i];
  }
  averange/=101;
return averange;
}
//101点取平均值（带一阶滤波）
int F101A(int in[], int *Tin)  {
  int averange = 0;
  for (int i = 0; i < 101; i++) {
    averange += in[i];
  }
  averange /= 101.0;
  if (*Tin == 0) *Tin=averange;
  else *Tin = 0.5 * averange + 0.5 * (*Tin);  //加速度一阶滤波
  return *Tin;
}
//加速度滤波+平滑处理（超过临界值才输出加速度）
void AProcess1( int *ai1, int *ai2, int *ai3,
               int in1[], int in2[], int in3[],
               int *Tin1, int *Tin2, int *Tin3 ) {
  int Apro1, Apro2, Apro3;
  Apro1 = F15(in1) - F101A(in1, Tin1);
  Apro2 = F15(in2) - F101A(in2, Tin2);
  Apro3 = F15(in3) - F101A(in3, Tin3);
  if ((abs(Apro1) + abs(Apro2) + abs(Apro3)) > 10) {  //加速度超过阀值是输出
    *ai1 = Apro1;
    *ai2 = Apro2;
    *ai3 = Apro3;
  }
  else {
    *ai1 = 0;  //加速度置0
    *ai2 = 0;
    *ai3 = 0;
  }   
}
//简单的加速度滤波+平滑处理
int AProcess2(int in[]){
  return F15(in)-F101B(in);
}
//大致计算手指活动时间占总的时间的程度
void iABS(int *Trend,int in1,int in2,int in3){
  if((abs(*Trend)>362)&&(abs(in1)>2||abs(in2)>2||abs(in3)>2)) *Trend=360;//in1!=0||in2!=0||in3!=0
  else if(abs(in1)>2||abs(in2)>2||abs(in3)>2) *Trend+=2; 
  else if(abs(*Trend)>20) *Trend-=20;
}
//加速度大于一定值时才计入速度变化
int VProcess1( int vi, int ai ) {
  if (abs(ai) > boundary)return vi + ai*5;
  else return vi;
}
//当加速度均连续为0时的速度置0处理
void VProcess2( int *vi1, int *vi2, int *vi3,
                int *xi1, int *xi2, int *xi3,
                int in1[], int in2[], int in3[] ) {
  if ( (in1[14] == 0) && (in1[12] == 0) && (in1[11] == 0) &&
       (in2[14] == 0) && (in2[12] == 0) && (in2[11] == 0) &&
       (in3[14] == 0) && (in3[12] == 0) && (in3[11] == 0) ) {
    *xi1 -= 0.4 * (*vi1);//消除速度置0前所多算的位移长度
    *xi2 -= 0.4 * (*vi2);
    *xi3 -= 0.4 * (*vi3);
    *vi1 = 0;  //速度置0
    *vi2 = 0;
    *vi3 = 0;
  }
}
//位移边界设定
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
//获取加速度数据
int GetA1() {
  adxl.readAccel(&ax0, &ay0, &az0);//读取原始3个数据
  cir(Xt, 101, ax0);  //将原始加速度值存入数组，用于滤波
  cir(Yt, 101, ay0);
  cir(Zt, 101, az0);
  AProcess1( &ax, &ay, &az,
            Xt,  Yt, Zt,
            &Tx, &Ty, &Tz );
  cir(AXt, 15, ax);  //将处理过的加速度值存入数组
  cir(AYt, 15, ay);
  cir(AZt, 15, az);
  iABS(&FS,ax,ay,az);
}
//获取加速度数据
int GetA2() {
  adxl.readAccel(&ax0, &ay0, &az0);//读取原始3个数据
  cir(Xt, 101, ax0);  //将原始加速度值存入数组，用于滤波
  cir(Yt, 101, ay0);
  cir(Zt, 101, az0);
  ax = AProcess2(Xt);
  ay = AProcess2(Yt);
  az = AProcess2(Zt);
  cir(AXt, 15, ax);  //将处理过的加速度值存入数组
  cir(AYt, 15, ay);
  cir(AZt, 15, az);
  iABS(&FS,ax,ay,az);
}
//获取速度数据
void GetV1() {
  vx = VProcess1(vx, ax);
  vy = VProcess1(vy, ay);
  vz = VProcess1(vz, az);
  VProcess2( &vx, &vy, &vz,
             &xx, &xy, &xz,
             AXt, AYt, AZt );
}
//获取速度数据
void GetV2() {
  vx=vx*0.90+ax*5; //去除速度的偏移
  vy=vy*0.90+ay*5;
  vz=vz*0.90+az*5;
}
//获取位移数据
void GetX1() {
  xx = xx + vx * 0.1; //位移为速度的简单积分运算
  xy = xy + vy * 0.1;
  xz = xz + vz * 0.1;
  xx = XProcess2(xx); //设定位移的边界
  xy = XProcess2(xy);
  xz = XProcess2(xz);
}
//获取位移数据
void GetX2() {
  xx=xx*0.80+vx; //去除位移的偏移
  xy=xy*0.80+vy;
  xz=xz*0.80+vz;
}

void setup() {
  Serial.begin(115200,SERIAL_8E2);//初始化
  adxl.powerOn();
  delay(20);
}

void loop() {
  for (int i = 0; i < 100; i++)
  {
    adxl.readAccel(&ax0, &ay0, &az0);//读取原始3个数据
    cir(Xt, 15, ax0);
    cir(Yt, 15, ay0);
    cir(Zt, 15, az0);
    delay(18);
  }
  while (1)
  {
    delay(18);    
    if(FS<300){ //检测到不是连续运动时，运行第1种算法
      GetA1();
      GetV1();
      GetX1();      
    }
    else{  //检测到是连续运动时，运行第2种算法
      GetA2();
      GetV2();
      GetX2();
    }
    DATA_SEND();   
    //TEST1();
  }
}


