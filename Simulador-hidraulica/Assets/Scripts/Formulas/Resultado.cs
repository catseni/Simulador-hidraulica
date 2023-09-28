using System;

public static class Resultado
{
    public static double FindNormalDepth(double Q, double b,double k1, double k2, double n, double So, double epsilon)
    {
        return NewtonRaphsonNormal.FindNormalDepth(Q, b, k1, k2, n, So, epsilon);
    }

    public static double FindCriticalDepth(double Q, double b, double k1,double k2, double n, double epsilon)
    {
        return NewtonRaphsonCritico.FindCriticalDepth(Q, b, k1,k2, n, epsilon);
    }
}