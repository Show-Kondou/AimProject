using UnityEngine;
using System.Collections;

public class CHuman : MonoBehaviour
{

    private GameObject m_carryHuman;//運ぶ用容器
    private GameObject m_find;
    private GameObject m_candy;
    private GameObject m_ghost;
    private GameObject[] m_dummyGhost;
    private GameObject m_candle;
    private CStateMachine<CHuman> m_pStateMachine;//定义一个状态机  
    private Vector3 m_moveDirection;
    private Animator m_animator;

    private SHumanScore m_shumanScore;

    [SerializeField]
    private float m_moveSpeed;
    public float DashSpeed;
    private int m_hunmanTime;
    [SerializeField]
    private float strength;
    private float m_hunmanstrength;
    [SerializeField]
    private int m_hp;
    [SerializeField]
    private bool m_dead;
    private bool m_itemFlag;
    private bool m_candyFlag;
    private bool m_carryFlag;
    [SerializeField]
    private bool m_lightOn;
    private int m_carryMove;
    [SerializeField]
    private int MaxLifeTime;
    private int m_reLife;
    private bool m_isReLifeScoreOn;
    private int m_id;
    public bool IsLocal;

    private ParticleSystem m_exMark;
    private ParticleSystem m_impatiencle;
    private ParticleSystem m_chikin;

    public float Area;
    public bool IsDebug;
    public CBeat beatSE;
    /// <summary>
    /// アクセス
    /// </summary>
    /// 

    public GameObject[] DummyGhost
    {
        get { return m_dummyGhost; }
    }
    
    public Animator AnimatorEDT
    {
        get { return m_animator; }
        set { m_animator = value; }
    }

    public CStateMachine<CHuman> PStateMachine
    {
        get { return m_pStateMachine; }
        set { m_pStateMachine = value; }
    }

    public GameObject CarryHuman
    {
        get { return m_carryHuman; }
        set { m_carryHuman = value; }
    }

    public ParticleSystem ExMark
    {
        get { return m_exMark; }
        set { m_exMark = value; }
    }

    public ParticleSystem Impatiencle
    {
        get { return m_impatiencle; }
        set { m_impatiencle = value; }
    }

    public ParticleSystem Chikin
    {
        get { return m_chikin; }
        set { m_chikin = value; }
    }

    public GameObject Candle
    {
        get { return m_candle; }
        set { m_candle = value; }
    }

    public GameObject Candy
    {
        get { return m_candy; }
        set { m_candy = value; }
    }

    public GameObject Find
    {
        get { return m_find; }
        set { m_find = value; }
    }

    public GameObject Ghost
    {
        get { return m_ghost; }
        set { m_ghost = value; }
    }

    public SHumanScore ShumanScore
    {
        get { return m_shumanScore; }
        set { m_shumanScore = value; }
    }

    public Vector3 MoveDirection
    {
        get { return m_moveDirection; }
        set { m_moveDirection = value; }
    }


    public float MoveSpeed
    {
        get { return m_moveSpeed; }
        set { m_moveSpeed = value; }
    }

    public int HunmanTime
    {
        get { return m_hunmanTime; }
        set { m_hunmanTime = value; }
    }
    public float Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    public float HunmanStrength
    {
        get { return m_hunmanstrength; }
        set { m_hunmanstrength = value; }
    }

    public bool Dead
    {
        get { return m_dead; }
        set { m_dead = value; }
    }

    public bool ItemFlag
    {
        get { return m_itemFlag; }
        set { m_itemFlag = value; }
    }

    public bool CandyFlag
    {
        get { return m_candyFlag; }
        set { m_candyFlag = value; }
    }


    public bool CarryFlag
    {
        get { return m_carryFlag; }
        set { m_carryFlag = value; }
    }

    public bool LightOn
    {
        get { return m_lightOn; }
        set { m_lightOn = value; }
    }

    public bool IsReLifeScoreOn
    {
        get { return m_isReLifeScoreOn; }
        set { m_isReLifeScoreOn = value; }
    }

    public int CarryMove
    {
        get { return m_carryMove; }
        set { m_carryMove = value; }
    }

    public int ReLife
    {
        get { return m_reLife; }
        set { m_reLife = value; }
    }

