using UnityEngine;
using System.Collections;


//!  CGameScene.cs	(シーン)
/*!
 * \details CGameScene	ゲームシーン
 * \author  Shoki Tsuneyama
 * \date    2016/10/11	新規作成

 */
public class CGameScene : CBaseScene
{
    [SerializeField]
    GameObject m_startEffect =null;
    override public void FadeInBefore()
    {

    }
    override public void FadeInAfter()
    {
        Instantiate(m_startEffect);
    }
    override public void FadeOutBefore()
    {

    }
    override public void FadeOutAfter()
    {
    }
    public void Start()
    {
//        FadeManager.Instance.LoadScene(SCENE_RAVEL.GAME, 0.5f, null);
    }
    public void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FadeManager.Instance.LoadLevel(SCENE_RAVEL.RESULT, 0.5f, null);
        }
    }
}
