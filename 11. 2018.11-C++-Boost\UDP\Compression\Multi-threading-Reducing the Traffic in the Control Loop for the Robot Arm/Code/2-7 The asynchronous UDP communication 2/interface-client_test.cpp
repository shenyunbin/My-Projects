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

long int sendcount = 0;

struct My
{
    std::array<double, 16> pose;
    double time;
};
My my;
My _my1;
My _my2;
My _my3;

void UDPReceiver(UDPComm &UDP)
{
    while (true)
    {
        UDP.UDPRecv((uchar8t *)&_my1, sizeof(_my1));
        pthread_mutex_lock(&recv_mutex);
        sendcount--;
        _my2.pose = _my1.pose;
        pthread_mutex_unlock(&recv_mutex);
    }
}

int main(){
        constexpr double kRadius = 0.3;
        std::array<double, 16> initial_pose;
        double time = 0.0;

        UDPComm UDP;
        UDP.UDPBind(7475);
        UDP.UDPSetRemoteAdr("192.168.1.99", 7474);

        std::thread r([&] { UDPReceiver(UDP); });
        double tp1, tp2;
        tp1 = 0.0;
        tp2 = 0.0;
        long int _sendcount;
        Timer T1;
    //std::thread r([&] { UDPReceiver(); });
    my.time=0.0;
    my.pose={0};
    double wt=0;
    double ww=0;
    double wmax=0;
    double wmin=1000;
    int count=0;
    for(;;){
        T1.start();
        tp1 = 0.001;
        my.time += tp1;
        tp2 = tp1;

        if (my.time == 0.0)
        {
            my.pose = {0};
        }

        UDP.UDPSend((uchar8t *)&my, sizeof(my));

        pthread_mutex_lock(&recv_mutex);
        sendcount++;
        pthread_mutex_unlock(&recv_mutex);

        while (T1.current_value() < 300);

        pthread_mutex_lock(&recv_mutex);
        _sendcount = sendcount;
        _my3.pose = _my2.pose;
        pthread_mutex_unlock(&recv_mutex);

        if (_sendcount == 0)
        {
            my.pose = _my3.pose;
        }
        else
        {
            my.time -= tp2;
        }

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
        r.join();
}