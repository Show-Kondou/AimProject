using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CFlashing : MonoBehaviour
{
    [SerializeField][Header("a")]
    List<float> m_frameTable =null;

    float m_nowFrame=0;
    
    int m_nowIndex=0;

    bool m_onLight = true;
    

    public bool OnLight{ get { return m_onLight; }}


    // Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_frameTable.Count <= m_nowIndex)    //最終フレーム
        { 
            Destroy(gameObject);
            return;
        }
        m_nowFrame += Time.deltaTime;
        if (m_nowFrame >= m_frameTable[m_nowIndex]) //チカチカ
        {
            m_nowFrame = 0;
            m_nowIndex++;
            m_onLight = transform.GetComponent<Light>().enabled ^= true;
        }
	}
}