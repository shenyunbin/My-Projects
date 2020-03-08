#include "FastAL2.5.h"
#include "NFastAL2.6.h"

int main()
{
	FastAL<SampleBufferFormat,SAMPLE> al(SAMPLERATE, BUFFERSIZE_SEC);
	//Timer T1;
	//T1.start();
	al.run();
	//T1.stop();
	//T1.print("FastAL ");


	NFastAL<SampleBufferFormat, SAMPLE> nal(SAMPLERATE, BUFFERSIZE_SEC);
	nal.run();
}