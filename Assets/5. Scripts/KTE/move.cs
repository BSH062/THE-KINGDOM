using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed;
    public float turnSpeedY;
    public float turnSpeedX;
    float mouseY;
    float mouseX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        mouseY += Input.GetAxis("Mouse X") * turnSpeedX * Time.deltaTime;
        mouseX += Input.GetAxis("Mouse Y") * turnSpeedY * Time.deltaTime;

        mouseX = Mathf.Clamp(mouseX, -40.0f, 40.0f);

        transform.localEulerAngles = new Vector3(-mouseX, mouseY, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float zm = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float xm = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(xm, 0, zm);
    }
}
