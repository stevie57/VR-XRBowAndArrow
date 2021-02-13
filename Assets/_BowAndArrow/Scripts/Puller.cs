using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// this script is sitting on the notch
public class Puller : XRBaseInteractable
{
    public float PullAmount { get; private set; } = 0.0f;

    public Transform start = null;
    public Transform end = null;

    //energy line effect
    public GameObject bowString;
    public GameObject topLineStartPos;
    public GameObject bottomLineStartPos;
    public GameObject NotchPos;
    LineRenderer topLine;
    LineRenderer BottomLine;
    bool showEnergyBowString = false;


    public XRBaseInteractor pullingInteractor = null;


    public void ForceInteract(XRBaseInteractor interactor)
    {
        OnSelectEnter(interactor);
    }

    public void PullerOnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
    }

    //protected override void OnHoverEnter(XRBaseInteractor interactor)
    //{
    //    base.OnHoverEnter(interactor);
    //    pullingInteractor = interactor;
    //}

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
        pullingInteractor = interactor;

        //--Energy Bow String effect --
        bowString.SetActive(false);
        topLineStartPos.SetActive(true);
        bottomLineStartPos.SetActive(true);
        topLine = topLineStartPos.GetComponent<LineRenderer>();
        topLine.startWidth = 0.01f; topLine.endWidth = 0.01f;
        BottomLine = bottomLineStartPos.GetComponent<LineRenderer>();
        BottomLine.startWidth = 0.01f; BottomLine.endWidth = 0.01f;
        showEnergyBowString = true;
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);
        pullingInteractor = null;
        PullAmount = 0.0f;

        //bow string effects disabled
        bowString.SetActive(true);
        showEnergyBowString = false;
        topLineStartPos.SetActive(false);
        bottomLineStartPos.SetActive(false);

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Vector3 pullPosition = pullingInteractor.transform.position;
                PullAmount = CalculatePull(pullPosition);

                if (showEnergyBowString)
                {
                    SetTopLine();
                    SetBottomline();
                }
            }
        }
    }

    void SetTopLine()
    {
        Vector3 topLineStart = topLine.transform.position;
        Vector3 topLineEnd = pullingInteractor.transform.position;

        topLine.SetPosition(0, topLineStart);
        topLine.SetPosition(1, topLineEnd);
    }


    private void SetBottomline()
    {
        Vector3 BottomLineStart = BottomLine.transform.position;
        Vector3 BottomLineEnd = pullingInteractor.transform.position;

        BottomLine.SetPosition(0, BottomLineStart);
        BottomLine.SetPosition(1, BottomLineEnd);
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection);

        return Mathf.Clamp(pullValue, 0, 1);
    }


}
