//Version 16.01.2019

//udp socket
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <string.h>

#include "robot_state_test.h" //only for test
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

    //crc check enabled
    bool crc_en = true;

    //set crc enable
    void CRCEnabled(bool bol)
    {
        crc_en = bol;
    }

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
        if (!crc_en)
            return 0;
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

    //compress enabled
    bool compress_en = true;

    //set compress enable
    void CompressEnabled(bool bol)
    {
        compress_en = bol;
    }

    //RleCompress
    ushort16t RleCompress(uchar8t *src, int32t length, uchar8t *dst)
    {
        uchar8t *start = dst;
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
        ushort16t new_length = dst - start + 1;
        *(ushort16t *)(start) = (ushort16t)(new_length);
        return new_length;
    }

    //RleDeompress
    uint32t RleDecompress(uchar8t *src, ushort16t length, uchar8t *dst)
    {
        //ushort16t length=*(ushort16t*)(src);
        uchar8t *start = dst;
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
        return dst - start;
    }
};

/*
class interface

    default settings: 
        crc32: enabled
        compress: enabled
        MTU: 1000
*/
class Interface : public CRC32, public Compress, public UDPComm
{

  public:
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
#define HEADER_LEN 10
#define CRC_LEN 4
#define BUF_LEN 2500
//Largest quantity of Packet
#define MAX_PKT_NUM 16
//Definition of each Byte in Header
#define HEADER_REQ_POS 0
#define HEADER_LENGTH_POS 1
#define HEADER_GROUP_SEL_POS 3
#define HEADER_GROUP0_POS 4
#define HEADER_GROUP1_POS 5
#define HEADER_GROUP2_POS 6
#define HEADER_GROUP3_POS 7
#define HEADER_GROUP4_POS 8
#define HEADER_GROUP5_POS 9

    //send_buf Header 16*10 0-9 0Packetcount 12Datalength 3Groupselect 4-9Groupdataselect
    uchar8t senddata_header[MAX_PKT_NUM * HEADER_LEN] = {0};
    //send data buffer
    uchar8t data_buf[BUF_LEN]; //without ={0}; will faster
    //compressed data buffer
    uchar8t compress_buf[BUF_LEN]; //without ={0}; will faster
    //compressed data buffer
    uchar8t recvdata_header[HEADER_LEN]; //without ={0}; will faster
    //send data length
    uint32t senddata_length = HEADER_LEN + CRC_LEN;
    //packet count means the whole send buffer will use 1-16 packets to send
    uint32t pkt_count = 1;

    /////////////////////////////////////////////////////////////////////////////////////
    //REQUEST DEFINITION FOR HEADER EDITION//////////////////////////////////////////////

    //request of the packet
    struct REQUEST
    {
        const uchar8t PKT_START = 0x30;  //0011----
        const uchar8t PKT_MORMAL = 0x60; //0110----
        const uchar8t PKT_END = 0xC0;    //1100----
        const uchar8t GET_PKT_NUM = 0x0F;
        const uchar8t GET_REQ = 0xF0;
        const uchar8t PKT_ERROR = 0x90; //1001----
        const uchar8t PKT_OK = 0xF0;    //1111----
    };

    REQUEST req;

    /////////////////////////////////////////////////////////////////////////////////////
    //ROBOT_STATE DEFINITION FOR HEADER EDITION//////////////////////////////////////////

