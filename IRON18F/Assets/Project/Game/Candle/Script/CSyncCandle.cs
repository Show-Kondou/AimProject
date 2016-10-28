using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


[NetworkSettings(channel = 0, sendInterval = 0.2f)]
public class CSyncCandle : NetworkBehaviour {

    [SyncVar]
    public Vector3 m_SyncPosition;

    [SyncVar]
    public bool m_SyncIsFire;

    [SyncVar]
    public bool m_SyncIsStock;


    public CCandle m_candle;


    private float threshold = 1.0f;

    void Start()
    {
        m_candle = gameObject.GetComponent<CCandle>();
        m_candle.isServer = isServer;
        if (isServer)
        {
            m_SyncPosition = transform.position;
        }
        if (isClient)
        {
            transform.position = new Vector3(0,-100,0);
        }
    }
    


    void Update()
    {
        if (isServer)
        {
            ServerUpdata();
        }


        if (isClient)
        {
            DownloadServer();
        }

        
    }

    [Server]
    void ServerUpdata()
    {
        if (Vector3.Distance(transform.position, m_SyncPosition) > threshold)
        {
            m_SyncPosition = transform.position;
        }

        if (m_SyncIsFire != m_candle.IsFire)
        {
            m_SyncIsFire = m_candle.IsFire;
        }

        if (m_SyncIsStock != m_candle.IsStock)
        {
            m_SyncIsStock = m_candle.IsStock;
        }

        if (m_SyncIsStock&&!m_SyncIsFire)
        {
            m_SyncPosition = new Vector3(0,-100,0);

        }

    }

    [Client]
    void DownloadServer()
    {
        if (Vector3.Distance(transform.position, m_SyncPosition) > threshold)
        {
              transform.position = m_SyncPosition;
        }
        m_candle.IsFire = m_SyncIsFire;
        m_candle.IsStock = m_SyncIsStock;
        
    }


}
