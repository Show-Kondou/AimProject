using UnityEngine;
using System.Collections;



//!  CResultScene.cs	(シーン)
/*!
 * \details CResultScene	リザルトシーン
 * \author  Shoki Tsuneyama
 * \date    2016/10/11	新規作成

 */
public class CResultScene : CBaseScene
{

    override public void FadeInBefore()
    {

    }
    override public void FadeInAfter()
    {

    }
    override public void FadeOutBefore()
    {

    }
    override public void FadeOutAfter()
    {

    }
    public void Start()
    {
        CSoundManager.Instance.PlayBGM(EAudioList.BGM_Result, CSoundManager.EFadeType.Cross, 2);

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FadeManager.Instance.LoadLevel(SCENE_RAVEL.TITLE, 0.5f, null);
        }
    }
}
