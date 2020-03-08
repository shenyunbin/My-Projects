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
from scipy import stats
from matplotlib.lines import Line2D
import mpl_toolkits.axisartist as axisartist

#ica based on signal amplitude ratios
def matrixAEstimation_Si_distribution(input_signals,orignal_signals,A_real):
    m,n = input_signals.shape
    sampling_interval=1#np.int(n/(10*m**2))
    sampling_signals = input_signals[:,::sampling_interval]
    _Signals=sampling_signals
    m,n = _Signals.shape
    indexs = np.nanargmax(np.abs(_Signals), axis=-2)
    orignal_signals_distribution=np.empty([m,0])
    for y in range(n):
        x = indexs[y]
        if _Signals[x, y]>0:
            if x==0:
                orignal_signals_distribution=np.hstack((orignal_signals_distribution,orignal_signals[:, y][:,None]))
        elif _Signals[x, y]<0:
            if x==0:
                orignal_signals_distribution=np.hstack((orignal_signals_distribution,-orignal_signals[:, y][:,None]))
    if m==2:
        S_l=orignal_signals_distribution[0]
        S_ohters=np.dot(A_real[0,1],orignal_signals_distribution[1,:])
        return S_l/15000,S_ohters/15000
    S_l=orignal_signals_distribution[0]
    S_ohters=np.dot(A_real[0,1:m-1],orignal_signals_distribution[1:m-1,:])
    return S_l/15000,S_ohters/15000

