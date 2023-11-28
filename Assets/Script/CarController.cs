using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject carbody;
    public float forwardSpeed = 5f;
    public float maxSpeed = 30f;
    public float turnSpeed = 10f;
    public float gravityforce = 10f;
    private float vertical;
    private float horizontal;

    public LayerMask whatIsGround;
    public float groundRayLength = 0.5f;
    public Transform groundRayPoint;
    public bool grounded;
    public TMP_Text speedtext;

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, horizontal * turnSpeed*Time.deltaTime,0));
        carbody.transform.position = this.transform.position + new Vector3(0, -0.5f, 0);
        carbody.transform.rotation = this.transform.rotation;

        UpdateUI();
    }
    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;
        if (Physics.Raycast(groundRayPoint.position, -transform.up,out hit,groundRayLength,whatIsGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        if (grounded)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.forward * vertical * forwardSpeed);
            }
        }
        
      
        
        
    }
    public void UpdateUI()
    {
        speedtext.text = Mathf.FloorToInt(rb.velocity.magnitude*3.6f) + " kph";
    }
}
