//!  CCandle.cs
/*!
 * \details CCandle	説明
 * \author  show kondou
 * \date    2016 10 / 12	新規作成

 */
using UnityEngine;
using System.Collections;



//!  CCandle.cs
/*!
 * \details CCandle	説明
 * \author  show Kondu
 * \date    2016 10 / 12	新規作成
 * \date    2016 10 / 13	寿命を設定
 *			2016 10 / 26	音とパーティクル追加
 */
public class CCandle : MonoBehaviour {
	// ===== 定数 =====
	/* 計算補正定数 */
	private const float CORRECTION          = 6.5F / 160.0F;    // 光の範囲とコライダーの範囲の補正値
	private const float MIN_ANGLE			= 50.0F;

	/* タグ */
	private const string TAG_ALTAR			= "Altar";
	private const string MODEL_NAME         = "Model";
	/* コンポーネントネーム */
	private const string SPOT_LIGHT_NAME    = "SpotLight";
	private const string POINT_LIGHT_NAME   = "PointLight";
	private const string COLLIDER_NAME      = "Collider";
	private const string PARTICLE_NAME      = "FireParticle";
	private const string SMOKE_NAME			= "SmokeParticle";
	private const string SHINE_NAME         = "ShineParticle";
	/* シーンネーム */
	private const string TITLE_SCENE        = "Title";
	/* メタデータ */
	private const float LIGHT_UP_AREA       = 50.0F;            // 光の強さのメタデータ
	private const float SUB_LIGHT           = 3.0F;             // 光の減少量のメタデータ



	// ===== メンバ変数 =====
	/* Candle用変数 */
	public  float m_lightUpArea;        // 光の強さ->寿命(単位不明)
	public  float m_subLight;           // 光の減少量(単位不明)
	private float m_maxLightUp;         // 光の強さの最大
	private float m_maxRadius;          // 最大の半径
	private bool  m_isFire;              // 火が点火しているか
	private bool  m_isStock;             // 持たれている
    public  bool  isServer;             // サーバーと更新しているか
	public  bool  m_isPutAltar;			// 祭壇に設置してある


	/* コンポーネント */
	private Light           m_spotLight;	// ライトコンポーネント
	private Light			m_pointLight;	// ポイントライト
	private SphereCollider	m_lightColl;	// コリジョンコンポーネント
	private CapsuleCollider m_modelColl;	// モデルのコリジョン
	private MeshRenderer    m_mesh;			// メッシュ
	private ParticleSystem  m_particle;		// 火のパーティクル
	private ParticleSystem  m_smoke;		// 鎮火パーティクル
	private ParticleSystem  m_shine;		// 所得促進エフェクト



	// ====== アクセサー =====
	public bool IsFire {
		get { return m_isFire; }
		set { m_isFire = value; }
	}

	public bool IsStock {
		get { return m_isStock; }
		set { m_isStock = value; }
	}



	// ===== メンバ関数 =====
	/*!  Start()
	*!   \details	オブジェクト生成時呼び出し関数
	*!
	*!   \return	none
	*/
	void Start() {
		Init();
	}



	/*!  Update()
	*!   \details	更新時呼び出し関数
	*!
	*!   \return	none
	*/
	void Update() {
		AbateCandle();
	}



	/*!  OnValidate()
	*!   \details	Inspector変更時イベント
	*!
	*!   \return	none
	*/
	private void OnValidate() {
		// 範囲外値の削除
		m_lightUpArea	= Mathf.Clamp( m_lightUpArea, 0.0F, 500.0F );
		m_subLight		= Mathf.Clamp( m_subLight,	  0.0F, m_lightUpArea );
	}



	/*!  Init()
	*!   \details	初期化関数
	*!
	*!   \return	none
	*/
	private void Init() {
		// メンバー初期化
		m_isFire = false;
		m_isStock = false;
		m_maxLightUp = m_lightUpArea;
		InitModel();
		InitParticle();
		InitLight();
		InitCollider();
		// InitDebug();    // デバッグ用初期化
		InitException();
	}



