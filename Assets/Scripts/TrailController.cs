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
    public float removeLife = 0.01f;

    private bool startedTrail = false;

    // use for changinf trail shader 
    public Material Black;
    public Material White;
    private bool didHit = false;

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

    private void OnTriggerEnter(Collider other)
    {
        // if we find a pulse 
        if (other.tag == "pulse" && !didHit)
        {
            // bool to only work for the first pulse we use
            didHit = true;

            //find the trail pices container
            GameObject Trail = GameObject.Find("TrailPieces");

            if (Trail)
            {
                // loop over all children and change the render
                MeshRenderer[] TrailChildren = Trail.GetComponentsInChildren<MeshRenderer>() as MeshRenderer[];
                foreach (MeshRenderer child in TrailChildren)
                {
                    string matName = child.material.name;

                    if (matName.Contains(Black.name))
                    {
                        child.material = White;
                    } 
                    else
                    {
                        child.material = Black;
                    }
                }

            }

            return;
        }
        didHit = false;
        return;
    }



}
