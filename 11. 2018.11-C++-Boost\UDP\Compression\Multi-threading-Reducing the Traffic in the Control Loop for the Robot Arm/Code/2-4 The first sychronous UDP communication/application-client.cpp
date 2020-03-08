//boost
#include "boost/asio.hpp"
//io
#include <iostream>
//str
#include <string>
//interface
#include "VSC4/interface.h"

using namespace std;
#define IPADDRESS "192.168.1.244"//"172.16.16.221" 
#define UDP_PORT 7474


#define REMOTE_IPADDRESS "192.168.1.244"//"172.16.16.221" 
#define REMOTE_UDP_PORT 7474


int main()
{
	//set the robot_state(for example)
	franka::RobotState robot_state;
	robot_state.O_T_EE={0.998578,0.0328747,-0.0417381,0,0.0335224,-0.999317,0.0149157,0,-0.04122,-0.016294,
             -0.999017,0,0.305468,-0.00814133,0.483198,1};
    robot_state.O_T_EE_d={0.998582,0.0329548,-0.041575,0,0.0336027,-0.999313,0.0149824,0,-0.0410535,
               -0.0163585,-0.999023,0,0.305444,-0.00810967,0.483251,1};
    robot_state.F_T_EE={0.7071,-0.7071,0,0,0.7071,0.7071,0,0,0,0,1,0,0,0,0.1034,1};
    robot_state.EE_T_K={1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1};  
    robot_state.m_ee={0.73};
    robot_state.theta={3,3,3,2,2,2,2};
	robot_state.O_F_ext_hat_K={3,5,5,7,9,2};
    robot_state.robot_mode=franka::RobotMode::kMove;
    
	Interface IF;

	//add robot_sate to send return the count of packets
	int pkt_count =IF.AddDatatypeToSend(IF.state.O_T_EE_d);


	//definition for the senddata and its length
    uchar8t *sendpacket[3];
    int length[3];
	//definition for the recv buffer	
	uchar8t recv_buf[1024] = {0};
    

	//udp connection
	boost::asio::io_service io_service;
	boost::asio::ip::udp::socket socket(io_service);
	boost::asio::ip::udp::endpoint end_point(boost::asio::ip::address::from_string(IPADDRESS), UDP_PORT);
	boost::asio::ip::udp::endpoint remote_endpoint(boost::asio::ip::address::from_string(REMOTE_IPADDRESS), REMOTE_UDP_PORT);
	socket.open(end_point.protocol());
	cout << "---------------" << endl;
	cout << "press Enter to start client! >>>>>>";
	cin.ignore();
	while (true)
	{
		try
		{
			cout << "---------------" << endl;
			cout << "press Enter to continue! >>>>>>";
			cin.ignore();
			
			/*
			//set the request of the senddata
			IF.SetReq(IF.req.PKT_START);
			*/
			//get the senddata and the length
			length[0]=IF.SenddataToSendbuf(0,robot_state,sendpacket[0]);
			//send the data
			socket.send_to(boost::asio::buffer(sendpacket[0], length[0]),remote_endpoint);




			//std::cout << "The robot state to send : " <<length[0]<< std::endl<< std::endl;

			std::cout<<robot_state<<std::endl<<std::endl<<std::endl;
			
			std::cout << "sended data length : " <<length[0]<< std::endl<< std::endl;

			std::cout << "sended data in HEX: " << std::endl<< std::endl;
			int n=0;
			for(int i=0;i<length[0];i++)std::cout<<std::hex<<int(*(sendpacket[0]+n++))<<" ";

			std::cout << std::endl<< std::endl;
			

			//recv the data to buf
			socket.receive_from(boost::asio::buffer(recv_buf, 1024),remote_endpoint);//, end_point
			/*
			std::cout << "received data transform to robot state: " << std::endl<< std::endl;
			IF.BufInit();
			//get the robotstate from recv_buf
			franka::RobotState robot_state2;
			//get the crc check result 1=right 2=error
			unsigned int crc_result=IF.RecvbufToRecvdata(recv_buf,robot_state2);
			//print the robot state
    		std::cout<<robot_state2<<std::endl<<std::endl<<std::endl;
			*/
			if(recv_buf[0]==IF.req.PKT_OK)std::cout<<"received successfully"<<std::endl<<std::endl<<std::endl;
			else if(recv_buf[0]==IF.req.PKT_ERROR)std::cout<<"received failed"<<std::endl<<std::endl<<std::endl;

		}
		catch (boost::system::system_error &e)
		{
			cout << "process failed:" << e.what() << endl;
		}
	}
}


