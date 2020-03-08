#include "boost/asio.hpp"  

#include <iostream>
#include "myinterface2.h"
using namespace std;  
using namespace boost::asio;  

struct My{
std::array<double, 16> pose;
double time;
}; 
My my;

int main()
{ 
    my.time=0.0;
    my.pose={0};
 	io_service ios;
	ip::udp::endpoint send_ep(ip::address::from_string("172.16.16.221"),7478);
	ip::udp::socket sock(ios);
	sock.open(ip::udp::v4());
    Timer T1;
    double wt=0;
    double ww=0;
    double wmax=0;
    double wmin=1000;
    int count=0;
    while(true)
    {
        //cout<<"start"<<endl;
        //cin.ignore();
        T1.start();
        my.time+=0.001;
        sock.send_to(buffer((uchar8t*)&my,sizeof(my)),send_ep);
    
        ip::udp::endpoint ep;
        sock.receive_from(buffer((uchar8t*)&my,sizeof(my)),ep);

        T1.stop();
        wt=T1.value();
        ww+=wt;
        count++;
        cout<<wt<<endl;
        if(wt>wmax)wmax=wt;
        if(wt<wmin)wmin=wt;
        
        if(count>999)break;

    }
        //cout<<my.time<<" "<<T1.value()<<endl; 
        //cout<<"receive from ip:"<<ep.address()<<endl;
        cout<<"average time "<<ww/1000<<endl;
        cout<<"max time "<<wmax<<endl;
        cout<<"min time "<<wmin<<endl;

}
