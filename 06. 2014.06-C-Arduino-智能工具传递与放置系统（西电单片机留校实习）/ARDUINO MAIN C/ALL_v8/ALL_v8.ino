#include <MsTimer2.h>
#include <EEPROM.h>
#include <IRremote.h>
#include <Wire.h> 
#include <LiquidCrystal_I2C.h>
#include <HX711.h>

//GLOBAL-全局子程序和变量-----------------------------------------------------
int t1_count=1000;//用于延时计时的定时计数器变量
int yi_mode=0,g_pause=0;//;yi_mode:机械手举起和放下的步骤;g_pause:停止标志位
int mode700_movi,mode700_angl;//手动调节模式(g_mode=700+)的舵机控制变量--mode700_movi:舵机号;mode700_angl:舵机脉冲长度;
int g_mode=0,g_xi,g_yi,g_weightnow,g_weightcount;//g_mode:当前运行步骤(以100为一个大步骤，后面为模式中的详细步骤);g_xi/g_yi:当前取放物品的位置号;g_weightnow:当前物品重量;
int g_numi[]={-1,-1},g_lcdi;//lcd当前取放物品的位置号显示辅助变量
int g_setangle[]={155,18,10,0,0},g_movflag;//++++++++++++++++++++++当前设置的舵机脉冲长度(进过转换后的)+++++++++++++++++++++++++++
int g_rtgy_start0=0,g_rtgy_end0=180,g_rtgy_start1=0,g_rtgy_end1=180;//机器人检测人体方位的辅助变量
const int g_res=350;//称重分辨率
void G_COM();//串口命令接收判断程序
void G_CONTROL();//舵机控制总程序(包括物品称重识别\人体感应\超声波测距)
void G_LCD();//LCD显示程序
//void G_DCMOV();//电机控制程序(预留)
void G_STMOV();//舵机脉冲周期性发送程序,用于维持舵机运转
void G_PAUSE();//令舵机动作暂停
void LCDinit();//LCD显示初始化

//GLOBAL_MOV_CONTROL-全局舵机控制子程序-----------------------------------------
const int Ylift[3][8][3]={                          {    { 0,0 , 0},{ 0,0 ,0 },{ 0,0 ,0 },{0 , 0,0 },{ 0,0 ,0 },{ 0, 0,0 },{ 0,0 ,0 },{ 0, 0, 0}    },                          {    { 0,0 , 0},{ 0,0 ,0 },{ 0,0 ,0 },{0 , 0,0 },{ 0,0 ,0 },{ 0, 0,0 },{ 0,0 ,0 },{ 0, 0, 0}    },                          {    { 0,0 , 0},{ 0,0 ,0 },{ 0,0 ,0 },{0 , 0,0 },{ 0,0 ,0 },{ 0, 0,0 },{ 0,0 ,0 },{ 0, 0, 0}    },                        };
       
void atstmov(int angl1,int angl2,int angl3,int angl4,int angl5);//改变各个舵机脉冲长度,正数为增加,负数为减少
void setstmov(int angl1,int angl2,int angl3,int angl4,int angl5);//设置各个舵机脉冲长度
void set234mov(int angl3,int angl4,int angl5);//设置2,3,4号舵机(最上面的3个舵机)的脉冲长度
void atonemov(int _movi,int _angl);//改变单个舵机的脉冲长度(movi:舵机号/angl:改变脉冲长度的值)
void setonemov(int movi,int angl);//设置单个舵机的脉冲长度(movi:舵机号/angl:脉冲长度)
void positturn(int xi);//机器人转到横坐标为xi的物品的程序
void positlift(int yi);//机器人拿起纵坐标为yi的物品程序
void Y0_lift();
void Y1_lift();
void Y2_lift();
void positdown(int yi);//机器人放到纵坐标为yi的物架程序
void Y0_down();
void Y1_down();
void Y2_down();

