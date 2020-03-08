// charpter III   测量压力
#include "U8glib.h"
U8GLIB_ST7920_128X64_1X u8g(8, 6, 7);// SPI Com: SCK = en = 8, MOSI = rw = 6, CS = di = 7
//A0-X+,A1-Y+,A2-X-,A3-Y-
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

void getZ1()
{
  pinMode(A2,OUTPUT);
  pinMode(A1,OUTPUT);
  digitalWrite(A2,LOW);
  digitalWrite(A1,HIGH);
  pinMode(A0,INPUT);
}
void getZ2()
{
  pinMode(A2,OUTPUT);
  pinMode(A1,OUTPUT);
  digitalWrite(A2,LOW);
  digitalWrite(A1,HIGH);
  pinMode(A3,INPUT);
}
void setup() {
  // put your setup code here, to run once:

}
//A0--Y+ A1--X+;A2--Y-;A3--X-
void loop() {
  // put your main code here, to run repeatedly:
  int Z1,Z2;
  int x,x0,y,y0;
  int RX;
  getZ1();
  Z1=analogRead(A0);
  getZ2();
  Z2=analogRead(A3);
  getx();
  x0=analogRead(A1);
  x=x/8;
  delay(20);
 RX=500*x/1024*(Z2/Z1-1);
 u8g.firstPage();
  do {
      u8g.setFont(u8g_font_6x10);
      u8g.setPrintPos(0, 20); 
      u8g.print(RX,DEC);
     //display
  }while(u8g.nextPage());
}
