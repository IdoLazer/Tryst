using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiating : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public int numObjects = 2;
    public float startTime;

    public void loadPlayers()
    {
        Vector3 where1 = Random.onUnitSphere;
        Vector3 where2 = Random.onUnitSphere;
        Vector3 onPlanet1 = where1 * GetComponent<SphereCollider>().radius * transform.localScale.x;
        Vector3 onPlanet2 = where2 * GetComponent<SphereCollider>().radius * transform.localScale.x;

        if (Vector3.Distance(onPlanet1, onPlanet2) < GetComponent<SphereCollider>().radius * transform.localScale.x)
        {
            onPlanet2 *= -1;
        }

        player1.transform.position = onPlanet1;
        player2.transform.position = onPlanet2;

        player1.SetActive(true);
        player2.SetActive(true);

        startTime = Time.deltaTime;
    }

}
