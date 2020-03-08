import numpy as np
import SigGenerator as SigGen
import SigMixer as SigMix
import SigTester as SigTst
import SigIca as SigIca
import Timer
from matplotlib import pyplot as plt
from scipy.spatial import distance 

import pandas as pd
import seaborn as sns

def whiten(X):
    X = X - X.mean(-1)[:, None]
    A = np.dot(X, X.T)
    D, P = np.linalg.eig(A)
    D = np.diag(D)
    D_inv = np.linalg.inv(D)
    D_half = np.sqrt(D_inv)
    V = np.dot(D_half, P.T)
    return np.dot(V, X), V

def cosine_distance(A,B):
    Aflat = np.hstack(A) 
    Bflat = np.hstack(B) 
    return distance.cosine(Aflat, Bflat) 
    
#ica based on signal amplitude ratios
def signalAmplitudeRatioICA(input_signals):
    m,n = input_signals.shape
    sampling_interval=np.int(n/(10*m**2))
    sampling_signals = input_signals[:,::sampling_interval]
    _Signals=sampling_signals
    m,n = _Signals.shape
    A = np.zeros([m, m])
    A_count = np.zeros([m])
    indexs = np.nanargmax(np.abs(_Signals), axis=-2)
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
    return A


#ica based on signal amplitude ratios
def matrixAEstimation_Si_distribution_2(input_signals,orignal_signals):
    m,n = input_signals.shape
    sampling_interval=1#np.int(n/(10*m**2))
    sampling_signals = input_signals[:,::sampling_interval]
    _Signals=sampling_signals
    m,n = _Signals.shape
    indexs = np.nanargmax(np.abs(_Signals), axis=-2)
    orignal_signals_distribution_1=np.empty([m,0])
    orignal_signals_distribution_2=np.empty([m,0])
    for y in range(n):
        x = indexs[y]
        if _Signals[x, y]>0 and x==0:
            orignal_signals_distribution_1=np.hstack((orignal_signals_distribution_1,orignal_signals[:, y][:,None]))
        else:
            orignal_signals_distribution_2=np.hstack((orignal_signals_distribution_2,orignal_signals[:, y][:,None]))
    
    S_l=orignal_signals_distribution_1[0]
    S_ohters=orignal_signals_distribution_1[1]

    return np.abs(np.mean(S_l)/np.mean(S_ohters))

if __name__ == '__main__':
    # initialize
    # signal Generator
    Gen = SigGen.SigGenerator(10000,10 )
    # signal mixer
    Mix = SigMix.SigMixer()
    # signal tester
    Tst = SigTst.SigTester()
    # signal ica
    Ica = SigIca.Ica()


    freq, Signals_N = Gen.GetWavs2(
        'D:/库/文档/GitHub/fast_bss/Code/wavs/arctic_4s (', ').wav', range(1,2+1))
    m,n=Signals_N.shape
    
    ##Signal Amplitude density################################################
    fig = plt.figure(figsize=(5, 8))
    ax = plt.subplot(1,1,1)
    ax.set_title("S value density distribution")
    ax.hist(Signals_N[1,:]/max(Signals_N[0,:]), bins=1000, color='steelblue', normed=True )
    #plt.axis([-1, 1, 0.0, 0.0015])
    #plt.show()
    _normal_sigs=abs(Signals_N[1,:]/max(Signals_N[1,:]))
    _hist=np.zeros(1001)
    for i in range(n):
        #n=1
        _hist[np.int(round(_normal_sigs[i]*1000))]+=1
    _hist=_hist/n
    for i in range(1000):
        print(round(_hist[i],4))
    
    '''
    ##CdICA initial W cosine distance vs FastICA#########################
    cos_diss1=[]
    cos_diss2=[]
    for i in range(1000):
        MixSignals, A_real = Mix.XGetMixSig(Signals_N, MixType="random_normal", Max=0, Min=0.33)
        W_real=np.linalg.inv(A_real)
        A_arica=signalAmplitudeRatioICA(MixSignals)
        W_arica = np.linalg.inv(A_arica)
        cos_diss1.append(cosine_distance(W_real,W_arica))
        W_random = np.ones((m, m), np.float)
        for i in range(m):
            for j in range(m):
                if i != j:
                    W_random[i, j] = np.random.random()
        cos_diss2.append(cosine_distance(W_real,W_random))
    fig = plt.figure(figsize=(5, 8))
    ax = plt.subplot(1,1,1)
    ax.set_title("CdICA vs FastICA cosine_distance")
    ax.hist(cos_diss1, bins=100, color='steelblue', density=True )
    ax.hist(cos_diss2, bins=100, color='black', density=True )
    #plt.axis([-1, 1, 0.0, 0.0015])
    plt.show()
    '''
    '''
    ##CdICA S1 S2 ratio density#######################################
    s1s2_ratios=[]
    for i in range(200):
        MixSignals, A_real = Mix.XGetMixSig(Signals_N, MixType="random", Max=0, Min=0.33)
        s1s2_ratios.append(matrixAEstimation_Si_distribution_2(MixSignals,Signals_N))
        print(i)
    fig = plt.figure(figsize=(5, 8))
    ax = plt.subplot(1,1,1)
    ax.set_title("|E(S1)|/|E(S2)| Value density")
    ax.hist(s1s2_ratios, bins=100, color='steelblue', density=True )
    #plt.axis([-1, 1, 0.0, 0.0015])
    plt.show()
    '''