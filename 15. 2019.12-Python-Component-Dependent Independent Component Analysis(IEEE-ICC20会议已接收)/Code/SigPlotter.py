from matplotlib import pyplot as plt
import numpy as np

class SigPlotter:
    def __init__(self):
        self.FigNum=0

    def Plot(self,times,Signals,show=True):
        plt.figure(self.FigNum)
        self.FigNum+=1
        #m=len(Signals)
        m=Signals.shape[0]
        for i in range(m):
            plt.subplot(m, 1, i + 1)
            plt.plot(times, Signals[i])
        if show:
            plt.show()
    
    def Plots(self,times,*Signalss):
        for Signals in Signalss:
            self.Plot(times,Signals,show=False)
        plt.show()
    
    def Plot1D(self,Signals,show=True):
        plt.figure(self.FigNum)
        self.FigNum+=1
        #m=len(Signals)
        m=Signals.shape[0]
        for i in range(m):
            plt.subplot(m, 1, i + 1)
            plt.plot(Signals[i])
        if show:
            plt.show()
    
    def Plots1D(self,*Signalss):
        for Signals in Signalss:
            self.Plot1D(Signals,show=False)
        plt.show()
