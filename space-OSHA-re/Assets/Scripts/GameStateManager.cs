using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance { get; private set; }

    public ShipEngineController Engine;
    private float DistanceTravelled;
    private float GoalDistance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GoalDistance = Engine.GoalDistance; 
        DistanceTravelled = Engine.DistanceTravelled;
    }

    // Update is called once per frame
    void Update()
    {
        DistanceTravelled = Engine.DistanceTravelled;
        if (DistanceTravelled >= GoalDistance)
        {
            InitiateWinState();
        }
    }

    public void InitiateWinState()
    {
        SceneManager.LoadScene("WinScreen");
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    public void InitiateLoseState()
    {
        SceneManager.LoadScene("LoseScreen");
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
