using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gui : MonoBehaviour
{
    private Camera player1Cam;
    private Camera player2Cam;
    public GameManager GM;



    void Start()
    {
        player1Cam = GameObject.FindGameObjectWithTag("Player1").GetComponentInChildren<Camera>();
        player2Cam = GameObject.FindGameObjectWithTag("Player2").GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
