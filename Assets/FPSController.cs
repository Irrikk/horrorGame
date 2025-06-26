using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 7f;    // Скорость ходьбы
    public float runSpeed = 13f;    // Скорость бега

    [Header("Mouse Settings")]
    public float mouseSensitivity = 2f; // Чувствительность мыши
    public float maxLookAngle = 90f;    // Макс. угол взгляда вверх/вниз

    private CharacterController characterController;
    private Camera playerCamera;
    private float verticalRotation = 0f;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked; // Курсор скрыт и в центре
        Cursor.visible = false;
    }

    void Update()
    {
        // === Движение ===
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Бег (зажата Shift)
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * currentSpeed * Time.deltaTime);

        // === Вращение камеры ===
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(0, mouseX, 0);
    }
}