/*
LCD  Arduino
PIN1 = GND
PIN2 = 5V
RS(CS) = 8; 
RW(SID)= 9; 
EN(CLK) = 3;
PIN15 PSB = GND;
*/
 
#include "LCD12864RSPI.h"
#define AR_SIZE( a ) sizeof( a ) / sizeof( a[0] )
 
unsigned char show0[]={0xBC,0xAB,0xBF,0xCD,0xB9,0xA4,0xB7,0xBB};//极客工坊
unsigned char show1[]="geek-workshop";

 
void setup()
{
LCDA.Initialise(); // 屏幕初始化
delay(100);
}
 
void loop()
{
LCDA.CLEAR();//清屏
delay(100);
LCDA.DisplayString(0,2,show0,AR_SIZE(show0));//第一行第三格开始，显示文字极客工坊
delay(100);
LCDA.DisplayString(2,1,show1,AR_SIZE(show1));;//第三行第二格开始，显示文字geek-workshop
delay(5000);
LCDA.CLEAR();//清屏
delay(5000);
}

