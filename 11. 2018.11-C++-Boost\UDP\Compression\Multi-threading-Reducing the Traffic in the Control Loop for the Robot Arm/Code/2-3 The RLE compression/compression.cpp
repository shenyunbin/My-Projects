// Sample demo for QuickLZ 1.5.x
#include <iostream>
// Remember to define QLZ_COMPRESSION_LEVEL and QLZ_STREAMING_MODE to the same values for the compressor and decompressor
#include <string>
#include <stdio.h>
#include <stdlib.h>

#include "quicklz.h"
#include "quicklz.c"
#include "interface_test.h"

#include <zlib.h>

#include <snappy.h>


#include <chrono>

class Timer
{
public:
    Timer()
        : t1(res::zero())
        , t2(res::zero())
    {
        start();
    }

    ~Timer()
    {}

    void start()
    {
        t1 = clock::now();
    }

    void stop()
    {
        t2 = clock::now();
        
    }

    void print(const char* str)
    {
        std::cout << str << " time: " 
            << std::chrono::duration_cast<res>(t2 - t1).count() / 1e3 << "ms." << std::endl;
    }

    double value()
    {
        return std::chrono::duration_cast<res>(t2 - t1).count() ;
    }

private:
    typedef std::chrono::high_resolution_clock clock;
    typedef std::chrono::microseconds res;

    clock::time_point t1;
    clock::time_point t2;
};



Compress RLE;

int main()
{

    Timer T1,T2,T3,T4;

    

    



    char *src, *dst;


    franka::RobotState robot_state;
    /*robot_state.O_T_EE_d = {0.998582, 0.0329548, -0.041575, 0, 0.0336027, -0.999313, 0.0149824, 0, -0.0410535,
                            -0.0163585, -0.999023, 0, 0.305444, -0.00810967, 0.483251, 1};*/
    robot_state.O_T_EE_d = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -0.999023, 0, -0.00810967, 0, 0};                      
    robot_state.F_T_EE = {0.7071,-0.7071,0,0,0.7071,0.7071,0,0,0,0,1,0,0,0,0.1034,1};
    Interface IF;
    IF.CompressEnabled(false);
    
    //IF.AddDatatypeToSend(IF.state.O_dP_EE_c);
    IF.AddDatatypeToSend(IF.state.F_T_EE);
    
    IF.AddDatatypeToSend(IF.state.O_T_EE_d);

    //definition for the senddata and its length
    uchar8t *sendpacket;
    size_t length;
    length = IF.SenddataToSendbuf(0, robot_state, sendpacket);

    size_t len1, len2,len3,len4;   
    dst = (char*) malloc(2500);
/*
 for(int i=0;i<246;i++){
     std::cout<<(int)(char)IF.data_buf[i]<<",";

 }
 */
    length=128;
    sendpacket=(uchar8t*)&robot_state.O_T_EE_d;


    T1.start();
	qlz_state_compress *state_compress = (qlz_state_compress *)malloc(sizeof(qlz_state_compress));
    len1 = qlz_compress(sendpacket, dst, length, state_compress);
    T1.stop();





       // original string len = 36
    char a[500];
    for(int i=0;i<length;i++){
        a[i]=*sendpacket++;
    }

    // placeholder for the compressed (deflated) version of "a" 
    char b[500]={0};

    // STEP 1.
    // deflate a into b. (that is, compress a into b)
    // zlib struct
    T2.start();
    z_stream defstream;
    defstream.zalloc = Z_NULL;
    defstream.zfree = Z_NULL;
    defstream.opaque = Z_NULL;
    // setup "a" as the input and "b" as the compressed output
    defstream.avail_in = (uInt)length; // size of input, string + terminator strlen(a)
    defstream.next_in = (Bytef *)a; // input char array
    defstream.avail_out = (uInt)sizeof(b); // size of output
    defstream.next_out = (Bytef *)b; // output char array
    
    // the actual compression work.
    deflateInit(&defstream, Z_BEST_COMPRESSION);
    deflate(&defstream, Z_FINISH);
    deflateEnd(&defstream);
    T2.stop();
    // This is one way of getting the size of the output
    len2=sizeof(b)-defstream.avail_out;
    

    length = IF.SenddataToSendbuf(0, robot_state, sendpacket);

    length=128;
    sendpacket=(uchar8t*)&robot_state.O_T_EE_d;

    std::string output;
    T3.start();
    len3=snappy::Compress((char*)sendpacket, length, &output);
    T3.stop();

    length=128;
    sendpacket=(uchar8t*)&robot_state.O_T_EE_d;
    uchar8t ddst[500];

    //IF.CompressEnabled(true);
    T4.start();
    len4=RLE.RleCompress(sendpacket, length, ddst);
    //len4=IF.SenddataToSendbuf(0, robot_state, sendpacket);
    T4.stop();


    std::cout<<"original:  "<< length <<std::endl;
    std::cout<<"quiclz:    "<<  len1 << " " << T1.value() << "us" <<std::endl; //quicklz
    std::cout<<"zlib:      "<<  len2 << " " << T2.value() << "us" <<std::endl; //zlib
    std::cout<<"snappy:    "<<  len3 << " " << T3.value() << "us" <<std::endl; //snappy
    std::cout<<"qlz:       "<<  len4 << " " << T4.value() << "us" <<std::endl; //qlz

    /*
    int num=0;
    for(int n=0;n<8;n++){
        for(int i=0;i<50;i++)std::cout<<std::hex<<(int)(uchar8t)output[num++]<<" ";
        std::cout<<std::endl;
        }
    */

}