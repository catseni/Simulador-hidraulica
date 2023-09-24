using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    [SerializeField] GameObject[] DataSection;

    private void Start()
    {
        foreach(GameObject game in DataSection) game.SetActive(false);
    }

    public void Test(int ValueSection)
    {
        foreach(GameObject game in DataSection) game.SetActive(false);
        if(ValueSection <= DataSection.Length) DataSection[ValueSection].SetActive(true);
    }
}
