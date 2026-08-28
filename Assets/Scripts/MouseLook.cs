using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 300f;
    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        // Khóa con trỏ chuột vào giữa màn hình khi chạy game
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Lấy thông tin khi di chuyển chuột
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Khóa góc nhìn lên xuống để không bị lật ngược đầu

        yRotation += mouseX;

        // Xoay Camera
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}