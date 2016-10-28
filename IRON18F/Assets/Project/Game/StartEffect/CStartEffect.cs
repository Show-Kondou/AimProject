using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CStartEffect : MonoBehaviour
{
    [SerializeField][Header("開始演出のリスト")]
    List<GameObject> m_effects =null;

    [SerializeField][Header("秒数")]
    List<float> m_frames =null;


	// Use this for initialization
	void Start ()
    {
        StartCoroutine(InstanceEffect());
	}
    IEnumerator InstanceEffect()
    {
        for(int i=0 ;i< m_effects.Count;i++)
        {
            GameObject temp = Instantiate(m_effects[i]);
            temp.transform.parent = transform;
            Destroy(temp, m_frames[i]);
            yield return new WaitForSeconds(m_frames[i]);
        }
        Destroy(gameObject);
    }
}
