using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private PlanetScript attractorPlanet;
    private GameObject planet;
    private Transform playerTransform;
    public float playerLife;
    public GameObject playerLose;
    public GameObject pressToReplay;

    void Start()
    {
        playerLife = 100f;
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
        playerLife = 100f;
    }
    public void loseLife(float amount)
    {
        playerLife -= amount;
    }




}
