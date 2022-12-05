using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class charactermovment : MonoBehaviour
{
    private Rigidbody2D Rb;
    private Vector2 velocity,mousepos;
    public float Speed,shoutdelay;
    public Transform Shoulder,Firespot;
    private float Angle;
    public GameObject Bulletprefab;
    private bool Canshoot;
    public bool autofire;
    public AudioSource gunshout;
    public Camera ScreenCamera, CasterCamera;
    public Vector2 LastMousePos;
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Canshoot = true;
        autofire = false;
    }

    
    void Update()
    {
        if(ScreenRay().transform.gameObject.CompareTag("Screen"))
        {
            LastMousePos = ScreenRayCaster().point;
        }

        if (GetComponent<HealthManager>().CurrentHP > 0)
        {

            velocity.x = Input.GetAxisRaw("Horizontal");
            velocity.y = Input.GetAxisRaw("Vertical");
            mousepos = LastMousePos;
           /* mousepos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            mousepos.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;*/
            Angle = Mathf.Rad2Deg * Mathf.Atan2(mousepos.y - Shoulder.position.y, mousepos.x - Shoulder.position.x);

            if (Input.GetButtonDown("Fire1") && Canshoot && !autofire)
            {
                Canshoot = false;
                fire();
                StartCoroutine(shootdelay(shoutdelay));
            }
            if (Input.GetButton("Fire1") && Canshoot && autofire)
            {
                Canshoot = false;
                fire();
                StartCoroutine(shootdelay(0.15f));
            }



            Rb.velocity = velocity * Speed;
            transform.rotation = Quaternion.Euler(0, 0, Angle);

        }
        else
        {
            Rb.velocity = Vector2.zero;
        }

    }
   

    private void fire()
    {
        Instantiate(Bulletprefab, Firespot.position, Firespot.rotation);
        gunshout.Play();

    }
    private IEnumerator shootdelay(float t)
    {
        yield return new WaitForSeconds(t);
        Canshoot = true;
    }
    public RaycastHit2D ScreenRayCaster()
    {
        RaycastHit Screenhit;
        Ray ScreenRay = CameraController.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D CastingHit = new RaycastHit2D();
        if (Physics.Raycast(ScreenRay, out Screenhit))
        {
            print(Screenhit.transform);
            Vector3 CasterMax = new Vector3(CasterCamera.transform.position.x + CasterCamera.orthographicSize * CasterCamera.aspect, CasterCamera.transform.position.y + CasterCamera.orthographicSize, CasterCamera.transform.position.z);
            Vector3 CasterMin = new Vector3(CasterCamera.transform.position.x - CasterCamera.orthographicSize * CasterCamera.aspect, CasterCamera.transform.position.y - CasterCamera.orthographicSize, CasterCamera.transform.position.z);
            Vector3 CasterPosition = new Vector3(Mathf.Lerp(CasterMin.x, CasterMax.x, Screenhit.textureCoord.x), Mathf.Lerp(CasterMin.y, CasterMax.y, Screenhit.textureCoord.y), CasterCamera.transform.position.z);
            CastingHit = Physics2D.Raycast(CasterPosition, Vector3.forward);

        }
        return CastingHit;
    }

    public RaycastHit ScreenRay()
    {
        RaycastHit Screenhit;
        Ray ScreenRay = CameraController.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ScreenRay, out Screenhit);
        return Screenhit;
    }
}
