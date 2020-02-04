using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateWorldWin : MonoBehaviour
{
    private string parentName;
    private string PLANET_ONE_PARENT = "WinGameA";
    private string PLANET_TWO_PARENT = "WinGameB";
    public float speedRotation = 5.0f;


    void Start()
    {
        parentName = transform.parent.name;
    }

void Update()
    {
        if(PLANET_ONE_PARENT == parentName)
        {
            float Xaxsis = KeyJoyController.getXAxis_Player1();
            float Yaxsis = KeyJoyController.getYAxis_player1();
            transform.Rotate(0f, Xaxsis * speedRotation, Yaxsis * speedRotation);
        }

        if (PLANET_TWO_PARENT == parentName)
        {
            float Xaxsis = KeyJoyController.getXAxis_Player2();
            float Yaxsis = KeyJoyController.getYAxis_Player2();
            transform.Rotate(0f, Xaxsis * speedRotation, Yaxsis * speedRotation);
        }
    }



}
