using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendData : MonoBehaviour
{
    List<float> valores = new List<float>() { };
    public Values values;

    public void saveData(string s)
    {
        float valor = float.Parse(s);
        valores.Insert(0, valor);
    }

    public void flush()
    {
        valores.Clear();
        Debug.Log("Flush de lista" + string.Join(", ", valores));
    }

    public void SendInfo()
    {
        values.receiveData(valores);

    }

}
