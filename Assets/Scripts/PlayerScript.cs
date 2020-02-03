using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private LinkedList<TotemScript> usedTotems;
    private int totemsCount;
    private string totemTag;
    private const string TOTEM_TAG_PLAYER_1 = "totem white";
    private const string TOTEM_TAG_PLAYER_2 = "totem white";
    private const string TAG_PLAYER_1 = "Player1";
    private const string TAG_PLAYER_2 = "Player2";

    private PlanetScript attractorPlanet;
    private GameObject planet;
    private Transform playerTransform;
    public float playerLife;
    private float initialLife;
    public float loseLifeSpeed = 0.1f;
    public GameObject playerLose;
    public GameObject pressToReplay;
    private bool DidHit = false;
    private firstPersonControl playerControl;

    //how long to wait b4get life
    private float Waiting4Pulse = 2.5f;

    //counter for the win screnn
    public int sizeOfTrail;
    public int numOfPulses;

    void Start()
    {
        playerControl = GetComponent<firstPersonControl>();
        totemsCount = 0;
        usedTotems = new LinkedList<TotemScript>();
        totemTag = tag == TAG_PLAYER_1 ? TOTEM_TAG_PLAYER_1 : TOTEM_TAG_PLAYER_2;
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
        return (playerControl.walkSpeed / playerControl.getInitialWalkSpeed());
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == totemTag){
            TotemScript totem = other.GetComponent<TotemScript>();
            bool usedTotem = totem.useTotem();
            if (usedTotem){
                playerControl.walkSpeed = playerControl.walkSpeed + totem.speedBoost;
                if (playerControl.walkSpeed >= playerControl.maxWalkSpeed)
                {
                    StartCoroutine(GetComponent<EchoController>().SendPulse());
                    playerControl.walkSpeed = playerControl.getInitialWalkSpeed();
                }
            }
        }
        
    }
}
    
    
    
    
    
    
    
    // ------ OLD stuff ---------
    
    //private void OnTriggerEnter(Collider other)
    //{
     //   if (other.tag == "pulse" && !DidHit)
       // {
         //   StartCoroutine(waitTillPulseIsOver());
           // DidHit = true;
            //return;
        //}
        //DidHit = false;
        //return;

    //}
    
    //private IEnumerator waitTillPulseIsOver()
   // {
     //   yield return new WaitForSeconds(2.5f);
       // GameObject otherPlayer = (tag == "Player1") ? GameObject.FindGameObjectWithTag("Player2") : GameObject.FindGameObjectWithTag("Player1");
        //if(otherPlayer.GetComponent<PlayerScript>().playerLife < 90)
        //{
          //  otherPlayer.GetComponent<PlayerScript>().playerLife += 10;

//        }


  //  }
//}
