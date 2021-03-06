项目简介
===========================

| 作者 | 沈云彬 |
| ------------- | ----------- |
| 邮箱 | shenyunbin@outlook.com |

-----------

## 01 四轴飞行器预定轨迹飞行控制

**技术**  
`C` `C51单片机` `0.2K代码`

**成果**  
2012年 西电星火杯校二等奖
    
**简介**  
使用C51单片机来控制继电器来控制飞行器遥控器。本人负责全部程序设计。

-----------

## 02 农业生产综合检测管理系统

**技术**  
`C` `VB.NET` `.NET Framework` `MSP430单片机` `Arduino单片机` `433M无线串口模块` `颜色传感器` `温湿度感器`  `光照传感器`  `电阻传感器` `声音传感器` `水位传感器` `1.5K代码`

**成果**  
2013年 全国大学生测量控制与仪器仪表创新设计大赛 陕西省一等奖 全国一等奖
    
**简介**  
使用了C51、MSP430和Arduino单片机和电脑程序。该系统涵盖了温湿度、声音、颜色、土壤湿度、硬度检测、关照控制、授粉、加湿、浇水、施肥等功能。单片机检测环境参数并且通过无线模块传输数据到电脑程序，电脑程序分析并发送控制信号给单片机，然后单片机执行相应的功能。在此项目中本人承担了所有的项目设计、软硬件程序设计及开发，还有样品制作（亚克力板、AutoCAD绘图并切割），还有大部分电路焊接。

-----------

## 03 数据插值与拟合

**技术**  
`VB.NET` `.NET Framework` `1K代码`

**简介**  
拉格朗日插值、牛顿插值、多次拟合的.NET Framework电脑程序

-----------

## 04 自动浇花系统

**技术**  
`C` `Arduino` `降水检测` `土壤电阻检测` `0.5K代码`

**简介**
基于Arduino，根据降雨、土壤湿度来控制水龙头阀门来达到自动浇花的功能，该系统正常运行了有半年多，后来因家里拆迁被拆除了，所有器材都是自己设计及采购。

-----------

## 05 通过WIFI传输数据的环境监测系统

**技术**  
`C` `MSP430` `WIFI通信` `低功耗设计` `温湿度检测` `土壤电阻检测` `0.5K代码`

**成果**  
充分利用MSP430的低功耗模式和定时唤醒WIFI模块的设计，大大降低能耗

**简介**  
系统基于MSP430，本人负责所有的程序编写，另一个学长负责PCB版图制作。项目难点在于低功耗控制，目的是让干电池供电成为可能。当时详细查阅了单片机说明书，将单片机休眠时频率调到最低并用计数器来控制休眠的时长，以达到超低功耗效果。

-----------

## 06 智能工具传递与放置系统

**技术**  
`C` `Arduino` `液晶屏显示` `模拟舵机控制` `红外遥控` `电阻应变片` `1.2K代码`

**简介**  
系统基于Arduino、多个模拟舵机和电阻应变片，根据物品重量来识别物品并放置到物品规定的位置。系统附带液晶屏显示功能和红外遥控功能。

-----------

## 07 手指运动参数检测系统设计

**技术**  
`C` `VB.NET` `Arduino` `.NET Framework` `433M无线串口模块` `数据可视化` `1K代码`

**成果**  
本科毕业设计 A 优秀
    
**简介**  
基于Arduino和.NET Framework，通过三轴加速度计来检测加速度，并算出手指运动轨迹，通过433MHz无线串口传输模块将数据传输到电脑，并由电脑负责显示手指运动的轨迹。项目难点在于设计快速滤波算法将高频噪声信号过滤掉，因为单片机性能限制，无法进行复傅里叶变换计算，于是使用了一次滤波来解决了这个问题。

-----------

## 08 防伪开票系统发票批量导入接口程序

**技术**  
`VB.NET` `.NET Framework` `VBA` `Access本地数据库` `1K代码`

**成果**  
该程序已稳定应用至今并还更新了一个大版本
    
