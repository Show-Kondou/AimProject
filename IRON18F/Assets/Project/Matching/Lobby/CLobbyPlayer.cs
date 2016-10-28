using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class CLobbyPlayer : NetworkBehaviour
{
    public enum CONTROL_TYPE
    {
        NONE = 0,
        HUMAN,
        GHOST
    };
    public CLobbyPlayer.CONTROL_TYPE m_meType;

    [SyncVar]
    public int m_IntmeType;

    private CLobbyGameManager m_lobbyManager;



    // Use this for initialization
    void Start()
    {
        m_lobbyManager = GameObject.Find("LobbyGameManager").GetComponent<CLobbyGameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnGUI()
    {


        if (!gameObject.GetComponent<NetworkLobbyPlayer>().readyToBegin)
        {

            if (m_lobbyManager.m_SelectHuman < 3 && isLocalPlayer)
            {
                if (GUI.Button(new Rect(0, 100, 160, 24), "Human"))
                {
                    m_meType = CONTROL_TYPE.HUMAN;
                    Cmd_AddHumanum();
                    gameObject.GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();
                }
            }


            if (!m_lobbyManager.CanSelectGhost && isLocalPlayer)
            {
                if (GUI.Button(new Rect(0, 140, 160, 24), "Ghost"))
                {

                    m_meType = CONTROL_TYPE.GHOST;
                    Cmd_SelectGhost();
                    gameObject.GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();
                }
            }
        }


        if (gameObject.GetComponent<NetworkLobbyPlayer>().readyToBegin)
        {
            GUI.Label(new Rect(0, 0, 100, 30), "Ready");
        }

    }

    [Command]
    void Cmd_SelectGhost()
    {
        m_IntmeType = 2;
        m_lobbyManager.CanSelectGhost = true;

        
       
    }
    [Command]
    void Cmd_AddHumanum()
    {
        m_IntmeType = 1;
        m_lobbyManager.m_SelectHuman++;


       
    }

}
