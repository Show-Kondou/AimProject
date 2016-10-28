using UnityEngine;
using System.Collections;

public class CMatchingCursor : MonoBehaviour
{
    bool m_isGhostSelect = false;
    Coroutine m_moveCoroutine =null;

    [SerializeField]
    Vector3 m_ghostSelectPos = new Vector3();
    [SerializeField]
    Vector3 m_humanSelectPos = new Vector3();
    [SerializeField]
    float m_sec = 2;
    FEase SpringLeap = CEase.SPRING;

	// Update is called once per frame
	void Update ()
    {
        if (m_moveCoroutine == null)
        {
            if (Input.GetKeyDown(KeyCode.D) && m_isGhostSelect)
            {
                m_isGhostSelect = false;
                m_moveCoroutine=StartCoroutine(Move(m_sec)); 
            }
            else if (Input.GetKeyDown(KeyCode.A) && m_isGhostSelect==false)
            {
                m_isGhostSelect = true;
                m_moveCoroutine = StartCoroutine(Move(m_sec)); 
            }
        }
	}

    IEnumerator Move(float sec)
    {
        Vector3 target,old;
        if(m_isGhostSelect)
        {
            target =m_ghostSelectPos;
            old = m_humanSelectPos;
        }
        else
        {
            target = m_humanSelectPos;
            old = m_ghostSelectPos;
        }
        for (float i = 0; i < sec; i += Time.deltaTime)
        {
            yield return 0;
            transform.SetX( SpringLeap(target.x, old.x, i / sec));
            transform.SetY( SpringLeap(target.y, old.y, i / sec));
        }
        SpringLeap(m_ghostSelectPos.x, m_humanSelectPos.x, 1);
        m_moveCoroutine = null;
    }
}
