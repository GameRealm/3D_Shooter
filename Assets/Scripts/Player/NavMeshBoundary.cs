using UnityEngine;
using UnityEngine.AI;

public class NavMeshBoundary : MonoBehaviour
{
    private CharacterController controller;

    [Header("Налаштування кордону")]
    public float checkRadius = 0.5f;   // Радіус перевірки прямо під ногами
    public float searchRadius = 10.0f; // Як далеко шукаємо берег, якщо випали
    public float pushForce = 8.0f;    // Сила повернення

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        NavMeshHit hit;

        // 1. Перевіряємо, чи ми ЗАРАЗ на NavMesh (у маленькому радіусі)
        // Якщо під нами немає синьої сітки:
        if (!NavMesh.SamplePosition(transform.position, out hit, checkRadius, NavMesh.AllAreas))
        {
            // 2. Шукаємо найближчу точку NavMesh у більшому радіусі (шукаємо берег)
            if (NavMesh.SamplePosition(transform.position, out hit, searchRadius, NavMesh.AllAreas))
            {
                // 3. Розраховуємо напрямок до цієї точки
                Vector3 directionToLand = (hit.position - transform.position).normalized;
                directionToLand.y = 0;

                // 4. Штовхаємо гравця назад
                controller.Move(directionToLand * pushForce * Time.deltaTime);

                // Візуалізація лінії до берега в Scene View (червона лінія)
                Debug.DrawLine(transform.position, hit.position, Color.red);
            }
        }
    }
}