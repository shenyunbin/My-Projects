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
	void initAudio() {}
	void startAudio(SampleBufferFormat* SampleBuffer)
	{
		for (int i = 0; i < buffersize - 200; i++)
		{
			SampleBuffer->ptrReceivedSamples[2 * (i + 110)] = SampleBuffer->ptrStimulusSamples[i];
			SampleBuffer->ptrReceivedSamples[2 * (i + 100) + 1] = SampleBuffer->ptrStimulusSamples[i];
		}
	}
	void closeAudio() {}
	void initServo(double* currentAngle) { std::cout << "currentAngle: " << (*currentAngle * 180 / M_PI) << std::endl; }
	//ABOVE JUST FOR TEST/////////////////////////////////////////////////////
public:
	//DEFINATION//////////////////////////////////////////////////////////////
	const double M_PI = 3.1415926;
	const double mic_distance = 0.2;
	const double speed_of_sound = 340;
	const int  stimulus_mode = 1;//0 sin, 1 square
	const long int stimulus_frequecy = 24000;
	const int cross_correlation_length = 128;
	double current_angle = 0.5 * M_PI;
	SAMPLE* cross_correlation_left = new SAMPLE[buffersize]();
	SAMPLE* cross_correlation_right = new SAMPLE[buffersize]();
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
		int k_max = buffersize - cross_correlation_length; //6000
		//cross_correlation
		for (int k = 0; k < k_max ;  k++)
			for (int i = 0; i < cross_correlation_length; i++)
			{
				cross_correlation_left[k] += SampleBuffer.ptrStimulusSamples[i] * SampleBuffer.ptrReceivedSamples[2 * (i + k)];
				cross_correlation_right[k] += SampleBuffer.ptrStimulusSamples[i] * SampleBuffer.ptrReceivedSamples[2 * (i + k) + 1];
			}
		//found the max correlation value
		SAMPLE max_left = 0, max_right = 0;
		for (int i = 0; i < k_max; i++)
		{
			if (cross_correlation_left[i] > max_left)
				max_left = cross_correlation_left[i];
			if (cross_correlation_right[i] > max_right)
				max_right = cross_correlation_right[i];
		}
		//found the delay of left and right side
		int delay_left = 0, delay_right = 0;
		for (int i = 0; i < k_max; i++)
			if (cross_correlation_left[i] > 0.8 * max_left)
			{
				delay_left = i;
				break;
			}
		for (int i = 0; i < k_max; i++)
			if (cross_correlation_right[i] > 0.8 * max_right)
			{
				delay_right = i;
				break;
			}
		//calculate the angle
		int delta_k = delay_left - delay_right;
		deltaAngle = 0.5 * M_PI - acos(speed_of_sound * delta_k / (sample_rate * mic_distance));
	}
	//SERVO MOTOR///////////////////////////////////////////////////////////
	void driveServo(double& currentAngle,double& deltaAngle)
	{
		currentAngle += deltaAngle;
		if (abs(currentAngle)<M_PI)
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
		iter_num =int( iter_num *sample_rate / buffersize);
		for (int i = 0; i < iter_num; i++)
		{
			startAudio(&SampleBuffer);
			decentralizationSampleBufferFormat(SampleBuffer);//optional
			double delta_angle = 0;
			calculateAngle(SampleBuffer, delta_angle);
			driveServo(current_angle, delta_angle);
		}
		closeAudio();
		delete[] SampleBuffer.ptrStimulusSamples;
		delete[] SampleBuffer.ptrReceivedSamples;
	}
	~AL()
	{
		delete[] cross_correlation_left;
		delete[] cross_correlation_right;
		std::cout << "Program finished..." << std::endl;
		std::cin;
	}
};