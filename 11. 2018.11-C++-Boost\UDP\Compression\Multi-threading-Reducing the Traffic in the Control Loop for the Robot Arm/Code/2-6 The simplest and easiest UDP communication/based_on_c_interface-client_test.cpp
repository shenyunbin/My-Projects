//boost
#include "boost/asio.hpp"
//stl
#include <iostream>
//mt
#include <thread>
//std
#include "myinterface2.h"
#include <iostream>
#include <cmath>
#include <franka/exception.h>
#include <franka/robot.h>
#include "examples_common.h"

struct My{
std::array<double, 16> pose;
double time;
}; 
My my;
My _my1;
My _my2;
void UDPReceiver(){
    UDPComm UDP2;
    UDP2.UDPBind(7475);
    UDP2.UDPSetRemoteAdr("172.16.16.221",7474);
    UDP2.UDPRecv((uchar8t*)&_my1,sizeof(_my1));
}


int main(){
    UDPComm UDP;
    UDP.UDPBind(7475);
    UDP.UDPSetRemoteAdr("172.16.16.221",7474);
    uchar8t recv_buf[1024];
    //std::thread r([&] { UDPReceiver(); });
    my.time=0.0;
    my.pose={0};
    Timer T1;
    double wt=0;
    double ww=0;
    double wmax=0;
    double wmin=1000;
    int count=0;
    for(;;){
        //std::cout << "input:";
		//std::cin.ignore();
		//std::cout << "endl";
        T1.start();
        my.time+=0.001;
        UDP.UDPSend((uchar8t*)&my,sizeof(my));
        UDP.UDPRecv((uchar8t*)&my,sizeof(my));
        //long int i=100000;
        //while(i--);

        T1.stop();
        wt=T1.value();
        ww+=wt;
        count++;
        std::cout<<wt<<std::endl;
        if(wt>wmax)wmax=wt;
        if(wt<wmin)wmin=wt;
        
        if(count>999)break;
        //std::cout<<my.time<<" "<<T1.value()<<std::endl;    
        //break;    
    }
        std::cout<<"average time "<<ww/1000<<std::endl;
        std::cout<<"max time "<<wmax<<std::endl;
        std::cout<<"min time "<<wmin<<std::endl;
}