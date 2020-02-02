using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemCreatorScript : MonoBehaviour
{
    public const string TOTEMS_PARENT_NAME = "TotemsContainer";
    public int numOfTotems = 20;
    [SerializeField][Range(0, 1f)] float randomDistributionFactor = 0.5f;
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
        Vector3[] locationsOnSphere = new Vector3[numOfTotems];
        int nCount = 0;
        float a = (4 * Mathf.PI) / numOfTotems;
        float d = Mathf.Sqrt(a);
        float mTheta = (int)(Mathf.PI / d);
        float dTheta = Mathf.PI / mTheta;
        float dPhi = a / dTheta;
        for (int m = 0; m < mTheta; m++)
        {
            float theta = Mathf.PI * (m + 0.5f) / mTheta;
            float mPhi = (int)((2 * Mathf.PI * Mathf.Sin(theta)) / dPhi);
            for (int n = 0; n < mPhi; n++)
            {
                float thetaPlusNoise = Mathf.Clamp(theta + Random.Range(-randomDistributionFactor, randomDistributionFactor), 0, Mathf.PI);
                float phi = Mathf.Clamp(((2 * Mathf.PI * n) / mPhi) + Random.Range(-randomDistributionFactor, randomDistributionFactor), 0, Mathf.PI * 2);
                locationsOnSphere[nCount] = new Vector3(
                    Mathf.Sin(thetaPlusNoise) * Mathf.Cos(phi), Mathf.Sin(thetaPlusNoise) * Mathf.Sin(phi), Mathf.Cos(thetaPlusNoise));
                nCount += 1;
            }
        }

        for (int i = 0; i < numOfTotems; i++)
        {
            Vector3 locationOnSphere = locationsOnSphere[i] * scaleFactor;
            Vector3 gravityUp = (locationOnSphere - transform.position).normalized;
            Vector3 localUp = TotemPrefab.transform.up;
            Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * TotemPrefab.transform.rotation;
            totemsBank[i] = Instantiate(TotemPrefab, locationOnSphere, targetRotation, totemsParent.transform) as GameObject;
        }
    }
}
