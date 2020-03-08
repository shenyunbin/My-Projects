#include <math.h>
#include<iostream>
#include"pre.h"
//MAIN CLASS//////////////////////////////////////////////////////////////
template<class SampleBufferFormat, class SAMPLE>
class FastAL
{
public:
	//DEFINATION//////////////////////////////////////////////////////////////
	int sample_rate, buffersize;
	const SAMPLE M_PI = 3.1415926;
	const SAMPLE mic_distance = 0.2;
	const SAMPLE speed_of_sound = 340;
	const int stimulus_frequecy = 24000;
	const int cross_correlation_length = 128;
	const SAMPLE audio_threshold = 96;
	double current_angle = 0.5 * M_PI;
	//INITIALIZATION//////////////////////////////////////////////////////////////
	FastAL(const int input_sample_rate, const double input_buffersize_sec)
	{
		sample_rate = input_sample_rate;
		buffersize = input_buffersize_sec * input_sample_rate;
	}
	//MALLOC PTR///////////////////////////////////////////////////////////////////
	void initSampleBufferFormat(SampleBufferFormat& SampleBuffer)
	{
		SampleBuffer.ptrStimulusSamples = new SAMPLE[buffersize]();
		SampleBuffer.ptrReceivedSamples = new SAMPLE[2 * buffersize]();
		SampleBuffer.delayAtw = 0;
		SampleBuffer.bufferSizeAtw = buffersize;
	}
	//GENERATE STIMULUS////////////////////////////////////////////////////////
	void generateStimulus(SampleBufferFormat& SampleBuffer)
	{
			int half_period = int(0.5 * sample_rate / stimulus_frequecy);
			int period = 2 * half_period;
			for (int i = 0; i < buffersize; i++)
				SampleBuffer.ptrStimulusSamples[i] = (i % period) < half_period ? 1 : -1;
	}
	//CALCULATE ANGLE////////////////////////////////////////////////////////////
	void calculateAngle(SampleBufferFormat& SampleBuffer, double& deltaAngle)
	{
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
		initSampleBufferFormat(SampleBuffer);
		initAudio();
		generateStimulus(SampleBuffer);
		initServo(&current_angle);
		int iter_num = 10;
		std::cout << "Please enter the time the program will run ( seconds ):" << std::endl;
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


//cross_correlation += SampleBuffer.ptrStimulusSamples[i] * SampleBuffer.ptrReceivedSamples[2 * (i + k) + offset];
//if (SampleBuffer.ptrStimulusSamples[i] > 0)
//cross_correlation += SampleBuffer.ptrReceivedSamples[2 * (i + k) + offset];
//else
//cross_correlation -= SampleBuffer.ptrReceivedSamples[2 * (i + k) + offset];