using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public bool sideways;
    public GameObject human;
    public Transform[] spawnpoints; 

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(human, spawnpoints[0].position, spawnpoints[0].rotation);
    }
}
