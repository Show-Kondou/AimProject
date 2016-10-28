using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public enum EStageBlocks
{
    None = 0x00,
    Block1 = 0x01,
    Block2 = 0x02,
    Block3 = 0x03,
    Block4 = 0x04,
    Block5 = 0x05,
    Block6 = 0x06,
    Block7 = 0x07,
    Block8 = 0x08,
    Block9 = 0x09,
    Block10 = 0x0A,
    Block11 = 0x0B,
    Block12 = 0x0C,
    Block13 = 0x0D,
    Block14 = 0x0E,
    Block15 = 0x0F,
    Block16 = 0x10,
    Block17 = 0x11,
    Block18 = 0x12,
    Block19 = 0x13,
    Block20 = 0x14,
    Block21 = 0x15,
    Block22 = 0x16,
    Block23 = 0x17,
    Block24 = 0x18,
    Block25 = 0x19,
    Block26 = 0x1A,
    Block27 = 0x1B,
    Block28 = 0x1C,
    Block29 = 0x1D,
    Block30 = 0x1E,
    Block31 = 0x1F,
    BlockMax = 0x1F,

    Sponer1P = 0x50,
    Sponer2P = 0x51,
    Sponer3P = 0x52,
    SponerMax = 0x53,

    GhostStart = 0x5F,
    CandleSponer = 0x80,
}

public class CStage : NetworkBehaviour
{

    //ステージのナンバリング   Stage0~4
    public const int STAGE_MAX = 5;
    [SerializeField][Header("ステージ番号(0~4)")]
    protected int m_stageIndex = 0; 


    [SerializeField][Header("人のスポナープレファブ×3")]
    protected GameObject[] m_humanSponers = new GameObject[(int)EStageBlocks.SponerMax & 0x0f];

    [SerializeField][Header("おばけの初期座標")]
    protected GameObject m_ghost = null;

    [SerializeField][Header("ブロック")]
    protected GameObject[] m_blocks = new GameObject[(int)EStageBlocks.BlockMax & 0x0f];

    //ステージのサイズ
    public static int WITDH = 48;
    public static int HEIGHT = 40;

    //ステージの格納場所
    protected GameObject[,] m_stageBlocks = new GameObject[HEIGHT, WITDH];

    // Use this for initialization
    void Start()
    {
        Load(m_stageIndex);


    }
    // Update is called once per frame
    void Update()
    {

    }
    protected Vector2[] m_sponerPoss = new Vector2[4];
    /*!  CreateBlock
    *!   \details	指定座標に指定のブロックを生成(おいてたら塗り替える)
    *!
    */
    protected void CreateBlock(int x, int y, int n)
    {
        switch ((EStageBlocks)n)
        {
            case EStageBlocks.Sponer1P:
                SafeDeleteBlock((int)m_sponerPoss[0].x, (int)m_sponerPoss[0].y);
                m_sponerPoss[0].x = (float)x;
                m_sponerPoss[0].y = (float)y;
                break;
            case EStageBlocks.Sponer2P:
                SafeDeleteBlock((int)m_sponerPoss[1].x, (int)m_sponerPoss[1].y);
                m_sponerPoss[1].x = (float)x;
                m_sponerPoss[1].y = (float)y;
                break;
            case EStageBlocks.Sponer3P:
                SafeDeleteBlock((int)m_sponerPoss[2].x, (int)m_sponerPoss[2].y);
                m_sponerPoss[2].x = (float)x;
                m_sponerPoss[2].y = (float)y;
                break;
            case EStageBlocks.GhostStart:
                SafeDeleteBlock((int)m_sponerPoss[3].x, (int)m_sponerPoss[3].y);
                m_sponerPoss[3].x = (float)x;
                m_sponerPoss[3].y = (float)y;
                break;
        }
        SafeDeleteBlock( x,  y);
        m_stageData[y, x] = (byte)n;
        m_stageBlocks[y, x] = InstanceBlock(n,x,y);


    }


    /*!  SafeDeleteBlock
    *!   \details	ブロックのデリート
    *!
     */
    protected void SafeDeleteBlock(int x, int y)
    {
        m_stageData[y, x] = 0;
        if (m_stageBlocks[y, x] != null)
            Destroy(m_stageBlocks[y, x].gameObject);
    }


    //ファイル操作用
    protected byte[,] m_stageData = new byte[HEIGHT, WITDH];
    void SetStageData(int y, int x, byte n) { m_stageData[y, x] = n; }

