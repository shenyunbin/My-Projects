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

int main(){
    UDPComm UDP;
    UDP.UDPBind(7474);
    constexpr double kRadius = 0.3;
    //uchar8t recv_buf[1024];
    for(;;){
        UDP.UDPRecv((uchar8t*)&my,sizeof(my));
        double angle = M_PI / 4 * (1 - std::cos(M_PI / 5.0 * my.time));
        double delta_x = kRadius * std::sin(angle);
        double delta_z = kRadius * (std::cos(angle) - 1);
        //my.pose = my.pose;
        my.pose[12] += delta_x;
        my.pose[14] += delta_z;
        //my.time+=1;
        //std::cout<<"rv"<<std::endl;
        UDP.UDPSend((uchar8t*)&my,sizeof(my));
        //std::cout<<"sd"<<std::endl;
    }
}