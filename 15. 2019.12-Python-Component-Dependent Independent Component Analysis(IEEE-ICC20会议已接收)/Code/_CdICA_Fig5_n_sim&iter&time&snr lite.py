import math

import numpy as np
import SigGenerator as SigGen
import SigMixer as SigMix
import SigTester as SigTst
import Timer
from scipy.spatial import distance 
from matplotlib import pyplot as plt



def cosine_distance(A,B):
    Aflat = np.hstack(A) 
    Bflat = np.hstack(B) 
    return distance.cosine(Aflat, Bflat)

'''
//COMPLEX ICA//////////////////////////////////////////
/////////////////////////////////////////////////////////

NOTES:
    X=A@S
    S=B@X
    B=A^-1

STEPS:
    1.SIGNAL SAMPLING -> OUTPUT SAMPLING SIGNAL

    2.ICA BASED ON SAMPLING SIGNAL AMPLITUDE RATIOS -> OUTPUT B1

    3.MULTI GRAD FAST ICA BASED ON EXTRACTED SIGNAL UND A1 -> OUTPUT B2

/////////////////////////////////////////////////////////
'''

class CICA():
    #whiten, let the correlation matrix to be an identity matrix
    def whiten(self, X):
        X = X - X.mean(-1)[:, None]
        A = np.dot(X, X.T)
        D, P = np.linalg.eig(A)
        D = np.diag(D)
        D_inv = np.linalg.inv(D)
        D_half = np.sqrt(D_inv)
        V = np.dot(D_half, P.T)
        return np.dot(V, X), V

    #function tanh(x), output is tanh(x) and the average of tanh(x)'
    def _tanh(self, x):
        gx = np.tanh(x)
        g_x = gx ** 2
        g_x -= 1
        g_x *= -1
        return gx, g_x.mean(-1)

    def _exp(self, x):
        exp = np.exp(-(x ** 2) / 2)
        gx = x * exp
        g_x = (1 - x ** 2) * exp
        return gx, g_x.mean(axis=-1)

    def _cube(self, x):
        return x ** 3, (3 * x ** 2).mean(axis=-1)

    #decorrelation, let the correlation matrix to be an identity matrix
    def decorrelation(self, W):
        U, S = np.linalg.eigh(np.dot(W, W.T))
        U = np.diag(U)
        U_inv = np.linalg.inv(U)
        U_half = np.sqrt(U_inv)
        rebuild_W = np.dot(np.dot(np.dot(S, U_half), S.T), W)
        return rebuild_W

    #transfer the matrix A to B, B=k*inv(A), and B should be normolized
    def generate_B(self,V,A,m,n):
        if A is None:
            return np.random.random_sample((n,n))
        return np.linalg.inv(np.dot(n*m*V, A))


    #the iteration part of original fastica
    def fasticaIteration(self,B,X,m,tol,max_iter=100):
        g = self._tanh
        for i in range(max_iter):
            gwtx, g_wtx = g(np.dot(B, X))
            B1 = self.decorrelation(np.dot(gwtx, X.T) - m * g_wtx[:, None] * B)
            lim = max(abs(abs(np.diag(np.dot(B1, B.T))) - 1))
            B = B1
            #print(B)
            if lim < tol:
                break
        #print('iter_num',i)
        return B,tol,i+1


    #multi grad fastica with a initial matrix A
    def singleFastICAwithA(self, Signals, A, fun='tanh',max_iter=200, tol=1e-04):
        X, V = self.whiten(Signals)
        n, m = X.shape
        X *= np.sqrt(m)
        #generate matrix B
        B0 = self.generate_B(V,A,m,n)
        #fastica iteration
        B,_tol,iter_num = self.fasticaIteration(B0,X,m,tol,max_iter)
        return np.dot(B, V),iter_num,B0,V


    #ica based on signal amplitude ratios
    def signalAmplitudeRatioICA(self, _signals):  #v6 the optimal
        T1=Timer.Timer("Test")
        T1.Start()
        #_signals=_signals[:,::100]
        m,n = _signals.shape
        A = np.empty([m, m])
        indexs_max_abs = np.nanargmax(np.abs(_signals), axis=-2)
        indexs_max_abs_positive = np.where(_signals[indexs_max_abs,np.arange(n)]>0,indexs_max_abs,np.nan)
        
        for y in range(m):
            selected_indexs=np.argwhere(indexs_max_abs_positive==y)
            A[:,[y]] = np.sum(_signals[:,selected_indexs],axis=-2)
        A  = np.dot(A, np.diag(1/np.max(A, axis=-1)))
        A = np.clip(A, 0.01, None)
        T1.Stop()
        T1.Print()
        return A


    #ica based on signal amplitude ratios
    def signalAmplitudeRatioICAv6(self, _signals):  #v6 the optimal
        T1=Timer.Timer("Test")
        T1.Start()
        #_signals=_signals[:,::100]
        m,n = _signals.shape
        A = np.empty([m, m])
        indexs_max_abs = np.nanargmax(np.abs(_signals), axis=-2)
        indexs_max_abs_positive = np.where(_signals[indexs_max_abs,np.arange(n)]>0,indexs_max_abs,np.nan)
        
        for y in range(m):
            selected_indexs=np.argwhere(indexs_max_abs_positive==y)
            A[:,[y]] = np.sum(_signals[:,selected_indexs],axis=-2)
        A  = np.dot(A, np.diag(1/np.max(A, axis=-1)))
        A = np.clip(A, 0.01, None)
        T1.Stop()
        T1.Print()
        return A


    def signalAmplitudeRatioICA3(self, input_signals):#v3ok!!! the fastest!
        T1=Timer.Timer("Test")
        T1.Start()
        _signals=input_signals
        m,n = _signals.shape
        A = np.zeros([m, m])
        _signals_abs=np.abs(_signals)
        indexs = np.nanargmax(_signals_abs, axis=-2)
        selected_max_signals=_signals[indexs,range(n)]
        selected_max_signals_positive=np.where(selected_max_signals>0,1,np.nan)
        #selected_max_signals=np.clip(_signals[indexs,range(n)], 0, None)
        #selected_max_signals_positive=np.where(selected_max_signals,1,np.nan)
        indexs=np.multiply(selected_max_signals_positive,indexs)
        for y in range(m):
            selected_max_signal=np.where(indexs==y,1,0)
            for x in range(m):
                A[x,y]=np.einsum('i, i',selected_max_signal,_signals[x])
        A  = np.dot(A, np.diag(1/np.max(abs(A), axis=-1)))
        A = np.clip(A, 0.01, None)
        T1.Stop()
        T1.Print()
        return A

    #complex ica##########################
    def sCICA(self,input_signals, arica_enabled=True):
        T3 = Timer.Timer("COMPLEX-ICA")
        T3.Start()
        if arica_enabled:
            #1.data extraction
            #2.signal amplitude ratio ica
            A1 = self.signalAmplitudeRatioICA(input_signals)
        else:
            A1=None
        T3.Stop()
        T5 = Timer.Timer("COMPLEX-ICA")
        T5.Start()
        #3.multi grad fastica based on the extracted signal and A1
        B2,iter_num,B,V = self.singleFastICAwithA(input_signals, A1)
        T5.Stop()
        return B2,iter_num,B,V,T3.Value(),T5.Value()



