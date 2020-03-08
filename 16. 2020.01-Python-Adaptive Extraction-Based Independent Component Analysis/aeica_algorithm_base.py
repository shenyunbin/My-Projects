import numpy as np
import math
from bss_eval import be
from pyfastbss_testbed import pyfbss_tb
import matplotlib.pyplot as plt
import datetime
import copy
'''
# FAST BSS Version 0.1.0:

    fastica: FastICA (most stable)
    meica: Multi-level extraction ICA (stable)
    cdica: Component dependent ICA (stable)
    aeica: Adaptive extraction ICA (*warning: unstable!)
    ufica: Ultra-fast ICA (cdica + aeica) (*warning: unstable!)

# Basic definition:

    S: Source signals. shape = (source_number, time_slots_number)
    X: Mixed source signals. shape = (source_number, time_slots_number)
    A: Mixing matrix. shape = (source_number, source_number)
    B: Separation matrix. shape = (source_number, source_number)
    hat_S: Estimated source signals durch ICA algorithms. 
        shape = (source_number, time_slots_number)

# Notes:

    X = A @ S
    S = B @ X
    B = A ^ -1
'''


class FastbssBasic():

    def whiten(self, X):
        '''
        # whiten(self, X):

        # Usage:

            Whitening the mixed signals, i.e. matrix X. Let the 
            mixed signals X are uncorrelated with each other. 
            Meanwhile the variance of each mixed signal 
            (i.e. each channel) x is 1, which is the premise of
            standard normal distribution.

        # Parameters:

            X: Mixed signals, matrix X.

        # Output:

            X, V
            X: Whitened mixed signals X.
            V: Whitening matrix.
        '''
        X = X - X.mean(-1)[:, np.newaxis]
        A = np.dot(X, X.T)
        D, P = np.linalg.eig(A)
        D = np.diag(D)
        D_inv = np.linalg.inv(D)
        D_half = np.sqrt(D_inv)
        V = np.dot(D_half, P.T)
        m = X.shape[1]
        return np.sqrt(m)*np.dot(V, X), V

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
        return gx, g_x.sum(axis=-1)

    def _cube(self, x):
        return x ** 3, (3 * x ** 2).sum(axis=-1)

    def decorrelation(self, B):
        '''
        # decorrelation(self, B):

        # Usage:

            Decorrelate the signals. Let each signal (i.e. channel) of 
            B@X is uncorrelated with each other.

        # Parameters:

            B: The estimated separation matrix B.

        # Output:

            Decorrelated separation matrix B.
        '''
        U, S = np.linalg.eigh(np.dot(B, B.T))
        U = np.diag(U)
        U_inv = np.linalg.inv(U)
        U_half = np.sqrt(U_inv)
        rebuild_B = np.dot(np.dot(np.dot(S, U_half), S.T), B)
        return rebuild_B

    def generate_initial_matrix_B(self, V, A=None):
        '''
        # decorrelation(self, B):

        # Usage:

            Generate the intial separation matrix for newton iteration.

        # Parameters:

            V: The whitening matrix, also used for getting the number of 
                the original sources. 
            A: The estimated mixing matrix. Then, the initial matrix B is 
                (V @ A)^-1. When the value of A is None, this function 
                will return a random matrix B, its size is according to
                the shape of matirx V.

        # Output:

            Initial separation matrix B. 
        '''
        n = np.shape(V)[0]
        if A is None:
            B = np.random.random_sample((n, n))
        else:
            B = np.linalg.inv(np.dot(V, A))
        try:
            return self.decorrelation(B)
        except:
            raise SystemError(
                'Error - initial matrix generation : unkown, please try it again!')
        else:
            return self.generate_initial_matrix_B(V)

    def _tieration(self, B, X):
        '''
        # _tieration(self, B, X):

        # Usage:

            Basic part of newton iteration for BSS.

        # Parameters:

            B: Separation matrix.
            X: Whitened mixed signals.

        # Output:

            Updated separation matrix B.
        '''
        gbx, g_bx = self._tanh(np.dot(B, X))
        B1 = self.decorrelation(np.dot(gbx, X.T) - g_bx[:, None] * B)
        lim = max(abs(abs(np.diag(np.dot(B1, B.T))) - 1))
        # print(lim)
        return B1, lim

    def newton_iteration(self, B, X, max_iter, tol):
        '''
        # newton_iteration(self, B, X, max_iter, tol):

        # Usage:

            Newton iteration part for BSS, the iteration jumps out
            when the convergence is smaller than the determined
            tolerance.

        # Parameters:

            B: Separation matrix.
            X: Whitened mixed signals.
            max_iter: Maximum number of iteration.
            tol: Tolerance of the convergence of the matrix B 
                calculated from the last iteration and the 
                matrix B calculated from current newton iteration.

        # Output:

            B,lim
            B: Separation matrix B.
            lim: Convergence of the iteration.
        '''
        for _ in range(max_iter):
            B, lim = self._tieration(B, X)

            # bss eval 4 -------------------------------------------
            be.B = B
            be.diff = lim
            be.record()

            if lim < tol:
                break
        return B, lim


