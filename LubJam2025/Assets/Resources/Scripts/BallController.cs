using UnityEngine;

public class BallController : MonoBehaviour
{
    public float shootForce = 10f; // Siła wystrzału
    private Rigidbody rb;

    private bool isStationary = true; // Czy kula jest nieruchoma?

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Tryb celowania
        Aim();
        if (isStationary)
        {
            if (Input.GetMouseButtonDown(0)) // Strzał lewym przyciskiem myszy
            {
                Shoot();
            }
        }
        else
        {
            // Sprawdzanie, czy kula się zatrzymała
            if (rb.velocity.magnitude < 0.1f) // Minimalna prędkość
            {
                rb.velocity = Vector3.zero;
                isStationary = true;
            }
        }
    }

    void Aim()
    {
        // Rotacja horyzontalna wokół globalnej osi Y
        float horizontal = Input.GetAxis("Mouse X") * 5f;
        transform.Rotate(0, horizontal, 0, Space.World); // Rotacja wokół globalnej osi Y

        // Rotacja wertykalna wokół lokalnej osi X
        float vertical = -Input.GetAxis("Mouse Y") * 5f;
        transform.Rotate(vertical, 0, 0, Space.Self); // Rotacja wokół lokalnej osi X
    }

    void Shoot()
    {
        isStationary = false;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * shootForce, ForceMode.Impulse);
    }
}