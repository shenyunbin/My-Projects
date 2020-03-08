#include <msp430.h> 

/*
 * main.c
 * 使用IO口输出 ，控制三色发光二极管，在三色发光二极管的不同引脚接不同电阻即可，要使用3个IO口：P1.3、P1.4、P1.5
 */
//初始化UART
#define uchar unsigned char
#define HIGH P2OUT|=BIT3;
#define LOW P2OUT&=~BIT3;
#define CPU           (1000000)
#define DelayNus(x)   (__delay_cycles((double)x*CPU/1000000.0))
//~~~~~RGB_LED~~~~~~~~~~
#define R_HIGH P1OUT|=BIT3;
#define R_LOW P1OUT&=~BIT3;//红灯亮
#define G_HIGH P1OUT|=BIT4;
#define G_LOW P1OUT&=~BIT4;//绿灯亮
#define B_HIGH P1OUT|=BIT5;
#define B_LOW P1OUT&=~BIT5;//蓝灯亮
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~
const int SOILPOINT_SHORTCIRCUIT_ADC10VALUE=5;              //判断土壤触点短路的ADC10测量值，即当ADC10测量值小于此数值时，即判断为土壤触点短路
const long int SOIL_DIV_RESI_VALUE=14350;           //与土壤电阻串联的分压电阻的大小（包括模拟开关的电阻）
const int ILLU_INTE_1_ADC10VALUE=800;          //光照级别为1时的ADC10测量临界值，即光敏电阻ADC10测量值>800时，光照级别为1，以下类同
const int ILLU_INTE_2_ADC10VALUE=500;          //光照级别为2时的ADC10测量临界值
const int ILLU_INTE_3_ADC10VALUE=300;          //光照级别为3时的ADC10测量临界值
const int ILLU_INTE_4_ADC10VALUE=150;          //光照级别为4时的ADC10测量临界值
const int ILLU_INTE_5_ADC10VALUE=50;           //光照级别为5时的ADC10测量临界值
const int ILLU_INTE_6_ADC10VALUE=0;            //光照级别为6时的ADC10测量临界值


int modecount;
int temp16,humd16;
unsigned char temph,templ,humdh,humdl,check,cal;
int itemp[6];

uchar transf_cmd[]="+++";//发送+++,回复a之后，再发送a，收到+OK，即转换为AT命令模式
uchar flag=0;
uchar RX_BUF[100];        //接受缓存
int RXi=0;               //RX_BUF[RXi]
uchar AT_WMODE[]="AT+WMODE";
uchar AT_WMODE_AP[]="=AP\n";
uchar AT_WMODE_STA[]="=STA\n";
uchar AT_WMODE_APSTA[]="=APSTA\n";
uchar AT_ENTM[]="AT+ENTM\n";//从AT命令模式进入透传模式
uchar AT_Z[]="AT+Z\n";//重启模块
uchar AT_WSSSID[]="AT+WSSSID";//设置AP的SSID
uchar WSSSID[]="=liuning\n";//SSID
uchar AT_WSKEY[]="AT+WSKEY";//设置STA的加密参数
uchar WSKEY[]="=WPA2PSK,AES,1234567890\n";//加密参数（认证模式，加密算法，密码）
uchar AT_WAKEY[]="AT+WAKEY";//设置AP的加密参数
uchar WAKEY[]="=WPA2PSK,AES,liuning91\n";
uchar AT_WSCAN[]="AT+WSCAN\n";//搜索AP，这个AT指令的返回值有很多有待验证
//函数声明
void wifi_transf_cmd(void);
void wifi_AP(void);
void wifi_STA(void);
void wifi_APSTA(void);
void restar(void);
void delete(int x);
void sendchar(uchar ch);
void sendstring(uchar *p);
void SEND_INT_TO_STR(unsigned int int16);
void SETDCO_1MHz(void);
void SETDCO_LOWEST_FREQUENT(void);
void DELAYSECONDS_CPUSLEEP(int n);
void ADC10_INIT(void);
int ADC10_READ(void);
void CD4052_CHANNEL(int n);
int SOILPOINT_ADC10VALUE(void);
unsigned int SOIL_RESI_MEASURE(void);
unsigned char ILLU_INTE_MEASURE(void);
unsigned char RECEIVE(void);
unsigned char AM2321_READ(void);
unsigned char AM2321_MEASURE(void);
//~~~~~~~~~~~~

