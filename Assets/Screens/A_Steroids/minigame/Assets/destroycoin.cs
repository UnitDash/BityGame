using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroycoin : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine("destroySelf");
        rb = GetComponent<Rigidbody2D>();
        Deloader.Instance.AddToTrash(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.down * speed;
    }
    IEnumerator destroySelf()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
