
//!  CSingleton.cs
/*!
 * \details CSingleton	継承したクラスがシングルトンになる
 *                      用途しては、スコアのマネージャーとか、かも
 * \author  Shoki Tsuneyama
 * \date    2016/10/05	新規作成

 */
public class CSingleton<T> where T : CSingleton<T>,new()
{
    protected static readonly T m_instance = new T();

    protected CSingleton(){}
    virtual protected void Awake()
    {

    }
    /*!  Instance
    *!   \details	インスタンスを取得
    *!
    *!   \return	インスタンス
    */
    public static T Instance
    {
        get
        {
            m_instance.Awake();
            return m_instance;
        }
    }   

}
