using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class CLobbyManager :NetworkLobbyManager {


    // for users to apply settings from their lobby player object to their in-game player object
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        var cc = lobbyPlayer.GetComponent<CLobbyPlayer>();
        var work = gamePlayer.GetComponent<CPlayer>();

        work.m_meType = cc.m_IntmeType;


        return true;
    }
}
