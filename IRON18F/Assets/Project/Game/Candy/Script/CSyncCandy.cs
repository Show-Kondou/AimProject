using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CSyncCandy : NetworkBehaviour {

    [SyncVar]
    public Vector3 m_syncPosition;

    private float threshold = 0.1f;

    // Use this for initialization
    void Start () {
        UpdateServer();
	}
	
	// Update is called once per frame
	void Update () {

        UpdateServer();


        UpdateClient();

	}


    void UpdateServer()
    {
        if (isServer)
        {
            if (Vector3.Distance(m_syncPosition, transform.position) > threshold)
            {
                m_syncPosition = transform.position;
            }

        }

    }

    void UpdateClient()
    {
        if (isClient)
        {

            transform.position = m_syncPosition;
        }
    }

}
