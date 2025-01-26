using System;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public static BallController instance;
    public float maxShootForce = 20f; // Maksymalna siła wystrzału
    public float chargeRate = 10f; // Prędkość ładowania (siła na sekundę)
    public float minimalSpeed = .1f; // Prędkość ładowania (siła na sekundę)
    [SerializeField] private float currentCharge = 0f; // Aktualna naładowana siła

    public Rigidbody rb;
    private bool isStationary = true; // Czy kula jest nieruchoma?
    private bool isCharging = false; // Czy trwa ładowanie strzału?

    [SerializeField] private Camera fpsCam;
    [SerializeField] private Camera thrdCam;

    [SerializeField] private Slider _slider;
    [SerializeField] private ParticleSystem jumpParticle;
    public ParticleSystem stickParticle;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        _slider.maxValue = maxShootForce;
    }

    void Update()
    {
        Aim();
        if (isStationary)
        {
            fpsCam.transform.gameObject.SetActive(true);
            thrdCam.transform.gameObject.SetActive(false);
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
                _slider.value = currentCharge;
            }

            if (Input.GetMouseButtonUp(0) && isCharging)
            {
                Shoot();
                rb.useGravity = true;

                isCharging = false; // Zakończ ładowanie
                currentCharge = 0;
                _slider.value = currentCharge;

            }
        }
        else
        {
            fpsCam.transform.gameObject.SetActive(false);
            thrdCam.transform.gameObject.SetActive(true);
            // print(rb.velocity);
            if (rb.velocity.magnitude < minimalSpeed) // Minimalna prędkość
            {
                Stationary();
            }
            else
            {
                rb.useGravity = true;
            }
        }
    }

    public void Stationary()
    {
        rb.velocity = Vector3.zero;
        isStationary = true;
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
        // print("Shoot");
        jumpParticle.Play();
        rb.useGravity = true;
        isStationary = false;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * currentCharge, ForceMode.Impulse); // Użycie załadowanej siły
    }
}