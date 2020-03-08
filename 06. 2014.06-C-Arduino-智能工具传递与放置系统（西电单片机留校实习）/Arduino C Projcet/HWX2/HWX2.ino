#include <IRremote.h>
 
int RECV_PIN = 5;//定义红外接收器的引脚为5
unsigned long int rec;  
 
IRrecv irrecv(RECV_PIN);
decode_results results;
int HWX(unsigned long int k);

void setup()
{
  Serial.begin(9600);
  irrecv.enableIRIn(); // 初始化红外接收器
}
 
void loop() 
{
  delay(100);
  if (irrecv.decode(&results))
  { 
    rec=results.value;
    irrecv.resume(); // 接收下一个值
    Serial.println(HWX(rec));
   }   
}
  
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