    public int ID
    {
        get { return m_id; }
        set { m_id = value; }
    }
    /// <summary>
    /// 設定
    /// </summary>
    public void StopHumanTime()
    {
        m_hunmanTime = 0;
    }
    public void StartHumanTime()
    {
        m_hunmanTime = 1;
    }
    /// <summary>
    /// 動作
    /// </summary>
    public void PlusStrength(float v)
    {
        
       m_hunmanstrength = v;
        

    }
    public void MinusStrength(float v)
    {
        if (m_hunmanstrength > 0)
        {
            m_hunmanstrength -= v;
        }

    }
    public void Damage()
    {
        m_hp = 0;
    }
    public bool Doa()
    {
        if (m_hp < 1)
        {
            if (m_reLife == MaxLifeTime)
            {
                m_reLife = 0;
            }
            return true;
        }
        return false;
    }
    public void ReLifeOn()
    {

        if (m_reLife < MaxLifeTime)
        {
            m_reLife++;/////////////////////////////////////
            if (m_reLife == MaxLifeTime)
            {
                m_hp = 1;
                m_pStateMachine.ChangeState(CHumanState_Main.Instance());
            }
        }
    }

    public void SetScore()
    {
        CGameScore.Instance.SetHumanScore(this);
    }
    /// <summary>
    /// Main
    /// </summary>
    ///

    void Start()
    {
        IsLocal = false;
        if (m_dummyGhost == null)
        {
            m_dummyGhost = GameObject.FindGameObjectsWithTag("DummyGhost");//優先
        }
        if (m_ghost == null)
        {
            m_ghost = GameObject.Find("Ghost(Clone)");//優先
            if (m_ghost == null)
            {
                m_ghost = GameObject.Find("Ghost");
            }
        }
        if (m_find == null)
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "find")
                {
                    m_find = child.gameObject;
                }
            }
        }
        if (m_candy == null)
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "Candy")
                {
                    m_candy = child.gameObject;
                }
            }
        }
        if (m_animator == null)
        {
            m_animator = this.GetComponent<Animator>();
        }

        if (ExMark == null || Impatiencle == null || Chikin==null)
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "ExclamationMark")
                {
                    ExMark = child.GetComponent<ParticleSystem>();
                }
                if(child.name == "ImpatienceParticle")
                {
                    Impatiencle = child.GetComponent<ParticleSystem>();
                }
                if (child.name == "StanChickParticle")
                {
                    Chikin = child.GetComponent<ParticleSystem>();
                    Chikin.gameObject.SetActive(false);
                }
            }
        
        }

        beatSE = new CBeat();
        m_shumanScore = new SHumanScore();
        m_hp = 1;
        m_reLife = MaxLifeTime;
        m_hunmanstrength = 100;
        m_hunmanTime = 1;
        m_pStateMachine = new CStateMachine<CHuman>(this);//初始化状态机  
        m_pStateMachine.SetCurrentState(CHumanState_Main.Instance()); //设置一个当前状态  
        m_pStateMachine.SetGlobalStateState(CHumanState_Wait.Instance());//设置全局状态  
        m_candyFlag = true;
        ShumanScore.resuscitation = 0;
        ShumanScore.setAlterCandle = 0;
        ShumanScore.setCandle = 0;
        m_isReLifeScoreOn = false;
        
    }

    void Update()
    {

        if (m_dummyGhost == null || m_dummyGhost.Length<3)
        {
            m_dummyGhost = GameObject.FindGameObjectsWithTag("DummyGhost");//優先
        }
        if (m_ghost == null)
        {
            m_ghost = GameObject.Find("Ghost(Clone)");//優先
            if(m_ghost==null)
            {
                m_ghost = GameObject.Find("Ghost");
            }
        }
        if (m_find == null)
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "find")
                {
                    m_find = child.gameObject;
                }
            }
        }

        if (m_candy == null)
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "Candy")
                {
                    m_candy = child.gameObject;
                }
            }
        }

        if (m_animator==null)
        {
            m_animator = this.GetComponent<Animator>();
        }

        if (ExMark == null || Impatiencle == null || Chikin == null)
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "ExclamationMark")
                {
                    ExMark = child.GetComponent<ParticleSystem>();
                }
                if (child.name == "ImpatienceParticle")
                {
                    Impatiencle = child.GetComponent<ParticleSystem>();
                }
                if (child.name == "StanChickParticle")
                {
                    Chikin = child.GetComponent<ParticleSystem>();
                    Chikin.gameObject.SetActive(false);
                }
            }
        }

        if (Doa())
        {
            m_pStateMachine.ChangeState(CHumanState_Dead.Instance());
        }

        if(HunmanStrength<=0)
        {
            if (!Impatiencle.isPlaying)
            {
                Impatiencle.Play();
            }
            
        }
        else
        {
            // Impatiencle.Stop();
          //  Impatience.Clear();
        }
    }

    public bool IsMotionEnd(string name)
    {
        bool isname = this.AnimatorEDT.GetCurrentAnimatorStateInfo(0).IsName(name);
        bool istime = this.AnimatorEDT.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;

        if (isname && istime)
        {
            return true;
        }

        return false;
    }

    public CStateMachine<CHuman> GetFSM()
    {
        return m_pStateMachine;
    }
}
