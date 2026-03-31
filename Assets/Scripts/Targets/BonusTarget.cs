using UnityEngine;

public class BonusTarget : BaseTarget
{
    [Header("Налаштування бонусу")]
    public float secondsToAdd = 10f;

    protected override void Start()
    {
        // Ми НЕ викликаємо base.Start(), тому об'єкт НЕ зникне сам по собі.
        // Він чекатиме лише влучання фаєрбола.

        // Можна додати якийсь візуальний ефект, щоб вона виділялася
        // Наприклад, повільне обертання
    }

    void Update()
    {
        // Бонусна ціль красиво крутиться навколо своєї осі
        transform.Rotate(Vector3.up * 50f * Time.deltaTime);
    }

    // Перевизначаємо метод влучання
    private void OnTriggerEnter(Collider other)
    {
        // Ігноруємо гравця та зону
        if (other.CompareTag("Player") || other.CompareTag("SpawnZone")) return;

        // Перевіряємо шар "Target"
        if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            BaseTarget target = other.GetComponent<BaseTarget>();

            if (target != null)
            {
                // Якщо це бонус — ми можемо або викликати специфічний метод, 
                // або покластися на перевизначений OnHit
                target.OnHit();
            }

            // Знищуємо фаєрбол після влучання в будь-яку ціль
            Destroy(gameObject);
        }
        else
        {
            // Якщо влучили в стіну або підлогу
            Destroy(gameObject);
        }
    }
}