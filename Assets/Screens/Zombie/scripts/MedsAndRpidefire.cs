using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedsAndRpidefire : MonoBehaviour
{
    public enum drops
    {
        med, ammo
    }
    public drops droptype;
    public GameObject Player;
    [HideInInspector] public bool token=false;
    public GameObject sprite;

    private void Start()
    {
        Player = FindObjectOfType<HealthManager>().gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {

            if (droptype == drops.med)
                if (Player.GetComponent<HealthManager>().CurrentHP < Player.GetComponent<HealthManager>().MaxHP)
                    Player.GetComponent<HealthManager>().CurrentHP += 1;
            if (droptype == drops.ammo)
            {
                Player.GetComponent<charactermovment>().autofire = true;
                StartCoroutine(rapidfiretimer());
            }
            token = true;
            sprite.gameObject.SetActive(false); 
            Destroy(gameObject,6);
            



        }
    }
   
        private IEnumerator rapidfiretimer()
        {
            yield return new WaitForSeconds(5f);
            Player.GetComponent<charactermovment>().autofire = false;
        }
   
}


