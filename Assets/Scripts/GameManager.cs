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


    void Start()
    {
        state = State.Start;
        
        display = GetComponent<DisplayScript>();
        display.StartDisplay();
        myGui = GetComponent<GameGui>();
        myGui.showStart();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Start:

                PressToStartGame();
                break;

            case State.Game:

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    shouldPaue();

                }

                if (player1.GetComponent<PlayerScript>().playerLife < 0)
                {
                    player1.GetComponent<PlayerScript>().die();
                    state = State.Lose;
                }
               
                distanceBetween = Vector3.Distance(player1.transform.position, player2.transform.position);
                //Debug.Log(distanceBetween);
                if (distanceBetween < 7)
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
                //myGui.lose();
                player1.GetComponent<PlayerScript>().pressToPlayAgain();
                player2.GetComponent<PlayerScript>().pressToPlayAgain();
                clearPieces();
                player1.GetComponent<PlayerScript>().restart();
                myGui.showStart();

                state = State.Start;
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
        myGui.guiSetUp();
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

        //myGui.resume();
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


}
