//YWROBOT
//Compatible with the Arduino IDE 1.0
//Library version:1.1
#include <DS3231.h>
#include <Wire.h> 
#include <LiquidCrystal_I2C.h>
DS3231 Clock;
bool Century=false;
bool h12;
bool PM;
byte ADay, AHour, AMinute, ASecond, ABits;
bool ADy, A12h, Apm;

byte year, month, date, DoW, hour, minute, second,temperature,keys,mode,humicrit,raincrit,timecrit,timevalue,point;
byte watering,shape1;
byte interval,intervaltest,intervalcrit,wateringtime,berain,watertime,autowatertimecrit;


//int kminute;

LiquidCrystal_I2C lcd(0x27,20,4);  // set the LCD address to 0x27 for a 16 chars and 2 line display

int keypress();
int ifkeypressed();
void maindisp();
void maincontrol();
int humivalue();
int rainvalue();
void setdisp(int setyear,int setmonth,int setdate,int sethour,int setminute,int humicrit,int raincrit,int setpoint);
void manudisp();
void setvalue(byte setyear,byte setmonth,byte setdate,byte sethour,byte setminute,byte sethumicrit,byte setraincrit);
void waterstart();
void waterstop();
void manuwaterdisp();
void autowaterdisp();
void humiwatering();
void daychanged();
void setwateringvalue();
void raintest();
void autowateringcontrol();
void autocritchangeddisp();

void i2c_eeprom_write_byte( int deviceaddress, unsigned int eeaddress, byte data ) ;
byte i2c_eeprom_read_byte( int deviceaddress, unsigned int eeaddress );

void setup()
{
 Wire.begin();
 mode=0;
 watering=0;
 timecrit=20;
 humicrit=i2c_eeprom_read_byte(0x57, 0);
 raincrit=i2c_eeprom_read_byte(0x57, 1);
 interval=i2c_eeprom_read_byte(0x57, 2);
 autowatertimecrit=i2c_eeprom_read_byte(0x57, 3);
 pinMode(2,INPUT);
 pinMode(3,INPUT);
 pinMode(4,INPUT);
 pinMode(5,INPUT);
 pinMode(6,INPUT);
 pinMode(A0,OUTPUT);
 pinMode(A1,OUTPUT);
 maindisp(); 
 setwateringvalue();
}


void loop()
{ 
//  int second,minute,hour,date,month,year,temperature; 
switch(mode)
{
case 0:
{
    if(Clock.getSecond()==0)
    {
      daychanged();
      maincontrol();
      maindisp();
      raintest();
      autowateringcontrol();
    }
    if(digitalRead(5)==HIGH){wateringtime=autowatertimecrit;autowateringcontrol();} 
    if(digitalRead(6)==HIGH){wateringtime=0;autowateringcontrol();maindisp();}
    if(digitalRead(4)==HIGH){autowatertimecrit=(autowatertimecrit+1)%100;i2c_eeprom_write_byte(0x57,3,autowatertimecrit);autocritchangeddisp();delay(200);}
    if(digitalRead(3)==HIGH){autowatertimecrit=(autowatertimecrit+99)%100;i2c_eeprom_write_byte(0x57,3,autowatertimecrit);autocritchangeddisp();delay(200);}   //keychanged
    autowaterdisp();
    break;
}
case 1:
{
    if(Clock.getSecond()==0)
    {     
     if(timevalue>0)timevalue-=1;
     if(timevalue<=0){timevalue=0;waterstop();} 
     manudisp();    
    }
    if(digitalRead(4)==HIGH){timecrit=(timecrit+5)%65; manudisp();} 
    if(digitalRead(3)==HIGH){timecrit=(timecrit+60)%65; manudisp();}
    if(digitalRead(5)==HIGH){timevalue=timecrit;waterstart(); manudisp();}
    if(digitalRead(6)==HIGH){timevalue=0;waterstop(); manudisp();}    //keychanged
    manuwaterdisp();
    break;
}
case 2:
{
    if(digitalRead(6)==HIGH){point=(point+1)%7;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);} 
    if(digitalRead(5)==HIGH){point=(point+6)%7;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);}
    if(digitalRead(4)==HIGH){  switch(point)
                                                {
                                                case 0:{year=(year+1)%31;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}
                                                case 1:{month=(month+1)%13;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}
                                                case 2:{date=(date+1)%32;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}  
                                                case 3:{hour=(hour+1)%24;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}  
                                                case 4:{minute=(minute+1)%60;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}   
                                                case 5:{humicrit=(humicrit+1)%100;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}
                                                case 6:{raincrit=(raincrit+1)%100;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}   
                                                default: break;
                                                }
                             }
    if(digitalRead(3)==HIGH){  switch(point)    //keychanged
                                                {    
                                                case 0:{year=(year+30)%31;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}
                                                case 1:{month=(month+12)%13;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}
                                                case 2:{date=(date+31)%32;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}  
                                                case 3:{hour=(hour+23)%24;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}  
                                                case 4:{minute=(minute+59)%60;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}   
                                                case 5:{humicrit=(humicrit+99)%100;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}
                                                case 6:{raincrit=(raincrit+99)%100;setdisp(year,month,date,hour,minute,humicrit,raincrit,point);break;}   
                                                default: break;
                                                }                                                
                             }                       
    break;
}
default: break;


}

