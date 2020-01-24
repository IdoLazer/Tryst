using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    public TrailPieceBank bank;
    [SerializeField] Transform trailCreatorLocation;
    public float waitingTimeBetweenTrailPieces = 0.3f;
    Coroutine leaveTrailCoroutine;
    public PlayerScript player;
    public float removeLife = 10f;

    private bool startedTrail = false;
    private KeyJoyController keyJoyController;
    void Start()
    {
        keyJoyController = new KeyJoyController() ;
    }
    void Update()
    {
        if (!bank)
            return;
        bool p1Trail = keyJoyController.GetPlayer1TrailPress();
        bool p2Trail = keyJoyController.GetPlayer2TrailPress();
        bool ShouldTrail = (tag == "Player1") ?  p1Trail : p2Trail ;
        if (ShouldTrail)
        {
            if (!startedTrail)
            {
                startedTrail = true;
                leaveTrailCoroutine = StartCoroutine(LeaveTrail());
            }
        }
        else
        {
            if (startedTrail)
            {
                startedTrail = false;
                StopCoroutine(leaveTrailCoroutine);
            }
        }
    }

    private IEnumerator LeaveTrail()
    {
        while (true)
        {
            bank.InstantiateTrailPiece(trailCreatorLocation.position);
            player.playerLife -= removeLife;
            yield return new WaitForSeconds(waitingTimeBetweenTrailPieces);
        }
    }
}
