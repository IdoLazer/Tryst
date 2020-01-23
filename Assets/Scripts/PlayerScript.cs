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

    //how long to wait b4get life
    public float Waiting4Pulse = 2.5f;

    void Start()
    {
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
    }
    public void die()
    {
        playerLose.SetActive(true);
    }
    public void pressToPlayAgain(){
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
            StartCoroutine(waitTillPulseIsOver());
            DidHit = true;
            return;
        }
        DidHit = false;
        return;

    }

    private IEnumerator waitTillPulseIsOver()
    {
        yield return new WaitForSeconds(Waiting4Pulse);
        GameObject otherPlayer = (tag == "Player1") ? GameObject.FindGameObjectWithTag("Player2") : GameObject.FindGameObjectWithTag("Player1");
        otherPlayer.GetComponent<PlayerScript>().playerLife += 10;


    }


}
