using UnityEngine;
using UnityEngine.Events;
using System.Collections;
public class AmongOSButton : MonoBehaviour
{

    [Header("Button")]
    public Sprite NotClickedButton;
    public Sprite ClickedButton;
    public Transform ChangePositionTarget;
    public Vector3 ChangePositionOffset = new Vector3(0.03f,-0.02f,0);

    private SpriteRenderer ButtonRanderer;

    [Header("ClickAction")]
    public bool DestroyObj;
    public GameObject DestroyedObject;

    void Start()
    {
        ButtonRanderer = GetComponent<SpriteRenderer>();
    }

    public bool IsClicked = false;
    public void Clicked(){
        if(!IsClicked)
            ChangePositionTarget.position = ChangePositionTarget.position + ChangePositionOffset;

        IsClicked = true;
        ButtonRanderer.sprite = ClickedButton;
    }
    public void NotClicked(){
        if(IsClicked)
            ChangePositionTarget.position = ChangePositionTarget.position - ChangePositionOffset;
            
        IsClicked = false;
        ButtonRanderer.sprite = NotClickedButton;
    }

    public void OnClickEnd(){
        if(IsClicked)
            ChangePositionTarget.position = ChangePositionTarget.position - ChangePositionOffset;
            
        IsClicked = false;
        ButtonRanderer.sprite = NotClickedButton;

        //Click Action
        if(DestroyObj)
            Destroy(DestroyedObject);


        //Fix Cant SelectAffter Destroy

    }
}
