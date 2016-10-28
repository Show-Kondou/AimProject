//!  CLightCollider.cs
/*!
 * \details CLightCollider	光の当たり判定
 * \author  show kondou
 * \date    2016 10 / 13	新規作成
 *			2016 10 / 17	処理軽減

 */
using UnityEngine;
using System.Collections.Generic;



//!  CLightCollider.cs
/*!
 * \details CLightCollider	光の当たり判定クラス
 * \author  Shoki Tsuneyama
 * \date    2016 10 / 13	新規作成

 */
public class CLightCollider : MonoBehaviour
{
    // ===== 構造体 =====
    public class LightChara<T>
    {
        public T _instance;     // インスタンス
        public bool islightIn;  // 光の範囲に入っている
    };



    // ===== 定数 =====
    private const float GROUND_POS_Y = 0.1F;
    private const int HUMAN_NUM = 3;
    private const int UPDATE_FRAME = 10;
    /* タグ */
    private const string TAG_HUMAN = "Human";
	private const string TAG_GHOST = "Ghost";
	private const string TAG_DUMMY_G = "DummyGhost";

	private const string TAG_STAGE = "Stage";




    // ===== メンバ変数 =====
    private Ray m_ray;
    private bool m_isNearWall;      // 近くに壁があるか判定
    private List<LightChara<CHuman>>		m_humanList = new List<LightChara<CHuman>>();
	private List<LightChara<DummyGhost>>    m_dymmyGhostList = new List<LightChara<DummyGhost>>();
    private LightChara<GhostMain>			m_ghost = new LightChara<GhostMain>();



    // ===== メンバ関数 =====

    /*!  Start()
	*!   \details	オブジェクト生成時呼び出し関数
	*!
	*!   \return	none
	*/
    void Start()
    {
        Init();
        InitCharaData();
    }



    /*!  Update()
	*!   \details	更新時呼び出し関数
	*!
	*!   \return	none
	*/
    void Update()
    {
        HitCheckCharacter();
    }



    /*!  Init()
	*!   \details	初期化関数
	*!
	*!   \return	none
	*/
    private void Init()
    {
        var pos = transform.position;
        pos.y = GROUND_POS_Y;
        m_ray.origin = pos;
    }



	/*!  InitCharData()
	*!   \details	説明
	*!
	*!   \return	戻り値
	*/
	private void InitCharaData() {
		GameObject[] humans = GameObject.FindGameObjectsWithTag( TAG_HUMAN );
		/* Human */
		for( int i = 0; i < humans.Length; i++ ) {
			LightChara<CHuman> lightHuman = new LightChara<CHuman>();
			lightHuman.islightIn = false;
			lightHuman._instance = humans[i].GetComponent<CHuman>();
			m_humanList.Add( lightHuman );
		}

		/* ダミーお化け */
		GameObject[] dummyGhost = GameObject.FindGameObjectsWithTag( TAG_DUMMY_G );
		for( int i = 0; i < dummyGhost.Length; i ++ ) {
			LightChara<DummyGhost> lightDummyGhost = new LightChara<DummyGhost>();
			lightDummyGhost.islightIn = false;
			lightDummyGhost._instance = dummyGhost[i].GetComponent<DummyGhost>();
			m_dymmyGhostList.Add( lightDummyGhost );
		}
	//	Debug.Log( "ダミーお化けの数：" + dummyGhost.Length );


		/* Ghost */
		m_ghost.islightIn = false;
        GameObject tmp_gameobj = GameObject.FindGameObjectWithTag(TAG_GHOST);
        if(tmp_gameobj!=null)
        {
            if( null == tmp_gameobj.GetComponent<GhostMain>() ) {
			    Debug.Log( "ghost is Null" );
		    } else {
			    m_ghost._instance = GameObject.FindGameObjectWithTag( TAG_GHOST ).GetComponent<GhostMain>();
		    }
        }
		

		
	}



