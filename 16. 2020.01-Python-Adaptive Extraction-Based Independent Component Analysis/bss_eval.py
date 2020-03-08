import time
import progressbar
import numpy as np
from pyfastbss_testbed import pyfbss_tb


class BssEvaluation():

    def __init__(self):
        self.V=None
        self.rec_start_time=0
        self.B=None
        self.diff=0
        self.ext_interval=0
        #self.time
        self.Vs=[]
        self.Bs=[]
        self.diffs=[]
        self.ext_intervals=[]
        self.times=[]
        self.snrs=[]

    def rec_start(self):
        self.__init__()
        self.rec_start_time = time.time()

    def record(self):
        recent_time = time.time()
        self.times.append(1000*(recent_time-self.rec_start_time))
        self.Bs.append(self.B)
        self.Vs.append(self.V)
        self.diffs.append(self.diff)
        self.ext_intervals.append(self.ext_interval)
        self.rec_start_time = self.rec_start_time+time.time()-recent_time

    def generate_results(self, S, X):
        print('Generate snr ...')
        for i in progressbar.progressbar(range(len(self.times))):
            if self.Vs[i] is not None:
                _B=np.dot(self.Bs[i],self.Vs[i])
            else:
                _B=self.Bs[i]
            hat_S=np.dot(_B,X)
            self.snrs.append(pyfbss_tb.bss_evaluation(S, hat_S,type='psnr'))
        return {'times':self.times,'ext_intervals':self.ext_intervals,'diffs':self.diffs,'snrs':self.snrs}

be = BssEvaluation()
