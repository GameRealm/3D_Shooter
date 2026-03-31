using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableObject
    {
        public GameObject prefab;
        [Range(0, 100)]
        public float spawnChance; // Шанс появи (наприклад: 70, 20, 10)
    }

    [Header("Налаштування префабів")]
    public SpawnableObject[] targets;

    [Header("Налаштування часу")]
    public float spawnInterval = 2f; // Частота спавну (раз на 2 секунди)

    private BoxCollider spawnArea;
    private float nextSpawnTime;

    void Awake()
    {
        // Отримуємо коллайдер, який буде межею зони
        spawnArea = GetComponent<BoxCollider>();
        if (spawnArea == null)
        {
            Debug.LogError("На об'єкті SpawnZone має бути BoxCollider (з галочкою Is Trigger)!");
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnRandomTarget();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnRandomTarget()
    {
        GameObject selectedPrefab = GetRandomPrefabByWeight();

        if (selectedPrefab != null)
        {
            Vector3 randomPos = GetRandomPositionInBounds();
            Instantiate(selectedPrefab, randomPos, Quaternion.identity);
        }
    }

    // Розрахунок рандомної позиції всередині BoxCollider
    Vector3 GetRandomPositionInBounds()
    {
        Bounds bounds = spawnArea.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    // Вибір префаба з урахуванням співвідношення (шансів)
    GameObject GetRandomPrefabByWeight()
    {
        float totalWeight = 0;
        foreach (var obj in targets) totalWeight += obj.spawnChance;

        float randomValue = Random.Range(0, totalWeight);
        float currentWeight = 0;

        foreach (var obj in targets)
        {
            currentWeight += obj.spawnChance;
            if (randomValue <= currentWeight)
            {
                return obj.prefab;
            }
        }

        return null;
    }
}