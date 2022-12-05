using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinscript : MonoBehaviour
{
    private float timebtwspawning = 0;
    public float starttimebtwspawn;
    public Transform spawnpos;
    private float ypos;
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        ypos = Random.Range(-2.61569f, 2.61569f);
        if (timebtwspawning <= 0)
        {
            Instantiate(coin, new Vector2(spawnpos.position.x + ypos, spawnpos.position.y), Quaternion.identity);


            timebtwspawning = starttimebtwspawn;
        }
        else
        {
            timebtwspawning -= Time.deltaTime;
        }
    }
}


