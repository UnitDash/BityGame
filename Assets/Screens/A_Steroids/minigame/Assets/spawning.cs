using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawning : MonoBehaviour
{
    public GameObject firePrefab,rock1,rock2,rock3;
    public Transform fireSpawnPointright;
    private float timebtwspawning=0;
    public float starttimebtwspawn;
    public Transform spawnpos;
    
    private float ypos,random;
    private AudioSource laser1;
    private bool CanShout;

    // Start is called before the first frame update
    void Start()
    {
        CanShout = true;
        laser1 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (starttimebtwspawn > 0.2f)
            starttimebtwspawn -= Time.deltaTime*0.01f; 
        
       
        random = Random.Range(1 ,4);
        ypos = Random.Range(-2.61569f, 2.61569f);
       if(timebtwspawning <= 0)
        {
            switch (random)
            {
                case 1: Instantiate(rock1, new Vector2(spawnpos.position.x + ypos, spawnpos.position.y), Quaternion.identity); break;
                case 2: Instantiate(rock2, new Vector2(spawnpos.position.x + ypos, spawnpos.position.y), Quaternion.identity); break;
                case 3: Instantiate(rock3, new Vector2(spawnpos.position.x + ypos, spawnpos.position.y), Quaternion.identity); break;
        
            }
    
           

            timebtwspawning = starttimebtwspawn;
        }
       else
        {
            timebtwspawning -= Time.deltaTime;
        }

        if (Input.GetKeyDown("space")&&CanShout)
        {

            laser1.Play();
            GameObject fire = Instantiate(firePrefab, fireSpawnPointright.position, Quaternion.identity);
            CanShout = false;
            StartCoroutine(Shoutdelay());

        }
    }
   

    private IEnumerator Shoutdelay()
    {
        yield return new WaitForSeconds(0.3f);
        CanShout = true;
    }

}
