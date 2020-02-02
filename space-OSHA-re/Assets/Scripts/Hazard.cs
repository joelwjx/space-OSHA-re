using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public static int hazardsFound { get; private set; }

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