//STMOV-底层舵机控制子程序--------------------------------------------------------
int servopin[]={8,9,10,11,12};//定义舵机接口数字接口
const int movspeed=3;
const int pulsesmin[]={800,600,600,500,700};//定义各个舵机实际最小脉宽
const int pulsesmax[]={2000,2000,1500,1600,2000};//定义各个舵机实际最大脉宽
int pulsesnow[]={2050,800,600,500,700};//定义当前各个舵机实际脉宽,单位为微妙
void movinit();//舵机初始化程序
void servopulse(int i,int ilength);//脉冲-引脚输出子程序
void stmovcontrol(float angl11,float angl12,float angl13,float angl14,float angl15);//依次输出各个舵机的脉冲信号长度
int pulsetoangle(int pulse_temp,int ik);//实际脉冲信号长度和程序中转化后的脉冲长度的转换程序

//CSB-超声波测距子程序---------------------------------------------------------
const int TrigPin = 4; //定义超声波传感器接口
const int EchoPin = 5; 
float distance; 
float measure();

//EEPROM-储存器储存读取、重量位置判断子程序------------------------------------------------------
int xtemp;//暂存变量
void writeweight(byte xi ,byte yi,unsigned int iweight16);//记录xi,yi位置物品的重量
unsigned int getweight(byte xi ,byte yi);//获取xi,yi位置物品的重量
void weightdataclr();//清空储存器
byte getposition(unsigned int iiweight16);//获取当前重量的物品的位置xi,yi
byte ifbethere(byte xi,byte yi);//获取当前位置xi,yi是否已经有物品存在
void puttothere(byte xi,byte yi);//将ifbewhere(xi,yi)置1
void getfromthere(byte xi,byte yi);//将ifbewhere(xi,yi)置0

//HWX-红外接收转码子程序---------------------------------------------------------
int RECV_PIN = 2;//定义红外接收器的引脚为2
unsigned long int rec;//暂存红外接收数据  
IRrecv irrecv(RECV_PIN);//定义红外接收引脚
decode_results results;
int HWX(unsigned long int k);//红外转换程序

//LCD-LCD显示子程序---------------------------------------------------------
int itemp[17];
LiquidCrystal_I2C lcd(0x27,20,4);  // set the LCD address to 0x27 for a 16 chars and 2 line display
void lcd_clrdisp(int line,char *p);//第line行先清除后显示数据
void lcd_disp(int line,char *p);//在第line行显示数据
void lcd_clr(int line);//清除第line行的显示数据
void lcd_dispint(int line,unsigned long int int16);//在显示器上显示数据

//HX711-HX711称重子程序-------------------------------------------------------
HX711 hx(6, 7);//定义 sck、dout 接脚
int hx711_weighting();//获取当前物品重量

//RTGY-人体探测子程序--------------------------------------------------------
int RTGY_PIN = 3;//设置人体感应模块引脚
byte RTGY();//获取当前是否有人存在


//Main function////////////////////////////////////////////////////////////////////////////////////////////////
void flash()//每20ms执行一次此函数
{
  if(g_pause) 
  {
    G_PAUSE();//舵机暂停处理程序
  } 
  else
  {
    G_STMOV();//舵机脉冲发送程序
  }
  if(t1_count<1000){t1_count+=20;}//timer1,定时计数器1（每20ms数值+20）
}

void setup()//硬件端口初始化
{
  pinMode(RTGY_PIN, INPUT);
  pinMode(servopin[0],OUTPUT);//设定舵机接口为输出接口
  pinMode(servopin[1],OUTPUT); 
  pinMode(servopin[2],OUTPUT);
  pinMode(servopin[3],OUTPUT); 
  pinMode(servopin[4],OUTPUT);
  pinMode(TrigPin, OUTPUT); 
  pinMode(EchoPin, INPUT);
  irrecv.enableIRIn(); // 初始化红外接收器
  lcd.init();//显示屏初始化
  lcd.backlight();//显示屏led灯打开
  LCDinit();//显示显示初始数据
  Serial.begin(9600);//打开串口
  MsTimer2::set(20, flash); // 20ms period
  MsTimer2::start();//定时中断开启
}

 void loop()//主函数
{
  movinit();//舵机初始化
  while(1)
  {
    G_COM();//串口命令接收判断程序
    G_CONTROL();//总控制程序
    G_LCD();//LCD显示程序
  }
}



