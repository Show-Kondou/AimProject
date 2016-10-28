using UnityEngine;
using System.Collections;

public class CRotatationX : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 3.0f, 0);
	}
}
