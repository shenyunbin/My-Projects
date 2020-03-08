//#pragma once
#include <math.h>
#include<iostream>


typedef double SAMPLE; /* SAMPLEs in format SAMPLE */

typedef struct
{ 
	SAMPLE* ptrStimulusSamples; /* pointer to buffer "mono stimulus SAMPLEs" for output */ 
	SAMPLE* ptrReceivedSamples; /* pointer to buffer "stereo response SAMPLEs" from input (left, right, ....) */ 
	long int bufferSizeAtw; /* number of SAMPLEs in stimulus buffer */ 
	long int delayAtw; /* delay between audio output and input in SAMPLEs */ 
} SampleBufferFormat;



//MAIN CLASS//////////////////////////////////////////////////////////////
template<int sample_rate, int buffersize>//BufferFormat
class AL
{
	//JUST FOR TEST//////////////////////////////////////////////////////////////
private:
	void initAudio() { std::cout << "initAudio" << std::endl; }
	void startAudio(SampleBufferFormat& SampleBuffer)
	{
		for (int i = 0; i < buffersize - 200; i++)
		{
			SampleBuffer.ptrReceivedSamples[2 * (i + 150)] = SampleBuffer.ptrStimulusSamples[i];
			SampleBuffer.ptrReceivedSamples[2 * (i + 100) + 1] = SampleBuffer.ptrStimulusSamples[i];
		}
		std::cout << "startAudio" << std::endl;
	}
	void closeAudio() { std::cout << "closeAudio" << std::endl; }
	void initServo(double* currentAngle) { std::cout << "currentAngle: " << (*currentAngle * 180 / M_PI) << std::endl; }
	//ABOVE JUST FOR TEST/////////////////////////////////////////////////////

	//DEFINATION//////////////////////////////////////////////////////////////
public:
	const double M_PI = 3.1415926;
	const double mic_distance = 0.2;
	const double speed_of_sound = 340;
	const int  stimulus_mode = 1;//0 sin, 1 square
	const long int stimulus_frequecy = 24000;
	const int cross_correlation_length = 128;
	double current_angle = 0.5 * M_PI;
	//MALLOC PTR///////////////////////////////////////////////////////////////////
	void mallocSampleBufferFormat(SampleBufferFormat& SampleBuffer)
	{
		SampleBuffer.ptrStimulusSamples = new SAMPLE[buffersize]();
		SampleBuffer.ptrReceivedSamples = new SAMPLE[2 * buffersize]();
	}
	//GENERATE STIMULUS////////////////////////////////////////////////////////
	void generateStimulus(SampleBufferFormat& SampleBuffer)
	{
		if (stimulus_mode == 0)
		{
			double delta_theta = double(stimulus_frequecy) * 2 * M_PI / sample_rate;
			for (int i = 0; i < buffersize; i++)
				SampleBuffer.ptrStimulusSamples[i] = sin(delta_theta * i);
		}
		else if (stimulus_mode == 1)
		{
			int half_period = int(0.5 * sample_rate / stimulus_frequecy);
			int period = 2 * half_period;
			for (int i = 0; i < buffersize; i++)
			{
				if ((i % period) < half_period)
					SampleBuffer.ptrStimulusSamples[i] = 1;
				else
					SampleBuffer.ptrStimulusSamples[i] = -1;
			}
		}
	}
	//DECENTRALIZATION////////////////////////////////////////////////////////
	void decentralizationSampleBufferFormat(SampleBufferFormat& SampleBuffer)
	{
		SAMPLE avr_sti, avr_rec, sum_sti = 0.0, sum_rec = 0.0;
		for (int i = 0; i < buffersize; i++)
			sum_sti += SampleBuffer.ptrStimulusSamples[i];
		for (int i = 0; i < 2 * buffersize; i++)
			sum_rec += SampleBuffer.ptrReceivedSamples[i];
		avr_sti = sum_sti / buffersize;
		avr_rec = SAMPLE(0.5 * sum_rec / buffersize);
		for (int i = 0; i < buffersize; i++)
			SampleBuffer.ptrStimulusSamples[i] -= avr_sti;
		for (int i = 0; i < 2 * buffersize; i++)
			SampleBuffer.ptrReceivedSamples[i] -= avr_rec;
	}
	//CALCULATE ANGLE////////////////////////////////////////////////////////////
	void calculateAngle(SampleBufferFormat& SampleBuffer, double& deltaAngle)
	{
		SAMPLE* cross_correlation_left = new SAMPLE[buffersize]();
		SAMPLE* cross_correlation_right = new SAMPLE[buffersize]();
		//cross_correlation
		for (int k = 0; k < buffersize - cross_correlation_length; k++)
			for (int i = 0; i < cross_correlation_length; i++)
			{
				cross_correlation_left[k] += SampleBuffer.ptrStimulusSamples[i] * SampleBuffer.ptrReceivedSamples[2 * (i + k)];
				cross_correlation_right[k] += SampleBuffer.ptrStimulusSamples[i] * SampleBuffer.ptrReceivedSamples[2 * (i + k) + 1];
			}
		//found the max correlation value
		SAMPLE max_left = 0, max_right = 0;
		for (int i = 0; i < buffersize; i++)
		{
			if (cross_correlation_left[i] > max_left)
				max_left = cross_correlation_left[i];
			if (cross_correlation_right[i] > max_right)
				max_right = cross_correlation_right[i];
		}
		//found the delay of left and right side
		int delay_left = 0, delay_right = 0;
		for (int i = 0; i < buffersize; i++)
			if (cross_correlation_left[i] > 0.8 * max_left)
			{
				delay_left = i;
				break;
			}
		for (int i = 0; i < buffersize; i++)
			if (cross_correlation_right[i] > 0.8 * max_right)
			{
				delay_right = i;
				break;
			}
		//calculate the angle
		int delta_k= delay_left - delay_right;
		deltaAngle = 0.5 * M_PI - acos(speed_of_sound * delta_k / (sample_rate * mic_distance));
	}
	void driveServo()
	{

	}
	//GET ANGLE/////////////////////////////////////////////////////////////////
	void getAngle(SampleBufferFormat& SampleBuffer, double& deltaAngle)
	{
		mallocSampleBufferFormat(SampleBuffer);
		initAudio();
		generateStimulus(SampleBuffer);
		startAudio(SampleBuffer);
		closeAudio();
		initServo(&current_angle);
		decentralizationSampleBufferFormat(SampleBuffer);
		calculateAngle(SampleBuffer, deltaAngle);
	}
};


/*
void Print(SampleBufferFormat& SampleBuffer)
{
	for (int i = 0; i < 1000; i++)
		std::cout <<std::hex<< SampleBuffer.ptrReceivedSamples[i] <<" " ;
}
		std::cout << "left:           " << std::endl;
		for (int i = 0; i < 1000; i++)
			std::cout << std::hex << cross_correlation_left[i] << " ";
		std::cout <<"right:           "<<std::endl;
		for (int i = 0; i < 1000; i++)
			std::cout << std::hex << cross_correlation_right[i] << " ";

		std::cout <<std::dec<< "delay_left: " << delay_left << std::endl;
		std::cout << "delay_right: " << delay_right << std::endl;
		std::cout << "max_left: " << max_left << std::endl;
		std::cout << "max_right: " << max_right << std::endl;
		std::cout << "delta_k: " << delta_k << std::endl;
*/

