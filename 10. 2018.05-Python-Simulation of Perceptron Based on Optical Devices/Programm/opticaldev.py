##upgrade at 16:14 31.07.18 version 9.0 ( add realteil output )
#normal input only with feld !!! only powerinput must inputed by power !!!
#all the caculation are based on feld! 


import math
import cmath
import numpy as np
from mpl_toolkits.mplot3d import art3d
from matplotlib import pyplot as plt




class microring():
    #defination------------------------------------------------------------------------------
    def __init__(self,radius=10e-4,alpha=2,phi_0=-0.0481):
        #power coupling ratio
        self.A=0.5
        #intensity-dependent refractive
        self.n2=7.5e-14 #cm2/W
        self.Aeff=1.2e-10 #cm2
        #original phase shift
        self.phi_0=phi_0
        #the power attenuation coefficient
        self.alpha=alpha #dB/cm 2.4
        #self.radius of ring
        self.radius=radius #cm  4e-4   we have changed the self.radius
        #resonance wave length
        self.lambda_freesp=1.55e-4 #cm
        #length of the ring
        self.L=2*cmath.pi*self.radius
        #a is single-pass amplitude transmission
        self.a=math.exp(-self.alpha*self.L)**0.5
        self.Emultiple=0.133*self.Aeff
    #change the value of microring
    def setmrrvalue(self,A=0.5):
        self.A=A
    #Power transform to E
    def powertofeld(self,power):
        if power is None:
            return None
        elif power==0:
            return 0
        else:
            E2=power*1.0/self.Emultiple
            return E2/math.sqrt(abs(E2))
    #E tansform to Power
    def feldtopower(self,feld):
        if feld is None:
            return None
        else:
            return self.Emultiple*abs(feld)*feld
    #set and change the value r radius alpha phi_0
    def setvalue(self,r,radius,alpha,phi_0):
        microring.__init__(r,radius,alpha,phi_0)
    #caculation---------------------------------------------------------------------------------
    #phi is single-pass phase shift ========> equation (4) page 1265
    def PHI(self,r,E1):
        P1=self.Emultiple*abs(E1)**2
        a1=self.a*r
        a2=-self.a*r*self.phi_0
        a3=(1-self.a*r)**2
        a4=-((1-self.a*r)**2)*self.phi_0 - 2*cmath.pi*self.L*self.n2 / (self.lambda_freesp*self.A) * self.a**2 * (1-r**2)*P1/self.Aeff
        arg=[a1,a2,a3,a4]
        roots=np.roots(arg)
        #print(roots)
        if (np.iscomplex(roots[0])==False and np.iscomplex(roots[1])==False and np.iscomplex(roots[2])==False):
            return np.max(roots), np.min(roots), 1
        elif (np.iscomplex(roots[0])==False):
            #print "true0"
            return roots[0], None, 0
        elif (np.iscomplex(roots[1])==False):
            #print "true1"
            return roots[1], None, 0
        else:
            #print "true2"
            return roots[2], None, 0  #0, 0, state
    #def t(phi(p))=E1/E2 ========> equation (5) page 1265
    def t(self,r,phi):
        return cmath.exp((cmath.pi+phi)*1j) * ( self.a-r*cmath.exp(-1j*phi) ) / ( 1-self.a*r*cmath.exp(1j*phi) )
    #Power change through the Microring resonator
    def MRR_Transf(self,r,E_in):
        phif,phis,state=self.PHI(r,E_in)
        if (state==1):
            t1=self.t(r,phif)
            t2=self.t(r,phis)
        else:
            t1=self.t(r,phif)
            t2=0
        E1=(t1*E_in)
        E2=(t2*E_in)
        return E1,E2





