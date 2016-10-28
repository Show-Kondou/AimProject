using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

[NetworkSettings(channel = 2, sendInterval = 0.2f)]
public class CSyncCandleSpawnerManager : NetworkBehaviour {


    [Header("スポーン間隔(秒)")]
    public float m_spawnInterval;

    float m_nowTime;

    List<GameObject> m_candleSpawner;

    // Use this for initialization
    void Start() {

        m_candleSpawner = new List<GameObject>();

        m_nowTime = 0;
    }

    // Update is called once per frame
    void Update() {
        if (!isServer) return;
        if (m_candleSpawner.Count != transform.childCount)
        {
            GetChildren();

        }




        SpawnCandle();

    }


    void SpawnCandle()
    {
        m_nowTime += Time.deltaTime;

        if (m_nowTime < m_spawnInterval) return;
        int i = 0;

        while (true)
        {
            
            int num = Random.Range(1, m_candleSpawner.Count);
            
            if (m_candleSpawner[num - 1].GetComponent<CSyncCandleSpawner>().m_nowSpawnCandle == null)
            {
                NetworkServer.Spawn(m_candleSpawner[num - 1].GetComponent<CSyncCandleSpawner>().SpwanCandle());
                break;
            }
            i++;
            if (i > m_candleSpawner.Count) break;
       

        }

        m_nowTime = 0;
    }

    void GetChildren()
    {

        m_candleSpawner = GetAllChildren.GetAll(gameObject);

    }

}


