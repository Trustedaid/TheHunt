using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCam : MonoBehaviour
{
    public float mouseSens;

    public Transform bodyCamPos;

    private float xRotation;

    private float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
    }

    public void MouseMovement()
    {
        var inputX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSens;
        var inputY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSens;
        

        xRotation -= inputY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation= Quaternion.Euler(xRotation, 0f, 0f);
        bodyCamPos.Rotate(Vector3.up * inputX);
    }
}