class Result:
    def __init__(self,name="NA"):
        self.PSNR=[]
        self.SNR=[]
        self.time=[]
        self.iter_num=[]
        self.count=1
        self.name=name
    def ShowReport(self):
        print('\n>> No.', self.count, ' Report of '+ self.name + ':')
        print('    Average time: ', np.mean(self.time), " s")
        print('    Average PSNR: ', np.mean(self.PSNR), " dB")
        print('    Average SNR: ', np.mean(self.SNR), " dB")
        #print('    Var: ', np.var(self.time), " ms")

    def ShowReport2(self):
        print('\n>> No.', self.count, ' Report of '+ self.name + ':')
        print('    Average time: ', np.mean(self.time), " s")
        print('    Average Iter Num: ', np.mean(self.iter_num), " times")


    def GetReport(self):
        return ','+str(round(np.mean(self.time)*1000,2))+'ms,'+str(round(np.mean(self.PSNR),2))+'dB'



def plot_res(x,_Datas,type,xl,yl,axis):
    fig = plt.figure(figsize=(5, 4))
    #ax = fig.add_subplot(1,1,1)
    _lw,_ms=0.8,4
    plt.plot(x, _Datas[type][0][0], color='red', linestyle='-',marker='o', linewidth=_lw,ms=_ms, markerfacecolor='none')
    plt.plot(x, _Datas[type][0][1], color='red', linestyle='-',marker='s', linewidth=_lw,ms=_ms, markerfacecolor='none')
    plt.plot(x, _Datas[type][0][2], color='red', linestyle='-',marker='x', linewidth=_lw,ms=_ms, markerfacecolor='none')
    plt.plot(x, _Datas[type][1][0], color='blue', linestyle=':',marker='o', linewidth=_lw,ms=_ms, markerfacecolor='none')
    plt.plot(x, _Datas[type][1][1], color='blue', linestyle=':',marker='s', linewidth=_lw,ms=_ms, markerfacecolor='none')
    plt.plot(x, _Datas[type][1][2], color='blue', linestyle=':',marker='x', linewidth=_lw,ms=_ms, markerfacecolor='none')
    plt.axis(axis) 
    plt.xlabel(xl)
    plt.ylabel(yl)
    plt.legend(['CdICA n=8',  'CdICA n=12', 'CdICA n=16',  'FastICA n=8', 'FastICA n=12',  'FastICA n=16'], loc='upper right')
    plt.grid(linestyle=':')
    plt.subplots_adjust(top=0.99, right=0.98, left=0.11)



