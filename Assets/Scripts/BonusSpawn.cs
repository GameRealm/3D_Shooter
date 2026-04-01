using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
public class BonusSpawner : MonoBehaviour
{
    public GameObject bonusPrefab;
    public int bonusCount = 10;

    private BoxCollider spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();
        SpawnBonuses();
    }

    void SpawnBonuses()
    {
        int spawned = 0;
        int attempts = 0;
        int maxAttempts = bonusCount * 200; 

        Bounds bounds = spawnArea.bounds;

        while (spawned < bonusCount && attempts < maxAttempts)
        {
            attempts++;

            Vector3 randomWorldPoint = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomWorldPoint, out hit, 30f, NavMesh.AllAreas))
            {
                Vector3 spawnPos = hit.position + Vector3.up * 0.5f;
                Instantiate(bonusPrefab, spawnPos, Quaternion.identity);
                spawned++;
            }
        }
    }
}