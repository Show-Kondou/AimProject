using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class CServer : NetworkBehaviour {
    public enum CONTROL_TYPE
    {
        NONE,
        HUMAN,
        GHOST
    };
  //  public GameObject player;


    void Start()
    {
       // NetworkServer.Spawn(player);
        
    }
}
