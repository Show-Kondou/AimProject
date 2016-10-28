using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//!  CStageEditor.cs
/*!
 * \details CStageEditor	ステージエディター
 * \author  Shoki Tsuneyama
 * \date    2016/10/12	新規作成
 * \        2016/10/13  はきだし、具体的なエディター部分作成

 */


public class CStageEditor : CStage
{
    //ステージを選択してるかのフラグ
    bool m_isStageArea =false;

    //カーソル系
    GameObject m_cursor;
    public Vector3 m_cursorPos;
    //現在選択中のブロック
    EStageBlocks m_selectBlock = EStageBlocks.Block1;

    void Start()
    {
        Load(m_stageIndex);
        m_cursor = Instantiate(m_blocks[0]);
        m_cursor.transform.localScale *= 0.5f;
        for(int i=0;i<4;i++)
            m_sponerPoss[i] = new Vector2(WITDH / 2, HEIGHT / 2);
        //ブロックのGUIを作成
        for (int i = 0; i < (int)EStageBlocks.BlockMax; i++)
        {
            GameObject temp;
            temp =Instantiate(m_blocks[i]);
            temp.name = ("Block") + i;
            temp.transform.parent = transform;
            temp.transform.SetX(15 + (i % 6) * 1.333f);
            temp.transform.AddZ(-(i/6)* 1.5f);
            temp.tag = ("EditorButton");
            temp.AddComponent<CEditorButton>();
            temp.GetComponent<CEditorButton>().m_button = EEditorButton.Block;
            temp.GetComponent<CEditorButton>().m_block = (EStageBlocks)i + 1;
        }
    }
	// Update is called once per frame
	void Update ()
    {

        UpdateMouse();  //マウス場所更新
        UpdateCursor(); //描画用カーソル更新
        ChangeBlock();     //ブロック置いたり消したり
        if (Input.GetKeyDown(KeyCode.Space))
            Write();
	}

    void Write()
    {
        CStageFileManager.Instance.LoadFileList();
        CStageFileManager.Instance.SetStageData(m_stageData, m_stageIndex);
    }

    /*!  ChangeBlock
    *!   \details	マウスクリックした座標にブロックを置く。
    *!              ブロック削除追加
    *!
    */
    void ChangeBlock()
    {
        if (m_isStageArea)
            return;
        if(Input.GetMouseButton(0))
        {
            CreateBlock((int)m_cursorPos.x, (int)m_cursorPos.z, (int)m_selectBlock);
        }
        if(Input.GetMouseButton(1) )
        {
            SafeDeleteBlock((int)m_cursorPos.x, (int)m_cursorPos.z);
        }
    }

    //カーソルを更新
    void UpdateCursor()
    {
        if (m_cursor == null)
            return;
        if (m_isStageArea)
            return;
        m_cursor.transform.position = new Vector3(m_cursorPos.x * 0.5f + 0.25f - (WITDH * 0.5f / 2),
            0, m_cursorPos.z * 0.5f + 0.25f - (HEIGHT * 0.5f / 2));
        m_cursor.transform.parent = transform;
    }
    /*!  UpdateMouse
    *!   \details	マウス座標(ローカライズ)
    */
    void UpdateMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            m_isStageArea = true;
            if (hit.point.x > -WITDH/4+0.25 &&
                hit.point.x < WITDH / 4 &&
                hit.point.y > -HEIGHT/4+0.25&&
                hit.point.y < HEIGHT/4)
            {
                m_isStageArea = false;
                m_cursorPos = hit.point*2;
                m_cursorPos.y = 0;
                m_cursorPos.x += (WITDH  / 2);
                m_cursorPos.z += (HEIGHT / 2);
                m_cursorPos.x = (int)m_cursorPos.x ;
                m_cursorPos.z = (int)m_cursorPos.z ;

            }
            if (hit.collider.gameObject.tag == "EditorButton")
            {
                if(Input.GetMouseButtonDown(0))
                    PushButton(hit.collider.gameObject.GetComponent<CEditorButton>());
            }
        }

    }
    void PushButton(CEditorButton button)
    {
        m_selectBlock = button.m_block;

        if (m_cursor != null)
            Destroy(m_cursor.gameObject);
        m_cursor = InstanceBlock((int)button.m_block, 0, 0);
        switch(button.m_button)
        {
            case EEditorButton.ArrowL:
                Debug.Log(m_stageIndex);
                m_stageIndex = (m_stageIndex + STAGE_MAX - 1) % STAGE_MAX;
                Load(m_stageIndex);
                break;
            case EEditorButton.ArrowR:
                Debug.Log(m_stageIndex);
                m_stageIndex = (m_stageIndex + 1) % STAGE_MAX;
                Load(m_stageIndex);
                break;
            case EEditorButton.Save:
                Write();
                break;
            case EEditorButton.Delete:
                Load(m_stageIndex);
                break;
        }
    }

    /*!  InstanceBlock
    *!   \details	ブロックのenum指定でインスタンス
    *!
    *!   \return	ブロックのゲームオブジェクト型
    */
    protected override GameObject InstanceBlock(int n, int x, int y)
    {
        GameObject ret = null;
        switch ((EStageBlocks)n)
        {

            case EStageBlocks.Block1:
            case EStageBlocks.Block2:
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
            case EStageBlocks.Block13:
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

            case EStageBlocks.Sponer1P:
                ret = Instantiate(m_humanSponers[0]);
                ret.name = ("Human01");
                ret.transform.SetX(x * 0.5f + 0.25f - (WITDH * 0.5f / 2));
                ret.transform.SetZ(y * 0.5f + 0.25f - (HEIGHT * 0.5f / 2));
                ret.transform.localScale *= 0.5f;
                break;
            case EStageBlocks.Sponer2P:
                ret = Instantiate(m_humanSponers[1]);
                ret.name = ("Human02");
                ret.transform.SetX(x * 0.5f + 0.25f - (WITDH * 0.5f / 2));
                ret.transform.SetZ(y * 0.5f + 0.25f - (HEIGHT * 0.5f / 2));
                ret.transform.localScale *= 0.5f;
                break;
            case EStageBlocks.Sponer3P:
                ret = Instantiate(m_humanSponers[2]);
                ret.name = ("Human03");
                ret.transform.SetX(x * 0.5f + 0.25f - (WITDH * 0.5f / 2));
                ret.transform.SetZ(y * 0.5f + 0.25f - (HEIGHT * 0.5f / 2));
                ret.transform.localScale *= 0.5f;
                break;
            case EStageBlocks.GhostStart:
                ret = Instantiate(m_ghost);
                ret.transform.SetX(x * 0.5f + 0.25f - (WITDH * 0.5f / 2));
                ret.transform.SetZ(y * 0.5f + 0.25f - (HEIGHT * 0.5f / 2));
                ret.transform.localScale *= 0.5f;
                break;
        }
        return ret;
    }
}