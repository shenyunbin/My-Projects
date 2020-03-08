#pragma once
#include <math.h>
#include<iostream>
#include"pre.h"
//MAIN CLASS//////////////////////////////////////////////////////////////
template<class SampleBufferFormat, class SAMPLE>
class XAL1
{
public:
	//DEFINATION//////////////////////////////////////////////////////////////
	int sample_rate, buffersize;
	const SAMPLE speed_of_sound = 340;
	const SAMPLE M_PI = SAMPLE(3.1415926);
	const SAMPLE mic_distance = SAMPLE(0.2);
	double current_angle = 0.5 * M_PI;
	//PARAMETERS//////////////////////////////////////////////////////////////
	const int stimulus_frequecy = 1600;
	const int stimulus_delay = 60;//>mic_distance*sample_rate
	const int cross_correlation_length = 12000;
	const int system_delay = 7000;
	const int max_propagation_delay = 300;
	const int signal_amplitude = 100;
	//INITIALIZATION//////////////////////////////////////////////////////////////
	XAL1(const int input_sample_rate, const double input_buffersize_sec)
	{
		sample_rate = input_sample_rate;
		buffersize = input_buffersize_sec * input_sample_rate;
	}
	//MALLOC PTR///////////////////////////////////////////////////////////////////
	void initSampleBufferFormat(SampleBufferFormat* SampleBuffer)
	{
		SampleBuffer->ptrStimulusSamples = new SAMPLE[buffersize]();
		SampleBuffer->ptrReceivedSamples = new SAMPLE[2 * buffersize]();
		SampleBuffer->delayAtw = 0;//this delayAtw is propagation_delay?
		SampleBuffer->bufferSizeAtw = 0;
	}
	//GENERATE STIMULUS////////////////////////////////////////////////////////
	void generateStimulus(SampleBufferFormat* SampleBuffer)
	{
		int half_period = int(0.5 * sample_rate / stimulus_frequecy);
		int period = 2 * half_period;
		int stimulus_length = cross_correlation_length - max_propagation_delay - 1000;
		for (int i = stimulus_delay; i < stimulus_length; i++)
			SampleBuffer->ptrStimulusSamples[i] = (i % period) < half_period ? signal_amplitude : -signal_amplitude;
		SampleBuffer->bufferSizeAtw = stimulus_length;
	}
	//FILTERING//////////////////////////////////////////////////////////////////////
	void filterAudio(SampleBufferFormat* SampleBuffer, const int gauss_filter_length)
	{
		for (int i = 0; i < buffersize - gauss_filter_length; i++)
		{
			int sum_left = 0, sum_right = 0;
			for (int ii = 0; ii < gauss_filter_length; ii++)
			{
				sum_left += SampleBuffer->ptrReceivedSamples[2 * (i + ii)];
				sum_right += SampleBuffer->ptrReceivedSamples[2 * (i + ii) + 1];
			}
			SampleBuffer->ptrReceivedSamples[2 * i] = sum_left;
			SampleBuffer->ptrReceivedSamples[2 * i + 1] = sum_right;
		}
	}
	//CALCULATE ANGLE////////////////////////////////////////////////////////////
	void calculateAngle(SampleBufferFormat* SampleBuffer, double* deltaAngle)
	{
		SAMPLE* cross_correlations = new SAMPLE[2 * stimulus_delay]();
		int max_cross_correlation = 0;
		int delta_k = 0;
		for (int k = 0; k < 2 * stimulus_delay; k++)
		{
			for (int i = 0; i < cross_correlation_length; i++)
				cross_correlations[k] += SampleBuffer->ptrReceivedSamples[2 * (i + k + system_delay)] * SampleBuffer->ptrReceivedSamples[2 * (i + stimulus_delay + system_delay) + 1];
			if (cross_correlations[k] > max_cross_correlation)
			{
				max_cross_correlation = cross_correlations[k];
				delta_k = k - stimulus_delay;
			}
			*deltaAngle = 0.5 * M_PI - acos(speed_of_sound * (delta_k) / (sample_rate * mic_distance));
		}
		delete[] cross_correlations;
	}
	//SERVO MOTOR///////////////////////////////////////////////////////////
	void driveServo(double* currentAngle, double* deltaAngle)
	{
		*currentAngle += *deltaAngle;
		if (abs(*currentAngle) < M_PI)
			initServo(currentAngle);
		else
			std::cout << "error: angle!" << std::endl;
	}
	//AUDIO LOCATION/////////////////////////////////////////////////////////////////
	void run()
	{
		std::cout << "Initialization..." << std::endl;
		SampleBufferFormat SampleBuffer;
		initSampleBufferFormat(&SampleBuffer);
		initAudio();
		generateStimulus(&SampleBuffer);
		initServo(&current_angle);
		int iter_num = 10;
		std::cout << "Please enter the time the program will run ( seconds ):" << std::endl;
		std::cin >> iter_num;
		iter_num = int(iter_num * sample_rate / buffersize);
		for (int i = 0; i < iter_num; i++)
		{
			startAudio(&SampleBuffer);
			//filterAudio(SampleBuffer, 2);
			double delta_angle = 0;
			calculateAngle(&SampleBuffer, &delta_angle);
			driveServo(&current_angle, &delta_angle);
		}
		closeAudio();
		delete[] SampleBuffer.ptrStimulusSamples;
		delete[] SampleBuffer.ptrReceivedSamples;
	}
	~XAL1()
	{
		std::cout << "Program finished..." << std::endl;
		std::cin;
	}
};