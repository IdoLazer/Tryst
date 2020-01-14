using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    public TrailPieceBank bank;
    public float waitingTimeBetweenTrailPieces = 0.3f;
    Coroutine leaveTrailCoroutine;
    public PlayerScript player;
    public float removeLife = 1.5f;

    private bool startedTrail = false;

    // Update is called once per frame

    void Update()
    {
        if (!bank)
            return;

        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire2"))
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
            bank.InstantiateTrailPiece(transform.position);
            player.playerLife -= removeLife;
            Debug.Log(player.playerLife);
            yield return new WaitForSeconds(waitingTimeBetweenTrailPieces);
        }
    }
}
