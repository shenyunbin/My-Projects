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

void loop() {
  // put your main code here, to run repeatedly:
  unsigned int x,y,x0,y0;
  getx();
  x0=analogRead(A1)-32;
  x=x0/8;
  gety();
  y0=analogRead(A0)-32;
  y=64-y0/16;
   u8g.firstPage();
  do {
    u8g.drawPixel(x,y);
    //display
  }while(u8g.nextPage());
}
