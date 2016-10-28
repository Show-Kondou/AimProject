using System;
using System.ComponentModel;
using UnityEngine.Networking.Match;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using System.Collections.Generic;

[RequireComponent(typeof(NetworkManager))]

public class NetworkManagerUI : MonoBehaviour
{
    public NetworkManager manager;
    [SerializeField]
    public GameObject[] Manager;
    [SerializeField]
    public bool showGUI = true;
    [SerializeField]
    public bool match_f = true;
    [SerializeField]
    public int offsetX;
    [SerializeField]
    public int offsetY;

    // Runtime variable
    bool m_ShowServer;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
        manager.StartMatchMaker();
    }

    void Update()
    {
        Manager = GameObject.FindGameObjectsWithTag("Player");
        if (Manager.Length >= 5)
        {
            StartGame();
        }
        if (!showGUI)
            return;

    }

    void OnGUI()
    {
        if (!showGUI)
            return;

        int xpos = 10 + offsetX;
        int ypos = 40 + offsetY;
        const int spacing = 24;

        bool noConnection = (manager.client == null || manager.client.connection == null ||
                             manager.client.connection.connectionId == -1);


        if (NetworkServer.active || manager.IsClientConnected())
        {
            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "始める"))
            {
                StartGame();
            }

            ypos += spacing;
            if (GUI.Button(new Rect(xpos, ypos, 200, 20), "戻る"))
            {
                manager.StopHost();
                manager.StartMatchMaker();
            }
            ypos += spacing;
        }
        if (!NetworkServer.active && !manager.IsClientConnected() && noConnection)
        {
            ypos += 10;

            if (manager.matchInfo == null)
            {
                if (GUI.Button(new Rect(xpos, ypos, 200, 20), "サーバーを作る"))
                {
                    manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", "", "", 0, 0, manager.OnMatchCreate);
                }
                ypos += spacing;

                GUI.Label(new Rect(xpos, ypos, 100, 20), "サーバー名:");
                manager.matchName = GUI.TextField(new Rect(xpos + 50, ypos, 100, 20), manager.matchName);
                ypos += spacing;

                manager.matchMaker.ListMatches(0, 20, "", true, 0, 0, manager.OnMatchList);
                ypos += spacing;
                if (manager.matches != null)
                {
                    foreach (var match in manager.matches)
                    {
                        if (GUI.Button(new Rect(xpos, ypos, 200, 20), "サーバー名[" + match.name + "]に入る"))
                        {
                            manager.matchName = match.name;
                            manager.matchSize = (uint)match.currentSize;
                            manager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, manager.OnMatchJoined);
                        }
                        ypos += spacing;
                    }
                }
                ypos += spacing;
            }
        }
    }
    void StartGame()
    {
        showGUI = false;
        match_f = false;
    }
}

