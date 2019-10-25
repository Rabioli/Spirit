using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarScript : MonoBehaviour
{
    // Start is called before the first frame update

    void Update()
    {
        healthText.text = "Health" + health.ToString();
        if (health<=0)
        {
            GameOver();
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Enemy") {
            health--;
            Destroy(col.gameObject);
         }
    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        frontWheelLCol.steerAngle = steeringAngle;
        frontWheelRCol.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        frontWheelLCol.motorTorque = verticalInput * motorForce;
        frontWheelRCol.motorTorque = verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontWheelLCol, frontWheelLT);
        UpdateWheelPose(frontWheelRCol, frontWheelRT);
        UpdateWheelPose(backWheelLCol, backWheelLT);
        UpdateWheelPose(backWheelRCol, backWheelRT);
    }
    private void UpdateWheelPose(WheelCollider col, Transform transf)
    {
        Vector3 _pos = transf.position;
        Quaternion _quat = transf.rotation;
        col.GetWorldPose(out _pos, out _quat);
        transf.position = _pos;
        transf.rotation = _quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    void GameOver()
    {
        healthText.text = "Game Over";
        enabled = false;
    }

    public float health = 10;
    public Text healthText;


    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    public WheelCollider frontWheelLCol, frontWheelRCol;
    public WheelCollider backWheelLCol, backWheelRCol;
    public Transform frontWheelLT, frontWheelRT;
    public Transform backWheelLT, backWheelRT;
    public float maxSteerAngle = 30;
    public float motorForce = 50;

}
