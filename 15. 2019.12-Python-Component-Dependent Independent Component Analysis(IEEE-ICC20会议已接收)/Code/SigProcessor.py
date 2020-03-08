import numpy as np
from matplotlib import pyplot as plt
from SigPlotter import SigPlotter as Plt
from mpl_toolkits.mplot3d import art3d

import copy
class SigProcessor:
    #TEST1##############################################################
    #corresponding to Main1-X, Main2-X
    #this test is to test the FastICA

    def GetSubSigs(self,Signals,*SubSigs,ProMode='averange'):
        y=[]
        if ProMode=='summation':
            for SubSig in SubSigs:
                y.append(Signals[SubSig,:].sum(axis=0))
            pass
        elif ProMode=='averange':
            for SubSig in SubSigs:
                y.append(Signals[SubSig,:].mean(axis=0))
            pass
        elif ProMode=='multiply':
            for SubSig in SubSigs:
                z=1
                for Sub in SubSig:
                    z=z*Signals[Sub,:]
                y.append(z)
            pass
        elif ProMode=='a,b.mean-c.mean':
            y.append(Signals[SubSigs[0],:].mean(axis=0))
            y.append(Signals[SubSigs[1],:].mean(axis=0)-Signals[SubSigs[2],:].mean(axis=0))
            pass
        elif ProMode=='a,b.sum-c.sum':
            y.append(Signals[SubSigs[0],:].sum(axis=0))
            y.append(Signals[SubSigs[1],:].sum(axis=0)-Signals[SubSigs[2],:].sum(axis=0))
            pass
        elif ProMode=='a,nummers.*b':
            y.append(Signals[SubSigs[0],:].sum(axis=0))
            temp=[]
            for i in range(len(SubSigs[1])):
                temp.append(float(SubSigs[1][i])*Signals[SubSigs[2][i],:])
            y.append((np.asarray(temp).sum(axis=0)))
            pass
        elif ProMode=='a,ones':
            y.append(Signals[SubSigs[0],:].sum(axis=0))
            y.append(np.ones(Signals.shape[1]))
            pass
        return np.asarray(y)

    #TEST2##############################################################
    #corresponding to Main3-X
    #This test is failed to the proportion of each component

    #let the range of the signals to 1,let the average value of matrix to 0
    def GetNormSigs(self,Signals):
        Ranges=1/(Signals.max(axis=1)-Signals.min(axis=1))
        for i in range(Signals.shape[0]):
            Signals[i]=Signals[i]-Signals[i].mean()
        return np.dot(np.diag(Ranges),Signals)

    def XGetNormSigs(self,Signals):
        Ranges=np.diag(1/(np.mean(abs(Signals),axis=1)))
        return np.dot(Ranges,Signals),Ranges

    #get the summation of the absolute values of the elements in matrix
    def GetSumRange(self,Signal):
        return np.abs(Signal).sum(axis=0)
        
    #get the sumrange of ......
    def GetProportion(self,SignalA,SignalB):
        deviations=[]
        proportions=np.arange(0,2,0.001)
        _SumRange=0
        grads=[]
        for i in proportions:
            SumRange=self.GetSumRange(SignalA-i*SignalB)
            if _SumRange==0:
                _SumRange=SumRange
            deviations.append(SumRange)
            grads.append(SumRange-_SumRange)
            _SumRange=SumRange
        return np.asarray(proportions),np.asarray(deviations),np.asarray(grads)


    #TEST3##############################################################
    #corresponding to Main4-X
    #This test is try to use the fft to get the proportion


    #TEST4##############################################################
    #corresponding to Main5-1
    #This test is to create a window that the extract the useful data
    def PeriodicExtract(self,Signals,Period=10):
        #Signals=asarray(Signals)
        PeriodData=[]
        PeriodCount=int(Signals.shape[1]/Period)
        for Signal in Signals:
            _PeriodData=np.zeros(Period)
            for i in range(PeriodCount*Period):
                _PeriodData[int(i%Period)]+=Signal[i]
            PeriodData.append(_PeriodData)
        return np.asarray(PeriodData)

    def PeriodicExtracts(self,Signals,MinPeriod=10,MaxPeriod=30,Mutiple=10):
        PeriodDatas=np.empty([Signals.shape[0],0])
        for Period in range(MinPeriod,MaxPeriod+1):
            _Period=Mutiple*Period
            PeriodData=self.PeriodicExtract(Signals,_Period)
            PeriodDatas=np.hstack((PeriodDatas,PeriodData))
        return np.asarray(PeriodDatas)

    def PeriodicRangeExtract(self,Signals,Period=10):
        #Signals=asarray(Signals)
        PeriodData=[]
        PeriodCount=int(Signals.shape[1]/Period)
        for Signal in Signals:
            _PeriodData=np.zeros(Period)
            for i in range(PeriodCount*Period):
                _PeriodData[int(i%Period)]+=Signal[i]
            PeriodData.append([sum(abs(_PeriodData))/1])#np.exp(-Period/100) (10**(1.08356555376067+0.46269069652456*np.log10(Period)))
        return np.asarray(PeriodData)

    def PeriodicRangeExtracts(self,Signals,MinPeriod=10,MaxPeriod=30,Mutiple=10):
        PeriodDatas=np.empty([Signals.shape[0],0])
        for Period in range(MinPeriod,MaxPeriod+1):
            _Period=Mutiple*Period
            PeriodData=self.PeriodicRangeExtract(Signals,_Period)
            PeriodDatas=np.hstack((PeriodDatas,PeriodData))
        return np.asarray(PeriodDatas)


    #TEST5##############################################################
    #corresponding to Main5-2
    #This test is to extract several determined datas to run fast ica
    def RadomExtracts(self,Signals,length,**kwargs):
        if 'ExtractRange' in kwargs:
            ExtractRange=kwargs['ExtractRange']
        else:
            ExtractRange=Signals.shape[1]
        ExtractedArray=np.empty([Signals.shape[0],0])
        RandomPositions=[np.random.randint(0,ExtractRange,length)]
        for i in RandomPositions:
            ExtractedArray=np.hstack((ExtractedArray,Signals[:,i]))
        return ExtractedArray




    def Quick2(self,datas,count):
        length=datas.shape[0]
        listR=datas.T.tolist()
        Out=[]
        for i in range(length):
            Out=Out+self.QuickSort2(listR,i)[-count:]+self.QuickSort2(listR,i)[:count]
        return np.asarray(Out).T#[:,1:20]

    #快速排序法
    def QuickSort2(self,datas,ref=0):  
        #data=datas[ref]
        #datas=data.T.tolist()
        if len(datas) >= 2:  # 递归入口及出口        
            mid = datas[len(datas)//2]  # 选取基准值，也可以选取第一个或最后一个元素        
            left, right = [], []  # 定义基准值左右两侧的列表        
            datas.remove(mid)  # 从原始数组中移除基准值        
            for num in datas:            
                if num[ref] >= mid[ref]:                
                    right.append(num)            
                else:                
                    left.append(num)        
            return self.QuickSort2(left) + [mid] + self.QuickSort2(right)    
        else:        
            return datas

    #TEST6/1##############################################################
    #corresponding to Main6-1
    #根据信号毛刺程度来判断是否还原
    #smoothen the signal linearly
    def GetSmoothSig(self,Signal,WindowLen=25):
        _Signal=np.zeros(Signal.shape)
        iter_num=int(Signal.shape[0]/WindowLen)-1
        for i in range(iter_num):
            a0=i*WindowLen
            sum=0
            for n in range(1,WindowLen):
                sum+=Signal[a0+n]
            ExpectedValue=\
                (Signal[a0]+sum*2+Signal[a0+WindowLen])/WindowLen-Signal[a0]
            #get the max and min value to reduce the wave
            SubSignal=Signal[np.arange(a0,a0+WindowLen+1)]
            if ExpectedValue>SubSignal.max():
                ExpectedValue=SubSignal.max()
            elif ExpectedValue<SubSignal.min():
                ExpectedValue=SubSignal.min()
            #set the limit to smoothen the Signal
            _Signal[a0+WindowLen]=ExpectedValue
            Range=Signal[a0+WindowLen]-Signal[a0]
            Step=Range/WindowLen
            StartValue=Signal[a0]
            for n in range(1,WindowLen):
                StartValue+=Step
                _Signal[a0+n]=StartValue
        return _Signal


    def GetSmoothSigs(self,Signals,WindowLen=25):
        _Signals=np.empty([0,Signals.shape[1]])
        for Signal in Signals:
            _Signals=np.vstack((_Signals,self.GetSmoothSig(Signal,WindowLen)))
        return _Signals

    #获取信号的不平整度
    def GetUnevennessValue(self,Signal,WindowLen=25,SmoothSigsOutput=False):
        _Signal=Signal
        SmoothSig=self.GetSmoothSig(_Signal,WindowLen)
        Unevenness=Signal-SmoothSig
        if SmoothSigsOutput:
            return SmoothSig,abs(Unevenness).sum(axis=-1)
        else:
            return abs(Unevenness).sum(axis=-1)

    #获取信号的不平整度
    def GetUnevennessValues(self,Signals,WindowLen=25,SmoothSigsOutput=False):
        SmoothSigs=self.GetSmoothSigs(Signals,WindowLen)
        Unevenness=Signals-SmoothSigs
        if SmoothSigsOutput:
            return SmoothSigs,abs(Unevenness).sum(axis=-1)
        else:
            return abs(Unevenness).sum(axis=-1)


    #根据信号不平整度来拟合信号混合比例
    def MyComponentAnalysis1(self,Signals,WindowLen=4):
        W=np.ones(Signals.shape[0])
        Unevennesses=np.array([])
        for i in np.arange(-1,1,0.01):
            W[0]=i
            _Signal=np.array([np.dot(W,Signals)])
            Unevenness=self.GetUnevennessValue(_Signal,WindowLen)
            #print('W+Uneven',W[0],Unevenness)
            Unevennesses=np.hstack((Unevennesses,Unevenness))
        return np.asarray(Unevennesses)


    #根据信号不平整度来拟合信号混合比例
    def MyComponentAnalysis11(self,Signals,WindowLen=4):
        W=np.ones(Signals.shape[0])
        POUT=[]
        PINW0=[]
        PINW1=[]
        for i in np.arange(-1,1,0.1):
            print(int(i*100),end=' ')
            W[0]=i
            for ii in np.arange(-1,1,0.1):
                if i*ii==0:
                    continue
                W[1]=ii
                _Signal=np.array([np.dot(W,Signals)])
                Unevenness=self.GetUnevennessValue(self.GetNormSigs(_Signal),WindowLen)
                #print(Unevenness[0])
                POUT.append(Unevenness[0])#np.max(_Signal) /(abs(i)+abs(ii))
                PINW0.append(i)
                PINW1.append(ii)
                ax = plt.subplot(111, projection='3d') 
        ax.plot_trisurf(PINW0,PINW1,POUT,cmap='viridis')  #'plasma' / 'viridis'
        ax.set_xlabel('W0')
        ax.set_ylabel('W1')
        ax.set_zlabel('POUT')
        ax.set_title('W0-W1-POUT')
        plt.show()



    #TEST6/2##############################################################
    #失败
    #corresponding to Main6-2
    #根据信号波动性来判断信号还原程度
    def GetSigFluctuation(self,Signal):
        length=Signal.shape[0]
        Sum=0
        Pre=Signal[0]
        for i in range(length):
            Sum+=abs(Signal[i]-Pre)
            Pre=Signal[i]
        return Sum


    #根据信号波动性来判断信号还原程度
    def GetSigFluctuations(self,Signals):
        length=Signals.shape[1]
        Sums=np.array([])
        for Signal in Signals:
            Sum=0
            Pre=Signal[0]
            for i in range(length):
                Sum+=abs(Signal[i]-Pre)
                Pre=Signal[i]
                #print('Pre/Sum:',Pre,Sum)
            Sums=np.hstack((Sums,Sum))
        return Sums

    #根据信号波动程度来拟合信号混合比例
    def MyComponentAnalysis2(self,Signals):
        W=np.ones(Signals.shape[0])
        #print(W)
        Fluctuations=np.array([])
        for i in np.arange(-1,1,0.01):
            W[0]=i
            _Signal=np.array(np.dot(W,Signals))
            #print('_Signal:',_Signal)
            Fluctuation=self.GetSigFluctuation(_Signal)
            #print('Fluctuation:',Fluctuation)
            Fluctuations=np.hstack((Fluctuations,Fluctuation))
        return np.asarray(Fluctuations)
    



    #TEST6/3##############################################################
    #失败
    #corresponding to Main6-3
    #根据信号梯度变化来判断信号还原程度
    def GetSigGradFluctuation(self,Signal):
        length=Signal.shape[0]
        GradSum=0
        GradPre=0
        GradNow=0
        Pre=Signal[0]
        for i in range(length):
            GradNow=Signal[i]-Pre
            GradSum+=abs(GradNow-GradPre)
            GradPre=GradNow
            Pre=Signal[i]
        return GradSum




    #根据信号波动程度来拟合信号混合比例
    def MyComponentAnalysis3(self,Signals):
        W=np.ones(Signals.shape[0])
        W[1]=1
        #print(W)
        Fluctuations=np.array([])
        for i in np.arange(-1,1,0.01):
            W[0]=i
            _Signal=np.array(np.dot(W,Signals))
            #print('_Signal:',_Signal)
            Fluctuation=self.GetSigGradFluctuation(_Signal)
            #print('Fluctuation:',Fluctuation)
            Fluctuations=np.hstack((Fluctuations,Fluctuation))
        return np.asarray(Fluctuations)
    


    #TEST7/1求偏导
    #corresponding to Main7-1
    def GetDiff(self,dF,dX):
        num=0.0
        diff=0.0
        for i in range(dX.shape[0]-1):
            if dX[i+1]!=dX[i]:
                diff+=(dF[i+1]-dF[i])/(dX[i+1]-dX[i])
                num+=1
        return diff/num