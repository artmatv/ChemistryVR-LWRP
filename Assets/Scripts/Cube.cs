using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    Vector3 TargetPosition;
    Vector3 ObjectPosition;
    Vector3 Distance;

    public SensoHandExample Hand;
    public TemperatureScript TempScript;
    public Container Container;

    public float maxdistance = 0.01f;
    bool isMove;
    GameObject Obj;

    public List<GameObject> MoleculesInside;
    public List<Vector3> MolPose;
    public List<GameObject> Molecules;
    public string MixerMol;
    public GameObject InstantiatedMol;

    [Header("Molecules")]
    Type MolType;
    public int O, H, H2;

    void Start()
    {
        TargetPosition = transform.position;
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            MolPose.Add(child.position);
        }
    }

    void Update()
    {
        if(isMove == true)
            Move();

        if (InstantiatedMol != null)
            InstantiatedMol.transform.Rotate(0, 1, 0);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Molecule")
        {
            col.attachedRigidbody.isKinematic = true;
            isMove = true;
            Obj = col.gameObject;
            MoleculesInside.Add(Obj);
            switch (col.GetComponent<MoleculeInteractable>().Type)
            {
                case Type.O: O++; break;
                case Type.H: H++; break;
                case Type.H2: H += 2; break;
                case Type.None: break;
                default: break;
            }
            Vibrate(500, 5);
        }

        else if(col.gameObject.tag == "VibroFinger")
            col.GetComponent<VibroFinger>().Vibrate();
    }

    void Move()
    {
        if (Obj != null)
        {
            ObjectPosition = Obj.transform.position;
            TargetPosition = MolPose[MoleculesInside.Count - 1];
            Distance = TargetPosition - ObjectPosition;
            if (Vector3.Distance(ObjectPosition, TargetPosition) > maxdistance)
            {
                Obj.transform.Translate(
                    (Distance.x * Time.deltaTime),
                    (Distance.y * Time.deltaTime),
                    (Distance.z * Time.deltaTime),
                    Space.World);
            }
        }

        else
        {
            isMove = false;
            Obj = null;
        }
    }

    public void Connect()
    {
        //O = 0; H = 0;
        /*foreach(GameObject point in MoleculesInside)
        {
            switch(point.GetComponent<MoleculeInteractable>().Type)
            {
                case Type.O: O++; break;
                case Type.H: H++; break;
                case Type.None: break;
                default: break;
            }
        }*/
        if (O == 2 && H == 0)
        {
            Destroy(MoleculesInside[MoleculesInside.Count-1]);
            MoleculesInside.RemoveAt(MoleculesInside.Count - 1);
            MoleculesInside[MoleculesInside.Count - 1].GetComponent<MoleculeInteractable>().Type = Type.O2;
        }

        if (O == 0 && H == 2)
        {
            Destroy(MoleculesInside[MoleculesInside.Count - 1]);
            MoleculesInside.RemoveAt(MoleculesInside.Count - 1);
            MoleculesInside[MoleculesInside.Count - 1].GetComponent<MoleculeInteractable>().Type = Type.H2; 
        }


        if (H==2 && O==1)
        {
            foreach (GameObject point in MoleculesInside)
            {
                Destroy(point);
            }
            MixerMol = "H2O";
            TempScript.Molecule = MixerMol;
            TempScript.NewTemp();
            H = 0;O = 0;
            MoleculesInside.Clear();
            InstantiatedMol = Instantiate(Molecules[0], transform);
            print("Instantiating");
        }
    }

    public void Vibrate(ushort duration, byte Hardness)
    {
        Hand.VibrateFinger(Senso.EFingerType.Thumb, duration, Hardness);
        Hand.VibrateFinger(Senso.EFingerType.Index, duration, Hardness);
        Hand.VibrateFinger(Senso.EFingerType.Third, duration, Hardness);
        Hand.VibrateFinger(Senso.EFingerType.Middle, duration, Hardness);
        Hand.VibrateFinger(Senso.EFingerType.Little, duration, Hardness);
    }

    public void Reset()
    {
        foreach (GameObject point in MoleculesInside)
        {
            Destroy(point);
        }
        if (InstantiatedMol != null)
        {
            Destroy(InstantiatedMol);
        }
        MoleculesInside.Clear();
        H = 0; O = 0;
        MixerMol = null;
        TempScript.Molecule = MixerMol;
        TempScript.NewTemp();
        Container.Reset();
    }
}
