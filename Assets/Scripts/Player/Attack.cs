using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [Header("Налаштування магії")]
    public GameObject fireballPrefab; 
    public Transform spawnPoint;    
    public float fireRate = 0.5f;   

    private float nextFireTime;
    private InputActions controls;
    private Animator anim;

    private void Awake()
    {
        controls = new InputActions();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Player.Fire.performed += context => StartAttack();
        controls.Player.Fire.canceled += context => EndAttack();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void StartAttack()
    {
        if (Time.time >= nextFireTime)
        {
            // 1. Вмикаємо параметр IsCasting для Blend Tree (плавне перемикання)
            anim.SetFloat("IsCasting", 1f);

            // 2. Якщо каст — це миттєва дія, можна використовувати Trigger
            anim.SetTrigger("Cast");

            nextFireTime = Time.time + fireRate;
        }
    }

    private void EndAttack()
    {
        // Коли відпускаємо кнопку, повертаємо аніматор у звичайний стан
        anim.SetFloat("IsCasting", 0f);
    }

    // ЦЕЙ МЕТОД МАЄ ВИКЛИКАТИСЯ ЧЕРЕЗ ANIMATION EVENT
    public void ShootFireball()
    {
        float screenHeight = Screen.height;
        float aimY = 0.5f + (250f / screenHeight);

        // Створюємо промінь, який б'є ТОЧНО в коло прицілу
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, aimY, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        // ВАЖЛИВО: Використовуємо шар (LayerMask), щоб ігнорувати гравця
        // Або просто перевіряємо, щоб дистанція була не занадто малою
        if (Physics.Raycast(ray, out hit, 100f))
        {
            // Якщо точка надто близько до гравця (менше 2 метрів), ігноруємо її
            if (Vector3.Distance(spawnPoint.position, hit.point) < 2f)
                targetPoint = ray.GetPoint(100f);
            else
                targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }

        GameObject ball = Instantiate(fireballPrefab, spawnPoint.position, Quaternion.identity);


        ball.transform.LookAt(targetPoint);

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {

            rb.linearVelocity = ball.transform.forward * 40f; // Використовуйте linearVelocity в Unity 6
        }
    }
}