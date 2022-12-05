using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class scorescript : MonoBehaviour
{
    private int i=0;
    public GameObject coinsoundobject;
    public TextMeshPro score;
    public GameObject coin;
    private AudioSource coinsound;

    public bool iscolliding = false;
    // Start is called before the first frame update
    void Start()
    {
        coinsound = coinsoundobject.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collition)
    {
       
        if (collition.gameObject.tag == "coin")
        {
            coinsound.Play();
            iscolliding = true;
            i++;
            score.text = i.ToString();
            Destroy(collition.gameObject);


        }
    }
}
