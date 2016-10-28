using UnityEngine;
using System.Collections;

//!  CMonoBehaviourSingleton.cs
/*!
 * \details CMonoBehaviourSingleton	継承したクラスがシングルトンになる。
 * \author  Shoki Tsuneyama
 * \date    10/04	新規作成

 */
public class CMonoBehaviourSingleton<T> : MonoBehaviour where T : CMonoBehaviourSingleton<T>
{
    protected static T m_instance =null;      // 中身

    public bool m_isInstance = false;   // 

    /*!  Instance
    *!   \details	インスタンスする
    */
    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType(typeof(T)) as T;
                if (m_instance == null)
                {
                    Debug.LogWarning(typeof(T) + "is nothing");
                }
            }

            return m_instance;
        }
    }
    
    /*!  Awake
    *!   \details	初期化
    *!
    */
    protected virtual void Awake()
    {
        CheckInstance();
    }

    /*!  CheckInstance
    *!   \details	インスタンスの存在をチェック
    *!
    *!   \return	インスタンスならtrue
    */
    protected bool CheckInstance()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        Debug.LogWarning(this.gameObject.name +
                  ": 2つ以上のシングルトンオブジェクトを生成しようとしました。");
        Destroy(this);
        return false;
    }
}