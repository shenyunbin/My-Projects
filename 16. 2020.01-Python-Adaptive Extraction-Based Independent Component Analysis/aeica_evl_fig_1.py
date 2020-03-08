import numpy as np
import math
from bss_eval import be
from pyfastbss_testbed import pyfbss_tb
import matplotlib.pyplot as plt
import datetime
import copy
from aeica_algorithm_base import be,aeica1,aeica2,fastica



if __name__ == '__main__':

    ###########################################################################################
    #####    USER-DEFINED DATA   ### ##########################################################
    ###########################################################################################
    # duration, source number
    duration, source_number = 30, 50
    # extraction interval determination
    _ext_multi_ica = 30
    # folder
    folder_address = '/Users/shenyunbin/Documents/Code/wavs/44100'
    ###########################################################################################
    ###########################################################################################
    ###########################################################################################
    if True:
        # get the signals from wavs
        
        S, A, X = pyfbss_tb.generate_matrix_S_A_X(
            folder_address, duration, source_number, mixing_type="random", max_min=(1, 0.01), mu_sigma=(0, 0.333))
        # results and modes definition
        __datas = {'fastica': None, 'aeica': None}

        pyfbss_tb.timer_start()
        hat_S = aeica1.aeica(X, max_iter=200, tol=1e-4, ext_adapt_ica=_ext_multi_ica)
        time_aeica = int(pyfbss_tb.timer_value())
        print('aeica(ms): ', time_aeica)
        print('expected snr: ',pyfbss_tb.bss_evaluation(S, hat_S, type='psnr'))
        be1 = copy.deepcopy(be)

        pyfbss_tb.timer_start()
        hat_S = fastica.fastica(X, max_iter=200, tol=1e-4)
        time_fastica = int(pyfbss_tb.timer_value())
        print('fastica(ms): ', time_fastica)
        print('expected snr: ',pyfbss_tb.bss_evaluation(S, hat_S, type='psnr'))
        be2 = copy.deepcopy(be)

        __datas['aeica'] = be1.generate_results(S, X)
        __datas['fastica'] = be2.generate_results(S, X)
        
        date_time = str(datetime.datetime.now().day) + str(datetime.datetime.now().hour)
        file_name = '__datas2_3'+date_time+'_'+str(time_aeica)+'_'+str(time_fastica)+'.evl'

        f = open(file_name, 'w')
        f.write(str(__datas))
        f.close()

    f = open(file_name, 'r')
    __datas = eval(f.read())
    f.close()

    fig = plt.figure(figsize=(5, 8))
    #plt.title('Source Number is '+str(source_number))
    _max_snr = np.max(__datas['fastica']['snrs'])
    _max_time = np.max(__datas['fastica']['times'])

    for i in range(len(__datas['fastica']['times'])):
        ax1 = fig.add_subplot(411)
        ax1.scatter(__datas['aeica']['times'], 100*np.array(__datas['aeica']
                                                            ['snrs'])/_max_snr, c='red', s=12, marker='+')
        ax1.scatter(__datas['fastica']['times'], 100*np.array(
            __datas['fastica']['snrs'])/_max_snr, c='black', s=12, marker='x')

        ax2 = fig.add_subplot(412)
        ax2.scatter(__datas['aeica']['times'], 100/np.array(__datas['aeica']
                                                            ['ext_intervals']), c='red', s=12, marker='+')
        ax2.scatter(__datas['fastica']['times'], 100/np.array(
            __datas['fastica']['ext_intervals']), c='black', s=12, marker='x')

        ax3 = fig.add_subplot(413)
        ax3.scatter(__datas['aeica']['times'], np.array(
            __datas['aeica']['diffs']), c='red', s=12, marker='+')
        ax3.scatter(__datas['fastica']['times'], np.array(
            __datas['fastica']['diffs']), c='black', s=12, marker='x')

        ax4 = fig.add_subplot(414)
        ax4.scatter(__datas['aeica']['times'], np.array(
            __datas['aeica']['ext_intervals']), c='red', s=12, marker='+')
        ax4.scatter(__datas['fastica']['times'], np.array(
            __datas['fastica']['ext_intervals']), c='black', s=12, marker='x')

    ax1.set_ylabel(r"$R_{SNR}\ /\ \%$")
    ax2.set_ylabel(r"$R_{data}\ /\ \%$")
    ax3.set_ylabel(r"$\Delta$")
    ax4.set_ylabel(r"$\mu_{k}$")

    ax1.set_xticklabels([])
    ax2.set_xticklabels([])
    ax3.set_xticklabels([])
    ax4.set_xlabel(r"$Time\ /\ ms$")

    ax1.legend(['AeICA', 'FastICA'], loc='upper left', ncol=2)

    ax1.grid(linestyle=':')
    ax2.grid(linestyle=':')
    ax3.grid(linestyle=':')
    ax4.grid(linestyle=':')

    plt.subplots_adjust(top=0.99, right=0.99, left=0.12, bottom=0.12)

    plt.show()
