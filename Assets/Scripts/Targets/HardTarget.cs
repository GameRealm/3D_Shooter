using UnityEngine;

public class AwaySpiralTarget : BaseTarget
{
    [Header("Налаштування спіралі від гравця")]
    public float spiralSpeed = 5f;      // Швидкість крутіння
    public float moveAwaySpeed = 4f;    // Швидкість віддалення від гравця
    public float spiralRadius = 2f;     // Радіус спіралі
    public bool shrinkOverTime = true;  // Чи зменшується радіус (ефект видалення в точку)

    private Vector3 startPos;
    private Vector3 moveDirection;      // Напрямок польоту
    private float timer = 0f;

    protected override void Start()
    {
        lifetime = 3f;
        base.Start();

        startPos = transform.position;

        // Визначаємо напрямок: від камери до цієї цілі
        // Це змусить ціль летіти рівно "від гравця"
        if (Camera.main != null)
        {
            moveDirection = (transform.position - Camera.main.transform.position).normalized;
        }
        else
        {
            moveDirection = Vector3.forward; // Дефолт, якщо камери немає
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 1. Рахуємо радіус (може звужуватися)
        float currentRadius = spiralRadius;
        if (shrinkOverTime)
        {
            currentRadius = spiralRadius * (1f - (timer / lifetime));
        }

        // 2. Локальне зміщення спіралі (по колу)
        // Використовуємо Sin та Cos для створення координат на площині
        float xOffset = Mathf.Cos(timer * spiralSpeed) * currentRadius;
        float yOffset = Mathf.Sin(timer * spiralSpeed) * currentRadius;

        // 3. Рух вперед ("від гравця")
        Vector3 forwardMovement = moveDirection * (timer * moveAwaySpeed);

        // 4. Фінальна позиція: 
        // Початкова точка + політ вперед + зміщення вбік/вгору відносно напрямку польоту
        // Використовуємо Quaternion.LookRotation, щоб спіраль завжди була перпендикулярна польоту
        Quaternion rotationToMoveDir = Quaternion.LookRotation(moveDirection);
        Vector3 sideOffset = rotationToMoveDir * new Vector3(xOffset, yOffset, 0);

        transform.position = startPos + forwardMovement + sideOffset;

        // Обертання самої моделі
        transform.Rotate(Vector3.forward * 300f * Time.deltaTime);
    }
}