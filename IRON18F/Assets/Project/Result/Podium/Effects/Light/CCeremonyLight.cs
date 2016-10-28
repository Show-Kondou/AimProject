using UnityEngine;
using System.Collections;

public class CCeremonyLight : CCeremonyEffect
{
    int m_targetIndex;
    int m_nextIndex;
    
    float m_timer = 0;
    float m_maxTime = 0.5f;

    Vector3 randomLeapPos;  
    
    void Start()
    {
        m_targetIndex = Random.Range((int)0, m_podiums.Length);
        m_nextIndex = Random.Range((int)0, m_podiums.Length);
    }
	
    void Update()
    {
        if (m_isDecision)
            WinerLight();
        else
            RandomLight();
        UpdateTimer(m_maxTime); //秒数更新
    }

    //優勝者にライトを向ける
    void WinerLight()
    {
        Vector3 temp = Vector3.Lerp(randomLeapPos, GetTargetPos(m_winner), CMath.Cubic(m_timer / m_maxTime));
        transform.LookAt(temp);
    }
    //ランダムでライトを動かす
    void RandomLight()
    {
        randomLeapPos = Vector3.Lerp(GetTargetPos(m_targetIndex), GetTargetPos(m_nextIndex), CMath.Cubic(m_timer / m_maxTime));
        transform.LookAt(randomLeapPos);
    }
    //秒数更新
    void UpdateTimer(float maxframe)
    {
        m_timer += Time.deltaTime;
        if (m_timer >= maxframe)
        {
            if (m_isDecision)
                m_timer = maxframe;
            else
                m_timer = 0;
            m_targetIndex = m_nextIndex;
            while (m_targetIndex == m_nextIndex)
               m_nextIndex = Random.Range((int)0, m_podiums.Length);
        }
    }

    override public void Decision(int winner)
    {
        m_winner = winner;
        m_isDecision = true;
        m_timer = 0;
    }

}
