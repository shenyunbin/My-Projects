//YWROBOT
//Compatible with the Arduino IDE 1.0
//Library version:1.1
#include <Wire.h> 
#include <LiquidCrystal_I2C.h>

LiquidCrystal_I2C lcd(0x27,20,4);  // set the LCD address to 0x27 for a 16 chars and 2 line display
void lcd_clrdisp(int line,char *p);
void lcd_disp(int line,char *p);
void lcd_clr(int line);
void lcd_dispint(int line,unsigned long int int16);

int itemp[17];

void setup()
{
  lcd.init();                      // initialize the lcd 
  lcd.init();
  // Print a message to the LCD.
  lcd.backlight();
  lcd_disp(0,"Select the Num:");
  lcd_dispint(1,12343337);
  lcd_disp(2,"Is that OK?");
}


void loop()
{
}

void lcd_clrdisp(int line,char *p)
{
  lcd.setCursor(0,line);
  lcd.print("                    ");
  lcd.setCursor(0,line);
  lcd.print(p);
}
void lcd_disp(int line,char *p)
{
  lcd.setCursor(0,line);
  lcd.print(p);
}
void lcd_clr(int line)
{
  lcd.setCursor(0,line);
  lcd.print("                    ");
}

void lcd_dispint(int line,unsigned long int int16)
{
  lcd.setCursor(0,line);
  unsigned long int ienable,i;
  unsigned long int iint;
  ienable=0;
  iint=int16;
  for(i=0;i<16;i++)
    {
	itemp[i]=0;
	itemp[i]=iint%10;
	iint=iint/10;
	itemp[i]+=48;
    }
  for(i=16;i>0;i--)
    {
	if(itemp[i-1]!=48){ienable=1;}
	if(ienable==1){lcd.write(itemp[i-1]);}
    }

}
