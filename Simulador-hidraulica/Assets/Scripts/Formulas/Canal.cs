using System;

public abstract class Canal
{
    public double y; //tirante del canal

    public abstract double Calcular_A();//Calcula el área mojada del canal
    public abstract double Calcular_P();//Calcula el perímetro mojado del canal
    public abstract double Calcular_T();//Calcula el ancho de la superficie del canal

    public double Calcular_Rh()//Calcula el radio hidráulico del canal
    {
        double A = Calcular_A();
        double P = Calcular_P();
        return A / P;
    }

    public double Calcular_Y()//Calcula el tirante medio del canal
    {
        double A = Calcular_A();
        double T = Calcular_T();
        return A / T;
    }
}
