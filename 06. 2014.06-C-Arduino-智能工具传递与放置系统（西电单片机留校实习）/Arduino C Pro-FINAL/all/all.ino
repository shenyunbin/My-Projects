#include <EEPROM.h>
#include <IRremote.h>
#include <Wire.h> 
#include <LiquidCrystal_I2C.h>
#include <HX711.h>

//GLOBAL-----------------------------------------------------
int imode;
int g_mode,g_xi,g_yi,g_weightnow,g_weightcount;
int g_numi[]={0,0},g_lcdi;
int g_setangle[]={2050,800,600,500,700},g_movflag;
int g_rtgy_start0=0,g_rtgy_end0=180,g_rtgy_start1=0,g_rtgy_end1=180;
void G_HWX();
void G_CONTROL();
void G_LCD();
//void G_DCMOV();
void G_STMOV();
void LCDinit();
void atstmov(int angl1,int angl2,int angl3,int angl4,int angl5);
void setstmov(int angl1,int angl2,int angl3,int angl4,int angl5);
void atonemov(int movi,int angl);
void setonemov(int movi,int angl);
void positturn(int xi);
void positleft(int yi);
//STMOV--------------------------------------------------------
int servopin[]={8,9,10,11,12};//定义舵机接口数字接口7
const int movspeed=10;
const int pulsesmin[]={2050,800,600,500,700};//定义最小脉宽
const int pulsesmax[]={580,2000,1600,1100,2000};//定义最大脉宽
int pulsesnow[]={2050,800,600,500,700};//定义当前脉宽
int anglemin[] ={0   ,0   ,-45 ,30  ,30  };
int anglemax[] ={180 ,180 ,90  ,180 ,180 };
void movinit();
void servopulse(int i,int ilength);
void stmovcontrol(int angl1,int angl2,int angl3,int angl4,int angl5);
int pulsetoangle(int pulse_temp,int ik);
//CSB---------------------------------------------------------
const int TrigPin = 3; 
const int EchoPin = 4; 
float distance; 
float measure();
//EEPROM------------------------------------------------------
int xtemp;
void writeweight(byte xi ,byte yi,unsigned int iweight16);
unsigned int getweight(byte xi ,byte yi);
void weightdataclr();
byte getposition(unsigned int iiweight16);
byte ifbethere(byte xi,byte yi);
void puttothere(byte xi,byte yi);
void getfromthere(byte xi,byte yi);
//HWX---------------------------------------------------------
int RECV_PIN = 5;//定义红外接收器的引脚为5
unsigned long int rec;  
IRrecv irrecv(RECV_PIN);
decode_results results;
int HWX(unsigned long int k);
//LCD---------------------------------------------------------
int itemp[17];
LiquidCrystal_I2C lcd(0x27,20,4);  // set the LCD address to 0x27 for a 16 chars and 2 line display
void lcd_clrdisp(int line,char *p);
void lcd_disp(int line,char *p);
void lcd_clr(int line);
void lcd_dispint(int line,unsigned long int int16);
//HX711-------------------------------------------------------
HX711 hx(6, 7);//定义 sck、dout 接脚
int hx711_weighting();
//RTGY--------------------------------------------------------
int RTGY_PIN = 2;
byte RTGY();

//Main function///////////////////////////////////////////////
void setup()
{
  pinMode(RTGY_PIN, INPUT);
  pinMode(8,OUTPUT);//设定舵机接口为输出接口
  pinMode(9,OUTPUT);//设定舵机接口为输出接口  
  pinMode(10,OUTPUT);//设定舵机接口为输出接口
  pinMode(11,OUTPUT);//设定舵机接口为输出接口 
  pinMode(12,OUTPUT);//设定舵机接口为输出接口 
  movinit();
  pinMode(TrigPin, OUTPUT); 
  pinMode(EchoPin, INPUT);
  irrecv.enableIRIn(); // 初始化红外接收器
  LCDinit();
}


 void loop()//将0到9的数转化为0到180角度，并让LED闪烁相应数的次数
{
  G_HWX();
  G_CONTROL();
  G_LCD();
  G_STMOV();
  delay(14);
}






