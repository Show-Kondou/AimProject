using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GhostStateID {
    MAIN,
    WAIT,
    MOVE,
    DASH,
    SLOW,
    STAN,
    ATK,
    AVATAR,

}

public class CGhostState_Main : CState<GhostMain>
{
   
    public static CGhostState_Main instance;
    public static CGhostState_Main Instance()
    {
        if (instance == null)
            instance = new CGhostState_Main();

        return instance;
    }

    public override void Enter(GhostMain ghost)
    {
        this.IsEnd = true;
        ID = (int)GhostStateID.MAIN;
        ghost.m_animator.SetInteger("State", 0);
        ghost.m_animator.SetInteger("Act", 0);
    }

    public override void Execute(GhostMain ghost)
    {

    }

    public override void Exit(GhostMain ghost)
    {
        
    }
}

public class CGhostState_Wait : CState<GhostMain>
{

    public static CGhostState_Wait instance;
    public static CGhostState_Wait Instance()
    {
        if (instance == null)
            instance = new CGhostState_Wait();

        return instance;
    }

    public override void Enter(GhostMain ghost)
    {
        this.IsEnd = true;
        ID = (int)GhostStateID.WAIT;
    }

    public override void Execute(GhostMain ghost)
    {


    }

    public override void Exit(GhostMain ghost)
    {

    }
}

public class CGhostState_Move : CState<GhostMain>
{

    public static CGhostState_Move instance;
    public static CGhostState_Move Instance()
    {
        if (instance == null)
            instance = new CGhostState_Move();

        return instance;
    }

    public override void Enter(GhostMain ghost)
    {
        ID = (int)GhostStateID.MOVE;
        this.IsEnd = true;

        ghost.m_viewStatus = GhostMain.GHOTS_VIEW_STATUS.INVISIBLE;

    }

    public override void Execute(GhostMain ghost)
    {
        // 移動する向きとスピードを代入する
        ghost.transform.position += new Vector3(ghost.MoveDirection.x * (ghost.NomalSpeed * ghost.ShowBuff) / 10, 0, ghost.MoveDirection.z * ghost.NomalSpeed / 10);

        if (ghost.MoveDirection.magnitude > 0.1f)
        {
            ghost.transform.rotation = Quaternion.LookRotation(ghost.MoveDirection);
        }

        ghost.m_animator.SetInteger("State", 1);
    }

    public override void Exit(GhostMain ghost)
    {
        ghost.m_viewStatus = GhostMain.GHOTS_VIEW_STATUS.INVISIBLE;
    }
}

public class CGhostState_Dash : CState<GhostMain>
{

    public static CGhostState_Dash instance;
    public static CGhostState_Dash Instance()
    {
        if (instance == null)
            instance = new CGhostState_Dash();

        return instance;
    }

    public override void Enter(GhostMain ghost)
    {
        ID = (int)GhostStateID.DASH;
        this.IsEnd = true;
        ghost.m_viewStatus = GhostMain.GHOTS_VIEW_STATUS.VISIBLE;
    }

    public override void Execute(GhostMain ghost)
    {
        // 移動する向きとスピードを代入する
        ghost.transform.position += new Vector3(ghost.MoveDirection.x * (ghost.DashSpeed * ghost.ShowBuff) / 10, 0, ghost.MoveDirection.z * ghost.DashSpeed / 10);

        if (ghost.MoveDirection.magnitude > 0.1f)
        {
            ghost.transform.rotation = Quaternion.LookRotation(ghost.MoveDirection);
        }
        ghost.m_animator.SetInteger("State", 2);

    }

    public override void Exit(GhostMain ghost)
    {
 
        ghost.m_viewStatus = GhostMain.GHOTS_VIEW_STATUS.INVISIBLE;
    }
}

public class CGhostState_Slow : CState<GhostMain>
{

    public static CGhostState_Slow instance;
    public static CGhostState_Slow Instance()
    {
        if (instance == null)
            instance = new CGhostState_Slow();

        return instance;
    }

    public override void Enter(GhostMain ghost)
    {
        ID = (int)GhostStateID.SLOW;
        this.IsEnd = true;
        ghost.m_viewStatus = GhostMain.GHOTS_VIEW_STATUS.VISIBLE;
    }

    public override void Execute(GhostMain ghost)
    {
        ghost.m_viewStatus = GhostMain.GHOTS_VIEW_STATUS.VISIBLE;
        // 移動する向きとスピードを代入する
        if(ghost.m_pStateMachine.CurrentState().ID==(int)GhostStateID.MOVE)
        {
            ghost.ShowBuff = ghost.SlowSpeed / ghost.NomalSpeed;
        }

        if(ghost.m_pStateMachine.CurrentState().ID==(int)GhostStateID.DASH)
        {
            ghost.ShowBuff = ghost.SlowSpeed / ghost.DashSpeed;
        }
        

    }

