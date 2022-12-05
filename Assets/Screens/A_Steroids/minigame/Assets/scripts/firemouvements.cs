using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class firemouvements : MonoBehaviour
{
    public float speed;
    
    private Rigidbody2D rb;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("destroySelf");
        Deloader.Instance.AddToTrash(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.up * speed;
        

    }

    IEnumerator destroySelf() 
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collition)
    {
        if (collition.gameObject.tag == "rocks")
        {
            
           
            Destroy(gameObject);
           
        }

    }
}
