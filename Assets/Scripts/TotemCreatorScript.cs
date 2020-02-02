using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemCreatorScript : MonoBehaviour
{
    public const string TOTEMS_PARENT_NAME = "TotemsContainer";
    public int numOfTotems = 50;
    private GameObject[] totemsBank;
    private GameObject totemsParent;
    private GameObject planet;
    private float scaleFactor;
    [SerializeField] GameObject TotemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        totemsBank = new GameObject[numOfTotems];
        scaleFactor = GetComponent<SphereCollider>().radius * transform.localScale.x;
        planet = GameObject.Find("Planet");
        CreateTotemsParent();
    }

    private void CreateTotemsParent()
    {
        totemsParent = GameObject.Find(TOTEMS_PARENT_NAME);
        if (!totemsParent)
        {
            totemsParent = new GameObject(TOTEMS_PARENT_NAME);
        }
    }

    public void LoadTotems()
    {
        for (int i = 0; i < numOfTotems; i++)
        {
            Vector3 locationOnSphere = Random.onUnitSphere * scaleFactor;
            Vector3 gravityUp = (locationOnSphere - transform.position).normalized;
            Vector3 localUp = TotemPrefab.transform.up;
            Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * TotemPrefab.transform.rotation;
            totemsBank[i] = Instantiate(TotemPrefab, locationOnSphere, targetRotation, totemsParent.transform) as GameObject;
        }
    }
}
