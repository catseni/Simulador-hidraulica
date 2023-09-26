using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

// Asegúrate de que estas clases y namespaces estén correctamente definidos y disponibles
using MisClases;

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

    #region Para Tirante Normal y Crítico
    public double Q;            // Gasto m³/s
    public double So;           // Pendiente del fondo (adimensional)
    public double n;            // Rugosidad
    public double yn;           // Tirante normal propuesto
    public static double yc;    // Tirante crítico propuesto
    public double epsilon = 1e-6; // Epsilon

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
    public TextMeshPro r_tiranteNormal;

    // Tirante crítico
    public TextMeshPro r_tiranteCritico;
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
    public TextMeshPro r_area;
    public TextMeshPro r_perimetro;
    public TextMeshPro r_radioHidraulico;
    public TextMeshPro r_anchoSuperficie;
    public TextMeshPro r_tiranteMedio;
    #endregion

    #endregion

    #region Pasar datos para guardar las variables
    // Método para pasar datos dependiendo del canal si es en la opción de Elementos Geométricos
    public void PasarDatosGeometricos()
    {
        switch (tipoCanal)
        {
            case TipoCanal.Trapecial:
                canalPoligonal.b = b;
                canalPoligonal.y = yn;
                break;
            case TipoCanal.Rectangular:
                canalPoligonal.b = b;
                canalPoligonal.y = yn;
                canalPoligonal.k1 = k1;
                canalPoligonal.k2 = k2;
                break;
            case TipoCanal.Triangular:
                canalPoligonal.y = yn;
                canalPoligonal.k1 = k1;
                canalPoligonal.k2 = k2;
                break;
            case TipoCanal.Circular:
                canalCircular.D = D;
                canalCircular.y = yn;
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
                resultadoNormal.Q = Q;
                resultadoNormal.b = b;
                resultadoNormal.k1 = k1;
                resultadoNormal.k2 = k2;
                resultadoNormal.So = So;
                resultadoNormal.n = n;
                resultadoNormal.yn = yn;
                resultadoNormal.yc = yc; // Acceso a yc a través de la clase
                break;
            case TipoCanal.Rectangular:
                resultadoNormal.Q = Q;
                resultadoNormal.b = b;
                resultadoNormal.So = So;
                resultadoNormal.n = n;
                resultadoNormal.yn = yn;
                resultadoNormal.k1 = k1;
                resultadoNormal.k2 = k2;
                resultadoNormal.yc = yc; // Acceso a yc a través de la clase
                break;
            case TipoCanal.Triangular:
                resultadoNormal.Q = Q;
                resultadoNormal.k1 = k1;
                resultadoNormal.k2 = k2;
                resultadoNormal.So = So;
                resultadoNormal.n = n;
                resultadoNormal.yn = yn;
                resultadoNormal.yc = yc; // Acceso a yc a través de la clase
                break;
            case TipoCanal.Circular:
                resultadoNormal.Q = Q;
                resultadoNormal.D = D;
                resultadoNormal.So = So;
                resultadoNormal.n = n;
                resultadoNormal.yn = yn;
                resultadoNormal.yc = yc; // Acceso a yc a través de la clase
                break;
            default:
                Debug.Log("Valores incorrectos, vuelve a intentar."); // Mensaje de error si el tipo de canal es desconocido
                break;
        }
    }

    public void PasarDatosCritico()
    {
        switch (tipoCanal)
        {
            case TipoCanal.Trapecial:
                resultadoCritico.Q = Q;
                resultadoCritico.b = b;
                resultadoCritico.k1 = k1;
                resultadoCritico.k2 = k2;
                yc = resultadoCritico.yc; // Acceso a yc a través de la clase
                break;
            case TipoCanal.Rectangular:
                resultadoCritico.Q = Q;
                resultadoCritico.b = b;
                yc = resultadoCritico.yc; // Acceso a yc a través de la clase
                resultadoCritico.k1 = k1;
                resultadoCritico.k2 = k2;
                break;
            case TipoCanal.Triangular:
                resultadoCritico.Q = Q;
                resultadoCritico.k1 = k1;
                resultadoCritico.k2 = k2;
                yc = resultadoCritico.yc; // Acceso a yc a través de la clase
                break;
            case TipoCanal.Circular:
                resultadoCritico.Q = Q;
                resultadoCritico.D = D;
                yc = resultadoCritico.yc; // Acceso a yc a través de la clase
                break;
            default:
                Debug.Log("Valores incorrectos, vuelve a intentar."); // Mensaje de error si el tipo de canal es desconocido
                break;
        }
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
        resultadoNormal = NewtonRaphsonNormal.FindNormalDepth(Q, b, n, So, epsilon);
    }

    public void CalcularCritico()
    {
        resultadoCritico = NewtonRaphsonCritico.FindCriticalDepth(Q, b, (k1 + k2) / 2, n, epsilon);
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
        r_tiranteNormal.text = resultadoNormal.yn.ToString();
    }

    public void RegistroCritico()
    {
        r_tiranteCritico.text = resultadoCritico.yc.ToString();
    }
    #endregion

    #region FuncionCalcular
    // Aquí juntas las tres funciones dependiendo del enum que tiene
    public void CalcularBoton()
    {
        PasarDatosGeometricos();
        CalcularGeometricos();

        switch (pestaña)
        {
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
