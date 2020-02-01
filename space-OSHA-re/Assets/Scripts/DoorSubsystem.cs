using UnityEngine;

[RequireComponent(typeof(Flammable))]
public class DoorSubsystem : Subsystem
{
    [SerializeField] private PowerGeneratorController generator;
    [SerializeField, Tooltip("The power required to set doors of index level to active")]
    private int[] doorLevels;
    public int doorLevel { get; private set; }

    private Flammable flammable;

    // Start is called before the first frame update
    private void Start()
    {
        isActivated = true;
        powerLevel = generator.PowerLevel;
        flammable = GetComponent<Flammable>();
    }

    // Update is called once per frame
    private void Update()
    {
        powerLevel = generator.PowerLevel;
        for(int i = 0; i < doorLevels.Length; i++)
        {
            if (powerLevel >= doorLevels[i])
            {
                doorLevel = i;
            }
            else break;
        }

        isActivated = doorLevel > 0;

        if(flammable.IsBurning)
        {
            doorLevel = 0;
        }
    }
}
