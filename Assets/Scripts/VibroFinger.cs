using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibroFinger : Senso.Finger
{
    public SensoHandExample Hand;
    public ushort duration;
    public byte Hardness;
    public void Vibrate()
    {
        Hand.VibrateFinger(FingerType, duration, Hardness);
        print("Vibrating");
    }
}
