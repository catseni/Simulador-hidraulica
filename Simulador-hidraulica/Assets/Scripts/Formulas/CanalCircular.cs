/*using System;

public class CanalCircular : Canal
{
    public double D; // diámetro del canal

    public override double Calcular_A() // Calcula el área mojada del canal
    {
        if (y <= D / 2)
        {
            double theta = 2 * Math.Acos(1 - (2 * (y / D)));
            double A = (Math.Pow(D, 2) / 4) * (theta - Math.Sin(theta));
            return A;
        }
        else
        {
            // El tirante es mayor que el radio, el canal está completamente lleno
            return (Math.PI * Math.Pow(D, 2)) / 4;
        }
    }

    public override double Calcular_P() // Calcula el perímetro mojado del canal
    {
        if (y <= D / 2)
        {
            double theta = 2 * Math.Acos(1 - (2 * (y / D)));
            double P = D * theta;
            return P;
        }
        else
        {
            // El tirante es mayor que el radio, el canal está completamente lleno
            return Math.PI * D;
        }
    }

    public override double Calcular_T() // Calcula el ancho de la superficie del canal
    {
        if (y <= D / 2)
        {
            return D;
        }
        else
        {
            // El tirante es mayor que el radio, el canal está completamente lleno
            return 2 * y;
        }
    }
}*/
using UnityEngine;

public class CanalCircular : Canal
{
    public double D; // diámetro del canal

    public override double Calcular_A() // Calcula el área mojada del canal
    {
        if (y <= D / 2)
        {
            double theta = 2 * Mathf.Acos(1 - (2 * (float)(y / D)));
            double A = (Mathf.Pow((float)D, 2) / 4) * (theta - Mathf.Sin((float)theta));
            return A;
        }
        else
        {
            // El tirante es mayor que el radio, el canal está completamente lleno
            return (Mathf.PI * Mathf.Pow((float)D, 2)) / 4;
        }
    }

    public override double Calcular_P() // Calcula el perímetro mojado del canal
    {
        if (y <= D / 2)
        {
            double theta = 2 * Mathf.Acos(1 - (2 * (float)(y / D)));
            double P = D * theta;
            return P;
        }
        else
        {
            // El tirante es mayor que el radio, el canal está completamente lleno
            return Mathf.PI * (float)D;
        }
    }

    public override double Calcular_T() // Calcula el ancho de la superficie del canal
    {
        if (y <= D / 2)
        {
            return D;
        }
        else
        {
            // El tirante es mayor que el radio, el canal está completamente lleno
            return 2 * y;
        }
    }
}
