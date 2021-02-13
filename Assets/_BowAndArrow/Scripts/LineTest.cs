using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour
{

    public GameObject topLineStartPos;
    public GameObject bottomLineStartPos;
    public GameObject NotchPos;
    LineRenderer topLine;
    LineRenderer BottomLine;


    // Start is called before the first frame update
    void Start()
    {
        topLine = topLineStartPos.GetComponent<LineRenderer>();
        BottomLine = bottomLineStartPos.GetComponent<LineRenderer>();
        SetTopLine();
    }


    void SetTopLine()
    {
        Vector3 topLineStart = topLineStartPos.transform.position;
        Vector3 topLineEnd = NotchPos.transform.position;

        topLine.SetPosition(0, topLineStart);
        topLine.SetPosition(1, topLineEnd);
    }
    // Update is called once per frame
    void Update()
    {
        SetTopLine();
    }
}
