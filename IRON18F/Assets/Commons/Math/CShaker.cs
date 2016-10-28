//! CShaker.cs
/*!
 *  \details オブジェクトを揺らすクラス
 *  \author  Shoki Tsuneyama
 *  \date    2015/07/08　新規作成
 *  \date    2015/10/15  CaseStudyの機能を排除
 */
using UnityEngine;
using System.Collections;

//! オブジェクトを揺らすクラス
class CShaker : MonoBehaviour
{
    CInitializeValues m_initializeValues;
    float m_sinceShakeStart = 0;

    [System.Serializable]
    public class CInitializeValues
    {
        [SerializeField]
        public Vector3 m_shakeVector = Vector3.up;
        [SerializeField]
        public float m_frequency = 1;
        [SerializeField]
        public float m_stayTime = 1;
        [SerializeField]
        public float m_amplitude = 1;
        [SerializeField]
        public float m_attenuationRate = 1;

        public CInitializeValues Clone()
        {
            return MemberwiseClone() as CInitializeValues;
        }
    }

    //! 初期化
    public void Initialize(
        CInitializeValues initializeValues)
    {
        m_initializeValues = initializeValues.Clone();
    }

    //! 初期化
    void Awake()
    {
    }

    //! 更新
    void Update()
    {
        float deltaTime = Time.deltaTime;
        m_sinceShakeStart += deltaTime;
        m_initializeValues.m_amplitude *= (1 - m_initializeValues.m_attenuationRate * deltaTime);

        if (Mathf.Abs(m_initializeValues.m_amplitude) <= 0.01f) Destroy(this);
        if (m_sinceShakeStart > m_initializeValues.m_stayTime) Destroy(this);

        transform.AddXYZ(
            m_initializeValues.m_shakeVector *
            CMath.CalcAmplitudeOfSinWave(m_initializeValues.m_frequency, m_sinceShakeStart) *
            m_initializeValues.m_amplitude);
    }
}
