import matplotlib.pyplot as plt
import numpy as np
import sys
import os
import time



def view(file_name,mode=True):
    
    f = open(file_name, 'r')
    __datas = eval(f.read())
    f.close()
    plt.close('all')
    if mode:
        plt.ion()
    fig = plt.figure(0)
    
    _max_snr = np.max(__datas['fastica']['snrs'])
    _max_time = np.max(__datas['fastica']['times'])

    for _ in range(len(__datas['fastica']['times'])):
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
    print('filename: ',file_name)
    if not mode:
        plt.show()


def round_view(mode=True):
    _filenames = os.listdir()
    filenames = []
    for filename in _filenames:
        if filename[-4:] == '.evl':
            filenames.append(filename)

    i = len(filenames) - 1
    while(i>=0):
        filename = filenames[i]
        view(filename,mode)
        
        if mode:
            cmd = input('command: ')
            if cmd == 'q':
                break
            elif cmd == '':
                i-=1
            elif cmd =='l':
                i+=1
        else:
            i-=1
        


if __name__ == '__main__':
    cmd = sys.argv[1]
    if cmd == '-all':
        round_view(True)
    if cmd == '-all2':
        round_view(False)
    else:
        view(cmd)
        input('press enter to exit')

