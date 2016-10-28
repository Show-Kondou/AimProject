using UnityEngine;
using System.Collections;



//!  CTitleScene.cs	(シーン)
/*!
 * \details CTitleScene	タイトルシーン
 * \author  Shoki Tsuneyama
 * \date    2016/10/11	新規作成

 */
public class CTitleScene : CBaseScene
{
    [SerializeField]
    GameObject m_fadeEffect = null;
    Coroutine m_isFadenow = null;
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
        CSoundManager.Instance.PlayBGM(EAudioList.BGM_Title, CSoundManager.EFadeType.Out, 2);
        CSoundManager.Instance.ChangeVolBGM(0.2f, 1, true);
    }
    public void Update()
    {
        if (m_isFadenow != null)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CSoundManager.Instance.PlaySE(EAudioList.SE_Start);
            m_isFadenow=StartCoroutine(Out(SCENE_RAVEL.MATCHING, 3));
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            CSoundManager.Instance.PlaySE(EAudioList.SE_Start);
            m_isFadenow = StartCoroutine(Out(SCENE_RAVEL.GAME, 3));
        }
    }
    IEnumerator Out( SCENE_RAVEL next,float sec)
    {
        GameObject temp = Instantiate(m_fadeEffect);
        temp.transform.parent = transform;
        yield return new WaitForSeconds(sec);
        FadeManager.Instance.LoadLevel(next, 0.5f, null);
    }
}
