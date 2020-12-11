using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public string[] genders, spouses;

    public Transform playerBody;
    public GameObject infobox;
    public Text villagerName;
    public Text genderText;
    public Text profName;
    public Text persName;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        infobox.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);

        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * Mathf.Infinity);

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            if(hit.transform.gameObject.GetComponent<Human>() != null)
            {
                infobox.gameObject.SetActive(true);
                var fondVill = hit.transform.gameObject.GetComponent<Human>();

                villagerName.text = hit.transform.gameObject.name;
                genderText.text = string.Format("Gender: {0}", genders[fondVill.gender]);
                profName.text = string.Format("Profession: {0}", fondVill.profession);
                persName.text = string.Format("Personality: {0}", fondVill.personality);

            }
            else
                infobox.gameObject.SetActive(false);



    }
}
