using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPieceBank : MonoBehaviour
{

    public const int MAX_TRAIL_PIECES = 1024;
    public const string TRAIL_PIECES_PARENT_NAME = "TrailPieces";

    [SerializeField] GameObject trailPiecePrefab;

    private GameObject trailPiecesParent;
    private GameObject[] trailPieceBank;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        trailPieceBank = new GameObject[MAX_TRAIL_PIECES];
        CreateTrailPiecesParent();
    }

    private void CreateTrailPiecesParent()
    {
        trailPiecesParent = GameObject.Find(TRAIL_PIECES_PARENT_NAME);
        if (!trailPiecesParent)
        {
            GameObject planet = GameObject.Find("Planet");
            trailPiecesParent = new GameObject(TRAIL_PIECES_PARENT_NAME);
            trailPiecesParent.transform.parent = planet.transform;
        }
    }

    public void InstantiateTrailPiece(Vector3 pos)
    {
        if (trailPiecesParent == null || trailPiecePrefab == null)
            return;

        if (trailPieceBank[index] == null)
        {
            trailPieceBank[index] = Instantiate(trailPiecePrefab, pos, Quaternion.identity, trailPiecesParent.transform);
        }
        else
        {
            trailPieceBank[index].transform.position = pos;
        }
        index = (index + 1) % MAX_TRAIL_PIECES;
    }

}