    struct ROBOT_STATE
    {
        //GROUP0
        const uchar8t O_T_EE = 0;
        const uchar8t O_T_EE_d = 1;
        const uchar8t F_T_EE = 2;
        const uchar8t EE_T_K = 3;
        const uchar8t m_ee = 4;
        const uchar8t I_ee = 5;
        const uchar8t F_x_Cee = 6;
        const uchar8t m_load = 7;
        //GROUP1;
        const uchar8t I_load = 8;
        const uchar8t F_x_Cload = 9;
        const uchar8t m_total = 10;
        const uchar8t I_total = 11;
        const uchar8t F_x_Ctotal = 12;
        const uchar8t elbow = 13;
        const uchar8t elbow_d = 14;
        const uchar8t elbow_c = 15;
        //GROUP2;
        const uchar8t delbow_c = 16;
        const uchar8t ddelbow_c = 17;
        const uchar8t tau_J = 18;
        const uchar8t tau_J_d = 19;
        const uchar8t dtau_J = 20;
        const uchar8t q = 21;
        const uchar8t q_d = 22;
        const uchar8t dq = 23;
        //GROUP3;
        const uchar8t dq_d = 24;
        const uchar8t ddq_d = 25;
        const uchar8t joint_contact = 26;
        const uchar8t cartesian_contact = 27;
        const uchar8t joint_collision = 28;
        const uchar8t cartesian_collision = 29;
        const uchar8t tau_ext_hat_filtered = 30;
        const uchar8t O_F_ext_hat_K = 31;
        //GROUP4;
        const uchar8t K_F_ext_hat_K = 32;
        const uchar8t O_dP_EE_d = 33;
        const uchar8t O_T_EE_c = 34;
        const uchar8t O_dP_EE_c = 35;
        const uchar8t O_ddP_EE_c = 36;
        const uchar8t theta = 37;
        const uchar8t dtheta = 38;
        const uchar8t current_errors = 39;
        //GROUP5;
        const uchar8t last_motion_errors = 40;
        const uchar8t control_command_success_rate = 41;
        const uchar8t robot_mode = 42;
        const uchar8t time = 43;
        const uchar8t UNDEF1 = 44;
        const uchar8t UNDEF2 = 45;
        const uchar8t UNDEF3 = 46;
        const uchar8t UNDEF4 = 47;
    };

    ROBOT_STATE state;

    //storage size for each state
    const uchar8t state_size[48] = {
        128, 128, 128, 128, 8, 72, 24, 8,
        72, 24, 8, 72, 24, 16, 16, 16,
        16, 16, 56, 56, 56, 56, 56, 56,
        56, 56, 56, 48, 56, 48, 56, 48,
        48, 48, 128, 48, 48, 56, 56, 0 /*37*/,
        0 /*37*/, 8, 4 /*4*/, 0 /*8*/, 0, 0, 0, 0};

    /////////////////////////////////////////////////////////////////////////////////////
    //MTU DEFINITION FOR PACKET DIVISION/////////////////////////////////////////////////

    //MTU
    uint32t MTU = 1000;