class FastICA(FastbssBasic):

    def fastica(self, X, max_iter=100, tol=1e-04):
        '''
        # fastica(self, X, max_iter=100, tol=1e-04):

        # Usage:

            Original FastICA.

        # Parameters:

            B: Separation matrix.
            X: Whitened mixed signals.
            max_iter: Maximum number of iteration.
            tol: Tolerance of the convergence of the matrix B 
                calculated from the last iteration and the 
                matrix B calculated from current newton iteration.

        # Output:

            Estimated source signals matrix S.
        '''

        # bss eval 1 -------------------------------------------
        be.rec_start()

        X, V = self.whiten(X)

        # bss eval 2 -------------------------------------------
        be.V = V

        B1 = self.generate_initial_matrix_B(V)

        # bss eval 3 -------------------------------------------
        be.ext_interval = 1

        B2 = self.newton_iteration(B1, X, max_iter, tol)[0]
        S2 = np.dot(B2, X)
        return S2


fastica = FastICA()


class AdapiveExtractionICA(FastbssBasic):

    def adaptive_extraction_iteration(self, X, B, max_iter, tol, _ext_adapt_ica):
        '''
        # adaptive_extraction_iteration(self, X, B, max_iter, tol, _ext_adapt_ica):

        # Usage:

            Adaptive extraction newton iteration.
            It is a combination of several fastica algorithm with different partial
            signals, which is extracted by different intervals. the extraction 
            interval can be detemined by the convergence of the iteration.

        # Parameters:

            X: Mixed signals, which is obtained from the observers.
            max_iter: Maximum number of iteration.
            tol: Tolerance of the convergence of the matrix B 
                calculated from the last iteration and the 
                matrix B calculated from current newton iteration.
            _ext_adapt_ica: The intial and the maximum extraction interval of the 
                input signals.

        # Output:

            Estimated source separation matrix B.
        '''
        ####################################################
        ####################################################
        return B

    def aeica(self, X, max_iter=100, tol=1e-04, ext_adapt_ica=20):
        '''
        # aeica(self, X, max_iter=100, tol=1e-04, ext_adapt_ica=30):

        # Usage:

            Adaptive extraction ICA.
            It is a combination of several fastica algorithm with different partial
            signals, which is extracted by different intervals. the extraction 
            interval can be detemined by the convergence of the iteration.
            A original fastica is added at the end, in order to get the best result.

        # Parameters:

            X: Mixed signals, which is obtained from the observers.
            max_iter: Maximum number of iteration.
            tol: Tolerance of the convergence of the matrix B 
                calculated from the last iteration and the 
                matrix B calculated from current newton iteration.
            _ext_adapt_ica: The intial and the maximum extraction interval of the 
                input signals.

        # Output:

            Estimated source signals matrix S.
        '''

        # bss eval 1 ----------------------------------
        be.rec_start()

        X, V = self.whiten(X)

        # bss eval 2 ----------------------------------
        be.V = V

        B1 = self.generate_initial_matrix_B(V)
        B2 = self.adaptive_extraction_iteration(
            X, B1, max_iter, tol, ext_adapt_ica)

        # bss eval 3 -------------------------------------------
        be.ext_interval = 1
        #be.V = None

        B3 = self.newton_iteration(B2, X, max_iter, tol)[0]
        S3 = np.dot(B3, X)
        return S3


