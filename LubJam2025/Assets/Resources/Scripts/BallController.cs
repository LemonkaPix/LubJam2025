using UnityEngine;

public class BallController : MonoBehaviour
{
    public float maxShootForce = 20f; // Maksymalna siła wystrzału
    public float chargeRate = 10f;   // Prędkość ładowania (siła na sekundę)
    [SerializeField] private float currentCharge = 0f; // Aktualna naładowana siła

    private Rigidbody rb;
    private bool isStationary = true; // Czy kula jest nieruchoma?
    private bool isCharging = false; // Czy trwa ładowanie strzału?

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isStationary)
        {
            Aim();

            // Rozpocznij ładowanie strzału
            if (Input.GetMouseButtonDown(0))
            {
                isCharging = true;
                currentCharge = 0f; // Reset ładunku
            }

            // Kontynuuj ładowanie strzału
            if (Input.GetMouseButton(0) && isCharging)
            {
                currentCharge += chargeRate * Time.deltaTime;
                currentCharge = Mathf.Clamp(currentCharge, 0f, maxShootForce); // Ogranicz do maksymalnej siły
            }

            // Wykonaj strzał
            if (Input.GetMouseButtonUp(0) && isCharging)
            {
                Shoot();
                isCharging = false; // Zakończ ładowanie
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
        rb.AddForce(transform.forward * currentCharge, ForceMode.Impulse); // Użycie załadowanej siły
    }
}