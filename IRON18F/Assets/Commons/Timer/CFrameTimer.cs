using UnityEngine;
using System.Collections;

public class CFrameTimer : MonoBehaviour 
{
    public float m_time = 0;
    private bool m_isStop=false;

    // Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_isStop)
            return;
        m_time += Time.deltaTime;
	}
    public void StopSwitch() { m_isStop = true; }   //ストップフラグオン
    public void StartTime() { m_isStop = false; }   //ストップフラグオフ
    public void Reset() { m_time = 0; }             //タイムをリセット
}
