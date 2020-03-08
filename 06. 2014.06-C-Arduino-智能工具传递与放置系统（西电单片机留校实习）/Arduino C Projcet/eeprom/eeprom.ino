#include <EEPROM.h>

int xtemp;
void writeweight(byte xi ,byte yi,unsigned int iweight16);
unsigned int getweight(byte xi ,byte yi);
void weightdataclr();
void setup()
{
  Serial.begin(9600);
}
void loop()
{
if(Serial.available())
{
  xtemp=Serial.read();
  if(xtemp=='o'){for(int i=0;i<10;i++){for(int j=0;j<10;j++){Serial.println(getweight(i ,j));}}}
  else if(xtemp=='i'){for(int i=0;i<10;i++){for(int j=0;j<10;j++){Serial.println(getposition(getweight(i ,j)));}}}
  else{for(int i=0;i<10;i++){for(int j=0;j<10;j++){writeweight(i,j,(i*10+j)*200);}}}
  
}

}
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
for (int iclr = 0; iclr < 200; iclr++)EEPROM.write(iclr, 0);
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

