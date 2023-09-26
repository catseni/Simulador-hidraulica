using System;

public class NewtonRaphsonNormal
{
    public static double FindNormalDepth(double Q, double b, double n, double So, double epsilon)
    {
        double T = 1.0;  // Valor inicial para el tirante normal T

        while (true)
        {
            double term1 = Q / (b * Math.Sqrt(So));
            double term2 = Math.Pow(T, 5.0 / 3.0);
            double term3 = Math.Sqrt(n);

            double f = term1 - term2 + term3;
            double dfdT = (5.0 / 3.0) * term1 - (2.0 / 3.0) * term3;

            double deltaT = f / dfdT;
            T += deltaT;

            if (Math.Abs(deltaT) < epsilon)
            {
                // Convergencia alcanzada dentro de la tolerancia especificada
                return T;
            }
        }
    }
}