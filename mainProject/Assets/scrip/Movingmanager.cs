using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movingmanager : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;

    private float speed = 10;

    private float sensX = 30;
    private float sensY = 30;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Transform>().position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Transform>().position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Transform>().position += transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Transform>().position -= transform.right * speed * Time.deltaTime;
        }

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation += mouseY;

        transform.rotation =Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(-xRotation, yRotation, 0);
    }
}
