using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetData : MonoBehaviour
{

    public SendData sendData;

public void ReadData(string s)
    {
        string input = s;

        Debug.Log("getdata"+input);
        sendData.saveData(input);
    }
}
