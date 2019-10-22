using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureScript : MonoBehaviour
{
    public int Temp = 0;
    public CurvedText text;
    public string Molecule;
    // Start is called before the first frame update
    void Start()
    {
        text.text = Molecule + " Temperature: " + Temp.ToString() + " Celsius";
    }

    public void NewTemp(int Temperature = 0)
    {
        Temp = Temperature;
        text.text = Molecule + " Temperature: " + Temp.ToString() + " Celsius";
    }
}
