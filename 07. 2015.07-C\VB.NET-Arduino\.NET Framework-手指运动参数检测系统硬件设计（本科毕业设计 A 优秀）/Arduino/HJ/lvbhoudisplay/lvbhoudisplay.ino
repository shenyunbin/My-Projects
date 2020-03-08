#include "U8glib.h"
U8GLIB_ST7920_128X64_1X u8g(8, 6, 7);// SPI Com: SCK = en = 8, MOSI = rw = 6, CS = di = 7

void getx()
{
 pinMode(A2, OUTPUT);
 pinMode(A0, OUTPUT);
 digitalWrite(A2, LOW);
 digitalWrite(A0, HIGH);
 digitalWrite(A3, LOW);
 digitalWrite(A1, LOW);
 pinMode(A3, INPUT);
 pinMode(A1, INPUT);
}

void gety()
{
 pinMode(A3, OUTPUT);
 pinMode(A1, OUTPUT);
 digitalWrite(A3, LOW);
 digitalWrite(A1, HIGH);
 digitalWrite(A2, LOW);
 digitalWrite(A0, LOW);
 pinMode(A2, INPUT);
 pinMode(A0, INPUT);
}


void setup() 
{
  // put your setup code here, to run once:

}

void loop() 
{
  //平滑处理采集的数据
  int xs0[12],ys0[12];//用以存储未处理前的数据;
  int x,y,x0,y0;
  int n,m;
  int xb,yb=0;//bevor
  int xn0,yn0,xn,yn;//nachdem
  for(n=0;n<12;n++)
   {
     getx();
     x0=analogRead(A1);
     xs0[n]=x0;
     gety();
     y0=analogRead(A0);
     ys0[n]=y0;
   }
   for(m=1;m<11;m++)
   {
     xb=xb+xs0[m];
     yb=yb+ys0[m];
   }
     xn0=xb/10;
     yn0=yb/10;
     xn=xn0/8;
     yn=64-yn0/16;
     //数据显示测试
      u8g.firstPage();
  do {
      u8g.setFont(u8g_font_6x10);
      u8g.setPrintPos(0, 20); 
      u8g.print(xn0,DEC);
      u8g.setPrintPos(64, 20); 
      u8g.print(xn,DEC);
      u8g.setPrintPos(0, 40); 
      u8g.print(yn0,DEC);
      u8g.setPrintPos(64, 40); 
      u8g.print(yn,DEC);
  }while(u8g.nextPage());
}
   
  
  