//Subs//////////////////////////////////////////////////////////////////////////////////////////////////////////
//GLOBAL--------------------------------------------------------------------------------------------------------
//comunicaton sub----------------------------------------------------
void G_COM()//串口命令接收判断程序
{
    if (Serial.available())
  { 
    byte recnum;
    recnum=Serial.read()-'0'; 
    if(recnum==('P'-'0'))g_pause=!g_pause;
    switch (g_mode) 
    {
//start mode,already for man to control-------  
    case (3):     
      if(0<=recnum&&recnum<10&&g_lcdi<2)
      {
        g_numi[g_lcdi]=recnum;
        g_lcdi+=1;
      }
      if(recnum==('B'-'0')&&g_lcdi>0)
      {
        g_lcdi-=1;
        g_numi[g_lcdi]=-1;        
      }
      if(recnum==('G'-'0'))
      {
        if(g_numi[0]==9&&g_numi[1]==9)
          {
            g_mode=600;
          }
        else if(g_numi[0]==8&&g_numi[1]==8)
          {
             g_mode=700;
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
      if(recnum==('B'-'0'))
      {
        g_lcdi-=1;
        g_numi[g_lcdi]=-1;
      }
      if(recnum==('G'-'0')&&g_lcdi==2)
      {
        // !ifbethere(g_numi[0],g_numi[1]) ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        if(1)
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
    case (401): 
      if(recnum==('G'-'0'))
      {
        g_mode=0;
      }    
      break;
//error mode---------      
    case (500):
      if(recnum==('G'-'0'))
      {
        g_mode=0;
      }
      break;
    case (503):
      if(recnum==('G'-'0'))
      {
        g_mode=300;
      }
      case (504):
      if(recnum==('G'-'0'))
      {
        g_mode=0;
      }
//set mode-----------
    case (600):
      break;
    case (601):
      if(recnum==1)//clean all of the data
      {
        weightdataclr();
        LCDinit();
        g_mode=0;
      }
      else if(recnum==2)//inite the machine
      {
        movinit();
        irrecv.enableIRIn(); // 初始化红外接收器
        LCDinit();
        g_mode=0;              
      }
      else if(recnum=='B'-'0')
      {
        LCDinit();
        g_mode=0;
      }
      break; 
    case (700):
      break;
    case (701):
      switch(recnum)
      {
        case (0):
          mode700_movi=0;
          mode700_angl=-1;
          atonemov(mode700_movi,mode700_angl);
          break;
        case (1):
          mode700_movi=0;
          mode700_angl=1;
          atonemov(mode700_movi,mode700_angl);
          break;
        case (2):
          mode700_movi=1;
          mode700_angl=-1;
          atonemov(mode700_movi,mode700_angl);
          break;
        case (3):
          mode700_movi=1;
          mode700_angl=1;
          atonemov(mode700_movi,mode700_angl);
          break;
        case (4):
          mode700_movi=2;
          mode700_angl=-1;
          atonemov(mode700_movi,mode700_angl);
          break;
        case (5):
          mode700_movi=2;
          mode700_angl=1;
          atonemov(mode700_movi,mode700_angl);
          break;
        case (6):
          mode700_movi=3;
          mode700_angl=-1;
          atonemov(mode700_movi,mode700_angl);
          break;
        case (7):
          mode700_movi=3;
          mode700_angl=1;
          atonemov(mode700_movi,mode700_angl);
          break;
        case (8):
          mode700_movi=4;
          mode700_angl=-1;
          atonemov(mode700_movi,mode700_angl);
          break;
        case (9):
          mode700_movi=4;
          mode700_angl=1;
          atonemov(mode700_movi,mode700_angl);
          break;
        case ('E'-'0'):
          atonemov(mode700_movi,mode700_angl);
          break;
        case ('B'-'0'):
          LCDinit();
          g_mode=0;
          break;
        default:
          break;          
      }
      break;    
    default:
      break;
    }
   }  
}

//main control sub----------------------------------------------------------
void G_CONTROL()//舵机控制总程序(包括物品称重识别\人体感应\超声波测距)
{
   switch (g_mode) 
    {
//start mode,already for man to control---------      
    case (0):
      setstmov(155,18,10,30,20);
      if(t1_count>980)t1_count=0;//delay 500ms
      if(t1_count>500)
      {
        t1_count=1000;
        g_mode+=1;
      }   
      break;
    case (1):
      if(g_movflag==1)
      {
        g_movflag=0;
        if(t1_count>980)t1_count=0;//delay 200ms
        if(t1_count>200)
        {
          t1_count=1000;
          g_weightnow=hx711_weighting();
          g_mode+=1;
        }
      } 
      break;
    case (3):
      if(g_movflag==1)
      {
        g_movflag=0;
        if(abs(g_weightnow-hx711_weighting())>g_res)
        {
          g_weightnow=hx711_weighting();
          g_mode=200;//>>>turn to the start mode 200 +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        }    
      }
      break;
//get the things in position (g_xi,g_yi)----------
    case (100): //preparation,straight the hand and put it to the highest
      setstmov(155,18,10,0,0); //一柱擎天   
      g_mode+=1;
      break; 
    case (101): //turn to the right angle (g_xi) first
      if(g_movflag==1)
      {
        g_movflag=0; 
        positturn(g_xi);        
        g_mode+=1;
      }
      break; 
    case (102): //prepare for hand to the right posit,aviod things falling down
      if(g_movflag==1)
      {
        set234mov(10,100,150); //低头
        g_movflag=0;        
        g_mode+=1;
      }
      break; 
    case (103)://let the automatic hand to the right posit (g_yi) 
        positlift(g_yi);
        if(yi_mode==100)
        {
          yi_mode=0;
          //getfromthere(g_xi,g_yi);//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
          g_mode+=1;
        }
      break; 
    case (104): //put up the things
      if(g_movflag==1)
      {
        g_movflag=0;
        set234mov(10,100,150); //低头
        g_mode+=1;
      } 
      break; 
    case (105)://let the automatic hands in the initial position
      if(g_movflag==1)
      {
        g_movflag=0;
        set234mov(10,0,0); //一柱擎天
        g_mode+=1;
      } 
      break;
    case (106)://turn around the hand
      if(g_movflag==1)
      {
        g_movflag=0; 
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
        g_movflag=0;
        setonemov(0,155);
        setonemov(1,18);
        g_mode+=2;
      } 
      break; 
    case (108): //look where the man is
      if(!RTGY()||g_movflag==1)
      {
        g_movflag=0;
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
        g_movflag=0;
        setonemov(2,10);
        setonemov(3,40);
        setonemov(4,0);
        g_mode+=1;
      }
      break;
    case (110)://look if bump to sometings
      if(measure()<10&&measure()>1)
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
        g_movflag=0;
        g_mode=400;
      }
      break;//>>>turn to the already mode 400
//put the things to position (g_xi,g_yi)---------      
    case (200): //make the hands straigt
      setstmov(155,18,10,0,0);//一柱擎天          
      g_mode+=1;
      break; 
    case (201)://weight the things
      if(g_movflag==1)
      {        
        g_movflag=0;
        if(t1_count>980)t1_count=0;
        if(t1_count>200)//set the delay time 200ms
        {
          t1_count=1000;
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
        }    
      }
      break;
    case (202): //turn to the right angle (g_xi)
      if(g_movflag==1)
      {
        g_movflag=0;
        positturn(g_xi); 
        g_mode+=1;
      }
      break; 
    case (203): //prepare for the hands put down
      if(g_movflag==1)
      {
        g_movflag=0;
        set234mov(10,100,150); //低头
        yi_mode=0;
        g_mode+=1;
      }
      break; 
    case (204)://put the automatic hands down
      positdown(g_yi);
      if(yi_mode==100)
      {
        yi_mode=0;
        //puttothere(g_xi,g_yi);//!!!!!!!!!!!!!!!!!!!!!!!!!!
        g_mode+=1;
      }
      break; 
    case (205)://put down the things 
      if(g_movflag==1)
      {
        g_movflag=0;
        set234mov(10,100,150); //低头
        g_mode+=1;
      } 
      break; 
    case (206)://put back the hands
      if(g_movflag==1)
      {
        g_movflag=0;
        set234mov(10,0,0); //一柱擎天
        g_mode+=1;
      } 
      break;
    case (207)://turn back the hands
      if(g_movflag==1)
      {
        g_movflag=0;
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
        g_movflag=0;
        setonemov(0,155);
        setonemov(1,18);
        g_mode+=2;
      } 
      break; 
    case (209): //look where the man is 
      if(!RTGY()||g_movflag==1)
      {
        g_movflag=0;
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
        g_movflag=0;
        setonemov(2,10);
        setonemov(3,40);
        setonemov(4,0);
        g_mode+=1;
      }
      break;
    case (211)://avoid bumping
      if(measure()<10&&measure()>1)
      {
        int ia[5];
        for(int i=0;i<5;i++)
        {
          ia[i]=pulsetoangle(pulsesnow[i],i);
        }
        setstmov(ia[0],ia[1],ia[2],ia[3],ia[4]);
        g_mode=0;//>>>turn to the already mode 400    
      }      
      if(g_movflag==1)
      {
        g_movflag=0;
        g_mode=0;
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
        g_movflag=0;
        delay(200);
        g_weightnow=hx711_weighting();
        g_mode+=1;
      }      
      break;
    case (401):
      if(abs(g_weightnow-hx711_weighting())>g_res)
      {
        if(t1_count>980)t1_count=0;
        if(t1_count>300)
        {
          t1_count=1000;
          g_weightnow=hx711_weighting();
          g_mode=0;//>>>turn to the start mode 0 
        }         
      }      
      break;
      
    default:
      break;
    }  
}

//lcd disp sub-----------------------------------------------------------
void G_LCD()//LCD显示程序
{
  switch (g_mode) 
    {
//start mode,already for man to control-------  
    case (2):
      lcd_disp(1,"Slect");
      lcd.setCursor(2,2);
      lcd.print("Delete");
      lcd.setCursor(16,2);
      lcd.print("Go");
      lcd.setCursor(7,3);
      lcd.print("Ready!       ");
      g_mode+=1;//>>>turn to start mode main
      break;
    case (3):
      lcd.setCursor(16,1);      
      if(g_numi[0]>=0)
      {
        lcd.print(g_numi[0]);
      }
      else
      {
        lcd.print("-");      
      }
      lcd.setCursor(18,1);
      if(g_numi[1]>=0)
      {
        lcd.print(g_numi[1]);
      }
      else
      {
        lcd.print("-");      
      }     
      break;
//get the things in position (g_xi,g_yi)---------      
    case (101): 
      lcd_disp(1,"Getto");
      lcd.setCursor(2,2);
      lcd.print("------");
      lcd.setCursor(16,2);
      lcd.print("--");
      lcd.setCursor(7,3);
      lcd.print("Busy Geting! ");
      
      break; 
//put the things to position (g_xi,g_yi)---------      
    case (201):
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
      if(g_numi[0]>=0)
      {
        lcd.print(g_numi[0]);
      }
      else
      {
        lcd.print("-");      
      }
      lcd.setCursor(18,1);
      if(g_numi[1]>=0)
      {
        lcd.print(g_numi[1]);
      }
      else
      {
        lcd.print("-");      
      } 
      lcd.setCursor(2,2);
      lcd.print("Delete");
      lcd.setCursor(16,2);
      lcd.print("Go"); 
      lcd.setCursor(7,3);
      lcd.print("Add New Thing");
      break;
//the machine is already for man to pick up the things---------      
    case (400):
      lcd.setCursor(2,2);
      lcd.print("------");
      lcd.setCursor(16,2);
      lcd.print("Ok");
      lcd.setCursor(7,3);
      lcd.print("Pick Please! ");    
      break;
//error mode---------      
    case (500):
      lcd.setCursor(7,3);
      lcd.print("Error No.0!  "); 
      break; 
    case (503):
      lcd.setCursor(7,3);
      lcd.print("Error No.3!  "); 
      break;
    case (504):
      lcd.setCursor(7,3);
      lcd.print("Error No.4!  "); 
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
    case (700):
      lcd_disp(1,"U-1- -3- -5- -7- -9-");
      lcd_disp(2,"N--- --- --- --- ---");
      lcd_disp(3,"D-0- -2- -4- -6- -8-");
      g_mode=701;
      break;
    case (701):
      lcd.setCursor(1,2);
      lcd.print(g_setangle[0]);
	  lcd.print(" ");
      lcd.setCursor(5,2);
      lcd.print(g_setangle[1]);
	  lcd.print(" ");
      lcd.setCursor(9,2);
      lcd.print(g_setangle[2]);
	  lcd.print(" ");
      lcd.setCursor(13,2);
      lcd.print(g_setangle[3]);
	  lcd.print(" ");
      lcd.setCursor(17,2);
      lcd.print(g_setangle[4]);
	  lcd.print(" ");
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

void G_STMOV()//舵机脉冲周期性发送程序,用于维持舵机运转
{
  stmovcontrol(g_setangle[0],g_setangle[1],g_setangle[2],g_setangle[3],g_setangle[4]);
}

void atstmov(int angl1,int angl2,int angl3,int angl4,int angl5)//改变各个个舵机的脉冲长度(angl:改变脉冲长度的值)
{
  g_setangle[0]+=angl1;
  g_setangle[1]+=angl2;
  g_setangle[2]+=angl3;
  g_setangle[3]+=angl4;
  g_setangle[4]+=angl5;
}

void G_PAUSE()//舵机暂停服务程序
{
    int ipauses[5];
    for(int i=0;i<5;i++)ipauses[i]=pulsesnow[i];
    G_STMOV(); 
    for(int i=0;i<5;i++)pulsesnow[i]=ipauses[i];
}

//--------------------------------------------------------
void LCDinit()//LCD初始化显示程序
{
  lcd_disp(0,"------NX ROBOT------");
  lcd_disp(1,"SlectPosition: [-,-]");
  lcd_disp(2,"<<Delete        Go>>");
  lcd_disp(3,"State: Ready!       ");
}

//GLOBAL_MOV_CONTROL-----------------------------------------------------------------------------------------------
void setstmov(int angl1,int angl2,int angl3,int angl4,int angl5)//设置各个舵机脉冲长度
{
  g_setangle[0]=angl1;
  g_setangle[1]=angl2;
  g_setangle[2]=angl3;
  g_setangle[3]=angl4;
  g_setangle[4]=angl5;
}

void set234mov(int angl3,int angl4,int angl5)//设置2,3,4号舵机(最上面的3个舵机)的脉冲长度
{
  g_setangle[2]=angl3;
  g_setangle[3]=angl4;
  g_setangle[4]=angl5;
}

void atonemov(int _movi,int _angl)//改变单个舵机的脉冲长度(movi:舵机号/angl:改变脉冲长度的值)
{
  g_setangle[_movi]=_angl+g_setangle[_movi];
}

void setonemov(int movi,int angl)//设置单个舵机的脉冲长度(movi:舵机号/angl:脉冲长度)
{
  g_setangle[movi]=angl;
}

void positturn(int xi)//机器人转到横坐标为xi的物品的程序
{
  switch (xi) 
    {
    case (0):
      setonemov(0,30);
      setonemov(1,18);
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

void positlift(int yi)//机器人拿起纵坐标为yi的物品程序
{
  switch (yi) 
    {
    case (0):
        Y0_lift();
      break;
    case (1): 
        Y1_lift();
      break;
    case (2): 
        Y2_lift();
      break;
    default:
      break;
    }  
}

void Y0_lift()
{
    switch (yi_mode) 
    {
    case (0):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(90,100,150);
        yi_mode+=1;
      }
      break;
    case (1):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(90,100,100);
        yi_mode+=1;
      }
      break;
    case (2):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(90,65,100);
        yi_mode+=1;
      }
      break;
    case (3):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(90,65,130);
        yi_mode+=1;
      }
      break;
    case (4):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(90,100,150);
        yi_mode=100;
      }
      break;
    default:
      break;
    }  

}

void Y1_lift()
{
    switch (yi_mode) 
    {
    case (0):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(56,100,150);
        yi_mode+=1;
      }
      break;
    case (1):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(52,110,150);
        yi_mode+=1;
      }
      break;
    case (2):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(52,110,90);
        yi_mode+=1;
      }
      break;
    case (3):
      if(g_movflag) 
      { 
        set234mov(52,83,90);
        yi_mode+=1;
      }
      break;
    case (4):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(56,88,150);
        yi_mode+=1;
      }
      break;
    case (5):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(56,100,150);
        yi_mode+=1;
      }
      break;
    case (6):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(10,100,150);
        yi_mode=100;
      }
      break;
    default:
        g_mode=504;
      break;
    }  

}

