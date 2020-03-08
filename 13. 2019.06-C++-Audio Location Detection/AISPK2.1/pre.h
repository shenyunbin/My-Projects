#pragma once

//timer
#include <chrono>


#define SAMPLERATE 96000.0
#define BUFFERSIZE_SEC 1

typedef float SAMPLE; /* SAMPLEs in format SAMPLE */
typedef struct
{
	SAMPLE* ptrStimulusSamples; /* pointer to buffer "mono stimulus SAMPLEs" for output */
	SAMPLE* ptrReceivedSamples; /* pointer to buffer "stereo response SAMPLEs" from input (left, right, ....) */
	long int bufferSizeAtw; /* number of SAMPLEs in stimulus buffer */
	long int delayAtw; /* delay between audio output and input in SAMPLEs */
} SampleBufferFormat;

//JUST FOR TEST//////////////////////////////////////////////////////////////
void initAudio() {}
void startAudio(SampleBufferFormat* SampleBuffer)
{
	for (int i = 0; i < SAMPLERATE * BUFFERSIZE_SEC - 6200; i++)
	{
		SampleBuffer->ptrReceivedSamples[2 * (i + 6130)] = SampleBuffer->ptrStimulusSamples[i];
		SampleBuffer->ptrReceivedSamples[2 * (i + 6150) + 1] = SampleBuffer->ptrStimulusSamples[i];
	}
}
void closeAudio() {}
void initServo(double* currentAngle) { std::cout << "currentAngle: " << (*currentAngle * 180 / 3.1415926) << std::endl; }
//ABOVE JUST FOR TEST/////////////////////////////////////////////////////


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

	void print(const char* str)
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
		return std::chrono::duration_cast<res>(t2 - t1).count() / 1e6;
	}

	void delayus(int us_count)
	{
		t1 = clock::now();
		while (std::chrono::duration_cast<res>(clock::now() - t1).count() < us_count);
	}

private:
	typedef std::chrono::high_resolution_clock clock;
	typedef std::chrono::microseconds res;
	clock::time_point t1;
	clock::time_point t2;
};

//PRINT///////////////////////////////////////////////////////////////////
void printSampleBufferFormat(SampleBufferFormat* SampleBuffer, int start_position)
{
	const int print_length = 2000;
	const int step_length = 10;
	const int amplitude_unit = 1;
	std::cout << "ptrStimulusSamples:" << std::endl;
	for (int i = start_position; i < start_position + print_length; i += step_length)
		std::cout << std::dec << int(SampleBuffer->ptrStimulusSamples[i] / amplitude_unit) << " ";
	std::cout << std::endl << std::endl;
	std::cout << "ptrReceivedSamples left:" << std::endl;
	for (int i = start_position; i < start_position + print_length; i += step_length)
		std::cout << std::dec << int(SampleBuffer->ptrReceivedSamples[2 * i] / amplitude_unit) << " ";
	std::cout << std::endl << std::endl;
	std::cout << "ptrReceivedSamples right:" << std::endl;
	for (int i = start_position; i < start_position + print_length; i += step_length)
		std::cout << std::dec << int(SampleBuffer->ptrReceivedSamples[2 * i + 1] / amplitude_unit) << " ";
	std::cout << std::endl << std::endl;
	std::cout << "delayAtw bufferSizeAtw:" << std::endl;
	std::cout << std::dec << SampleBuffer->delayAtw << "      " << SampleBuffer->bufferSizeAtw << std::endl;
}

