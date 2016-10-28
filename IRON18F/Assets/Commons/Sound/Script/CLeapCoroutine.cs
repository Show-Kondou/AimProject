using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public delegate void FLeapCoroutine(float value);
//!  CLeapCoroutine.cs
/*!
 * \details CLeapCoroutine	コルーチンで変更する数値データの提携処理がめんどくさいから
 *                          テンプレートでもいいけど、用途があんまりないのでfloat固定
 * \author  Shoki Tsuneyama
 * \date    2016/10/22	新規作成

 */
public class CLeapCoroutine : MonoBehaviour
{
    //ここに内容がはいる
    List<Coroutine> m_corFlg = new List<Coroutine>();
    List<FLeapCoroutine> m_contents = new List<FLeapCoroutine>();
    bool m_isDeltaTime =true;

    public void Add(FLeapCoroutine contents,int index)
    {
        m_contents.Add(contents);
        m_corFlg.Add( null);
    }
    public void StartLeap(int index,float sec, bool isOverWrite)
    {
        //リープ処理を上書き
        if (isOverWrite || m_contents == null)
        {
            if (m_corFlg[index] != null)
                StopCoroutine(m_corFlg[index]); //上書き処理
            m_corFlg[index] = StartCoroutine(LeapCoroutine(m_contents[index], sec));
        }
    }

    IEnumerator LeapCoroutine(FLeapCoroutine func,float sec)
    {
        for (float m_timer = 0; m_timer < sec; m_timer += (m_isDeltaTime) ? Time.deltaTime : 1/60 )
        {
            yield return 0;
            func(m_timer / sec);
        }
    }
}
