using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private PlanetScript attractorPlanet;
    private GameObject planet;
    private Transform playerTransform;
    public float playerLife;
    private float initialLife;
    public GameObject playerLose;
    public GameObject pressToReplay;
    private bool DidHit = false;

    //counter for the win screnn
    public int sizeOfTrail;
    public int numOfPulses;
    //pulse reminder
    private float HowLongTillPulse = 10f;
    private float currTimePulse;
    //trail reminder
    private float HowLongTillTrail = 20f;
    private float currTimeTrail;
    //lightour reminder
    private bool FirstWarning = true;
    public float randValInput = 0.3f; 



    void Start()
    {
        //get the time we start
        currTimePulse = Time.time;
        currTimeTrail = Time.time;

        sizeOfTrail = 0;
        numOfPulses = 0;
        initialLife = playerLife;
        GetComponent<Rigidbody>().useGravity = false;
        planet = GameObject.FindGameObjectWithTag("Planet");
        if (planet == null)
        {
            Debug.Log("OH WHYYYY");
        }
        attractorPlanet = planet.GetComponent<PlanetScript>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        playerTransform = transform;
        playerLose.SetActive(false);
        pressToReplay.SetActive(false);
    }

    void FixedUpdate()
    {
        if (attractorPlanet)
        {
            attractorPlanet.Attract(playerTransform);
        }

        //this parts checks if the player didnt send a pulse and if so will activate 
        // a reminder 
        if (Time.time - currTimePulse > HowLongTillPulse)
        {
            ShouldActiveStatment("didntsendPulse");
            HowLongTillPulse = HowLongTillPulse * 2;
            currTimePulse = Time.time;
        }

        //this parts checks if the player didnt make a trail and if so will activate 
        // a reminder 
        if (Time.time - currTimeTrail > HowLongTillTrail)
        {
            ShouldActiveStatment("didntMakeTRail");
            HowLongTillPulse = HowLongTillPulse * 2;
            currTimeTrail = Time.time;
        }

        //this parts checks if the player life low
        if (playerLife <= 15 && FirstWarning)
        {
            GetComponent<PlayerUIScript>().ActivateUI("lightout");
            FirstWarning = false;
        }

        if(playerLife <= 5 && !FirstWarning)
        {
            GetComponent<PlayerUIScript>().ActivateUI("lightout");
        }

        //basic call

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
        return (playerLife / 100);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pulse" && !DidHit)
        {
            currTimePulse = Time.time;
            DidHit = true;
        }
        if (other.tag == "pulse" && DidHit)
        {
            DidHit = false;
            ShouldActiveStatment("recievespulse");
            return;
        }

        if (other.tag == "piece")
        {
            if(other.name == "InvertedTrailPiece(Clone)")
            {
                if (tag == "Player1")
                {

                    ShouldActiveStatment("meetOtherTrail");
                    return;
                }

                if (tag == "Player2")
                {
                    currTimeTrail = Time.time;
                }
            }
            if(other.name == "Trail Piece(Clone)")
            {
                if (tag == "Player2")
                {
                    ShouldActiveStatment("meetOtherTrail");
                    return;
                }
                if(tag == "Player1")
                {
                    currTimeTrail = Time.time;

                }
            }
        }

    }


    private void ShouldActiveStatment(string name)
    {
        float randVal = Random.value;
        if(randVal < randValInput)
        {
            GetComponent<PlayerUIScript>().ActivateUI(name);
        }
        return;
    }
}

    


