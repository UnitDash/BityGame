using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float waitTime;
    [SerializeField] float sizeUpgrade;
    [SerializeField] Rigidbody2D rb;
    
    float x,y;

    private void Start()
    {
        StartCoroutine("Moving");
    }

    void Update()
    {
        transform.localScale = new Vector2(transform.localScale.x + (Time.deltaTime / 10), transform.localScale.y + (Time.deltaTime / 10));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        } else 
        {
            transform.localScale = new Vector2(transform.localScale.x + sizeUpgrade, transform.localScale.y + sizeUpgrade);
        }
    }

    IEnumerator Moving()
    {
        while(true)
        {
            x = Mathf.Round(Random.Range(speed, -speed));
            y = Mathf.Round(Random.Range(speed, -speed));
            if(x == 0 && y == 0)
            {
                x = 1;
                y = 1;
            }
            rb.velocity = new Vector2(x,y);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
