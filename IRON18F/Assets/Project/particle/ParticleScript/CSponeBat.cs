using UnityEngine;
using System.Collections;

public class CSponeBat : MonoBehaviour {
    [SerializeField]
    GameObject FirstPrefab;
    [SerializeField]
    GameObject SecondPrefab;
    [SerializeField]
    float GenerationTiming;

    private GameObject FirstObj;
    private GameObject SecondObj;
	// Use this for initialization
	void Start () {
        FirstObj = (GameObject)Instantiate(FirstPrefab,this.transform);
	}
	
	// Update is called once per frame
	void Update () {
        if (FirstObj != null && SecondObj == null)
        {
            if (FirstObj.GetComponent<ParticleSystem>().time >= GenerationTiming)
            {
                //Destroy(FirstObj);Pause
                //FirstObj.GetComponent<ParticleSystem>().Pause();
                SecondObj = (GameObject)Instantiate(SecondPrefab,this.transform);
            }
        }
        if (SecondObj != null)
        {
            if (SecondObj.GetComponent<ParticleSystem>().time >= 1.5f)
            {
                Destroy(FirstObj);
            }
            if (!SecondObj.GetComponent<ParticleSystem>().IsAlive())
            {
                //Destroy(FirstObj);
                Destroy(this.gameObject);
            }
        }
        
	}
}
