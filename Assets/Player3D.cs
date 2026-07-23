using System;
using UnityEngine;
using UnityEngine.UI;

public class Player3D : MonoBehaviour
{
    public static Player3D instance { get; private set; }
    [SerializeField] Joystick joystick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Vector3 inputVector = Vector3.zero;

    [SerializeField] float moveSpeed;
    float zDirection;
    // Update is called once per frame

    void Awake()
    {
        instance = this;
    }
    public void DecreaseZ()
    {
        zDirection = -1;
    }

    public void IncreaseZ()
    {
        zDirection = 1;


    }

    public void Stop()
    {
        zDirection = 0;
    }

    void Update()
    {
        inputVector.x = -joystick.Horizontal;
        inputVector.y = joystick.Vertical;
        inputVector.z = zDirection;
        transform.position += inputVector * moveSpeed * Time.deltaTime;

    }
}