class coupler():

    def __init__(self,theta=-0.25*math.pi):
        #power coupling ratio
        self.theta=theta
        self.Aeff=1.2e-10 #cm2
        self.Emultiple=0.133*self.Aeff
    #Power changement through the coupler
    def input(self,P1,P2,theta):
        costheta= math.cos(theta)
        sintheta= math.sin(theta)
        Pout1=costheta*P1-sintheta*P2
        Pout2=sintheta*P1+costheta*P2
        return Pout1,Pout2
    #set power coupling ratio
    def setvalue(self,theta):
        self.__init__(theta)
        #Power transform to E
    def powertofeld(self,power):
        if power is None:
            return None
        elif power==0:
            return 0
        else:
            E2=power*1.0/self.Emultiple
            return E2/math.sqrt(abs(E2))
    #E tansform to Power
    def feldtopower(self,feld):
        if feld is None:
            return None
        else:
            return self.Emultiple*abs(feld)*feld
    #def special transform
    def sinput(self,P1,P2,theta=0):
        theta=self.theta if theta==0 else theta
        costheta= math.cos(theta)
        sintheta= math.sin(theta)
        Pout1=costheta*P1-sintheta*P2
        Pout2=sintheta*P1+costheta*P2
        return Pout1,Pout2
    def s1input(self,P1,P2,theta=0):
        theta=self.theta if theta==0 else theta
        costheta= math.cos(theta)
        sintheta= math.sin(theta)
        Pout1=costheta*P1-sintheta*P2
        return Pout1
    def s2input(self,P1,P2,theta=0):
        theta=self.theta if theta==0 else theta
        costheta= math.cos(theta)
        sintheta= math.sin(theta)
        Pout2=sintheta*P1+costheta*P2
        return Pout2
    def th_EtoP_s1input(self,P1,P2,theta=0):
        if P2==0:
            return None
        elif P1==0:
            return None
        else:
            theta=self.theta if theta==0 else theta
            costheta= math.cos(theta)
            sintheta= math.sin(theta)
            Pout1=costheta*P1-sintheta*P2
            return abs(self.feldtopower(Pout1))

    




class amplifier(microring): 

    #defination------------------------------------------------------------------------------
    def __init__(self,r=0.966,radius=10e-4,alpha=2,phi_0=-0.0481):
        self.bi_mode=0
        #add a coupler
        self.coupler=coupler()
        #self-coupling coefficient, r1=r2 hier 0.979
        self.r=r
        #init of class microring
        microring.__init__(self,radius,alpha,phi_0)
    #set and change the value r radius alpha phi_0
    def setvalue(self,r,radius,alpha,phi_0):
        self.__init__(r,radius,alpha,phi_0)
    #def Input Power X ( small signal ) and W ( weight/gain )
    def input(self,x,w,realoutput=False):
        E1,E2=self.coupler.sinput(x,w)
        #Power changed through the Microring resonator
        E11,E12=self.MRR_Transf(self.r,E1)
        E21,E22=self.MRR_Transf(self.r,E2)
        Eout =self.coupler.s1input(E11+E12,E21+E22,theta=0.25*math.pi)  #this theta is different!!
        #P_out( absolute value or the gain of the power )
        return Eout.real if realoutput else Eout
    def input2(self,x,w,realoutput=False):
        E1,E2=self.coupler.sinput(x,w)
        #Power changed through the Microring resonator
        E11,E12=self.MRR_Transf(self.r,E1)
        E21,E22=self.MRR_Transf(self.r,E2)
        Pout1=self.coupler.th_EtoP_s1input(E11,E21,theta=0.25*math.pi)
        Pout2=self.coupler.th_EtoP_s1input(E11,E22,theta=0.25*math.pi)
        Pout3=self.coupler.th_EtoP_s1input(E12,E21,theta=0.25*math.pi)
        Pout4=self.coupler.th_EtoP_s1input(E12,E22,theta=0.25*math.pi)          
        return Pout1,Pout2,Pout3,Pout4
    def input3(self,x,w,realoutput=False):
        P1,P2,P3,P4=self.input2(x,w,realoutput)
        outputs=[]
        for i in [P1,P2,P3,P4]:
            if i is not None:
                outputs.append(i)
        if len(outputs)==1:
            if outputs[0]>0.005:
                self.bi_mode=1
            else:
                self.bi_mode=0 #should be 0 ######################################################################## 
        if len(outputs)==0:
            outputs.append(0)
        output=min(outputs) if self.bi_mode==0 else max(outputs) 
        return output #,self.bi_mode
    def powerinput(self,x,w,realoutput=False):
        x=self.powertofeld(x)
        w=self.powertofeld(w)
        return self.feldtopower(self.input(x,w,realoutput))
    def powerinput2(self,x,w,realoutput=False):
        x=self.powertofeld(x)
        w=self.powertofeld(w)
        P1,P2,P3,P4=self.input2(x,w,realoutput)
        outputs=[]
        for i in [P1,P2,P3,P4]:
            if i is not None:
                outputs.append(i)
        if len(outputs)==1:
            if outputs[0]>0.005:
                self.bi_mode=1
            else:
                self.bi_mode=1 #should be 0 ######################################################################## 
        if len(outputs)==0:
            outputs.append(0)
        output=min(outputs) if self.bi_mode==0 else max(outputs) 
        return output #,self.bi_mode
    def powerinput3(self,x,w,realoutput=False):
        x=self.powertofeld(x)
        w=self.powertofeld(w)
        return abs(self.feldtopower(self.input(x,w,realoutput)))

    #def setvalue(self.radius,):






