using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class EnemyTextDisplay : MonoBehaviour
{
    public GameManager gameManager;
    private TMP_Text TextDisplay;
    public SpawnManager spawnManager;
    public EnemyCount EnemyCount;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void InsertText()
    {
        TextDisplay = GetComponent<TextMeshProUGUI>();
        TextDisplay.text = "<u>Wave 1</u><br>Remaining<br>Blue Cubes : " + EnemyCount.EnemyOneCount.ToString() +
            "<br> Red Cubes : " + EnemyCount.EnemyTwoCount.ToString();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
