import numpy as np
import math
import time

'''
//ULTRA-FAST ICA//////////////////////////////////////////
/////////////////////////////////////////////////////////
DEFINITION:
    S:SIGNALS
    X:OBTAINED MIXED SIGNALS
    A:MIXING MATRIX
    B:SEPARATION MATRIX
NOTES:
    X=A@S
    S=B@X
    B=A^-1
STEPS:
    0.PARAMETERS FOR UFICA OPTIMIZATION
    1.MIXING MATRIX ESTIMATION -> OUTPUT A1
    2.MULTI GRAD FAST ICA BASED ON EXTRACTED SIGNAL UND A1 -> OUTPUT B2
/////////////////////////////////////////////////////////
'''
class UltraFastICA():

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
        return gx, g_x.sum(axis=-1)

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
    def generateInitialMixingMatrix(self,V,A,n):
        if A is None:
            B = np.random.random_sample((n,n)) 
        else:
            B = np.linalg.inv(np.dot(V, A))
        try:
            return self.decorrelation(B)
        except:
            print('Initial Matrix Error!')
        else:
            return self.generateInitialMixingMatrix(None,None,n)

    #the iteration part of original fastica
    def newtonIteration(self,B,X,tol,max_iter,test_mode=False):
        g = self._tanh
        for i in range(max_iter):
            gbx, g_bx = g(np.dot(B,X))
            B1 = self.decorrelation(np.dot(gbx, X.T) - g_bx[:, None] * B)
            lim = max(abs(abs(np.diag(np.dot(B1, B.T))) - 1))
            B = B1
            if lim < tol:
                break
        if test_mode:
            return i,lim
        else:
            return B,lim

    #ica based on signal amplitude ratios version 6 optimal
    def mixingMatrixEstimation(self, input_signals, _ext_initial_matrix, test_mode=False):
        m,n = input_signals.shape
        sampling_interval = np.int(_ext_initial_matrix)
        _signals = input_signals[:,::sampling_interval] if sampling_interval else input_signals
        m,n = _signals.shape
        A = np.zeros([m, m])
        indexs_max_abs = np.nanargmax(np.abs(_signals), axis=-2)
        indexs_max_abs_positive = np.where(_signals[indexs_max_abs,np.arange(n)]>0,indexs_max_abs,np.nan)
        if test_mode:
            count_min=np.inf
            for y in range(m):
                _count=np.argwhere(indexs_max_abs_positive==y).shape[0]
                if _count<count_min:
                    count_min=_count
            return count_min
        else:
            for y in range(m):
                selected_indexs=np.argwhere(indexs_max_abs_positive==y)
                if selected_indexs.shape[0]<5:
                    return None
                A[:,[y]] = np.sum(_signals[:,selected_indexs], axis=-2)
            A = np.dot(A, np.diag(1/np.max(A, axis=-1)))
            A = np.clip(A, 0.01, None)
            return A

    #multi stage ica with a initial matrix A
    def multiLevelICA(self, Signals, A, max_iter, tol, _ext_multi_ica):
        return np.dot(B, V)

    #original FastICA with initial mixing matrix A
    def originalFastICAWithA(self,input_signals,A,max_iter=200, tol=1e-04):
        X, V = self.whiten(input_signals)
        n, m = X.shape
        X *= np.sqrt(m)
        B = self.generateInitialMixingMatrix(V,A,n)
        B,_tol = self.newtonIteration(B,X,tol,max_iter)
        return np.dot(B,V)

    #the original fastica
    def originalFastICA(self,input_signals,max_iter=200, tol=1e-04,test_mode=False):
        X, V = self.whiten(input_signals)
        n, m = X.shape
        X *= np.sqrt(m)
        B = self.generateInitialMixingMatrix(V,None,n)
        B,_tol = self.newtonIteration(B,X,tol,max_iter)
        estimated_signals=np.dot(B,X)
        if test_mode:
            return estimated_signals
        else:
            return np.dot(B,V)

    #optimzation for extraction intervals
    def UficaOptimization(self,input_signals,A=None,max_iter=10, tol=1e-04, 
            initial_w_precision=20, sample_size=30, max_extraction=100,step=5):
        #generate estimated signal with orignal fastica
        estimated_signals=self.originalFastICA(input_signals,test_mode=True)
        n, m = estimated_signals.shape
        if m/n<=max_extraction:
            raise ValueError('The amount of input data is too small to use ufica!')
        _exts=np.arange(step,max_extraction,step)
        #optimization of extraction interval for initial separation matrix estimation
        ext_initial_matrix=0
        for i in _exts:
            _ext=i
            count_min=0 
            for j in range(sample_size):
                mixing_matrix=np.random.random_sample((n,n))
                generated_signals=np.dot(mixing_matrix, estimated_signals)
                count_min+=self.mixingMatrixEstimation(generated_signals, _ext, test_mode=True)
            if (count_min/sample_size)<initial_w_precision:
                break
            else:
                ext_initial_matrix=_ext
            print('Extraction interval optimization - initial matrix estimation:    ', _ext, count_min/sample_size)

        #optimization for maximum extraction interval for multi-level ica
        total_times=np.array([])
        for i in _exts:
            _ext=i
            _total_time=0
            for j in range(sample_size):  
                mixing_matrix=np.random.random_sample((n,n)) 
                generated_signals=np.dot(mixing_matrix, estimated_signals)
                _start_time=time.time()
                self.ufica(generated_signals, ext_initial_matrix=ext_initial_matrix, ext_multi_ica=_ext)
                _total_time+=(time.time()-_start_time)
            total_times=np.hstack((total_times,_total_time))
            print('Extraction interval optimization - multi-level ica:    ', _ext, _total_time)
        p2=np.polyfit(_exts,total_times,2)
        total_times=np.polyval(p2,_exts)
        ext_multi_ica=_exts[np.argwhere(total_times==np.min(total_times))][0,0]
        print('Extraction interval optimization finished! \
            - initial matrix estimation , multi-level ica = ',ext_initial_matrix,ext_multi_ica)
        return ext_initial_matrix,ext_multi_ica

    #ultra-fast ica############################################
    def ufica(self,input_signals,mode='ufica', max_iter=200, tol=1e-04,
            ext_initial_matrix=0, ext_multi_ica=100):
        if mode == 'ufica':
            A1 = self.mixingMatrixEstimation(input_signals, ext_initial_matrix)
            B2 = self.multiLevelICA(input_signals, A1, max_iter, tol, ext_multi_ica)
        elif mode == 'multiica':
            A1 = None
            B2 = self.multiLevelICA(input_signals, A1, max_iter, tol, ext_multi_ica)
        elif mode == 'cdica':
            A1 = self.mixingMatrixEstimation(input_signals, ext_initial_matrix)
            B2 = self.originalFastICAWithA(input_signals, A1, max_iter, tol)
        elif mode == 'fastica':
            B2 = self.originalFastICA(input_signals, max_iter, tol)
        return B2