	/*!  InitDebug()
	*!   \details	初期化関数
	*!
	*!   \return	none
	*/
	private void InitDebug() {
		/* メタデータで初期化 */
		// m_lightUpArea = LIGHT_UP_AREA;
		// m_subLight = SUB_LIGHT;
		Debug.Log( "[" + transform.name + "]生成\n" );
		//PutCandle( transform.position );    // テスト
	}



	/*!  InitModel()
	*!   \details	メッシュの初期化
	*!
	*!   \return	none
	*/
	private void InitModel() {
		// コンポーネント取得
		m_mesh = transform.FindChild( MODEL_NAME ).GetComponent<MeshRenderer>();
	}



	/*!  InitParticle()
	*!   \details	パーティクル初期化
	*!
	*!   \return	none
	*/
	private void InitParticle() {
		// コンポーネント取得
		m_particle = transform.FindChild( PARTICLE_NAME ).GetComponent<ParticleSystem>();
		m_smoke = transform.FindChild( SMOKE_NAME ).GetComponent<ParticleSystem>();
		m_shine = transform.FindChild( SHINE_NAME ).GetComponent<ParticleSystem>();

		// 初期化
		m_particle.gameObject.SetActive( false );
		m_smoke.gameObject.SetActive( false );
	}



	/*!  InitLight()
	*!   \details	ライトの初期化
	*!
	*!   \return	none
	*/
	private void InitLight() {
		// コンポーネント取得
		m_spotLight = gameObject.transform.FindChild( SPOT_LIGHT_NAME ).GetComponent<Light>();
		m_pointLight = gameObject.transform.FindChild( POINT_LIGHT_NAME ).GetComponent<Light>();
		
		// 初期化
		m_spotLight.spotAngle = m_lightUpArea;
		m_spotLight.gameObject.SetActive( false );
		m_pointLight.gameObject.SetActive( false );
	}



	/*!  InitCollider()
	*!   \details	当たり判定の初期化
	*!
	*!   \return	none
	*/
	private void InitCollider() {
		m_modelColl = GetComponent<CapsuleCollider>();
		m_lightColl = gameObject.transform.FindChild( COLLIDER_NAME ).GetComponent<SphereCollider>();

		// 初期化
		m_maxRadius = m_lightColl.radius;
		// m_lightColl.radius = m_maxRadius * m_lightUpArea / m_maxLightUp;
		m_lightColl.radius = (m_lightUpArea - MIN_ANGLE) / (m_maxLightUp - MIN_ANGLE) * 0.25F * 10.0F + 0.5F;
		m_lightColl.gameObject.SetActive( false );
	}



	/*!  InitException()
	*!   \details	当たり判定の初期化
	*!
	*!   \return	none
	*/
	private void InitException() {
		// 例外処理タイトルシーン
		if( TITLE_SCENE != gameObject.scene.name )	return;
		m_subLight = 0.0F;
		PutCandle( transform.position, null );
	}



	/*!  AbateCandle()
	*!   \details	ロウソクの減衰
	*!
	*!   \return	none
	*/
	private void AbateCandle() {

		m_mesh.material.color += new Color( 0,0,0, 0.01F );

		// 例外処理
		if( !m_isFire )
			return;

		if( !m_lightColl.gameObject.activeInHierarchy ) {
			SetRenderer( true );
		}

		// 生存確認
		bool isLife = MIN_ANGLE >= m_lightUpArea;
		if( isLife ) {
			EndCandle();
			return;
		}
		// 光の強さを弱める
		m_lightUpArea -= m_subLight * Time.deltaTime;
		// Debug.Log( (m_lightUpArea - MIN_ANGLE) / (m_maxLightUp - MIN_ANGLE) * 100.0F + "％" );
		/* 各コンポーネントへ値の反映 */
		m_spotLight.spotAngle = m_lightUpArea;
		//m_lightColl.radius = m_maxRadius * m_lightUpArea / m_maxLightUp;
		m_lightColl.radius = (m_lightUpArea - MIN_ANGLE) / (m_maxLightUp - MIN_ANGLE) * 0.25F * 10.0F + 0.5F;


	}



