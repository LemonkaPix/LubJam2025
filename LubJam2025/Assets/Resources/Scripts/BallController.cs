using UnityEngine;

public class BallController : MonoBehaviour
{
    public float maxShootForce = 20f; // Maksymalna siła wystrzału
    public float chargeRate = 10f; // Prędkość ładowania (siła na sekundę)
    public float minimalSpeed = .1f; // Prędkość ładowania (siła na sekundę)
    [SerializeField] private float currentCharge = 0f; // Aktualna naładowana siła

    private Rigidbody rb;
    private bool isStationary = true; // Czy kula jest nieruchoma?
    private bool isCharging = false; // Czy trwa ładowanie strzału?

    [SerializeField] private Camera fpsCam;
    [SerializeField] private Camera thrdCam;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Aim();
        if (isStationary)
        {
            fpsCam.enabled = true;
            thrdCam.enabled = false;
            if (rb.velocity.magnitude >= minimalSpeed) // Minimalna prędkość
            {
                isStationary = false;
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                isCharging = true;
                currentCharge = 0f; // Reset ładunku
            }

            if (Input.GetMouseButton(0) && isCharging)
            {
                currentCharge += chargeRate * Time.deltaTime;
                currentCharge = Mathf.Clamp(currentCharge, 0f, maxShootForce); // Ogranicz do maksymalnej siły
            }

            if (Input.GetMouseButtonUp(0) && isCharging)
            {
                Shoot();
                isCharging = false; // Zakończ ładowanie
            }
        }
        else
        {
            fpsCam.enabled = false;
            thrdCam.enabled = true;
            print(rb.velocity);
            if (rb.velocity.magnitude < minimalSpeed) // Minimalna prędkość
            {
                rb.velocity = Vector3.zero;
                isStationary = true;
            }
        }
    }

    void Aim()
    {
        float horizontal = Input.GetAxis("Mouse X") * 5f;
        transform.Rotate(0, horizontal, 0, Space.World); // Rotacja wokół globalnej osi Y

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