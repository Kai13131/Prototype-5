using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public Rigidbody rb;

    public Camera camera;
    public Transform cameraPivot;

    public float MouseSensitivity = 3f;
    float xRotation = 0f;

    public float jumpForce = 8f;
    private int jumpCount = 0;

    public float wallSpeed = 8f;
    public bool isWallRunning = false;

    public Vector3 cameraOffset = new Vector3(0.5f, 0f, -3);
    public Vector3 aimCameraOffset = new Vector3(1.5f, 0f, -1.5f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.gravity = new Vector3(0, -20f, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CameraMove();
        Jump();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * z + transform.right * x;
        move = move * speed;

        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y,move.z);

        if (isWallRunning)
        {
            move = transform.forward * z + transform.right * x;
            move *= wallSpeed;

            rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
        }
    }
    void CameraMove()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        transform.Rotate(0, mouseX, 0);

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x,
                0f,
                rb.linearVelocity.z
            );
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;

            if (jumpCount >= 2)
            {
                jumpForce = 10f;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Player touched the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            jumpCount = 0;
            isWallRunning = true;
        }
    }
}
