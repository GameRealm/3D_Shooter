using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 playerVelocity;

    [Header("Налаштування руху")]
    public float speed = 6.0f;
    public float gravityValue = -19.62f;

    [Header("Камера та пріоритет миші")]
    public Transform playerCamera;
    public float sensitivity = 2.0f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private InputActions controls;
    private Animator anim;

    private void Awake()
    {
        controls = new InputActions();
        controls.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        yRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        HandleRotation(); // Пріоритет №1
        ApplyMovement();  // Пріоритет №2
    }

    private void HandleRotation()
    {
        // Отримуємо дельту миші (наскільки пікселів вона зсунулася за кадр)
        Vector2 lookInput = controls.Player.Look.ReadValue<Vector2>();

        // Якщо відчуваєш, що 720 міняється на 360, прибираємо множник 0.1f 
        // або замінюємо його на 1.0f для прямого вводу
        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // ПРЯМИЙ ПОВОРОТ: Без Slerp чи Lerp, щоб модель не "доганяла" мишку, 
        // а була прибита до неї залізно.
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        if (playerCamera != null)
        {
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    private void ApplyMovement()
    {
        moveInput = controls.Player.Move.ReadValue<Vector2>();

        // Тепер, коли мишка крутить тіло миттєво, 
        // напрямок руху завжди ідеально збігається з поглядом.
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;

        controller.Move(move * speed * Time.deltaTime);

        // Гравітація
        if (controller.isGrounded && playerVelocity.y < 0) playerVelocity.y = -2f;
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Анімація (використовуємо Magnitude для загальної швидкості)
        if (anim != null)
        {
            anim.SetFloat("Magnitude", moveInput.magnitude, 0.05f, Time.deltaTime);
        }
    }
}