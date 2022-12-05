using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyrock : MonoBehaviour
{
    public float rotatespeed,speed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("destroySelf");
        rb = GetComponent<Rigidbody2D>();
        Deloader.Instance.AddToTrash(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.down * speed;
        transform.Rotate(0,0,360*Time.deltaTime * rotatespeed);
    }
    IEnumerator destroySelf()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collition)
    {
        if (collition.gameObject.tag == "fire")
        {
            Destroy(gameObject);
        }
    }

}
