//!  CTweenDelay.cs
/*!
 * \details CTweenの遅延実行
 * \author  Shoki Tsuneyama
 * \date    15.12.09　新規作成
 */

using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CTween))]
public class CTweenDelay : MonoBehaviour 
{
    public float m_wait = 0;
    public float m_enterStartTime = 0;
    CTween m_cTween = null;
	// Use this for initialization
	void Awake () 
    {
        m_cTween = GetComponent<CTween>();
        m_cTween.m_isAwake = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if( (m_enterStartTime += Time.deltaTime) >= m_wait )
        {
            m_cTween.Run();
            Destroy(this);
        }
	}
}
