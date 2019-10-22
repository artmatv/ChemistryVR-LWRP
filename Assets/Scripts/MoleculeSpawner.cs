using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleculeSpawner : MonoBehaviour
{
    MoleculeStorage Storage;
    public GameObject Molecule;
    public GameObject StorageObj;
    public GameObject Instantiated;

    public Type FirstType = Type.None;
    public Type LastType = Type.O2;

    Vector3 TargetPosition;
    Vector3 ObjectPosition;
    Vector3 Distance;

    public Type Type;
    public Text Text;

    public bool isMove;

    // Start is called before the first frame update
    void Start()
    {
        TargetPosition = StorageObj.transform.position;
        Storage = StorageObj.GetComponent<MoleculeStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Storage.Molecule)
        {
            Instantiated = Instantiate(Molecule, transform);
            Instantiated.GetComponent<MoleculeInteractable>().Type = Type;
            Storage.Molecule = true;
            isMove = true;
            Instantiated.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (isMove)
        {
            Move();
        }

    }

    void Move()
    {
        if (Instantiated != null)
        {
            ObjectPosition = Instantiated.transform.position;
            Distance = TargetPosition - ObjectPosition;
            if (Vector3.Distance(ObjectPosition, TargetPosition) > .001f)
            {

                Instantiated.transform.Translate(
                    (Distance.x * Time.deltaTime),
                    (Distance.y * Time.deltaTime),
                    (Distance.z * Time.deltaTime),
                    Space.World);
            }
        }

        else
            Storage.Molecule = false;
    }

    public void SwitchMoleculeRight()
    {
        if (LastType != Type)
        {
            Type++;
            Text.text = Type.ToString();
            Storage.MoleculeObj.GetComponent<MoleculeInteractable>().Type = Type;
        }
    }

    public void SwitchMoleculeLeft()
    {
        if (FirstType != Type)
        {
            Type--;
            Text.text = Type.ToString();
            Storage.MoleculeObj.GetComponent<MoleculeInteractable>().Type = Type;
        }
    }
}
