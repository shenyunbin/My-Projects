 #include <Wire.h>  //调用arduino自带的I2C库
     
    #define Register_ID 0
    #define Register_2D 0x2D
    #define Register_X0 0x32
    #define Register_X1 0x33
    #define Register_Y0 0x34
    #define Register_Y1 0x35
    #define Register_Z0 0x36
    #define Register_Z1 0x37
     
     
    int ADXAddress = 0xA7>>1;  //转换为7位地址
    int reading = 0;
    int val = 0;
    int X0,X1,X_out;
    int Y0,Y1,Y_out;
    int Z1,Z0,Z_out;
    float Xg,Yg,Zg,temp;
    long int Xv,Yv,Zv;
     
void ADXL345()
{
  Wire.beginTransmission(ADXAddress);
      Wire.write(Register_X0);
      Wire.write(Register_X1);
      Wire.endTransmission();
      Wire.requestFrom(ADXAddress,2);
      if(Wire.available()<=2);
      {
        X0 = Wire.read();
        X1 = Wire.read();
        X1 = X1<<8;
        X_out = X0+X1;
      }
     
      Wire.beginTransmission(ADXAddress);
      Wire.write(Register_Y0);
      Wire.write(Register_Y1);
      Wire.endTransmission();
      Wire.requestFrom(ADXAddress,2);
      if(Wire.available()<=2);
      {
        Y0 = Wire.read();
        Y1 = Wire.read();
        Y1 = Y1<<8;
        Y_out = Y0+Y1;
      }
     
      Wire.beginTransmission(ADXAddress);
      Wire.write(Register_Z0);
      Wire.write(Register_Z1);
      Wire.endTransmission();
      Wire.requestFrom(ADXAddress,2);
      if(Wire.available()<=2);
      {
        Z0 = Wire.read();
        Z1 = Wire.read();
        Z1 = Z1<<8;
        Z_out = Z0+Z1;
      }
    
}



    void setup()
    {
      Wire.begin();  //初始化I2C
      delay(100);
      Wire.beginTransmission(ADXAddress);
      Wire.write(Register_2D);
      Wire.write(8);
      Wire.endTransmission();
      Serial.begin(9600);
      
    }
     
    void loop()
    {
      float gall;
    
     ADXL345();
      Xg = X_out/256.00;//把输出结果转换为重力加速度g,精确到小数点后2位。
      Yg = Y_out/256.00;
      Zg = Z_out/256.00;
     // if(fabs(Xg^2+Yg^2+Zg^2-1)<0.01)
    //  Xv=Xv+Xg*10;
     // Yv=Yv+Yg*10;
      //Zv=Zv+Zg
      //if
      
      gall=(X_out^2+X_out^2+X_out^2-256^2)/100.00;
      if(fabs(gall-temp)>0.1)
      {
        temp=(X_out^2+X_out^2+X_out^2-256^2)/100.00;
        Xv=Xv+Xg*1000;
        Yv=Yv+Yg*10;
        Zv=Zv+(Zg-1)*1000;      
      }
      else
      {
        Xv=0;
        Yv=0;
        Zv=0;        
      }
      
       Serial.print("  X="); //使屏幕显示文字X=
      Serial.print(Xg);
      Serial.print("  Y=");
      Serial.print(Yg);
      Serial.print("  Z=");
      Serial.print(Zg);
      Serial.print("  GALL=");
      Serial.println(temp);
      Serial.print("  Xv="); //使屏幕显示文字X=
      Serial.print(Xv);
      Serial.print("  Yv=");
      Serial.print(Yv);
      Serial.print("  Zv=");
      Serial.println(Zv);
      long int aa=(Xv)^2+(Yv)^2+(Zv)^2;
      Serial.println(aa);
      
      delay(100);  //延时0.3秒，刷新频率这里进行调整
     
}








