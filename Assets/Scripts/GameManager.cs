using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public enum State
    {
        Start,
        Game,
        Win,
        Lose,
    }

    public Instantiating init;
    private State state;
    private static bool IsPaused = false;
    public GameObject player1;
    public GameObject player2;
    private float distanceBetween;
    private GameGui myGui;
    private DisplayScript display;

    //all things to do when we are dead
    private bool PlayerOneDead = false;
    private bool PlayerTwoDead = false;
    private float disease = 1f;
    public float diseaseTime = 10; // how often to remove one life
    private float TimeToDo = 0f;


    void Start()
    {
        state = State.Start;
        display = GetComponent<DisplayScript>();
        display.StartDisplay();
        myGui = GetComponent<GameGui>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Start:

                // this function shows the start menue:
                myGui.showStart();
                // when pressed will load the actual game 
                PressToStartGame();
                break;

            case State.Game:

                if (Input.GetKeyDown(KeyCode.P))
                {
                    shouldPaue();

                }

                if (player1.GetComponent<PlayerScript>().playerLife < 0)
                {
                    if (!PlayerOneDead)
                    {
                        TimeToDo = Time.time;
                    }
                    player1.GetComponent<PlayerScript>().die();
                    PlayerOneDead = true;
                    if (Time.time - TimeToDo >= diseaseTime)
                    {
                        player2.GetComponent<PlayerScript>().loseLife(disease);
                    }

                }

                if (player2.GetComponent<PlayerScript>().playerLife < 0)
                {
                    player2.GetComponent<PlayerScript>().die();
                    PlayerTwoDead = true;
                    player1.GetComponent<PlayerScript>().loseLife(disease);


                }
                if (PlayerOneDead && PlayerTwoDead)
                {
                    state = State.Lose;
                }

                distanceBetween = Vector3.Distance(player1.transform.position, player2.transform.position);
                //Debug.Log(distanceBetween);

                if (distanceBetween < 3f)
                {
                    state = State.Win;
                }
                break;

            case State.Win:
                myGui.win();
                clearPieces();
                player1.GetComponent<PlayerScript>().restart();
                myGui.showStart();
                state = State.Start;
                break;

            case State.Lose:
                // playes the animation for the press to restart
                player1.GetComponent<PlayerScript>().pressToPlayAgain();
                player2.GetComponent<PlayerScript>().pressToPlayAgain();
                // todo change the M botton
                if(Input.GetKeyDown(KeyCode.M) && Input.GetKeyDown(KeyCode.Space))
                {
                    clearPieces();
                    player1.GetComponent<PlayerScript>().restart();
                    player2.GetComponent<PlayerScript>().restart();
                    state = State.Start;

                }

                break;

        }
    }


    public void PressToStartGame()
    {
        if (Input.anyKey)
        {
            init.loadPlayers();
            StartCoroutine(FindPlayersAndStartGame());

        }
    }

    private IEnumerator FindPlayersAndStartGame()
    {
        while (player1 == null || player2 == null)
        {
            player1 = GameObject.FindGameObjectWithTag("Player1");
            player2 = GameObject.FindGameObjectWithTag("Player2");
            yield return new WaitForSeconds(0.01f);
        }
        state = State.Game;
        myGui.GamePlayCloseGui();
    }

    void shouldPaue()
    {
        if (IsPaused)
        {
            Resume();
        }

        else
        {
            Pause();
        }
    }

    void Resume()
    {

        Time.timeScale = 1f;
        IsPaused = false;
    }

    void Pause()
    {
        //myGui.pause();
        Time.timeScale = 0.0f;
        IsPaused = true;

    }

    public State getState()
    {
        return state;
    }

    public void clearPieces()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Piece");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
    
    public bool getIsPlayer1Dead()
    {
        return PlayerOneDead;
    }

    public bool getIsPlayer2Dead()
    {
        return PlayerTwoDead;
    }
  
}
