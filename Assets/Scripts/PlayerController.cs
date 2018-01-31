using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movement variabels
    public float speed = 1.0f;
    public int invert = 1;//negative int for mouse inversion
    public float tilt = 3.0f;
    Rigidbody shipRigidbody;


    //weapon variables
    public Rigidbody blasterBolt;
    public float boltVelocity = 1.0f;
    public Transform[] blasterGuns;


    void Start ()
    {
        Cursor.visible = false;//temporarily
        Cursor.lockState = CursorLockMode.Locked;

        shipRigidbody = GetComponent<Rigidbody>();

	}
	
	
	void Update ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical") * invert;
        
        Vector3 movementVector = new Vector3(horizontal, vertical, 0) * speed*Time.deltaTime;
        Vector3 finalDirection = new Vector3(horizontal, vertical, 10.0f);
        //shipRigidbody.AddRelativeForce(movementVector, ForceMode.Acceleration);
        shipRigidbody.velocity = movementVector;
        //transform.position += movementVector;
        
        transform.rotation=Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection),Mathf.Deg2Rad* tilt);


        if (Input.GetButtonDown("Fire1"))
        {
            foreach (Transform blaster in blasterGuns)
            {
                Rigidbody spawnedBolt = Instantiate(blasterBolt, blaster.position, Quaternion.Euler(90, transform.rotation.y, transform.rotation.z));
                spawnedBolt.AddForce(transform.forward * boltVelocity, ForceMode.VelocityChange);
            }
        }
        
	}
}
