#include "myinterface.h"
#include <iostream>

struct MyData
{
    std::array<double, 16> pose;
    double time;
};

MyData mydata;


main()
{
    MyData robot;
    robot.pose={0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    Timer T1;
    Timer T2;
    //DATASERVICE
    DataService<std::array<double, 16>, double> DS;

    double time = 0.0;
    T1.start();
    for (;;)
    {
        
        if (time == 0)
        {
            DS.Init(robot.pose, time);
            DS.run();
        }

        time = T1.gettime();
        T2.start();
        DS.UpdateSendData(robot.pose, time);
        mydata.pose = DS.RecvState();
        T2.stop();
        //std::cout <<(robot.pose[12]/*-mydata.pose[12]*/)<< " " << time <<std::endl;
        std::cout<<T2.value()<<std::endl;
        robot.pose =mydata.pose;
        
        //std::cout << " received12/14/t: " << robot.pose[12] << " " << robot.pose[14] << " " << time << std::endl;
        usleep(10000);
    }
}


/*
static pthread_mutex_t recv_mutex; //thread block

template <class T1, class T2>
class DataService
{
  public:
    struct MyState{
        T1 send_state;
        T1 recv_state;
        T2 send_time;
        T2 recv_time;
    }; 
    MyState mystate;

    bool _enabled = false;


    DataService()
    {

    }

    void Init(T1 &state, T2 &time)
    {
        mystate.send_state = state;
        mystate.recv_state = state;
        mystate.send_time = time;
        mystate.recv_time = time;
    }

    void run()
    {
        _enabled = true;
        //thread
        pthread_t thread; //thread id
        pthread_create(&thread, NULL, UDPSvr, (void *)&mystate); 
    }

    static void *UDPSvr(void *args) 
    {
        MyState *my = (MyState *)args;
        T1 _state;
        T2 _time;
        uchar8t _req;
        DataInterface DI(1);
        DI._Init(7474);
        while (1)
        {
            _req = DI.Recv(_state);
            if (_req == 1)//moving
            {
                pthread_mutex_lock(&recv_mutex);
                my->recv_state = _state;
                _time = my->send_time;
                pthread_mutex_unlock(&recv_mutex);
                DI.Send(_time);
                //std::cout << "command moving! " << _time<< std::endl;
            }
            else if (_req == 's')//start
            {
                pthread_mutex_lock(&recv_mutex);
                _state=my->send_state;
                _time=my->send_time;
                pthread_mutex_unlock(&recv_mutex);
                DI.Send(_state, _time);
                std::cout << "command start! " <<_state << _time<< std::endl;
            }
            else
            {
                std::cout << "command others!" << std::endl;
            }
            
        }
    }

    T1 RecvState()
    {
        pthread_mutex_lock(&recv_mutex);
        T1 _state = mystate.recv_state;
        pthread_mutex_unlock(&recv_mutex);
        return _state;
    }

    T2 RecvTime()
    {
        pthread_mutex_lock(&recv_mutex);
        T2 _time = mystate.recv_time;
        pthread_mutex_unlock(&recv_mutex);
        return _time;
    }

    void UpdateSendData(T1 _state, T2 _time)
    {
        pthread_mutex_lock(&recv_mutex);
        mystate.send_time = _time;
        mystate.send_state = _state;
        pthread_mutex_unlock(&recv_mutex);
    }
};
*/