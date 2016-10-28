using UnityEngine;
using System.Collections;

public class CCeremonyEffect : MonoBehaviour
{
    protected CPodium[] m_podiums;
    protected CAwardsCeremony m_awards;
    protected int m_winner;
    protected bool m_isDecision = false;

    virtual public void SetCeremony(CAwardsCeremony awards,CPodium[] podiums)
    {
        m_awards = awards;
        m_podiums = podiums;
    }
    virtual public void Decision(int winner)
    {
        m_winner = winner;
        m_isDecision = true;
    }
    //添え字してい
    protected Vector3 GetTargetPos(int index)
    {
        Vector3 ret = m_podiums[index].transform.position;
        ret.y += 4.73881f;
        return ret;
    }
}
