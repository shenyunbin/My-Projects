// charpter II DISPLAY
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

void setup() {
  // put your setup code here, to run once:

}

void loop()
{
  // put your main code here, to run repeatedly:
int x,x0,y,y0;
int n,m=0;
int n0=0;
int xd[128],yd[128];
for(n=0;n<256;n++)
{
  getx();
  x0=analogRead(A1);
  gety();
  y0=analogRead(A0);
  x=x0/8;
  y=64-y0/16;
  if(x>0||y>0)
  {
  xd[m]=x;
  yd[m]=y;
  m++;
  }
  delay(10);
  if(m==128)
  break;
}//save number
    u8g.firstPage();
  do {
    for(n=0;n<128;n++)
    {
    u8g.drawPixel(xd[n0],yd[n0]);
    }
  }while(u8g.nextPage());
}
