#include "myinterface2.h"
#include <iostream>
#include <cmath>
#include <franka/exception.h>
#include <franka/robot.h>
#include "examples_common.h"

//change time according to the period
double TimeGenerator(double &current_time, double target_time, double period)
{
    double time_interval=target_time-current_time;
    if(time_interval>period)
    {
        current_time+=period;
        return 1;
    }
    else if(-time_interval>period)
    {
        current_time-=period;
        return -1;
    }
    else
    {
        return 0;
    }
}


int main(int argc, char **argv)
{
    if (argc != 2)
    {
        std::cerr << "Usage: " << argv[0] << " <robot-hostname>" << std::endl;
        return -1;
    }
    try
    {
        franka::Robot robot(argv[1]); 
        setDefaultBehavior(robot);

        // First move the robot to a suitable joint configuration
        std::array<double, 7> q_goal = {{0, -M_PI_4, 0, -3 * M_PI_4, 0, M_PI_2, M_PI_4}};
        MotionGenerator motion_generator(0.5, q_goal); 
        std::cout << "WARNING: This example will move the robot! "
                  << "Please make sure to have the user stop button at hand!" << std::endl
                  << "Press Enter to continue..." << std::endl;
        std::cin.ignore();
        robot.control(motion_generator); 
        std::cout << "Finished moving to initial joint configuration." << std::endl;

        // Set additional parameters always before the control loop, NEVER in the control loop!
        // Set collision behavior.
        robot.setCollisionBehavior(
            {{20.0, 20.0, 18.0, 18.0, 16.0, 14.0, 12.0}}, {{20.0, 20.0, 18.0, 18.0, 16.0, 14.0, 12.0}},
            {{20.0, 20.0, 18.0, 18.0, 16.0, 14.0, 12.0}}, {{20.0, 20.0, 18.0, 18.0, 16.0, 14.0, 12.0}},
            {{20.0, 20.0, 20.0, 25.0, 25.0, 25.0}}, {{20.0, 20.0, 20.0, 25.0, 25.0, 25.0}},
            {{20.0, 20.0, 20.0, 25.0, 25.0, 25.0}}, {{20.0, 20.0, 20.0, 25.0, 25.0, 25.0}});

        //DATASERVICE
        DataService<std::array<double, 2>, std::array<double, 16>> DS;

        std::array<double, 2> command_time;
        std::array<double, 2> current_time;
        double time_period = 0.0;
        current_time[0]=0.0;
        current_time[1]=0.0;
        command_time=current_time;

        double time;
        constexpr double kRadius = 0.3;
        std::array<double, 16> initial_pose;
        robot.control([&time, &time_period, &command_time, &current_time, &DS, &initial_pose](const franka::RobotState &robot_state,
                                 franka::Duration period) -> franka::CartesianPose {
            time_period = period.toSec();
            time+=time_period;

            if (time == 0.0)
            {
                initial_pose = robot_state.O_T_EE_c;
                DS.Init(current_time,initial_pose);
                DS.run();
            }

            command_time=DS.RecvCommand();
            //std::array<double, 2> command_feedback;

            TimeGenerator(current_time[0],command_time[0],time_period);
            TimeGenerator(current_time[1],command_time[1],time_period);

            double angle_x = M_PI / 4 * (1 - std::cos(M_PI / 5.0 * current_time[0]));
            double angle_z = M_PI / 4 * (1 - std::cos(M_PI / 5.0 * current_time[1]));
            double delta_x = kRadius * std::sin(angle_x);
            double delta_z = kRadius * (std::cos(angle_z) - 1);

            std::array<double, 16> new_pose = initial_pose;
            new_pose[12] += delta_x;
            new_pose[14] += delta_z;


            DS.UpdateSendData(current_time,robot_state.O_T_EE_c);
            return new_pose;
        });
    }
    catch (const franka::Exception &e)
    {
        std::cout << e.what() << std::endl;
        return -1;
    }

    return 0;
}