if __name__ == '__main__':
    # initialize
    # signal Generator
    Gen = SigGen.SigGenerator(1000, 0.5)
    # signal mixer
    Mix = SigMix.SigMixer()
    # signal tester
    Tst = SigTst.SigTester()
    #CICA
    CICA = CICA()

    _mean=0.0
    _sourcenums=[8,12,16]
    _sigmas=np.arange(5,101,5)
    x_range=20
    repeat_num=1
    pic_length=x_range

    Datas=np.zeros([5,2,3,x_range,repeat_num])

    if True:

        for ii in range(3):

            for jj in range(x_range):

                for repeat in range(repeat_num):
                    # get the signals from wavs1 5 11 18  / 15 20 25 30 , '14', '15', '21', '28'
                    freq, Signals_N = Gen.GetWavs2(
                        'D:/库/文档/GitHub/fast_bss/Code/wavs/arctic_4s (', ').wav', range(1,_sourcenums[ii]+1))
                    #Signals_N=Signals_N[:,0:8000]
                    m,n=Signals_N.shape
                    MixSignals, A_real = Mix.XGetMixSig(Signals_N, MixType="random_normal", Max=_mean, Min=_sigmas[jj]*0.0033)
                    W_real=np.linalg.inv(A_real)
                    #CICA
                    B_arica_1, iter_num_1, W_arica_1, V_1,T_arica_1, T5 = CICA.sCICA(MixSignals,True)
                    W_arica_1=np.dot(W_arica_1,V_1)
                    diff=cosine_distance(W_arica_1,W_real)

                    S_cdica_1=np.dot(B_arica_1, MixSignals)

                    #FICA
                    B_arica_2, iter_num_2, W_random_2, V_2,T_arica_2, T6 = CICA.sCICA(MixSignals,False)
                    W_arica_2=np.dot(W_random_2,V_2)
                    random_diff=cosine_distance(W_random_2,W_real)

                    S_fastica_2=np.dot(B_arica_2, MixSignals)

                    Datas[0][0][ii][jj][repeat]=diff
                    Datas[1][0][ii][jj][repeat]=iter_num_1
                    Datas[2][0][ii][jj][repeat]=T5*1000+T_arica_2*1000
                    Datas[3][0][ii][jj][repeat]=Tst.GetSigSNR_PLUS(np.abs(Signals_N), np.abs(S_cdica_1))
                    Datas[4][0][ii][jj][repeat]=T_arica_1*1000

                    Datas[0][1][ii][jj][repeat]=random_diff
                    Datas[1][1][ii][jj][repeat]=iter_num_2
                    Datas[2][1][ii][jj][repeat]=T6*1000+T_arica_2*1000
                    Datas[3][1][ii][jj][repeat]=Tst.GetSigSNR_PLUS((Signals_N), (S_fastica_2))
                    Datas[4][1][ii][jj][repeat]=T_arica_2*1000

                print("now is", _mean,_sigmas[jj],_sourcenums[ii])

        Datas=np.asarray(Datas)
        

        Datas.tofile("Eva_Datas_5_2_3_20_30_new_best_8_12_16.bin")

    Datas=np.fromfile("Eva_Datas_5_2_3_20_30_new_best_8_12_16.bin")
    Datas.shape=[5,2,3,x_range,repeat_num]
    _Datas=np.mean(Datas,axis=-1)

    x=_sigmas
    plot_res(x,_Datas,0,xl=r'$\Lambda\ /\ \%$',yl=r'$\Delta$',axis=[0, 101, 0.0, 1.25])
    plot_res(x,_Datas,1,xl=r'$\Lambda\ /\ \%$',yl=r'$N_{inter}$',axis=[0, 101, 0, 38])
    plot_res(x,_Datas,2,xl=r'$\Lambda\ /\ \%$',yl=r'$T\ /\ ms$',axis=[0, 101, 0, 1200])
    plot_res(x,_Datas,3,xl=r'$\Lambda\ /\ \%$',yl=r'$SNR\ /\ dB$',axis=[0, 101, 0, 60])
    plt.show()


