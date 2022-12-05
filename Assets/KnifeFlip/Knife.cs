using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{   
    public Rigidbody rb;
    private Vector3 start;
    private Vector3 end;
    public float force = 5f;
    public float torque = 20f;
    private float time;
    public Transform StartPos;
    private bool Playing;
    public float TorqueDevider = 500;
    private void Start()
    {
        Playing = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                start = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                end = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Swipe();
            }

            if (Input.GetKeyDown(KeyCode.X))
                KnifeReset();

            GUIController.Instance.OneFrameText(GUIController.Instance.InputsText, "Click and drage with the mouse to flip the Knife | X to reset");
        }
        else
        {
            KnifeReset();
        }

    }
    void Swipe()
    {   
        time = Time.time;
        Vector3 swipe = end - start;
        rb.AddForce(swipe * force, ForceMode.Impulse);
        rb.AddTorque(0f, 0f, torque * swipe.normalized.magnitude/ TorqueDevider, ForceMode.Impulse);
    }
    public void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("table"))
        {
            rb.isKinematic = true;
        }
    }
    

    public void KnifeReset()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        this.transform.position = StartPos.position;
        this.transform.SetPositionAndRotation(StartPos.position, StartPos.rotation);
        rb.isKinematic = false;
    }

    public void StartPlaying()
    {
        Playing = true;
    }

    public void EndPlaying()
    {
        Playing = false;
    }
}
