﻿using System;
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
            bank.InstantiateTrailPiece(trailCreatorLocation.position);
            player.playerLife -= removeLife;
            yield return new WaitForSeconds(waitingTimeBetweenTrailPieces);
        }
    }
}
