using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemScript : MonoBehaviour
{
    private GameObject planet;
    private PlanetScript attractorPlanet;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        planet = GameObject.FindGameObjectWithTag("Planet");
        if (planet == null)
        {
            Debug.Log("Planet Not Found!");
        }
        attractorPlanet = planet.GetComponent<PlanetScript>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        attractorPlanet.Attract(transform);
        Vector3 where = Random.onUnitSphere;
        Vector3 onPlanet = where * GetComponent<SphereCollider>().radius * transform.localScale.x;
        transform.position = onPlanet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
