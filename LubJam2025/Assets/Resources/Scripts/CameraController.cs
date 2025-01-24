using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera; // Kamera główna
    public Transform thirdPersonCameraPosition; // Pozycja kamery trzecioosobowej
    public Transform playerTransform; // Transform gracza (kuli)
    public float transitionSpeed = 5f;

    private bool isThirdPerson = false; // Czy kamera jest w trzecioosobowym trybie?

    void Update()
    {
        if (isThirdPerson)
        {
            // Obracaj kamerę za graczem tylko w osi Y
            Vector3 cameraOffset = thirdPersonCameraPosition.position - playerTransform.position;
            cameraOffset = Quaternion.Euler(0, playerTransform.eulerAngles.y, 0) * cameraOffset;

            // Ustaw pozycję i rotację kamery
            Vector3 targetPosition = playerTransform.position + cameraOffset;
            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position,
                targetPosition,
                Time.deltaTime * transitionSpeed
            );

            // Kamera patrzy na gracza
            mainCamera.transform.LookAt(playerTransform.position);
        }
        else
        {
            // Tryb pierwszoosobowy
            mainCamera.transform.localPosition = Vector3.zero;
            mainCamera.transform.localRotation = Quaternion.identity;
        }
    }

    public void SetThirdPersonCamera(bool state)
    {
        isThirdPerson = state;
    }
}