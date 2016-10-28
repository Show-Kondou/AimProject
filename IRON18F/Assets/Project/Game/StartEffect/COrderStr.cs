using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class COrderStr : MonoBehaviour
{
    [SerializeField][Header("テキスト")]
    string m_text="デフォルト";

    [SerializeField][Header("TextMeshデータ")]
    TextMesh m_textMesh=null;
    
    [SerializeField][Header("文字間隔")]
    Vector3 m_gap = new Vector3(2,0,0);

    [SerializeField][Header("中央揃え")]
    bool m_isCenter = true;

    [SerializeField][Header("生成フレーム(全部で何フレームか)")]
    float m_frame =1;

	void Start ()
    {
        if (m_textMesh == null)
        { 
            Debug.Log("TextMeshがnull");
            return;
        }
        if (m_frame <= 0)
        {
            for (int i = 0; i < m_text.Length; i++)
            {
                TextMesh temp = Instantiate(m_textMesh);
                temp.text = m_text.Substring(i, 1);
                temp.transform.parent = transform;
                temp.name = temp.text;
                if (m_isCenter)//中央揃え
                    temp.transform.AddXYZ(m_gap * (i - (m_text.Length / 2)) + transform.position);
                else
                    temp.transform.AddXYZ(m_gap * i + transform.position);
            }
        }
        else
            StartCoroutine(PushStr(m_frame / m_text.Length));
	}
    //フォント描画するやつ
	IEnumerator PushStr(float interval)
    {
        for (int i = 0; i < m_text.Length; i++)
        {
            yield return new WaitForSeconds(interval);
            TextMesh temp = Instantiate(m_textMesh);
            temp.text = m_text.Substring(i, 1);
            temp.transform.parent = transform;
            temp.name = temp.text;
            if (m_isCenter)//中央揃え
                temp.transform.AddXYZ(m_gap * (i - (m_text.Length / 2)) + transform.position);
            else
                temp.transform.AddXYZ(m_gap * i + transform.position);
        }
    }
}