**简介**  
独立分析并根据税务局防伪开票系统接口规范3.0版编写了发票批量导入接口程序。因为网上没有先例，所以难点在于理解其用于批量导入的xml文件格式。程序功能是将Excel表格数据转换成xml格式，然后防伪开票系统就可以直接批量导入发票数据，节省了大量时间。

-----------

## 09 订单查询报价和图纸查看系统

**技术**  
`VB.NET` `.NET Framework` `VBA` `SQL Server数据库` `Access本地数据库` `FTP` `7K代码`

**成果**  
该系统已稳定应用至今，并总共还更新了两个大版本
    
**简介**  
支持多台电脑局域网登录使用，并可以将各种格式的Excel表格数据导入到数据库。其他功能有订单零部件编辑功能，订单零部件查询，图纸查看，数据备份及恢复等等。

-----------

## 10 模拟基于集成光学器件的神经元单元

**技术**  
`Python` `1K代码`

**简介**  
基于已有论文和数学公式，用python模拟各种光学器件包括光学耦合器、微环谐振器、阈值器、放大器及运算放大器，并用这些模拟的光学器件组成一个简单的神经元。

-----------

##  11 减少在机器人控制循环中的网络延时

**技术**  
`C++` `UDP` `多线程` `RLZ压缩算法` `2K代码`

**成果**  
将通信耗时在机器人控制循环中从100us以上降低到1-2us（本机host模拟状态下）

**简介**  
根据数据特点利用自己自行设计的RLZ数据压缩算法减少数据发送量，相比主流方法要更快压缩率也更高。利用高性能boost库或直接调用系统UDP接口来提升数据发送效率，使用了多线程技术，并使用微服务来将负责数据的接收与发送和负责控制机器人的模块分开，以减少通信在机器人控制循环中的耗时。

-----------

## 12 霍夫变换

**技术**  
`C++` `霍夫变换算法` `1K代码`

**简介**  
霍夫变换识别照片中黄色小球位置。

-----------

## 13 音源位置跟踪

**技术**  
`C++` `霍夫变换算法` `0.5K代码`

**简介**  
利用两个麦克风接收的声音的时间差来确定音源所在的角度。

-----------

## 14 快速盲源分离

**技术**  
`Python` `FastICA算法` `计算向量化` `数据可视化` `2K代码`

**成果**  
将FastICA的速度在特定音频样本下最多达到原来的5-8倍速度（不过后续扩大了样本后，发现鲁棒性还欠佳，于是提出了新的解决方案）
    
**简介**
具体实现由两种原创方法组合，第一是提出了一种全新的盲源分离方法，可以大致得出分离矩阵，然后作为初始矩阵再代入FastICA以减少FastICA的迭代次数。第二是自适应调整FastICA每次迭代时所使用的的数据量，以减少每次迭代所需的时间。项目难点在于第一种方法中的公式论证需要大量的从未学过的数学和统计学知识，并推理的长达两个月。难点二是怎样自适应选取每次迭代所用的数据量。

-----------

## 15 基于成分分析的主成分分离（CdICA）

**技术**  
`Python` `FastICA算法` `计算向量化` `数据可视化` `1.5K代码`

**成果**  
论文已被IEEE-ICC20会议接收（本人为二作，并主要负责程序编写、理论推导还有论文图片，论文编写由导师负责）

**简介**  
学生工时期项目，此项目其实是之前快速盲源分离项目中的一个部分，即是通过一种全新和特殊的方法来猜测大致的音源混合矩阵，再得出大致的盲源分离矩阵。我的工作包括对算法进行进一步的数学统计学方面验证和推理，还有对猜测混合矩阵的算法进行进一步的向量化改进，使得计算速度提高到了之前的大概20倍以上，并和导师一起撰写论文CdICA。

-----------

##  16 自适应调整每次迭代所需的数据量的主成分分离（AeICA）

**技术**  
`Python` `FastICA算法` `计算向量化` `数据可视化` `1.5K代码`

**成果**  
提升盲源分离速度到之前2倍以上。

**简介**  
学生工时期项目，此项目也是之前快速盲源分离项目中的一部分的延伸。具体略。

