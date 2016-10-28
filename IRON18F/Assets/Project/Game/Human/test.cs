using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    Animator tmp;
	// Use this for initialization
	void Start () {
        tmp = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        tmp.SetInteger("State", 0);

	    if(Input.GetKey(KeyCode.A))
        {
            tmp.SetInteger("State",1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            tmp.SetInteger("State", 2);
        }
	}
}
