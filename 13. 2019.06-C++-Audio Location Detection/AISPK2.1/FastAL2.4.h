#pragma once
#include <math.h>
#include<iostream>
#include"pre.h"
//MAIN CLASS//////////////////////////////////////////////////////////////
template<class SampleBufferFormat, class SAMPLE>//BufferFormat
class FastAL
{
public:
	//DEFINATION//////////////////////////////////////////////////////////////
	int sample_rate, buffersize;
	const double M_PI = 3.1415926;
	const double mic_distance = 0.2;
	const double speed_of_sound = 340;
	const int  stimulus_mode = 1;/*0 sin, 1 square*/
	const long int stimulus_frequecy = 24000;
	const int cross_correlation_length = 128;
	const double audio_threshold = 96;
	double current_angle = 0.5 * M_PI;
	//INITIALIZATION//////////////////////////////////////////////////////////////
	FastAL(const int input_sample_rate, const double input_buffersize_sec)
	{
		sample_rate = input_sample_rate;
		buffersize = input_buffersize_sec* input_sample_rate;
	}
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
				SampleBuffer.ptrStimulusSamples[i] = (i % period) < half_period ? 1 : -1;
		}
	}
	//CALCULATE ANGLE////////////////////////////////////////////////////////////
	void calculateAngle(SampleBufferFormat& SampleBuffer, double& deltaAngle)
	{
		int delay_left = 0, delay_right = 0;
		//cross_correlation
		auto findMaxCorr = [this](SampleBufferFormat& SampleBuffer, int offset, int& delay)
		{
			for (int k = 0; k < buffersize - cross_correlation_length; k++)
			{
				int cross_correlation = 0;
				for (int i = 0; i < cross_correlation_length; i++)
					cross_correlation += SampleBuffer.ptrStimulusSamples[i] * SampleBuffer.ptrReceivedSamples[2 * (i + k) + offset];
				if (cross_correlation > audio_threshold)
				{
					delay = k;
					return;
				}
			}
		};
		findMaxCorr(SampleBuffer, 0, delay_left);
		findMaxCorr(SampleBuffer, 1, delay_right);
		//calculate the angle
		int delta_k = delay_left - delay_right;
		deltaAngle = 0.5 * M_PI - acos(speed_of_sound * delta_k / (sample_rate * mic_distance));
	}
	//SERVO MOTOR///////////////////////////////////////////////////////////
	void driveServo(double& currentAngle, double& deltaAngle)
	{
		currentAngle += deltaAngle;
		if (abs(currentAngle) < M_PI)
			initServo(&currentAngle);
		else
			std::cout << "error: angle!" << std::endl;
	}
	//AUDIO LOCATION/////////////////////////////////////////////////////////////////
	void run()
	{
		std::cout << "Initialization..." << std::endl;
		SampleBufferFormat SampleBuffer;
		mallocSampleBufferFormat(SampleBuffer);
		initAudio();
		generateStimulus(SampleBuffer);
		initServo(&current_angle);
		int iter_num = 1;
		std::cout << "Please enter the program run time:" << std::endl;
		std::cin >> iter_num;
		iter_num = int(iter_num * sample_rate / buffersize);
		for (int i = 0; i < iter_num; i++)
		{
			startAudio(&SampleBuffer);
			double delta_angle = 0;
			calculateAngle(SampleBuffer, delta_angle);
			driveServo(current_angle, delta_angle);
		}
		closeAudio();
		delete[] SampleBuffer.ptrStimulusSamples;
		delete[] SampleBuffer.ptrReceivedSamples;
	}
	~FastAL()
	{
		std::cout << "Program finished..." << std::endl;
		std::cin;
	}
};


/*

		auto calculateDelay = [this](SampleBufferFormat& SampleBuffer, int offset)
		{
			for (int k = 0; k < buffersize - cross_correlation_length; k++)
			{
				int cross_correlation = 0;
				for (int i = 0; i < cross_correlation_length; i++)
					cross_correlation += SampleBuffer.ptrStimulusSamples[i] * SampleBuffer.ptrReceivedSamples[2 * (i + k) + offset];
				if (cross_correlation > audio_threshold)
					return k;
			}
		};
		int delta_k = calculateDelay(SampleBuffer, 0) - calculateDelay(SampleBuffer, 1);
		deltaAngle = 0.5 * M_PI - acos(speed_of_sound * delta_k / (sample_rate * mic_distance));
*/