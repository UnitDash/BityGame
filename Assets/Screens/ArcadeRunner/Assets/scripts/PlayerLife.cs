using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private ItemCollector itemCollector;





    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        itemCollector = GetComponent<ItemCollector>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            Debug.Log("Player died");
            Die();
        }
        else if(collision.gameObject.CompareTag("Enemy")){
             Destroy(collision.gameObject.transform.parent.gameObject);
            
            
        }
    }

    private void Die()
    {
        StartCoroutine("deathRoutine");
    }

    IEnumerator deathRoutine(){
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        yield return new WaitForSeconds(2.5f);
        RestartLevel();
        yield return new WaitForSeconds(0.5f);
          rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void RestartLevel()
    {
           itemCollector.resetPlayerCheckPoint();
    }
}
