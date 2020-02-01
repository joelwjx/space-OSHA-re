using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public PowerGeneratorController PowerGenerator;
    public OxygenGeneratorController OxygenGenerator;
    public WaterGeneratorController WaterGenerator;
    public ShipEngineController ShipEngine;

    public int PowerLevel;

    public Text PowerLevelText;

    // Start is called before the first frame update
    void Start()
    {
        PowerLevel = PowerGenerator.GetComponent<PowerGeneratorController>().PowerLevel;
    }

    void Update()
    {
        if (PowerLevel <= 0)
        {
            OxygenGenerator.SetActiveState(false);
            WaterGenerator.SetActiveState(false);
            ShipEngine.SetActiveState(false);
        }
        else
        {
            OxygenGenerator.SetActiveState(true);
            WaterGenerator.SetActiveState(true);
            ShipEngine.SetActiveState(true);
        }

        PowerLevel = PowerGenerator.PowerLevel;
    }
}