void Y2_lift()
{
    switch (yi_mode) 
    {
    case (0):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(10,100,20);
        yi_mode+=1;
      }
      break;
    case (1):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(10,63,20);
        yi_mode+=1;
      }
      break;
    case (2):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(25,63,20);
        yi_mode+=1;
      }
      break;
    case (3):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(25,63,125);
        yi_mode+=1;
      }
      break;
    case (4):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(10,100,125);
        yi_mode=100;
      }
      break;
    case (5):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(10,100,150);
        yi_mode+=1;
      }
      break;
    default:
        g_mode=504;
      break;
    }  
}

void positdown(int yi)//机器人放到纵坐标为yi的物架程序
{
  switch (yi) 
    {
    case (0):
        Y0_down();
      break;
    case (1):
        Y1_down();
      break;
    case (2):
        Y2_down();
      break;
    default:
      break;
    }  
}

void Y0_down()
{
    switch (yi_mode) 
    {
    case (0):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(60,100,150);
        yi_mode+=1;
      }
      break;
    case (1):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(60,90,150);
        yi_mode+=1;
      }
      break;
    case (2):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(60,90,106);
        yi_mode+=1;
      }
      break;
    case (3):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(60,72,106);
        yi_mode+=1;
      }
      break;
    case (4):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(60,72,50);
        yi_mode+=1;
      }
      break;
    case (5):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(60,100,50);
        yi_mode+=1;
      }
      break;
    case (6):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(60,100,150);
        yi_mode+=1;
      }
      break;
    case (7):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(10,100,150);
        yi_mode=100;
      }
      break;
    default:
        g_mode=504;
      break;
    }  

}

