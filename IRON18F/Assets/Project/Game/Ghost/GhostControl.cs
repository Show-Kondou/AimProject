using UnityEngine;
using System.Collections;

public class GhostControl : MonoBehaviour {

    private GhostMain m_ghostMain;

    // Use this for initialization
    void Start() {

        m_ghostMain = gameObject.GetComponent<GhostMain>();

    }

    // Update is called once per frame
    void Update()
    {
        if (m_ghostMain == null)
        {
            //回避策↓
           // m_ghostMain = gameObject.GetComponent<GhostMain>();
            Debug.Log("GhostMainを取得できていない");
            return;
        }

        if (m_ghostMain.m_pStateMachine.CurrentState().ID == (int)GhostStateID.STAN)
        {
            //m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Stan.Instance());
            return;
        }

        Move();

        
        CInputComponent.InputAction(CInputComponent.KEY_ACTION.TRIGGER, KeyCode.Joystick1Button1, PutDummy);

        //あとで消す
        CInputComponent.InputAction(CInputComponent.KEY_ACTION.TRIGGER, KeyCode.J, PutDummy);

        if (m_ghostMain.m_pStateMachine.CurrentState().ID != (int)GhostStateID.ATK)
        {
            CInputComponent.InputAction(CInputComponent.KEY_ACTION.TRIGGER, KeyCode.Joystick1Button2, OnAttackFlg);
            //あとで消す
            CInputComponent.InputAction(CInputComponent.KEY_ACTION.TRIGGER, KeyCode.K, OnAttackFlg);
        }
       

    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");

        float z = Input.GetAxisRaw("Vertical");

        // 移動する向きを求める
        Vector3 tmp = new Vector3(x, 0, z);

        m_ghostMain.MoveDirection = tmp.normalized;

        if (m_ghostMain.MoveDirection.magnitude > 0.1f)
        {
            if (!CInputComponent.InputAction(CInputComponent.KEY_ACTION.PRESS, KeyCode.LeftShift, Dash))
            {
                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Move.Instance(), m_ghostMain.m_pStateMachine.CurrentState().IsEnd);
            }
            return;
        }
       
        m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Main.Instance(), m_ghostMain.m_pStateMachine.CurrentState().IsEnd);

    }

    void OnAttackFlg()
    {

        m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Atk.Instance(), m_ghostMain.m_pStateMachine.CurrentState().IsEnd);
    }

    void PutDummy()
    {
        //追加
        //Dummyを置く
        m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Avatar.Instance(), m_ghostMain.m_pStateMachine.CurrentState().IsEnd);

    }

    void Dash()
    {
        m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Dash.Instance(), m_ghostMain.m_pStateMachine.CurrentState().IsEnd);
    }
}
