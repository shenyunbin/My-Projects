
#include <MsTimer2.h>
#include "Wire.h"
#include "I2Cdev.h"
#include "MPU6050.h"

#define Ax_offset 0
#define Ay_offset 0
#define Az_offset 0
#define Gx_offset -2.73
#define Gy_offset -0.21
#define Gz_offset 0.63

long LastTime, NowTime,TimeSpan; //用来对角速度积分的


MPU6050 accelgyro;

int16_t ax, ay, az;
int16_t gx, gy, gz;
int aax[50],aay[50],aaz[50],ggx,ggy,ggz;
int GX[50],GY[50],GZ[50];
int ii=0;
int DataSize=50;


void INT16_SEND(int int16)
{
   Serial.write(byte(int16>>14)&0x3F);
   Serial.write(byte(int16>>7)&0x3F);
   Serial.write(byte(int16)&0x3F);  
}
 void DATA_SEND()
 {
 Serial.write(254);
 for(int i=0;i<DataSize;i++)
 {
 INT16_SEND(aax[i]);
 INT16_SEND(aay[i]);
 INT16_SEND(aaz[i]);
 INT16_SEND(GX[i]);
 INT16_SEND(GY[i]);
 INT16_SEND(GZ[i]);
 }
 Serial.write(255);
 }


void flash() {
}

void setup() {
    Wire.begin();
    Serial.begin(115200);
    accelgyro.initialize();
  //    MsTimer2::set(10, flash); // 500ms period
//  MsTimer2::start();
}

void loop() {    
    if(ii<(DataSize-1))
  {
    delay(8);
    ii++;
    accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);
    aax[ii]=ax/16.38400-Ax_offset;
    aay[ii]=ay/16.38400-Ay_offset;
    aaz[ii]=az/16.38400-Az_offset;
    ggx=gx/1.3100-Gx_offset;
    ggy=gy/1.3100-Gy_offset;
    ggz=gz/1.3100-Gz_offset;
    GX[ii]=GX[ii-1]+ggx*10/1000;
    GY[ii]=GY[ii-1]+ggx*10/1000;
    GZ[ii]=GZ[ii-1]+ggx*10/1000;
  }
     if(ii==(DataSize-1))
    {
    ii=0;
    accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);
    aax[ii]=ax/16.38400-Ax_offset;
    aay[ii]=ay/16.38400-Ay_offset;
    aaz[ii]=az/16.38400-Az_offset;
    ggx=gx/1.3100-Gx_offset;
    ggy=gy/1.3100-Gy_offset;
    ggz=gz/1.3100-Gz_offset;
    GX[ii]=GY[50]+ggx*10/1000;
    GY[ii]=GY[50]+ggx*10/1000;
    GZ[ii]=GZ[50]+ggx*10/1000;
    DATA_SEND();
    }
}


