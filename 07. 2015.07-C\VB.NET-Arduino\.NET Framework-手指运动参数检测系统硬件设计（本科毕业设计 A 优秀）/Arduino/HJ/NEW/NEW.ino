//显示按键位置共计8个按键
#include "U8glib.h"
U8GLIB_ST7920_128X64_1X u8g(8, 6, 7);// SPI Com: SCK = en = 8, MOSI = rw = 6, CS = di = 7
int x[100],y[100],xy[2];
int nulls;
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
  if(number>(num/2)){
    in[0]=sumx/number;
    in[1]=sumy/number;
    return 1;
  }
  else return 0;
}

//数组数据更新
void CIR(int a[], int num, int temp) {
  int i;
  for (i = 0; i < num - 1; i++) {
    a[i] = a[i + 1];
  }
  a[i] = temp;
}

void CLEAR(int a[], int num) {
  for (int i = 0; i < num ; i++) {
    a[i] = 0;
  }
}

void setup() {
  // put your setup code here, to run once:

}

void loop() {
  if(GETXY(xy,5)&&nulls<10){
    CIR(x,100,xy[0]);
    CIR(y,100,xy[1]);
    nulls=0;
  }
  else if(GETXY(xy,5)&&nulls>10){
    CLEAR(x,100);
    CLEAR(x,100);
    CIR(x,100,xy[0]);
    CIR(y,100,xy[1]);
    nulls=0;
  }
  else nulls++;

    
  u8g.firstPage();
  do {
      u8g.setFont(u8g_font_6x10);
      u8g.setPrintPos(64, 16); 
      u8g.print(12,DEC);
  }while(u8g.nextPage());
}
