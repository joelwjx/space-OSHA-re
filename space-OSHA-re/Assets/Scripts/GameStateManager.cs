using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public ShipEngineController Engine;
    private float DistanceTravelled;
    private float GoalDistance;

    // Start is called before the first frame update
    void Start()
    {
        GoalDistance = 100;
        DistanceTravelled = Engine.DistanceTravelled;
    }

    // Update is called once per frame
    void Update()
    {
        DistanceTravelled = Engine.DistanceTravelled;
        if (DistanceTravelled >= GoalDistance) Debug.Log("Win state");
    }
}