void Y1_down()
{
  yi_mode=100;
}

void Y2_down()
{
    switch (yi_mode) 
    {
    case (0):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(10,100,70);
        yi_mode+=1;
      }
      break;
    case (1):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(10,35,70);
        yi_mode+=1;
      }
      break;
    case (2):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(52,35,70);
        yi_mode+=1;
      }
      break;
    case (3):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(52,35,105);
        yi_mode+=1;
      }
      break;
    case (4):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(52,97,105);
        yi_mode+=1;
      }
      break;
    case (5):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(52,100,105);
        yi_mode+=1;
      }
      break;
    case (6):
      if(g_movflag) 
      { 
        g_movflag=0;  
        set234mov(10,100,150);
        yi_mode=100;
      }
      break;
    default:
        g_mode=504;
      break;
    }  

}

//STMOV------------------------------------------------------------------------------------------------------
void movinit()//舵机位置初始化程序
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

void servopulse(int i,int ilength)//脉冲-引脚输出子程序
{
  digitalWrite(servopin[i],HIGH);//将舵机接口电平至高
  delayMicroseconds(ilength);//延时脉宽值的微秒数
  digitalWrite(servopin[i],LOW);//将舵机接口电平至低
}

void stmovcontrol(float angl11,float angl12,float angl13,float angl14,float angl15)//依次输出各个舵机的脉冲信号长度
{
  float ipulse[5],iangle[]={angl11,angl12,angl13,angl14,angl15};
  g_movflag=1; 
  for(int i=0;i<5;i++)
  { 
    ipulse[i]=iangle[i]*10+500;   
    if(ipulse[i]<=pulsesmin[i]){ipulse[i]=pulsesmin[i];}
    if(ipulse[i]>=pulsesmax[i]){ipulse[i]=pulsesmax[i];}
    if(ipulse[i]>(pulsesnow[i]+movspeed)){pulsesnow[i]+=movspeed;g_movflag=0;}
    else if(ipulse[i]<(pulsesnow[i]-movspeed)){pulsesnow[i]-=movspeed;g_movflag=0;}
    else{pulsesnow[i]=ipulse[i];}
    servopulse(i,pulsesnow[i]);
  }
}

