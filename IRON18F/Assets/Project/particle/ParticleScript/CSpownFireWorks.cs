using UnityEngine;
using System.Collections;

public class CSpownFireWorks : MonoBehaviour {
    [SerializeField]
    GameObject FireWorksPrefab;
    [SerializeField]
    int instantiate_interval;

    public int num;
	// Use this for initialization
	void Start () {
        num = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (instantiate_interval<=num)
        {
            Instantiate(FireWorksPrefab, new Vector3(Random.Range(-3.8f, 3.8f), Random.Range(6.0f, 3.0f), 2.5f), this.transform.rotation);
            instantiate_interval = Random.Range(40, 60);
            num = 0;
        }
        num++;
	}
}