	/*!  EndCandle()
	*!   \details	キャンドルの終了関数
	*!
	*!   \return	noen
	*/
	private void EndCandle() {
		SetRenderer( false );
		m_mesh.gameObject.SetActive( true );
		if( !m_smoke.gameObject.activeSelf ) {
			// Debug.Log( "鎮火パーティクル" );
			CSoundManager.Instance.PlaySE( EAudioList.SE_CandleVanish );	// 鎮火音
			m_smoke.gameObject.SetActive( true );
			m_smoke.Play();
		}
		if( !m_smoke.isPlaying ) {
			Destroy( gameObject );
		}
	}



	/*!  PutCandle( Vector3 )
	*!   \details	ロウソクの設置
	*!
	*!   \return	treu  … 祭壇
	*!				false … ノーマル
	*/
	public void PutCandle( Vector3 pos, CHuman human ) {
		SetRenderer( true );
		// m_smoke.gameObject.SetActive( false );
		pos.y = 0.1F;
		transform.position = pos;   // ポジション設置
		m_isFire = true;            // 点火
		if( human ) {
			human.ShumanScore.setAlterCandle += 1;  // スコア追加 +1
		}
		if( human.IsLocal ) {
			CSoundManager.Instance.PlaySE( EAudioList.SE_CandlePut );       // 置く音
			CSoundManager.Instance.PlaySE( EAudioList.SE_CandleIgnition );  // 着火音
		}
	}



	/*!  GetCandle()
	*!   \details	説明
	*!
	*!   \return	戻り値
	*/
	public void GetCandle() {
        IsStock = true;
        transform.position = new Vector3(0, -300, 0);
		SetRenderer( false );
		// m_smoke.gameObject.SetActive( false );
		m_shine.gameObject.SetActive( false );
	}



	/*!  SetRenderer( bool )
	*!   \details	描画
	*!
	*!   \return	戻り値
	*/
	private void SetRenderer( bool bFlag ) {
		m_mesh.gameObject.SetActive( bFlag );
		m_lightColl.gameObject.SetActive( bFlag );
		m_modelColl.enabled = bFlag;
		m_spotLight.gameObject.SetActive( bFlag );
		m_pointLight.gameObject.SetActive( bFlag );
		m_particle.gameObject.SetActive( bFlag );
		// m_smoke.gameObject.SetActive( bFlag );
	}



	/*!  OnTriggerEnter( Collider )
	*!   \details	当たり判定
	*!
	*!   \return	noen
	*/
	// 祭壇の設置判定
	void OnTriggerEnter( Collider coll ) {
		SetAltar( coll );
	}



	/*!  SetAltar( Collider )
	*!   \details	祭壇にロウソクを設置
	*!
	*!   \return	none
	*/
	private void SetAltar( Collider coll ) {
		// 例外処理
		if( TAG_ALTAR != coll.tag )	return;
        var altar = coll.GetComponent<Altar>();
        if ( altar.isLight ) {
            // Debug.Log("祭壇にもうあった");
            return;
        }


        if ( isServer ) {
            //Debug.Log("ロウソクが祭壇に置かれた");
            altar.OnLight();
            transform.position = altar.transform.position;
            transform.SetY(altar.transform.position.y + 1.5F);
        }
		m_isFire = false;
		m_isPutAltar = true;
        m_modelColl.enabled = false;
		m_mesh.gameObject.SetActive( true );
        m_spotLight.gameObject.SetActive(false);
        m_pointLight.gameObject.SetActive(false);
        m_particle.gameObject.SetActive(false);

	}
}
