#include "Wire.h"
#include "I2Cdev.h"
#include "ADXL345.h"
ADXL345 adxl; //variable adxl is an instance of the ADXL345 library
float Ax, Ay, Az;
int  ax, ay, az, xx, xy, xz, Xt[25], Yt[25], Zt[25];
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
  INT16_SEND(ax);
  INT16_SEND(ay);
  INT16_SEND(az);
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
  Serial.println(az);
}
//5点3次曲线平滑算法
int Fivethree(int in[]) { 
  return (-3 * (in[0] + in[4]) + 12 * (in[1] + in[3]) + 17 * in[2]) / 35;
}
//7点2次曲线平滑算法
int SivenTwo(int in[])
{
return ( -2.0 * (in[0] + in[6]) +
                       3.0 * (in[1] + in[5]) +
                      6.0 * (in[2] + in[4]) + 7.0 * in[3] ) / 21.0;
}
//10点1次曲线平滑算法
int Tenone(int in[])  {
  int averange;
  for(int i=0; i<10;i++){
    averange+=in[i];
  }
  averange/=10;
return averange;
}
//数组数据更新
void cir(int a[],int num, int temp) {  
  int i;
  for(i=0;i<num-1;i++){
    a[i]=a[i+1];
  }
  a[i] = temp;
}
//获取经过5点3次平滑算法后的数据
void GetData() {  
  adxl.readAccel(&ax, &ay, &az);//读取原始3个数据
  cir(Xt,10,ax);
  cir(Yt,10,ay);
  cir(Zt,10,az);
  ax = Fivethree(Xt);
  ay = Fivethree(Yt);
  az = Fivethree(Zt);
}




void setup() {
  Serial.begin(115200);//初始化
  adxl.powerOn();
}

void loop() {
  delay(18);
  GetData();
  //DATA_SEND();
  TEST();
}


