// Copyright (c) 2017 Franka Emika GmbH
// Use of this source code is governed by the Apache-2.0 license, see LICENSE
#include <cmath>
#include <iostream>
#include <thread>
#include <franka/exception.h>
#include <franka/robot.h>

#include "examples_common.h"

#include "myinterface2.h"
/**
 * @example generate_cartesian_pose_motion.cpp
 * An example showing how to generate a Cartesian motion.
 *
 * @warning Before executing this example, make sure there is enough space in front of the robot.
 */

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

int main(int argc, char **argv)
{
    if (argc != 2)
    {
        std::cerr << "Usage: " << argv[0] << " <robot-hostname>" << std::endl;
        return -1;
    }
    UDPComm UDP;
    UDP.UDPBind(7475);
    UDP.UDPSetRemoteAdr("192.168.1.99", 7474);
    std::thread r([&] { UDPReceiver(UDP); });
    try
    {
        franka::Robot robot(argv[1]); //连接机器人 argv1为机器人地址robothostname
        setDefaultBehavior(robot);

        // First move the robot to a suitable joint configuration
        std::array<double, 7> q_goal = {{0, -M_PI_4, 0, -3 * M_PI_4, 0, M_PI_2, M_PI_4}};
        MotionGenerator motion_generator(0.5, q_goal); //根据q-goal设置机器人参数
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

        constexpr double kRadius = 0.3;
        std::array<double, 16> initial_pose;
        double time = 0.0;


//location of thread
        
        my.time = 0.0;
        double tp1, tp2;
        tp1 = 0.0;
        tp2 = 0.0;
        long int _sendcount;
        Timer T1;

        robot.control([&time, &T1, &UDP, &_sendcount, &tp1, &tp2](const franka::RobotState &robot_state,
                                                                  franka::Duration period) -> franka::CartesianPose {
            T1.start();
            tp1 = period.toSec();
            my.time += tp1;
            tp2 = tp1;

            if (my.time == 0.0)
            {
                my.pose = robot_state.O_T_EE_c;
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

            return my.pose;
        });
    }
    catch (const franka::Exception &e)
    {
        std::cout << e.what() << std::endl;
        return -1;
    }
    r.join();
    return 0;
}
