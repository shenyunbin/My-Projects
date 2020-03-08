int servopin[]={8,9,10,11,12};//定义舵机接口数字接口7
int myangle,i,j,imode,angelval[5];//定义角度变量
const int movspeed=10;
const int pulsesmin[]={2050,800,600,500,700};//定义最小脉宽
const int pulsesmax[]={580,2000,1600,1100,2000};//定义最大脉宽
int pulsesnow[]={2050,800,600,500,700};//定义最大脉宽
int anglemin[] ={0   ,0   ,-45 ,30  ,30  };
int anglemax[] ={180 ,180 ,90  ,180 ,180 };
//bit angleori[] ={0   ,0   ,0   ,0   ,0   };
void movinit()
{
  for(i=2;i<5;i++)
  {
    for(j=0;j<50;j++)
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
  for(i=0;i<2;i++)
  {
    for(j=0;j<50;j++)
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
  if(ilength>pulsesnow[i]+movspeed){pulsesnow[i]+=movspeed;}
  else if(ilength<pulsesnow[i]-movspeed){pulsesnow[i]-=movspeed;}
  else{pulsesnow[i]=ilength;}
  digitalWrite(servopin[i],HIGH);//将舵机接口电平至高
  delayMicroseconds(pulsesnow[i]);//延时脉宽值的微秒数
  digitalWrite(servopin[i],LOW);//将舵机接口电平至低
}

void movcontrol(int angl1,int angl2,int angl3,int angl4,int angl5)//定义一个脉冲函数
{
  int ipulse[5],iangle[]={angl1,angl2,angl3,angl4,angl5};
  for(i=0;i<5;i++)
  {
    if(iangle[i]>anglemax[i]){iangle[i]=anglemax[i];}
    if(iangle[i]<anglemin[i]){iangle[i]=anglemin[i];}
    ipulse[i]=(iangle[i]-anglemin[i])/(anglemax[i]-anglemin[i])*(pulsesmax[i]-pulsesmin[i])+pulsesmin[i];
    servopulse(i,ipulse[i]);
  }
}

void setup()
{
  
  pinMode(8,OUTPUT);//设定舵机接口为输出接口
  pinMode(9,OUTPUT);//设定舵机接口为输出接口  
  pinMode(10,OUTPUT);//设定舵机接口为输出接口
  pinMode(11,OUTPUT);//设定舵机接口为输出接口 
  pinMode(12,OUTPUT);//设定舵机接口为输出接口 
  movinit();
}
 void loop()//将0到9的数转化为0到180角度，并让LED闪烁相应数的次数
 {

if(imode<350)
{
movcontrol(0,0,-45,0,0);
}
if(imode>350)
{
movcontrol(180,180,180,180,180);
}
if(imode>700)imode=0;
imode++;
delay(14);
}
