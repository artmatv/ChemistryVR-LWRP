using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnFall : MonoBehaviour
{
    public GameObject collider;
    void OnTriggerEnter(Collider col)
    {
        collider = col.gameObject;
        Destroy(col.gameObject);
    }
}
