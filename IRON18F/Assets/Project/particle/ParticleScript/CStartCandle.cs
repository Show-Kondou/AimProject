using UnityEngine;
using System.Collections;

public class CStartCandle : MonoBehaviour {
    [SerializeField]
    GameObject FirePrefab;
    [SerializeField]
    GameObject SmokePrefab;

    private GameObject FireObj;
    private GameObject SmokeObj;
	// Use this for initialization
	void Start () {
        FireObj = (GameObject)Instantiate(FirePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1.13f, this.transform.position.x), FirePrefab.transform.rotation, this.transform);
	}
	
	// Update is called once per frame
	void Update () {

        if (FireObj!=null)
        {
            if (FireObj.GetComponent<ParticleSystem>().time >= 3.5f)
            {
                Destroy(FireObj);
                SmokeObj = (GameObject)Instantiate(SmokePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1.2f, this.transform.position.x), SmokePrefab.transform.rotation, this.transform);
            }
        }
        if (SmokeObj != null)
        {
            if (!SmokeObj.GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(this.gameObject);
            }
        }
        
	}
}
