using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public GameObject Broken, Good;
    public BoxCollider2D WindowCollider;
    public AudioSource windowcrash;
   
   
    void Start()
    {
        Broken.SetActive(false);
        Good.SetActive(true);
        WindowCollider.enabled = true;
        gameObject.layer = 6;
       
    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Broken.SetActive(true);
            Good.SetActive(false);
            windowcrash.Play();
            WindowCollider.enabled = false;
            gameObject.layer = 0;
            AstarPath.active.UpdateGraphs(WindowCollider.bounds);

        }
    }
}