if(digitalRead(2)==HIGH)
{ 
  if(mode==2)setvalue(year,month,date,hour,minute,humicrit,raincrit);
  lcd.setCursor(14,1); 
  lcd.print("OK!...");
  delay(100);  
  mode+=1;
  if(mode>2)mode=0;
  switch(mode)
  {
  case 0:maindisp();break;
  case 1:manudisp();break;
  case 2:setdisp(year,month,date,hour,minute,humicrit,raincrit,1);point=1;break;
  default: break;
  }


}

}




int keypress()
{
if(digitalRead(2)==HIGH)return 2;
if(digitalRead(3)==HIGH)return 3;
if(digitalRead(4)==HIGH)return 4;
if(digitalRead(5)==HIGH)return 5;
if(digitalRead(6)==HIGH)return 6;
}

int ifkeypressed()
{
if(digitalRead(2)==HIGH)return 1;
else if(digitalRead(3)==HIGH)return 1;
else if(digitalRead(4)==HIGH)return 1;
else if(digitalRead(5)==HIGH)return 1;
else if(digitalRead(6)==HIGH)return 1;
else return 0;
}

void maindisp()
{
//  second=Clock.getSecond();
  minute=Clock.getMinute();
  hour=Clock.getHour(h12, PM);
  date=Clock.getDate();
  month=Clock.getMonth(Century);
  year=Clock.getYear();
  
  temperature=Clock.getTemperature();
  
  
  lcd.init();                      // initialize the lcd 
 // Print a message to the LCD.
  lcd.backlight();
  lcd.setCursor(0,0);
  lcd.print(20,DEC);
  lcd.print(year,DEC);
  lcd.print("-");
  lcd.print(month,DEC);
  lcd.print("-");
  lcd.print(date,DEC);
  lcd.setCursor(15,0);
  lcd.print(hour/10,DEC);
  lcd.print(hour%10,DEC);
  lcd.print(":");
  lcd.print(minute/10,DEC);
  lcd.print(minute%10,DEC);
  lcd.setCursor(0,1);
  lcd.print("TEMP:");
  lcd.print(temperature,DEC);
  lcd.print("'C");
  lcd.setCursor(0,2);
  lcd.print("_.._:");
  lcd.print(humivalue(),DEC);
  lcd.setCursor(16,2);
  lcd.print("[");
  lcd.print(humicrit,DEC);
  lcd.print("]");
  lcd.setCursor(0,3);
  lcd.print("////:");
  lcd.print(rainvalue(),DEC);
  lcd.setCursor(16,3);
  lcd.print("[");
  lcd.print(raincrit,DEC);
  lcd.print("]");
}

void maincontrol()
{
if(Clock.getMinute()==0&&Clock.getHour(h12, PM)==watertime)
  {
    if(berain<=15&&interval>=intervaltest){humiwatering();}
    if(berain<=15&&interval>=intervalcrit){wateringtime=autowatertimecrit;interval=0;i2c_eeprom_write_byte(0x57,2,interval);}            //main control changged 2014.1.25
  }
}



int humivalue()
{
  int value;
  digitalWrite(A1,HIGH);
  value=102-analogRead(A3)/10;
  digitalWrite(A1,LOW);
  return value;
}
int rainvalue()
{
  return (102-analogRead(A2)/10);
}

