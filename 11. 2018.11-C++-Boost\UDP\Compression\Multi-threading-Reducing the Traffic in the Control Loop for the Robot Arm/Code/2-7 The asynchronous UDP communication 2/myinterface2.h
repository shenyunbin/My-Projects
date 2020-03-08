//Version 30.01.2019 Strongly changed!!!!

//udp socket
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <string.h>


#include <cmath>
#include <iostream>

//threading
#include <sys/select.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <netinet/in.h>
#include <cstdlib>
#include <cstdio>
#include <cstring>
#include <pthread.h>

//timer
#include <chrono>

#include <franka/exception.h>
#include <franka/robot.h>

#include "examples_common.h"
//#include <franka/robot.h>



//type definition for different palttforms
typedef unsigned char uchar8t;
typedef char char8t; //not used
typedef unsigned short int ushort16t;
typedef unsigned int uint32t;
typedef int int32t;                    //not used
typedef unsigned long int ulongint64t; //not used
typedef long int longint64t;           //not used
typedef double double64t;







int ccount=0;
/////////////////////////////////////////////////////////////////////////////////////
//BASIC DEFINITION///////////////////////////////////////////////////////////////////

//BIT DEFINE
#define BIT1 0x01
#define BIT2 0x02
#define BIT3 0x04
#define BIT4 0x08
#define BIT5 0x10
#define BIT6 0x20
#define BIT7 0x40
#define BIT8 0x80

/////////////////////////////////////////////////////////////////////////////////////
//SEND DATA & HEADER DEFINITION//////////////////////////////////////////////////////

//Header & CRC32 & Buffer length
#define HEADER_LEN 3
#define CRC_LEN 4
#define BUF_LEN 1500

//Largest quantity of Packet
#define MAX_PKT_NUM 16
//Definition of each Byte in Header
#define HEADER_REQ_POS 0
#define HEADER_LENGTH_POS 1

struct SENDMODE
{   //do nothing
    const uchar8t Normal = 0;
    //control
    const uchar8t Move_Return = 1;
    const uchar8t Move = 2;
    //answer
    const uchar8t RInfo = 3;
    const uchar8t RState = 4;
    const uchar8t RTime = 5;
    //ask
    const uchar8t Info = 'i';
    const uchar8t State = 's';
    const uchar8t Time = 't';
    const uchar8t End = 'e';  
};

constexpr SENDMODE SdMod;





//timer
class Timer
{
  public:
    Timer()
        : t1(res::zero()), t2(res::zero())
    {
        start();
    }

    ~Timer()
    {
    }

    void start()
    {
        t1 = clock::now();
    }

    void stop()
    {
        t2 = clock::now();
    }

    void print(const char *str)
    {
        std::cout << str << " time: "
                  << std::chrono::duration_cast<res>(t2 - t1).count() / 1e3 << "ms." << std::endl;
    }

    double value()
    {
        return std::chrono::duration_cast<res>(t2 - t1).count();
    }

    double current_value()
    {
        return std::chrono::duration_cast<res>(clock::now() - t1).count();
    }

    double gettime()
    {
        t2 = clock::now();
        return std::chrono::duration_cast<res>(t2 - t1).count()/ 1e6 ;
    }

    void delayus(int us_count)
    {
        t1 = clock::now();
        while(std::chrono::duration_cast<res>(clock::now() - t1).count()<us_count);
    }

  private:
    typedef std::chrono::high_resolution_clock clock;
    typedef std::chrono::microseconds res;
    clock::time_point t1;
    clock::time_point t2;
};



//UDP communication
class UDPComm
{

  public:
    /////////////////////////////////////////////////////////////////////////////////////
    //UDP SOCKET RECEIVE & SEND//////////////////////////////////////////////////////////

    int32t localSocket;
    struct sockaddr_in localAddr;
    struct sockaddr_in remoteAddr;
    socklen_t remoteAddrLength = sizeof(remoteAddr);

    //UDP socket initialization
    UDPComm()
    {
        localSocket = socket(AF_INET, SOCK_DGRAM, 0);
        bzero(&localAddr, sizeof(localAddr));
        bzero(&remoteAddr, sizeof(remoteAddr));
    }

