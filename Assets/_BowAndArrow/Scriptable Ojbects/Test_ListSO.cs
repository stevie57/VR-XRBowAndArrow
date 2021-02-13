using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "test cube list ", menuName = "Testing SO")]
public class Test_ListSO : ScriptableObject
{
    public List<GameObject> cubelist;
    public void createCubeList()
    {
        cubelist = new List<GameObject>();
    }

    


}
