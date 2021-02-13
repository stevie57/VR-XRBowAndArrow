using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DispenseCube : XRSocketInteractor
{
    public GameObject CubePrefab;

    protected override void Awake()
    {
        base.Awake();
        CreateAndSelectCube();
    }

    void CreateAndSelectCube()
    {
        CubeGrab cube = CreateCube();
        SelectCube(cube);
    }
    
    private CubeGrab CreateCube()
    {
        GameObject cubeObject = Instantiate(CubePrefab, transform.position, transform.rotation);
        return cubeObject.GetComponent<CubeGrab>();
    }

    void SelectCube(CubeGrab cube)
    {
        OnSelectEnter(cube);
        cube.OnSelectEnter(this);
    }

    protected override void OnSelectExit(XRBaseInteractable interactable)
    {
        base.OnSelectExit(interactable);
        CreateAndSelectCube();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
