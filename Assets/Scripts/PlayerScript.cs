using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private LinkedList<TotemScript> usedTotems;
    private int totemsCount;
    private const string TOTEM_TAG = "totem";
    private const string TOTEM_TAG_PLAYER_2 = "totem white";
    private const string TAG_PLAYER_1 = "Player1";
    private const string TAG_PLAYER_2 = "Player2";
    private const string BLACK_TRAIL_TAG = "Trail Piece(Clone)";
    private const string WHITE_TRAIL_TAG = "InvertedTrailPiece(Clone)";
    private const string PULSE_TAG = "pulse";
    private const string TRAIL_TAG = "t";


    private PlanetScript attractorPlanet;
    private GameObject planet;
    private Transform playerTransform;
    public float playerLife;
    private float initialLife;
    public float loseLifeSpeed = 0.1f;
    public GameObject playerLose;
    public GameObject pressToReplay;
   /public ParticleSystem boostPS;
    public ParticleSystem closeToWinPS;
    public ParticleSystem winPS;
    private bool DidHit = false;
    private firstPersonControl playerControl;

    //how long to wait b4get life
    private float Waiting4Pulse = 2.5f;

    //counter for the win screnn
    public int sizeOfTrail;
    public int numOfPulses;

    //bool for only adding ui once
    private bool playedPulse = false;

    void Start()
    {
        playerControl = GetComponent<firstPersonControl>();
        totemsCount = 0;
        usedTotems = new LinkedList<TotemScript>();
        sizeOfTrail = 0;
        numOfPulses = 0;
        initialLife = playerLife;
        GetComponent<Rigidbody>().useGravity = false;
        planet = GameObject.FindGameObjectWithTag("Planet");
        if (planet == null)
        {
            Debug.Log("Planet not found! Whyyy?");
        }
        attractorPlanet = planet.GetComponent<PlanetScript>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        playerTransform = transform;
        playerLose.SetActive(false);
        pressToReplay.SetActive(false);
        //check who the other trail is:

    }

    void FixedUpdate()
    {
        if (attractorPlanet)
        {
            attractorPlanet.Attract(playerTransform);
        }
    }
    public void die()
    {
        playerLose.SetActive(true);
    }
    public void pressToPlayAgain()
    {
        pressToReplay.SetActive(true);

    }
    public void restart()
    {
        pressToReplay.SetActive(false);
        playerLose.SetActive(false);
        playerLife = initialLife;
    }
    public void loseLife(float amount)
    {
        playerLife -= amount;
    }

    public float ValForShader()
    {
        return ((playerControl.walkSpeed - playerControl.getInitialWalkSpeed()) / playerControl.maxWalkSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TOTEM_TAG)
        {
            TotemScript totem = other.GetComponent<TotemScript>();
            bool usedTotem = totem.useTotem();
            if (usedTotem)
            {
                //boostPS.Play();
                playerControl.walkSpeed = playerControl.walkSpeed + totem.speedBoost;
                if (playerControl.walkSpeed >= playerControl.maxWalkSpeed)
                {
                    StartCoroutine(GetComponent<EchoController>().SendPulse());
                    GetComponent<PlayerUIScript>().ActivateUI("sentPulse");
                    playerControl.walkSpeed = playerControl.getInitialWalkSpeed();
                }
            }
        }

        if (other.name == BLACK_TRAIL_TAG && tag == TAG_PLAYER_2)
        {
            GetComponent<PlayerUIScript>().ActivateUI("meetOtherTrail");

        }

        if (other.name == WHITE_TRAIL_TAG && tag == TAG_PLAYER_1)
        {
            GetComponent<PlayerUIScript>().ActivateUI("meetOtherTrail");

        }

        if (other.tag == PULSE_TAG && !playedPulse)
        {
            playedPulse = true;
            GetComponent<PlayerUIScript>().ActivateUI("recievespulse");

        }
        playedPulse = false;

    }

    public void CloseToWin(bool isCloseToWin)
    {
        if (isCloseToWin)
        {
            closeToWinPS.Play();
        }
        else
        {
            closeToWinPS.Pause();
        }
    }

    public void Win(Vector3 meetingPoint)
    {
        winPS.Play();
        playerControl.MoveTowards(meetingPoint);
    }
}







