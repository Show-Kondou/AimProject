using UnityEngine;
using System.Collections;


//!  CState.cs
/*!
 * \details CState	ステート
 * \author  張爽
 * \date    2016/10/11	新規作成

 */
public class CState<entity_type>
{

    public int ID;
    public bool IsEnd;
    public entity_type Target;
        
    public virtual void Enter(entity_type entityType)
    {
        
    }
      
    public virtual void Execute(entity_type entityType)
    {

    }
      
    public virtual void Exit(entity_type entityType)
    {

    }  
}