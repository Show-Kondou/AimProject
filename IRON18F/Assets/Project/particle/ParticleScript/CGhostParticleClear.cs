using UnityEngine;
using System.Collections;

public class CGhostParticleClear : MonoBehaviour {
    [SerializeField]
    GameObject HeadSmokePrefab;
    [SerializeField]
    GameObject BodySmokePrefab;

    [SerializeField]
    float timing;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (HeadSmokePrefab.GetComponent<ParticleSystem>().time <= timing)
        {
            HeadSmokePrefab.GetComponent<ParticleSystem>().Clear();
            BodySmokePrefab.GetComponent<ParticleSystem>().Clear();
        }

    }
}
