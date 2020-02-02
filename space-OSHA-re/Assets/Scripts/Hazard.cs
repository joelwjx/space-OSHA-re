using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public static int hazardsFound { get; private set; }

    [SerializeField, Tooltip("The message to display when the player finds and interacts with this hazard")]
    private string foundMessage;

    public UnityEngine.Events.UnityEvent onFound;

    private bool playerInArea;
    private bool found;

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!found && playerInArea && Input.GetKeyDown("e"))
        {
            found = true;
            if(foundMessage != null && foundMessage.Length > 0)
            {
                HazardMessage.Instance.PlayMessage(foundMessage);
            }
            hazardsFound++;
            onFound.Invoke();
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
