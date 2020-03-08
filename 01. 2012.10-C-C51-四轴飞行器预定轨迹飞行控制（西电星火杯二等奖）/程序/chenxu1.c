#include<reg52.h>
sbit k0=P1^0;
sbit k1=P1^1;
sbit k2=P1^2;
sbit k3=P1^3;
sbit k4=P1^4;
sbit k5=P1^5;
sbit k6=P1^6;
sbit k7=P1^7;
unsigned int i,o;
unsigned delay(unsigned int k);
unsigned int flyf(unsigned int f);
unsigned int flyb(unsigned int b);
unsigned int flyl(unsigned int l);
unsigned int flyr(unsigned int r);
unsigned int up(unsigned int u);
unsigned int down(unsigned int d);
stop();

main()
{
P1=0xff;
while(1)
{
k0=0;
i=delay(300);
k0=1;
i=delay(500);
};
}




unsigned delay(unsigned int k)
{
unsigned int a,b;
for(a=0;a<k;a++)
    for(b=0;b<k;b++);
return i;
}




unsigned int flyf(unsigned int f)
{
for(o=0;o<f;o++)
{
k0=0;
i=delay(50);
k0=1;
i=delay(100);
}
return i;
}
unsigned int flyb(unsigned int b)
{
for(o=0;o<b;o++)
{
k0=0;
i=delay(50);
k0=1;
i=delay(100);
}
return i;
}
unsigned int flyl(unsigned int l)
{
for(o=0;o<l;o++)
{
k0=0;
i=delay(50);
k0=1;
i=delay(100);
}
return i;
}
unsigned int flyr(unsigned int r)
{
for(o=0;o<r;o++)
{
k0=0;
i=delay(50);
k0=1;
i=delay(100);
}
return i;
}
unsigned int up(unsigned int u)
{
for(o=0;o<u;o++)
{
k0=0;
i=delay(50);
k0=1;
i=delay(100);
}
return i;
}
unsigned int dowm(unsigned int d)
{
for(o=0;o<d;o++)
{
k0=0;
i=delay(50);
k0=1;
i=delay(100);
}
return i;
}
stop()
{
}
