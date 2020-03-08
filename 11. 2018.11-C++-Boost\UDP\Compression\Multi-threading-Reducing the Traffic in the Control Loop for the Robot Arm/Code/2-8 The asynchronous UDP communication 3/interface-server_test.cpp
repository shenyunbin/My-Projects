#include "myinterface2.h"
#include <iostream>
#include <cmath>
#include <franka/exception.h>
#include <franka/robot.h>
#include "examples_common.h"

//change time according to the period
double TimeGenerator(double &current_time, double target_time, double period)
{
    double time_interval = target_time - current_time;
    if (time_interval > period)
    {
        current_time += period;
        return 1;
    }
    else if (-time_interval > period)
    {
        current_time -= period;
        return -1;
    }
    else
    {
        return 0;
    }
}

int main()
{

    //DATASERVICE
    DataService<std::array<double, 2>, std::array<double, 16>> DS;

    std::array<double, 2> command_time;
    std::array<double, 2> current_time;
    double time_period = 0.0;
    current_time[0] = 0.0;
    current_time[1] = 0.0;
    command_time = current_time;

    double time=0.0;
    constexpr double kRadius = 0.3;
    std::array<double, 16> initial_pose={0};
    Timer T1;
    for (;;)
    {
        if (time == 0.0)
        {
            //initial_pose = robot_state.O_T_EE_c;
            DS.Init(current_time, initial_pose);
            DS.run();
        }
        
        T1.start();
        time_period = 0.001;
        time += time_period;
        
        command_time = DS.RecvCommand();
        //std::array<double, 2> command_feedback;

        TimeGenerator(current_time[0], command_time[0], time_period);
        TimeGenerator(current_time[1], command_time[1], time_period);

        double angle_x = M_PI / 4 * (1 - std::cos(M_PI / 5.0 * current_time[0]));
        double angle_z = M_PI / 4 * (1 - std::cos(M_PI / 5.0 * current_time[1]));
        double delta_x = kRadius * std::sin(angle_x);
        double delta_z = kRadius * (std::cos(angle_z) - 1);

        std::array<double, 16> new_pose = initial_pose;
        new_pose[12] += delta_x;
        new_pose[14] += delta_z;
        
        DS.UpdateSendData(current_time, initial_pose);
        T1.stop();
        //std::cout<<T1.value()<<std::endl;
    }

    return 0;
}