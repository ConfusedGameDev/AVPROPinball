using UnityEngine;

public class PositionAdjuster : MonoBehaviour
{
    public Transform playerCamera;
    public Vector3 Offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = playerCamera.position +Offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
