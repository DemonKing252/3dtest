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


        //wheel_front_left.GetComponent<WheelCollider>().motorTorque = wheelTorque * vertical
        //wheel_front_right.GetComponent<WheelCollider>().motorTorque = wheelTorque * vertical;
        //wheel_back_left.GetComponent<WheelCollider>().motorTorque = wheelTorque * vertical;
        //wheel_back_right.GetComponent<WheelCollider>().motorTorque = wheelTorque * vertical;

        rb.maxAngularVelocity = maxAngularVel;

        //wheel_front_left.GetComponent<Rigidbody>().maxAngularVelocity = maxAngularVel;
        //wheel_front_right.GetComponent<Rigidbody>().maxAngularVelocity = maxAngularVel;
        //wheel_back_left.GetComponent<Rigidbody>().maxAngularVelocity = maxAngularVel;
        //wheel_back_right.GetComponent<Rigidbody>().maxAngularVelocity = maxAngularVel;
    }

    public CharacterController controller;
    public Transform cam;
    public Rigidbody rb;

    public Rigidbody[] wheels;

    public Text speedText;

    public float maxAngularVel = 40f;
    public float wheelTorque = 50f;
    public float refreshRate = 1.0f;
    private float timeElapsed = 0.0f;
    public float steering = 15f;



    float realVert = 0f;

    [Serializable]
    public enum Metric
    {
        KPH,
        MPH
    }
    public Metric metric = Metric.KPH;

    public GameObject wheel_front_left;
    public GameObject wheel_front_right;
    public GameObject wheel_back_left;
    public GameObject wheel_back_right;
    Vector3 force = Vector3.zero;

    private float vertical;
    private float horiz;

    // Update is called once per frame
    void Update()
    {

        //foreach (Rigidbody rb in wheels)
        //{
        //    if (maxAngularVel != rb.maxAngularVelocity)
        //        rb.maxAngularVelocity = maxAngularVel;
        //}

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


        //
        //
        horiz = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        //
        //Vector3 moveBy = new Vector3(horiz, 0f, vertical).normalized;
        //
        //float target = Mathf.Atan2(horiz, 0f) * Mathf.Rad2Deg /*+ cam.eulerAngles.y*/;
        ////target = Mathf.Clamp(target, -5f + cam.eulerAngles.y, +5 + cam.eulerAngles.y);
        //
        //float steering = (horiz * 30f);
        //wheel_front_left.transform.rotation  = Quaternion.Euler(wheel_front_left.transform.rotation.eulerAngles.x , steering, wheel_front_left.transform.rotation.eulerAngles.z );
        //wheel_front_right.transform.rotation = Quaternion.Euler(wheel_front_right.transform.rotation.eulerAngles.x, steering, wheel_front_right.transform.rotation.eulerAngles.z);
        //
        ////transform.rotation = Quaternion.Euler(0f, target, 0f);
        //Vector3 moveDir = Quaternion.Euler(0f, target, 0f) * moveBy;
        //
        ////Vector3 force = new Vector3(transform.rotation.eulerAngles.x, 0f, transform.rotation.eulerAngles.z) * 40f;
        //realVert = Mathf.Lerp(realVert, vertical, Time.deltaTime * 0.3f);
        //
        //force = transform.right * vertical * wheelForce;




        ////controller.SimpleMove(moveDir * speed);
        //foreach (Rigidbody rb in wheels)
        //{
        //    rb.AddTorque(force, ForceMode.Acceleration);
        //}
        Vector3 position1;
        Quaternion rot1;
        wheel_front_left.GetComponent<WheelCollider>().GetWorldPose(out position1, out rot1);

        Vector3 position2;
        Quaternion rot2;
        wheel_front_right.GetComponent<WheelCollider>().GetWorldPose(out position2, out rot2);


        wheel_front_left.transform.rotation = rot1;
        wheel_front_right.transform.rotation = rot2;

    }
    void FixedUpdate()
    {
        wheel_front_left.GetComponent<WheelCollider>().motorTorque = wheelTorque * vertical;
        wheel_front_right.GetComponent<WheelCollider>().motorTorque = wheelTorque * vertical;
        wheel_back_left.GetComponent<WheelCollider>().motorTorque = wheelTorque * vertical;
        wheel_back_right.GetComponent<WheelCollider>().motorTorque = wheelTorque * vertical;

        wheel_front_left.GetComponent<WheelCollider>().steerAngle = horiz * steering;
        wheel_front_right.GetComponent<WheelCollider>().steerAngle = horiz * steering;


    }
}