void setdisp(int setyear,int setmonth,int setdate,int sethour,int setminute,int sethumicrit,int setraincrit,int setpoint)
{
  lcd.init();                      // initialize the lcd 

  lcd.backlight();
  lcd.setCursor(0,0);
  lcd.print("SET:");
  lcd.print(20,DEC);
  lcd.print(setyear,DEC);
  lcd.print("-");
  lcd.print(setmonth,DEC);
  lcd.setCursor(11,0);
  lcd.print("-");
  lcd.print(setdate,DEC);
  lcd.setCursor(15,0);
  lcd.print(sethour/10,DEC);
  lcd.print(sethour%10,DEC);
  lcd.setCursor(17,0);
  lcd.print(":");
  lcd.print(setminute/10,DEC);
  lcd.print(setminute%10,DEC);
  lcd.setCursor(0,2);
  lcd.print("_.._:");
  lcd.print(sethumicrit,DEC);
  lcd.setCursor(12,2);
  lcd.print("////:");
  lcd.print(setraincrit,DEC);
  switch(setpoint)
  {
  case 0:{lcd.setCursor(6,1);lcd.print("-");break;}
  case 1:{lcd.setCursor(9,1);lcd.print("-");break;}
  case 2:{lcd.setCursor(12,1);lcd.print("-");break;}  
  case 3:{lcd.setCursor(15,1);lcd.print("-");break;}  
  case 4:{lcd.setCursor(18,1);lcd.print("-");break;}   
  case 5:{lcd.setCursor(5,3);lcd.print("-");break;}
  case 6:{lcd.setCursor(17,3);lcd.print("-");break;}   
  default: break;
  }
}


void manudisp()
{
  minute=Clock.getMinute();
  hour=Clock.getHour(h12, PM);
  date=Clock.getDate();
  month=Clock.getMonth(Century);
  year=Clock.getYear();  
  temperature=Clock.getTemperature();
  
  
  lcd.init();                      // initialize the lcd 
 // Print a message to the LCD.
  lcd.backlight();
  lcd.setCursor(0,0);
  lcd.print(20,DEC);
  lcd.print(year,DEC);
  lcd.print("-");
  lcd.print(month,DEC);
  lcd.print("-");
  lcd.print(date,DEC);
  lcd.setCursor(15,0);
  lcd.print(hour/10,DEC);
  lcd.print(hour%10,DEC);
  lcd.print(":");
  lcd.print(minute/10,DEC);
  lcd.print(minute%10,DEC);
  lcd.setCursor(0,1);
  lcd.print("TEMP:");
  lcd.print(temperature,DEC);
  lcd.print("'C");
  lcd.setCursor(0,2);
  lcd.print("-------MANUAL-------");
  lcd.setCursor(0,3);
  lcd.print(">>:");
  lcd.print(timevalue,DEC);
  lcd.print("Min");
  lcd.setCursor(16,3);
  lcd.print("[");
  lcd.print(timecrit,DEC);
  lcd.print("]");
}

void setvalue(byte setyear,byte setmonth,byte setdate,byte sethour,byte setminute,byte sethumicrit,byte setraincrit)
{
  Clock.setYear(setyear);  //Set the year (Last two digits of the year)
  Clock.setMonth(setmonth);  //Set the month of the year
  Clock.setDate(setdate);  //Set the date of the month
  Clock.setHour(sethour);  //Set the hour 
  Clock.setMinute(setminute);//Set the minute 
  humicrit=sethumicrit;
  i2c_eeprom_write_byte(0x57,0,humicrit);
  raincrit=setraincrit;
  i2c_eeprom_write_byte(0x57,1,raincrit);
}

void waterstart()
{
digitalWrite(A0,HIGH);
watering=1;
}

void waterstop()
{
digitalWrite(A0,LOW);
watering=0;
}

void manuwaterdisp()
{if(watering==1)
  {
    if((Clock.getSecond()%2)==0){lcd.setCursor(14,1);lcd.print("-|- |-");shape1=1;}
    else{lcd.setCursor(14,1);lcd.print("-| -|-");shape1=0;}
  }
 else{lcd.setCursor(14,1);lcd.print("-|XX|-");}
}