    //set MTU
    bool SetMTU(uint32t mtu_length)
    {
        if (mtu_length < 200)
            return false;
        else
        {
            MTU = mtu_length;
            return true;
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //BUFFER INITIALIZATION//////////////////////////////////////////////////////////////

    void BufInit()
    {
        memset(data_buf, 0, sizeof(data_buf));
        memset(compress_buf, 0, sizeof(compress_buf));
        memset(recvdata_header, 0, sizeof(recvdata_header));
    }

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
    uchar8t *mStrCpy(uchar8t *&pre, uint32t copy_length, const uchar8t *next)
    {
        for (uint32t i = 0; i < copy_length; i++)
            *pre++ = *next++;
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
    //HEADER EDITION/////////////////////////////////////////////////////////////////////

    //request of the packet for send
    uchar8t _req = req.PKT_MORMAL;

    //set the request
    void SetReq(uchar8t request)
    {
        _req = request;
    }

    //header edition --- edit datatype to send
    void InitDatatypeToSend()
    {
        for (uint32t i = 3; i < (HEADER_LEN * MAX_PKT_NUM); i++)
            senddata_header[i] = '\0';
        senddata_length = HEADER_LEN + CRC_LEN; //10Header+32CRC
        pkt_count = 1;
    }

    //header edition --- add the determinate datatype to send
    uint32t AddDatatypeToSend(const uchar8t datatype)
    {
        senddata_length += (uint32t)state_size[datatype];
        if (senddata_length > (MTU * pkt_count))
        {
            senddata_length = MTU * pkt_count + state_size[datatype] + HEADER_LEN + CRC_LEN;
            pkt_count++;
        }
        const uchar8t BIT[8] = {0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80};
        uint32t num = HEADER_GROUP_SEL_POS + pkt_count * HEADER_LEN - HEADER_LEN; //set location at 3 --- Group select byte
        uint32t group_sel = datatype / 8;                                         //group select
        uint32t data_sel = datatype % 8;                                          //select data in determinate group
        senddata_header[num] = senddata_header[num] | BIT[group_sel];
        ++num += group_sel;
        senddata_header[num] = senddata_header[num] | BIT[data_sel];
        return pkt_count;
    }

    //header edition --- add the determinate datatype to send (binary mode)
    uint32t AddDatatypeToSend(const uchar8t *datatype, uint32t datatype_count)
    {
        for (uint32t i = 0; i < datatype_count; i++)
            AddDatatypeToSend(*datatype++);
        return pkt_count;
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //SENDDATA EDITION///////////////////////////////////////////////////////////////////

    //get send data group by group
    uchar8t *GetSenddataGroup0(uchar8t *send_buf, uchar8t *&send_buf_sel, const franka::RobotState &robot_state)
    {
        uchar8t data_sel = send_buf[HEADER_GROUP0_POS]; //Group0 dataselect
        if (data_sel & BIT1)
            DataToStr(robot_state.O_T_EE, send_buf_sel);
        if (data_sel & BIT2)
            DataToStr(robot_state.O_T_EE_d, send_buf_sel);
        if (data_sel & BIT3)
            DataToStr(robot_state.F_T_EE, send_buf_sel);
        if (data_sel & BIT4)
            DataToStr(robot_state.EE_T_K, send_buf_sel);
        if (data_sel & BIT5)
            DataToStr(robot_state.m_ee, send_buf_sel);
        if (data_sel & BIT6)
            DataToStr(robot_state.I_ee, send_buf_sel);
        if (data_sel & BIT7)
            DataToStr(robot_state.F_x_Cee, send_buf_sel);
        if (data_sel & BIT8)
            DataToStr(robot_state.m_load, send_buf_sel);
        return send_buf_sel;
    }

    uchar8t *GetSenddataGroup1(uchar8t *send_buf, uchar8t *&send_buf_sel, const franka::RobotState &robot_state)
    {
        uchar8t data_sel = send_buf[HEADER_GROUP1_POS]; //Group1 dataselect
        if (data_sel & BIT1)
            DataToStr(robot_state.I_load, send_buf_sel);
        if (data_sel & BIT2)
            DataToStr(robot_state.F_x_Cload, send_buf_sel);
        if (data_sel & BIT3)
            DataToStr(robot_state.m_total, send_buf_sel);
        if (data_sel & BIT4)
            DataToStr(robot_state.I_total, send_buf_sel);
        if (data_sel & BIT5)
            DataToStr(robot_state.F_x_Ctotal, send_buf_sel);
        if (data_sel & BIT6)
            DataToStr(robot_state.elbow, send_buf_sel);
        if (data_sel & BIT7)
            DataToStr(robot_state.elbow_d, send_buf_sel);
        if (data_sel & BIT8)
            DataToStr(robot_state.elbow_c, send_buf_sel);
        return send_buf_sel;
    }

    uchar8t *GetSenddataGroup2(uchar8t *send_buf, uchar8t *&send_buf_sel, const franka::RobotState &robot_state)
    {
        uchar8t data_sel = send_buf[HEADER_GROUP2_POS]; //Group2 dataselect
        if (data_sel & BIT1)
            DataToStr(robot_state.delbow_c, send_buf_sel);
        if (data_sel & BIT2)
            DataToStr(robot_state.ddelbow_c, send_buf_sel);
        if (data_sel & BIT3)
            DataToStr(robot_state.tau_J, send_buf_sel);
        if (data_sel & BIT4)
            DataToStr(robot_state.tau_J_d, send_buf_sel);
        if (data_sel & BIT5)
            DataToStr(robot_state.dtau_J, send_buf_sel);
        if (data_sel & BIT6)
            DataToStr(robot_state.q, send_buf_sel);
        if (data_sel & BIT7)
            DataToStr(robot_state.q_d, send_buf_sel);
        if (data_sel & BIT8)
            DataToStr(robot_state.dq, send_buf_sel);
        return send_buf_sel;
    }

    uchar8t *GetSenddataGroup3(uchar8t *send_buf, uchar8t *&send_buf_sel, const franka::RobotState &robot_state)
    {
        uchar8t data_sel = send_buf[HEADER_GROUP3_POS]; //Group3 dataselect
        if (data_sel & BIT1)
            DataToStr(robot_state.dq_d, send_buf_sel);
        if (data_sel & BIT2)
            DataToStr(robot_state.ddq_d, send_buf_sel);
        if (data_sel & BIT3)
            DataToStr(robot_state.joint_contact, send_buf_sel);
        if (data_sel & BIT4)
            DataToStr(robot_state.cartesian_contact, send_buf_sel);
        if (data_sel & BIT5)
            DataToStr(robot_state.joint_collision, send_buf_sel);
        if (data_sel & BIT6)
            DataToStr(robot_state.cartesian_collision, send_buf_sel);
        if (data_sel & BIT7)
            DataToStr(robot_state.tau_ext_hat_filtered, send_buf_sel);
        if (data_sel & BIT8)
            DataToStr(robot_state.O_F_ext_hat_K, send_buf_sel);
        return send_buf_sel;
    }

    uchar8t *GetSenddataGroup4(uchar8t *send_buf, uchar8t *&send_buf_sel, const franka::RobotState &robot_state)
    {
        uchar8t data_sel = send_buf[HEADER_GROUP4_POS]; //Group4 dataselect
        if (data_sel & BIT1)
            DataToStr(robot_state.K_F_ext_hat_K, send_buf_sel);
        if (data_sel & BIT2)
            DataToStr(robot_state.O_dP_EE_d, send_buf_sel);
        if (data_sel & BIT3)
            DataToStr(robot_state.O_T_EE_c, send_buf_sel);
        if (data_sel & BIT4)
            DataToStr(robot_state.O_dP_EE_c, send_buf_sel);
        if (data_sel & BIT5)
            DataToStr(robot_state.O_ddP_EE_c, send_buf_sel);
        if (data_sel & BIT6)
            DataToStr(robot_state.theta, send_buf_sel);
        if (data_sel & BIT7)
            DataToStr(robot_state.dtheta, send_buf_sel);
        //if(data_sel&BIT8)DataToStr(robot_state.current_errors,send_buf_sel);
        return send_buf_sel;
    }

    uchar8t *GetSenddataGroup5(uchar8t *send_buf, uchar8t *&send_buf_sel, const franka::RobotState &robot_state)
    {
        uchar8t data_sel = send_buf[HEADER_GROUP5_POS]; //Group5 dataselect
        //if(data_sel&BIT1)DataToStr(robot_state.last_motion_errors,send_buf_sel);
        if (data_sel & BIT2)
            DataToStr(robot_state.control_command_success_rate, send_buf_sel);
        if (data_sel & BIT3)
            DataToStr(robot_state.robot_mode, send_buf_sel);
        //if(data_sel&BIT4)DataToStr(robot_state.time,send_buf_sel);
        return send_buf_sel;
    }

    //generate the senddata
    uint32t SenddataToSendbuf(uchar8t send_pkt_num, const franka::RobotState &robot_state, uchar8t *&str)
    {
        uchar8t *send_buf = data_buf; //+(send_pkt_num*MTU);//(uchar8t*) malloc(MTU);
        send_buf[HEADER_REQ_POS] = _req | send_pkt_num;
        uint32t header_pos = HEADER_LEN * send_pkt_num + HEADER_GROUP_SEL_POS;
        for (uint32t i = HEADER_GROUP_SEL_POS; i < HEADER_LEN; i++)
            send_buf[i] = senddata_header[header_pos++];
        uchar8t *send_buf_sel = send_buf + HEADER_LEN;
        uchar8t group_sel = send_buf[HEADER_GROUP_SEL_POS];
        if (group_sel & BIT1)
            GetSenddataGroup0(send_buf, send_buf_sel, robot_state);
        if (group_sel & BIT2)
            GetSenddataGroup1(send_buf, send_buf_sel, robot_state);
        if (group_sel & BIT3)
            GetSenddataGroup2(send_buf, send_buf_sel, robot_state);
        if (group_sel & BIT4)
            GetSenddataGroup3(send_buf, send_buf_sel, robot_state);
        if (group_sel & BIT5)
            GetSenddataGroup4(send_buf, send_buf_sel, robot_state);
        if (group_sel & BIT6)
            GetSenddataGroup5(send_buf, send_buf_sel, robot_state);
        ushort16t length = send_buf_sel - send_buf;
        *(ushort16t *)(send_buf + HEADER_LENGTH_POS) = (ushort16t)(length);
        DataAddCheck(send_buf, length);
        length += CRC_LEN;
        if (compress_en)
        {
            length = RleCompress(send_buf, length, compress_buf);
            str = compress_buf;
        }
        else
        {
            str = send_buf;
        }
        return length;
    }

    //generate the senddata2
    uint32t StateDirectToSendbuf(const franka::RobotState &robot_state, uchar8t *&str)
    {
        uchar8t *send_buf = data_buf;
        send_buf[HEADER_REQ_POS] = _req;
        uchar8t *send_buf_sel = send_buf + HEADER_LEN;
        DataToStr(robot_state, send_buf_sel);
        ushort16t length = send_buf_sel - send_buf;
        *(ushort16t *)(send_buf + HEADER_LENGTH_POS) = (ushort16t)(length);
        DataAddCheck(send_buf, length);
        length += CRC_LEN;
        if (compress_en)
        {
            length = RleCompress(send_buf, length, compress_buf);
            str = compress_buf;
        }
        else
        {
            str = send_buf;
        }
        return length;
    }

    /////////////////////////////////////////////////////////////////////////////////////
    //RECVDATA CONVERSION////////////////////////////////////////////////////////////////

    //get request form recv_buf
    uchar8t ReqOfRecvbuf()
    {
        return recvdata_header[HEADER_REQ_POS] & req.GET_REQ;
    }

    //get paket nummer of this packet form recv_buf
    uchar8t PktnumOfRecvbuf()
    {
        return recvdata_header[HEADER_REQ_POS] & req.GET_PKT_NUM;
    }

    //get received data from recv_buf group by group
    uchar8t *GetRecvdataGroup0(uchar8t *recvbuf, uchar8t *&recvbuf_sel, franka::RobotState &robot_state)
    {
        uchar8t data_sel = recvbuf[HEADER_GROUP0_POS]; //Group0 dataselect
        if (data_sel & BIT1)
            StrToData(recvbuf_sel, robot_state.O_T_EE);
        if (data_sel & BIT2)
            StrToData(recvbuf_sel, robot_state.O_T_EE_d);
        if (data_sel & BIT3)
            StrToData(recvbuf_sel, robot_state.F_T_EE);
        if (data_sel & BIT4)
            StrToData(recvbuf_sel, robot_state.EE_T_K);
        if (data_sel & BIT5)
            StrToData(recvbuf_sel, robot_state.m_ee);
        if (data_sel & BIT6)
            StrToData(recvbuf_sel, robot_state.I_ee);
        if (data_sel & BIT7)
            StrToData(recvbuf_sel, robot_state.F_x_Cee);
        if (data_sel & BIT8)
            StrToData(recvbuf_sel, robot_state.m_load);
        return recvbuf_sel;
    }

    uchar8t *GetRecvdataGroup1(uchar8t *recvbuf, uchar8t *&recvbuf_sel, franka::RobotState &robot_state)
    {
        uchar8t data_sel = recvbuf[HEADER_GROUP1_POS]; //Group1 dataselect
        if (data_sel & BIT1)
            StrToData(recvbuf_sel, robot_state.I_load);
        if (data_sel & BIT2)
            StrToData(recvbuf_sel, robot_state.F_x_Cload);
        if (data_sel & BIT3)
            StrToData(recvbuf_sel, robot_state.m_total);
        if (data_sel & BIT4)
            StrToData(recvbuf_sel, robot_state.I_total);
        if (data_sel & BIT5)
            StrToData(recvbuf_sel, robot_state.F_x_Ctotal);
        if (data_sel & BIT6)
            StrToData(recvbuf_sel, robot_state.elbow);
        if (data_sel & BIT7)
            StrToData(recvbuf_sel, robot_state.elbow_d);
        if (data_sel & BIT8)
            StrToData(recvbuf_sel, robot_state.elbow_c);
        return recvbuf_sel;
    }

    uchar8t *GetRecvdataGroup2(uchar8t *recvbuf, uchar8t *&recvbuf_sel, franka::RobotState &robot_state)
    {
        uchar8t data_sel = recvbuf[HEADER_GROUP2_POS]; //Group2 dataselect
        if (data_sel & BIT1)
            StrToData(recvbuf_sel, robot_state.delbow_c);
        if (data_sel & BIT2)
            StrToData(recvbuf_sel, robot_state.ddelbow_c);
        if (data_sel & BIT3)
            StrToData(recvbuf_sel, robot_state.tau_J);
        if (data_sel & BIT4)
            StrToData(recvbuf_sel, robot_state.tau_J_d);
        if (data_sel & BIT5)
            StrToData(recvbuf_sel, robot_state.dtau_J);
        if (data_sel & BIT6)
            StrToData(recvbuf_sel, robot_state.q);
        if (data_sel & BIT7)
            StrToData(recvbuf_sel, robot_state.q_d);
        if (data_sel & BIT8)
            StrToData(recvbuf_sel, robot_state.dq);
        return recvbuf_sel;
    }

    uchar8t *GetRecvdataGroup3(uchar8t *recvbuf, uchar8t *&recvbuf_sel, franka::RobotState &robot_state)
    {
        uchar8t data_sel = recvbuf[HEADER_GROUP3_POS]; //Group3 dataselect
        if (data_sel & BIT1)
            StrToData(recvbuf_sel, robot_state.dq_d);
        if (data_sel & BIT2)
            StrToData(recvbuf_sel, robot_state.ddq_d);
        if (data_sel & BIT3)
            StrToData(recvbuf_sel, robot_state.joint_contact);
        if (data_sel & BIT4)
            StrToData(recvbuf_sel, robot_state.cartesian_contact);
        if (data_sel & BIT5)
            StrToData(recvbuf_sel, robot_state.joint_collision);
        if (data_sel & BIT6)
            StrToData(recvbuf_sel, robot_state.cartesian_collision);
        if (data_sel & BIT7)
            StrToData(recvbuf_sel, robot_state.tau_ext_hat_filtered);
        if (data_sel & BIT8)
            StrToData(recvbuf_sel, robot_state.O_F_ext_hat_K);
        return recvbuf_sel;
    }

    uchar8t *GetRecvdataGroup4(uchar8t *recvbuf, uchar8t *&recvbuf_sel, franka::RobotState &robot_state)
    {
        uchar8t data_sel = recvbuf[HEADER_GROUP4_POS]; //Group4 dataselect
        if (data_sel & BIT1)
            StrToData(recvbuf_sel, robot_state.K_F_ext_hat_K);
        if (data_sel & BIT2)
            StrToData(recvbuf_sel, robot_state.O_dP_EE_d);
        if (data_sel & BIT3)
            StrToData(recvbuf_sel, robot_state.O_T_EE_c);
        if (data_sel & BIT4)
            StrToData(recvbuf_sel, robot_state.O_dP_EE_c);
        if (data_sel & BIT5)
            StrToData(recvbuf_sel, robot_state.O_ddP_EE_c);
        if (data_sel & BIT6)
            StrToData(recvbuf_sel, robot_state.theta);
        if (data_sel & BIT7)
            StrToData(recvbuf_sel, robot_state.dtheta);
        //if(data_sel&BIT8)StrToData(recvbuf_sel,robot_state.current_errors);
        return recvbuf_sel;
    }

    uchar8t *GetRecvdataGroup5(uchar8t *recvbuf, uchar8t *&recvbuf_sel, franka::RobotState &robot_state)
    {
        uchar8t data_sel = recvbuf[HEADER_GROUP5_POS]; //Group5 dataselect
        //if(data_sel&BIT1)StrToData(recvbuf_sel,robot_state.last_motion_errors);
        if (data_sel & BIT2)
            StrToData(recvbuf_sel, robot_state.control_command_success_rate);
        if (data_sel & BIT3)
            StrToData(recvbuf_sel, robot_state.robot_mode);
        //if(data_sel&BIT4)StrToData(recvbuf_sel,robot_state.time);
        return recvbuf_sel;
    }

    //get recvived data from recv_buf
    bool RecvbufToRecvdata(uchar8t *recvdata, franka::RobotState &robot_state)
    {
        uchar8t *recvbuf = recvdata;
        ushort16t length;
        if (compress_en)
        {
            length = *(ushort16t *)(recvbuf);
            if (length > MTU)
                return false;
            RleDecompress(recvbuf, length, data_buf);
            recvbuf = data_buf;
        }
        length = *(ushort16t *)(recvbuf + HEADER_LENGTH_POS);
        if (MakeCRC(recvbuf, length + CRC_LEN))
            return false;
        for (int i = 0; i < HEADER_LEN; i++)
            recvdata_header[i] = recvbuf[i];
        uchar8t group_sel = recvbuf[HEADER_GROUP_SEL_POS];
        uchar8t *recvbuf_sel = recvbuf + HEADER_LEN;
        if (group_sel & BIT1)
            GetRecvdataGroup0(recvbuf, recvbuf_sel, robot_state);
        if (group_sel & BIT2)
            GetRecvdataGroup1(recvbuf, recvbuf_sel, robot_state);
        if (group_sel & BIT3)
            GetRecvdataGroup2(recvbuf, recvbuf_sel, robot_state);
        if (group_sel & BIT4)
            GetRecvdataGroup3(recvbuf, recvbuf_sel, robot_state);
        if (group_sel & BIT5)
            GetRecvdataGroup4(recvbuf, recvbuf_sel, robot_state);
        if (group_sel & BIT6)
            GetRecvdataGroup5(recvbuf, recvbuf_sel, robot_state);
        return true;
    }

    //get the whole Robotstate from recv_buf
    bool RecvbufDirectToState(uchar8t *recvdata, franka::RobotState &robot_state)
    {
        uchar8t *recvbuf = recvdata;
        ushort16t length;
        if (compress_en)
        {
            length = *(ushort16t *)(recvbuf);
            if (length > MTU)
                return false;
            RleDecompress(recvbuf, length, data_buf);
            recvbuf = data_buf;
        }
        length = *(ushort16t *)(recvbuf + HEADER_LENGTH_POS);
        if (MakeCRC(recvbuf, length + CRC_LEN))
            return false;
        recvdata_header[HEADER_REQ_POS] = recvbuf[HEADER_REQ_POS];
        uchar8t *recvbuf_sel = recvbuf + HEADER_LEN;
        StrToData(recvbuf_sel, robot_state);
        return true;
    }
};