    public override void Exit(GhostMain ghost)
    {

        ghost.m_viewStatus = GhostMain.GHOTS_VIEW_STATUS.INVISIBLE;
        ghost.ShowBuff = 1;
    }
}

public class CGhostState_Stan : CState<GhostMain>
{
    float m_nowTime;
    public static CGhostState_Stan instance;
    public static CGhostState_Stan Instance()
    {
        if (instance == null)
            instance = new CGhostState_Stan();

        return instance;
    }

    public override void Enter(GhostMain ghost)
    {
        m_nowTime = 0;
        ID = (int)GhostStateID.STAN;
        ghost.m_animator.SetInteger("State", 3);
        this.IsEnd = true;
        ghost.GhostScore.hitCandy++;
    }

    public override void Execute(GhostMain ghost)
    {

        //スタンされているなら　スタン時間を計算する


            m_nowTime += Time.deltaTime;
            if (m_nowTime >= ghost.m_stanInterval)
            {

                ghost.m_pStateMachine.ChangeState(CGhostState_Main.Instance());

                m_nowTime = 0;
            }
        
        
    }

    public override void Exit(GhostMain ghost)
    {

    }
}

public class CGhostState_Atk : CState<GhostMain>
{
    private float count;
    private bool IsAtkPatOn;
    public static CGhostState_Atk instance;
    public static CGhostState_Atk Instance()
    {
        if (instance == null)
            instance = new CGhostState_Atk();

        return instance;
    }

    public override void Enter(GhostMain ghost)
    {
        count = 0;
        Debug.Log(count);
        ghost.m_viewStatus = GhostMain.GHOTS_VIEW_STATUS.VISIBLE;
        this.IsEnd = false;
        ID = (int)GhostStateID.ATK;
        IsAtkPatOn = false;
        ghost.m_animator.SetInteger("Act", 1);
        
    }

    public override void Execute(GhostMain ghost)
    {
        count += Time.deltaTime;
        if (count > 0.15f)
        {
            if (IsAtkPatOn==false)
            {
                GameObject tmp_obj = (GameObject)MonoBehaviour.Instantiate(ghost.atkParticle, ghost.transform.position, ghost.transform.rotation);
                tmp_obj.transform.parent = ghost.transform;
                IsAtkPatOn = true;
            }
            ghost.IsAttack = true;
            ghost.transform.Translate(ghost.transform.forward * ghost.AtkSpeed * Time.deltaTime * 10, Space.World);
        }
        if (ghost.IsMotionEnd("Atk"))
        {
            IsEnd = true;
        }
    }

    public override void Exit(GhostMain ghost)
    {
        ghost.IsAttack = false;
        IsAtkPatOn = false;
        ghost.m_viewStatus = GhostMain.GHOTS_VIEW_STATUS.INVISIBLE;
        ghost.m_animator.SetInteger("Act", 0);
    }
}

public class CGhostState_Avatar : CState<GhostMain>
{

    public static CGhostState_Avatar instance;
    public static CGhostState_Avatar Instance()
    {
        if (instance == null)
            instance = new CGhostState_Avatar();

        return instance;
    }

    public override void Enter(GhostMain ghost)
    {

        ID = (int)GhostStateID.AVATAR;
        this.IsEnd = false;
        if (ghost.m_nowDunmmyNum != 0)
        {
            ghost.m_animator.SetInteger("Act", 2);
        }
    }

    public override void Execute(GhostMain ghost)
    {

        if (ghost.IsMotionEnd("Bunshin"))
        {
            IsEnd = true;
        }
    }

    public override void Exit(GhostMain ghost)
    {
        ghost.m_nowDunmmyNum = 0;
        for (int i = 0; i < 3; i++)
        {
            if (ghost.dGhost[i].GetComponent<DummyGhost>().IsAct == false)
            {
                ghost.m_nowDunmmyNum++;
            }
        }

        if (ghost.m_nowDunmmyNum != 0)
        {
            Debug.Log("Dummyを置いたよ");
            for (int i = 0; i < 3; i++)
            {
                if (ghost.dGhost[i].GetComponent<DummyGhost>().IsAct == false)
                {
                    ghost.dGhost[i].transform.parent = null;
                    ghost.dGhost[i].GetComponent<CapsuleCollider>().enabled = true;
                    ghost.dGhost[i].GetComponent<DummyGhost>().IsAct = true;
                    ghost.dGhost[i].transform.position = ghost.transform.position;
                    break;
                }

            }
        }
        ghost.m_animator.SetInteger("Act", 0);
    }
}