//!  CGameEnd.cs
/*!
 * \details 	CGameEnd
 * \author  show kondou
 * \date    2016 10 / 28	新規作成
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



//!  CGameEnd.cs
/*!
 * \details CGameEnd	説明
 * \author  show Kondu
 * \date    2016 10 / 28	新規作成
 */
public class CGameEnd : MonoBehaviour {
	// ===== 定数 =====
	private const string TAG_HUMAN = "Human";		// 人のタグ
	private const string ALTAR_MGR = "AltarMgr";	// アルターマネージャー



	// ===== メンバ変数 =====
	private List<CHuman>    m_humanList = new List<CHuman>();    // 人のインスタンス
	private AltarManager    m_altarMgr;     // 祭壇Mgrインスタン
	private int             m_HumanNum;     // 人の数
	public bool m_isInit = false;			// 初期化判定



	// ===== メンバ関数 =====
	/*!  Start()
	*!   \details	オブジェクト生成時呼び出し関数
	*!
	*!   \return	none
	*/
	void Start () {
		Init();
	}



	/*!  Update()
	*!   \details	更新時呼び出し関数
	*!
	*!   \return	none
	*/
	void Update () {
		Init();

		CheckHuman();
	}



	/*!  Init()
	*!   \details	初期化
	*!
	*!   \return	none
	*/
	private void Init() {
		// インスタン所得
		GameObject[] humans = GameObject.FindGameObjectsWithTag( TAG_HUMAN );
		GameObject altarMgr = GameObject.Find( ALTAR_MGR );

		// インスタンス保存
		for( int i = 0; i < humans.Length; i ++ ) {
			m_humanList.Add( humans[i].GetComponent<CHuman>() );
		}
		m_altarMgr = altarMgr.GetComponent<AltarManager>();

		m_HumanNum = m_humanList.Count;

		// 取得ジェック
		// Debug.Log( "人の数：" + m_HumanNum );
		// Debug.Log( "祭壇：" + m_altarMgr );
		m_isInit = 0 < m_humanList.Count;
	}



	/*!  CheckHuman()
	*!   \details	人のチェック
	*!
	*!   \return	none
	*/
	private void CheckHuman() {
		if( !m_isInit )
			return;
		int deadNum = 0;
		foreach( var i in m_humanList ) {
			bool isDead = i.Doa();
			if( isDead ) {
				deadNum ++;
			}
		}
		//
		bool isGhostWin = m_HumanNum == deadNum;
		if( isGhostWin ) {
			// ゴースト勝利イベント
			WinGhost();
		}
	}



	/*!  WinHuman()
	*!   \details	人のチェック
	*!
	*!   \return	none
	*/
	public void WinHuman() {
		Debug.Log( "人の勝ち" );
		FadeManager.Instance.LoadLevel( SCENE_RAVEL.RESULT, 0.5F, null );
	}



	/*!  WinHuman()
	*!   \details	人のチェック
	*!
	*!   \return	none
	*/
	public void WinGhost() {
		Debug.Log( "お化けの勝ち" );
		FadeManager.Instance.LoadLevel( SCENE_RAVEL.RESULT, 0.5F, null );
	}


}
