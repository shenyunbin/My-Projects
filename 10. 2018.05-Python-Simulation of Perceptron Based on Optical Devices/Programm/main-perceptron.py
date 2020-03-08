"""
version 1.5 using a new module perceptron !!!! important!! 26.07.18
"""

from random import choice 
from numpy import array, random
from matplotlib import pyplot as plt
import opticaldev as odev
import sys

perceptron=odev.perceptron()

n=10
errors=[]
#highvalue   Signals are 0 or h(1 in logic language)
h=100e-3
e=15e-3 #expected value
training_data = [ 
        ( array([0,0,h]), 0 ),  #,dtype=complex
        ( array([0,h,h]), e ), 
        ( array([h,0,h]), e ), 
        ( array([h,h,h]), e ), ]
#weights w
w=array( random.uniform(0e-3,50e-3,len(training_data[0][0])) ) 
#w=[50e-3,50e-3,50e-3]
ws0=[]
ws1=[]
ws2=[]
bi_mode=0
bi_modes=[]
for i in range(n):
    for ii in range(500):
        for iii in training_data:
            x, expected = iii#choice(training_data)
            #if i>2:
            w,error,bi_mode=perceptron.powerinput4(x,w,expected,eta=0.2)
            errors.append(error) #error 添加到 errors末尾
            ws0.append(w[0])
            ws1.append(w[1])
            ws2.append(w[2])
            bi_modes.append(bi_mode)
    sys.stdout.write("\b\b\b\b\b\b\b\b\b"+str(i)+'/'+str(n))
    sys.stdout.flush()

print(' Finish!')

for x, _ in training_data: 
    print("{}: -> {}".format( x[:3], perceptron.powerinput4(x,w) ) )

print(w)
plt.figure()
plt.subplot(211)
plt.title('errors')
plt.plot(errors)
plt.subplot(212)
plt.title('0/1 - lower/upper curve of thresholder')
plt.plot(bi_modes)
plt.show()

plt.figure()
plt.subplot(311)
plt.title('w1')
plt.plot(ws0)
plt.subplot(312)
plt.title('w2')
plt.plot(ws1)
plt.subplot(313)
plt.title('w3')
plt.plot(ws2)
plt.show()
