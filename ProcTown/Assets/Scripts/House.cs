using System.Collections;
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
        var firstDwell = Instantiate(human, spawnpoints[0].position, spawnpoints[0].rotation);
        switch(buildType)
        {
            case 0:
                firstDwell.GetComponent<Human>().profession = firstDwell.GetComponent<Human>().gender == 1 ? firstDwell.GetComponent<Human>().houseProfM[Random.Range(0, 10)] : firstDwell.GetComponent<Human>().houseProfF[Random.Range(0, 10)];
                break;
            case 1:
                firstDwell.GetComponent<Human>().profession = firstDwell.GetComponent<Human>().gender == 1 ? firstDwell.GetComponent<Human>().storeProfM[Random.Range(0, 10)] : firstDwell.GetComponent<Human>().storeProfF[Random.Range(0, 10)];
                break;
            case 2:
                firstDwell.GetComponent<Human>().profession = firstDwell.GetComponent<Human>().mayorProfs[0];
                break;
        }
    }
}
