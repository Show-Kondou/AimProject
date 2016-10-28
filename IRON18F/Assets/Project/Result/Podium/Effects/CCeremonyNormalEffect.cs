using UnityEngine;
using System.Collections;

public class CCeremonyNormalEffect : CCeremonyEffect
{
    [SerializeField]
    GameObject m_instance =null;

    [SerializeField]
    bool m_isDecision_ = false;

    void Create()
    {
        GameObject gameObj =Instantiate(m_instance);
        gameObj.transform.parent = transform;
    }

    override public void Decision(int winner)
    {
        if (m_isDecision_ )
        Create();
    }

    override public void SetCeremony(CAwardsCeremony awards, CPodium[] podiums)
    {
        if (m_isDecision_ == false)
            Create();
    }
}
