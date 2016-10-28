//!  CCandy.cs
/*!
 * \details 	CCandy
 * \author  show kondou
 * \date    2016 10 / 17	新規作成

 */
using UnityEngine;
using System.Collections;



//!  CCandy.cs
/*!
 * \details CCandy
 * \author  show Kondou
 * \date    2016 10 / 17	新規作成
 *			2016 10 / 26	音とパーティクル追加
 */
public class CCandy : MonoBehaviour {
	// ===== 定数 =====
	/* タグ */
	private const string TAG_HUMAN = "Human";
	private const string TAG_GHOST = "Ghost";
	private const string TAG_STAGE = "Stage";
	/* デモデータ */
	private const float SPEED_CORRECION = 1.0F;


	// ===== メンバ変数 =====
	/* キャンディ用変数 */
	public  float	m_speed;		// スピード
	public  float	m_maxdDstance;  // 最大距離
	public  bool	m_isShot;		// 発射中か
	private float	m_distance;		// 残りの移動距離
	private Vector3 m_vector;		// 発射方向
	/* コンポーネント */
	private SphereCollider m_coll;		// ボックス（形が変わったら変更）
	private GameObject m_mesh;		// メッシュレンダー



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
		CheckLimit();
		MoveCandy();
	}



	/*!  Init()
	*!   \details	初期化関数
	*!
	*!   \return	none
	*/
	private void Init() {
		m_isShot = false;
		m_speed /= SPEED_CORRECION;
		m_distance = m_maxdDstance;
		// SetRender( false );
	}



	/*!  ShotCandy( Vector3, Vector3 )
	*!   \details	キャンディ発射
	*!
	*!   \return	none
	*/
	public void ShotCandy( Vector3 pos, Vector3 vec ) {
		if( m_isShot )	return;
		gameObject.SetActive( true );
		m_coll = GetComponent<SphereCollider>();
		m_mesh = GetComponent<MeshRenderer>().gameObject;
		transform.forward = vec;
		

		Debug.Log( "キャンディ発射" );
		transform.parent = null;

		SetRender( true );

		m_isShot = true;
		transform.position = pos;
		m_vector = vec;
		m_distance = m_maxdDstance;
		CSoundManager.Instance.PlaySE( EAudioList.SE_CandyThrow );
	}



	/*!  GetCandy()
	*!   \details	キャンディを拾う
	*!
	*!   \return	拾えたか
	*/
	public bool GetCandy() {
		if( m_isShot )	return false;
		SetRender( false );
		return true;
	}




	/*!  MoveCandy()
	*!   \details	キャンディ移動処理
	*!
	*!   \return	none
	*/
	public void MoveCandy() {
		if( !m_isShot )	return;
		Vector3 addPos = m_vector * m_speed * Time.deltaTime;
		transform.position += addPos;
		m_distance -= Vector3.Distance( Vector3.zero, addPos );

	}



	/*!  CheckLimit()
	*!   \details	移動距離が限界かチェックする
	*!
	*!   \return	none
	*/
	private void CheckLimit() {
		if( 0 <= m_distance )	return;
		m_distance = m_maxdDstance;
		m_isShot = false;
	}




	/*!  SetRender( bool )
	*!   \details	当たり判定と見た目のアクティビティ設定
	*!
	*!   \return	none
	*/
	private void SetRender( bool isFlag ) {
		m_coll.enabled = isFlag;
		m_mesh.SetActive( isFlag );
	}



	/*!  HitStage()
	*!   \details	壁にあったら
	*!
	*!   \return	none
	*/
	private void HitStage() {
		m_isShot = false;
	}




	/*!  OnCollisionEnter( Collision )
	*!   \details	当たった
	*!
	*!   \return	戻り値
	*/
	void OnTriggerEnter( Collider coll ) {

		switch( coll.tag ) {
		/* 人 */
		case TAG_HUMAN:
			break;
		/* お化け */
		case TAG_GHOST:
			coll.GetComponent<GhostMain>().HitCandy();
			CSoundManager.Instance.PlaySE( EAudioList.SE_CandyHit );
            Destroy(gameObject);
			break;
		/* ステージ */
		case TAG_STAGE:
			HitStage();
			CSoundManager.Instance.PlaySE( EAudioList.SE_CandyHit );
			break;
		}
	}
}