//Subs///////////////////////////////////////////////////////
//GLOBAL-----------------------------------------------------
void G_HWX()
{
   if (irrecv.decode(&results))
  { 
    unsigned long int rec;
    int recnum;
    rec=results.value;  
    irrecv.resume(); // 接收下一个值
    recnum=HWX(rec); 
    switch (g_mode) 
    {
//start mode,already for man to control-------  
    case (1):     
      if(0<=recnum&&recnum<10&&g_lcdi<2)
      {
        g_numi[g_lcdi]=recnum;
        g_lcdi+=1;
      }
      if(recnum==100)
      {
        g_numi[g_lcdi]=0;
        g_lcdi-=1;
      }
      if(recnum==200)
      {
        if(g_numi[0]==9&&g_numi[1]==9)
          {
            g_mode=600;
          }
        else if( ifbethere(g_numi[0],g_numi[1]) )
          {
            g_mode=100;g_xi=g_numi[0];g_yi=g_numi[1]; 
          } 
        else 
          {
            g_mode=500;//the thing isn't there
          } 
      }
      break;
//get the things in position (g_xi,g_yi)---------      
    case (100): 
      break; 
//put the things to position (g_xi,g_yi)---------      
    case (200):
      break;
//find the new things,and write the weight to (xi,yi)----------      
    case (300):
      if(0<=recnum&&recnum<10&&g_lcdi<2)
      {
        g_numi[g_lcdi]=recnum;
        g_lcdi+=1;
      }
      if(recnum==100)
      {
        g_numi[g_lcdi]=0;
        g_lcdi-=1;
      }
      if(recnum==200)
      {
        if( !ifbethere(g_numi[0],g_numi[1]) )
        { 
          g_mode=301;
          g_xi=g_numi[0];
          g_yi=g_numi[1]; 
        } 
        else 
        {
          g_mode=503;//the thing is all raedy there
        }
      }
      break;
//the machine is already for man to pick up the things---------      
    case (400):      
      break;
//error mode---------      
    case (500):
      if(recnum==200)
      {
        g_mode=0;
      }
      break;
    case (503):
      if(recnum==200)
      {
        g_mode=300;
      }
//set mode-----------
    case (600):
      break;
    case (601):
      if(recnum==1)//clean all of the data
      {
        weightdataclr();
        g_mode=0;
      }
      else if(recnum==2)//inite the machine
      {
        movinit();
        irrecv.enableIRIn(); // 初始化红外接收器
        g_mode=0;              
      }
      else if(recnum==100)
      {
        g_mode=0;
      }
      break;     
    default:
      break;
    }
   }  
}

