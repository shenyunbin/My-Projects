import numpy as np
import SigGenerator
import SigMixer
import SigTester
import Timer
import fast_bss 

class Result:
    def __init__(self,name="NA"):
        self.SNR=[]
        self.time=[]
        self.count=1
        self.name=name
    def ShowReport(self):
        print('\n>> No.', self.count, ' Report of '+ self.name + ':')
        print('    Average time: ', np.mean(self.time), " s")
        print('    Average SNR: ', np.mean(self.SNR), " dB")
        print('    Var: ', np.var(self.time), " ms")



if __name__ == '__main__':
    # initialize
    # signal Generator
    Gen = SigGenerator.SigGenerator(1000, 0.5)
    # signal mixer
    Mix = SigMixer.SigMixer()
    # signal tester
    Tst = SigTester.SigTester()
    #CICA
    Ufica = fast_bss.UltraFastICA()
    
    ###########################################################################################
    #####    USER-DEFINED DATA   ### ##########################################################
    ###########################################################################################
    #source number
    source_number=8
    #extraction interval determination
    _ext_initial_matrix,_ext_multi_ica = 30,70
    #use the optimization or not
    optimization_enable=False
    ###########################################################################################
    ###########################################################################################
    ###########################################################################################
    
    # get the signals from wavs
    freq, Signals = Gen.GetWavs2(
                'D:/库/文档/GitHub/fast_bss/Code/wavs/arctic_4s (', ').wav', range(1,source_number+1))
    MixSignals, MixMatrix = Mix.XGetMixSig(Signals, MixType="random_limit", Max=1.00, Min=0.5)
    #optimization
    if optimization_enable:
        _ext_initial_matrix,_ext_multi_ica = Ufica.UficaOptimization(MixSignals)
    #results and modes definition
    Results = [[],[],[],[]]
    Modes = ['fastica','cdica','multiica','ufica']
    for i in range(4):
        Results[i]=Result(Modes[i])
    #test repetitions
    for i in range(1):
        for ii in range(20):
            MixSignals, MixMatrix = Mix.XGetMixSig(Signals, MixType="random_normal", Max=0, Min=0.033)
            for iii in range(4):
                #Timer
                T5 = Timer.Timer()
                T5.Start()
                B_arica = Ufica.ufica(MixSignals,mode=Modes[iii],ext_initial_matrix=_ext_initial_matrix,ext_multi_ica=_ext_multi_ica)
                T5.Stop()
                #storage the results
                Results[iii].time.append(T5.Value())
                S1=np.dot(B_arica,MixSignals)
                Results[iii].SNR.append(Tst.GetSigSNR_PLUS(np.abs(Signals), np.abs(S1)))
            

    for i in range(4):
        Results[i].ShowReport()