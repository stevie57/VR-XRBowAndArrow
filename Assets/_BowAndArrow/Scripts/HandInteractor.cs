﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandInteractor : XRDirectInteractor
{

    public void ForceInteract(XRBaseInteractable interactable)
    {
        OnSelectEnter(interactable);
    }

    public void ForceDeinteract(XRBaseInteractable interactable)
    {
        OnSelectExit(interactable);
    }

}
