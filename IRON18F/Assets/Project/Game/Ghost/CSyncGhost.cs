using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class CSyncGhost : NetworkBehaviour {

    GhostMain m_ghostMain;


    [SyncVar]
    public Vector3 m_SyncPostion;

    [SyncVar]
    public Quaternion m_SyncRotation;

    //通信
    [SyncVar]
    public int m_status;

    //補間率
    private float lerpRate = 10;

    private float threshold = 0.1f;

    void Start()
    {
        if (transform.GetComponent<GhostMain>())
        {
            m_ghostMain = transform.GetComponent<GhostMain>();
            m_ghostMain.IsLocalPlayer = isLocalPlayer;
        }
        if (isLocalPlayer)
        {

                transform.GetComponent<GhostControl>().enabled = true;
                Cmd_UpdateTransform(transform.position, transform.rotation);
            
        }
   

    }

    void Update()
    {
        if (isLocalPlayer)
        {
            if (Quaternion.Angle(transform.rotation, m_SyncRotation) > threshold || Vector3.Distance(transform.position, m_SyncPostion) > threshold)
            {
                Cmd_UpdateTransform(transform.position, transform.rotation);
            }

            if (m_ghostMain.m_pStateMachine.CurrentState().ID != m_status)
            {
                Cmd_UpdateClient(m_ghostMain.m_pStateMachine.CurrentState().ID);
            }
            // Debug.Log("Command");
 


        }
        else {

            UpdateServer();
            //位置情報の補間
            transform.position = Vector3.Lerp(transform.position, m_SyncPostion, Time.deltaTime * lerpRate);
            transform.rotation = m_SyncRotation;

        }

    }


    [Command(channel = 0)]
    void Cmd_UpdateClient(int status)
    {
        m_status = status;
    }

    [Command(channel = 1)]
    void Cmd_UpdateTransform(Vector3 pos,Quaternion rota)
    {
        m_SyncPostion = pos;
        m_SyncRotation = rota;
    }

    void UpdateServer()
    {

        switch (m_status)
        {

            case (int)GhostStateID.MAIN:

                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Main.Instance());
                break;
            case (int)GhostStateID.ATK:

                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Atk.Instance());
                break;
            case (int)GhostStateID.AVATAR:

                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Avatar.Instance());
                break;
            case (int)GhostStateID.DASH:

                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Dash.Instance());
                break;
            case (int)GhostStateID.MOVE:

                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Move.Instance());
                break;
            case (int)GhostStateID.SLOW:

                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Slow.Instance());
                break;
            case (int)GhostStateID.STAN:

                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Stan.Instance());
                break;
            case (int)GhostStateID.WAIT:

                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Wait.Instance());
                break;

        }
    }
    
 
}
