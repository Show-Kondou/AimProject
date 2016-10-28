using UnityEngine;
using System.Collections;



//!  CStateMachine.cs
/*!
 * \details CStateMachine	説明
 * \author  張爽
 * \date    2016/10/11	新規作成

 */

public class CStateMachine<entity_type>  
{  
    private entity_type m_pOwner;  
  
    private CState<entity_type> m_pCurrentState;//当前状态  
    private CState<entity_type> m_pPreviousState;//上一个状态  
    private CState<entity_type> m_pGlobalState;//全局状态  
  

    /*状态机构造函数*/  
    public CStateMachine (entity_type owner)  
    {  
        m_pOwner = owner;  
        m_pCurrentState = null;  
        m_pPreviousState = null;  
        m_pGlobalState = null;  
    }  
      
    /*进入全局状态*/  
    public void GlobalStateEnter()  
    {  
        m_pGlobalState.Enter(m_pOwner);  
    }  
      
    /*设置全局状态*/  
    public void SetGlobalStateState(CState<entity_type> GlobalState)  
    {  
        m_pGlobalState = GlobalState;  
        m_pGlobalState.Target = m_pOwner;  
        m_pGlobalState.Enter(m_pOwner);
    }
    
    /*设置当前状态*/  
    public void SetCurrentState(CState<entity_type> CurrentState)  
    {  
        m_pCurrentState = CurrentState;  
        m_pCurrentState.Target = m_pOwner;  
        m_pCurrentState.Enter(m_pOwner);  
    }  
  
    public void ChangeGlobalStateState()
    {
        m_pCurrentState = m_pGlobalState;
    }

    /*Update*/  
    public void SMUpdate ()  
    {

        if (m_pGlobalState != null)
        {
            m_pGlobalState.Execute(m_pOwner);
        }
            
          
        if (m_pCurrentState != null)
        {
            m_pCurrentState.Execute(m_pOwner);  
        }
            
    }

    /*状态改变*/
    public void ChangeGlobalState(CState<entity_type> pNewState)
    {
        if (pNewState == null)
        {
            Debug.LogError("can't find this state");
        }

        if (pNewState != m_pGlobalState)
        {
            //触发退出状态调用Exit方法  
            m_pGlobalState.Exit(m_pOwner);
            //设置新状态为当前状态  
            m_pGlobalState = pNewState;
            m_pGlobalState.Target = m_pOwner;
            //进入当前状态调用Enter方法  
            m_pGlobalState.Enter(m_pOwner);
        }

    }

    /*状态改变*/  
    public void ChangeState (CState<entity_type> pNewState)  
    {  
        if (pNewState == null) {  
            Debug.LogError ("can't find this state");  
        }
       
       if (pNewState != m_pCurrentState)
       {
           //触发退出状态调用Exit方法  
           m_pCurrentState.Exit(m_pOwner);
           //保存上一个状态   
           m_pPreviousState = m_pCurrentState;
           //设置新状态为当前状态  
           m_pCurrentState = pNewState;
           m_pCurrentState.Target = m_pOwner;
           //进入当前状态调用Enter方法  
           m_pCurrentState.Enter(m_pOwner);
       }
        
    }

    public void ChangeState(CState<entity_type> pNewState,bool IsEnd)
    {
        if (pNewState == null)
        {
            Debug.LogError("can't find this state");
        }
        if (IsEnd==true)
        {
            if (pNewState != m_pCurrentState)
            {
                //触发退出状态调用Exit方法  
                m_pCurrentState.Exit(m_pOwner);
                //保存上一个状态   
                m_pPreviousState = m_pCurrentState;
                //设置新状态为当前状态  
                m_pCurrentState = pNewState;
                m_pCurrentState.Target = m_pOwner;
                //进入当前状态调用Enter方法  
                m_pCurrentState.Enter(m_pOwner);
            }
        }
    }  

    public void RevertToPreviousState ()  
    {  
        //切换到前一个状态  
        ChangeState (m_pPreviousState);  
          
    }  
  
    public CState<entity_type> CurrentState ()  
    {  
        //返回当前状态  
        return m_pCurrentState;  
    }  
    public CState<entity_type> GlobalState ()  
    {  
        //返回全局状态  
        return m_pGlobalState;  
    }  
    public CState<entity_type> PreviousState ()  
    {  
        //返回前一个状态  
        return m_pPreviousState;  
    }  
  
}  