void G_CONTROL()
{
   switch (g_mode) 
    {
//start mode,already for man to control---------      
    case (1):
      if(hx711_weighting()>100)
      {
      g_mode=200;
      }    
      break;
//get the things in position (g_xi,g_yi)----------
    case (100): //preparation
      setstmov(90,90,90,90,90);    
      g_mode+=1;
      break; 
    case (101): //turn to the right angle (g_xi) first
      if(g_movflag==1)
      {
        positturn(g_xi); 
        g_mode+=1;
      }
      break; 
    case (102): //prepare for hand to the right posit,aviod things falling down
      if(g_movflag==1)
      {
        atstmov(0,0,0,50,70); 
        g_mode+=1;
      }
      break; 
    case (103)://let the automatic hand to the right posit (g_yi)
      if(g_movflag==1)
      {
        positleft(g_yi);
        g_mode+=1;
      } 
      break; 
    case (104): //put up the things
      if(g_movflag==1)
      {
        atstmov(0,0,0,90,90);
        g_mode+=1;
      } 
      break; 
    case (105)://let the automatic hands in the initial position
      if(g_movflag==1)
      {
        setstmov(90,90,90,90,90);
        g_mode+=1;
      } 
      break;
    case (106)://turn around the hand
      if(g_movflag==1)
      {
        atstmov(180,180,0,0,0);
        g_mode+=1;
      } 
      break; 
    case (107)://look wether a man here
      if(RTGY())
      {
        g_rtgy_start0=pulsesnow[0];
        g_rtgy_start1=pulsesnow[1];        
        g_mode+=1;
      }
      if(g_movflag==1)//if there is no man here,turn to the initial position
      {
        setonemov(0,90);
        setonemov(1,90);
        g_mode+=2;
      } 
      break; 
    case (108): //look where the man is
      if(!RTGY()||g_movflag==1)
      {
        int ia0,ia1;
        g_rtgy_end0=pulsesnow[0];
        g_rtgy_end1=pulsesnow[1];
        ia0=pulsetoangle((g_rtgy_start0+g_rtgy_end0)/2,1);
        ia1=pulsetoangle((g_rtgy_start1+g_rtgy_end1)/2,1);
        setonemov(0,ia0);
        setonemov(1,ia1);
        g_mode+=1;
      } 
      break;       
    case (109)://left the things to man
      if(g_movflag==1)
      {
        setonemov(3,90);
        setonemov(4,90);
        setonemov(5,90);
        g_mode+=1;
      }
      break;
    case (110)://look if bump to sometings
      if(measure()<10)
      {
        int ia[5];
        for(int i=0;i<5;i++)
        {
          ia[i]=pulsetoangle(pulsesnow[i],i);
        }
        setstmov(ia[0],ia[1],ia[2],ia[3],ia[4]);
        g_mode=400;//>>>turn to the already mode 400    
      }
      if(g_movflag==1)
      {      
        g_mode=400;
      }
      break;//>>>turn to the already mode 400
//put the things to position (g_xi,g_yi)---------      
    case (200): //make the hands straigt
      setstmov(90,90,90,90,90);    
      g_mode+=1;
      break; 
    case (201)://weight the things
      if(g_movflag==1)
      {
        g_weightcount+=1;
        if(g_weightcount>20)//set the delay time
        {          
          int i,itemp;
          itemp=hx711_weighting();
          i=getposition(itemp);
          if(i!=0xFF)
          {
            g_xi=i/10;//get the position (g_xi,g_yi)
            g_yi=i%10;
            writeweight(g_xi ,g_yi,itemp);//correct the weight
            g_mode+=1;             
          }
          else
          {
            g_mode=300;//>>>turn to the new things 300         
          }
          g_weightcount=0;
        }        
      }
      break;
    case (202): //turn to the right angle (g_xi)
      if(g_movflag==1)
      {        
        positturn(g_xi); 
        g_mode+=1;
      }
      break; 
    case (203): //prepare for the hands put down
      if(g_movflag==1)
      {
        setstmov(0,0,0,50,70); 
        g_mode+=1;
      }
      break; 
    case (204)://put the automatic hands down
      if(g_movflag==1)
      {
        positleft(g_yi);
        g_mode+=1;
      } 
      break; 
    case (205)://put down the things 
      if(g_movflag==1)
      {
        atstmov(0,0,0,90,90);
        g_mode+=1;
      } 
      break; 
    case (206)://put back the hands
      if(g_movflag==1)
      {
        atstmov(90,90,90,90,90);
        g_mode+=1;
      } 
      break;
    case (207)://turn back the hands
      if(g_movflag==1)
      {
        atstmov(180,180,0,0,0);
        g_mode+=1;
      } 
      break; 
    case (208)://look if there is a man here
      if(RTGY())
      {
        g_rtgy_start0=pulsesnow[0];
        g_rtgy_start1=pulsesnow[1];        
        g_mode+=1;
      }
      if(g_movflag==1)//if there is no man here,turn to the initial position
      {
        setonemov(0,90);
        setonemov(1,90);
        g_mode+=2;
      } 
      break; 
    case (209): //look where the man is 
      if(!RTGY()||g_movflag==1)
      {
        int ia0,ia1;
        g_rtgy_end0=pulsesnow[0];
        g_rtgy_end1=pulsesnow[1];
        ia0=pulsetoangle((g_rtgy_start0+g_rtgy_end0)/2,1);
        ia1=pulsetoangle((g_rtgy_start1+g_rtgy_end1)/2,1);
        setonemov(0,ia0);
        setonemov(1,ia1);
        g_mode+=1;
      } 
      break;       
    case (210): //left the hands to man
      if(g_movflag==1)
      {
        setonemov(3,90);
        setonemov(4,90);
        setonemov(5,90);
        g_mode+=1;
      }
      break;
    case (211)://avoid bumping
      if(measure()<10)
      {
        int ia[5];
        for(int i=0;i<5;i++)
        {
          ia[i]=pulsetoangle(pulsesnow[i],i);
        }
        setstmov(ia[0],ia[1],ia[2],ia[3],ia[4]);
        g_mode=0;//>>>turn to the start mode 0     
      }
      if(g_movflag==1)
      {
        setonemov(3,90);
        setonemov(4,90);
        setonemov(5,90);       
        g_mode=0;//>>>turn to the start mode 0
      }
      break;
//find the new things,and write the weight to (xi,yi)----------
    case (300)://
      break;
    case (301):
      writeweight(g_xi ,g_yi,hx711_weighting());//correct the weight
      g_mode=202;//>>>turn to the puting things mode 202
      break;
//the machine is already for man to pick up the things---------
    case (400)://take the things
      if(g_movflag==1)
      {
        g_weightnow=hx711_weighting();
        g_mode+=1;
      }      
      break;
    case (401):
      if(g_weightnow-hx711_weighting()>100)
      {
        g_weightnow=hx711_weighting();
        g_mode=0;//>>>turn to the start mode 0 
      }      
      break;
      
    default:
      break;
    }  
}

