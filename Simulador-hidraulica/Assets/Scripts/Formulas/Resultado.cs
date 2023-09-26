using System;

public static class Resultado
{
    public static double FindNormalDepth(double Q, double b, double n, double So, double epsilon)
    {
        return NewtonRaphsonNormal.FindNormalDepth(Q, b, n, So, epsilon);
    }

    public static double FindCriticalDepth(double Q, double b, double k, double n, double epsilon)
    {
        return NewtonRaphsonCritico.FindCriticalDepth(Q, b, k, n, epsilon);
    }
}