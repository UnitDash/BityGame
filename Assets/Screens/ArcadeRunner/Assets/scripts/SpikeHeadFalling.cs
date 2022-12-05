using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadFalling : MonoBehaviour
{
    public float fallingSpeed=5.0f;
    public float resetSpeed=2.0f;
    public float waitingTime=2.0f;
    public Animator spikeAnim;
    bool onGround;


     [SerializeField] private Transform ceilling;
     [SerializeField] private Transform ground;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){ 
        
        if(transform.position == ground.position)
        {
            
            StartCoroutine("wait");

            
         

        } else if(transform.position == ceilling.position)
        {
            onGround = false;
        }

        if(!onGround)
        {
            transform.position = Vector2.MoveTowards(transform.position,ground.position,Time.deltaTime *fallingSpeed);
        }else
        {
            
            transform.position = Vector2.MoveTowards(transform.position,ceilling.position,Time.deltaTime* resetSpeed);             
        }
              
    }

    IEnumerator wait()
    {
        spikeAnim.SetBool("tophit", true);
       
       

        yield return new WaitForSeconds(waitingTime);
                
        onGround = true;
       
    }   
    private void LateUpdate() {
    
            spikeAnim.SetBool("tophit", false);
}

}