'''

    
    def signalAmplitudeRatioICA_v5(self, input_signals):
        T1=Timer.Timer("Test")
        T1.Start()
        _signals=input_signals
        m,n = _signals.shape
        A = np.zeros([m, m])
        _signals_abs=np.abs(_signals)

        indexs_abs = np.nanargmax(_signals_abs, axis=-2)
        indexs = np.nanargmax(_signals, axis=-2)

         
        selected_max_signals=np.zeros([m,n])
        selected_max_signals[indexs,range(n)]=1 
        selected_max_signals_abs=np.zeros([m,n])
        selected_max_signals_abs[indexs_abs,range(n)]=1
        selected_signals=np.multiply(selected_max_signals,selected_max_signals_abs)

        for y in range(m):
            #selected_max_signal=np.multiply(selected_max_signals_nan[y],selected_max_signals_positive)
            for x in range(m):
                A[x,y]=np.einsum('i, i',selected_signals[y],_signals[x])
        A  = np.dot(A, np.diag(1/np.max(abs(A), axis=-1)))
        A = np.clip(A, 0.01, None)
        T1.Stop()
        T1.Print()
        return A


    def signalAmplitudeRatioICA_v4(self, input_signals):
        T1=Timer.Timer("Test")
        T1.Start()
        _signals=input_signals
        m,n = _signals.shape
        A = np.zeros([m, m])
        _signals_abs=np.abs(_signals)

        indexs = np.nanargmax(_signals_abs, axis=-2)
        indexs_normal = np.nanargmax(_signals, axis=-2)

        selected_max_signals=_signals[indexs,range(n)]
        selected_max_signals_positive=np.where(selected_max_signals>0,1,0)
        #indexs=np.multiply(selected_max_signals_positive,indexs)
         
        selected_max_signals_nan=np.zeros([m,n])
        selected_max_signals_nan[indexs,range(n)]=1 


        for y in range(m):
            selected_max_signal=np.multiply(selected_max_signals_nan[y],selected_max_signals_positive)
            for x in range(m):
                A[x,y]=np.einsum('i, i',selected_max_signal,_signals[x])
        A  = np.dot(A, np.diag(1/np.max(abs(A), axis=-1)))
        A = np.clip(A, 0.01, None)
        T1.Stop()
        T1.Print()
        return A



    def signalAmplitudeRatioICA_v2(self, input_signals):
        T1=Timer.Timer("Test")
        T1.Start()
        m,n = input_signals.shape
        _Signals=input_signals
        m,n = _Signals.shape
        A = np.zeros([m, m])
        indexs = np.nanargmax(np.abs(_Signals), axis=-2)
        selected_max_signals=_Signals[indexs,range(n)]
        selected_max_signals_positive=np.where(selected_max_signals>0,1,0)
        for y in range(m):
            slected_max_signal=np.multiply(selected_max_signals_positive,np.where(indexs==y,1,0))
            for x in range(m):
                A[x,y]=np.einsum('i, i',slected_max_signal,_Signals[x])#np.sum(np.dot(slected_max_signal,_Signals[x]))
        A  = np.dot(A, np.diag(1/np.max(abs(A), axis=-1)))
        A = np.clip(A, 0.01, None)

        #selected_max_signals=_Signals[indexs,range(n)]
        #map(lambda x, y: A[x, y], [1, 3, 5, 7, 9], [2, 4, 6, 8, 10])
        T1.Stop()
        T1.Print()
        return A

    def signalAmplitudeRatioICA_v1_old(self, input_signals):
        m,n = input_signals.shape
        sampling_interval=np.int(n/(10*m**2))
        sampling_interval=1
        sampling_signals = input_signals[:,::sampling_interval]
        _Signals=sampling_signals
        m,n = _Signals.shape
        A = np.zeros([m, m])
        A_count = np.zeros([m])
        T1=Timer.Timer("Test")
        T1.Start()
        indexs = np.nanargmax(np.abs(_Signals), axis=-2)
        selected_max_signals=_Signals[indexs,range(n)]
        selected_max_signals_positive=np.where(selected_max_signals>0,1,0)
        for y in range(m):
            slected_max_signal=np.multiply(selected_max_signals_positive,np.where(indexs==y,1,0))
            for x in range(m):
                A[x,y]=np.einsum('i, i',slected_max_signal,_Signals[x])#np.sum(np.dot(slected_max_signal,_Signals[x]))
        A  = np.dot(A, np.diag(1/np.max(abs(A), axis=-1)))
        _A = np.clip(A, 0.01, None)

        #selected_max_signals=_Signals[indexs,range(n)]
        #map(lambda x, y: A[x, y], [1, 3, 5, 7, 9], [2, 4, 6, 8, 10])
        T1.Stop()
        T1.Print()



        for y in range(n):
            x = indexs[y]
            if _Signals[x, y]>0:
                Temp = _Signals[:, y]
                A[:, x] += Temp
                A_count[x] += _Signals[x, y]
            elif _Signals[x, y]<0:
                Temp = _Signals[:, y]
                A[:, x] -= Temp
                A_count[x] -= _Signals[x, y]
        A = np.dot(A, np.diag(1/A_count))
        A = np.clip(A, 0.01, None)
        print(_A-A)
        return A

'''