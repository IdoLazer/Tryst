using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGui : MonoBehaviour
{  
    private Camera player1Cam;
    private Camera player2Cam;

    private float startTime;
    private float pulseTime;
    private float TrailTime;

    public GameObject Win;
    public GameObject Lose;

    public GameObject VCOpening;

    public void showStart()
    {
        VCOpening.SetActive(true);
       

    }

    public void guiSetUp()
        // this function should be called once we start the game 
    {

        VCOpening.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);



        //Text text1 = GameObject.FindGameObjectWithTag("playerOneWrapper").GetComponentInChildren<Text>();
        //Text text2 = GameObject.FindGameObjectWithTag("playerTwoWrapper").GetComponentInChildren<Text>();

        //text1.text = "What are you looking for?";
        //text2.text = "What are you looking for?";

    }

    public void win()
    {
        Win.SetActive(true);
        Debug.Log("win");
    }

    public void lose()
    {
        Lose.SetActive(true);
        Debug.Log("Lose");
    }
}
