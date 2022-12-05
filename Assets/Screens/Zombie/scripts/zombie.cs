using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class zombie : MonoBehaviour
{
     private AIDestinationSetter destinationSetter;
    public Transform player;
    private float Score;
    private AIPath AIPath;
    public AudioSource zombie1,zombie2,zombie3,zombie4,dying;
    public GameObject dead, alive;
    private int random;
   



    private void Awake()
    {
        random = Random.Range(1, 7);
        switch (random)
        {
            case 1:zombie1.Play(); break;
            case 2: zombie2.Play(); break;
            case 3: zombie3.Play(); break;
            case 4: zombie4.Play(); break;
                   
        }
    }
    private void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        destinationSetter.target = FindObjectOfType<charactermovment>().transform;
        AIPath = GetComponent<AIPath>();
        random = Random.Range(1,50);
        
        if (random == 2)
            AIPath.maxSpeed = 15f;





    }
    private void Update()
    {
        
        Score = destinationSetter.target.GetComponent<HealthManager>().score;
        if (random != 2)
        {

        if (Score < 1000)
            AIPath.maxSpeed = 2f;
        else if (Score < 2000)
            AIPath.maxSpeed = 3f;
        else if (Score < 3000)
            AIPath.maxSpeed = 4f;
        else if (Score < 4000)
            AIPath.maxSpeed = 6f;
        else
            AIPath.maxSpeed = 8f;

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            destinationSetter.target.GetComponent<HealthManager>().score += 20;
            die();
            
        }

    }
   private void die()
    {
        dead.gameObject.SetActive(true);
        alive.gameObject.SetActive(false);
        dying.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        destinationSetter.enabled = false;
        GetComponent<AIPath>().enabled = false;

        Destroy(gameObject,2);
    }

}