void G_LCD()
{
  switch (g_mode) 
    {
//start mode,already for man to control-------  
    case (0):
      lcd_disp(1,"Slect");
      lcd.setCursor(2,2);
      lcd.print("Delete");
      lcd.setCursor(16,2);
      lcd.print("Go");
      lcd.setCursor(7,3);
      lcd.print("Ready!");
      g_mode+=1;//>>>turn to start mode main
      break;
    case (1):
      lcd.setCursor(16,1);
      lcd.print(g_xi);
      lcd.setCursor(18,1);
      lcd.print(g_yi);      
      break;
//get the things in position (g_xi,g_yi)---------      
    case (100): 
      lcd_disp(1,"Getto");
      lcd.setCursor(2,2);
      lcd.print("------");
      lcd.setCursor(16,2);
      lcd.print("--");
      lcd.setCursor(7,3);
      lcd.print("Busy Geting! ");
      break; 
//put the things to position (g_xi,g_yi)---------      
    case (200):
      lcd_disp(1,"Putto");
      lcd.setCursor(2,2);
      lcd.print("------");
      lcd.setCursor(16,2);
      lcd.print("--");
      lcd.setCursor(7,3);
      lcd.print("Busy Puting! ");
      break;
//find the new things,and write the weight to (xi,yi)----------      
    case (300):
      lcd.setCursor(16,1);
      lcd.print(g_xi);
      lcd.setCursor(18,1);
      lcd.print(g_yi); 
      lcd.setCursor(7,3);
      lcd.print("Add New Thing");
      break;
//the machine is already for man to pick up the things---------      
    case (400):
      lcd.setCursor(7,3);
      lcd.print("Pick Please! ");    
      break;
//error mode---------      
    case (500):
      lcd.setCursor(7,3);
      lcd.print("Error No.0!"); 
      break; 
    case (503):
      lcd.setCursor(7,3);
      lcd.print("Error No.3!"); 
      break;
//set mode--------      
    case (600):
      lcd_disp(1,"SET:  1.clr   2.init");
      lcd.setCursor(2,2);
      lcd.print("BACK  ");
      lcd.setCursor(16,2);
      lcd.print("--");
      lcd.setCursor(7,3);
      lcd.print("Set Mode!    ");
      g_mode+=1;
      break;   
    default:
      break;
    }
}

/*
void G_DCMOV()
{
}
*/

void G_STMOV()
{
  stmovcontrol(g_setangle[0],g_setangle[1],g_setangle[2],g_setangle[3],g_setangle[4]);
}

void atstmov(int angl1,int angl2,int angl3,int angl4,int angl5)
{
  g_setangle[0]+=angl1;
  g_setangle[1]+=angl2;
  g_setangle[2]+=angl3;
  g_setangle[3]+=angl4;
  g_setangle[4]+=angl5;
}

//--------------------------------------------------------
void LCDinit()
{
  lcd_disp(0,"-----BENZ ROBOT-----");
  lcd_disp(1,"SlectPosition: [-,-]");
  lcd_disp(2,"<<Delete        Go>>");
  lcd_disp(3,"State: Ready!       ");
}

void setstmov(int angl1,int angl2,int angl3,int angl4,int angl5)
{
  g_setangle[0]=angl1;
  g_setangle[1]=angl2;
  g_setangle[2]=angl3;
  g_setangle[3]=angl4;
  g_setangle[4]=angl5;
}

