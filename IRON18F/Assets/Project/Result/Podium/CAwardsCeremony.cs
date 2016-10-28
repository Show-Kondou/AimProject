using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CAwardsCeremony : MonoBehaviour
{
    [SerializeField]
    CPodium[] m_podiums =new CPodium[4];

    [SerializeField]
    List<CCeremonyEffect> m_effects = null;
    
    [SerializeField]
    float m_podiumUpperSec = 10;

	// Use this for initialization
	void Start ()
    {
        m_podiums[0].StartTween(m_podiumUpperSec, CGameScore.Instance.GetHumanScore(0));
        m_podiums[1].StartTween(m_podiumUpperSec, CGameScore.Instance.GetHumanScore(1));
        m_podiums[2].StartTween(m_podiumUpperSec, CGameScore.Instance.GetHumanScore(2));
        m_podiums[3].StartTween(m_podiumUpperSec, CGameScore.Instance.GetGhostScore());

        StartCoroutine(EffectEvent());
    }


    //効果にイベントを送る
    IEnumerator EffectEvent()
    {
        CreateEffects();    //効果作成

        yield return new WaitForSeconds(m_podiumUpperSec);
        //優勝者を各効果に通知する
        for (int i = 0; i < m_effects.Count; i++)
            m_effects[i].Decision(CGameScore.Instance.GetWinerIndex());   
    }

    //効果作成
    void CreateEffects()
    {
        for (int i = 0; i < m_effects.Count; i++)
        {
            m_effects[i] = Instantiate(m_effects[i]);
            m_effects[i].SetCeremony(this, m_podiums);
            m_effects[i].name = "Effect" + i;
            m_effects[i].transform.parent = transform;
        }
    }
    
	// Update is called once per frame
	void Update ()
    {
	
	}
}