class opo:

    def __init__(self,phi=0):
        self.phi=phi       
    def input(self,P1):
        return P1*cmath.exp(1j*self.phi)
    def setvalue(self,phi=0):
        self.phi=phi     





#programmable amplifier
class pgm_amplifier():

    def __init__(self):
        self.opo=opo()
        self.amplifier=amplifier()
        self.coupler=coupler()
    #defination according to the figure in the paper
    def input(self,signal,bias_input,realoutput=False):
        E1,E2=self.coupler.input(signal,0,theta=-0.25*math.pi)
        E21,E22=self.coupler.input(E2,0,theta=-0.25*math.pi)
        #P1=self.opo.input(P1,0)
        E11,E12=self.coupler.input(80000,bias_input,theta=-0.25*math.pi) #E1 80000***************************************************************
        #print(E1)
        E3=self.amplifier.input(E21,E11)
        E4=self.amplifier.input(E22,E12)
        Eout=self.coupler.s1input(E3,E4)
        return Eout.real if realoutput else Eout
    #input with power
    def powerinput(self,signal,bias_input,realoutput=False):
        x=self.amplifier.powertofeld(signal)
        w=self.amplifier.powertofeld(bias_input)
        return self.amplifier.feldtopower(self.input(x,w,realoutput))
    def input2(self,signal,bias_input,realoutput=False):
        E1,E2=self.coupler.input(signal,0,theta=-0.25*math.pi)
        E21,E22=self.coupler.input(E2,0,theta=-0.25*math.pi)
        #P1=self.opo.input(P1,0)
        E11,E12=self.coupler.input(E1,bias_input,theta=-0.25*math.pi)
        E3=self.amplifier.input3(E21,E11)
        E4=self.amplifier.input3(E22,E12)
        Eout=E3-E4 #self.coupler.s1input(E3,E4)
        return Eout.real if realoutput else Eout
    #input with power
    def powerinput2(self,signal,bias_input,realoutput=False):
        x=self.amplifier.powertofeld(signal)
        w=self.amplifier.powertofeld(bias_input)
        return self.input2(x,w,realoutput)
    def powerinput3(self,signal,bias_input,realoutput=False):
        x=self.amplifier.powertofeld(signal)
        w=self.amplifier.powertofeld(bias_input)
        return abs(self.amplifier.feldtopower(self.input(x,w,realoutput)))



