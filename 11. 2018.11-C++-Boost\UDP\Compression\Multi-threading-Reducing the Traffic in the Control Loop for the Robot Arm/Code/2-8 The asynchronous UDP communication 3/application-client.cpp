#include "myinterface2.h"
#include <iostream>
#include <cmath>

char8t *REMOTE_IPADDRESS; //"172.16.16.221"
int32t REMOTE_UDP_PORT = 7474;

struct MyData
{
    std::array<double, 2> command;
    std::array<double, 16>  feedback;
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
        if (DI.Recv(mydata_init.command,mydata_init.feedback)==SdMod.RInfo)
        {
            mydata=mydata_init;
            for (;;)
            {
                //T1.start();
                //std::cout <<(mydata.command[0]/*-mydata_init.pose[12]*/)<< " x/z " <<mydata.command[1] <<" t: "<<time <<std::endl;
                DI.Send(SdMod.Move_Return,mydata.command);
                DI.Recv(mydata.feedback);
                //T1.start();

                //time=mydata.time-mydata_init.time;
                time+=0.0005;
                //usleep(1000);
                //while (T1.current_value() < 1000);
                if (time > 10)
                {
                    //DI.SendCmd(SdMod.End);
                    break;
                }
                mydata.command[0]=time;
                mydata.command[1]=time;

            }
        }
    }



}