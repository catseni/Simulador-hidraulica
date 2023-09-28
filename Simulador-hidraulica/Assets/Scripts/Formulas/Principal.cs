using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public enum TipoCanal
{
    Trapecial,
    Triangular,
    Rectangular,
    Circular
}

public enum Pestaña
{
    Geometricos,
    Normal,
    Critico
}

public class CanalCalculations : MonoBehaviour
{
    #region Variables
    public TipoCanal tipoCanal;
    public Pestaña pestaña;

    public Values values;

    #region Para Tirante Normal y Crítico
    public double Q;            // Gasto m³/s
    public double So;           // Pendiente del fondo (adimensional)
    public double n;            // Rugosidad
    public double yn;           // Tirante normal propuesto
    public static double yc;    // Tirante crítico propuesto
    public double epsilon = 0.000001; // Epsilon

    // Para el trapecial
    public double b;            // Ancho de la plantilla
    public double k1;           // Talud 1
    public double k2;           // Talud 2

    // Para el circular
    public double D;            // Diámetro

    // Resultados
    public ResultadosCalculos resultadoNormal = new ResultadosCalculos();
    public ResultadosCalculos resultadoCritico = new ResultadosCalculos();

    // Variables donde se mostrarán los resultados
    // Tirante normal
    public TMP_Text r_tiranteNormal;

    // Tirante crítico
    public TMP_Text r_tiranteCritico;
    #endregion

    #region Elementos geométricos
    public CanalPoligonal canalPoligonal = new CanalPoligonal();
    public CanalCircular canalCircular = new CanalCircular();

    // Variables que guardarán los resultados de los cálculos
    public double A;            // Área mojada
    public double P;            // Perímetro mojado
    public double T;            // Ancho de superficie
    public double Rh;           // Radio hidráulico
    public double Y;            // Tirante medio

    // Variables que mostrarán los resultados
    public TMP_Text r_area;
    public TMP_Text r_perimetro;
    public TMP_Text r_radioHidraulico;
    public TMP_Text r_anchoSuperficie;
    public TMP_Text r_tiranteMedio;
    #endregion

    #endregion

    #region Pasar datos para guardar las variables
    // Método para pasar datos dependiendo del canal si es en la opción de Elementos Geométricos
    public void PasarDatosGeometricos()
    {
        switch (tipoCanal)
        {
            case TipoCanal.Trapecial:
                canalPoligonal.b = values.val[3];
                canalPoligonal.y = values.val[2];
                canalPoligonal.k1 = values.val[1];
                canalPoligonal.k2 = values.val[0];
                break;
            case TipoCanal.Rectangular:
                canalPoligonal.b = values.val[1];
                canalPoligonal.y = values.val[0];
                break;
            case TipoCanal.Triangular:
                canalPoligonal.b = 0;
                canalPoligonal.y = values.val[2];
                canalPoligonal.k1 = values.val[1];
                canalPoligonal.k2 = values.val[0];
                break;
            case TipoCanal.Circular:
                canalCircular.D = values.val[0];
                canalCircular.y = values.val[1];
                break;
            default:
                Debug.Log("Valores incorrectos, vuelve a intentar."); // Mensaje de error si el tipo de canal es desconocido
                break;
        }
    }

    public void PasarDatosNormal()
    {
        switch (tipoCanal)
        {
            case TipoCanal.Trapecial:
                resultadoNormal.Q  = values.val[5];
                resultadoNormal.b  = values.val[3];
                resultadoNormal.k1 = values.val[4];
                resultadoNormal.k2 = values.val[2];
                resultadoNormal.So = values.val[1];
                resultadoNormal.n  = values.val[0];
                break;
            case TipoCanal.Rectangular:
                resultadoNormal.Q  = values.val[3];
                resultadoNormal.b  = values.val[2];
                resultadoNormal.So = values.val[1];
                resultadoNormal.n  = values.val[0];
                resultadoNormal.k1 = 0;
                resultadoNormal.k2 = 0;
                break;
            case TipoCanal.Triangular:
                resultadoNormal.Q  = values.val[4];
                resultadoNormal.k1 = values.val[3];
                resultadoNormal.k2 = values.val[2];
                resultadoNormal.So = values.val[1];
                resultadoNormal.n  = values.val[0];
                break;
            case TipoCanal.Circular:
                resultadoNormal.Q  = values.val[3];
                resultadoNormal.D  = values.val[2];
                resultadoNormal.So = values.val[1];
                resultadoNormal.n  = values.val[0];
                break;
            default:
                Debug.Log("Valores incorrectos, vuelve a intentar."); // Mensaje de error si el tipo de canal es desconocido
                break;
        }
        Debug.Log("Datos Normales pasados");
    }

