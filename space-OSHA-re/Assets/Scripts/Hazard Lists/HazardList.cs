using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HazardList : ScriptableObject
{
    [Tooltip("A list of hazard prefabs")]
    public List<Hazard> hazards;
}