	/*!  OnTriggerStay( Collider )
	*!   \details	当たっている間呼ばれるメソッド
	*!
	*!   \return	戻り値
	*/
    void OnTriggerEnter(Collider coll) {
        // キャラクターorステージに当たったか
        bool isHitChar = TAG_HUMAN  == coll.tag ||
                        TAG_GHOST   == coll.tag ||
						TAG_DUMMY_G == coll.tag;
        // 壁判定
        m_isNearWall = TAG_STAGE == coll.tag;
        //Debug.Log(coll.transform.name);

        if (!isHitChar) return;

		// ヒットしたオブジェクトを保存
        SortingObjct(coll);
    }



    /*!  SortingObjct( Collider )
	*!   \details	キャラクター選別
	*!
	*!   \return	noen
	*/
    private void SortingObjct(Collider coll) {
        // タグで選別
        switch (coll.tag)
        {
            case TAG_HUMAN:
                for (int i = 0; i < m_humanList.Count; i++)
                {
                    var human = m_humanList[i];
                    if (coll.name != human._instance.name) continue;
                    human.islightIn = true;
                    human._instance.LightOn = true;
                    m_humanList[i] = human;
                }
                break;

            case TAG_GHOST:
                m_ghost.islightIn = true;
                m_ghost._instance.m_viewStatus = GhostMain.GHOTS_VIEW_STATUS.VISIBLE;
                break;

		case TAG_DUMMY_G:
			Debug.Log( "ダミーお化け？\nName：" + coll.name );
			for( int i = 0; i < m_dymmyGhostList.Count; i++ ) {
				bool isNameCheck = coll.name == m_dymmyGhostList[i]._instance.name;
				bool isAct = m_dymmyGhostList[i]._instance.IsAct;

				if( !(isNameCheck && isAct) ) {
					continue;
				}
				Debug.Log( "照らし始め" );
				m_dymmyGhostList[i].islightIn = true;
				m_dymmyGhostList[i]._instance.m_viewStatus = DummyGhost.GHOTS_VIEW_STATUS.VISIBLE;
			}
			break;
		}
	}



	/*!  OnTriggerExit( Collider )
	*!   \details	光から出たら呼ばれるメソッド
	*!
	*!   \return	戻り値
	*/
	void OnTriggerExit(Collider coll) {
		// 光から出たら
		bool isHitChar = TAG_HUMAN == coll.tag ||
						TAG_GHOST == coll.tag ||
						TAG_DUMMY_G == coll.tag;
		if (!isHitChar ) return;

        // --- 各オブジェクトのイベント ---
        switch (coll.tag)
        {
            /* 人 */
            case TAG_HUMAN:
                for (int i = 0; i < m_humanList.Count; i++)
                {
                    var human = m_humanList[i];
                    if (coll.name != human._instance.name) continue;
                    human.islightIn = false;
                    human._instance.LightOn = false;
                    m_humanList[i] = human;
                    break;
                }
                break;
            /* お化け */
            case TAG_GHOST:
                /* お化けを透明に */
                // Debug.Log( "お化けノットヒットライト" );
                m_ghost._instance.m_pStateMachine.ChangeGlobalState(CGhostState_Main.Instance());
                m_ghost.islightIn = false;
                break;
		case TAG_DUMMY_G:
			for( int i = 0; i < m_dymmyGhostList.Count; i ++ ) {
				bool isNameCheck = coll.name == m_dymmyGhostList[i]._instance.name;
				bool isAct = m_dymmyGhostList[i]._instance.IsAct;

				if( !(isNameCheck && isAct) ) {
					continue;
				}
				Debug.Log( "照らし終わり" );
				m_dymmyGhostList[i].islightIn = false;
				m_dymmyGhostList[i]._instance.m_viewStatus = DummyGhost.GHOTS_VIEW_STATUS.INVISIBLE;
			}
			break;
        }
    }



