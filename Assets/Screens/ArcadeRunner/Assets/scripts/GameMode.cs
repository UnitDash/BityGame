using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    bool canEnter;
    GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }
    private void OnMouseOver() {
        
    }
    void OnTriggerStay2D(Collider2D other) {
        coin = GameObject.FindWithTag("Coins");
        if(coin == null){
            canEnter=true;
        } 
      

        if(canEnter)
        {
            if(Input.GetKey("e")){           
        // Hna Logic bch tkiti l pc
            }    
        }
    }
}

