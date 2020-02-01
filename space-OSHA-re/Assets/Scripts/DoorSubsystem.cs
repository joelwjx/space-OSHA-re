using UnityEngine;

public class DoorSubsystem : Subsystem
{
    [SerializeField] private PowerGeneratorController generator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float combustionInterval = 0.5f;
    [SerializeField, Range(0,1f)] private float combustionChance = 0.02f;
    [SerializeField, Tooltip("The power required to set doors of index level to active")]
    private int[] doorLevels;
    public int doorLevel { get; private set; }

    private float combustionTimer = 0;

    // Start is called before the first frame update
    private void Start()
    {
        isActivated = true;
        isOnFire = false;
        powerLevel = generator.PowerLevel;
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

        if(!isOnFire && combustionTimer > combustionInterval)
        {
            if(Random.value <= combustionChance)
            {
                isOnFire = true;
            }
            combustionTimer = 0;
        }

        if(isOnFire)
        {
            doorLevel = 0;
        }
        combustionTimer += Time.deltaTime;

        // Update sprite
        sprite.color = isOnFire ? Color.red : Color.white;
        Color c = sprite.color;
        sprite.color = new Color(c.r, c.g, c.b, 0.5f + doorLevel / (float)doorLevels.Length);
    }

    void OnTriggerStay2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player" && isOnFire && Input.GetKeyDown("e"))
        {
            Debug.Log("Clicked");
            isOnFire = false;
        }
    }
}
