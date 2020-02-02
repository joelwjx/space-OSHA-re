using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawnPoint : MonoBehaviour
{
    [SerializeField, Range(0,100f)] private float xRange = 0;
    [SerializeField, Range(0,100f)] private float yRange = 0;

    [SerializeField] private HazardList hazards;

    private bool alreadySpawned => currentHazard != null;

    private Hazard currentHazard;

    public void Spawn()
    {
        if (alreadySpawned) return;

        var go = Instantiate(hazards.hazards[Random.Range(0, hazards.hazards.Count)].gameObject, transform);
        currentHazard = go.GetComponent<Hazard>();
        currentHazard.transform.localPosition
            = new Vector3(
                Random.Range(-xRange, xRange),
                Random.Range(-yRange, yRange),
                0);

        currentHazard.transform.rotation = Quaternion.identity;

        currentHazard.onFound.AddListener(OnHazardFound);
    }

    private void OnHazardFound()
    {
        currentHazard.onFound.RemoveListener(OnHazardFound);
        currentHazard = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(xRange, yRange, 0));
    }
}