    //local UDP port open & bind
    bool UDPBind(int32t udp_port)
    {
        localAddr.sin_family = AF_INET;
        localAddr.sin_port = htons(udp_port);
        localAddr.sin_addr.s_addr = INADDR_ANY;
        if (bind(localSocket, (struct sockaddr *)&localAddr, sizeof(localAddr)) == -1)
            return false;
        return true;
    }

    //set remote udp address und port
    void UDPSetRemoteAdr(char8t *address, int32t udp_port)
    {
        remoteAddr.sin_family = AF_INET;
        remoteAddr.sin_port = htons(udp_port); //atoi(udp_port)
        remoteAddr.sin_addr.s_addr = inet_addr(address);
        remoteAddrLength = sizeof(remoteAddr);
    }

    //receive UDP socket from any IP
    void UDPRecv(uchar8t *recv_buf, int32t size)
    {
        recvfrom(localSocket, recv_buf, size, 0, (struct sockaddr *)&remoteAddr, &remoteAddrLength);
    }

    //send message to remote IP
    void UDPSend(const uchar8t *send_buf, int32t size)
    {
        sendto(localSocket, send_buf, size, 0, (struct sockaddr *)&remoteAddr, remoteAddrLength);
    }

    //send message to selected IP
    void UDPSendTo(const uchar8t *send_buf, int32t size, char8t *address, int32t udp_port)
    {
        remoteAddr.sin_family = AF_INET;
        remoteAddr.sin_port = htons(udp_port); //atoi(udp_port)
        remoteAddr.sin_addr.s_addr = inet_addr(address);
        remoteAddrLength = sizeof(remoteAddr);
        sendto(localSocket, send_buf, size, 0, (struct sockaddr *)&remoteAddr, remoteAddrLength);
    }
};

//class crc32 check
class CRC32
{

  public:
    /////////////////////////////////////////////////////////////////////////////////////
    //CRC DATA CHECKOUT//////////////////////////////////////////////////////////////////

    //CRC32 look up table
    uint32t crc32_table[256] = {
        0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA, 0x076DC419, 0x706AF48F, 0xE963A535,
        0x9E6495A3, 0x0EDB8832, 0x79DCB8A4, 0xE0D5E91E, 0x97D2D988, 0x09B64C2B, 0x7EB17CBD,
        0xE7B82D07, 0x90BF1D91, 0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE, 0x1ADAD47D,
        0x6DDDE4EB, 0xF4D4B551, 0x83D385C7, 0x136C9856, 0x646BA8C0, 0xFD62F97A, 0x8A65C9EC,
        0x14015C4F, 0x63066CD9, 0xFA0F3D63, 0x8D080DF5, 0x3B6E20C8, 0x4C69105E, 0xD56041E4,
        0xA2677172, 0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B, 0x35B5A8FA, 0x42B2986C,
        0xDBBBC9D6, 0xACBCF940, 0x32D86CE3, 0x45DF5C75, 0xDCD60DCF, 0xABD13D59, 0x26D930AC,
        0x51DE003A, 0xC8D75180, 0xBFD06116, 0x21B4F4B5, 0x56B3C423, 0xCFBA9599, 0xB8BDA50F,
        0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924, 0x2F6F7C87, 0x58684C11, 0xC1611DAB,
        0xB6662D3D, 0x76DC4190, 0x01DB7106, 0x98D220BC, 0xEFD5102A, 0x71B18589, 0x06B6B51F,
        0x9FBFE4A5, 0xE8B8D433, 0x7807C9A2, 0x0F00F934, 0x9609A88E, 0xE10E9818, 0x7F6A0DBB,
        0x086D3D2D, 0x91646C97, 0xE6635C01, 0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E,
        0x6C0695ED, 0x1B01A57B, 0x8208F4C1, 0xF50FC457, 0x65B0D9C6, 0x12B7E950, 0x8BBEB8EA,
        0xFCB9887C, 0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3, 0xFBD44C65, 0x4DB26158, 0x3AB551CE,
        0xA3BC0074, 0xD4BB30E2, 0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB, 0x4369E96A,
        0x346ED9FC, 0xAD678846, 0xDA60B8D0, 0x44042D73, 0x33031DE5, 0xAA0A4C5F, 0xDD0D7CC9,
        0x5005713C, 0x270241AA, 0xBE0B1010, 0xC90C2086, 0x5768B525, 0x206F85B3, 0xB966D409,
        0xCE61E49F, 0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4, 0x59B33D17, 0x2EB40D81,
        0xB7BD5C3B, 0xC0BA6CAD, 0xEDB88320, 0x9ABFB3B6, 0x03B6E20C, 0x74B1D29A, 0xEAD54739,
        0x9DD277AF, 0x04DB2615, 0x73DC1683, 0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8,
        0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1, 0xF00F9344, 0x8708A3D2, 0x1E01F268,
        0x6906C2FE, 0xF762575D, 0x806567CB, 0x196C3671, 0x6E6B06E7, 0xFED41B76, 0x89D32BE0,
        0x10DA7A5A, 0x67DD4ACC, 0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5, 0xD6D6A3E8,
        0xA1D1937E, 0x38D8C2C4, 0x4FDFF252, 0xD1BB67F1, 0xA6BC5767, 0x3FB506DD, 0x48B2364B,
        0xD80D2BDA, 0xAF0A1B4C, 0x36034AF6, 0x41047A60, 0xDF60EFC3, 0xA867DF55, 0x316E8EEF,
        0x4669BE79, 0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236, 0xCC0C7795, 0xBB0B4703,
        0x220216B9, 0x5505262F, 0xC5BA3BBE, 0xB2BD0B28, 0x2BB45A92, 0x5CB36A04, 0xC2D7FFA7,
        0xB5D0CF31, 0x2CD99E8B, 0x5BDEAE1D, 0x9B64C2B0, 0xEC63F226, 0x756AA39C, 0x026D930A,
        0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713, 0x95BF4A82, 0xE2B87A14, 0x7BB12BAE,
        0x0CB61B38, 0x92D28E9B, 0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21, 0x86D3D2D4, 0xF1D4E242,
        0x68DDB3F8, 0x1FDA836E, 0x81BE16CD, 0xF6B9265B, 0x6FB077E1, 0x18B74777, 0x88085AE6,
        0xFF0F6A70, 0x66063BCA, 0x11010B5C, 0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45,
        0xA00AE278, 0xD70DD2EE, 0x4E048354, 0x3903B3C2, 0xA7672661, 0xD06016F7, 0x4969474D,
        0x3E6E77DB, 0xAED16A4A, 0xD9D65ADC, 0x40DF0B66, 0x37D83BF0, 0xA9BCAE53, 0xDEBB9EC5,
        0x47B2CF7F, 0x30B5FFE9, 0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6, 0xBAD03605,
        0xCDD70693, 0x54DE5729, 0x23D967BF, 0xB3667A2E, 0xC4614AB8, 0x5D681B02, 0x2A6F2B94,
        0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B, 0x2D02EF8D};

