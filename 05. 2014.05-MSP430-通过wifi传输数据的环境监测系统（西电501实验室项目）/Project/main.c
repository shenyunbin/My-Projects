#include <msp430.h> 

/*
 * main.c
 * ʹ��IO����� ��������ɫ��������ܣ�����ɫ��������ܵĲ�ͬ���ŽӲ�ͬ���輴�ɣ�Ҫʹ��3��IO�ڣ�P1.3��P1.4��P1.5
 */
//��ʼ��UART
#define uchar unsigned char
#define HIGH P2OUT|=BIT3;
#define LOW P2OUT&=~BIT3;
#define CPU           (1000000)
#define DelayNus(x)   (__delay_cycles((double)x*CPU/1000000.0))
//~~~~~RGB_LED~~~~~~~~~~
#define R_HIGH P1OUT|=BIT3;
#define R_LOW P1OUT&=~BIT3;//�����
#define G_HIGH P1OUT|=BIT4;
#define G_LOW P1OUT&=~BIT4;//�̵���
#define B_HIGH P1OUT|=BIT5;
#define B_LOW P1OUT&=~BIT5;//������
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~
const int SOILPOINT_SHORTCIRCUIT_ADC10VALUE=5;              //�ж����������·��ADC10����ֵ������ADC10����ֵС�ڴ���ֵʱ�����ж�Ϊ���������·
const long int SOIL_DIV_RESI_VALUE=14350;           //���������贮���ķ�ѹ����Ĵ�С������ģ�⿪�صĵ��裩
const int ILLU_INTE_1_ADC10VALUE=800;          //���ռ���Ϊ1ʱ��ADC10�����ٽ�ֵ������������ADC10����ֵ>800ʱ�����ռ���Ϊ1��������ͬ
const int ILLU_INTE_2_ADC10VALUE=500;          //���ռ���Ϊ2ʱ��ADC10�����ٽ�ֵ
const int ILLU_INTE_3_ADC10VALUE=300;          //���ռ���Ϊ3ʱ��ADC10�����ٽ�ֵ
const int ILLU_INTE_4_ADC10VALUE=150;          //���ռ���Ϊ4ʱ��ADC10�����ٽ�ֵ
const int ILLU_INTE_5_ADC10VALUE=50;           //���ռ���Ϊ5ʱ��ADC10�����ٽ�ֵ
const int ILLU_INTE_6_ADC10VALUE=0;            //���ռ���Ϊ6ʱ��ADC10�����ٽ�ֵ


int modecount;
int temp16,humd16;
unsigned char temph,templ,humdh,humdl,check,cal;
int itemp[6];

uchar transf_cmd[]="+++";//����+++,�ظ�a֮���ٷ���a���յ�+OK����ת��ΪAT����ģʽ
uchar flag=0;
uchar RX_BUF[100];        //���ܻ���
int RXi=0;               //RX_BUF[RXi]
uchar AT_WMODE[]="AT+WMODE";
uchar AT_WMODE_AP[]="=AP\n";
uchar AT_WMODE_STA[]="=STA\n";
uchar AT_WMODE_APSTA[]="=APSTA\n";
uchar AT_ENTM[]="AT+ENTM\n";//��AT����ģʽ����͸��ģʽ
uchar AT_Z[]="AT+Z\n";//����ģ��
uchar AT_WSSSID[]="AT+WSSSID";//����AP��SSID
uchar WSSSID[]="=liuning\n";//SSID
uchar AT_WSKEY[]="AT+WSKEY";//����STA�ļ��ܲ���
uchar WSKEY[]="=WPA2PSK,AES,1234567890\n";//���ܲ�������֤ģʽ�������㷨�����룩
uchar AT_WAKEY[]="AT+WAKEY";//����AP�ļ��ܲ���
uchar WAKEY[]="=WPA2PSK,AES,liuning91\n";
uchar AT_WSCAN[]="AT+WSCAN\n";//����AP�����ATָ��ķ���ֵ�кܶ��д���֤
//��������
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
	while(flag==0)//ת��ΪAT����ģʽ
	{
	while(flag==0)
	{
		sendstring(transf_cmd);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-1]=='a')flag=1;    //�ж����޻ظ�a
	}
	sendchar('a');
	_delay_cycles(1000000);
	if(!(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k'))
		flag=0;
	//delete(RXi);//��ջ���
	}
	flag=0;//falg��λ
	delete(RXi);//��ջ���
	RXi=0;


}
void wifi_AP(void)//��ΪAPģʽ
{
	while(flag==0)
	{
		sendstring(AT_WMODE);
		sendstring(AT_WMODE_AP);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')
		{sendstring(AT_WAKEY);//����AP����
		sendstring(WAKEY);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')flag=1;
		}
	}
	flag=0;//falg��λ
	delete(RXi);//��ջ���
	RXi=0;
	restar();
	_delay_cycles(1000000);
}

void wifi_STA(void)//��ΪSTAģʽ
{
	while(flag==0)
	{
		sendstring(AT_WMODE);
		sendstring(AT_WMODE_STA);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')
		{sendstring(AT_WSKEY);//����STA����
		sendstring(WSKEY);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')flag=1;
		}

	}
	flag=0;//falg��λ
	delete(RXi);//��ջ���
	RXi=0;
	restar();
	_delay_cycles(1000000);
}

