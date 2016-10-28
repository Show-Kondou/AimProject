using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class CSyncHuman : NetworkBehaviour {


    [SyncVar]
    public Vector3 m_SyncPostion;

    [SyncVar]
    public Quaternion m_SyncRotation;

    [SyncVar]
    public int m_synclocalHumanState;

    [SyncVar]
    public int m_syncGlobalHumanState;

    [SyncVar]
    public GameObject m_SyncCandle;

    CHuman m_human;

    //補間率
    private float lerpRate = 10;

    private float threshold = 0.1f;
    private float threshold_rotation = 10.0f;

    // Use this for initialization
    void Start () {
        m_human = gameObject.GetComponent<CHuman>();
        if (isLocalPlayer||isServer)
        {
            gameObject.GetComponent<CHumanControl>().enabled = true;
            m_human.IsLocal = true;

        }
       
        if (isLocalPlayer)
        {
            Cmd_SyncTransform(transform.position, transform.rotation);
            Cmd_SyncState((int)m_human.PStateMachine.GlobalState().ID, (int)m_human.PStateMachine.CurrentState().ID);
        }
	}

    void Update()
    {

        if (!isLocalPlayer)
        {

            //位置情報の補間
            transform.position = Vector3.Lerp(transform.position, m_SyncPostion, Time.deltaTime * lerpRate);
            if (Quaternion.Angle(transform.rotation, m_SyncRotation) > threshold_rotation)

            {
                //角度の補間
                transform.rotation = Quaternion.Lerp(transform.rotation, m_SyncRotation, Time.deltaTime * lerpRate);
            }

            if (m_human.Candle != m_SyncCandle)
            {
                m_human.Candle = m_SyncCandle;
            }

             SetState();
            
        }
        else
        {
            UpdateServer();
        }


    }



    [Client]
    void UpdateServer()
    {
        if (Quaternion.Angle(transform.rotation, m_SyncRotation) > threshold_rotation || Vector3.Distance(transform.position, m_SyncPostion) > threshold)
        {
            Cmd_SyncTransform(transform.position, transform.rotation);
        }

        if (m_human.PStateMachine.GlobalState().ID != m_syncGlobalHumanState ||
            m_human.PStateMachine.CurrentState().ID != m_synclocalHumanState)
        {
            Cmd_SyncState((int)m_human.PStateMachine.GlobalState().ID, (int)m_human.PStateMachine.CurrentState().ID);
        }

        if (m_SyncCandle != m_human.Candle)
        {
            Cmd_SyncCandle(m_human.Candle);
        }
        //Cmd_SyncTransform(transform.position, transform.rotation);
       // Cmd_SyncState((int)m_human.PStateMachine.GlobalState().ID, (int)m_human.PStateMachine.CurrentState().ID);
    }

    [Command(channel = 1)]
    void Cmd_SyncTransform(Vector3 pos, Quaternion rota)
    {

        m_SyncPostion = pos;
        m_SyncRotation = rota;

    }

    [Command(channel = 0)]
    void Cmd_SyncState(int globalId,int localId)
    {
        m_syncGlobalHumanState = globalId;
        m_synclocalHumanState = localId;
    }
    //

    [Command(channel = 0)]
    void Cmd_SyncCandle(GameObject Candle)
    {
        
        m_SyncCandle = Candle;

    }

    void SetState()
    {

        switch (m_synclocalHumanState)
        {
            case (int)StateID.BIKURI:
                m_human.PStateMachine.ChangeState(CHumanState_Bikuri.Instance());
                break;
            case (int)StateID.CARRY:
                m_human.PStateMachine.ChangeState(CHumanState_Carry_Motion.Instance());
                break;
            case (int)StateID.DASH:
                m_human.PStateMachine.ChangeState(CHumanState_Dash_Motion.Instance());
                break;
            case (int)StateID.DEAD:
                m_human.PStateMachine.ChangeState(CHumanState_Dead.Instance());
                break;
            case (int)StateID.GET:
                m_human.PStateMachine.ChangeState(CHumanState_Get.Instance());
                break;
            case (int)StateID.ITEM:
                m_human.PStateMachine.ChangeState(CHumanState_Item.Instance());
                break;
            case (int)StateID.MAIN:
                m_human.PStateMachine.ChangeState(CHumanState_Main.Instance());
                break;
            case (int)StateID.MOVE:
                m_human.PStateMachine.ChangeState(CHumanState_Move_Motion.Instance());
                break;
            case (int)StateID.SET:
                m_human.PStateMachine.ChangeState(CHumanState_Set.Instance());
                break;
            case (int)StateID.USE:
                m_human.PStateMachine.ChangeState(CHumanState_Use.Instance());
                break;

        }

        switch (m_syncGlobalHumanState)
        {
            case (int)StateID.PANIK:
                m_human.PStateMachine.SetGlobalStateState(CHumanState_Perception.Instance());
                break;
            case (int)StateID.WAIT:
                m_human.PStateMachine.SetGlobalStateState(CHumanState_Wait.Instance());
                break;
        }
    }

}
