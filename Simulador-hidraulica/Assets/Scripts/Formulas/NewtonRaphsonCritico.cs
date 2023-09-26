using System;

public static class NewtonRaphsonCritico
{
    public static double FindCriticalDepth(double Q, double b, double k, double n, double epsilon)
    {
        double T = 1.0;  // Valor inicial para el tirante crítico T

        while (true)
        {
            double term1 = Q * Q * n * n;
            double term2 = b * b * 9.81; // Usar directamente el valor de la gravedad (9.81 m/s²)
            double term3 = Math.Pow(b / T + 2 * Math.Sqrt(1 + k * k), -10.0 / 3.0);

            double f = Math.Pow(T, 8.0 / 3.0) - term1 / (term2 * term3);
            double dfdT = (8.0 / 3.0) * Math.Pow(T, 5.0 / 3.0) + (10.0 / 3.0) * term1 / (term2 * Math.Pow(b / T + 2 * Math.Sqrt(1 + k * k), 13.0 / 3.0));

            double deltaT = f / dfdT;
            
            T -= deltaT;

            if (Math.Abs(deltaT) < epsilon)
            {
                // Convergencia alcanzada dentro de la tolerancia especificada
                return T;
            }
        }
    }
}
