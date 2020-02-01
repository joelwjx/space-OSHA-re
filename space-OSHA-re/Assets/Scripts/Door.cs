using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField, Tooltip("The door system that controls this door, null for manual opening")]
    private DoorSubsystem doorSystem;
    [SerializeField, Range(0, 4), Tooltip("The level of this door, higher levels require more power")]
    private int level;
    [SerializeField, Range(0, 60), Tooltip("How long (in seconds) it takes for the door to close after being opened")]
    private float closeTime = 10;

    // Opening and closing the door graphics/physics handled through these events
    public UnityEngine.Events.UnityEvent onOpen;
    public UnityEngine.Events.UnityEvent onClose;

    public bool isOpen { get; private set; }

    private bool canOpen => !doorSystem || doorSystem.doorLevel >= level;

    private bool playerInArea = false;
    [SerializeField] private float closeTimer;

    // Start is called before the first frame update
    void Start()
    {
        if(!doorSystem)
        {
            level = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isOpen && canOpen && playerInArea && Input.GetKeyDown("e"))
        {
            isOpen = true;
            onOpen.Invoke();
            closeTimer = 0;
        }
        else if(closeTimer > closeTime || (isOpen && playerInArea && Input.GetKeyDown("e")))
        {
            isOpen = false;
            onClose.Invoke();
        }

        if(isOpen)
        {
            closeTimer += Time.deltaTime;
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            playerInArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }
}
