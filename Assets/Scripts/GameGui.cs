using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGui : MonoBehaviour
{  

    public GameObject Win1;
    public GameObject Win2;
    //private GameObject Lose;
    public GameObject StartMenu1;
    public GameObject StartMenu2;
    



    public void showStart()
    {
        StartMenu1.SetActive(true);
        StartMenu2.SetActive(true);
    }

    public void GamePlayCloseGui()
        // this function should be called once we start the game 
    {
        StartMenu1.SetActive(false);
        StartMenu2.SetActive(false);
        Win1.SetActive(false);
        Win2.SetActive(false);
        //Lose.SetActive(false);
    }

    public void win()
    {
        Win1.SetActive(true);
        Win2.SetActive(true);
        StartMenu1.SetActive(false);
        StartMenu2.SetActive(false);
        Debug.Log("win");
    }

    public Transform getWinOne()
    {
        return Win1.transform.Find("StatsContainer");
    }

    public Transform getWinTwo()
    {
        return Win2.transform.Find("StatsContainer");
    }

    public Transform getWinningTitleOne()
    {
        return Win1.transform.Find("WinTitle");
    }

    public Transform getWinningTitleTwo()
    {
        return Win2.transform.Find("WinTitle");
    }

    // public void lose()
    // {
    //     Lose.SetActive(true);
    //     Debug.Log("Lose");
    // }
}
