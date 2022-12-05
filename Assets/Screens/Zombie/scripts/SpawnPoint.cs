using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    /*[HideInInspector]*/ public bool InScreen,free=false;
    public Transform Handler;
    public Vector2 CameraRange;
    public DropsSpawnerManager manager;
    public enum spawnertype
    {
        med1,med2,rapidfire,speed,zombie
    }
    public spawnertype spawner;



    void Update()
    {
        if (transform.position.x > Handler.position.x-CameraRange.x && transform.position.x<= Handler.position.x+CameraRange.x&& transform.position.y > Handler.position.y - CameraRange.y && transform.position.y <= Handler.position.y + CameraRange.y)
        {
            InScreen = true;
        }
        else
        {
            InScreen=false;
        }
        
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spawner != spawnertype.zombie)
        {
            
        if (collision.gameObject.CompareTag("Player"))
        {
                
            if (spawner == spawnertype.med1)
                manager.free1 = true;
            if (spawner == spawnertype.med2)
                manager.free2 = true;
            if (spawner == spawnertype.rapidfire)
                manager.free3 = true;
            if (spawner == spawnertype.speed)
                manager.free4 = true;

        }
        }



    }
  

    }

