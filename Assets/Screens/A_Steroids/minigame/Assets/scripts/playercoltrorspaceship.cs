using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercoltrorspaceship : MonoBehaviour
{
    public float mouvementspeed;
    private Rigidbody2D rb;
    private float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = Vector2.Lerp(rb.velocity,new Vector2(horizontal * mouvementspeed, rb.velocity.y*Time.deltaTime),5*Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D collition)
    {
        if (collition.gameObject.tag == "rocks")
        {
            SceneManager.UnloadSceneAsync("A_Steroids");
            SceneManager.LoadSceneAsync("A_Steroids", LoadSceneMode.Additive); 
        }
    }
}
