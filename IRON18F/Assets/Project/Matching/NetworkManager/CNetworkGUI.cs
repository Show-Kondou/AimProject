using UnityEngine;
using System.Collections;

using UnityEngine.Networking;

public class CNetworkGUI : CBaseScene
{
    bool isHaveNetworkRole = false;

    void Start()
    {
        isHaveNetworkRole = false;
    }

    private void OnDisconnected(NetworkMessage msg)
    {
        isHaveNetworkRole = false;
        FadeManager.Instance.LoadLevel(SCENE_RAVEL.MATCHING, 0.0f, null);

    }

    void OnGUI()
    {
        if (isHaveNetworkRole)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 12, 160, 24), "Stop"))
            {
                NetworkManager.singleton.StopServer();
                NetworkManager.singleton.StopClient();
                OnDisconnected(null);
            }
            return;
        }

        if (GUI.Button(new Rect(0 , 0, 160, 24), "Start Server"))
        {
            isHaveNetworkRole = NetworkManager.singleton.StartServer();
        }


        if (GUI.Button(new Rect(0 , 30, 160, 24), "Start Client"))
        {
            var client = NetworkManager.singleton.StartClient();
            client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
            isHaveNetworkRole = true;
        }
    }
}