void wifi_transf_cmd(void)
{
	while(flag==0)//转换为AT命令模式
	{
	while(flag==0)
	{
		sendstring(transf_cmd);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-1]=='a')flag=1;    //判断有无回复a
	}
	sendchar('a');
	_delay_cycles(1000000);
	if(!(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k'))
		flag=0;
	//delete(RXi);//清空缓存
	}
	flag=0;//falg复位
	delete(RXi);//清空缓存
	RXi=0;


}
void wifi_AP(void)//设为AP模式
{
	while(flag==0)
	{
		sendstring(AT_WMODE);
		sendstring(AT_WMODE_AP);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')
		{sendstring(AT_WAKEY);//设置AP加密
		sendstring(WAKEY);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')flag=1;
		}
	}
	flag=0;//falg复位
	delete(RXi);//清空缓存
	RXi=0;
	restar();
	_delay_cycles(1000000);
}

void wifi_STA(void)//设为STA模式
{
	while(flag==0)
	{
		sendstring(AT_WMODE);
		sendstring(AT_WMODE_STA);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')
		{sendstring(AT_WSKEY);//设置STA加密
		sendstring(WSKEY);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')flag=1;
		}

	}
	flag=0;//falg复位
	delete(RXi);//清空缓存
	RXi=0;
	restar();
	_delay_cycles(1000000);
}

void wifi_APSTA(void)//设为AP+STA模式
{
	while(flag==0)
	{
		sendstring(AT_WMODE);
		sendstring(AT_WMODE_APSTA);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')
		{sendstring(AT_WAKEY);//设置AP加密
		sendstring(WAKEY);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')flag=1;
		flag=0;
		sendstring(AT_WSSSID);
		sendstring(WSSSID);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')flag=1;
		flag=0;
		sendstring(AT_WSKEY);//设置STA加密
		sendstring(WSKEY);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')flag=1;
		}

	}
	flag=0;//falg复位
	delete(RXi);//清空缓存
	RXi=0;
	restar();
	_delay_cycles(1000000);
}

void restar(void)
{
	sendstring(AT_Z);
}

void delete(int x)//清空缓存
{
	for(;x>=0;x--)
		RX_BUF[x]=0x00;
}


void Init_UCA0(void)
{


	  UCA0CTL1 |= UCSSEL_2;                     // SMCLK
	  UCA0BR0 = 104;                            // 1MHZ
	  UCA0BR1 = 0;                              // 1MHZ 9600
	  UCA0MCTL =UCBRS0;                        // Modulation    UCBRS1
	  UCA0CTL1 &= ~UCSWRST;                     // **Initialize USCI state machine**
	  IE2 |= UCA0RXIE;                  // Enable USCI_A0 RX and TX interrupt
	  _EINT();
}
void sendchar(uchar ch)/*单片机发送一字节数据*/
{
	while(!(IFG2&UCA0TXIFG));
	UCA0TXBUF=ch;
}
void sendstring(uchar *p) /*通过串口发送字符串*/
{

  while(*p)
  {
	  sendchar(*p);
	  p++;
  }
}
void SEND_INT_TO_STR(unsigned int int16)
{

	int ienable,i;
	unsigned int iint;
	ienable=0;
	iint=int16;
	for(i=0;i<6;i++)
	{
		itemp[i]=0;
		itemp[i]=iint%10;
		iint=iint/10;
		itemp[i]+=48;
	}
	for(i=6;i>0;i--)
    {
		if(itemp[i-1]!=48){ienable=1;}
		if(ienable==1){sendchar(itemp[i-1]);}
	}

}
void SETDCO_1MHz(void)
{
	BCSCTL1 = CALBC1_1MHZ;                      // Set DCO
	DCOCTL = CALDCO_1MHZ;
}

void SETDCO_LOWEST_FREQUENT(void)  //设置最慢的DCO时钟用于延时
{
    DCOCTL = 0;
    BCSCTL1 =0xB0;
}

void DELAYSECONDS_CPUSLEEP(int n)  //CPU睡眠，定时唤醒
{
	if(n>5)n=6;
	SETDCO_LOWEST_FREQUENT();
    TA0CCTL0 = CCIE;
    TA0CCR0 = 10000*n;
    TA0CTL = TASSEL_2 | MC_1 | TACLR | ID_3;
    __bis_SR_register(LPM0_bits | GIE);
}

