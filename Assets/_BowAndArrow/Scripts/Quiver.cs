using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class Quiver : XRSocketInteractor
{
    public GameObject arrowPrefab = null;
    Vector3 attachOffset = Vector3.zero;
    List<GameObject> pooledArrows;
    public int amountToPoolArrow = 20 ;

    protected override void Awake()
    {
        base.Awake();
        CreateAndSelectArrow();
        SetAttachOffset();
    }

    protected override void OnSelectExit(XRBaseInteractable interactable)
    {
        base.OnSelectExit(interactable);
        CreateAndSelectArrow();
        SetAttachOffset();
    }

    private void CreateAndSelectArrow()
    {
        //creating a variable of type arrow called arrow. Arrow is assigned and runs the CreateArrow()funciton
        Arrow arrow = CreateArrow();
         // the created arrow is then passed into the selectArrow function.
        SelectArrow(arrow);
    }


    private Arrow CreateArrow()
    {
        // simple instantiate call for the arrow. 
        GameObject arrowObject = Instantiate(arrowPrefab, transform.position - attachOffset, transform.rotation);
        return arrowObject.GetComponent<Arrow>();
    }

    private void SelectArrow(Arrow arrow)
    {
        OnSelectEnter(arrow);
        arrow.OnSelectEnter(this);
    }

    private void SetAttachOffset()
    {
        // select target is taken from XRbaseInteractrable script. 
        // quiver inherits from XRsocketInteractor which inherits from XRBaseInteractor which has the select variable.
        // check if the select target is a grabable object.
        // attach offset = interactable.attachTransform.Localposition.
        if (selectTarget is XRGrabInteractable interactable)
        //attachOffset is preassigned in the prefab. Taking that and storing the information here.
            attachOffset = interactable.attachTransform.localPosition;
    }
}
