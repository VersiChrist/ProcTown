
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    bool move;
    Rigidbody rb;
    Quaternion targetRotation;
    public LayerMask layer;


    // Start is called before the first frame update
    void Start()
    {
        HouseGeneration.population++;
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("ChangeDirection", 5f, 5f);
        InvokeRepeating("Halt", 0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(move)
            rb.MovePosition(transform.position + transform.forward * 3 * Time.deltaTime);

        if (transform.position.z < 2f && transform.position.x > 2f)
        {
            CancelInvoke("ChangeDirection");
            transform.rotation = Quaternion.Euler(0, 0, 0);
            InvokeRepeating("ChangeDirection", 5f, 5f);
        }   

        if (transform.position.z > 2f && transform.position.x < 2f)
        {
            CancelInvoke("ChangeDirection");
            transform.rotation = Quaternion.Euler(0, 90, 0);
            InvokeRepeating("ChangeDirection", 5f, 5f);
        }     

        if (transform.position.z > 2f && transform.position.x > 38f)
        {
            CancelInvoke("ChangeDirection");
            transform.rotation = Quaternion.Euler(0, -90, 0);
            InvokeRepeating("ChangeDirection", 5f, 5f);
        }
               
        if (transform.position.z > 38.5f && transform.position.x > 2f)
        {
            CancelInvoke("ChangeDirection");
            transform.rotation = Quaternion.Euler(0, 180, 0);
            InvokeRepeating("ChangeDirection", 5f, 5f);
        }

    }

    void Halt()
    {
        move = Random.Range(0, 21) > 6;
    }

    void ChangeDirection()
    {
        var randDirection = Random.Range(0,8);
        transform.rotation = Quaternion.Euler(0, 45 * randDirection, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        CancelInvoke("ChangeDirection");
        transform.rotation = Random.Range(0,2) == 0 ? Quaternion.Inverse(transform.rotation) : Quaternion.Euler(0, transform.rotation.y * 90, 0);
        Debug.Log("Did Hit");
        InvokeRepeating("ChangeDirection", 5f, 5f);
    }
}

