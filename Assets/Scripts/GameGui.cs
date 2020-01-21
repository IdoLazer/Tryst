using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGui : MonoBehaviour
{  

    public GameObject Win;
    public GameObject Lose;
    public GameObject StartMenu;
    



    public void showStart()
    {

        StartMenu.SetActive(true);      
    }

    public void GamePlayCloseGui()
        // this function should be called once we start the game 
    {
        StartMenu.SetActive(false);
        Win.SetActive(false);
        Lose.SetActive(false);
    }

    public void win()
    {
        Win.SetActive(true);
        Debug.Log("win");
    }

    // public void lose()
    // {
    //     Lose.SetActive(true);
    //     Debug.Log("Lose");
    // }
}
