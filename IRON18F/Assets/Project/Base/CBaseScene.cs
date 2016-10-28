using UnityEngine;
using System.Collections;



//!  CBaseScene.cs
/*!
 * \details CBaseScene	シーンの親クラス
 * \author  Shoki Tsuneyama
 * \date    2016/10/11	新規作成

 */
public class CBaseScene : MonoBehaviour
{
    virtual public void FadeInBefore()
    {

    }
    virtual public void FadeInAfter(){}
    virtual public void FadeOutBefore(){}
    virtual public void FadeOutAfter(){}

    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
