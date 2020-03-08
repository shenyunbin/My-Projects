//int servopin=8;//定义舵机接口数字接口7
int myangle;//定义角度变量
int pulsewidth;//定义脉宽变量
int val;
 int i;
void servopulse(int servopin,int myangle)//定义一个脉冲函数
{
  digitalWrite(servopin,HIGH);//将舵机接口电平至高
  delayMicroseconds(myangle);//延时脉宽值的微秒数
  digitalWrite(servopin,LOW);//将舵机接口电平至低

}
void setup()
{

  pinMode(8,OUTPUT);//设定舵机接口为输出接口
  pinMode(9,OUTPUT);//设定舵机接口为输出接口  
  pinMode(10,OUTPUT);//设定舵机接口为输出接口
  pinMode(11,OUTPUT);//设定舵机接口为输出接口 
}
 void loop()//将0到9的数转化为0到180角度，并让LED闪烁相应数的次数
 {

if(i<100)
{
servopulse(8,800);//引用脉冲函数
servopulse(9,800);//引用脉冲函数
servopulse(10,800);//引用脉冲函数
servopulse(11,800);//引用脉冲函数

}
if(i>100)
{
servopulse(8,1300);//引用脉冲函数
servopulse(9,1300);//引用脉冲函数
servopulse(10,1300);//引用脉冲函数
servopulse(11,1300);//引用脉冲函数

}
if(i>300)i=0;
i++;
delay(15);
}
