import numpy as np
import SigGenerator as SigGen
import SigMixer as SigMix
import SigTester as SigTst
import SigIca as SigIca
import Timer
from matplotlib import pyplot as plt
from scipy.spatial import distance 
import mpl_toolkits.axisartist as axisartist

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

    diffs=np.array([])

    title="N8_sigma0-1"
    _mean=0.0
    _sourcenums=[2,8,16]
    _sigmas=np.arange(5,101,5)

    _cdica_diffs=[[],[],[]]
    _fastica_diffs=[[],[],[]]


    for ii in range(3):
        for _sigma in _sigmas:
            # get the signals from wavs1 5 11 18  / 15 20 25 30 , '14', '15', '21', '28'
            freq, Signals_N = Gen.GetWavs2(
                'D:/库/文档/GitHub/fast_bss/Code/wavs/arctic_4s (', ').wav', range(1,_sourcenums[ii]+1))
            m,n=Signals_N.shape
            MixSignals, A_real = Mix.XGetMixSig(Signals_N, MixType="random_normal_rd", Max=_mean, Min=_sigma*0.0033)
            #arica
            W_real=np.linalg.inv(A_real)
            A_arica=signalAmplitudeRatioICA(MixSignals)
            W_arica = np.linalg.inv(A_arica)
            diff=cosine_distance(W_arica,W_real)
            _cdica_diffs[ii].append(diff)
            #random for fast ica
            W_random = np.ones((m, m), np.float)
            for i in range(m):
                for j in range(m):
                    if i != j:
                        W_random[i, j] = np.random.random()
            A_random=np.linalg.inv(W_random)
            random_diff=cosine_distance(W_random,W_real)
            _fastica_diffs[ii].append(random_diff)
            #results


            print('now is ',_mean,_sigma)

    _cdica_diffs[2] = np.clip(_cdica_diffs[2], None, 0.5)
    x=_sigmas
    #x=means
    fig = plt.figure(figsize=(5, 5))
    ax = fig.add_subplot(1,1,1)
    _lw,_ms=0.8,4
    #ax.set_title("The cosine distance of Matrix A (mean=0,source=8)")
    plt.plot(x, _cdica_diffs[0], color='red', linestyle='-',marker='o', linewidth=_lw,ms=_ms, markerfacecolor='none')
    plt.plot(x, _cdica_diffs[1], color='red', linestyle='-',marker='s', linewidth=_lw,ms=_ms, markerfacecolor='none')
    plt.plot(x, _cdica_diffs[2], color='red', linestyle='-',marker='x', linewidth=_lw,ms=_ms, markerfacecolor='none')
    plt.plot(x, _fastica_diffs[0], color='blue', linestyle='-',marker='o', linewidth=_lw,ms=_ms, markerfacecolor='none')
    plt.plot(x, _fastica_diffs[1], color='blue', linestyle='-',marker='s', linewidth=_lw,ms=_ms, markerfacecolor='none')
    plt.plot(x, _fastica_diffs[2], color='blue', linestyle='-',marker='x', linewidth=_lw,ms=_ms, markerfacecolor='none')
    '''
    fmt = '%.0f%%'
    xticks = mtick.FormatStrFormatter(fmt)
    ax.xaxis.set_major_formatter(xticks)
    '''
    plt.axis([0, 101, 0.0, 1.0])
    
    plt.xlabel(r'$\Lambda\ /\ \%$')
    plt.ylabel(r'$\Delta$')
    plt.legend(['CdICA n=2',  'CdICA n=8', 'CdICA n=16',  'FastICA n=2', 'FastICA n=8',  'FastICA n=16'], loc='upper left')
    plt.grid(linestyle=':')
    plt.show()
