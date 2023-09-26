using System;

public class CanalPoligonal : Canal
{
    public double b; // ancho de la plantilla del canal
    public double k1; // primer talud del canal
    public double k2; // segundo talud del canal

    /*
        Puede ocurrir que:
            k1, k2 = 0; pero, b debe ser distinto de 0
            b = 0; pero, k1 y k2 deben ser distintos de 0
        Para los canales rectangular y triangular respectivamente.
    */

    public override double Calcular_A() // Calcula el área mojada del canal
    {
        double A = (b * y) + (((k1 + k2) / 2) * (y * y));
        return A;
    }

    public override double Calcular_P() // Calcula el perímetro mojado del canal
    {
        double P = b + (Math.Sqrt(1 + k1 * k1)) * y + (Math.Sqrt(1 + k2 * k2)) * y;
        return P;
    }

    public override double Calcular_T() // Calcula el ancho de la superficie del canal
    {
        double T = b + (k1 + k2) * y;
        return T;
    }
}