int pulsetoangle(int pulse_temp,int ik)//实际脉冲信号长度和程序中转化后的脉冲长度的转换程序
{
  return (pulse_temp-500)/10;
}

//CSB----------------------------------------------------------------------------------------------------------
float measure()//超声波测距程序
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

//EEPROM-------------------------------------------------------------------------------------------------------
void writeweight(byte xi ,byte yi,unsigned int iweight16)//记录xi,yi位置物品的重量
{
  if(xi<10&&yi<10)
  {
     EEPROM.write(xi*20+2*yi, iweight16>>8);
     EEPROM.write(xi*20+2*yi+1, iweight16);
  }  
}

unsigned int getweight(byte xi ,byte yi)//获取xi,yi位置物品的重量
{
  unsigned int iread;
  if(xi<10&&yi<10)
  {
     iread=(EEPROM.read(xi*20+2*yi)<<8)+EEPROM.read(xi*20+2*yi+1);
     return iread;
  }
}

void weightdataclr()//清空储存器
{
  for (int iclr = 0; iclr < 300; iclr++)EEPROM.write(iclr, 1);
}

byte getposition(unsigned int iiweight16)//获取当前重量的物品的位置xi,yi
{
  unsigned int xi,yi,itemp,difmin=300;
  for(int i=0;i<10;i++)
  {
    for(int j=0;j<10;j++)
    {
      itemp=abs(iiweight16-getweight(i,j));
      if(itemp<difmin){difmin=itemp;xi=i,yi=j;}
    }
  }
  if(difmin<g_res){return xi*10+yi;}
  else{return 0xFF;}
}

