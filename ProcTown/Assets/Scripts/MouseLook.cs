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
    public Text villagerName, genderText, profName, persName, family, likes, dislikes;

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
                likes.text = string.Format("Likes: {0}", fondVill.likes);
                dislikes.text = string.Format("Dislikes: {0}", fondVill.dislikes);
                
                if (fondVill.profession != "Child" && fondVill.spouse != null || fondVill.profession == "Child")
                    family.text = fondVill.profession != "Child" ? string.Format("{0}: {1}", spouses[fondVill.gender], fondVill.spouse.name) : string.Format("Parents: {0}/{1}", fondVill.parent1.name, fondVill.parent2.name);
                else
                    family.text = "No spouse";
            }
            else
                infobox.gameObject.SetActive(false);
    }
}
