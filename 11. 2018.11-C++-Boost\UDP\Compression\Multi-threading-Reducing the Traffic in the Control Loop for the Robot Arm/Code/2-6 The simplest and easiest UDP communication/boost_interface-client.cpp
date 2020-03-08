#include "boost/asio.hpp"  

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

		my.time=0.0;
		my.pose={0};
		io_service ios;
		ip::udp::endpoint send_ep(ip::address::from_string("192.168.1.99"),7478);
		ip::udp::socket sock(ios);
		sock.open(ip::udp::v4());
		Timer T1;
		double wt=0;
		int count=0;

        double time;
        constexpr double kRadius = 0.3;
        std::array<double, 16> initial_pose;
        robot.control([&sock,&send_ep](const franka::RobotState &robot_state,
                                 franka::Duration period) -> franka::CartesianPose {

			my.time+=period.toSec();

            if (my.time == 0.0)
            {
                my.pose = robot_state.O_T_EE_c;
            }

			sock.send_to(buffer((uchar8t*)&my,sizeof(my)),send_ep);
		
			ip::udp::endpoint ep;
			sock.receive_from(buffer((uchar8t*)&my,sizeof(my)),ep);

            return my.pose;
        });
    }
    catch (const franka::Exception &e)
    {
        std::cout << e.what() << std::endl;
        return -1;
    }

    return 0;
}