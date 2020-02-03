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
    public PlayerScript player;
    


    void Start()
    {
        CreateEchoesParent();
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
        //bool p1Pulse = KeyJoyController.getPulsePressed_Player1() ;
        //bool p2Pulse = KeyJoyController.getPulsePressed_Player2() ;
        //bool ShouldPulse = (tag == "Player1") ?  p1Pulse : p2Pulse ;

        //if (ShouldPulse && GetComponent<PlayerScript>().playerLife > 0)
        //{
        //    if (canSendPulse && player.playerLife > 0)
        //    {
        //        player.playerLife -= removeLife;
        //        StartCoroutine(SendPulse());
        //    }
        //}
    }

    public IEnumerator SendPulse()
    {
        player.numOfPulses += 1;
        GameObject pulseContainer = Instantiate(pulseContainerPrefab, pulseCreatorLocation.position, transform.rotation, echoPulsesParent.transform);
        Instantiate(echoPulseBlackPrefab, pulseCreatorLocation.position, transform.rotation, pulseContainer.transform);
        yield return new WaitForSeconds(0.2f);
        Instantiate(echoPulseWhitePrefab, pulseCreatorLocation.position, transform.rotation, pulseContainer.transform);
        yield return new WaitForSeconds(timeBetweenPulses);
        canSendPulse = true;
    }
}
