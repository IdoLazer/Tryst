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

    void Update()
    {
        if (!bank)
            return;
        bool p1Trail = KeyJoyController.getTrailPressed_Player1();
        bool p2Trail = KeyJoyController.getTrailPressed_Player2();
        bool ShouldTrail = (tag == "Player1") ?  p1Trail : p2Trail ;
        if (ShouldTrail  && GetComponent<PlayerScript>().playerLife > 0)
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
            player.sizeOfTrail += 1;
            yield return new WaitForSeconds(waitingTimeBetweenTrailPieces);
        }
    }


}