    public void PasarDatosCritico()
    {
        switch (tipoCanal)
        {
            case TipoCanal.Trapecial:
                resultadoCritico.Q  = values.val[3];
                resultadoCritico.b  = values.val[2];
                resultadoCritico.k1 = values.val[1];
                resultadoCritico.k2 = values.val[0];
                break;
            case TipoCanal.Rectangular:
                resultadoCritico.Q  = values.val[1];
                resultadoCritico.b  = values.val[0];
                resultadoCritico.k1 = k2 - k1;
                resultadoCritico.k2 = k1 - k2 * resultadoCritico.Q;
                break;
            case TipoCanal.Triangular:
                resultadoCritico.Q  = values.val[2];
                resultadoCritico.k1 = values.val[1];
                resultadoCritico.k2 = values.val[0];
                break;
            case TipoCanal.Circular:
                resultadoCritico.Q  = values.val[1];
                resultadoCritico.D  = values.val[0];
                break;
            default:
                Debug.Log("Valores incorrectos, vuelve a intentar."); // Mensaje de error si el tipo de canal es desconocido
                break;
        }
        Debug.Log("Datos Criticos pasados");
    }
    #endregion

    #region Para calcular las variables
    // Métodos para calcular las variables
    public void CalcularGeometricos()
    {
        if (tipoCanal == TipoCanal.Circular)
        {
            A = canalCircular.Calcular_A();
            P = canalCircular.Calcular_P();
            T = canalCircular.Calcular_T();
            Rh = canalCircular.Calcular_Rh();
            Y = canalCircular.Calcular_Y();
        }
        else
        {
            A = canalPoligonal.Calcular_A();
            P = canalPoligonal.Calcular_P();
            T = canalPoligonal.Calcular_T();
            Rh = canalPoligonal.Calcular_Rh();
            Y = canalPoligonal.Calcular_Y();
        }
    }

    public void CalcularNormal()
    {
        if(tipoCanal == TipoCanal.Circular)
        {
            resultadoNormal.yn = NewtonRaphsonNormal.FindNormalDepthCircular(resultadoNormal.Q, resultadoNormal.D, resultadoNormal.n, resultadoNormal.So, epsilon);
        }
        else
            resultadoNormal.yn = NewtonRaphsonNormal.FindNormalDepth(resultadoNormal.Q, resultadoNormal.b, resultadoNormal.k1,resultadoNormal.k2, resultadoNormal.n, resultadoNormal.So, epsilon);
    }

    public void CalcularCritico()
    {
        if(tipoCanal == TipoCanal.Circular)
        {
            resultadoCritico.yc = NewtonRaphsonCritico.FindCriticalDepthCircular(resultadoCritico.Q, resultadoCritico.D, epsilon);
        }
        else
        {
            resultadoCritico.yc = NewtonRaphsonCritico.FindCriticalDepth(resultadoCritico.Q, resultadoCritico.b, resultadoCritico.k1 , resultadoCritico.k2, resultadoCritico.n, epsilon);
        }
    }
    #endregion

    #region Para mostrar los resultados
    // Método para registrar los resultados
    public void RegistroGeometrico()
    {
        r_area.text = A.ToString();
        r_perimetro.text = P.ToString();
        r_anchoSuperficie.text = T.ToString();
        r_radioHidraulico.text = Rh.ToString();
        r_tiranteMedio.text = Y.ToString();
    }

    public void RegistroNormal()
    {
        //r_tiranteNormal.text = 1.ToString();
        r_tiranteNormal.text = resultadoNormal.yn.ToString();
    }

    public void RegistroCritico()
    {
        //r_tiranteCritico.text = 2.ToString();
        r_tiranteCritico.text = resultadoCritico.yc.ToString();
    }
    #endregion

    #region FuncionCalcular
    // Aquí juntas las tres funciones dependiendo del enum que tiene
    public void CalcularBoton()
    {
        switch (pestaña)
        {
            case Pestaña.Geometricos:
                PasarDatosGeometricos();
                CalcularGeometricos();
                RegistroGeometrico();
                break;
            case Pestaña.Normal:
                PasarDatosNormal();
                CalcularNormal();
                RegistroNormal();
                break;
            case Pestaña.Critico:
                PasarDatosCritico();
                CalcularCritico();
                RegistroCritico();
                break;
        }
    }
    #endregion
}
