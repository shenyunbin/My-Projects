// charpter I NUMBER
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

void loop() {
  // put your main code here, to run repeatedly:
  int x,y,x0,y0,x1,y1;
  float v;
  getx();
  x0=analogRead(A1);
  if(x0<32)
  {
    x0=0;
  }
  x=x0/8;
  gety();
  y0=analogRead(A0);
   if(y0<32)
  {
    y0=0;
  }
  y=y0/16;
  delay(200);
  getx();
  x1=analogRead(A1);
  if(x1<32)
  {
    x1=0;
  }
  gety();
  y1=analogRead(A0);
   if(y1<32)
  {
    y1=0;
  }
  v=sqrt((x1-x)*(x1-x)+(y1-y)*(y1-y));
 u8g.firstPage();
  do {
      u8g.setFont(u8g_font_6x10);
      u8g.setPrintPos(0, 20); 
      u8g.print(x0,DEC);
      u8g.setPrintPos(64, 20); 
      u8g.print(x,DEC);
      u8g.setPrintPos(0, 40); 
      u8g.print(y0,DEC);
      u8g.setPrintPos(64, 40); 
      u8g.print(y,DEC);
      u8g.setPrintPos(64,50);
      u8g.print(v,DEC);
   //display
  }while(u8g.nextPage());
}
