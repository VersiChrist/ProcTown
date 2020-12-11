using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public float speed = 12f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (transform.position.x >= 100 || transform.position.x <= -100 )
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);

        if (transform.position.z >= 100 || transform.position.z <= -100)
            transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.z);

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (Input.GetKeyDown(KeyCode.T))
            Time.timeScale = Mathf.Abs(Time.timeScale - 1);
    }
}
