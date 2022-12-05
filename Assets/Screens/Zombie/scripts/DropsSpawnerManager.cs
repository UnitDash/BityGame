using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DropsSpawnerManager : MonoBehaviour
{
    public SpawnPoint Spawnpoint_med, Spawnpoint_med2,SpawnPoint_rapidfire,SpawnPoint_speedboost;
    public bool CanSpawnmed_1, CanSpawnmed_2, CanSpawnrapidfire, CanSpawnspeedboost;
    public GameObject rapidfireprefab,medkitprefab;
    public GameObject Player;
    public bool free1, free2, free3,free4;


    void Start()
    {
        CanSpawnmed_1= CanSpawnmed_2= CanSpawnrapidfire= CanSpawnspeedboost = true;
        free1= free2= free3=free4=true;

    }

    
    void Update()
    {
        
        if (CanSpawnmed_1&&free1&& !Spawnpoint_med.InScreen)
        {
            spawn(Spawnpoint_med,medkitprefab);
            CanSpawnmed_1 = false;free1 = false;
          StartCoroutine(Ispawndelay(8, 1));
        }

        if (CanSpawnmed_2 && free2 && !Spawnpoint_med2.InScreen)
        {
            spawn(Spawnpoint_med2,medkitprefab);
            CanSpawnmed_2 = false;free2=false;
            StartCoroutine(Ispawndelay(8, 2));
        }
        if (CanSpawnrapidfire && free3 && !SpawnPoint_rapidfire.InScreen)
        {
            spawn(SpawnPoint_rapidfire, rapidfireprefab);
            CanSpawnrapidfire = false;free3=false;

             
                StartCoroutine(Ispawndelay(8, 3));
            

        }







    }

  
    private void spawn(SpawnPoint a,GameObject dropetype)
    {
       
        GameObject Drop = Instantiate(dropetype, a.transform.position, a.transform.rotation);
        Deloader.Instance.AddToTrash(Drop);
    }
    private IEnumerator Ispawndelay(float t, int a)
    {
        yield return new WaitForSeconds(t);
        switch (a)
        {
            case 1:
                CanSpawnmed_1=true;break;
            case 2:
                CanSpawnmed_2=true;break;
            case 3:
                CanSpawnrapidfire=true;break;   

        }

    }
}
