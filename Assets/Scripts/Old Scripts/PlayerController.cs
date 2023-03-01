using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float sensX;
    public float sensY;

    private float xRotation = 0f;
    public bool mouseInvert ;


    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var xInput = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        var yInput = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        if (mouseInvert == false)
        {
            xRotation -= yInput;
        }
        else
        {
            xRotation -= yInput;
        }

        xRotation = Mathf.Clamp(xRotation, -20f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * xInput);
        transform.Rotate(Vector3.right* yInput);
    }

    public void CheckMouseInvert()
    {
    }
}