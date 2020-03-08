#include "myinterface.h"
#include <iostream>
#include <cmath>

char8t *REMOTE_IPADDRESS; //"172.16.16.221"
int32t REMOTE_UDP_PORT = 7474;

struct MyData
{
    std::array<double, 16> pose;
    double time;
};

main(int argc, char **argv)
{
    if (argc != 2)
    {
        std::cerr << "Usage: " << argv[0] << " <server-ip>" << std::endl;
        return -1;
    }
    REMOTE_IPADDRESS = argv[1];


    DataInterface DI(1);

    MyData mydata;
    MyData mydata_init;
    constexpr double kRadius = 0.3;
    double time = 0.0;

    for (;;)
    {
        std::cout << " press Enter to connect the interface >>>>>> " << std::endl;
        std::cin.ignore();
        DI._Init(REMOTE_IPADDRESS, REMOTE_UDP_PORT);
        time=0.0;
        DI.SendCmd(SdMod.Info);
        Timer T1;
        if (DI.Recv(mydata_init.pose,mydata_init.time)==SdMod.RInfo)
        {
            mydata=mydata_init;
            for (;;)
            {
                T1.start();
                //std::cout <<(mydata.pose[12]/*-mydata_init.pose[12]*/)<< " x/z " <<mydata.pose[14] <<" t: "<<time <<std::endl;
                DI.Send(SdMod.Move_Return,mydata.pose);
                DI.Recv(mydata.time);
                T1.stop();

                //time=mydata.time-mydata_init.time;
                time+=0.0002;
                if (time > 10)
                {
                    //DI.SendCmd(SdMod.End);
                    break;
                }

                double angle = M_PI / 4 * (1 - std::cos(M_PI / 5.0 * time));
                double delta_x = kRadius * std::sin(angle);
                double delta_z = kRadius * (std::cos(angle) - 1);
                mydata.pose = mydata_init.pose;
                mydata.pose[12] += delta_x;
                mydata.pose[14] += delta_z;
                std::cout<<T1.value()<<std::endl;
                
            }
        }
    }



}