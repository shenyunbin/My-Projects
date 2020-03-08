import numpy as np
import soundfile as sf
from scipy.io import wavfile
#import matplotlib as plt

class SigGenerator:
    def __init__(self,SamplingRate=10000,SamplingTime=0.2):
        self.SamplingRate=SamplingRate
        self.SamplingTime=SamplingTime
        self.times=np.arange(0.0,self.SamplingTime,1.0/SamplingRate)

    def GetSig(self,fun):
        y=np.array([])
        for time in self.times:
            y=np.append(y,fun(time))
        return y

    def GetSigs(self,*funs):
        y=[]
        for fun in funs:
            y.append(self.GetSig(fun))
        return np.asarray(y)

    def funSin(self,time,period=0.100):
        return 0.5*np.sin(time / period * 2 * np.pi)

    def funSin1(self,time,period=0.010):
        return 0.5*np.sin(time / period * 2 * np.pi)

    def funSin2(self,time,period=0.020):
        return 0.5*np.sin(time / period * 2 * np.pi)

    def funSin3(self,time,period=0.040):
        return 0.5*np.sin(time / period * 2 * np.pi)

    def funSin4(self,time,period=0.080):
        return 0.5*np.sin(time / period * 2 * np.pi)

    def funSin5(self,time,period=0.160):
        return 0.5*np.sin(time / period * 2 * np.pi)

    def funSinx1(self,time,period1=0.010,period2=0.015,period3=0.020):
        return self.fsinx(time,period1)+self.fsinx(time,period2)+self.fsinx(time,period3)

    def funSinx2(self,time,period1=0.015,period2=0.030,period3=0.025):
        return self.fsinx(time,period1)+self.fsinx(time,period2)+self.fsinx(time,period3)

    def funSinx3(self,time,period1=0.020,period2=0.045,period3=0.010):
        return self.fsinx(time,period1)+self.fsinx(time,period2)+self.fsinx(time,period3)

    def fsinx(self,time,period):
        return 0.5*np.sin(time / period * 2 * np.pi)

    def funCos(self,time,period=0.50):
        return 0.5*np.cos(time / period * 2 * np.pi)

    def funRect(self,time,period=0.050):
        return 0.5 if (time%period)>(period/2) else -0.5

    def funAngle(self,time,period=0.020):
        return (time%period)/period-0.5

    def funNoise(self,time=None,period=None):
        return np.random.random()-0.5

    def funZeros(self,time=None,period=None):
        return 0
    
    def GetWavs(self,pre_addr,end_addr,*args):
        y=[]
        for arg in args:
            sample_rate, X = wavfile.read(pre_addr+arg+end_addr)
            y.append(X)
        return sample_rate,np.asarray(y)
    def GetWavs2(self,pre_addr,end_addr,args):
        y=[]
        for arg in args:
            sample_rate, X = wavfile.read(pre_addr+str(arg)+end_addr)
            y.append(X)
        return sample_rate,np.asarray(y)