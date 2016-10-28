using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings(channel = 2, sendInterval = 0.2f)]
public class CSyncAltar :NetworkBehaviour {

    [SyncVar]
    public bool m_isLight;

    private Altar m_Altar;
    void Start()
    {
        m_Altar = transform.GetComponent<Altar>();

    }
    

    void Update()
    {

        if (!isServer)
            return;

        if (m_isLight != m_Altar.isLight)
        {
            Cmd_UpdateServer(m_Altar.isLight);
        }
    }

    public void Cmd_UpdateServer(bool value)
    {

        m_isLight = value;

    }

}
