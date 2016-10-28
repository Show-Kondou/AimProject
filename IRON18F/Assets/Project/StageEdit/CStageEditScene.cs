using UnityEngine;
using System.Collections;



//!  CStageEditScene.cs	(シーン)
/*!
 * \details CStageEditScene	ステセレシーン
 * \author  Shoki Tsuneyama
 * \date    2016/10/12	新規作成

 */
public class CStageEditScene : CBaseScene
{
    [SerializeField][Header("グリッドプレファブ")]
    CStageGrid m_grid = null;
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
        m_grid = Instantiate(m_grid);
    }
    public void Update()
    {
        m_grid.SwitchDraw();
    }
}
