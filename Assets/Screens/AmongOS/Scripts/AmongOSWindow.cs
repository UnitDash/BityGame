using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmongOSWindow : MonoBehaviour
{
    [Header("Window")]
    public BoxCollider2D DragZone;
    [HideInInspector]public Vector2 MousePositionDelta;

    [HideInInspector]public bool Dragging = false;
    private void LateUpdate() {
        RaycastHit2D Raycaster = AmongOS.ScreenRayCaster();

        if(Input.GetKeyDown(KeyCode.Mouse0) && Raycaster.transform != null && Raycaster.transform == DragZone.transform)
            Dragging = true;
        if(Input.GetKeyUp(KeyCode.Mouse0)|| Raycaster.transform == null)
            Dragging = false;
        StartCoroutine(CalculateMousePositionDelta(Raycaster.point));

        if (Dragging)
            this.transform.localPosition = this.transform.localPosition + (Vector3)MousePositionDelta;
            
        this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y,-0.1f);
    }

    IEnumerator CalculateMousePositionDelta(Vector2 PrevInput){

        yield return 0;
        MousePositionDelta =  AmongOS.ScreenRayCaster().point - PrevInput;
    }
}