void wifi_APSTA(void)//��ΪAP+STAģʽ
{
	while(flag==0)
	{
		sendstring(AT_WMODE);
		sendstring(AT_WMODE_APSTA);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')
		{sendstring(AT_WAKEY);//����AP����
		sendstring(WAKEY);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')flag=1;
		flag=0;
		sendstring(AT_WSSSID);
		sendstring(WSSSID);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')flag=1;
		flag=0;
		sendstring(AT_WSKEY);//����STA����
		sendstring(WSKEY);
		_delay_cycles(1000000);
		if(RX_BUF[RXi-6]=='o'&&RX_BUF[RXi-5]=='k')flag=1;
		}

	}
	flag=0;//falg��λ
	delete(RXi);//��ջ���
	RXi=0;
	restar();
	_delay_cycles(1000000);
}

void restar(void)
{
	sendstring(AT_Z);
}

void delete(int x)//��ջ���
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
void sendchar(uchar ch)/*��Ƭ������һ�ֽ�����*/
{
	while(!(IFG2&UCA0TXIFG));
	UCA0TXBUF=ch;
}
void sendstring(uchar *p) /*ͨ�����ڷ����ַ���*/
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

void SETDCO_LOWEST_FREQUENT(void)  //����������DCOʱ��������ʱ
{
    DCOCTL = 0;
    BCSCTL1 =0xB0;
}

void DELAYSECONDS_CPUSLEEP(int n)  //CPU˯�ߣ���ʱ����
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
	ADC10CTL1 = INCH_7;                            // P1.7��Ϊģ����������˿�
	ADC10AE0 |= BIT7;
}

int ADC10_READ(void)
{
//	int ADC_MEM;
    ADC10CTL0 |= ENC + ADC10SC;             // Sampling and conversion start�����жϴ����Ƿ���Ȼ��·
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
	DelayNus(20);                          //�ȴ�ģ�⿪���ȶ�
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
	DelayNus(20);                          //�ȴ�ģ�⿪���ȶ�
	itemp=ADC10_READ();
	if(itemp>ILLU_INTE_1_ADC10VALUE){illuinte_temp=1;}
	else if(itemp>ILLU_INTE_2_ADC10VALUE){illuinte_temp=2;}
	else if(itemp>ILLU_INTE_3_ADC10VALUE){illuinte_temp=3;}
	else if(itemp>ILLU_INTE_4_ADC10VALUE){illuinte_temp=4;}
	else if(itemp>ILLU_INTE_5_ADC10VALUE){illuinte_temp=5;}
	else{illuinte_temp=6;}

	return illuinte_temp;
}

unsigned char RECEIVE(void)                //���ܺ���
{
	int i;
	unsigned char num=0,tem,cnt;           //��ʱ�������ڴ洢��������
	  for(cnt=0;cnt<8;cnt++)
	  {
	    tem=0;
	    i=100;
	    while((!(P2IN&BIT3))&&(i--));      //�ȴ�50us�ĵ͵�ƽ����
	    DelayNus(30);
	    if((P2IN&BIT3))                    //����30us����Ϊ1
	    {
	      tem=1;
	      i=100;
	      while(P2IN&BIT3&&(i--));         //�ȴ�һλ�Ĳɼ�����
	    }
	    num<<=1;
	    num|=tem;
	  }
	  return num;

}
unsigned char AM2321_READ(void)            //dht11��ʪ�ȶ�ȡ��������ȡ���ݳɹ�����ֵ1�����ɹ�����0����ȡ�����ݱ�����temph,templ,humdh,humdl,humd16,temp16��
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

	humdh&=~BIT7;                          //��ȥ����λ������λ���ȶ���������������������
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
	DELAYSECONDS_CPUSLEEP(3);              //�ȴ�3��ʹAM2321�ȶ�
	itemp=AM2321_READ();
	for(i=0;i<5;i++)
	{
		if(itemp==1)break;
		DELAYSECONDS_CPUSLEEP(3);//����Ļ��ٲ�һ��
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
    sendchar('K');//����
    wifi_transf_cmd();
    wifi_APSTA();
    _delay_cycles(30000000);//������ʱ��
    //~~~~~~~~~~~���濪ʼ��⴫��������
    int SoilResiValue;
    unsigned char IlluValue,IfReadRight;
    while(1){
    _delay_cycles(10000000);
    SoilResiValue =  SOIL_RESI_MEASURE();         //��ȡ��������ֵ�����ֵ��λΪһŷķ
    IlluValue     =  ILLU_INTE_MEASURE();         //��ȡ����ǿ��ֵ�����ֵΪ1,2,3,4,5,6,����ֵԽ�����Խǿ
    IfReadRight   =  AM2321_MEASURE();            //��ȡAM2321��ֵ����ȡ�ɹ�����1�����򷵻�0����ȡ����ʪ����ֵ������ȫ�ֱ���temph,templ,humdh,humdl,humd16,temp16��
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
    CD4052_CHANNEL(4052);                               //�ر�ģ�⿪��

    DELAYSECONDS_CPUSLEEP(1);

    SETDCO_1MHz();




    //~~~~~~~~~~~~~~~~~~~
    	/*sendstring("liuning");//����
    	_delay_cycles(1000000);//����*/
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
