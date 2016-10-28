using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
/// <summary>
/// CGhostMain.cs
///  * \details CGhostMain	鬼のメイン関数
///  * \author  Kenbun Kimoto
///  * \date    ファイルの作成日	新規作成
///</summary>
public class GhostMain : MonoBehaviour
{

    //鬼の視覚状態
    public enum GHOTS_VIEW_STATUS{
        VISIBLE,
        INVISIBLE
    };

    //鬼のステータス
    public enum GHOTS_STATUS
    {
        NORMAL,
        DASH,
        STAN

    }
    public SGhostScore GhostScore;

    public Animator m_animator;

    public CStateMachine<GhostMain> m_pStateMachine;

    public GHOTS_VIEW_STATUS m_viewStatus;

    public GHOTS_STATUS m_Status;

    public int m_nowDunmmyNum;

    public GameObject atkParticle;

    private ParticleSystem m_movepatincle;
    public ParticleSystem Movepatincle
    {
        get { return m_movepatincle; }
        set { m_movepatincle = value; }
    }
    //ノーマルスピード
    [SerializeField]
    private float m_normalSpeed;

    public float NomalSpeed {
        get { return m_normalSpeed; }
    }

    //スロー状態スピード
    [SerializeField]
    private float m_slowSpeed;
    public float SlowSpeed
    {
        get { return m_slowSpeed; }
    }

    //ダッシュ中スピード
    [SerializeField]
    private float m_dashSpeed;
    public float DashSpeed
    {
        get { return m_dashSpeed; }
    }

    //攻撃中スピード
    [SerializeField]
    private float m_atkSpeed;
    public float AtkSpeed
    {
        get { return m_atkSpeed; }
    }

    [SerializeField]
    private float m_showBuff;

    public float ShowBuff
    {
        get { return m_showBuff; }
        set { m_showBuff = value; }
    }

    private Vector3 m_moveDirection;

    public Vector3 MoveDirection {
        get { return m_moveDirection; }
        set { m_moveDirection = value; }
    }


    private bool m_isAttack;
    public bool IsAttack
    {
        get { return m_isAttack; }
        set { m_isAttack = value; }
    }

    //スタン時間
    public float m_stanInterval;

    //最大Dummy数
    public int m_maxDunmmyNum;


    private bool m_isLocalPlayer;
    public bool IsLocalPlayer{
        get { return m_isLocalPlayer; }
        set { m_isLocalPlayer = value; }
    }

    public void SetScore()
    {
        CGameScore.Instance.SetGhostScore(this);
    }

    private float m_nowTime;

    public GameObject []dGhost;



    // Use this for initialization
    void Start () {
        m_showBuff = 1;
        m_viewStatus = GHOTS_VIEW_STATUS.INVISIBLE;
        m_pStateMachine = new CStateMachine<GhostMain>(this);
        m_animator = this.GetComponent<Animator>();
        m_pStateMachine.SetCurrentState(CGhostState_Main.Instance());
        m_pStateMachine.SetGlobalStateState(CGhostState_Wait.Instance());
        m_nowDunmmyNum = m_maxDunmmyNum;
        dGhost = new GameObject[3];
        GhostScore = new SGhostScore();
        GhostScore.hitCandy = 0;
        GhostScore.knockOut = 0;
        int count = 1;
        foreach (Transform i in this.transform)
        {
            if (i.name == ("ghostkodomo_" + count.ToString()))
            {
                dGhost[count - 1] = i.gameObject;
                count++;
            }
            if(i.name=="GhostMeshParticle")
            {
                m_movepatincle = i.GetComponent<ParticleSystem>();
            }
        }

	}
	
	// Update is called once per frame
	void Update () {


        if ( m_viewStatus == GHOTS_VIEW_STATUS.VISIBLE)
        {
            this.gameObject.layer = 0;
        }
        else
        {
            this.gameObject.layer = 8;
        }
        m_pStateMachine.SMUpdate();
	}

    //トリガー
    void OnCollisionTrigger(Collision other)
    {
        m_Status = GHOTS_STATUS.STAN;
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Candy")
    //    {
    //        if (other.GetComponent<CCandy>().m_isShot==true)
    //        {
    //            m_Status = GHOTS_STATUS.STAN;
    //        }
    //    }


    //}

	public void HitCandy() {

        m_pStateMachine.ChangeState(CGhostState_Stan.Instance());

        
	}


    public bool IsMotionEnd(string name)
    {
        bool isname = this.m_animator.GetCurrentAnimatorStateInfo(0).IsName(name);
        bool istime = this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;

        if (isname && istime)
        {
            return true;
        }

        return false;
    }

    //プレス
    //void OnCollisionStay(Collision other)
    //{
    //    //ろうそくの光にあたっていれば
    //    if (other.gameObject.tag == "")
    //    {
    //        m_viewStatus = GHOTS_VIEW_STATUS.VISIBLE;
    //        gameObject.GetComponent<GhostControl>().m_nowSpeed = m_slowSpeed;

    //    }

    //}

    //リリース
    //void OnCollisionRelease(Collision other)
    //{

    //    //ろうそくの光から離れている
    //    if (other.gameObject.tag == "")
    //    {
    //        m_viewStatus = GHOTS_VIEW_STATUS.INVISIBLE;
    //        gameObject.GetComponent<GhostControl>().m_nowSpeed = m_normalSpeed;
    //    }


    //}

    public CStateMachine<GhostMain> GetFSM()
    {
        return m_pStateMachine;
    }
}