    //make CRC32 table
    void MakeCRC32Table()
    {
        uint32t c;
        for (uint32t i = 0; i < 256; i++)
        {
            c = (uint32t)i;
            for (uint32t bit = 0; bit < 8; bit++)
                c & 1 ? c = (c >> 1) ^ (0xEDB88320) : c = c >> 1;
            crc32_table[i] = c;
        }
    }

    //MakeCRC_code
    uint32t MakeCRC(uchar8t *str, ushort16t size)
    {
        uint32t crc = 0xFFFFFFFF;
        while (size--)
            crc = (crc >> 8) ^ (crc32_table[(crc ^ *str++) & 0xff]);
        return crc;
    }

    //add check code to the end of string
    void DataAddCheck(uchar8t *str_start, ushort16t size)
    {
        *(uint32t *)(str_start + size) = MakeCRC(str_start, size);
    }
};

/*
class compress

    note:
        convert the char array 'ABC 00000...000 DE' to ABC 0X DE, X is the number of 0
        maximal length of char array to compress is 128*256-1=32767
*/
class Compress
{

  public:
    /////////////////////////////////////////////////////////////////////////////////////
    //RLE COMPRESS///////////////////////////////////////////////////////////////////////

    //RleCompress
    void RleCompress(const uchar8t *src, const ushort16t length, uchar8t *dst_start, ushort16t &new_length)
    {
        uchar8t *dst = dst_start;
        uint32t count = 0;
        dst += 2;
        for (int32t i = 0; i < length; i++)
        {
            if (*src == 0)
            {
                src++;
                count++; //std::cout<<std::dec<<int(count)<<std::endl;
                continue;
            }
            if (count != 0)
            {
                *dst++ = 0;
                if (count < 128)
                {
                    *dst++ = count;
                }
                else
                {
                    *dst++ = uchar8t(count % 128) | 0x80;
                    *dst++ = uchar8t(count / 128);
                }
                count = 0;
            }
            *dst++ = *src++;
        }
        if (count != 0)
        {
            *dst++ = 0;
            if (count < 128)
            {
                *dst = count;
            }
            else
            {
                *dst++ = uchar8t(count % 128) | 0x80;
                *dst++ = uchar8t(count / 128);
            }
        }
        new_length = dst - dst_start + 1; // + 1;
        *(ushort16t *)(dst_start) = (ushort16t)(new_length);
        //return new_length;
    }

