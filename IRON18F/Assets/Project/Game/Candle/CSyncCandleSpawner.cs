using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings(channel = 2, sendInterval = 0.2f)]

public class CSyncCandleSpawner : MonoBehaviour {


    [SerializeField]
    private GameObject m_candle;

    public GameObject m_nowSpawnCandle;


    void Start()
    {
        m_nowSpawnCandle = null;
    }
    void Update()
    {
        if (m_nowSpawnCandle == null) return;

        if (m_nowSpawnCandle.GetComponent<CCandle>().IsStock == true)
        {
            m_nowSpawnCandle = null;
        }

    }

    public GameObject SpwanCandle()
    {
        m_nowSpawnCandle = Instantiate(m_candle);
        m_nowSpawnCandle.transform.position = transform.position;
        m_nowSpawnCandle.transform.parent = transform;


        return m_nowSpawnCandle;
    }

    
}
