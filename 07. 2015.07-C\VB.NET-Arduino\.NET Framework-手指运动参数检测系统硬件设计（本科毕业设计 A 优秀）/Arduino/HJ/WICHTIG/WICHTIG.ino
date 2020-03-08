


/*
.	LCD  Arduino
PIN1 = GND
PIN2 = 5V.
RS(CS) = 8;
RW(SID)= 9;
EN(CLK) = 3;
PIN15 PSB = GND;
*/


//将触摸屏连接到ANALOGIN 0~4
#include "U8glib.h"
U8GLIB_ST7920_128X64_1X u8g(8, 6, 7);// SPI Com: SCK = en = 8, MOSI = rw = 6, CS = di = 7



//static const int latchPin = 6;
//static const int clockPin = 8;
//static const int dataPin = 7;


//*********************延时函数************************//
/*
void delayns(void)
{
  delayMicroseconds(80);
}


void WriteByte(int dat)
{
  digitalWrite(latchPin, HIGH);
  delayns();
  shiftOut(dataPin, clockPin, MSBFIRST, dat);
  digitalWrite(latchPin, LOW);
}


void WriteCommand(int CMD)
{
  int H_data, L_data;
  H_data = CMD;
  H_data &= 0xf0;           //屏蔽低4位的数据
  L_data = CMD;             //xxxx0000格式
  L_data &= 0x0f;           //屏蔽高4位的数据
  L_data <<= 4;             //xxxx0000格式
  WriteByte(0xf8);          //RS=0，写入的是指令；
  WriteByte(H_data);
  WriteByte(L_data);
}


void WriteData(int CMD)
{
  int H_data, L_data;
  H_data = CMD;
  H_data &= 0xf0;           //屏蔽低4位的数据
  L_data = CMD;             //xxxx0000格式
  L_data &= 0x0f;           //屏蔽高4位的数据
  L_data <<= 4;             //xxxx0000格式
  WriteByte(0xfa);          //RS=1，写入的是数据
  WriteByte(H_data);
  WriteByte(L_data);
}



void Initialise()
{
  pinMode(latchPin, OUTPUT);
  pinMode(clockPin, OUTPUT);
  pinMode(dataPin, OUTPUT);
  digitalWrite(latchPin, LOW);
  delayns();

  WriteCommand(0x30);        //功能设定控制字
  WriteCommand(0x0c);        //显示开关控制字
  WriteCommand(0x01);        //清除屏幕控制字
  WriteCommand(0x06);        //进入设定点控制字
}


void CLEAR(void)
{
  WriteCommand(0x30);//
  WriteCommand(0x01);//清除显示
}


void DisplayString(int X, int Y, unsigned char *ptr, int dat)
{
  int i;

  switch (X)
  {
    case 0:  Y |= 0x80; break;

    case 1:  Y |= 0x90; break;

    case 2:  Y |= 0x88; break;

    case 3:  Y |= 0x98; break;

    default: break;
  }
  WriteCommand(Y); // 定位显示起始地址

  for (i = 0; i < dat; i++)
  {
    WriteData(ptr[i]);//显示汉字时注意码值，连续两个码表示一个汉字
  }
}



void DisplaySig(int M, int N, int sig)
{
  switch (M)
  {
    case 0:  N |= 0x80; break;

    case 1:  N |= 0x90; break;

    case 2:  N |= 0x88; break;

    case 3:  N |= 0x98; break;

    default: break;
  }
  WriteCommand(N); // 定位显示起始地址
  WriteData(sig); //输出单个字符
}




void DrawFullScreen(unsigned char *p)
{
  int ygroup, x, y, i;
  int temp;
  int tmp;

  for (ygroup = 0; ygroup < 64; ygroup++)    //写入液晶上半图象部分
  { //写入坐标
    if (ygroup < 32)
    {
      x = 0x80;
      y = ygroup + 0x80;
    }
    else
    {
      x = 0x88;
      y = ygroup - 32 + 0x80;
    }
    WriteCommand(0x34);        //写入扩充指令命令
    WriteCommand(y);           //写入y轴坐标
    WriteCommand(x);           //写入x轴坐标
    WriteCommand(0x30);        //写入基本指令命令
    tmp = ygroup * 16;
    for (i = 0; i < 16; i++)
    {
      temp = p[tmp++];
      WriteData(temp);
    }
  }
  WriteCommand(0x34);        //写入扩充指令命令
  WriteCommand(0x36);        //显示图象
}

int x0 = 0, y0 = 0;*/