    //RleDeompress
    void RleDecompress(const uchar8t *src, uchar8t *dst_start) //ushort16t length,
    {
        ushort16t length = *(ushort16t *)(src);
        uchar8t *dst = dst_start;
        ushort16t count = 0;
        src += 2;
        for (int32t i = 2; i < length; i++)
        {
            if (*src == 0)
            {
                src++;
                count = ushort16t(*(src));
                if ((*src) & 0x80)
                    count = count - 0x80 + ushort16t(*++src) * 0x80;
                while (count--)
                    *dst++ = 0;
                src++;
                continue;
            }
            *dst++ = *src++;
        }
    }
};

//
class StrProcess
{
  public:
    /////////////////////////////////////////////////////////////////////////////////////
    //DATA & STRING BASIC CONVERSION/////////////////////////////////////////////////////

    //string copy at determinate position
    uchar8t *mStrCpy(uchar8t *&pre, uint32t start_byte_position, uint32t copy_length, const uchar8t *next)
    {
        pre += start_byte_position;
        for (uint32t i = 0; i < copy_length; i++)
            *pre++ = *next++;
        *(pre) = '\0';
        return pre;
    }

    //string copy at start position
    uchar8t *mStrCpy(uchar8t *&pre, uint32t copy_length, const uchar8t *dst)
    {
        for (uint32t i = 0; i < copy_length; i++)
            *pre++ = *dst++;
        *(pre) = '\0';
        return pre;
    }

    //data to str conversion
    template <class T>
    uchar8t *StrToData(uchar8t *&str, T &data)
    {
        data = *(T *)(str);
        return str += sizeof(data);
    }

    //data to string conversion
    template <class T>
    uchar8t *DataToStr(T &data, uchar8t *&str)
    {
        return mStrCpy(str, sizeof(data), (uchar8t *)(&data));
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //AAD/GET DATA TO/FROM STRING////////////////////////////////////////////////////////

    //start position of the char array
    //uchar8t *start_position;
    uchar8t *process_position;

    //get start position of the char array
    void SPStartAt(uchar8t *start_position)
    {
        process_position = start_position;
    }

    //add data to string
    template <class T>
    uchar8t *SPAddData(T &data)
    {
        return DataToStr(data, process_position);
    }

    //get data from string
    template <class T>
    uchar8t *SPGetData(T &data)
    {
        return StrToData(process_position, data);
    }

    //get process position
    uchar8t *SPPosition()
    {
        return process_position;
    }
};

/*
class DataProcess

    default settings: 
        crc32: enabled
        compress: enabled
        MTU: 1000
*/
class DataProcess : public CRC32, public Compress, public StrProcess
{

  public:
    /////////////////////////////////////////////////////////////////////////////////////
    //SEND DATA & HEADER DEFINITION//////////////////////////////////////////////////////

    //send data buffer
    uchar8t data_buf[BUF_LEN]; //without ={0}; will faster
    //compressed data buffer
    uchar8t compress_buf[BUF_LEN]; //without ={0}; will faster

    //dataface mode 0 = slow & low traffic, 1 = normal, 2 = fast & high traffic
    uchar8t process_mode = 1;

    /////////////////////////////////////////////////////////////////////////////////////
    //DATAPROCESS SET MODE///////////////////////////////////////////////////////////////

