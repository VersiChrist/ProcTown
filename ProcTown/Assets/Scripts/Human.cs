
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    bool move;
    Rigidbody rb;
    public int gender;
    public GameObject[] heads, torsos;
    public string personality, profession;
    public GameObject spouse, parent1, parent2;
    public string[] mafp, masp, fefp, fesp, personalities;
    public string[] houseProfM, houseProfF, storeProfM, storeProfF, mayorProfs;

    // Start is called before the first frame update
    void Start()
    {
        var f = Random.Range(0, mafp.Length);
        var s = Random.Range(0, masp.Length);

        gameObject.name = string.Format("{0}{1}", gender == 1 ? mafp[f] : fefp[f], gender == 1 ? masp[s] : fesp[s]);
        personality = personalities[Random.Range(0, personalities.Length)];

        for (int i = 0; i < 1; i++)
        {
            var r = Random.Range(0, heads.Length);

            if (gender == 1 && r % 2 == 1 || gender == 2 && r % 2 == 0)
                i--;
            else
            {
                GameObject tors = Instantiate(torsos[r], new Vector3(transform.localPosition.x, transform.localPosition.y + 0.285f, transform.localPosition.z), transform.rotation);
                tors.transform.parent = gameObject.transform;
            }
        }

        for (int i = 0; i < 1; i++)
        {
            var r = Random.Range(0, heads.Length);

            if (gender == 1 && r % 2 == 1 || gender == 2 && r % 2 == 0)
                i--;
            else
            {
                GameObject head = Instantiate(heads[r], new Vector3(transform.position.x, transform.localPosition.y + 0.725f, transform.position.z), transform.rotation);
                head.transform.parent = gameObject.transform;
            }
        }

        if (profession == "Child")
            transform.localScale = new Vector3(.75f, .75f, .75f);

        HouseGeneration.population++;
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("ChangeDirection", 5f, 5f);
        InvokeRepeating("Halt", 0f, 3f);
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
        InvokeRepeating("ChangeDirection", 5f, 5f);
    }
}