void autowaterdisp()
{if(watering==1)
  {
    if((Clock.getSecond()%2)==0){lcd.setCursor(14,1);lcd.print("-|- |-");shape1=1;}
    else{lcd.setCursor(14,1);lcd.print("-| -|-");shape1=0;}
    lcd.setCursor(0,1);lcd.print(">>:");lcd.print(wateringtime,DEC);lcd.print("Min...  ");
  }
  else{lcd.setCursor(14,1);lcd.print("-|");lcd.print(interval/10,DEC);lcd.print(interval%10,DEC);lcd.print("|-");}
}

void humiwatering()
{
if(humivalue()<=humicrit){wateringtime=autowatertimecrit;interval=0;i2c_eeprom_write_byte(0x57,2,interval);}               //set interval=0
}

void daychanged()
{
if(Clock.getMinute()==0&&Clock.getHour(h12, PM)==(watertime-1))                              //daychanged is being interval changed
  {
    if(berain<=15){interval=interval+1;}
    if(berain>=60){interval=0;}
    berain=0;
    setwateringvalue();
    i2c_eeprom_write_byte(0x57,2,interval);
  }
}

void setwateringvalue()
{
    month=Clock.getMonth(Century);
    if(month==12||month==2){watertime=16;intervaltest=5;intervalcrit=15;}                              //set watering time in all months
    else if(month==1){watertime=15;intervaltest=5;intervalcrit=25;}
    else if(month>=7&&month<=9){watertime=8;intervaltest=1;intervalcrit=5;}
    else{watertime=8;intervaltest=2;intervalcrit=7;}
}

void raintest()
{
if(rainvalue()>raincrit)berain=berain+1;
while(Clock.getSecond()<1);
}

void autowateringcontrol()
{
if(wateringtime>0){waterstart();wateringtime=wateringtime-1;delay(1000);}
else{waterstop();}
if(wateringtime==1){interval=0;i2c_eeprom_write_byte(0x57,2,interval);}                           //set interval=0
}

void autocritchangeddisp()
{
lcd.setCursor(0,1);
lcd.print(">>:");
lcd.print(autowatertimecrit,DEC);
lcd.print("Min..");
lcd.print("OK!");
}



//AT24C32 HEADFILE
void i2c_eeprom_write_byte( int deviceaddress, unsigned int eeaddress, byte data ) {
    int rdata = data;
    Wire.beginTransmission(deviceaddress);
    Wire.write((int)(eeaddress >> 8)); // MSB
    Wire.write((int)(eeaddress & 0xFF)); // LSB
    Wire.write(rdata);
    Wire.endTransmission();
  }

  // WARNING: address is a page address, 6-bit end will wrap around
  // also, data can be maximum of about 30 bytes, because the Wire library has a buffer of 32 bytes
  void i2c_eeprom_write_page( int deviceaddress, unsigned int eeaddresspage, byte* data, byte length ) {
    Wire.beginTransmission(deviceaddress);
    Wire.write((int)(eeaddresspage >> 8)); // MSB
    Wire.write((int)(eeaddresspage & 0xFF)); // LSB
    byte c;
    for ( c = 0; c < length; c++)
      Wire.write(data[c]);
    Wire.endTransmission();
  }

  byte i2c_eeprom_read_byte( int deviceaddress, unsigned int eeaddress ) {
    byte rdata = 0xFF;
    Wire.beginTransmission(deviceaddress);
    Wire.write((int)(eeaddress >> 8)); // MSB
    Wire.write((int)(eeaddress & 0xFF)); // LSB
    Wire.endTransmission();
    Wire.requestFrom(deviceaddress,1);
    if (Wire.available()) rdata = Wire.read();
    return rdata;
  }

  // maybe let's not read more than 30 or 32 bytes at a time!
  void i2c_eeprom_read_buffer( int deviceaddress, unsigned int eeaddress, byte *buffer, int length ) {
    Wire.beginTransmission(deviceaddress);
    Wire.write((int)(eeaddress >> 8)); // MSB
    Wire.write((int)(eeaddress & 0xFF)); // LSB
    Wire.endTransmission();
    Wire.requestFrom(deviceaddress,length);
    int c = 0;
    for ( c = 0; c < length; c++ )
      if (Wire.available()) buffer[c] = Wire.read();
  }


