# -*- encoding:utf-8 -*-
import sys
import math
import numpy as np
from copy import deepcopy

import psutil
from psutil._common import bytes2human
import time


class Ica():
    def whiten(self, X):
        X = X - X.mean(-1)[:, None]
        A = np.dot(X, X.T)
        D, P = np.linalg.eig(A)
        D = np.diag(D)
        D_inv = np.linalg.inv(D)
        D_half = np.sqrt(D_inv)
        V = np.dot(D_half, P.T)
        return np.dot(V, X), V

    def _tanh(self, x):
        gx = np.tanh(x)
        g_x = gx ** 2
        g_x -= 1
        g_x *= -1
        return gx, g_x.mean(-1)#返回gx=tanh(x)和每一个x对应的g_x=tanh(x)'平均值?

    def _exp(self, x):
        exp = np.exp(-(x ** 2) / 2)
        gx = x * exp
        g_x = (1 - x ** 2) * exp
        return gx, g_x.mean(axis=-1)

    def _cube(self, x):
        return x ** 3, (3 * x ** 2).mean(axis=-1)

    def decorrelation(self, W):
        U, S = np.linalg.eigh(np.dot(W, W.T))
        U = np.diag(U)
        U_inv = np.linalg.inv(U)
        U_half = np.sqrt(U_inv)
        rebuild_W = np.dot(np.dot(np.dot(S, U_half), S.T), W)
        return rebuild_W

    # fastICA
    def fastica(self, Signals, fun='tanh',max_iter=200, tol=1e-04):

        X,V = self.whiten(Signals)

        n, m = X.shape
        p = float(m)
        if fun == 'tanh':
            g = self._tanh
        elif fun == 'exp':
            g = self._exp
        elif fun == 'cube':
            g = self._cube
        else:
            raise ValueError('The algorighm does not '
                             'support the support the user-defined function.'
                             'You must choose the function in (`tanh`, `exp`, `cube`)')

        X *= np.sqrt(X.shape[1])

        W = np.ones((n, n), np.float32)
        for i in range(n):
            for j in range(n):
                W[i, j] = np.random.random()

        # 迭代计算W
        for i in range(max_iter):
            gwtx, g_wtx = g(np.dot(W, X))
            W1 = self.decorrelation(np.dot(gwtx, X.T) / p - g_wtx[:, None] * W)#后面部分为减去平均值，中心化
            lim = max(abs(abs(np.diag(np.dot(W1, W.T))) - 1))
            #print(sum(abs(abs(np.diag(np.dot(W1, W.T))) - 1)))
            W = W1
            if lim < tol:
                break
        #print("iter_num1",i)
        #print("W1WWWW\n:",W)
        S=np.dot(np.dot(W, V), Signals)
        return V,W,S

     # fastICA
    def fastica_spltest(self, spl_interval, Signals, fun='tanh',max_iter=200, tol=1e-04):

        X,V = self.whiten(Signals)

        n, m = X.shape
        p = float(m)
        if fun == 'tanh':
            g = self._tanh
        elif fun == 'exp':
            g = self._exp
        elif fun == 'cube':
            g = self._cube
        else:
            raise ValueError('The algorighm does not '
                             'support the support the user-defined function.'
                             'You must choose the function in (`tanh`, `exp`, `cube`)')

        X *= np.sqrt(X.shape[1])

        X1=np.sqrt(spl_interval)*X[:, range(0, m, spl_interval)]

        W = np.ones((n, n), np.float32)
        for i in range(n):
            for j in range(n):
                W[i, j] = np.random.random()

        for i in range(max_iter):
            gwtx, g_wtx = g(np.dot(W, X1))
            W1 = self.decorrelation(np.dot(gwtx, X1.T) / p - g_wtx[:, None] * W)#后面部分为减去平均值，中心化
            lim = max(abs(abs(np.diag(np.dot(W1, W.T))) - 1))
            W = W1
            if lim < tol:
                break

        S=np.dot(np.dot(W, V), Signals)
        return V,W,S,i