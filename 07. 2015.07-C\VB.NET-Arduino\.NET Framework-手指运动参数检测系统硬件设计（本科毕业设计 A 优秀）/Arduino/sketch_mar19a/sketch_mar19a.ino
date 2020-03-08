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
unsigned char logo0[]={                                         //笑脸
      0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0x01,0xFF,
      0x80,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0x1F,0xFF,
      0xF8,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0xFF,0xFF,
      0xFF,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x03,0xFE,0x00,
      0x7F,0xC0,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x0F,0xF0,0x00,
      0x0F,0xF0,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x1F,0x80,0x00,
      0x01,0xF8,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x3E,0x00,0x00,
      0x00,0x7C,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0xF8,0x00,0x00,
      0x00,0x1F,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x01,0xF0,0x00,0x00,
      0x00,0x0F,0x80,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x03,0xE0,0x00,0x00,
      0x00,0x07,0xC0,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x07,0xC0,0x00,0x00,
      0x00,0x01,0xE0,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x07,0x80,0x00,0x00,
      0x00,0x00,0xE0,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x0F,0x00,0x00,0x00,
      0x00,0x00,0xF0,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x1E,0x00,0x00,0x00,
      0x00,0x00,0x78,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x3C,0x00,0x00,0x00,
      0x00,0x00,0x3C,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x38,0x00,0x40,0x00,
      0x00,0x80,0x1C,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x78,0x03,0xF8,0x00,
      0x07,0xF0,0x1E,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x70,0x07,0xFE,0x00,
      0x0F,0xFC,0x0E,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0xF0,0x1F,0xBF,0x00,
      0x3F,0x7E,0x0F,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0xE0,0x3E,0x0F,0x80,
      0x7C,0x1F,0x07,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0xE0,0x7C,0x07,0xC0,
      0xF8,0x0F,0x87,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x01,0xE0,0xF8,0x01,0xE1,
      0xF0,0x03,0xC3,0x80,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x01,0xC0,0xF0,0x01,0xE1,
      0xE0,0x03,0xC3,0x80,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x01,0xC0,0xE0,0x00,0xE1,
      0xC0,0x01,0xC3,0x80,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x01,0xC0,0x00,0x00,0x00,
      0x00,0x00,0x03,0x80,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x03,0x80,0x00,0x00,0x00,
      0x00,0x00,0x01,0xC0,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x03,0x80,0x00,0x00,0x00,
      0x00,0x00,0x01,0xC0,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x03,0x80,0x00,0x00,0x00,
      0x00,0x00,0x01,0xC0,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x03,0x80,0x00,0x00,0x00,
      0x00,0x00,0x01,0xC0,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x03,0x80,0x00,0x00,0x00,
      0x00,0x00,0x01,0xC0,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x03,0x80,0x00,0x00,0x00,
      0x00,0x00,0x01,0xC0,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x03,0x80,0x00,0x00,0x00,
      0x00,0x00,0x01,0xC0,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x03,0x80,0x00,0x00,0x00,
      0x00,0x00,0x01,0xC0,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x03,0x80,0x00,0x00,0x00,
      0x00,0x00,0x01,0xC0,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x03,0x80,0x00,0x00,0x00,
      0x00,0x00,0x01,0xC0,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x01,0xC0,0x00,0x00,0x00,
      0x00,0x00,0x01,0x80,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x01,0xC0,0x00,0x00,0x00,
      0x00,0x00,0x03,0x80,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x01,0xC0,0x00,0x00,0x00,
      0x00,0x00,0x03,0x80,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x01,0xE0,0x00,0x00,0x00,
      0x00,0x00,0x03,0x80,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0xE0,0x00,0x00,0x00,
      0x00,0x00,0x07,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0xE0,0x00,0x00,0x00,
      0x00,0x00,0x07,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0xF0,0x00,0x0C,0x00,
      0x0C,0x00,0x0F,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x70,0x00,0x1E,0x00,
      0x1E,0x00,0x0E,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x78,0x00,0x1F,0x00,
      0x3E,0x00,0x1E,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x38,0x00,0x07,0x80,
      0xF8,0x00,0x1C,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x3C,0x00,0x03,0xE1,
      0xF0,0x00,0x3C,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x1E,0x00,0x01,0xFF,
      0xE0,0x00,0x78,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x0F,0x00,0x00,0xFF,
      0x80,0x00,0xF0,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x07,0x00,0x00,0x3F,
      0x00,0x00,0xF0,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x07,0x80,0x00,0x00,
      0x00,0x01,0xE0,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x03,0xE0,0x00,0x00,
      0x00,0x07,0xC0,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x01,0xF0,0x00,0x00,
      0x00,0x0F,0x80,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0xF8,0x00,0x00,
      0x00,0x1F,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x3E,0x00,0x00,
      0x00,0x7E,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x1F,0x80,0x00,
      0x01,0xF8,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x0F,0xE0,0x00,
      0x07,0xF0,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x03,0xFE,0x00,
      0x3F,0xC0,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0xFF,0xFF,
      0xFF,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0x1F,0xFF,
      0xF8,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0x01,0xFF,
      0x80,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
      0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
};
 
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
delay(100);
LCDA.DrawFullScreen(logo0);
delay(5000);
}