void atonemov(int movi,int angl)
{
  g_setangle[movi]+=angl;
}

void setonemov(int movi,int angl)
{
  g_setangle[movi]=angl;
}

void positturn(int xi)
{
  switch (xi) 
    {
    case (0):
      setonemov(0,90);
      setonemov(1,90);
      break;
    case (1):
      setonemov(0,90);
      setonemov(1,90);
      break;
    case (2):
      setonemov(0,90);
      setonemov(1,90);
      break;
    case (3):
      setonemov(0,90);
      setonemov(1,90);
      break;
    default:
      break;
    }    
}

void positleft(int yi)
{
  switch (yi) 
    {
    case (0):
      setonemov(2,90);
      setonemov(3,90);
      setonemov(4,90);
      break;
    case (1):
      setonemov(2,90);
      setonemov(3,90);
      setonemov(4,90);
      break;
    case (2):
      setonemov(2,90);
      setonemov(3,90);
      setonemov(4,90);
      break;
    case (3):
      setonemov(2,90);
      setonemov(3,90);
      setonemov(4,90);
      break;
    default:
      break;
    }  
}

//STMOV--------------------------------------------------------
void movinit()
{
  for(int i=2;i<5;i++)
  {
    for(int j=0;j<50;j++)
    {switch(i)
     {case 4:
      digitalWrite(servopin[4],HIGH);//将舵机接口电平至高
      delayMicroseconds(pulsesnow[4]);//延时脉宽值的微秒数
      digitalWrite(servopin[4],LOW);//将舵机接口电平至低
      case 3:     
      digitalWrite(servopin[3],HIGH);//将舵机接口电平至高
      delayMicroseconds(pulsesnow[3]);//延时脉宽值的微秒数
      digitalWrite(servopin[3],LOW);//将舵机接口电平至低
      case 2:
      digitalWrite(servopin[2],HIGH);//将舵机接口电平至高
      delayMicroseconds(pulsesnow[2]);//延时脉宽值的微秒数
      digitalWrite(servopin[2],LOW);//将舵机接口电平至低
      default:
      delay(50);
     }
    }
  }
  for(int i=0;i<2;i++)
  {
    for(int j=0;j<50;j++)
    {
      switch(i)
     {case 1:
      digitalWrite(servopin[1],HIGH);//将舵机接口电平至高
      delayMicroseconds(pulsesnow[1]);//延时脉宽值的微秒数
      digitalWrite(servopin[1],LOW);//将舵机接口电平至低
      case 0:
      digitalWrite(servopin[0],HIGH);//将舵机接口电平至高
      delayMicroseconds(pulsesnow[0]);//延时脉宽值的微秒数
      digitalWrite(servopin[0],LOW);//将舵机接口电平至低
       case 4:
      digitalWrite(servopin[2],HIGH);//将舵机接口电平至高
      delayMicroseconds(pulsesnow[2]);//延时脉宽值的微秒数
      digitalWrite(servopin[2],LOW);//将舵机接口电平至低
      case 3:     
      digitalWrite(servopin[3],HIGH);//将舵机接口电平至高
      delayMicroseconds(pulsesnow[3]);//延时脉宽值的微秒数
      digitalWrite(servopin[3],LOW);//将舵机接口电平至低
      case 2:
      digitalWrite(servopin[4],HIGH);//将舵机接口电平至高
      delayMicroseconds(pulsesnow[4]);//延时脉宽值的微秒数
      digitalWrite(servopin[4],LOW);//将舵机接口电平至低
      default:
      delay(50);
     }
    }
  }
}

void servopulse(int i,int ilength)
{
  if(ilength>pulsesnow[i]+movspeed){pulsesnow[i]+=movspeed;g_movflag=0;}
  else if(ilength<pulsesnow[i]-movspeed){pulsesnow[i]-=movspeed;g_movflag=0;}
  else{pulsesnow[i]=ilength;}
  digitalWrite(servopin[i],HIGH);//将舵机接口电平至高
  delayMicroseconds(pulsesnow[i]);//延时脉宽值的微秒数
  digitalWrite(servopin[i],LOW);//将舵机接口电平至低
}

