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
    Quaternion rotato;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(hall, hallPosition, hall.transform.rotation);

        for (int i = 0; i < positions.Length; i++)
        {
            Debug.Log(i);
            var randHouse = Random.Range(0, house.Length);

            switch (i > 6 && house[randHouse].GetComponent<House>().sideways)
            {
                case false:
                    rotato = Quaternion.Euler(0, 90, 0);
                    break;
                case true:
                    rotato = Quaternion.Euler(0, -90, 0);
                    break;
            }

            Instantiate(house[randHouse], positions[i], rotato);
        }
    }
}