    /*!  Load
    *!   \details	Csvのステージデータを読み込み
    *!
    */
    public void Load(int stageNo)
    {
        CStageFileManager.Instance.LoadFileList();
        m_stageData = CStageFileManager.Instance.GetStageData(stageNo);
        for (int y = 0; y < HEIGHT; y++)
        for (int x = 0; x < WITDH; x++)
            CreateBlock(x, y, m_stageData[y, x]);
    }

    /*!  InstanceBlock
    *!   \details	ブロックのenum指定でインスタンス
    *!
    *!   \return	ブロックのゲームオブジェクト型
    */
    protected virtual GameObject InstanceBlock(int n,int x, int y)
    {
        GameObject ret = null;
        switch ((EStageBlocks)n)
        {

            case EStageBlocks.Block1:
            case EStageBlocks.Block3:
            case EStageBlocks.Block4:
            case EStageBlocks.Block5:
            case EStageBlocks.Block6:
            case EStageBlocks.Block7:
            case EStageBlocks.Block8:
            case EStageBlocks.Block9:
            case EStageBlocks.Block10:
            case EStageBlocks.Block11:
            case EStageBlocks.Block12:
            case EStageBlocks.Block14:
            case EStageBlocks.Block15:
            case EStageBlocks.Block16:
            case EStageBlocks.Block17:
            case EStageBlocks.Block18:
            case EStageBlocks.Block19:
            case EStageBlocks.Block20:
            case EStageBlocks.Block21:
            case EStageBlocks.Block22:
            case EStageBlocks.Block23:
            case EStageBlocks.Block24:
            case EStageBlocks.Block25:
            case EStageBlocks.Block26:
            case EStageBlocks.Block27:
            case EStageBlocks.Block28:
            case EStageBlocks.Block29:
            case EStageBlocks.Block30:
            case EStageBlocks.Block31:
                ret = Instantiate(m_blocks[n - 1]);
                Transform blocks = transform.FindChild("Blocks");
                ret.transform.parent = blocks;


                ret.transform.SetX(x * 0.5f + 0.25f - (WITDH * 0.5f / 2));
                ret.transform.SetZ(y * 0.5f + 0.25f - (HEIGHT * 0.5f / 2));
                ret.transform.localScale *= 0.5f;

                break;

            case EStageBlocks.Block2:
                if (isServer)
                {
                    ret = Instantiate(m_blocks[n - 1]);
                    foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
                    {
                        // シーン上に存在するオブジェクトならば処理.
                        if (obj.activeInHierarchy)
                        {
                            if (obj.name == "CandleSpawnerManager")
                            {
                                ret.transform.parent = obj.transform;
                            }
                        }

                    }
                    ret.transform.SetX(x * 0.5f + 0.25f - (WITDH * 0.5f / 2));
                    ret.transform.SetZ(y * 0.5f + 0.25f - (HEIGHT * 0.5f / 2));

                } 
                break;
            //if (isServer)
            //{
            //    var candle = Instantiate(m_blocks[n - 1]);
            //    foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            //    {
            //        // シーン上に存在するオブジェクトならば処理.
            //        if (obj.activeInHierarchy)
            //        {
            //            if (obj.name == "CandleSpawnerManager")
            //            {
            //                candle.transform.parent = obj.transform;
            //            }
            //        }
            //    }
            //    candle.transform.SetX(x * 0.5f + 0.25f - (WITDH * 0.5f / 2));
            //    candle.transform.SetZ(y * 0.5f + 0.25f - (HEIGHT * 0.5f / 2));
            //    NetworkServer.Spawn(candle);
            //}

            case EStageBlocks.Block13:
                if (isServer)
                {
                    var altar = Instantiate(m_blocks[n - 1]);
                    foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
                    {
                        // シーン上に存在するオブジェクトならば処理.
                        if (obj.activeInHierarchy)
                        {
                            if (obj.name == "AltarManager")
                            {
                                altar.transform.parent = obj.transform;
                            }
                        }
                    }
                    altar.transform.SetX(x * 0.5f + 0.25f - (WITDH * 0.5f / 2));
                    altar.transform.SetZ(y * 0.5f + 0.25f - (HEIGHT * 0.5f / 2));
                    NetworkServer.Spawn(altar);
                }
                break;

            case EStageBlocks.Sponer1P:
                ret = Instantiate(m_humanSponers[0]);
                ret.name = ("Human01");
                break;
            case EStageBlocks.Sponer2P:
                ret = Instantiate(m_humanSponers[1]);
                ret.name = ("Human02");
                break;
            case EStageBlocks.Sponer3P:
                ret = Instantiate(m_humanSponers[2]);
                ret.name = ("Human03");
                break;
            case EStageBlocks.GhostStart:
                ret = Instantiate(m_ghost);
                break;
        }
        return ret;
    }
}
