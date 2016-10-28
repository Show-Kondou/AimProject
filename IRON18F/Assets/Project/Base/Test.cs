using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    private Animator test;

	// Use this for initialization
	void Start () {
        test = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.H))
        {
            test.SetInteger("state", 1);
        }
        else
        {
            test.SetInteger("state", 0);
        }
	}
}
