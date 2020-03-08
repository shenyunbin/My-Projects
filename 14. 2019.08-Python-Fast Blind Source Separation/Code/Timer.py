import time

class Timer:
    def __init__(self,name="Timer"):
        self.start_time=0
        self.time_interval=0
        self.name=name
        Timer.__name__

    def Start(self):
        self.start_time=time.time()

    def Stop(self):
        self.time_interval=time.time()-self.start_time

    def Value(self):
        return self.time_interval
    
    def Print(self):
        print("The time of ",self.name," is : ",self.time_interval)

