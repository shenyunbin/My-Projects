#pragma once
#include <math.h>
#include<iostream>
#include"pre.h"
//MAIN CLASS//////////////////////////////////////////////////////////////
template<class SampleBufferFormat, class SAMPLE>
class XAL3
{
public:
	//DEFINATION//////////////////////////////////////////////////////////////
	int sample_rate, buffersize;
	const SAMPLE speed_of_sound = 340;
	const SAMPLE M_PI = SAMPLE(3.1415925);
	const SAMPLE mic_distance = SAMPLE(0.2);
	double current_angle = 0.5 * M_PI;
	//PARAMETERS//////////////////////////////////////////////////////////////
	const int stimulus_frequecy = 800;
	int stimulus_half_period = 0;
	int stimulus_period = 0;
	const int cross_correlation_length = 12000;
	const int system_delay = 7000;
	//const int min_propagation_delay = 30;
	const int max_propagation_delay = 300;
	const int signal_amplitude = 100;
	//INITIALIZATION//////////////////////////////////////////////////////////////
	XAL3(const int input_sample_rate, const double input_buffersize_sec)
	{
		sample_rate = input_sample_rate;
		buffersize = input_buffersize_sec * input_sample_rate;
		stimulus_half_period = int(0.5 * sample_rate / stimulus_frequecy);
		stimulus_period = 2 * stimulus_half_period;
	}
	//GENERATE STIMULUS////////////////////////////////////////////////////////
	void generateStimulus(SampleBufferFormat* SampleBuffer)
	{
		int stimulus_length = cross_correlation_length - max_propagation_delay - 1000;
		for (int i = 0; i < stimulus_length; i++)
			SampleBuffer->ptrStimulusSamples[i] = (i % stimulus_period) < stimulus_half_period ? signal_amplitude : -signal_amplitude;
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
		//printSampleBufferFormat(SampleBuffer, 7000);
		SAMPLE* cross_correlations_left = new SAMPLE[stimulus_period]();
		SAMPLE* cross_correlations_right = new SAMPLE[stimulus_period]();
		for (int k = 0; k < stimulus_period; k++)
		{
			for (int i = 0; i < cross_correlation_length; i++)
			{
				cross_correlations_left[k] += SampleBuffer->ptrStimulusSamples[i] * SampleBuffer->ptrReceivedSamples[2 * (i + k + system_delay)];
				cross_correlations_right[k] += SampleBuffer->ptrStimulusSamples[i] * SampleBuffer->ptrReceivedSamples[2 * (i + k + system_delay) + 1];
			}
		}
		int delay_left = 0, delay_right = 0;
		SAMPLE max_left = 0, max_right = 0;
		for (int k = 0; k < stimulus_period; k++)
		{
			if (cross_correlations_left[k] > max_left)
			{
				max_left = cross_correlations_left[k];
				delay_left = k;
			}
			if (cross_correlations_right[k] > max_right)
			{
				max_right = cross_correlations_right[k];
				delay_right = k;
			}
		}
		int delta_k = delay_left - delay_right;
		if (delta_k > stimulus_half_period)
			delta_k -= stimulus_period;
		else if (delta_k < -stimulus_half_period)
			delta_k += stimulus_period;
		//std::cout <<"  d_k: "<< delta_k <<std::endl;
		*deltaAngle = 0.5 * M_PI - acos(speed_of_sound * (delta_k) / (sample_rate * mic_distance));
		delete[] cross_correlations_left, cross_correlations_right;
	}
};