#ica based on signal amplitude ratios
def matrixAEstimation_Si_distribution_2(input_signals,orignal_signals,A_real):
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
    _S_l_=orignal_signals_distribution_2[0]
    _S_ohters_=orignal_signals_distribution_2[1]
    return S_l/15000,S_ohters/15000,_S_l_/15000,_S_ohters_/15000


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

    title="N8_sigma0-1"
    _mean=0.0
    _sigmas=[0.17,0.33,0.5]
    _lambdas=[50,100]
    _sourcenums=[2,8,16]
    fig = plt.figure(figsize=(5, 6))
    ii=1
    
    for i in [0,1,2]:
        for j in [0,1]:
            # get the signals from wavs1 5 11 18  / 15 20 25 30 , '14', '15', '21', '28'
            freq, Signals_N = Gen.GetWavs2(
                'D:/库/文档/GitHub/fast_bss/Code/wavs/arctic_4s (', ').wav', range(1,_sourcenums[i]+1))
            m,n=Signals_N.shape
            MixSignals, A = Mix.XGetMixSig(Signals_N, MixType="random_normal", Max=_mean, Min=_sigmas[j])
            #arica
            S_l,S_others=matrixAEstimation_Si_distribution(MixSignals,Signals_N,A)

            #ax = fig.add_subplot(3,2,ii)

            #fig = plt.figure(figsize=(5, 4))
            #ax = fig.add_subplot(1,1,1)
            ax = axisartist.Subplot(fig, 3,2,ii)  
            fig.add_axes(ax)
            ax.axis[:].set_visible(False)
            ax.axis["x"] = ax.new_floating_axis(0,0)
            ax.axis["x"].set_axisline_style("->", size = 1.0)
            ax.axis["y"] = ax.new_floating_axis(1,0)
            ax.axis["y"].set_axisline_style("-|>", size = 1.0)
            ax.axis["x"].set_axis_direction("top")
            ax.axis["y"].set_axis_direction("right")
            ii+=1
            ax.set_title((r'$\Lambda=$')+str(_lambdas[j])+'% n='+str(_sourcenums[i]))
            plt.scatter(S_others,S_l,s=0.5,alpha=0.5)
            ax.spines['bottom'].set_linewidth(1)
            ax.spines['left'].set_linewidth(1)
            ax.spines['top'].set_color('none')
            ax.spines['right'].set_color('none')
            ax.spines['bottom'].set_position(('data', 0))
            ax.spines['left'].set_position(('data', 0))
            plt.annotate(s=r'${\widehat{\mathbf{s}}_{other}}^T$',xy=(1.5,0),xytext=(1.5,0.1) )
            plt.annotate(s=r'${\widehat{\mathbf{s}}_{l}}^T$',xy=(0,1.35),xytext=(0.1,1.35) )
            plt.annotate(s='0',xy=(0,0),xytext=(-0.25,-0.35) )
            plt.axis([-1.5, 1.5,-1.5, 1.5])
            plt.xticks([])
            plt.yticks([])
            plt.grid(linestyle=':')
    plt.show()



    fig = plt.figure(figsize=(3.5, 3))
    i=0
    # get the signals from wavs1 5 11 18  / 15 20 25 30 , '14', '15', '21', '28'
    freq, Signals_N = Gen.GetWavs2(
        'D:/库/文档/GitHub/fast_bss/Code/wavs/arctic_4s (', ').wav', range(1,_sourcenums[i]+1))
    m,n=Signals_N.shape
    MixSignals, A = Mix.XGetMixSig(Signals_N, MixType="manual_2D", Max=0.7, Min=0.3)
    #arica
    print(A)
    S_l,S_others,_S_l_,_S_others_=matrixAEstimation_Si_distribution_2(MixSignals,Signals_N,A)

    ax = axisartist.Subplot(fig, 1,1,1)  
    fig.add_axes(ax)
    ax.axis[:].set_visible(False)
    ax.axis["x"] = ax.new_floating_axis(0,0)
    ax.axis["x"].set_axisline_style("-|>", size = 1.0)
    ax.axis["y"] = ax.new_floating_axis(1,0)
    ax.axis["y"].set_axisline_style("-|>", size = 1.0)
    ax.axis["x"].set_axis_direction("top")
    ax.axis["y"].set_axis_direction("right")
    ii+=1
    plt.scatter(S_others,S_l,s=0.05,alpha=1,c='red')
    plt.scatter(_S_others_,_S_l_,s=0.05,alpha=1,c='blue')#0.3 0.3
    k1=(1.0-A[0,1])/(1.0-A[1,0])
    _max1=np.sqrt(1+k1**2)
    line1 = [(-1/_max1,-k1/_max1), (1/_max1,k1/_max1)]
    k2=-(1+A[0,1])/(1+A[1,0])
    _max2=np.sqrt(1+k2**2)
    line2 = [(-1/_max2,-k2/_max2), (1/_max2,k2/_max2)]
    (line1_xs, line1_ys) = zip(*line1)
    (line2_xs, line2_ys) = zip(*line2)
    ax.add_line(Line2D(line1_xs, line1_ys, linewidth=1, color='black'))
    ax.add_line(Line2D(line2_xs, line2_ys, linewidth=1, color='black'))

    ax.spines['bottom'].set_linewidth(1)
    ax.spines['left'].set_linewidth(1)
    #ax.spines['bottom'].set_color('blue')
    #ax.spines['left'].set_color('blue')
    ax.spines['top'].set_color('none')
    ax.spines['right'].set_color('none')
    ax.spines['bottom'].set_position(('data', 0))
    ax.spines['left'].set_position(('data', 0))
    plt.annotate(s=r'${\widehat{\mathbf{s}}_2}^T$' ,xy=(1,0),xytext=(1.1,0) )
    plt.annotate(s=r'${\widehat{\mathbf{s}}_1}^T$' ,xy=(0,1),xytext=(0,1.1) )
    plt.annotate(s=r'$f_4({\widehat{\mathbf{s}}_2}^T)$' ,xy=(1/_max1,k1/_max1),xytext=(1/_max1,k1/_max1+0.1) )
    plt.annotate(s=r'$f_3({\widehat{\mathbf{s}}_2}^T)$' ,xy=(-1/_max2,-k2/_max2),xytext=(-1/_max2,-k2/_max2+0.1) )
    plt.axis([-1, 1,-1, 1])
    plt.xticks([])
    plt.yticks([])
    plt.grid(linestyle=':')
    plt.show()
