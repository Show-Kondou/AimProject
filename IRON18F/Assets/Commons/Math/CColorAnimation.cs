//! CColorAnimation.cs
/*!
 * \details 2つの色でアニメーションさせる
 * \author  Shoki Tsuneyama
 * \date    2015/06/09　新規作成
 */
using UnityEngine;
using System.Collections;

namespace CaseStudy
{
    [RequireComponent(typeof(Renderer))]
    public class CColorAnimation : MonoBehaviour
    {
        [SerializeField]
        public Color   m_startColor = Color.black;
        [SerializeField]
        public Color m_endColor = Color.white;
        [SerializeField]
        public float m_time = 2;
        float m_timeCounter = 0;

        [Header("加算合成などを用いる物はチェック")]
        [SerializeField]
        bool m_isTintColor = false;

        Material m_material;
        bool m_isDown = false;

        Color m_defaultColor;

        public bool m_isAwake = true;
        bool        m_isPlay = false;
        
        //! クラスの初期化
        public void Awake()
        {
            m_material = transform.GetComponent<Renderer>().material;

            if (m_isTintColor)
                m_defaultColor = m_material.GetColor("_TintColor");
            else
                m_defaultColor = m_material.color;
            if (m_isAwake)
                m_isPlay = true;
        }   

        //! オブジェクトを毎フレーム更新
        public void Update()
        {
            if ( m_isPlay == false )
                    return;

            if (m_material == null)
            {
                Debug.Log("Materialがnullです");
                return;
            }
            m_timeCounter += Time.deltaTime;
            //タイムスケールが止まっているときはデフォルトタイムスケールを使用する
            if( m_time < m_timeCounter)
            {
                m_isDown ^= true;
                m_timeCounter = 0;
            }

            if(m_isTintColor)
            {
                if (m_isDown)
                    m_material.SetColor("_TintColor", Color.Lerp(m_startColor, m_endColor, m_timeCounter / m_time));
                else
                    m_material.SetColor("_TintColor", Color.Lerp(m_endColor, m_startColor, m_timeCounter / m_time));
            }
            else
            {
                if (m_isDown)
                    m_material.color = Color.Lerp(m_startColor, m_endColor, m_timeCounter / m_time);
                else
                    m_material.color = Color.Lerp(m_endColor, m_startColor, m_timeCounter / m_time);
            }
        }
        public void Run()
        {
            if (enabled)
                return;
            enabled = true;
            m_isPlay = true;

            m_timeCounter = 0;
            m_isDown = false;

            if (m_isTintColor)
                m_material.SetColor("_TintColor", m_startColor);
            else
                m_material.color = m_startColor;
        }

        public void Stop()
        {
            if (!enabled)
                return;
            enabled = false;

            if (m_isTintColor)
                m_material.SetColor("_TintColor",m_defaultColor);
            else
                m_material.color = m_defaultColor;
        }
    }
}
