using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CarEngine : MonoBehaviour
{
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        
    }

    public float speed = 3f;
    public CharacterController controller;
    public Transform cam;
    public Rigidbody rb;

    public Rigidbody[] wheels;

    public Text speedText;

    public float maxAngularVel = 40f;
    public float wheelForce = 50f;
    public float refreshRate = 1.0f;
    private float timeElapsed = 0.0f;


    float realVert = 0f;

    [Serializable]
    public enum Metric
    {
        KPH,
        MPH
    }
    public Metric metric = Metric.KPH;

    // Update is called once per frame
    void Update()
    {

        foreach (Rigidbody rb in wheels)
        {
            if (maxAngularVel != rb.maxAngularVelocity)
                rb.maxAngularVelocity = maxAngularVel;
        }

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= refreshRate)
        {
            timeElapsed = 0f;
            Vector2 lateralSpeed = new Vector2(rb.velocity.x, rb.velocity.z);

            float conversion = (metric == Metric.KPH ? 3.6f : 2.236f);
            string speed = (lateralSpeed.magnitude * conversion).ToString("F1");
            //string speed_mph = (lateralSpeed.magnitude * 2.236f).ToString("F1");


            speedText.text = "Speed: " + /*lateralSpeed.magnitude.ToString("F1") */ speed + (metric == Metric.KPH ? " km/h" : " mph");
        }


        float horiz = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveBy = new Vector3(horiz, 0f, vertical).normalized;

        float target = Mathf.Atan2(horiz, 0f) * Mathf.Rad2Deg /*+ cam.eulerAngles.y*/;
        //target = Mathf.Clamp(target, -5f + cam.eulerAngles.y, +5 + cam.eulerAngles.y);

        //transform.rotation = Quaternion.Euler(0f, target, 0f);
        Vector3 moveDir = Quaternion.Euler(0f, target, 0f) * moveBy;

        //Vector3 force = new Vector3(transform.rotation.eulerAngles.x, 0f, transform.rotation.eulerAngles.z) * 40f;
        realVert = Mathf.Lerp(realVert, vertical, Time.deltaTime * 0.3f);

        Vector3 force = transform.right * vertical * wheelForce;

        
        //controller.SimpleMove(moveDir * speed);
        foreach (Rigidbody rb in wheels)
        {
            rb.AddTorque(force, ForceMode.Acceleration);
        }
    }
}
