using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public GameObject Heart1, Heart2, Heart3, EmpteyHeart1, EmpteyHeart2, EmpteyHeart3;
    [HideInInspector] public  int CurrentHP, MaxHP = 3;
    [HideInInspector]public  float score;
    public TextMeshPro scoretext, deadtext;
    public GameObject alive, dead;
    public AudioSource noooo,bite,gettingeated;
    void Start()
    {
        CurrentHP = MaxHP;
        Heart1.gameObject.SetActive(true);
        Heart2.gameObject.SetActive(true);
        Heart3.gameObject.SetActive(true);
        EmpteyHeart1.gameObject.SetActive(false);
        EmpteyHeart2.gameObject.SetActive(false);
        EmpteyHeart3.gameObject.SetActive(false);
        score = 0;
        scoretext.text = score.ToString();
        
        
    }

    
    void Update()
    {
        if (CurrentHP > 0)
        {


        score += Time.deltaTime*0.5f;
        deadtext.text = "YOU DIED\nScore : " + ((int)score).ToString() + "\nPress X to restart";
        
        scoretext.text = ((int)score).ToString();

        
        
            
        


        if (CurrentHP == 1)
        {
            Heart1.gameObject.SetActive(true);
            Heart2.gameObject.SetActive(false);
            Heart3.gameObject.SetActive(false);
            EmpteyHeart1.gameObject.SetActive(false);
            EmpteyHeart2.gameObject.SetActive(true);
            EmpteyHeart3.gameObject.SetActive(true);
        }
        if (CurrentHP == 2)
        {
            Heart1.gameObject.SetActive(true);
            Heart2.gameObject.SetActive(true);
            Heart3.gameObject.SetActive(false);
            EmpteyHeart1.gameObject.SetActive(false);
            EmpteyHeart2.gameObject.SetActive(false);
            EmpteyHeart3.gameObject.SetActive(true);
        }
        if (CurrentHP == 3)
        {
            Heart1.gameObject.SetActive(true);
            Heart2.gameObject.SetActive(true);
            Heart3.gameObject.SetActive(true);
            EmpteyHeart1.gameObject.SetActive(false);
            EmpteyHeart2.gameObject.SetActive(false);
            EmpteyHeart3.gameObject.SetActive(false);
        }

        }
        if (CurrentHP <= 0 && Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("Zombied");
            SceneManager.LoadSceneAsync("Zombied", LoadSceneMode.Additive);
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
            damege();
    }
    private void damege()
    {
        bite.Play();
        if (CurrentHP > 1)
            CurrentHP -= 1;
        else
        {

            CurrentHP = 0;
            Heart1.gameObject.SetActive(false);
            Heart2.gameObject.SetActive(false);
            Heart3.gameObject.SetActive(false);
            EmpteyHeart1.gameObject.SetActive(true);
            EmpteyHeart2.gameObject.SetActive(true);
            EmpteyHeart3.gameObject.SetActive(true);
            Die();
        }


    }
    private void Die()
    {
        noooo.Play();gettingeated.Play();
        alive.gameObject.SetActive(false);
        dead.gameObject.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;
        deadtext.gameObject.SetActive(true);
        Time.timeScale = 1.8f;
        
       
    }
}
