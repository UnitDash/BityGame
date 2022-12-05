using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private AudioSource lasersound;
    // Start is called before the first frame update
    void Start()
    {
        lasersound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixUpdate()
    {   Debug.Log("it");
        if (Input.GetKeyDown("space"))
        {
            lasersound.Play();
        }
    }
}
