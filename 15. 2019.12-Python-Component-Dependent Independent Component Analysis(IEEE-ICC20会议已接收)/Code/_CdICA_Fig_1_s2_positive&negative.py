import numpy as np
import SigGenerator as SigGen
import SigMixer as SigMix
import SigTester as SigTst
import SigIca as SigIca
import Timer
from matplotlib import pyplot as plt
from scipy.spatial import distance 

import pandas as pd
import seaborn as sns
from scipy import stats
from matplotlib.lines import Line2D
import mpl_toolkits.axisartist as axisartist

lim=5
a_1_2=0.3
a_2_1=0.7

def f_1(s_1,s_2):
    return 1*s_1+a_1_2*s_2

def f_2(s_1,s_2):
    return a_2_1*s_1+1*s_2

def _f_2(s_1,s_2):
    return -a_2_1*s_1-1*s_2

if __name__ == '__main__':

    s_1=[-lim,lim]
    s_2=1
    #f_1=1*s_1+a_1_2*s_2
    #f_2=a_2_1*s_1+1*s_2


    points_x=[0,0,0.7/0.3]
    points_y=[a_1_2*s_2,1*s_2,f_1(0.7/0.3,s_2)]
    points_y_scatter=[a_1_2*s_2,1*s_2,0]



    #fig = plt.figure(figsize=(5, 4))
    #ax = fig.add_subplot(1,1,1)
    fig = plt.figure(figsize=(5, 2.5))
    ax = axisartist.Subplot(fig, 121)  
    fig.add_axes(ax)
    ax.axis[:].set_visible(False)
    ax.axis["x"] = ax.new_floating_axis(0,0)
    ax.axis["x"].set_axisline_style("-|>", size = 1.0)
    ax.axis["y"] = ax.new_floating_axis(1,0)
    ax.axis["y"].set_axisline_style("-|>", size = 1.0)
    ax.axis["x"].set_axis_direction("top")
    ax.axis["y"].set_axis_direction("right")
    #plt.scatter(_S_others_,_S_l_,s=0.3,alpha=0.3,c='black')

    plt.scatter(points_x,points_y,s=5,alpha=1,c='black',zorder=30)
    plt.annotate(s=r'$(0,a_{1, 2}{\mathbf{s}_2}^T)$' ,xy=(points_x[0],points_y[0]),xytext=(points_x[0]+0.4,points_y[0]) )
    plt.annotate(s=r'$(0,{\mathbf{s}_2}^T$)' ,xy=(points_x[1],points_y[1]),xytext=(points_x[1]-2.7,points_y[1]-0.1) )
    plt.annotate(s=r'$(\frac{1 - a_{1, 2}}{1 - a_{2, 1}} {\mathbf{s}_2}^T,0)$' ,xy=(points_x[2],0),xytext=(points_x[2]-0.4,-1) )
    
    line1 = [(s_1[0],f_1(s_1[0],s_2)), (s_1[1],f_1(s_1[1],s_2))]
    line2 = [(s_1[0],f_2(s_1[0],s_2)), (s_1[1],f_2(s_1[1],s_2))]
    line3 = [(points_x[2],points_y[2]), (points_x[2],0)]
    (line1_xs, line1_ys) = zip(*line1)
    (line2_xs, line2_ys) = zip(*line2)
    (line3_xs, line3_ys) = zip(*line3)
    ax.add_line(Line2D(line1_xs, line1_ys, linewidth=1, color='blue',ls='--'))
    ax.add_line(Line2D(line2_xs, line2_ys, linewidth=1, color='red'))
    ax.add_line(Line2D(line3_xs, line3_ys, linewidth=1, color='black', ls=':'))
    plt.annotate(s=r'$f_1({\mathbf{s}_1}^T)$' ,xy=(s_1[1],f_1(s_1[1],s_2)),xytext=(s_1[1]+0.1,f_1(s_1[1],s_2+0.1)) )
    plt.annotate(s=r'$f_2({\mathbf{s}_1}^T)$' ,xy=(s_1[1],f_2(s_1[1],s_2)),xytext=(s_1[1]+0.1,f_2(s_1[1],s_2+0.1)) )
    plt.annotate(s=r'$0$' ,xy=(0,0),xytext=(-0.55,-0.55) )
    ax.spines['bottom'].set_linewidth(1)
    ax.spines['left'].set_linewidth(1)
    ax.spines['top'].set_color('none')
    ax.spines['right'].set_color('none')
    ax.spines['bottom'].set_position(('data', 0))
    ax.spines['left'].set_position(('data', 0))
    plt.annotate(s=r'${\mathbf{s}_1}^T$' ,xy=(lim+0.1,0.1),xytext=(lim+0.8,0.2) )
    #plt.annotate(s=r'$f_1({\mathbf{s}_1}^T),f_2({\mathbf{s}_1}^T)$' ,xy=(0,lim),xytext=(0.1,lim+1.2) )

    plt.axis([-lim+1.5, lim+2,-lim+1.5, lim+1])
    plt.xticks([])
    plt.yticks([])
    plt.grid(linestyle=':')
    #plt.show()



    s_1=[-lim,lim]
    s_2=-1
    #f_1=1*s_1+a_1_2*s_2
    #f_2=a_2_1*s_1+1*s_2
    points_x=[0,0,1.3/1.7]
    points_y=[f_1(0,s_2),_f_2(0,s_2),f_1(1.3/1.7,s_2)]
    points_y_scatter=[a_1_2*s_2,1*s_2,0]

    #fig = plt.figure(figsize=(5, 4))
    #ax = fig.add_subplot(1,1,1)

    #fig = plt.figure(figsize=(5, 4))
    ax = axisartist.Subplot(fig, 122)  
    fig.add_axes(ax)
    ax.axis[:].set_visible(False)
    ax.axis["x"] = ax.new_floating_axis(0,0)
    ax.axis["x"].set_axisline_style("-|>", size = 1.0)
    ax.axis["y"] = ax.new_floating_axis(1,0)
    ax.axis["y"].set_axisline_style("-|>", size = 1.0)
    ax.axis["x"].set_axis_direction("top")
    ax.axis["y"].set_axis_direction("right")


    plt.scatter(points_x,points_y,s=5,alpha=1,c='black',zorder=30)
    plt.annotate(s=r'$(0,-a_{1, 2}{\mathbf{s}_2}^T)$' ,xy=(points_x[0],points_y[0]),xytext=(points_x[0]-4.7,-1) )#points_y[0]
    plt.annotate(s=r'$(0,{\mathbf{s}_2}^T$)' ,xy=(points_x[1],points_y[1]),xytext=(points_x[1]-2.9,points_y[1]-0.3) )
    plt.annotate(s=r'$(\frac{1 + a_{1, 2}}{1 + a_{2, 1}} {\mathbf{s}_2}^T,0)$' ,xy=(points_x[2],0),xytext=(points_x[2],-1) )

    line1 = [(s_1[0],f_1(s_1[0],s_2)), (s_1[1],f_1(s_1[1],s_2))]
    line2 = [(s_1[0],_f_2(s_1[0],s_2)), (0.86*s_1[1],_f_2(0.86*s_1[1],s_2))]
    line3 = [(points_x[2],points_y[2]), (points_x[2],0)]
    (line1_xs, line1_ys) = zip(*line1)
    (line2_xs, line2_ys) = zip(*line2)
    (line3_xs, line3_ys) = zip(*line3)
    ax.add_line(Line2D(line1_xs, line1_ys, linewidth=1, color='blue',ls='--'))
    ax.add_line(Line2D(line2_xs, line2_ys, linewidth=1, color='red'))
    ax.add_line(Line2D(line3_xs, line3_ys, linewidth=1, color='black', ls=':'))

    plt.annotate(s=r'$f_1({\mathbf{s}_1}^T)$' ,xy=(s_1[1],f_1(s_1[1],s_2)),xytext=(s_1[1]+0.1,f_1(s_1[1],s_2+0.4)) )
    plt.annotate(s=r'$-f_2({\mathbf{s}_1}^T)$' ,xy=(s_1[1],_f_2(s_1[1],s_2)),xytext=(0.86*s_1[1]+0.1,_f_2(0.86*s_1[1],s_2+0.1)) )
    plt.annotate(s=r'$0$' ,xy=(0,0),xytext=(0.1,0.1) )

    ax.spines['bottom'].set_linewidth(1)
    ax.spines['left'].set_linewidth(1)
    ax.spines['top'].set_color('none')
    ax.spines['right'].set_color('none')
    ax.spines['bottom'].set_position(('data', 0))
    ax.spines['left'].set_position(('data', 0))
    plt.annotate(s=r'${\mathbf{s}_1}^T$' ,xy=(lim+0.1,0.1),xytext=(lim+0.8,0.2) )
    #plt.annotate(s=r'$f_1({\mathbf{s}_1}^T),-f_2({\mathbf{s}_1}^T)$' ,xy=(0,lim),xytext=(0.1,lim+1.2) )



    plt.axis([-lim+1.5, lim+2,-lim+1.5, lim+1])
    plt.xticks([])
    plt.yticks([])
    plt.grid(linestyle=':')
    plt.subplots_adjust(top=0.91, bottom=0.01,right=0.98, left=0.01)
    plt.show()