using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CPlayer : NetworkBehaviour {

    [SyncVar]
    public int m_meType;

    public GameObject m_ghost;
    public GameObject m_human;

    void Start()
    {
        if (isServer||isLocalPlayer)
        {

            GameObject newPlayer = null;

            var Conn = connectionToClient;

            
            switch (m_meType)
            {
                case 1:
                    if (isServer)
                    {
                        newPlayer = Instantiate(m_human);
                    }

                    NetworkServer.ReplacePlayerForConnection(Conn, newPlayer, 0);
                    if (isServer)
                    {
                        NetworkServer.Spawn(newPlayer);
                    }
                    break;
                case 2:
                    if (isServer)
                    {
                        newPlayer = Instantiate(m_ghost);
                    }

                    NetworkServer.ReplacePlayerForConnection(Conn, newPlayer, 0);
                    if (isServer)
                    {
                        NetworkServer.Spawn(newPlayer);
                    }
                    break;

            }



            Destroy(this.gameObject);

        }




    }
}
