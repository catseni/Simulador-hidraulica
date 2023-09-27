using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Values : MonoBehaviour
{
    public List<float> val = new List<float>();

    public void receiveData(List<float> list)
    {
        val = list;
    }

    public void test()
    {
        Debug.Log("Notificacion desde el main " + val[0] +" , "+ val[1]);
    }
}
