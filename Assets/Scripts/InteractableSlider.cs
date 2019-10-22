using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSlider : MonoBehaviour
{
    public float Temp = 0;
    public Container Container;
    public TemperatureScript TextTemp;
    Vector3 prevpos;
    void Start()
    {

    }

    void Update()
    {
        Temp = transform.localPosition.z * 1000;
        Container.SliderTemp = (int)Temp;
        TextTemp.NewTemp((int)Temp);
        prevpos = transform.position;
        if (transform.position.z > 0.101)
            transform.position = prevpos;
        if (transform.position.z < -0.101)
            transform.position = prevpos;

    }
}
