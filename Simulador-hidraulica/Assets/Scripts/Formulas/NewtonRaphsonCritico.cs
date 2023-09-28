using System;

public static class NewtonRaphsonCritico
{
    public static double FindCriticalDepth(double Q, double b, double k1, double k2, double n, double epsilon)
    {
        double T = 1.0;  // Valor inicial para el tirante crítico T
        double k = (k1 + k2)/2;
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
    public static double FindCriticalDepthCircular(double Q, double D, double epsilon)
    {
        // Calcular el radio hidráulico inicial (mitad del diámetro)
        double R = D / 2.0;

        // Valor inicial para el tirante crítico (toma un valor cercano a R)
        double y0 = R;
        int maxIterations = 50; // Número máximo de iteraciones

        // Iteración de Newton-Raphson para el tirante crítico
        int iterations = 0;
        double y1 = y0;

        while (iterations < maxIterations)
        {
            // Área transversal y perímetro mojado
            double A = Math.PI * Math.Pow(y0, 2.0);
            double P = Math.PI * y0 * 2.0;

            // Radio hidráulico en el tirante actual
            double R_c = A / P;

            // Velocidad en el tirante actual
            double V_c = Q / A;

            // Calcular el tirante crítico mediante la ecuación de flujo
            double fY0 = Math.Pow(V_c, 2.0) / (9.81 * R_c) - R_c;

            // Derivada de la ecuación con respecto a y
            double fPrimeY0 = -2.0 * Math.Pow(V_c, 2.0) / (9.81 * Math.Pow(R_c, 2.0)) - 1.0;

            y1 = y0 - (fY0 / fPrimeY0);

            if (Math.Abs(y1 - y0) < epsilon)
                break;

            y0 = y1;
            iterations++;
        }

        if (iterations < maxIterations)
        {
            return y1;
        }
        else
        {
            return double.NaN;
        }
    }
}
