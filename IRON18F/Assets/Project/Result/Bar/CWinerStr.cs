using UnityEngine;
using System.Collections;

public class CWinerStr : MonoBehaviour
{
    [SerializeField]
    GameObject m_humanStr = null;
    [SerializeField]
    GameObject m_ghostStr = null;

	// Use this for initialization
	void Start ()
    {
        if( CGameScore.Instance.GetWinerIndex() == 3)
        {
            GameObject temp = Instantiate(m_ghostStr);
            temp.transform.parent = transform;
        }
        else
        {
            GameObject temp = Instantiate(m_humanStr);
            temp.transform.parent = transform;
        }
	
	}
	
}
