import opticaldev as odev
import numpy as np
from matplotlib import pyplot as plt


def funxy(x,y):
    return x*y

#amplifier
amplifier=odev.amplifier()
amplifier.setvalue(r=0.966,radius=4e-4,alpha=3,phi_0=-0.0481)
fig0=odev.fig(name='amplifier',num=25,unit=1e-3,amp=1e3)
fig0.output3d(amplifier.powerinput3,xmin=0,xmax=100,wmin=0,wmax=80,gainoutput=False)

#programmable amplifier
pamp=odev.pgm_amplifier()
pamp.amplifier.setvalue(r=0.926,radius=6e-4,alpha=3,phi_0=-0.0481)
pamp.opo.setvalue(phi=0)
fig1=odev.fig(name='programmable amplifier r=0.926 radius=6e-4cm',num=25,unit=1e-3,amp=1e3)
fig1.output3d(pamp.powerinput3,xmin=0,xmax=120,wmin=0,wmax=80,gainoutput=False)
#fig1.output3d2(pamp.powerinput3,w=0.1,xmin=0,xmax=120,gainoutput=False)

#thresholder
thresholder=odev.thresholder()
thresholder.setvalue(r1=0.970,r2=0.989,radius=4e-4,alpha=2.4,phi_0=-0.0481) 

#fig2=odev.fig(name='thresholder',num=50,unit=1e-3,amp=1e3)
#fig2.output2d(thresholder.powerinput,xmin=-60,xmax=60,gainoutput=False)



#thresholder2
#thresholder=odev.thresholder()
#thresholder.setvalue(r1=0.966,r2=0.979,radius=4e-4,alpha=2.4,phi_0=-0.0481)
P_ins=np.linspace(0e-3,25e-3,num=3000)
POUT=[]
PIN=[]
for P_in in P_ins:
    P1,P2=thresholder.powerinput3(P_in)
    POUT.extend([P1,P2])
    PIN.extend([P_in,P_in])


plt.figure()
plt.scatter(PIN,POUT)
plt.title('thresholder 2')
plt.show()


xs=np.linspace(0e-3,80e-3,1000)
POUT=[]
PINX=[]
for x in xs:
    POUT.append( pamp.powerinput3(0.1,x)*1000 ) 
    PINX.append(x*1000)
plt.figure()
plt.scatter(PINX,POUT) 
plt.title('Programmable amplifier, input x=100mW')
plt.xlabel('Pin')
plt.ylabel('Pout')
plt.show()


fig3=odev.fig(name='function of fx=x*y',num=25,unit=1e-3,amp=1e3)
fig3.output3d(funxy,xmin=0,xmax=120,wmin=0,wmax=80,gainoutput=False)


"""
#thresholder2 old version
thresholderold=odev.thresholderold()
fig3=odev.fig(name='thresholderold',num=50,unit=1e-3,amp=1e3)
fig3.output2d(thresholderold.powerinput,xmin=0,xmax=60,gainoutput=False)
"""