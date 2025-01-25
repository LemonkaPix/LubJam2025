using UnityEngine;

public class CameraThirdPersonFollow : MonoBehaviour
{
    public Transform player; // Referencja do gracza
    public Vector3 offset;   // Stała pozycja kamery względem gracza

    void LateUpdate()
    {
        // Ustaw pozycję kamery względem gracza
        transform.position = player.position + offset;

        // Ustaw rotację kamery, aby podążała za graczem tylko w osi Y
        Quaternion targetRotation = Quaternion.Euler(0, player.eulerAngles.y, 0);
        transform.rotation = targetRotation;
    }
}