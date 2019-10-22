using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeStorage : MonoBehaviour
{
    public bool Molecule;
    public GameObject MoleculeObj;
    MoleculeSpawner Spawner;

    // Start is called before the first frame update
    void Start()
    {
        Spawner = GetComponentInParent<MoleculeSpawner>();
    }

    void OnTriggerExit(Collider col)
    {
        if (Spawner.Instantiated == col.gameObject)
        {
            Molecule = false;
            MoleculeObj = null;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(Spawner.Instantiated == col.gameObject)
        {
            Molecule = true;
            Spawner.isMove = false;
            MoleculeObj = col.gameObject;
        }
    }
}
