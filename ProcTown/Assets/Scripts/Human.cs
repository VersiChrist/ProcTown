using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    Rigidbody rb;
    Quaternion targetRotation;
    public LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
            Invoke("ChangeDirection", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * 4 * Time.deltaTime);

        if (transform.rotation == targetRotation)
            Invoke("ChangeDirection", 5f);

        /*
        RaycastHit hit;

        if (Physics.Raycast(transform.position + (Vector3.up/2), transform.TransformDirection(Vector3.forward), out hit, 4, layer))
        {
            targetRotation = Quaternion.Euler(0, transform.rotation.y + 90, 0);
            Debug.DrawRay(transform.position + (Vector3.up / 2), transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position + (Vector3.up / 2), transform.TransformDirection(Vector3.forward) * 7, Color.green);
        }
        */
    }

    void ChangeDirection()
    {
        var randDirection = Random.Range(0,8);
        transform.rotation = Quaternion.Euler(0, 45 * randDirection, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.y - 90, 0);
        Debug.Log("Did Hit");
    }
}
