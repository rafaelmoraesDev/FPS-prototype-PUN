using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivity = 50;
    public Transform Player;

    private float xRotation = 0f;

    private void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * this.MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * this.MouseSensitivity * Time.deltaTime;

        this.xRotation -= mouseY;
        this.xRotation = Mathf.Clamp(this.xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(this.xRotation, 0f, 0f);
        this.Player.Rotate(Vector3.up * mouseX);
    }
}