from pyfastbss_core import pyfbss,newaeica
from pyfastbss_testbed import pyfbss_tb
import progressbar
from aeica_algorithm_base import be,aeica1,aeica2,fastica
import datetime
import copy
import numpy as np
import matplotlib.pyplot as plt
import pickle


def save_obj(obj, name ):
    with open('obj/fig2/'+ name + '.pkl', 'wb') as f:
        pickle.dump(obj, f, pickle.HIGHEST_PROTOCOL)

def load_obj(name ):
    with open('obj/fig2/' + name + '.pkl', 'rb') as f:
        return pickle.load(f)


def bulid_dict_datas():
    D1={'times':[],'snrs':[]}
    D2={}
    for i in range(5,105,5):
        D2[str(i)]=copy.deepcopy(D1)
    D3 = {}
    for i in ['fastica','aeica1','aeica2']:
        D3[i]=copy.deepcopy(D2)
    return D3


def dict_to_arr(__datas, _srcs, _reps):
    _mods = ['fastica','aeica1','aeica2']
    _vals = ['times','snrs']
    _srcs = [str(value) for value in _srcs]
    n1,n2,n3,n4 = len(_mods),len(_vals),len(_srcs),len(_reps)
    _ds = np.zeros([n1,n2,n3,n4])
    for n1 in range(len(_mods)):
        for n2 in range(len(_vals)):
            for n3 in range(len(_srcs)):
                for n4 in range(len(_reps)):
                    if len(__datas[_mods[n1]][_srcs[n3]][_vals[n2]])>0:
                        _ds[n1][n2][n3][n4]=__datas[_mods[n1]][_srcs[n3]][_vals[n2]][_reps[n4]]
    return _ds





width,height=5,4
adj_l,adj_b=0.1,0.1


def ax_plot(ax, x, _Datas, color, linestyle,lw=0.8,ms=6,marker='o'):
    mean=np.mean(_Datas,axis=-1)
    std=np.std(_Datas,axis=-1)
    ax.plot(x,mean, color=color, linestyle=linestyle, linewidth=lw,ms=ms,marker=marker)
    ax.errorbar(x, mean, xerr=0, yerr=2.457*std/(30**0.5), color=color, linestyle=linestyle, linewidth=lw)


def plot_res_rate(x,Datas):
    Datas_1 = Datas[0][0]
    Datas_2 = Datas[2][0]
    fig = plt.figure(figsize=(width,height))

    ax1 = fig.add_subplot(111)
    #ax1.plot(x, y1)
    ax_plot(ax1,x,Datas_2,color='red', linestyle='-',ms=4,marker='+')
    ax_plot(ax1,x,Datas_1,color='black', linestyle='-',ms=4,marker='x')
    #ax1.set_ylim([0, 650])
    ax1.set_ylabel(r'$Time\ /\ ms$')
    ax1.set_xlabel(r'$Source\ Number\ /\ n$')
    ax1.legend([r'$AeICA$', r'$FastICA$'], loc='upper left',ncol=2)
    ax1.grid(linestyle=':')

    ax2 = ax1.twinx()  # this is the important function
    #ax2.plot(x, y2, 'r')
    ax_plot(ax2,x,Datas_2/Datas_1*100,color='blue', linestyle=':', ms=4,marker=None)
    ax2.set_xlim([0, 41])
    #ax2.set_ylim([0, 105])
    ax2.set_ylabel(r'$R_{t}\ /\ \%$')
    ax2.legend([r'$R_{t}$'], loc='upper right',ncol=1)
    ax2.grid(linestyle=':')

    
def plot_snr(x,Datas):
    Datas_1 = Datas[0][1]
    Datas_2 = Datas[2][1]

    fig=plt.figure(figsize=(width,height))
    ax1 = fig.add_subplot(111)
    ax_plot(ax1,x,Datas_2,color='red', linestyle='-',ms=4,marker='+')
    ax_plot(ax1,x,Datas_1,color='black', linestyle='-',ms=4,marker='x')
    ax1.set_xlim([0,41])
    #ax1.set_ylim([15,40])
    ax1.set_yscale('log')
    #ax1.set_yticks([20,25,30,35,40])
    ax1.set_xlabel(r'$Source\ Number\ /\ n$')
    ax1.set_ylabel(r'$log(SNR)\ /\ dB$')
    ax1.legend([r'$AeICA$', r'$FastICA$'], loc='upper left',ncol=2)
    ax1.grid(linestyle=':')
    plt.subplots_adjust(top=0.99, right=0.98, left=0.14,bottom=0.12)




if __name__ == '__main__':

    ###########################################################################################
    #####    USER-DEFINED DATA   ### ##########################################################
    ###########################################################################################
    # duration
    duration = 30
    # extraction interval determination
    _ext_multi_ica = 30
    # folder
    folder_address = '/Users/shenyunbin/Documents/Code/wavs/44100'
    # source numbers
    _srcs = range(5,45,5)
    # repeat numbers
    _reps = range(2)
    ###########################################################################################
    ###########################################################################################
    ###########################################################################################

    __datas = bulid_dict_datas()


    if False:
        for _rep in progressbar.progressbar(_reps):
            for _src in progressbar.progressbar(_srcs):
                _snr = 0
                while(_snr<25):
                    S, A, X = pyfbss_tb.generate_matrix_S_A_X(
                        folder_address, duration, _src, mixing_type="random", max_min=(1, 0.01), mu_sigma=(0, 0.333))
                    hat_S = fastica.fastica(X, max_iter=30, tol=1e-4)
                    _snr = pyfbss_tb.bss_evaluation(S, hat_S, type='psnr')
                    _snr =30
                    
                pyfbss_tb.timer_start()
                hat_S = fastica.fastica(X, max_iter=200, tol=1e-4)
                time = pyfbss_tb.timer_value()
                snr = pyfbss_tb.bss_evaluation(S, hat_S, type='psnr')
                __datas['fastica'][str(_src)]['times'].append(time)
                __datas['fastica'][str(_src)]['snrs'].append(snr)


                # pyfbss_tb.timer_start()
                # hat_S = aeica1.aeica(X, max_iter=200, tol=1e-4, ext_adapt_ica=_ext_multi_ica)
                # time = pyfbss_tb.timer_value()
                # snr = pyfbss_tb.bss_evaluation(S, hat_S, type='psnr')
                # __datas['aeica1'][str(_src)]['times'].append(time)
                # __datas['aeica1'][str(_src)]['snrs'].append(snr)

                pyfbss_tb.timer_start()
                hat_S = aeica2.aeica(X, max_iter=200, tol=1e-4, ext_adapt_ica=_ext_multi_ica)
                time = pyfbss_tb.timer_value()
                snr = pyfbss_tb.bss_evaluation(S, hat_S, type='psnr')
                __datas['aeica2'][str(_src)]['times'].append(time)
                __datas['aeica2'][str(_src)]['snrs'].append(snr)


        date_time = str(datetime.datetime.now().day) + str(datetime.datetime.now().hour) + str(datetime.datetime.now().minute)
        file_name = '__fig2_datas_2_'+date_time

        save_obj(__datas,file_name)
    else:
        file_name = '__fig2_datas_2_'+str(62259)

    __datas = load_obj(file_name)
    
    #mods, vals, srcs, reps
    _ds =  dict_to_arr(__datas, _srcs, _reps)

    plot_res_rate(_srcs,_ds)
    plot_snr(_srcs,_ds)
    plt.show()

