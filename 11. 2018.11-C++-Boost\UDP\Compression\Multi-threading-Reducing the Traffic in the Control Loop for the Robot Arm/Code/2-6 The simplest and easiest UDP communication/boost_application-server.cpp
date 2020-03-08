
#include "boost/asio.hpp"  

//std
#include "myinterface2.h"
#include <iostream>
#include <cmath>
#include <franka/exception.h>
#include <franka/robot.h>
#include "examples_common.h"


using namespace std;  
using namespace boost::asio;  
 
struct My{
std::array<double, 16> pose;
double time;
}; 
My my;

int main()  
{
    io_service ios;  
    ip::udp::endpoint server_ep(ip::udp::v4(),7478);  
    ip::udp::socket sock(ios,server_ep);  
    constexpr double kRadius = 0.3;
    while(true)  
    {  
        //char buf[1024];  
        boost::system::error_code ec;  
        ip::udp::endpoint remote_ep;//接收远程连接进来的端点  
        sock.receive_from(buffer((uchar8t*)&my,sizeof(my)),remote_ep,0,ec);  

        double angle = M_PI / 4 * (1 - std::cos(M_PI / 5.0 * my.time));
        double delta_x = kRadius * std::sin(angle);
        double delta_z = kRadius * (std::cos(angle) - 1);

        my.pose[12] += delta_x;
        my.pose[14] += delta_z;

        sock.send_to(buffer((uchar8t*)&my,sizeof(my)),remote_ep);  
    }  
    return 0;  
} 