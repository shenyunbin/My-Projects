#include "FastAL2.5.h"
#include "NFastAL2.6.h"
#include "XAL1.h"
#include "XAL2.h"
#include "XAL3.h"
#include "TestAL.h"
int main()
{
	//FastAL<SampleBufferFormat,SAMPLE> al(SAMPLERATE, BUFFERSIZE_SEC);
	//Timer T1;
	//T1.start();
	//al.run();
	//T1.stop();
	//T1.print("FastAL ");


	//NFastAL<SampleBufferFormat, SAMPLE> nal(SAMPLERATE, BUFFERSIZE_SEC);
	//nal.run();
	XAL1<SampleBufferFormat, SAMPLE> xal1(SAMPLERATE, BUFFERSIZE_SEC);
	xal1.run();
	XAL2<SampleBufferFormat, SAMPLE> xal2(SAMPLERATE, BUFFERSIZE_SEC);
	xal2.run();
	XAL3<SampleBufferFormat, SAMPLE> xal3(SAMPLERATE, BUFFERSIZE_SEC);
	xal3.run();

	TestAL<SampleBufferFormat, SAMPLE> testal3(SAMPLERATE, BUFFERSIZE_SEC);
	testal3.run();

}