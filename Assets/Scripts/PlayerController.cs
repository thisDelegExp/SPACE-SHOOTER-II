using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movement variabels
    public float speed = 1.0f;
    public float targetDistance = 10.0f;
    Rigidbody shipRigidbody;
    

    //weapon variables
    public GameObject blasterBolt;
    public float boltVelocity = 1.0f;
    public Transform[] blasterGuns;


     void Start()
    {
        Cursor.visible = false;//temporarily
        //Cursor.lockState = CursorLockMode.Locked;
        shipRigidbody = GetComponent<Rigidbody>();
    }  

    void Update()
    {
        Vector2 mouse = Input.mousePosition;

       Ray mouseRay = Camera.main.ScreenPointToRay(mouse);
        float targetPoint = transform.position.magnitude + targetDistance;
        Vector3 target = mouseRay.origin + mouseRay.direction * targetPoint;
        transform.LookAt(target);
        foreach (Transform blaster in blasterGuns)
            blaster.LookAt(target);
        target.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, target, speed);


        if (Input.GetButtonDown("Fire1"))
        {
            foreach (Transform blaster in blasterGuns)
            {
                GameObject bolt = Instantiate(blasterBolt, blaster.position, blaster.rotation) as GameObject;
                bolt.GetComponent<Rigidbody>().AddForce(transform.forward * boltVelocity, ForceMode.Impulse);

            }
        }

    }
}



        





   


    