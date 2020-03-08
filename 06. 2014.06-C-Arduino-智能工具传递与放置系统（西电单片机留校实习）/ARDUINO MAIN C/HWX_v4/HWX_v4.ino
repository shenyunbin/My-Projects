#include <IRremote.h>
 
int RECV_PIN = 9;//设置红外接收信号引脚�
unsigned long int rec;  
 
IRrecv irrecv(RECV_PIN);
decode_results results;
char HWX(unsigned long int k);

void setup()
{
  Serial.begin(9600);
  irrecv.enableIRIn(); //红外接收初始化 
}
 
void loop() 
{
  //delay(50);
  if (irrecv.decode(&results))
  { 
    rec=results.value;
    irrecv.resume(); //接收下一组数据�
    Serial.write(HWX(rec));
    //Serial.print(rec);
   }   
}
  
char HWX(unsigned long int k)
  {
         switch (k) {
         case (16738455):             //0
         { return '0';
           break;
         }
         case (16724175):             //1
         {  return '1';
            break;
         }
         case (16718055):             //2
         {  return '2';
            break;
         }
         case (16743045):             //3
         {  return '3';
            break;
         }
         case (16716015):             //4
         {  return '4';
            break;
         }
         case (16726215):             //5
         {  return '5';
            break;
         }
         case (16734885):             //6
         {  return '6';
            break;
         }
         case (16728765):             //7
         {  return '7';
            break;
         }
         case (16730805):             //8
         {  return '8';
            break;
         }
         case (16732845):             //9
         {  return '9';
            break;
         }
         case (16720605):            //left
         {  return 'B';
            break;
         }
         case (16712445):            //right
         {  return 'G';
            break;
         }
         case (16761405):
         {
           return 'P'; 
           break;        
         }
         case (4294967295):
         {
           delay(100);
           return 'E'; 
           break;        
         }
         
         default:
           break; 
     }
      //irrecv.resume(); �
   }