    /*!  HitCheckCharacter( Collider )
	*!   \details	照らされているかチェック
	*!
	*!   \return	戻り値
	*/
    private void HitCheckCharacter() {
        /* 処理の軽量化 */
        bool isUpdate = Time.frameCount % UPDATE_FRAME == 0;
        if (!isUpdate) return;

        // --- 各オブジェクトのイベント ---
        /* 人 */
        for (int i = 0; i < m_humanList.Count; i++)
        {
            var human = m_humanList[i];
            if (!human.islightIn) continue;
            // Debug.Log( m_humanList[i]._instance.name + "のチェック" );
            if (!RayCheck(human._instance.transform))
            {
                //	Debug.Log( human._instance.name + "は照らされていない。" );
                human._instance.LightOn = false;
                m_humanList[i] = human;
                continue;
            }
            // 人のイベント
            // Debug.Log( human._instance.name + "は照らされている。" );
            human._instance.LightOn = true;
            m_humanList[i] = human;

        }

		/* ダミーお化け */
		for( int i = 0; i < m_dymmyGhostList.Count; i ++ ) {
			var dummy = m_dymmyGhostList[i];
			// Debug.Log( "ダミーお化け\nName：" + dummy._instance.name + "\n光に入っているか：" + dummy.islightIn
			// 	+ "\n変数：" + m_dymmyGhostList[i]._instance.m_viewStatus );
			if( !dummy.islightIn ) continue;
			if( !RayCheck( dummy._instance.transform ) ) {
				dummy._instance.m_viewStatus = DummyGhost.GHOTS_VIEW_STATUS.INVISIBLE;
				m_dymmyGhostList[i] = dummy;
				continue;
			}
			dummy._instance.m_viewStatus = DummyGhost.GHOTS_VIEW_STATUS.VISIBLE;
			m_dymmyGhostList[i] = dummy;
		}

        //* お化け */
        if (!m_ghost.islightIn) return;
        if (!m_ghost._instance) return;
        if (!RayCheck(m_ghost._instance.transform))
        {
            m_ghost._instance.m_viewStatus = GhostMain.GHOTS_VIEW_STATUS.INVISIBLE;
        } else {
            m_ghost._instance.m_pStateMachine.ChangeGlobalState(CGhostState_Slow.Instance());

        }
        // お化けのイベント
        // Debug.Log( m_ghost._instance.name + "は照らされている。" );

    }



	/*!  RayCheck( Collider )
	*!   \details	レイで照らしているか判定
	*!
	*!   \return	bool 照らしていたらtrue
	*/
	private bool RayCheck( Transform chara  ) {
		// 壁が無いよ
		//Debug.Log( m_isNearWall );
		//if( m_isNearWall )	return true;

		var dir = chara.position;
		/* レイの方向設定 */
		dir -= transform.position;
		dir.y = GROUND_POS_Y;
		m_ray.direction = dir;

		/* レイを飛ばす */
		RaycastHit[] hits = Physics.RaycastAll( m_ray );

		/* レイに当たった物を検索 */
		float charaDistance = Vector3.Distance( transform.position, chara.position );
		float stageDistance = 0.0F;
		/* 一番近い壁を検索 */
		for( int i = 0; i < hits.Length; i ++ ) {
			if( TAG_STAGE != hits[i].transform.tag ) continue;
			stageDistance = Vector3.Distance( transform.position, hits[i].transform.position );
			break;
		}
		// Debug.Log( "壁との距離" + stageDistance + "\nキャラのと距離" + charaDistance );
		/* ヒット判定 */
		if( 0.0F >= stageDistance ) {
			/* 壁なし */
			// Debug.Log( "壁なし" );
			return true;
		}

		if( charaDistance <= stageDistance ) {
			/* キャラクターの方が近いので照らされている */
			// Debug.Log( "キャラクターの方が近い" );
			return true;
		} else {
			/* 壁のほうが近いので照らされていない */
			// Debug.Log( "壁のほうが近い" );
			return false;
		}
	}
}