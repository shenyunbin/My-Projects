#include "Wire.h"
#include "I2Cdev.h"
#include "ADXL345.h"
ADXL345 adxl; 
int ax, ay, az, vx, vy, vz, xx, xy, xz;
int ax0, ay0, az0;
int Tx, Ty, Tz;
int Xt[101], Yt[101], Zt[101], AXt[15], AYt[15], AZt[15],VXt[5],VYt[5],VZt[5];
int boundary = 10; //4
int FS;
//鏁村瀷鏁版嵁鍙戦�
void INT16_SEND(int int16)  {
  unsigned int uint16;
  uint16 = int16 + 32767;
  Serial.write(uint16 >> 8);
  Serial.write(uint16);
}
//鏁版嵁鍙戦�
void DATA_SEND()  {
  Serial.write(253);
  INT16_SEND(ax0);
  INT16_SEND(ay0);
  INT16_SEND(az0);
  INT16_SEND(ax);
  INT16_SEND(ay);
  INT16_SEND(az);
  INT16_SEND(vx);
  INT16_SEND(vy);
  INT16_SEND(vz);
  INT16_SEND(xx);
  INT16_SEND(xy);
  INT16_SEND(xz);
  Serial.write(254);
}
//Test Function
void TEST1() {
  Serial.print(ax0);
  Serial.print("\t");
  Serial.print(ay0);
  Serial.print("\t");
  Serial.print(az0);
  Serial.print("\t");
  Serial.print(ax);
  Serial.print("\t");
  Serial.print(ay);
  Serial.print("\t");
  Serial.print(az);
  Serial.print("\t");
  Serial.print(vx);
  Serial.print("\t");
  Serial.print(vy);
  Serial.print("\t");
  Serial.print(vz);
  Serial.print("\t");
  Serial.print(xx);
  Serial.print("\t");
  Serial.print(xy);
  Serial.print("\t");
  Serial.println(xz);
}
void TEST2() {
  Serial.print(F15(Yt));
  Serial.print("\t");
  Serial.print(ay);
  Serial.print("\t");
  Serial.print(vy);
  Serial.print("\t");
  Serial.println(xy);
}
//15鐐瑰彇骞冲潎鍊�int  F15S(int in[])  {
  int averange = 0;
  for (int i = 40; i < 55; i++) {
    averange += in[i];
  }
  averange /= 15.0;
  return averange;
}
//15鐐瑰彇骞冲潎鍊�int  F15(int in[])  {
  int averange = 0;
  for (int i = 43; i < 58; i++) {  //43 58
    averange += in[i];
  }
  averange /= 15.0;
  return averange;
}
//101鐐瑰彇骞冲潎鍊�int F101B(int in[])  {
  int averange;
  for(int i=0; i<101;i++){
    averange+=in[i];
  }
  averange/=101.0;
