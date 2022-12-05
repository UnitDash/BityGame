using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigguy : MonoBehaviour
{
    public float bigFallingSpeed=5.0f;
    public float bigResetSpeed=2.0f;
    public float bigWaitingTime=2.0f;
    // public Animator spikeAnim;
    public bool onGround;


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
            transform.position = Vector2.MoveTowards(transform.position,ground.position,Time.deltaTime *bigFallingSpeed);
        }else
        {
            
            transform.position = Vector2.MoveTowards(transform.position,ceilling.position,Time.deltaTime* bigResetSpeed);             
        }
              
    }

    IEnumerator wait()
    {
        // spikeAnim.SetBool(name, true);
       
       

        yield return new WaitForSeconds(bigWaitingTime);
                
        onGround = true;
       
    }   
    private void LateUpdate() {
    
            // spikeAnim.SetBool(name, false);
}

}
