using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public enum State
    {
        Start,
        Game,
        Win,
        Lose,
    }

    public instantiating init;
    private State state;
    private static bool IsPaused = false;
    private GameObject player1;
    private GameObject player2;
    private float distanceBetween;
    
    void Start()
    {
        state = State.Start;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Start:

                //myGui.showStart();
                // this case will be in charge of pulling up the opening scene and when we press start in the scene it will change 
                // the state = State.Game; which will start the game
                state = State.Game;
                StartGame();

                break;

            case State.Game:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    shouldPaue();

                }
                
                if (player1.GetComponent<PlayerScript>().playerLife < 0)
                {

                    state = State.Lose;
                }
               
                distanceBetween = Vector3.Distance(player1.transform.position, player2.transform.position);
                Debug.Log(distanceBetween);
                if (distanceBetween < 7)
                {
                    // myGui.winEffect();   
                    state = State.Win;
                }

                break;
            case State.Win:
                //myGui.winScene();
                Debug.Log("win");
                break;

            case State.Lose:
                // myGui.lose()
                Debug.Log("Lose");
                break;

        }
    }

    void StartGame()
    {
        // SceneManager.LoadScene("signaling sandbox 2");
        init.loadPlayers();
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

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

        //myGui.resume()
        Time.timeScale = 1f;
        IsPaused = false;
    }

    void Pause()
    {
        //myGui.pause()
        Time.timeScale = 0.0f;
        IsPaused = true;

    }

    public void LoadMenu()
    {

        SceneManager.LoadScene("OpeningScene");
    }

    public State getState()
    {
        return state;
    }
}
