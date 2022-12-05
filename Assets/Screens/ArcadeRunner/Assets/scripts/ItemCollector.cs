using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int Coins = 0;

    public GameObject player;


    private Vector3 checkPointPosition;
    [SerializeField] private TextMeshPro CoinsText;

    [SerializeField] private AudioSource collectionSoundEffect;

    void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (collision.gameObject.CompareTag("Coins"))
        {
            setNewCheckPoint(newCheckPoint: collision.gameObject.transform.position);
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            Coins++;
            CoinsText.text = "Coins: " + Coins;
        }
    }

     void setNewCheckPoint(Vector3 newCheckPoint){
        checkPointPosition=newCheckPoint;
    }
     public void resetPlayerCheckPoint(){
        player.transform.position=checkPointPosition;
    }
}
