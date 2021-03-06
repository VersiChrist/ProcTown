﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseGeneration : MonoBehaviour
{
    public Text popText;
    public GameObject hall, well; 
    public GameObject wall, halfwall, housefolder, wallfolder;
    public Text titleText;
    public GameObject[] house;
    public Vector3 hallPosition;
    public Vector3[] positions;
    public string[] fpref, secpref, title;

    int randPos;
    Quaternion rotato;
    public static int population;

    // Start is called before the first frame update
    void Start()
    {
        population = 0;
        GameObject tohall = Instantiate(hall, hallPosition, hall.transform.rotation);
        tohall.transform.parent = housefolder.transform;

        GameObject spwell = Instantiate(well, new Vector3(19.5f, 4, 22.5f), well.transform.rotation);
        spwell.transform.parent = housefolder.transform;

        var numOfHouses = Random.Range(4, 15);
        Vector3[] taken = new Vector3[numOfHouses];

        var f = Random.Range(0, fpref.Length);
        var s = Random.Range(0, secpref.Length);
        var t = Random.Range(0, title.Length);

        titleText.text = string.Format("{0}{1}{2}", fpref[f], secpref[s], title[t]);

        for (int i = 0; i < numOfHouses; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                randPos = Random.Range(0, 14);

                foreach (Vector3 item in taken)
                    if (positions[randPos] == item)
                        j--;
            }

            var randHouse = Random.Range(0, house.Length);

            rotato = Quaternion.Euler(0, 90 * (randPos > 6 && house[randHouse].GetComponent<House>().sideways ? -1 : 1), 0);

            GameObject sph = Instantiate(house[randHouse], positions[randPos], rotato);
            sph.transform.parent = housefolder.transform;
            taken[i] = positions[randPos];
        }

        for (int i = 0; i < 40; i++)
        {

            switch (i%2==0)
            {
                case false:
                    GameObject wall0 = Instantiate(wall, new Vector3(i + .5f, 5, .5f), Quaternion.identity);
                    wall0.transform.parent = wallfolder.transform;
                    GameObject wall1 = Instantiate(halfwall, new Vector3(i + .5f, 5, 39.5f), Quaternion.identity);
                    wall1.transform.parent = wallfolder.transform;
                    break;
                case true:
                    wall0 = Instantiate(halfwall, new Vector3(i + .5f, 5, .5f), Quaternion.identity);
                    wall0.transform.parent = wallfolder.transform;
                    wall1 = Instantiate(wall, new Vector3(i + .5f, 5, 39.5f), Quaternion.identity);
                    wall1.transform.parent = wallfolder.transform;
                    break;
            }

            if (i > 0 && i < 39)
            {
                switch (i % 2 == 0)
                {
                    case false:
                        GameObject wall2 = Instantiate(wall, new Vector3(.5f, 5, i + .5f), Quaternion.identity);
                        wall2.transform.parent = wallfolder.transform;
                        wall2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                        
                        GameObject wall3 = Instantiate(halfwall, new Vector3(39.5f, 5, i + .5f), Quaternion.identity);
                        wall3.transform.parent = wallfolder.transform;
                        wall3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                        break;
                    case true:
                        wall2 = Instantiate(halfwall, new Vector3(.5f, 5, i + .5f), Quaternion.identity);
                        wall2.transform.parent = wallfolder.transform;
                        wall2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

                        wall3 = Instantiate(wall, new Vector3(39.5f, 5, i + .5f), Quaternion.identity);
                        wall3.transform.parent = wallfolder.transform;
                        wall3.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                        break;
                }
            }
        }
    }

    private void Update()
    {
        popText.text = string.Format("Population: {0}", population);
    }
}
