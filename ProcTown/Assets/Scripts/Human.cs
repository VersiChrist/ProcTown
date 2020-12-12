
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    bool move, talking, canTalk;
    Rigidbody rb;
    public int gender;
    public GameObject[] heads, torsos;
    public string personality, profession, likes, dislikes, quote;
    public GameObject spouse, parent1, parent2, talker, bubble;
    public string[] mafp, masp, fefp, fesp, personalities;
    public string[] houseProfM, houseProfF, storeProfM, storeProfF, mayorProfs, likesDislikes, quotes;

    // Start is called before the first frame update
    void Start()
    {
        var f = Random.Range(0, mafp.Length);
        var s = Random.Range(0, masp.Length);

        gameObject.name = string.Format("{0}{1}", gender == 1 ? mafp[f] : fefp[f], gender == 1 ? masp[s] : fesp[s]);
        personality = personalities[Random.Range(0, personalities.Length)];
        likes = likesDislikes[Random.Range(0, likesDislikes.Length)];
        quote = quotes[Random.Range(0, quotes.Length)];

        for (int i = 0; i < 1; i++)
        {
            var rand = Random.Range(0, likesDislikes.Length);

            if (likesDislikes[rand] == likes)
                i--;
            else
                dislikes = likesDislikes[rand]; 
        }

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
        StartCoroutine(CanTalkNow());
        InvokeRepeating("ChangeDirection", 5f, 5f);
        InvokeRepeating("Halt", 0f, 3f);
    }

    private void FixedUpdate()
    {
        if(move && !talking)
            rb.MovePosition(transform.position + transform.forward * 3 * Time.deltaTime);

        if (talking)
        {
            Vector3 targetDirection = talker.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }

        if (!talking)
        {
            if (transform.position.z < 2f && transform.position.x > 2f)
            {
                Invoking();
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (transform.position.z > 2f && transform.position.x < 2f)
            {
                Invoking();
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }

            if (transform.position.z > 2f && transform.position.x > 38f)
            {
                Invoking();
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }

            if (transform.position.z > 38.5f && transform.position.x > 2f)
            {
                Invoking();
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 3) && !talking)
            if (hit.transform.gameObject.GetComponent<Human>() != null && hit.transform.gameObject.GetComponent<Human>().talker == null && canTalk)
            {
                if (Random.Range(0, 11) > 5)
                {
                    CancelInvoke("ChangeDirection");
                    talking = true;
                    canTalk = false;
                    talker = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<Human>().talker = gameObject;
                    hit.transform.gameObject.GetComponent<Human>().talking = true;
                    hit.transform.gameObject.GetComponent<Human>().canTalk = false;
                    hit.transform.gameObject.GetComponent<Human>().CancelInvoke("ChangeDirection");
                    StartCoroutine(Talking());
                }
            }

        bubble.SetActive(talking);
    }

    void Invoking()
    {
        CancelInvoke("ChangeDirection");
        InvokeRepeating("ChangeDirection", 5f, 5f);
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
        if (!talking)
        {
            Invoking();
            transform.rotation = Random.Range(0, 2) == 0 ? Quaternion.Inverse(transform.rotation) : Quaternion.Euler(0, transform.rotation.y * 90, 0);
        }
    }

    IEnumerator Talking()
    {
        yield return new WaitForSeconds(Random.Range(15 , 31));
        StopTalking();
    } 

    void StopTalking()
    {
        talker.GetComponent<Human>().talking = false;
        talker.GetComponent<Human>().talker = null;
        talker.GetComponent<Human>().StartCoroutine(CanTalkNow());
        talker.transform.gameObject.GetComponent<Human>().InvokeRepeating("ChangeDirection", 5f, 5f);

        talking = false;
        talker = null;
        StartCoroutine(CanTalkNow());
        InvokeRepeating("ChangeDirection", 5f, 5f);
    }    

    IEnumerator CanTalkNow()
    {
        yield return new WaitForSeconds(10);
        canTalk = true;
    }
}

