using UnityEngine;

public class SimpleTarget : BaseTarget
{
    private Vector3 initialScale;

    protected override void Start()
    {
        // Викликаємо Start із базового класу (BaseTarget), щоб запустилося видалення
        base.Start();
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Розраховуємо, скільки часу залишилося до видалення
        // (Time.time - spawnTime — це скільки об'єкт уже живе)
        // Але простіше відстежувати це через lifetime

        float timeToLive = lifetime; // Беремо значення з BaseTarget

        // Використовуємо простий таймер всередині об'єкта
        lifetime -= Time.deltaTime;

        // Якщо залишилася 1 секунда або менше — починаємо зменшувати
        if (lifetime <= 1f && lifetime > 0)
        {
            // Плавне зменшення від початкового розміру до нуля за 1 секунду
            transform.localScale = Vector3.Lerp(Vector3.zero, initialScale, lifetime);
        }
    }
}