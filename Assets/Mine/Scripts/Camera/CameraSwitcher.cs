using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Camera Positions")]
    public Transform[] cameraPositions; // Assign your 3 camera positions in inspector
    public float moveSpeed = 5f;        // Smooth movement speed

    private int currentIndex = 0;

    void Update()
    {
        // Flick up/down with mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f) // scroll up
        {
            currentIndex = Mathf.Max(0, currentIndex - 1);
        }
        else if (scroll < 0f) // scroll down
        {
            currentIndex = Mathf.Min(cameraPositions.Length - 1, currentIndex + 1);
        }

        // Smoothly move and rotate camera
        if (cameraPositions.Length > 0)
        {
            Transform target = cameraPositions[currentIndex];
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * moveSpeed);
        }
    }
}
