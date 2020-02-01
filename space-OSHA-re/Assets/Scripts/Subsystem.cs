using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A base class for any basic ship subsystem that requires power
public abstract class Subsystem : MonoBehaviour
{
    public int powerLevel { get; protected set; }
    public bool isPowered => powerLevel > 0;

    public bool isOnFire { get; protected set; }
    public bool isActivated { get; protected set; }

}
