using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CLobbyGameManager : NetworkBehaviour {

    [SyncVar]
    public bool CanSelectGhost;

    [SyncVar]
    public int m_SelectHuman;


    void Start()
    {

    }

}