void setup()
{
  /*
  Serial.begin(9600);
  Initialise(); // initialize
  delay(100);
  */
}

void loop()
{
  /*CLEAR();// clear screen*/

  //touch screen
  int n=0;
  int m=0;
  int a0[256];
  int b0[256];
  for(n=0;n<256;n++)
  {
  pinMode(A2, OUTPUT);
  pinMode(A0, OUTPUT);
  digitalWrite(A2, LOW);
  digitalWrite(A0, HIGH);

  digitalWrite(A3, LOW);
  digitalWrite(A1, LOW);

  pinMode(A3, INPUT);
  pinMode(A1, INPUT);
  

  
  int x= analogRead(A1)-14;
  int x0 = x/8;
  a0[n]=x0;
  pinMode(A3, OUTPUT);
  pinMode(A1, OUTPUT);
  digitalWrite(A3, LOW);
  digitalWrite(A1, HIGH);

  digitalWrite(A2, LOW);
  digitalWrite(A0, LOW);

  pinMode(A2, INPUT);
  pinMode(A0, INPUT);
  delay(10);
  //xLow has analog port -14 !!
  int y = analogRead(A0)-14;
  int y0 = 64-y/16;
  b0[n]=y0;
  
  }
 

  //out to display x,y
/*   for(m=0;m<128;m++)
      {
  if (a0[m] < 8 || b0[m] < 4)
  {
    //splitNum(0, 1, 0);
    //splitNum(1, 1, 0);
   u8g.firstPage(); 
   do {
      /*u8g.setFont(u8g_font_6x10);
      u8g.setPrintPos(64, 20); 
      u8g.print(0);
      u8g.setPrintPos(64, 40); 
      u8g.print(0);
      u8g.drawPixel( 0,  0);
      } while(u8g.nextPage());

  }
  else
  {  */
    
     u8g.firstPage(); 
   do {
     /* u8g.setFont(u8g_font_6x10);
      u8g.setPrintPos(0, 20); 
      u8g.print(x0,DEC);
      u8g.setPrintPos(64, 20); 
      u8g.print(x,DEC);
      u8g.setPrintPos(0, 40); 
      u8g.print(y0,DEC);
      u8g.setPrintPos(64, 40); 
      u8g.print(y,DEC);*/
     
     // u8g.drawPixel( x0,  y0);
      for(m=0;m<256;m++)
      {
       u8g.drawPixel(a0[m],b0[m]);
       }
      }while(u8g.nextPage());
}
  

  

//split number and display to screen//
/*void splitNum(int dx, int dy, int sn)
{
  int d0 = 0;
  int d1 = 0;
  int d2 = 0;
  int d3 = 0;
  if (sn < 10) {
    d0 = sn;
    d1 = 0;
    d2 = 0;
    d3 = 0;
  }
  else if (sn < 100) {

    d0 = sn % 10;
    d1 = sn / 10;
    d2 = 0;
    d3 = 0;
  }
  else if (sn < 1000) {
    d0 = sn % 10;
    d1 = sn % 100 / 10;
    d2 = sn / 100;
    d3 = 0;
  }
  else {
    d0 = sn % 10;
    d1 = sn % 100 / 10;
    d2 = sn % 1000 / 100;
    d3 = sn / 1000;
  }

  
  /*DisplaySig(dx, dy, d3 + 48);
  DisplaySig(dx, dy + 1, d2 + 48);
  DisplaySig(dx, dy + 2, d1 + 48);
  DisplaySig(dx, dy + 3, d0 + 48);
  u8g.firstPage(); 
   do {
    u8g.drawStr(dx, dy,d3 + 48);
    u8g.drawStr(dx, dy + 1, d2 + 48);
    u8g.drawStr(dx, dy + 2, d1 + 48);
    u8g.drawStr(dx, dy + 3, d0 + 48);
  } while( u8g.nextPage() );*/




