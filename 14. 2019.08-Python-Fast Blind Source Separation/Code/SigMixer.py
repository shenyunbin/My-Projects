import numpy as np

class SigMixer:
    def GetMixSig(self, Signals, MixType="normal",Max=1,Min=0.1):
        Signals = np.asarray(Signals)
        MixMatrix = self.GetMixSigMatrix(Signals.shape[0], MixType=MixType,Max=Max,Min=Min)
        #print('\nMix:\n',MixMatrix,'\nMix_inv:\n',np.linalg.inv(MixMatrix))
        return np.dot(MixMatrix, Signals)

    def XGetMixSig(self, Signals, MixType="normal",Max=1,Min=0.1):
        Signals = np.asarray(Signals)
        MixMatrix = self.GetMixSigMatrix(Signals.shape[0], MixType=MixType,Max=Max,Min=Min)      
        return np.dot(MixMatrix, Signals),MixMatrix

    def XGetMixSig2(self, Signals, MixType="normal",Max=1,Min=0.1):
        Signals = np.asarray(Signals)
        MixMatrix = np.array([  [0.9,1 ,0.9,0.8],
                                [1 ,0.9,0.8,0.7],
                                [0.8,0.9,1 ,0.9],
                                [0.7,0.8,0.9,1 ]    ])
        return np.dot(MixMatrix, Signals),MixMatrix


    def GetMixSigMatrix(self, SignalsCount, MixType="normal",Max=1,Min=0.1):
        MixMatrix = np.zeros([SignalsCount, SignalsCount])
        if SignalsCount < 2:
            return MixMatrix
        krange = range(1-SignalsCount, SignalsCount, 1)
        if MixType == "normal":
            for i in krange:
                MixMatrix += Min*np.eye(SignalsCount, k=i, dtype=float)
            MixMatrix += (Max-Min)*np.eye(SignalsCount, k=0, dtype=float)
        elif MixType == "linear":
            for i in krange:
                MixMatrix += (Max-np.abs((Max-Min)*i/(SignalsCount-1))) * \
                    np.eye(SignalsCount, k=i, dtype=float)
        elif MixType == "random":
            MixMatrix = np.ones((SignalsCount, SignalsCount), np.float)
            for i in range(SignalsCount):
                for j in range(SignalsCount):
                    if i != j:
                        MixMatrix[i, j] = np.random.random()
        elif MixType == "random_limit":
            MixMatrix = np.ones((SignalsCount, SignalsCount), np.float)
            for i in range(SignalsCount):
                for j in range(SignalsCount):
                    if i != j:
                        MixMatrix[i, j] = Min+(Max-Min)*np.random.random()
        elif MixType == "random_all":
            for i in range(SignalsCount):
                for j in range(SignalsCount):
                        MixMatrix[i, j] = np.random.random()
            indexs=np.argmax(MixMatrix,axis=0)
            for i in range(SignalsCount):
                if(indexs[i]!=i):
                    temp=MixMatrix[i,i]
                    MixMatrix[i,i]=MixMatrix[indexs[i],i]*1.1
                    MixMatrix[indexs[i],i]=temp
        elif MixType == "random_normal":
            Radom_Numbers = np.random.normal(Max,Min,9000)
            index=0
            for i in range(SignalsCount):
                for j in range(SignalsCount):
                    while(abs(Radom_Numbers[index])>=0.99):# or Radom_Numbers[index]<=0.00
                        index+=1
                    MixMatrix[i, j] = abs(Radom_Numbers[index])
                    index+=1
            for i in range(SignalsCount):
                MixMatrix[i, i] = 1
        elif MixType == "random_var":
            Radom_Numbers = np.random.normal(Max,Min,9000)
            index=0
            for i in range(SignalsCount):
                for j in range(SignalsCount):
                    while(Radom_Numbers[index]>=0.99 or Radom_Numbers[index]<=0.01):
                        index+=1
                    MixMatrix[i, j] = Radom_Numbers[index]
                    index+=1
            for i in range(SignalsCount):
                MixMatrix[i, i] = 1
        elif MixType == "manual_2D":
            MixMatrix = np.ones((SignalsCount, SignalsCount), np.float)
            MixMatrix[1,0]=Max
            MixMatrix[0,1]=Min
        elif MixType == "random_normal_wr":
            Radom_Numbers = np.random.normal(Max,Min,9000)
            index=0
            for i in range(SignalsCount):
                for j in range(SignalsCount):
                    while(abs(Radom_Numbers[index])>=0.99):# or Radom_Numbers[index]<=0.00
                        index+=1
                    MixMatrix[i, j] = abs(Radom_Numbers[index])
                    index+=1
            for i in range(SignalsCount):
                MixMatrix[i, i] = 1
            _SignalsCount=round(SignalsCount)
            _Max=round(Max,3)*1e3
            _Min=round(Min,3)*1e3
            _FileName="Code/.random_matrixs_a/random_normal_s"+str(_SignalsCount)+"_mu"+str(_Max)+"_si"+str(_Min)+".bin"
            MixMatrix.tofile(_FileName)
        elif MixType == "random_normal_rd":
            _SignalsCount=round(SignalsCount)
            _Max=round(Max,3)*1e3
            _Min=round(Min,3)*1e3
            _FileName="Code/.random_matrixs_a/random_normal_s"+str(_SignalsCount)+"_mu"+str(_Max)+"_si"+str(_Min)+".bin"
            _MixMatrix = np.fromfile(_FileName)
            _MixMatrix.shape=[SignalsCount,SignalsCount]
            MixMatrix=_MixMatrix
        return MixMatrix
'''
            mu=Max
            sigma=Min
            Radom_Numbers = np.random.normal(Max,Min,1000)
            index=0
            fx=0
            for i in range(SignalsCount):
                for j in range(SignalsCount):
                    while (fx>=0.99 or fx<=0.01):
                        bins=Radom_Numbers[index]
                        fx=1/(sigma * np.sqrt(2 * np.pi)) * np.exp( - (bins - mu)**2 / (2 * sigma**2)
                        index+=1
                    MixMatrix[i, j] = Radom_Numbers[index]
                    index+=1
            for i in range(SignalsCount):
                MixMatrix[i, i] = 1
'''