byte ifbethere(byte xi,byte yi)//获取当前位置xi,yi是否已经有物品存在
{
  return EEPROM.read(xi*10+yi+200);
}

void puttothere(byte xi,byte yi)//将ifbewhere(xi,yi)置1
{
  EEPROM.write(xi*10+yi+200,1);
}

void getfromthere(byte xi,byte yi)//将ifbewhere(xi,yi)置0
{
  EEPROM.write(xi*10+yi+200,0);
}

//HWX--------------------------------------------------------------------------------------------------------
int HWX(unsigned long int k)//红外信号转换程序
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

//LCD---------------------------------------------------------------------------------------------------------
void lcd_clrdisp(int line,char *p)//第line行先清除后显示数据
{
  lcd.setCursor(0,line);
  lcd.print("                    ");
  lcd.setCursor(0,line);
  lcd.print(p);
}
void lcd_disp(int line,char *p)//在第line行显示数据
{
  lcd.setCursor(0,line);
  lcd.print(p);
}
void lcd_clr(int line)//清除第line行的显示数据
{
  lcd.setCursor(0,line);
  lcd.print("                    ");
}

void lcd_dispint(int line,unsigned long int int16)//在显示屏上显示整型int数据
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

//HX711-------------------------------------------------------------------------------------------------------
int hx711_weighting()//获取当前物品重量
{
  double sum = 0;
  int iwe;
  for (int i = 0; i < 3; i++)
    sum += hx.read();
  if(sum<10000000&&sum>-10000000)
  {
    iwe=sum/1000;
  }
  else
  {
    iwe=-10000;
  }
  return iwe;
}

//RTGY----------------------------------------------------------------------------------------------------------
byte RTGY()//获取当前是否有人存在
{
return digitalRead(RTGY_PIN);
}


