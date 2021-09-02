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
    public Text rpmText;

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
    public float _lateralSpeed;

    // Update is called once per framef
    float conversion;
    public void OnThrottleChanged(float val)
    {
        //vertical = val;
    }
    void Update()
    {

        wheel1_rpm = wheel_front_left.GetComponent<WheelCollider>().rpm;
        //avg_rpm = ((wheel1_rpm));// * 11f) + 737f;


        _lateralSpeed = new Vector2(rb.velocity.x, rb.velocity.z).magnitude * conversion;

        avg_rpm = (new Vector2(rb.velocity.x, rb.velocity.z).magnitude * 242f) + 737f;

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= refreshRate)
        {
            // Speed

            timeElapsed = 0f;
            Vector2 lateralSpeed = new Vector2(rb.velocity.x, rb.velocity.z);
        
            conversion = (metric == Metric.KPH ? 3.6f : 2.236f);
            string speed = (lateralSpeed.magnitude * conversion).ToString("F1");
            
        
            speedText.text = "Speed: " + /*lateralSpeed.magnitude.ToString("F1") */ speed + (metric == Metric.KPH ? " km/h" : " mph");

            // RPM

            rpmText.text = "Engine: " + avg_rpm.ToString("F1") + " RPM";
        }

        //
        //
        horiz = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 position1;
        Quaternion rot1;
        wheel_front_left.GetComponent<WheelCollider>().GetWorldPose(out position1, out rot1);

        Vector3 position2;
        Quaternion rot2;
        wheel_front_right.GetComponent<WheelCollider>().GetWorldPose(out position2, out rot2);

        Vector3 position3;
        Quaternion rot3;
        wheel_back_left.GetComponent<WheelCollider>().GetWorldPose(out position3, out rot3);

        Vector3 position4;
        Quaternion rot4;
        wheel_back_right.GetComponent<WheelCollider>().GetWorldPose(out position4, out rot4);



        wheel_front_left.transform.rotation = rot1;
        wheel_front_right.transform.rotation = rot2;
        wheel_back_left.transform.rotation = rot3;
        wheel_back_right.transform.rotation = rot4;



    }
    private float wheel1_rpm = 0f;
    private float wheel2_rpm = 0f;
    private float wheel3_rpm = 0f;
    private float wheel4_rpm = 0f;
    public float avg_rpm = 0f;

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
