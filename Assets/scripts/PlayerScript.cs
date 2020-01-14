using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private PlanetScript attractorPlanet;
    private GameObject planet;
    private Transform playerTransform;
    public float playerLife;

    void Start()
    {
        playerLife = 100f;
        GetComponent<Rigidbody>().useGravity = false;
        planet = GameObject.FindGameObjectWithTag("Planet");
        attractorPlanet = planet.GetComponent<PlanetScript>();
        // GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        playerTransform = transform;
    }

    void FixedUpdate()
    {
        if (attractorPlanet)
        {
            attractorPlanet.Attract(playerTransform);
        }
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
}
