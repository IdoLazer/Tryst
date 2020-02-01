using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMatChanger : MonoBehaviour
{
    // use for changinf trail shader 
    public Material Black;
    public Material White;
    private bool didHit = false;

    private void OnTriggerEnter(Collider other)
    {
        // if we find a pulse 
        // Debug.Log(other.tag);

        if (other.tag == "pulse" && !didHit)
        {
            // bool to only work for the first pulse we use
            didHit = true;
            MeshRenderer rend = GetComponent<MeshRenderer>() as MeshRenderer;
            string matName = rend.material.name;

            if (matName.Contains(Black.name))
            {
                rend.material = White;
            }
            else
            {
                rend.material = Black;
            }
            return;
        }

        didHit = false;
        return;
    }
}