void ADC10_INIT(void)
{
	ADC10CTL0 = ADC10SHT_2 + ADC10ON + ADC10IE;    // ADC10ON, interrupt enabled
	ADC10CTL1 = INCH_7;                            // P1.7作为模拟量的输入端口
	ADC10AE0 |= BIT7;
}

int ADC10_READ(void)
{
//	int ADC_MEM;
    ADC10CTL0 |= ENC + ADC10SC;             // Sampling and conversion start，，判断触点是否仍然短路
    __bis_SR_register(CPUOFF + GIE);        // LPM0, ADC10_ISR will force exit
//    ADC_MEM=ADC10MEM;
    return ADC10MEM;
}

void CD4052_CHANNEL(int n)
{
	switch(n)
	{
	case 0:P2OUT&=~BIT2;P2OUT&=~BIT1;P2OUT&=~BIT0;break;
	case 1:P2OUT&=~BIT2;P2OUT&=~BIT1;P2OUT|= BIT0;break;
	case 2:P2OUT&=~BIT2;P2OUT|= BIT1;P2OUT&=~BIT0;break;
	case 3:P2OUT&=~BIT2;P2OUT|= BIT1;P2OUT|= BIT0;break;
	case 4052:P2OUT|= BIT2;break;
	}

}

int SOILPOINT_ADC10VALUE(void)
{
	CD4052_CHANNEL(0);
	return ADC10_READ();
}

unsigned int SOIL_RESI_MEASURE(void)
{
	unsigned int soilresi_temp,itemp;
	long int icompare;
	CD4052_CHANNEL(0);
	DelayNus(20);                          //等待模拟开关稳定
	itemp=ADC10_READ();
	if(itemp<1015){icompare=SOIL_DIV_RESI_VALUE*itemp/(1023-itemp);}
	else{icompare=60000;}
	if(icompare<60000){soilresi_temp=icompare;}
	else{soilresi_temp=60000;}
	return soilresi_temp;
}

unsigned char ILLU_INTE_MEASURE(void)
{
	int itemp;
	unsigned char illuinte_temp;
	CD4052_CHANNEL(1);
	DelayNus(20);                          //等待模拟开关稳定
	itemp=ADC10_READ();
	if(itemp>ILLU_INTE_1_ADC10VALUE){illuinte_temp=1;}
	else if(itemp>ILLU_INTE_2_ADC10VALUE){illuinte_temp=2;}
	else if(itemp>ILLU_INTE_3_ADC10VALUE){illuinte_temp=3;}
	else if(itemp>ILLU_INTE_4_ADC10VALUE){illuinte_temp=4;}
	else if(itemp>ILLU_INTE_5_ADC10VALUE){illuinte_temp=5;}
	else{illuinte_temp=6;}

	return illuinte_temp;
}

unsigned char RECEIVE(void)                //接受函数
{
	int i;
	unsigned char num=0,tem,cnt;           //临时变量用于存储接受数据
	  for(cnt=0;cnt<8;cnt++)
	  {
	    tem=0;
	    i=100;
	    while((!(P2IN&BIT3))&&(i--));      //等待50us的低电平结束
	    DelayNus(30);
	    if((P2IN&BIT3))                    //长于30us定义为1
	    {
	      tem=1;
	      i=100;
	      while(P2IN&BIT3&&(i--));         //等待一位的采集结束
	    }
	    num<<=1;
	    num|=tem;
	  }
	  return num;

}
unsigned char AM2321_READ(void)            //dht11温湿度读取函数，读取数据成功返回值1，不成功返回0，读取的数据保存在temph,templ,humdh,humdl,humd16,temp16中
{
	int i;
	P2DIR|=BIT3;
	P2OUT|=BIT3;

	SETDCO_1MHz();

	//HIGH;
	//DelayNus(1800);
	LOW;
	DelayNus(20000);
	HIGH;
	P2REN|=BIT3;
	P2DIR&=~BIT3;
	DelayNus(5);
	i=100;
	while(P2IN&BIT3&&(i--));
	i=100;
	while((!P2IN&BIT3)&&(i--));
	i=100;
	while((P2IN&BIT3)&&(i--));
	//P2REN&=~BIT3;
	//Data comes
	humdh=RECEIVE();
	humdl=RECEIVE();
	temph=RECEIVE();
	templ=RECEIVE();
	check=RECEIVE();

	humdh&=~BIT7;                          //除去符号位，符号位不稳定，好像是器件本身问题
	temph&=~BIT7;

	cal=humdh+humdl+temph+templ;

	if((check>=cal)||(check==cal+0x80)||(check==cal-0x80))
	{
		temp16=2*(templ+temph*256);
		humd16=2*(humdl+humdh*256);
/*
		humdh*=2;
		humdl*=2;
		temph*=2;
		templ*=2;
*/
		return 1;
	}
	else
	{
		temp16=0;
		humd16=0;
		return 0;
	}
}

