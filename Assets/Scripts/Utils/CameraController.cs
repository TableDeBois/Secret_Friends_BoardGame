using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float sensibilite = 300f; //Sensibilit� de la souri
    private float moveSpeed = 200f;
    private float scrollSpeed = 200f;
    


    private float xRotation = 0f; //rotation verticale
    private bool isRightClickHeld = false;


    // Start is called before the first frame update
    void Start()
    {
        // Verrouiller le curseur au centre de l'�cran pour une meilleure exp�rience de contr�le
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        HandleMouseRotation();
        HandleMouvement();
    }

    private void HandleMouseRotation()
    {
        // V�rifie si le clic droit est enfonc� pour activer la rotation
        if (Input.GetMouseButtonDown(1))
        {
            isRightClickHeld = true;
            Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur lors du clic droit
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRightClickHeld = false;
            Cursor.lockState = CursorLockMode.None; // D�verrouille le curseur en rel�chant le clic droit
        }

        // Si clic droit maintenu, permettre la rotation
        if (isRightClickHeld)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensibilite * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensibilite * Time.deltaTime;

            // Rotation verticale (pitch)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limite de l'angle vertical

            // Applique la rotation verticale
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Applique la rotation horizontale
            transform.Rotate(Vector3.forward * mouseX, Space.World);
        }
    }

    private void HandleMouvement()
    {
        // D�placement de la cam�ra avec les fl�ches directionnelles
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move;

        // Monter et descendre la cam�ra avec la molette de la souris
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        transform.position += Vector3.up * scroll;
    }
}
