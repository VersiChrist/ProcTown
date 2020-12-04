using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGeneration : MonoBehaviour
{
    public GameObject hall;
    public GameObject well;
    public GameObject[] house;
    public Vector3 hallPosition;
    public Vector3[] positions;

    int randPos;
    Quaternion rotato;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(hall, hallPosition, hall.transform.rotation);
        var numOfHouses = Random.Range(3, 15);
        Vector3[] taken = new Vector3[numOfHouses];

        for (int i = 0; i < numOfHouses; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                randPos = Random.Range(0, 14);

                foreach (Vector3 item in taken)
                {
                    if (positions[randPos] == item)
                        j--;
                }
            }

            var randHouse = Random.Range(0, house.Length);

            switch (randPos > 6 && house[randHouse].GetComponent<House>().sideways)
            {
                case false:
                    rotato = Quaternion.Euler(0, 90, 0);
                    break;
                case true:
                    rotato = Quaternion.Euler(0, -90, 0);
                    break;
            }

            Instantiate(house[randHouse], positions[randPos], rotato);
            taken[i] = positions[randPos];
        }
    }
}