unsigned char AM2321_MEASURE(void)
{
	unsigned char i,itemp;
	CD4052_CHANNEL(2);
	DELAYSECONDS_CPUSLEEP(3);              //等待3秒使AM2321稳定
	itemp=AM2321_READ();
	for(i=0;i<5;i++)
	{
		if(itemp==1)break;
		DELAYSECONDS_CPUSLEEP(3);//错误的话再测一次
		itemp=AM2321_READ();
	}
	return itemp;
}
void main(void) {
    WDTCTL = WDTPW | WDTHOLD;	// Stop watchdog timer
    BCSCTL1 = CALBC1_1MHZ;                    // Set DCO
    DCOCTL = CALDCO_1MHZ;
    P1DIR|=BIT3+BIT4+BIT5;
    P1SEL = BIT1 + BIT2 ;                     // P1.1 = RXD, P1.2=TXD
    P1SEL2 = BIT1 + BIT2 ;                     // P1.1 = RXD, P1.2=TXD
    Init_UCA0();
    P2DIR |= BIT0+BIT1+BIT2;
    ADC10_INIT();
    SETDCO_1MHz();
    sendchar('K');//测试
    wifi_transf_cmd();
    wifi_APSTA();
    _delay_cycles(30000000);//重启的时间
    //~~~~~~~~~~~下面开始检测传感器数据
    int SoilResiValue;
    unsigned char IlluValue,IfReadRight;
    while(1){
    _delay_cycles(10000000);
    SoilResiValue =  SOIL_RESI_MEASURE();         //读取土壤电阻值，输出值单位为一欧姆
    IlluValue     =  ILLU_INTE_MEASURE();         //读取光照强度值，输出值为1,2,3,4,5,6,并且值越大光照越强
    IfReadRight   =  AM2321_MEASURE();            //读取AM2321的值，读取成功返回1，否则返回0；读取的温湿度数值保存在全局变量temph,templ,humdh,humdl,humd16,temp16中
    sendstring("\nSoil:   ");
    SEND_INT_TO_STR(SoilResiValue);
    sendstring("\nIllu:   ");
    SEND_INT_TO_STR(IlluValue);
    sendstring("isright?:   ");
    SEND_INT_TO_STR(IfReadRight);
    sendstring("\nHumd:   ");
    SEND_INT_TO_STR(humd16);
    sendstring("\nTemp:   ");
    SEND_INT_TO_STR(temp16);
    sendstring("\nend\n");
    }
    CD4052_CHANNEL(4052);                               //关闭模拟开关

    DELAYSECONDS_CPUSLEEP(1);

    SETDCO_1MHz();




    //~~~~~~~~~~~~~~~~~~~
    	/*sendstring("liuning");//测试
    	_delay_cycles(1000000);//测试*/
      while(1);
}
#pragma vector=USCIAB0RX_VECTOR
__interrupt void USCI0RX_ISR1(void)
{
	while(!(IFG2&UCA0RXIFG));
	RX_BUF[RXi]=UCA0RXBUF;
	RXi++;
}
#pragma vector=ADC10_VECTOR
__interrupt void ADC10_ISR(void)
{
	ADC10CTL0 &=~( ENC + ADC10SC);
	__bic_SR_register_on_exit(CPUOFF + GIE);      // Clear CPUOFF bit from 0(SR)
}

#pragma vector=TIMER0_A0_VECTOR
__interrupt void TIMER0_A0_ISR(void)
{
	P1OUT ^= 0x01;                            // Toggle P1.0
	TA0CCR0 = 10000;                              // Add Offset to CCR0
	TA0CCTL0 &=~ CCIE;                            // CCR0 interrupt disabled
	__bic_SR_register_on_exit(LPM0_bits + GIE);
}
