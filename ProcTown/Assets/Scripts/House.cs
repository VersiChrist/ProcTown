﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public bool sideways;
    public GameObject human;
    public int buildType;
    public Transform[] spawnpoints; 

    // Start is called before the first frame update
    void Start()
    {
        var numOfDwell = Random.Range(0, 3);

        var firstDwell = Instantiate(human, spawnpoints[0].position, spawnpoints[0].rotation);
        GameObject secDwell = numOfDwell > 0 ? Instantiate(human, spawnpoints[1].position, spawnpoints[1].rotation) : null;
        GameObject thirdDwell = numOfDwell > 1 ? Instantiate(human, spawnpoints[2].position, spawnpoints[2].rotation) : null;

        firstDwell.GetComponent<Human>().gender = Random.Range(1, 3);

        if (secDwell != null)
        {
            secDwell.GetComponent<Human>().gender = firstDwell.GetComponent<Human>().gender == 1 ? 2 : 1;
            firstDwell.GetComponent<Human>().spouse = secDwell;
            secDwell.GetComponent<Human>().spouse = firstDwell;
        }

        if (thirdDwell != null)
        {
            thirdDwell.GetComponent<Human>().GetComponent<Human>().profession = "Child";
            thirdDwell.GetComponent<Human>().gender = Random.Range(1, 3);
            thirdDwell.GetComponent<Human>().parent1 = firstDwell;
            thirdDwell.GetComponent<Human>().parent2 = secDwell;
        }


        switch (buildType)
        {
            case 0:
                firstDwell.GetComponent<Human>().profession = firstDwell.GetComponent<Human>().gender == 1 ? firstDwell.GetComponent<Human>().houseProfM[Random.Range(0, 10)] : firstDwell.GetComponent<Human>().houseProfF[Random.Range(0, 10)];
                
                if(secDwell != null)
                    secDwell.GetComponent<Human>().profession = secDwell.GetComponent<Human>().gender == 1 ? secDwell.GetComponent<Human>().houseProfM[Random.Range(0, 10)] : secDwell.GetComponent<Human>().houseProfF[Random.Range(0, 10)];
                break;
            case 1:
                firstDwell.GetComponent<Human>().profession = firstDwell.GetComponent<Human>().gender == 1 ? firstDwell.GetComponent<Human>().storeProfM[Random.Range(0, 10)] : firstDwell.GetComponent<Human>().storeProfF[Random.Range(0, 10)];
                
                if (secDwell != null)
                    secDwell.GetComponent<Human>().profession = secDwell.GetComponent<Human>().gender == 1 ? secDwell.GetComponent<Human>().storeProfM[Random.Range(0, 10)] : secDwell.GetComponent<Human>().storeProfF[Random.Range(0, 10)];
                break;
            case 2:
                firstDwell.GetComponent<Human>().profession = firstDwell.GetComponent<Human>().mayorProfs[0];
                
                if (secDwell != null)
                    secDwell.GetComponent<Human>().profession = secDwell.GetComponent<Human>().mayorProfs[secDwell.GetComponent<Human>().gender];
                break;
        }
    }
}
