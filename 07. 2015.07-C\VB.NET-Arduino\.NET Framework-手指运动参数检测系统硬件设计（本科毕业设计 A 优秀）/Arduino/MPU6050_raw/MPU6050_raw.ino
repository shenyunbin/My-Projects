
#include "Wire.h"

#include "I2Cdev.h"
#include "MPU6050.h"


MPU6050 accelgyro;

#define Gx_offset -2.73
#define Gy_offset -0.21
#define Gz_offset 0.63
#define Ax_offset 0.06
#define Ay_offset -0.01
#define Az_offset -0.07

int16_t ax,ay,az;
int16_t gx,gy,gz;//存储原始数据
float aax,aay,aaz,ggx,ggy,ggz;//存储量化后的数据
float Ax,Ay,Az;//单位 g(9.8m/s^2)
float Gx,Gy,Gz;//单位 °/s
 
float Angel_accX,Angel_accY,Angel_accZ;//存储加速度计算出的角度
 
long LastTime,NowTime,TimeSpan;//用来对角速度积分的



void setup() {
    // join I2C bus (I2Cdev library doesn't do this automatically)
    Wire.begin();

    // initialize serial communication
    // (38400 chosen because it works as well at 8MHz as it does at 16MHz, but
    // it's really up to you depending on your project)
    Serial.begin(38400);

    // initialize device
    Serial.println("Initializing I2C devices...");
    accelgyro.initialize();

    // verify connection
    Serial.println("Testing device connections...");
    Serial.println(accelgyro.testConnection() ? "MPU6050 connection successful" : "MPU6050 connection failed");

}

void loop() {
    accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);
    aax=ax/16384.00-Ax_offset;
    aay=ay/16384.00-Ay_offset;
    aaz=az/16384.00-Az_offset;
    ggx=gx/131.00-Gx_offset;
    ggy=gy/131.00-Gy_offset;
    ggz=gz/131.00-Gz_offset;
    NowTime=millis();//获取当前程序运行的毫秒数
  TimeSpan=NowTime-LastTime;//积分时间这样算不是很严谨
//下面三行就是通过对角速度积分实现各个轴的角度测量，当然假设各轴的起始角度都是0
  Gx=Gx+(ggx)*TimeSpan/1000.00;
  Gy=Gy+(ggy)*TimeSpan/1000.00;
  Gz=Gz+(ggz)*TimeSpan/1000.00;
  Ax=Ax+(aax)*TimeSpan/1000.00;
  Ay=Ay+(aay)*TimeSpan/1000.00;
  Az=Az+(aaz)*TimeSpan/1000.00;
 
  LastTime=NowTime;






   
    Serial.print("a/g:\t");
    Serial.print(Ax); Serial.print("\t");
    Serial.print(Ay); Serial.print("\t");
    Serial.print(Az); Serial.print("\t");

    
    Serial.print(Gx); Serial.print("\t");
    Serial.print(Gy); Serial.print("\t");
    Serial.println(Gz); Serial.print("\t");


}
