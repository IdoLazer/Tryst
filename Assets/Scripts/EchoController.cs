using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoController : MonoBehaviour
{
    public const string ECHO_PULSE_PARENT_NAME = "Echoes";

    [SerializeField] GameObject echoPulseBlackPrefab;
    [SerializeField] GameObject echoPulseWhitePrefab;
    [SerializeField] GameObject pulseContainerPrefab;
    [SerializeField] Transform pulseCreatorLocation;
    [SerializeField] float timeBetweenPulses = 0.5f;

    private GameObject echoPulsesParent;
    private bool canSendPulse = true;

    //removing life when using pulse
    public float removeLife = 10;
    private PlayerScript player;



    void Start()
    {
        CreateEchoesParent();
        player = GameObject.FindObjectOfType<PlayerScript>();
    }

    private void CreateEchoesParent()
    {
        echoPulsesParent = GameObject.Find(ECHO_PULSE_PARENT_NAME);
        if (!echoPulsesParent)
        {
            echoPulsesParent = new GameObject(ECHO_PULSE_PARENT_NAME);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonDown("Fire1"))
        {
            if (canSendPulse && player.playerLife > 0)
            {
                player.playerLife -= removeLife;
                StartCoroutine(SendPulse());
            }
        }
    }

    private IEnumerator SendPulse()
    {
        GameObject pulseContainer = Instantiate(pulseContainerPrefab, pulseCreatorLocation.position, transform.rotation, echoPulsesParent.transform);
        Instantiate(echoPulseBlackPrefab, pulseCreatorLocation.position, transform.rotation, pulseContainer.transform);
        yield return new WaitForSeconds(0.2f);
        Instantiate(echoPulseWhitePrefab, pulseCreatorLocation.position, transform.rotation, pulseContainer.transform);
        yield return new WaitForSeconds(timeBetweenPulses);
        canSendPulse = true;
    }
}
