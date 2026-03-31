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
        int maxAttempts = bonusCount * 200; // Більше спроб для кращого розкиду

        // Отримуємо межі колайдера у світових координатах
        Bounds bounds = spawnArea.bounds;

        while (spawned < bonusCount && attempts < maxAttempts)
        {
            attempts++;

            // Генеруємо абсолютно випадкову точку в межах СВІТОВОГО куба
            Vector3 randomWorldPoint = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );

            NavMeshHit hit;
            // Шукаємо найближчий NavMesh (збільшуємо радіус пошуку до 30м для пагорбів)
            if (NavMesh.SamplePosition(randomWorldPoint, out hit, 30f, NavMesh.AllAreas))
            {
                // Створюємо бонус БЕЗ прив'язки до батька (щоб не було розтягування)
                Vector3 spawnPos = hit.position + Vector3.up * 0.5f;
                Instantiate(bonusPrefab, spawnPos, Quaternion.identity);

                spawned++;
            }
        }

        Debug.Log($"Спавн завершено! Створено: {spawned} за {attempts} спроб.");
    }
}