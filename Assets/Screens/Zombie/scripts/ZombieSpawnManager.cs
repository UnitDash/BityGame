using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ZombieSpawnManager : MonoBehaviour
{
    public SpawnPoint Spawnpoint_1, Spawnpoint_2, Spawnpoint_3, Spawnpoint_4;
    public   float Score,defficulty;
    
    public bool CanSpawn;
    public int random;
    public GameObject zombieprefab;
    public GameObject Player;


    void Start()
    {
        CanSpawn = true;
    
        
    }

    
    void Update()
    {
        
        Score = Player.GetComponent<HealthManager>().score;
        if (Score < 4000)
        {
        if ((Score / 100) % 10 < 1)
            defficulty = 3f;
        else if ((Score / 100) % 10 < 7)
            defficulty = 1f;
        else if ((Score / 100) % 10 < 10)
            defficulty = 0.5f;
        }else
        {
            defficulty = 0.5f;
        }
        
        random = Random.Range(1, 5);
        switch (random)
        {
            case 1 : spawn(Spawnpoint_1); break;  
            case 2 : spawn(Spawnpoint_2); break;
            case 3 : spawn(Spawnpoint_3); break;
            case 4 : spawn(Spawnpoint_4); break;

        }


    }

    private IEnumerator Ispawndelay(float t)
    {
        yield return new WaitForSeconds(t);
       
        CanSpawn = true;
    }
    private void spawn(SpawnPoint a)
    {
        if (a.InScreen)
            return;
        if(!CanSpawn)
            return;
        GameObject Zombie = Instantiate(zombieprefab, a.transform.position, a.transform.rotation);
        Deloader.Instance.AddToTrash(Zombie);
        CanSpawn=false;
        StartCoroutine(Ispawndelay(defficulty));
    }
}
