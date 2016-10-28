using UnityEngine;
using System.Collections;

public class CHumanControl : MonoBehaviour {
    CHuman m_human;
    int keyIn;
    
	// Use this for initialization
	void Start () {
        m_human = this.GetComponent<CHuman>();
	}
	
	// Update is called once per frame
	void Update () {
       
            if (m_human.Dead == false)
            {
                keyIn = 0;
                if (m_human.IsLocal || m_human.IsDebug)
                {
                   Control();
                }
                FindBody();
                FindCandle();
                FindCandy();
                if (m_human.IsLocal || m_human.IsDebug)
                {
                    if (keyIn == 0)
                    {
                        m_human.PStateMachine.ChangeState(CHumanState_Main.Instance(), m_human.PStateMachine.CurrentState().IsEnd);
                    }
                }
            }
     

        m_human.PStateMachine.SMUpdate();

	}

    void Control()
    {
        m_human.CarryMove = 0;
        if (Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical")!=0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                move(2);
            }
            else
            {
                move(1);

            }
            m_human.CarryMove = 1;
            keyIn = 1;
        }
        if (Input.GetKeyUp(KeyCode.C) && m_human.CarryFlag == true)
        {
            m_human.PStateMachine.ChangeState(CHumanState_Main.Instance(), m_human.PStateMachine.CurrentState().IsEnd);
            keyIn = 1;

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
			
            if (m_human.ItemFlag == true)
            {
                m_human.PStateMachine.ChangeState(CHumanState_Set.Instance(), m_human.PStateMachine.CurrentState().IsEnd);
               
            }
			keyIn = 1;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {

            if (m_human.CandyFlag == true)
            {
                m_human.PStateMachine.ChangeState(CHumanState_Use.Instance(), m_human.PStateMachine.CurrentState().IsEnd);

            }
            keyIn = 1;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_human.PStateMachine.ChangeState(CHumanState_Bikuri.Instance(), m_human.PStateMachine.CurrentState().IsEnd);
            keyIn = 1;
        }
        
    }

    void move(int type)
    {
        float x = Input.GetAxisRaw("Horizontal");

        float z = Input.GetAxisRaw("Vertical");

        Vector3 tmp = new Vector3(x, 0, z);

         m_human.MoveDirection=tmp;
         m_human.MoveDirection.Normalize();

        if(m_human.CarryFlag != true)
        {
            if (type == 2 && m_human.HunmanStrength > 0)
            {
                m_human.PStateMachine.ChangeState(CHumanState_Dash.Instance(), m_human.PStateMachine.CurrentState().IsEnd);
            }
            else
            {
                m_human.PStateMachine.ChangeState(CHumanState_Move.Instance(), m_human.PStateMachine.CurrentState().IsEnd);
            }
        }


    }

    void FindBody()
    {
        if (m_human.Find.GetComponent<CFind>().FindBodyFlag == true)
        {
            if (m_human.PStateMachine.CurrentState()!=CHumanState_Dash.Instance() && m_human.ItemFlag == false)
            {
                if (Input.GetKey(KeyCode.C))
                {
                    m_human.PStateMachine.ChangeState(CHumanState_Carry.Instance(), m_human.PStateMachine.CurrentState().IsEnd);
                    
                    keyIn = 1;
                }

            }
        }
        else{
            if(m_human.PStateMachine.CurrentState()==CHumanState_Carry.Instance()||m_human.PStateMachine.CurrentState()==CHumanState_Carry_Motion.Instance())
            {
                m_human.PStateMachine.ChangeState(CHumanState_Main.Instance(), m_human.PStateMachine.CurrentState().IsEnd);
            }
        }
        
    }

    void FindCandle()
    {
        if (m_human.Find.GetComponent<CFind>().FindCandleFlag == true)
        {
            if (m_human.CarryFlag == false && m_human.ItemFlag == false)
            {

                m_human.PStateMachine.ChangeState(CHumanState_Item.Instance(), m_human.PStateMachine.CurrentState().IsEnd);
                m_human.ItemFlag = true;
                keyIn = 1;
            }
        }
        m_human.Find.GetComponent<CFind>().FindCandleFlag = false;
    }

    void FindCandy()
    {
        if (m_human.Find.GetComponent<CFind>().FindCandyFlag == true)
        {
            if (m_human.CarryFlag == false && m_human.CandyFlag == false)
            {
                m_human.CandyFlag = true;
                m_human.PStateMachine.ChangeState(CHumanState_Get.Instance(), m_human.PStateMachine.CurrentState().IsEnd);
                keyIn = 1;
            }
        }
        m_human.Find.GetComponent<CFind>().FindCandyFlag = false;
    }

   
}
