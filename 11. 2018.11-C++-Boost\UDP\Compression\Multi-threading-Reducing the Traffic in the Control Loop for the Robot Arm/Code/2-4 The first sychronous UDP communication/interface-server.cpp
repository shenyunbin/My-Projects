#include <iostream>

//robot
#include <cmath>
#include <franka/exception.h>
#include <franka/robot.h>
#include "examples_common.h"

//interface
#include "interface.h"

using namespace std;

int main(int argc, char **argv)
{
    if (argc != 2)
    {
        std::cerr << "Usage: " << argv[0] << " <robot-hostname>" << std::endl;
        return -1;
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //ROBOT INITIALIZATION///////////////////////////////////////////////////////////////

    franka::Robot robot(argv[1]); //connect to robot
    setDefaultBehavior(robot);
    // First move the robot to a suitable joint configuration
    std::array<double, 7> q_goal = {{0, -M_PI_4, 0, -3 * M_PI_4, 0, M_PI_2, M_PI_4}};
    MotionGenerator motion_generator(0.5, q_goal); //根据q-goal设置机器人参数
    std::cout << "WARNING: This example will move the robot! "
              << "Please make sure to have the user stop button at hand!" << std::endl
              << "Press Enter to continue..." << std::endl;
    std::cin.ignore();
    robot.control(motion_generator); //move the robot
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

    /////////////////////////////////////////////////////////////////////////////////////
    //INTERFACE INITIALIZATION///////////////////////////////////////////////////////////

    Interface IF;
    IF.UDPBind(7474);
    franka::RobotState robot_state;
    IF.AddDatatypeToSend(IF.state.O_T_EE_d);
    uchar8t *sendpacket[3];
    int length[3];

    std::cout << "server start receiving! >>>>>>" << std::endl;
    uchar8t recv_buf[1024] = {0};

    IF.UDPRecv(recv_buf, sizeof(recv_buf));
    unsigned int crc_result = 1;
    //IF.RecvbufToRecvdata((uchar8t *)recv_buf, robot_state);

    for (;;)
    {

        /////////////////////////////////////////////////////////////////////////////////////
        //MAIN LOOP//////////////////////////////////////////////////////////////////////////

        if (crc_result == 0)
        {
            IF.UDPSend(&IF.req.PKT_ERROR, 1);
            std::cout << ">> error & continue receiving >>>>>>>>>>>>" << std::endl;
            continue;
        }
        else
        {    
            cout << "received UDP data, crc_result: " << crc_result << endl;
            robot.control([&time, &initial_pose](const franka::RobotState& robot_state,
                                                franka::Duration period) -> franka::CartesianPose {
            time += period.toSec();

            if (time == 0.0) {
                initial_pose = robot_state.O_T_EE_c;
            }

            
            double angle = M_PI / 4 * (1 - std::cos(M_PI / 5.0 * time));
            double delta_x = kRadius * std::sin(angle);
            double delta_z = kRadius * (std::cos(angle) - 1);

            std::array<double, 16> new_pose = initial_pose;
            new_pose[12] += delta_x;
            new_pose[14] += delta_z;



            length[0] =IF.SenddataToSendbuf(0, robot_state, sendpacket[0]);
            IF.UDPSend(sendpacket[0], length[0]);

            //receive data//////////////////////////////////
            IF.UDPRecv(recv_buf, sizeof(recv_buf));
            unsigned int crc_result = IF.RecvbufToRecvdata((uchar8t *)recv_buf, robot_state);



            if (time >= 10.0) {
                std::cout << std::endl << "Finished motion, shutting down example" << std::endl;
                return franka::MotionFinished(new_pose);
            }
            return new_pose;
            }
            );


        }

    }
}

/*

*/