    void SetMode(uchar8t mode)
    {
        process_mode = mode;
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //REQUEST EDITION/////////////////////////////////////////////////////////////////////

    //request of the packet for send
    uchar8t _req = SdMod.Normal;

    //set the request
    void SetReq(uchar8t request)
    {
        _req = request;
    }

    uchar8t GetReq()
    {
        return _req;
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //BUFFER INITIALIZATION//////////////////////////////////////////////////////////////

    //buffer init
    void BufInit()
    {
        memset(data_buf, 0, sizeof(data_buf));
        memset(compress_buf, 0, sizeof(compress_buf));
    }

    //data processing init
    void DPInit()
    {
        SPStartAt(data_buf + 3);
    }

    void DPFinish()
    {
        //add request 0th bit
        data_buf[HEADER_REQ_POS] = _req;
        //add length 1-2th bit
        ushort16t length = SPPosition() - data_buf;
        *(ushort16t *)(data_buf + HEADER_LENGTH_POS) = (ushort16t)(length);
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //SENDDATA EDITION///////////////////////////////////////////////////////////////////

    //get send data
    void GetSendData(uchar8t *&str, uint32t &str_length)
    {
        DPFinish();
        //get length
        ushort16t length = *(ushort16t *)(data_buf + HEADER_LENGTH_POS);
        //according to the mode add crc or compress
        switch (process_mode)
        {
        case 0: //slow & low traffic & stable
            //AddCRC
            DataAddCheck(data_buf, length);
            //compress
            ushort16t new_length;
            RleCompress(data_buf, length + CRC_LEN, compress_buf, new_length);
            str = compress_buf;
            str_length = new_length;
            break;
        case 1: //normal & high traffic & stable
            //AddCRC
            DataAddCheck(data_buf, length);
            //compress
            str = data_buf;
            str_length = length + CRC_LEN;
            break;
        default: //fast & high traffic & unstable
            str = data_buf;
            str_length = length;
        }
    }

    //get received data
    uchar8t GetRecvData(uchar8t *recvdata) //,uchar8t *&data_position
    {
        ushort16t length;
        switch (process_mode)
        {
        case 0: //slow & low traffic & stable
            //decompress
            RleDecompress(recvdata, data_buf);
            //get length & request
            _req = *data_buf;
            length = *(ushort16t *)(data_buf + HEADER_LENGTH_POS);
            //crc check
            if (MakeCRC(data_buf, length + CRC_LEN))
                return 0;
            //get data start position
            SPStartAt(data_buf + 3);
            return _req;
            break;
        case 1: //normal & high traffic & stable
            //get length & request
            _req = *recvdata;
            length = *(ushort16t *)(recvdata + HEADER_LENGTH_POS);
            //crc check
            if (MakeCRC(recvdata, length + CRC_LEN))
                return 0;
            //get data start position
            SPStartAt(recvdata + 3);
            return _req;
            break;
        default: //fast & high traffic & unstable
            _req = *recvdata;
            //get data start position
            SPStartAt(recvdata + 3);
            return _req;
            break;
        }
    }
};

//Data Interface
class DataInterface : public DataProcess, public UDPComm
{
  public:
    uchar8t recv_buf[1024] = {0};

    /////////////////////////////////////////////////////////////////////////////////////
    //INIT///////////////////////////////////////////////////////////////////////////////

    DataInterface(uchar8t mode)
    {
        SetMode(mode);
    }

    DataInterface()
    {
    }

    void Init(int32t port)
    {
        UDPBind(port);
        RecvInit();
    }

    void Init(char8t *address, int32t port)
    {
        UDPSetRemoteAdr(address, port);
        UDPBind(port+1);// for test uns stable
        SendInit();
    }

    void _Init(int32t port)
    {
        UDPBind(port);
    }

    void _Init(char8t *address, int32t port)
    {
        UDPSetRemoteAdr(address, port);
        UDPBind(port+1);// for test uns stable
    }

    void SendInit()
    {
        uchar8t a = 'A';
        UDPSend(&a, 1);
    }

    bool RecvInit()
    {
        UDPRecv(recv_buf, sizeof(recv_buf));
        if (*recv_buf=='A')
            return true;
        return RecvInit();
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //DATA SEND//////////////////////////////////////////////////////////////////////////

    void SendCmd(uchar8t request)
    {
        uint32t length;
        uchar8t *send_data;
        SetReq(request);
        DPInit();
        GetSendData(send_data, length);
        UDPSend(send_data, length);
    }

    template <class T1, class T2>
    void Send(uchar8t _req, T1 &data1, T2 &data2)
    {
        uint32t length;
        uchar8t *send_data;
        SetReq(_req);
        DPInit();
        SPAddData(data1);
        SPAddData(data2);
        GetSendData(send_data, length);
        UDPSend(send_data, length);
    }

    template <class T1>
    void Send(uchar8t _req, T1 &data1)
    {
        uint32t length;
        uchar8t *send_data;
        SetReq(_req);
        DPInit();
        SPAddData(data1);
        GetSendData(send_data, length);
        UDPSend(send_data, length);
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //DATA RECV//////////////////////////////////////////////////////////////////////////

    template <class T1, class T2>
    uchar8t Recv(T1 &data1, T2 &data2)
    {
        UDPRecv(recv_buf, sizeof(recv_buf));
        uchar8t _req=GetRecvData(recv_buf);
        if (_req>32)
            return _req;
        SPGetData(data1);
        SPGetData(data2);
        return _req;
    }

    template <class T1>
    uchar8t Recv(T1 &data1)
    {
        UDPRecv(recv_buf, sizeof(recv_buf));
        uchar8t _req=GetRecvData(recv_buf);
        if (_req>32)
            return _req;
        SPGetData(data1);
        return _req;
    }
};




//class data service
/*
#define	RUN 	1
#define STOP	0
int status = STOP;
//pthread_mutex_t send_mutex = PTHREAD_MUTEX_INITIALIZER;
pthread_cond_t cond = PTHREAD_COND_INITIALIZER;
*/
static pthread_mutex_t recv_mutex; //thread block

template <class T1, class T2>
class DataService
{
  public:
    struct MyState{
        T1 send_command;
        T1 recv_command;
        T2 send_feedback;
        T2 recv_feedback;
    }; 
    MyState mystate;

    DataService()
    {

    }

    void Init(T1 command, T2 feedback)
    {
        mystate.send_command = command;
        mystate.send_command = command;
        mystate.send_feedback = feedback;
        mystate.send_feedback = feedback;
    }

    void run()
    {
        //thread
        pthread_t thread; //thread id
        pthread_create(&thread, NULL, UDPSvr, (void *)&mystate); 
    }

    static void *UDPSvr(void *args) 
    {
        MyState *my = (MyState *)args;
        T1 _command;
        T2 _feedback;
        DataInterface DI(1);
        DI._Init(7474);
        Timer C1;
        while (1)
        {
            C1.start();
            switch (DI.Recv(_command))
            {
            case SdMod.Move_Return:
                pthread_mutex_lock(&recv_mutex);
                my->recv_command = _command;
                _feedback = my->send_feedback;
                /*
                status = STOP;//STOP
                while(!status)
                {
                    pthread_cond_wait(&cond, &recv_mutex);
                }
                */
                pthread_mutex_unlock(&recv_mutex);
                DI.Send(SdMod.RTime,_feedback);
                break;
            case SdMod.Move:
                pthread_mutex_lock(&recv_mutex);
                my->recv_command = _command;
                _feedback = my->send_feedback;
                pthread_mutex_unlock(&recv_mutex);
                break;
            case SdMod.Info: 
                pthread_mutex_lock(&recv_mutex);
                _command=my->send_command;
                _feedback=my->send_feedback;
                pthread_mutex_unlock(&recv_mutex);
                DI.Send(SdMod.RInfo,_command, _feedback);
                break;
            case SdMod.End:
                break;
            default:
                break;
            }
            C1.stop();
            std::cout<<C1.value()<<std::endl;
        }
    }

    T1 RecvCommand()
    {
        pthread_mutex_lock(&recv_mutex);
        T1 _command = mystate.recv_command;
        pthread_mutex_unlock(&recv_mutex);
        return _command;
    }

    T2 RecvFeedback()
    {
        pthread_mutex_lock(&recv_mutex);
        T2 _feedback = mystate.recv_feedback;
        pthread_mutex_unlock(&recv_mutex);
        return _feedback;
    }

    void UpdateSendData(T1 _command, T2 _feedback)
    {
        pthread_mutex_lock(&recv_mutex);
        mystate.send_feedback = _feedback;
        mystate.send_command = _command;
		//status = RUN;
		//pthread_cond_signal(&cond);
        pthread_mutex_unlock(&recv_mutex);
    }
};





/*
//change time according to the period
bool TimeGenerator(double &current_time, double target_time, double period)
{
    double time_interval=target_time-current_time;
    if(time_interval>period)
    {
        current_time+=period;
        return false;
    }
    else if(-time_interval>period)
    {
        current_time-=period;
        return false;
    }
    else
    {
        return true;
    }
}
*/