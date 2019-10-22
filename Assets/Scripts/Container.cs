using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EInside
{
    None, Liquid, Gas, Ice
}

public class Container : MonoBehaviour
{
    public Cube Mixer;
    public TemperatureScript Temp;

    public int SliderTemp = 0;
    public EInside Inside;

    public GameObject LiquidObject;
    public GameObject SmokeObject;
    public GameObject IceObject;
    public Slider slider;

    void Update()
    {
        if (EInside.Liquid == Inside)
        {
            LiquidObject.SetActive(true);
            SmokeObject.SetActive(false);
            IceObject.SetActive(false);
        }

        if (EInside.Gas == Inside)
        {
            LiquidObject.SetActive(false);
            SmokeObject.SetActive(true);
            IceObject.SetActive(false);
        }

        if(EInside.Ice == Inside)
        {
            LiquidObject.SetActive(false);
            SmokeObject.SetActive(false);
            IceObject.SetActive(true);
        }

        if(EInside.None == Inside)
        {
            LiquidObject.SetActive(false);
            SmokeObject.SetActive(false);
            IceObject.SetActive(false);
        }

        if (SliderTemp >= 1)
            Liquid();
        if (SliderTemp <= 0)
            Ice();
        if (SliderTemp >= 100)
            Gas();
    }

    public void ChangeTemp()
    {
        SliderTemp = (int)slider.value;
        Temp.NewTemp(SliderTemp);
    }

    public void Liquid()
    {
        if(Mixer.MixerMol == "H2O")
            Inside = EInside.Liquid;
    }

    public void Gas()
    {
        if (Mixer.MixerMol == "H2O")
            Inside = EInside.Gas;
    }

    public void Ice()
    {
        if (Mixer.MixerMol == "H2O")
            Inside = EInside.Ice;
    }

    public void Reset()
    {
        Inside = EInside.None;
    }
}