void stmovcontrol(int angl1,int angl2,int angl3,int angl4,int angl5)//定义一个脉冲函数
{
  int ipulse[5],iangle[]={angl1,angl2,angl3,angl4,angl5};
  g_movflag=1;
  for(int i=0;i<5;i++)
  {
    if(iangle[i]>anglemax[i]){iangle[i]=anglemax[i];}
    if(iangle[i]<anglemin[i]){iangle[i]=anglemin[i];}
    ipulse[i]=(iangle[i]-anglemin[i])/(anglemax[i]-anglemin[i])*(pulsesmax[i]-pulsesmin[i])+pulsesmin[i];
    servopulse(i,ipulse[i]);
  }
}

int pulsetoangle(int pulse_temp,int ik)
{
    if(pulse_temp>pulsesmax[ik]){return anglemax[ik];}
    else if(pulse_temp<pulsesmin[ik]){return anglemin[ik];}
    else {return (pulse_temp-pulsesmin[ik])/(pulsesmax[ik]-pulsesmin[ik])*(anglemax[ik]-anglemin[ik])+anglemin[ik];}
}

//CSB---------------------------------------------------------
float measure()
{
    float distance;
    digitalWrite(TrigPin, LOW); 
    delayMicroseconds(2); 
    digitalWrite(TrigPin, HIGH); 
    delayMicroseconds(10);
    digitalWrite(TrigPin, LOW); 
    // 检测脉冲宽度，并计算出距离
    distance = pulseIn(EchoPin, HIGH,50000) / 58.00;   

  return distance;
}

//EEPROM------------------------------------------------------
void writeweight(byte xi ,byte yi,unsigned int iweight16)
{
  if(xi<10&&yi<10)
  {
     EEPROM.write(xi*20+2*yi, iweight16>>8);
     EEPROM.write(xi*20+2*yi+1, iweight16);
  }  
}

unsigned int getweight(byte xi ,byte yi)
{
  unsigned int iread;
  if(xi<10&&yi<10)
  {
     iread=(EEPROM.read(xi*20+2*yi)<<8)+EEPROM.read(xi*20+2*yi+1);
     return iread;
  }
}

void weightdataclr()
{
  for (int iclr = 0; iclr < 300; iclr++)EEPROM.write(iclr, 0);
}

byte getposition(unsigned int iiweight16)
{
  unsigned int xi,yi,itemp,difmin=100;
  for(int i=0;i<10;i++)
  {
    for(int j=0;j<10;j++)
    {
      itemp=abs(iiweight16-getweight(i,j));
      if(itemp<difmin){difmin=itemp;xi=i,yi=j;}
    }
  }
  if(difmin<100){return xi*10+yi;}
  else{return 0xFF;}
}

byte ifbethere(byte xi,byte yi)
{
  return EEPROM.read(xi*10+yi+200);
}

void puttothere(byte xi,byte yi)
{
  EEPROM.write(xi*10+yi+200,1);
}

void getfromthere(byte xi,byte yi)
{
  EEPROM.write(xi*10+yi+200,0);
}

//HWX---------------------------------------------------------
int HWX(unsigned long int k)
  {
         switch (k) {
         case (16738455):             //0
         { return 0;
           break;
         }
         case (16724175):             //1
         {  return 1;
            break;
         }
         case (16718055):             //2
         {  return 2;
            break;
         }
         case (16743045):             //3
         {  return 3;
            break;
         }
         case (16716015):             //4
         {  return 4;
            break;
         }
         case (16726215):             //5
         {  return 5;
            break;
         }
         case (16734885):             //6
         {  return 6;
            break;
         }
         case (16728765):             //7
         {  return 7;
            break;
         }
         case (16730805):             //8
         {  return 8;
            break;
         }
         case (16732845):             //9
         {  return 9;
            break;
         }
         case (16720605):            //left
         {  return 100;
            break;
         }
         case (16712445):            //right
         {  return 200;
            break;
         }
         default: return 320;
     }
      //irrecv.resume(); // 接收下一个值
   }

//LCD---------------------------------------------------------
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

//HX711-----------------------------------------------------
int hx711_weighting()
{
  double sum = 0;
  int iwe;
  for (int i = 0; i < 5; i++)
    sum += hx.read();
  iwe=sum/10000;
  return iwe;
}

//RTGY--------------------------------------------------------
byte RTGY()
{
return digitalRead(RTGY_PIN);
}

