// Some automatic door system for your games :)
// Make sure your player is tagged "Player"


using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform doorHinge;       // Reference to the door's hinge (parent of the door)
    public float openAngle = -90f;    // The angle to rotate when door opens (negative for a leftward rotation)
    public float openSpeed = 2f;      // Speed of door opening and closing
    private bool isOpen = false;      // To check if the door is open or closed
    private Quaternion closedRotation;  // Initial rotation of the door (closed state)
    private Quaternion openRotation;    // Target rotation when door is open
    public AudioSource openDoor;
    public AudioSource closeDoor;

    void Start()
    {
        // Store the initial rotation (closed rotation) of the door hinge
        closedRotation = doorHinge.rotation;
        
        // Calculate the open rotation by rotating around the Z-axis
        openRotation = Quaternion.Euler(doorHinge.eulerAngles.x, doorHinge.eulerAngles.y, doorHinge.eulerAngles.z + openAngle);
    }

    // When the player enters the trigger area, open the door
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Open the door
            isOpen = true;
            closeDoor.Stop();
            openDoor.Play();
        }
    }

    // When the player exits the trigger area, close the door
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Close the door
            isOpen = false;
            openDoor.Stop();
            closeDoor.Play();
        }
    }

    void Update()
    {
        // Smoothly rotate the door to the open or closed position
        if (isOpen)
        {
            // Rotate to the open rotation
            doorHinge.rotation = Quaternion.Slerp(doorHinge.rotation, openRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            // Rotate back to the closed rotation
            doorHinge.rotation = Quaternion.Slerp(doorHinge.rotation, closedRotation, Time.deltaTime * openSpeed);
        }
    }
}
