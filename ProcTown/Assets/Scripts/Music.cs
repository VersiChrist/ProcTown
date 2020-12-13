using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    bool muted;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("BGM");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            muted = !muted;

        AudioListener.volume = muted == false ? 1 : 0;
    }
}
