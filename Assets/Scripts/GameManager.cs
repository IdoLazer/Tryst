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
    public MenuPlayerController menuPlayer1;
    public MenuPlayerController menuPlayer2;
    public float delayUntilReplayAvailable = 2;
    private float distanceBetween;
    private GameGui myGui;
    private DisplayScript display;
    private GameObject planetBlack;
    private GameObject planetWhite;

    //all things to do when we are dead
    private bool PlayerOneDead = false;
    private bool PlayerTwoDead = false;
    private float disease = 1f;
    public float diseaseTime = 10; // how often to remove one life  
    private float TimeToDo = 0f;

    //if we are close to wining 
    public bool ShouldSlowMo = false;
    
    //counter for the win screen
    float GameTime;
    public String[] winTitle = { "you win", "you won", "you wan", "you won" };

    //public size for Trail display at win
    public float TrailSize = 6;


    void Start()
    {
        state = State.Start;
        display = GetComponent<DisplayScript>();
        display.StartDisplay(); //this connects display cameras to 2 monitors
        myGui = GetComponent<GameGui>();
        myGui.showStart();
        //planetBlack = GameObject.Find("PlanetBlack");
        //planetWhite = GameObject.Find("PlanetWhite");

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Start:

                // this function shows the start menue:
                GameTime = Time.time;
                myGui.showStart();

                // when pressed will load the actual game 
                MeetToStartGame();
                break;

            case State.Game:

                if (Input.GetKeyDown(KeyCode.P))
                {
                    shouldPaue();

                }

                if (player1.GetComponent<PlayerScript>().playerLife <= 0)
                {
                    if (!PlayerOneDead)
                    {
                        TimeToDo = Time.time;
                    }
                    player1.GetComponent<PlayerScript>().die();
                    PlayerOneDead = true;
                    if (Time.time - TimeToDo >= diseaseTime)
                    {
                        TimeToDo = Time.time;
                        player2.GetComponent<PlayerScript>().loseLife(disease);
                    }

                }

                if (player2.GetComponent<PlayerScript>().playerLife <= 0)
                {
                    if (!PlayerTwoDead)
                    {
                        TimeToDo = Time.time;
                    }
                    player2.GetComponent<PlayerScript>().die();
                    PlayerTwoDead = true;
                    if (Time.time - TimeToDo >= diseaseTime)
                    {
                        TimeToDo = Time.time;
                        player1.GetComponent<PlayerScript>().loseLife(disease);
                    }

                }
                if (PlayerOneDead && PlayerTwoDead)
                {
                    StartCoroutine(LoseGame());
                }

                distanceBetween = Vector3.Distance(player1.transform.position, player2.transform.position);
                Debug.Log(distanceBetween);

                if (distanceBetween < 3f)
                {
                    player1.layer = 0;
                    player2.layer = 0;
                    Debug.Log("hello");
                    Time.timeScale = 0.2f;
                    transform.position = Vector3.MoveTowards(player1.transform.position, player2.transform.position, 10 * Time.deltaTime);
                    transform.position = Vector3.MoveTowards(player2.transform.position, player1.transform.position, 10 * Time.deltaTime);

                }

                else
                {
                    player1.layer = 14; //P1CAM
                    player2.layer = 15; //P2CAM
                }
                if (distanceBetween < 1f)
                {
                    GameTime = Time.time - GameTime;
                    updateGuiStats();
                    state = State.Win;
                }
                break;

            case State.Win:

                //enlargeTrail();
                myGui.win();
                Time.timeScale = 1.0f;
                player1.SetActive(false);
                player2.SetActive(false);
                //planetBlack.GetComponent<PlanetRotateScript>().rotatePlanet(player1);
                //planetWhite.GetComponent<PlanetRotateScript>().rotatePlanet(player2);

                PressToStartGame();

                break;

            case State.Lose:
                // playes the animation for the press to restart
                player1.GetComponent<PlayerScript>().pressToPlayAgain();
                player2.GetComponent<PlayerScript>().pressToPlayAgain();
                // todo change the M botton

                if (KeyJoyController.getTrailPressed_Player1() || KeyJoyController.getTrailPressed_Player2())
                {
                    clearPieces();
                    player1.GetComponent<PlayerScript>().restart();
                    player2.GetComponent<PlayerScript>().restart();
                    player1.SetActive(false);
                    player2.SetActive(false);
                    PlayerOneDead = false;
                    PlayerTwoDead = false;

                    state = State.Start;

                }

                break;

        }
    }

    private IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(delayUntilReplayAvailable);
        state = State.Lose;
    }

    public void PressToStartGame()
    {
        bool BothPress = KeyJoyController.getTrailPressed_Player1() || KeyJoyController.getTrailPressed_Player2();

        if (BothPress)
        {
            clearPieces();
            state = State.Start;

        }
    }
    public void MeetToStartGame(){
        if (menuPlayer1.meet || menuPlayer2.meet){
            menuPlayer1.reset();
            menuPlayer2.reset();
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

    public void updateGuiStats()
    {

        Transform win1Contain = myGui.getWinOne();
        Transform win2Contain = myGui.getWinTwo();

        TextMesh momentsPlayerOne = win1Contain.transform.GetChild(0).GetComponent<TextMesh>();
        TextMesh TrailsNumPlayerOne = win1Contain.transform.GetChild(1).GetComponent<TextMesh>();
        TextMesh PulseNumPlayerOne = win1Contain.transform.GetChild(2).GetComponent<TextMesh>();

        momentsPlayerOne.text = Mathf.Round(GameTime).ToString() + " moments";
        TrailsNumPlayerOne.text = player1.GetComponent<PlayerScript>().sizeOfTrail.ToString() + " light years";
        PulseNumPlayerOne.text = player1.GetComponent<PlayerScript>().numOfPulses.ToString() + " pulses";

        TextMesh momentsPlayerTwo = win2Contain.transform.GetChild(0).GetComponent<TextMesh>();
        TextMesh TrailsNumPlayerTwo = win2Contain.transform.GetChild(1).GetComponent<TextMesh>();
        TextMesh PulseNumPlayerTwo = win2Contain.transform.GetChild(2).GetComponent<TextMesh>();

        momentsPlayerTwo.text = Mathf.Round(GameTime).ToString() + " moments";
        TrailsNumPlayerTwo.text = player2.GetComponent<PlayerScript>().sizeOfTrail.ToString() + " light years";
        PulseNumPlayerTwo.text = player2.GetComponent<PlayerScript>().numOfPulses.ToString() + " pulses";

        getWinningStatment();
    }

    private void enlargeTrail()
    {
        GameObject TrailHolder = GameObject.Find("TrailPieces");
        Transform[] TrailFamily = TrailHolder.GetComponentsInChildren<Transform>();
        //foreach (Transform var in TrailFamily)
        //{
            //var.transform.localScale = new Vector3(TrailSize, TrailSize, TrailSize);
        //}
    }

    private void getWinningStatment()
    {
        Transform titleOne = myGui.getWinningTitleOne();
        Transform titleTwo = myGui.getWinningTitleTwo();

        TextMesh text1 = titleOne.GetComponent<TextMesh>();
        TextMesh text2 = titleTwo.GetComponent<TextMesh>();

        int randChosenOne = UnityEngine.Random.Range(0, (int)winTitle.Length);
        int randChosenTwo = UnityEngine.Random.Range(0, (int)winTitle.Length);
        if(randChosenTwo == randChosenOne)
        {
            if(randChosenTwo != 0)
            {
                randChosenTwo = 0;
            }
            else
            {
                randChosenTwo = 1;
            }
        }

        text1.text = winTitle[randChosenOne];
        text2.text = winTitle[randChosenTwo];
    }

}
