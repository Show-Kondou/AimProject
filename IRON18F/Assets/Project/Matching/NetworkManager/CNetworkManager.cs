using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CNetworkManager : NetworkBehaviour {


    public GameObject player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkManager.singleton.isNetworkActive)
        {
            NetworkServer.Spawn(player);

        }
    }

}
