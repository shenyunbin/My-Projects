#include <iostream>
#include <cmath>
#include<fstream>


int main()
{
    std::ofstream fout("/mnt/c/WKS/output4.txt");
    double time=0.0;
    double angle_x1=0.0;//Angle
    double angle_x2=0.0;

    double angle_dx1=0.0;//Angular velocity
    double angle_dx2=0.0;

    double angle_ddx1=0.0;//Angular acceleration
    double angle_ddx2=0.0;

    double angle_dddx1=0.0;//Rate of change of angular acceleration

    for(int i=0;i<5000;i++)
    {
        angle_x1 = M_PI / 4 * (1 - std::cos(M_PI / 5.0 * time));

        if(i!=2500)time+=0.001;
        
        angle_dx1 = (angle_x1-angle_x2)/0.001;
        angle_x2 = angle_x1;

        angle_ddx1 = (angle_dx1-angle_dx2)/0.001;
        angle_dx2 = angle_dx1;

        angle_dddx1 = (angle_ddx1-angle_ddx2)/0.001;
        angle_ddx2 = angle_ddx1;
        
        fout<<angle_x1<<"  "<<angle_dx1<<" "<<angle_ddx1<<"    "<<angle_dddx1<<std::endl;
    }
    fout << std::flush; 
    fout.close();
}