class thresholder(microring):
    #init defination
    def __init__(self,r1=0.966,r2=0.979,radius=4e-4,alpha=2.4,phi_0=-0.0481):
        #add a coupler
        self.coupler=coupler()
        #self-coupling coefficient
        self.r1=r1
        self.r2=r2
        #init of class microring
        microring.__init__(self,radius,alpha,phi_0)
        #phi_b ========> equation (7) page 1265
        self.phi_b=-( cmath.atan( self.r2*math.sin(self.phi_0) / (self.a-self.r2*math.cos(self.phi_0)) )
               +math.atan( self.a*self.r2*math.sin(self.phi_0) / (1-self.a*self.r2*math.cos(self.phi_0)) )
               -math.atan( self.r1*math.sin(self.phi_0) / (self.a-self.r1*math.cos(self.phi_0)) )
               -math.atan( self.a*self.r1*math.sin(self.phi_0) / (1-self.a*self.r1*math.cos(self.phi_0)) ) 
                )
        self.bi_mode=0 #bistability mode
        #self.last_input=0
    #set values
    def setvalue(self,r1,r2,radius,alpha,phi_0):
        self.__init__(r1,r2,radius,alpha,phi_0)
    #function to get the P_out ========> equation (6) page 1265
    def input(self,E_in,realoutput=False):
        E1,E2=self.coupler.input(E_in,0,theta=0.2554123111786*math.pi)
        #Power changed through the Microring resonator
        E11,E12=self.MRR_Transf(self.r1,E1)
        E21,E22=self.MRR_Transf(self.r2,E2) 
        Eout =self.coupler.s1input(E11+E12,(E21+E22)*cmath.exp(1j*self.phi_b),theta=1.2445876888214*math.pi)
        #P_out( with absolute value or not )
        return Eout.real if realoutput else Eout
    #input with power
    def powerinput(self,P_in,realoutput=False):
        E_in=self.powertofeld(P_in)
        return self.feldtopower(self.input(E_in,realoutput))
    def input2(self,E_in,realoutput=False):
        E1,E2=self.coupler.input(E_in,0,theta=0.2554123111786*math.pi)
        #Power changed through the Microring resonator
        E11,E12=self.MRR_Transf(self.r1,E1)
        E21,E22=self.MRR_Transf(self.r2,E2)
        Pout1=self.coupler.th_EtoP_s1input(E11,(E21)*cmath.exp(1j*self.phi_b),theta=1.2445876888214*math.pi)
        Pout2=self.coupler.th_EtoP_s1input(E11,(E22)*cmath.exp(1j*self.phi_b),theta=1.2445876888214*math.pi)
        Pout3=self.coupler.th_EtoP_s1input(E12,(E21)*cmath.exp(1j*self.phi_b),theta=1.2445876888214*math.pi)
        Pout4=self.coupler.th_EtoP_s1input(E12,(E22)*cmath.exp(1j*self.phi_b),theta=1.2445876888214*math.pi)          
        return Pout1,Pout2,Pout3,Pout4
    def powerinput2(self,P_in,realoutput=False):
        E_in=self.powertofeld(P_in)
        P1,P2,P3,P4=self.input2(E_in,realoutput)
        outputs=[]
        for i in [P1,P2,P3,P4]:
            if i is not None:
                outputs.append(i)
        if len(outputs)==1:
            if outputs[0]>0.005:
                self.bi_mode=1
            else:
                self.bi_mode=0 #########################################################################  
        if len(outputs)==0:
            outputs.append(0)
        output=min(outputs) if self.bi_mode==0 else max(outputs) 
        return output,self.bi_mode#self.bi_mode               #!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        #return E1+E2+E3+E4
    def powerinput3(self,P_in,realoutput=False):
        E_in=self.powertofeld(P_in)
        P1,P2,P3,P4=self.input2(E_in,realoutput)
        outputs=[]
        for i in [P1,P2,P3,P4]:
            if i is not None:
                outputs.append(i)
        if len(outputs)==0:
            outputs.append(0)
        return min(outputs),max(outputs)    
    





