//显示按键位置共计8个按键
#include "U8glib.h"
U8GLIB_ST7920_128X64_1X u8g(8, 6, 7);// SPI Com: SCK = en = 8, MOSI = rw = 6, CS = di = 7
int x[10],y[10],xy[2];
int GETXONCE()
{
 pinMode(A2, OUTPUT);
 pinMode(A0, OUTPUT);
 digitalWrite(A2, LOW);
 digitalWrite(A0, HIGH);
 digitalWrite(A3, LOW);
 digitalWrite(A1, LOW);
 pinMode(A3, INPUT);
 pinMode(A1, INPUT);
 return analogRead(A1)/8;;
}

int GETYONCE()
{
 pinMode(A3, OUTPUT);
 pinMode(A1, OUTPUT);
 digitalWrite(A3, LOW);
 digitalWrite(A1, HIGH);
 digitalWrite(A2, LOW);
 digitalWrite(A0, LOW);
 pinMode(A2, INPUT);
 pinMode(A0, INPUT);
 return 64-analogRead(A0)/16;
}

int GETX(int in[],int num)
{
for(int i=0;i<num;i++)in[0]+=GETXONCE()/num;
}

int GETXY(int in[],int num)
{
  int yi,xi,sumx,sumy,number;
  for(int i=0;i<num;i++){
    xi=GETXONCE();
    yi=GETYONCE();
    if(yi>0||xi>0){
      sumx+=xi;
      sumy+=yi;
      number++;
    }
  }
  in={sumx/number,sumy/number};
}

//数组数据更新
void CIR(int a[], int num, int temp) {
  int i;
  for (i = 0; i < num - 1; i++) {
    a[i] = a[i + 1];
  }
  a[i] = temp;
}

void setup() {
  // put your setup code here, to run once:

}

void loop() {
  int x,y;
  int p=0;
  x=GETX();
  y=GETY();
  if(4<x<32&&32<y<62)
  {
   p=1;
  }
  if(32<x<64&&32<y<62)
  {
   p=2;
  }
   if(64<x<96&&32<y<62)
  {
   p=3;
  }
  if(96<x<128&&32<y<62)
  {
   p=4;
  }
  if(4<x<32&&0<y<32)
  {
    p=5;
  }
  if(32<x<64&&0<y<32)
  {
    p=6;
  }
  if(64<x<96&&0<y<32)
  {
    p=7;
  }
  if(96<x<128&&0<y<32)
  {
    p=8;
  }
    
  u8g.firstPage();
  do {
      u8g.setFont(u8g_font_6x10);
      u8g.setPrintPos(64, 16); 
      u8g.print(p,DEC);
  }while(u8g.nextPage());
}