aeica1 = AdapiveExtractionICA()


class AdapiveExtractionICA2(FastbssBasic):

    def whiten_with_inv_V(self, X):
        '''
        # whiten_with_inv_V(self, X):

        # Usage:

            Whitening the mixed signals, i.e. matrix X. Let the 
            mixed signals X are uncorrelated with each other. 
            Meanwhile the variance of each mixed signal 
            (i.e. each channel) x is 1, which is the premise of
            standard normal distribution.

        # Parameters:

            X: Mixed signals, matrix X.

        # Output:

            X, V, V_inv
            X: Whitened mixed signals X.
            V: Whitening matrix.
            V_inv: The inverse of the whitening matrix V.
        '''
        X = X - X.mean(-1)[:, np.newaxis]
        A = np.dot(X, X.T)
        D, P = np.linalg.eig(A)
        D = np.diag(D)
        D_inv = np.linalg.inv(D)
        D_half = np.sqrt(D_inv)
        V = np.dot(D_half, P.T)
        m = X.shape[1]
        V_inv = np.dot(P, np.sqrt(D))
        return np.sqrt(m)*np.dot(V, X), V, V_inv

    def adaptive_extraction_iteration(self, X, B, max_iter, tol, break_coef, ext_adapt_ica):
        '''
        # adaptive_extraction_iteration(self, X, B, max_iter, tol, _ext_adapt_ica):

        # Usage:

            Adaptive extraction newton iteration.
            It is a combination of several fastica algorithm with different partial
            signals, which is extracted by different intervals. the extraction 
            interval can be detemined by the convergence of the iteration.

        # Parameters:

            X: Mixed signals, which is obtained from the observers.
            max_iter: Maximum number of iteration.
            tol: Tolerance of the convergence of the matrix B 
                calculated from the last iteration and the 
                matrix B calculated from current newton iteration.
            _ext_adapt_ica: The intial and the maximum extraction interval of the 
                input signals.

        # Output:

            Estimated source separation matrix B.
        '''
        ####################################################
        ####################################################
        return B

    def aeica(self, X, max_iter=100, tol=1e-04, break_coef=0.9, ext_adapt_ica=50):
        '''
        # aeica(self, X, max_iter=100, tol=1e-04, ext_adapt_ica=30):

        # Usage:

            Adaptive extraction ICA.
            It is a combination of several fastica algorithm with different partial
            signals, which is extracted by different intervals. the extraction 
            interval can be detemined by the convergence of the iteration.
            A original fastica is added at the end, in order to get the best result.

        # Parameters:

            X: Mixed signals, which is obtained from the observers.
            max_iter: Maximum number of iteration.
            tol: Tolerance of the convergence of the matrix B 
                calculated from the last iteration and the 
                matrix B calculated from current newton iteration.
            _ext_adapt_ica: The intial and the maximum extraction interval of the 
                input signals.

        # Output:

            Estimated source signals matrix S.
        '''

        # bss eval 1 ----------------------------------
        be.rec_start()
        be.V = None

        B1 = self.generate_initial_matrix_B(X)
        B2 = self.adaptive_extraction_iteration(
            X, B1, max_iter, tol, break_coef, ext_adapt_ica)
        S2 = np.dot(B2, X)
        return S2


aeica2 = AdapiveExtractionICA2()
