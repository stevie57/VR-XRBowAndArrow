using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_cubeListManager : MonoBehaviour
{
    public Test_ListSO test_listSO;
    public int AmountToPoolCube = 5;
    public GameObject cubePrefab;
    List<GameObject> cubelist;

    private void Start()
    {
        SpawnCubes();
    }

    private void SpawnCubes()
    {
        test_listSO.createCubeList();
        cubelist = test_listSO.cubelist;
        GameObject tmp;
        for (int i = 0; i < AmountToPoolCube; i++)
        {
            tmp = Instantiate(cubePrefab);
            tmp.SetActive(false);
            cubelist.Add(tmp);
            Debug.Log("cube added");
        }
    }


    private GameObject GetPooledCubes()
    {
        for (int i = 0; i < AmountToPoolCube; i++)
        {
            if (!cubelist[i].activeInHierarchy)
            {
                Debug.Log("Running " + i);
                return cubelist[i];
            }       
        }
        return null;
    }

    private GameObject WithdrawOneCube()
    {
        int i = 0;
        if(cubelist[i] != null)
        {
            return cubelist[i];
        }
        else
        {
            i++;
        }
        return null;
    }

    public void StandardGetCubes()
    {
        Debug.Log("Standard button is pressed");
        GameObject cube = GetPooledCubes();
        cube.SetActive(true);
        Debug.Log("All standard buttons implemented");
    }

    public void GetOneCube()
    {
        GameObject cube = WithdrawOneCube();
        cube.SetActive(true);
    }

    
}
