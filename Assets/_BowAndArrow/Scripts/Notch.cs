using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Notch : XRSocketInteractor
{
    private Puller puller = null;
    private Arrow currentArrow = null;

    protected override void Awake()
    {
        base.Awake();
        puller = GetComponent<Puller>();
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        // subscribing to the OnSelectExit for puller to release arrow
        puller.onSelectExit.AddListener(TryToReleaseArrow);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        puller.onSelectExit.RemoveListener(TryToReleaseArrow);
    }

    protected override void OnHoverEnter(XRBaseInteractable interactable)
    {
        if(interactable is Arrow arrow && arrow.selectingInteractor is HandInteractor hand)
        {
            // forcing the arrow to decouple from the hand.
            arrow.OnSelectExit(hand);
            // making the hand direct interactor drop the arrow. Socket automatically picks up the arrow.
            hand.ForceDeinteract(arrow);
            // making the puller script to pick up the hand. Event listener auto triggers the notch OnSelect
            puller.ForceInteract(hand);
            // making the hand accept the puller. Completing the coupling of hand and puller.
            hand.ForceInteract(puller);
        }

    }

    protected override void OnSelectEnter(XRBaseInteractable interactable)
    {
        if(interactable)
        {
            base.OnSelectEnter(interactable);
            StoreArrow(interactable);
        }


        // prevent notch from storing other interactable objects
        //Debug.Log("checking if its an arrow");
        //if (!CheckInteractable(interactable))
        //{
        //    interactable.colliders[0].enabled = false;
        //    interactable.interactionLayerMask = 1 << LayerMask.NameToLayer("ignore");
        //    base.OnSelectExit(interactable);
        //    Debug.Log("box is dropped");
        //}

            //sockets are looking to grab interactables. this function is to make sure it stores the arrow

    }

    // check if interactable is arrow using gameobject tag
    bool CheckInteractable(XRBaseInteractable interactable)
    {
        if(interactable.tag == "arrow")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StoreArrow(XRBaseInteractable interactable)
    {
        //checking to see if its an arrow before storing
        if (interactable is Arrow arrow)
            currentArrow = arrow;
    }

    private void TryToReleaseArrow(XRBaseInteractor interactor)
    {
        if (currentArrow)
        {
            ForceDeselect();
            ReleaseArrow();
        }
    }

    private void ForceDeselect()
    {
        // forcing socket to release arrow
        base.OnSelectExit(currentArrow);
        // forcing our hand interactor to release arrow
        currentArrow.OnSelectExit(this);
    }

    //private void ForceSelect(Arrow currentArrow)
    //{
    //    base.OnSelectEnter(currentArrow);
    //    currentArrow.OnSelectEnter(this);
    //}

    private void ReleaseArrow()
    {
        currentArrow.Release(puller.PullAmount);
        currentArrow = null;
    }

    public override XRBaseInteractable.MovementType? selectedInteractableMovementTypeOverride
    {
        get { return XRBaseInteractable.MovementType.Instantaneous; }
    }

    
}