return averange;
}
//101鐐瑰彇骞冲潎鍊硷紙甯︿竴闃舵护娉級
int F101A(int in[], int *Tin)  {
  int averange = 0;
  
  for (int i = 0; i < 101; i++) {
    averange += in[i];
  }
  averange /= 101.0;
  if (*Tin == 0) *Tin=averange;
  else *Tin = 0.5 * averange + 0.5 * (*Tin);  //鍔犻�搴︿竴闃舵护娉�  return *Tin;
}
//鍔犻�搴︽护娉�骞虫粦澶勭悊锛堣秴杩囦复鐣屽�鎵嶈緭鍑哄姞閫熷害锛�void AProcess1( int *ai1, int *ai2, int *ai3,
               int in1[], int in2[], int in3[],
               int *Tin1, int *Tin2, int *Tin3 ) {
  int Apro1, Apro2, Apro3,F1,F2,F3;
  F1=F15(in1);
  F2=F15(in2);
  F3=F15(in3);
  Apro1 = F1 - F101A(in1, Tin1);
  Apro2 = F2 - F101A(in2, Tin2);
  Apro3 = F3 - F101A(in3, Tin3);
  if ((abs(Apro1) + abs(Apro2) + abs(Apro3)) > boundary) {  //鍔犻�搴﹁秴杩囬榾鍊兼槸杈撳嚭
    *ai1 = Apro1-2;
    *ai2 = Apro2-2;
    *ai3 = Apro3-2;
  }
  else {
    *ai1 = 0;  //鍔犻�搴︾疆0
    *ai2 = 0;
    *ai3 = 0;
  }
  if((abs(F15S(in1)-F1)<2)&&(abs(F1)<20))*ai1=0;  
  if((abs(F15S(in2)-F2)<2)&&(abs(F2)<20))*ai2=0;
  if((abs(F15S(in3)-F3)<2)&&(abs(F3)<20))*ai3=0;
}
//绠�崟鐨勫姞閫熷害婊ゆ尝+骞虫粦澶勭悊
int AProcess2(int in[]){
  return F15(in)-F101B(in);
}
//澶ц嚧璁＄畻鎵嬫寚娲诲姩鏃堕棿鍗犳�鐨勬椂闂寸殑绋嬪害
void iABS(int *Trend,int in1,int in2,int in3){
  if((abs(*Trend)>362)&&(abs(in1)>2||abs(in2)>2||abs(in3)>2)) *Trend=360;//in1!=0||in2!=0||in3!=0
  else if(abs(in1)>2||abs(in2)>2||abs(in3)>2) *Trend+=2; 
  else if(abs(*Trend)>20) *Trend-=20;
}
//鍔犻�搴﹀ぇ浜庝竴瀹氬�鏃舵墠璁″叆閫熷害鍙樺寲
void VProcess1( int *vi, int in[]) {
  *vi += (in[1]+in[0])*0.02*100;
}
//褰撳姞閫熷害鍧囪繛缁负0鏃剁殑閫熷害缃�澶勭悊
void VProcess2( int *vi1, int *vi2, int *vi3,
                int *xi1, int *xi2, int *xi3,
                int in1[], int in2[], int in3[] ) {
  if ( (in1[8] == 0) && (in1[5] == 0) && (in1[3] == 0) && (in1[0] == 0) &&
       (in2[8] == 0) && (in2[5] == 0) && (in2[3] == 0) && (in2[0] == 0) &&
       (in3[8] == 0) && (in3[5] == 0) && (in3[3] == 0) && (in3[0] == 0) ) {
    //*xi1 -= 0.5 * (*vi1);//娑堥櫎閫熷害缃�鍓嶆墍澶氱畻鐨勪綅绉婚暱搴�    //*xi2 -= 0.5 * (*vi2);
    //*xi3 -= 0.5 * (*vi3);
    *vi1 = 0;  //閫熷害缃�
    *vi2 = 0;
    *vi3 = 0;
  }
  else if( (abs(*vi1) < 450) && 
           (abs(*vi2) < 450) &&
           (abs(*vi3) < 450) && 
           (in1[0] == 0) && 
           (in2[0] == 0) && 
           (in3[0] == 0) ) {
    *vi1 = 0;  //閫熷害缃�
    *vi2 = 0;
    *vi3 = 0;                     
  }
}
//鍔犻�搴﹀ぇ浜庝竴瀹氬�鏃舵墠璁″叆閫熷害鍙樺寲
void XProcess1( int *xi, int in[]) {
  *xi += (in[4]+in[3])*0.02*5;
}
//浣嶇Щ杈圭晫璁惧畾
int XProcess2(int xi) {
  int bond = 12000;
  if (xi > bond)return bond;
  else if (xi < -bond)return -bond;
  else return xi;
}
//鏁扮粍鏁版嵁鏇存柊
void cir(int a[], int num, int temp) {
  int i;
  for (i = 0; i < num - 1; i++) {
    a[i] = a[i + 1];
  }
  a[i] = temp;
}
//鑾峰彇鍔犻�搴︽暟鎹�int GetA1() {
  adxl.readAccel(&ax0, &ay0, &az0);//璇诲彇鍘熷3涓暟鎹�  cir(Xt, 101, ax0);  //灏嗗師濮嬪姞閫熷害鍊煎瓨鍏ユ暟缁勶紝鐢ㄤ簬婊ゆ尝
  cir(Yt, 101, ay0);
  cir(Zt, 101, az0);
  AProcess1( &ax, &ay, &az,
            Xt,  Yt, Zt,
            &Tx, &Ty, &Tz );
  cir(AXt, 15, ax);  //灏嗗鐞嗚繃鐨勫姞閫熷害鍊煎瓨鍏ユ暟缁�  cir(AYt, 15, ay);
  cir(AZt, 15, az);
  iABS(&FS,ax,ay,az);
}
//鑾峰彇鍔犻�搴︽暟鎹�int GetA2() {
  adxl.readAccel(&ax0, &ay0, &az0);//璇诲彇鍘熷3涓暟鎹�  cir(Xt, 101, ax0);  //灏嗗師濮嬪姞閫熷害鍊煎瓨鍏ユ暟缁勶紝鐢ㄤ簬婊ゆ尝
  cir(Yt, 101, ay0);
  cir(Zt, 101, az0);
  ax = AProcess2(Xt);
  ay = AProcess2(Yt);
  az = AProcess2(Zt);
  cir(AXt, 15, ax);  //灏嗗鐞嗚繃鐨勫姞閫熷害鍊煎瓨鍏ユ暟缁�  cir(AYt, 15, ay);
  cir(AZt, 15, az);
}
//鑾峰彇閫熷害鏁版嵁
void GetV1() {
  VProcess1(&vx, AXt);
  VProcess1(&vy, AYt);
  VProcess1(&vz, AZt);
  VProcess2( &vx, &vy, &vz,
             &xx, &xy, &xz,
             AXt, AYt, AZt );
  cir(VXt, 5, vx);  //灏嗗鐞嗚繃鐨勫姞閫熷害鍊煎瓨鍏ユ暟缁�  cir(VYt, 5, vy);
  cir(VZt, 5, vz);
}
//鑾峰彇閫熷害鏁版嵁
void GetV2() {
  vx=vx*0.90+ax*5; //鍘婚櫎閫熷害鐨勫亸绉�  vy=vy*0.90+ay*5;
  vz=vz*0.90+az*5;
}
//鑾峰彇浣嶇Щ鏁版嵁
void GetX1() {
  XProcess1(&xx, VXt);
  XProcess1(&xy, VYt);
  XProcess1(&xz, VZt);
  xx = XProcess2(xx); //璁惧畾浣嶇Щ鐨勮竟鐣�  xy = XProcess2(xy);
  xz = XProcess2(xz);
}
//鑾峰彇浣嶇Щ鏁版嵁
void GetX2() {
  xx=xx*0.80+vx; //鍘婚櫎浣嶇Щ鐨勫亸绉�  xy=xy*0.80+vy;
  xz=xz*0.80+vz;
}

void setup() {
  Serial.begin(115200,SERIAL_8E2);//鍒濆鍖�  adxl.powerOn();
  delay(20);
}

void loop() {
  for (int i = 0; i < 100; i++)
  {
    adxl.readAccel(&ax0, &ay0, &az0);//璇诲彇鍘熷3涓暟鎹�    cir(Xt, 101, ax0);
    cir(Yt, 101, ay0);
    cir(Zt, 101, az0);
    delay(18);
  }
  while (1)
  {
    delay(18);    
    if(FS<400){ //妫�祴鍒颁笉鏄繛缁繍鍔ㄦ椂锛岃繍琛岀1绉嶇畻娉�      GetA1();
      iABS(&FS,ax,ay,az);
      GetV1();
      GetX1();      
    }
    else{  //妫�祴鍒版槸杩炵画杩愬姩鏃讹紝杩愯绗�绉嶇畻娉�      GetA2();
      iABS(&FS,ax,ay,az);
      GetV2();
      GetX2();
    }
    //DATA_SEND();   
    TEST2();
  }
}