class perceptron():

    def __init__(self,realoutput=True):
        self.mrr=microring()
        self.pamp=pgm_amplifier()
        self.pamp.amplifier.setvalue(r=0.926,radius=6e-4,alpha=3,phi_0=-0.0481)
        self.pamp.coupler.setvalue(theta=-0.25*math.pi)
        self.thresholder=thresholder(r1=0.970,r2=0.989,radius=4e-4,alpha=2.4,phi_0=-0.0481) #r1=0.970,r2=0.989,radius=4e-4,alpha=2.4,phi_0=-0.0481
        self.coupler=coupler()
        self.RTE=realoutput
    
    def powerinput(self,x,w,expected=None,eta=None):
        result=0
        for k in range(len(x)):
            result+=( self.pamp.powerinput(x[k], w[k],realoutput=self.RTE)) #k from 0 to 2
        output=self.thresholder.powerinput(result,realoutput=self.RTE)
        if eta==None:
            return output
        else:
            error=self.coupler.s1input(expected,output,theta=0.25*math.pi)        
            w -= eta * error * x #w += eta * error * x
            return w,error
    def powerinput2(self,x,w,highlevel=0.1,lowlevel=0):
        result=0
        for k in range(len(x)):
            result+=( self.pamp.powerinput(x[k], w[k],realoutput=self.RTE)) #k from 0 to 2
        output=self.thresholder.powerinput(result,realoutput=self.RTE)       
        return highlevel if output>0.002 else lowlevel
    def powerinput3(self,x,w,expected=None,eta=None): #,resultold=None
        result=0
        results=[0,0,0]
        for k in range(len(x)):
            temp=abs( self.pamp.powerinput(x[k], w[k],realoutput=False)) #k from 0 to 2
            result+=temp
            results[k]=temp
        Pmin,Pmax=self.thresholder.powerinput3(result,realoutput=self.RTE)
        if eta==None:
            return Pmin,Pmax,result
        else:
            #if resultold>0.0
            if expected==0:
                output=Pmax
            else:
                output=Pmax
            error=self.coupler.s1input(expected,output,theta=0.25*math.pi) 
            for ii in range(3):
                w[ii] += eta * error * results[ii] 
            for i in range(len(w)):
                if w[i]<0:
                    w[i]=0
            return w,error,expected
    def powerinput4(self,x,w,expected=None,eta=None): #,resultold=None
        result=0
        results=[0,0,0]
        for k in range(len(x)):
            temp=abs( self.pamp.powerinput(x[k], w[k],realoutput=False)) #k from 0 to 2
            result+=temp
            results[k]=temp
        Pout,bi_mode=self.thresholder.powerinput2(result,realoutput=self.RTE)
        if eta==None:
            return Pout,result
        else:
            #if resultold>0.0
            error=expected-Pout #self.coupler.s1input(expected,output,theta=0.25*math.pi) 
            for ii in range(3):
                w[ii] += eta * error * results[ii]#*w[ii]*100#x
            for k in range(len(w)):
                if w[k]<0:
                    w[k]=0
            return w,error,bi_mode







#figure output
class fig():
    #def plot amount number, input data unit, output amplifier factor
    def __init__(self,name='Figure',num=25,unit=1e-3,amp=1e3,ampz=1e3):
        self.name=name
        self.num=num
        self.unit=unit
        self.amp=amp
        self.ampz=ampz
        self.label3d_x='X'
        self.label3d_y='W'
        self.label3d_z='OUTPUT'
        self.label2d_x='X'
        self.label2d_y='OUTPUT'
    def setbasevalue(self,num=25,unit=1e-3,amp=1e3,ampz=1e3):
        self.num=num
        self.unit=unit
        self.amp=amp
        self.ampz=ampz
    def set3dlabel(self,x,y,z):
        self.label3d_x=x
        self.label3d_y=y
        self.label3d_z=z
    def set2dlabel(self,x,y):
        self.label2d_x=x
        self.label2d_y=y
    def settitle(self,name='Figure'):
        self.name=name
    #figure output3d
    def output3d(self,func,xmin=-20,xmax=20,wmin=-50,wmax=100,gainoutput=False):
        xs=np.linspace(xmin*self.unit,xmax*self.unit,num=self.num)
        ws=np.linspace(wmin*self.unit,wmax*self.unit,num=self.num)
        POUT=[]
        PINX=[]
        PINW=[]
        for w in ws :
            for x in xs:
                POUT.append( func(x,w).real*self.amp/x if gainoutput else func(x,w).real*self.ampz ) 
                PINX.append(x*self.amp)
                PINW.append(w*self.amp)
        ax = plt.subplot(111, projection='3d') 
        ax.plot_trisurf(PINX,PINW,POUT,cmap='viridis')  #'plasma' / 'viridis'
        ax.set_xlabel(self.label3d_x)
        ax.set_ylabel(self.label3d_y)
        ax.set_zlabel(self.label3d_z)
        ax.set_title(self.name)
        plt.show()
    #figure output2d
    def output2d(self,func,xmin=-20,xmax=20,gainoutput=False):
        xs=np.linspace(xmin*self.unit,xmax*self.unit,num=self.num)
        POUT=[]
        PINX=[]
        for x in xs:
            POUT.append( func(x).real*self.amp/x if gainoutput else func(x).real*self.ampz ) 
            PINX.append(x*self.amp)
        plt.figure()
        plt.scatter(PINX,POUT) 
        plt.title(self.name)
        plt.xlabel(self.label2d_x)
        plt.ylabel(self.label2d_y)
        plt.show()