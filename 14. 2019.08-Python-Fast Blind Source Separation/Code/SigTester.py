import numpy as np
from scipy.stats import pearsonr
from sklearn import preprocessing

class SigTester:
    def GetCorr(self,a_signal,b_signal):
        #a_signal=a_signal/max(a_signal)
        #b_signal=b_signal/max(b_signal)
        #a_signal=preprocessing.scale(a_signal)
        #b_signal=preprocessing.scale(b_signal)
        return abs((np.corrcoef([a_signal,b_signal]))[0,1])
    
    def GetMaxCorrs(self,orginal_a_signals,b_signals,ordered_signal_output=True):
        V=np.diag(1/(np.max(abs(orginal_a_signals),axis=1)))
        orginal_a_signals=np.dot(V,orginal_a_signals)
        V=np.diag(1/(np.max(abs(b_signals),axis=1)))
        b_signals=np.dot(V,b_signals)
        #compared_data = preprocessing.scale(compared_data)
        #comparing_data = preprocessing.scale(comparing_data)
        corrs=[]
        ordered_b_signals=[]
        for a_signal in orginal_a_signals:
            corr=0
            for b_signal in b_signals:
                _corr=abs(self.GetCorr(a_signal,b_signal))
                if _corr>corr:
                    corr=_corr
                    ordered_b_signal=b_signal
            ordered_b_signals.append(ordered_b_signal)
            corrs.append(corr)
        if ordered_signal_output==True:           
            return np.asarray(corrs),np.asarray(ordered_b_signals)
        else:
            return np.asarray(corrs)
    
    def GetW(self,x):
        N=x.shape[1]
        W_inv=np.linalg.inv(x)
        _W=np.dot(np.diag(1/np.max(abs(W_inv), axis=0)),W_inv.T)
        indexs=np.argmax(abs(_W),axis=0)
        maxs=_W[range(N),indexs]
        W=np.empty([0,N])
        for i in indexs:
            W=np.vstack((W, np.sign(maxs[i])*_W[i,:]))
        return W

    def GetAformB_PLUS(self,B):
        A1=np.linalg.inv(B)
        A2=np.dot(A1,np.diag(1/np.max(abs(A1), axis=-1)))
        return A2

    def GetAFromB_amp(self,x):
        N=x.shape[1]
        W_inv=np.linalg.inv(x)
        _W=np.dot(np.diag(1/np.max(abs(W_inv), axis=-2)),W_inv.T)
        indexs=np.argmax(abs(_W),axis=-2)
        maxs=_W[range(N),indexs]
        W=np.empty([0,N])
        for i in indexs:
            W=np.vstack((W, np.sign(maxs[i])*_W[i,:]))
        return W


    def GetAFromB_corr(self,B,A_real):
        n=B.shape[1]
        A=np.linalg.inv(B)
        _A=np.dot(np.diag(1/np.max(abs(A), axis=0)),A.T)

        #corrs=np.cov(A[1,:],A_real[1,:])

        A_out=np.empty([0,n])
        for i in range(n):
            corr=0
            _j=0
            for j in range(n):
                _corr=np.cov(A[j,:],A_real[i,:])[1,0]
                if _corr>corr:
                    _j=j
                    corr=_corr
            A_out=np.vstack((A_out, _A[_j,:]))
        return A_out


    def GetWPSNR(self,orginal,test):
        #orginal=orginal/np.sum(orginal)
        #test=test/np.sum(test)
        VAR=0
        m=orginal.shape[0]
        for x in range(m):
            for y in range(m):
                noise=np.abs(test[x,y]-orginal[x,y])
                #signal=np.abs(orginal[x,y])
                VAR+=noise**2
        VAR=VAR/(m**2)
        PSNR=20*np.log10(1/VAR)
        return PSNR

    def GetWPSNR_PLUS(self,orginal,test):
        VAR=0
        m=orginal.shape[0]
        orginal_flat=np.sort(np.abs(np.hstack(orginal)))
        test_flat=np.sort(np.abs(np.hstack(test)))
        for x in range(m**2):
            noise=np.abs(test_flat[x]-orginal_flat[x])
            VAR+=noise**2
        VAR=VAR/(m**2)
        PSNR=10*np.log10(1/VAR)
        return PSNR

    def GetSigSNR(self,orginal,test):
        m,n=orginal.shape
        V=np.diag(1/(np.max(abs(orginal),axis=1)))
        orginal=np.dot(V,orginal)
        V=np.diag(1/(np.max(abs(test),axis=1)))
        test=np.dot(V,test)
        SNR=np.zeros(m)
        for x in range(m):
            noise=0
            signal=0
            for y in range(n):
                noise+=np.abs(test[x,y]-orginal[x,y])**2
                signal+=np.abs(orginal[x,y])**2
            SNR[x]=20*np.log10(signal/noise)
        return SNR.mean()
    def GetSigSNR2(self,orginal,test):
        m,n=orginal.shape
        V=np.diag(1/(np.max(abs(orginal),axis=1)))
        orginal=np.dot(V,orginal)
        V=np.diag(1/(np.max(abs(test),axis=1)))
        test=np.dot(V,test)
        SNR=np.zeros(m)
        for x in range(m):
            noise=0
            signal=0
            for y in range(n):
                noise+=np.abs(test[x,y]-orginal[x,y])**2
                signal+=np.abs(orginal[x,y])**2
            SNR[x]=20*np.log10(signal/noise)
        return SNR.mean()

    def GetSigSNR_PLUS(self,orginal_a_signals,b_signals):
        #print('corrcoef:',np.corrcoef(orginal_a_signals,b_signals))
        #temp_corrs,ordered_b_signals=self.GetMaxCorrs(orginal_a_signals,b_signals,True)
        Out=self.SNR2(orginal_a_signals, b_signals)
        return Out##self.GetSigSNR(orginal_a_signals,ordered_b_signals)#np.mean(temp_corrs)#

    def snr(self, compared_data, comparing_data):
        m,n=compared_data.shape
        V=np.diag(1/(np.mean(abs(compared_data),axis=1)))
        compared_data=np.dot(V,compared_data)
        V=np.diag(1/(np.mean(abs(comparing_data),axis=1)))
        comparing_data=np.dot(V,comparing_data)
        print('compared:',compared_data[0])
        print('comparing:',comparing_data[0])
        print('diffs:',compared_data[0]-comparing_data[0])

        compared_data=np.hstack(compared_data)
        comparing_data=np.hstack(comparing_data)
        #compared_data = preprocessing.scale(compared_data)
        #comparing_data = preprocessing.scale(comparing_data)
        A_sig = 0
        A_diff = 0

        for i in range(compared_data.__len__()):
            A_sig += abs(compared_data[i])
            A_diff += abs(abs(comparing_data[i]) - abs(compared_data[i]))
        snr = 20 * np.log10(A_sig / A_diff)
        return snr

    def SNR2(self, compared_data, comparing_data):
        m,n=compared_data.shape
        V=np.diag(1/(np.max(abs(compared_data),axis=1)))
        compared_data=np.dot(V,compared_data)
        V=np.diag(1/(np.max(abs(comparing_data),axis=1)))
        comparing_data=np.dot(V,comparing_data)

        #compared_data=preprocessing.scale(compared_data)
        #comparing_data=preprocessing.scale(comparing_data)

        A_sig = 0
        A_diff = 0
        for _compared_data in compared_data:
            _diff=[]
            for _comparing_data in comparing_data:
                _diff.append(np.sum(np.abs(np.abs(_compared_data)-np.abs(_comparing_data))))
            A_diff+=np.min(_diff)
            A_sig+=np.sum(np.abs(_compared_data))
        
        #compared_data=np.hstack(compared_data)
        #comparing_data=np.hstack(comparing_data)
        #compared_data = preprocessing.scale(compared_data)
        #comparing_data = preprocessing.scale(comparing_data)
        #A_sig = 0
        #A_diff = 0
        '''
        for i in range(compared_data.__len__()):
            A_sig += abs(compared_data[i])
            A_diff += abs(abs(comparing_data[i]) - abs(compared_data[i]))
        '''
        snr = 20 * np.log10(A_sig / A_diff)
        return snr

    def GetCorrs(self,orginal_a_signals,b_signals):
        corrs=[]
        ordered_b_signals=[]
        for a_signal in orginal_a_signals:
            corr=0
            for b_signal in b_signals:
                _corr=abs(pearsonr(a_signal,b_signal)[0])
                if _corr>corr:
                    corr=_corr
                    ordered_b_signal=b_signal
            ordered_b_signals.append(ordered_b_signal)
            corrs.append(corr)
        return np.mean(corrs)