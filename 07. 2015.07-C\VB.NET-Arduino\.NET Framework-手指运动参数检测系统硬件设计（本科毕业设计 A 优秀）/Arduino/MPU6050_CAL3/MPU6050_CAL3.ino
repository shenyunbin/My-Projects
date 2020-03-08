#include <MsTimer2.h>               //瀹氭椂鍣ㄥ簱鐨�澶存枃浠�
#include "Wire.h"

#include "I2Cdev.h"
#include "MPU6050.h"


MPU6050 accelgyro;
#define Ax_offset 0
#define Ay_offset 0
#define Az_offset 0
#define Gx_offset -2.73
#define Gy_offset -0.21
#define Gz_offset 0.63

int16_t ax, ay, az;
int16_t gx, gy, gz; //瀛樺偍鍘熷鏁版嵁
float aax, aay, aaz, ggx, ggy, ggz; //瀛樺偍閲忓寲鍚庣殑鏁版嵁
float Ax, Ay, Az; //鍗曚綅 g(9.8m/s^2)
float Ax0, Ay0, Az0; //鍗曚綅 g(9.8m/s^2)
float Ax1, Ay1, Az1; //鍗曚綅 g(9.8m/s^2)
float Vx, Vy, Vz; //鍗曚綅 g(9.8m/s^2)
float Gx, Gy, Gz; //鍗曚綅 掳/s
float Gx1, Gy1, Gz1; //鍗曚綅 掳/s
float Gx0, Gy0, Gz0; //鍗曚綅 掳/s
float AG[6];
float Xconst, Yconst, Zconst, G0;

float Angel_accX, Angel_accY, Angel_accZ; //瀛樺偍鍔犻�搴﹁绠楀嚭鐨勮搴�
long LastTime, NowTime, TimeSpan = 100; //鐢ㄦ潵瀵硅閫熷害绉垎鐨�
float count;

void flash()                      
{
  Ax0 = ((AG[0]) * 10 / count);
  Ay0 = ((AG[1]) * 10 / count);
  Az0 = ((AG[2]) * 10 / count);
  if ((fabs(Ax0 - Ax1) + fabs(Ay0 - Ay1) ) < 0.5)
  {
    Gx0 = (float)(atan2(sqrt(Ay0 * Ay0 + Az0 * Az0), Ax0)); //X
    Gy0 = (float)(atan2(sqrt(Ax0 * Ax0 + Az0 * Az0), Ay0)); //Y�
    Gz0 = (float)(atan2(sqrt(Ax0 * Ax0 + Ay0 * Ay0), Az0)); //Z
    Gx = (float)(atan(cos(Gy0) / cos(Gz0)) * 180 / 3.14159265);
    Gy = (float)(-atan(cos(Gx0) / cos(Gz0)) * 180 / 3.14159265);
    G0 = sqrt(Ax0 * Ax0 + Ay0 * Ay0 + Az0 * Az0);
    Vx = Vy = Vz = 0;
    Serial.println("!!!!!!!!!!!!!!!!!!!!!!!!");//* 180 / 3.14159265
  }
  else
  {
    Gx1 = ((AG[3] / count) - Gx_offset) * TimeSpan / 1000.00;
    Gy1 = ((AG[4] / count) - Gy_offset) * TimeSpan / 1000.00;
    //Gz1 = ((AG[5] / count)- Gz_offset) * TimeSpan / 1000.00;
    Gx = Gx + Gy1;
    Gy = Gy + Gx1;
  }
  Ax1 = Ax0;
  Ay1 = Ay0;
  Az1 = Az0;

  Gx1 = Gx * 3.14159265 / 180.00;
  Gy1 = Gy * 3.14159265 / 180.00;
  Az = Az0 * cos(Gx1) + Ay0 * sin(Gx1);
  Ay = -Az0 * sin(Gx1) + Ay0 * cos(Gx1);
  Ax = Ax0 * cos(Gy1) + Az0 * sin(Gy1);
  Az = -Ax0 * sin(Gy1) + Az0 * cos(Gy1) - G0;


  //  Gz=acos(cos(Gy)*cos(Gy1));
  Vx = Vx + Ax * TimeSpan / 1000.00;
  Vy = Vy + Ay * TimeSpan / 1000.00;
  Vz = Vz + Az * TimeSpan / 1000.00;


  count = 0;
  for (int i = 0; i < 6; i++)AG[i] = 0;

  Serial.print("a/g:\t");
  Serial.print(Vx); Serial.print("\t");
  Serial.print(Vy); Serial.print("\t");
  Serial.print(Vz); Serial.print("\t");
  // Serial.print(Vx); Serial.print("\t");
  // Serial.print(Vy); Serial.print("\t");
  // Serial.print(Vz); Serial.print("\t");


  Serial.print(Gx); Serial.print("\t");
  Serial.println(Gy); Serial.print("\t");
  //Serial.println(Gz); Serial.print("\t");

  //    LastTime=NowTime;
}

void setup() {
  Wire.begin();
  Serial.begin(38400);
  Serial.println("Initializing...");
  accelgyro.initialize();
  MsTimer2::set(TimeSpan, flash);      
  MsTimer2::start();                

}

void loop() {
    accelgyro.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);
    aax = ax / 16384.00;
    aay = ay / 16384.00;
    aaz = az / 16384.00;
    ggx = gx / 131.00;
    ggy = gy / 131.00;
    ggz = gz / 131.00;
    AG[0] += aax;
    AG[1] += aay;
    AG[2] += aaz;
    AG[3] += ggx;
    AG[4] += ggy;
    AG[5] += ggz;
    count++;
}



