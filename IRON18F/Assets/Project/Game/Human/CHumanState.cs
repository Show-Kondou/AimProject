using UnityEngine;
using System.Collections;

public enum StateID{
    MAIN,
    PANIK,//gb
    WAIT,//gb
    MOVE,
    DASH,
    CARRY,
    DEAD,
    ITEM,
    GET,
    SET,
    USE,
    BIKURI,
   
}

public class CHumanState_Main : CState<CHuman>
{
    public static CHumanState_Main instance;
    public static CHumanState_Main Instance()
    {
        if (instance == null)
            instance = new CHumanState_Main();
        return instance;
    }

    public override void Enter(CHuman human)
    {
        ID = (int)StateID.MAIN;
        IsEnd = true;
        human.AnimatorEDT.SetInteger("State", 0);
        human.AnimatorEDT.SetInteger("Act", 0);
    }

    public override void Execute(CHuman human)
    {
        human.AnimatorEDT.SetInteger("State", 0);
        human.AnimatorEDT.SetInteger("Act", 0);
       
    }

    public override void Exit(CHuman human)
    {

    }
}

public class CHumanState_Perception : CState<CHuman>    // 人鼓動
{
    
    int DghostPniku = 0, GhostPniku = 0;
    float range=0;
    public static CHumanState_Perception instance;
    public static CHumanState_Perception Instance()
    {
        if (instance == null)
            instance = new CHumanState_Perception();
       
        return instance;
    }

    public override void Enter(CHuman human)
    {
        ID = (int)StateID.PANIK;
        IsEnd = true;
    }

    public override void Execute(CHuman human)
    {
       
        if(human.Dead==false)
        {
            human.AnimatorEDT.SetInteger("GState", 1);

            if (human.LightOn == true)
            {
                human.PlusStrength(human.Strength);
            }

            if (human.Ghost != null)
            {
                float lenth = (human.transform.position - human.Ghost.transform.position).magnitude;
                range = lenth;
                if (lenth > human.Area)
                {
                    if( DghostPniku==0)
                    {
                        human.PStateMachine.SetGlobalStateState(CHumanState_Wait.Instance());
                        human.AnimatorEDT.SetInteger("GState", 0);
                        
                    }
                    GhostPniku = 0;
                }
                else
                {
                    GhostPniku = 1;
                }
            }
            if (human.DummyGhost!=null)
            {
                foreach (GameObject obj in human.DummyGhost)
                {
                    if (obj != null)
                    {
                        if (obj.GetComponent<DummyGhost>() != null)
                        {
                            if (obj.GetComponent<DummyGhost>().IsAct == true)
                            {
                                float lenth = (human.transform.position - obj.transform.position).magnitude;
                                if (lenth < range)
                                {
                                    range = lenth;
                                }
                                if (lenth < 1)
                                {
                                    lenth = 1f;
                                }
                                if (lenth > human.Area)
                                {
                                    DghostPniku = 0;
                                }
                                else
                                {
                                    DghostPniku = 1;
                                    break;
                                }
                            }
                            else
                            {
                                DghostPniku = 0;
                            }
                        }
                    }

                }
                if (DghostPniku == 0)
                {
                    if (GhostPniku==0)
                    {
                        human.PStateMachine.ChangeGlobalState(CHumanState_Wait.Instance());
                        human.AnimatorEDT.SetInteger("GState", 0);
                       
                    }
                }
            }
            
        }
        if (human.IsLocal || human.IsDebug)
        {
            human.beatSE.Panic(1 - range / human.Area);
        }
        
    }

    public override void Exit(CHuman human)
    {
        range = 0;
    }
}

public class CHumanState_Wait:CState<CHuman>    //N
{
    public static CHumanState_Wait instance;
    public static CHumanState_Wait Instance()
    {
        if (instance == null)
            instance = new CHumanState_Wait();
  
        return instance;
    }

    public override void Enter(CHuman human)
    {
       ID = (int)StateID.WAIT;
       IsEnd = true;
    }

    public override void Execute(CHuman human)
    {
        if (human.Dead == false)
        {

            human.AnimatorEDT.SetInteger("GState", 0);

            if (human.LightOn == true)
            {
                human.PlusStrength(human.Strength);
            }

            if (human.Ghost != null)
            {
                float lenth = (human.transform.position - human.Ghost.transform.position).magnitude;
                if (lenth <= human.Area)
                {
                    human.PStateMachine.ChangeGlobalState(CHumanState_Perception.Instance());
                    human.AnimatorEDT.SetInteger("GState", 1);
                }
            }
            if (human.DummyGhost!=null)
            {
                foreach (GameObject obj in human.DummyGhost)
                {
                    if (obj!=null)
                    {
                        if (obj.GetComponent<DummyGhost>() != null)
                        {
                            if (obj.GetComponent<DummyGhost>().IsAct == true)
                            {
                                float lenth = (human.transform.position - obj.transform.position).magnitude;
                                if (lenth <= human.Area)
                                {
                                    human.PStateMachine.SetGlobalStateState(CHumanState_Perception.Instance());
                                    human.AnimatorEDT.SetInteger("GState", 1);
                                }
                            }
                        }
                    }
                    
                }
            }
        }
    }

    public override void Exit(CHuman human)
    {

    }
}

public class CHumanState_Move : CState<CHuman>
{
    public static CHumanState_Move instance;
    public static CHumanState_Move Instance()
    {
        if (instance == null)
            instance = new CHumanState_Move();
  
        return instance;
    }

    public override void Enter(CHuman human)
    {
        ID = (int)StateID.MOVE;
        IsEnd = true;
    }

    public override void Execute(CHuman human)
    {
        human.transform.Translate(human.MoveDirection * human.MoveSpeed * Time.deltaTime * human.HunmanTime * 10, Space.World);
        Quaternion TargetRotation = Quaternion.LookRotation(human.MoveDirection, Vector3.up);
        human.transform.rotation = Quaternion.Slerp(human.transform.rotation, TargetRotation, Time.deltaTime * human.HunmanTime * 20f);
        human.AnimatorEDT.SetInteger("State", 1);

    }

    public override void Exit(CHuman human)
    {
   
    }
}

public class CHumanState_Dash : CState<CHuman>
{
    public static CHumanState_Dash instance;
    public static CHumanState_Dash Instance()
    {
        if (instance == null)
            instance = new CHumanState_Dash();
     
        return instance;
    }

    public override void Enter(CHuman human)
    {
       ID = (int)StateID.DASH;
       IsEnd = true;
    }

    public override void Execute(CHuman human)
    {
        human.transform.Translate(human.MoveDirection * human.DashSpeed * Time.deltaTime * human.HunmanTime * 10, Space.World);
        human.MinusStrength(0.2f);
        Quaternion TargetRotation = Quaternion.LookRotation(human.MoveDirection, Vector3.up);
        human.transform.rotation = Quaternion.Slerp(human.transform.rotation, TargetRotation, Time.deltaTime * human.HunmanTime * 10f);
        human.AnimatorEDT.SetInteger("State", 2);
    }

    public override void Exit(CHuman human)
    {

    }
}

public class CHumanState_Carry : CState<CHuman>
{
    public static CHumanState_Carry instance;
    public static CHumanState_Carry Instance()
    {
        if (instance == null)
            instance = new CHumanState_Carry();
    
        return instance;
    }

    public override void Enter(CHuman human)
    {
        human.CarryFlag = true;
        ID = (int)StateID.CARRY;
        IsEnd = true;
    }

    public override void Execute(CHuman human)
    {
        if (human.Find.GetComponent<CFind>() != null)
        {
            if (human.Find.GetComponent<CFind>().Body != null)
            {
                human.Find.GetComponent<CFind>().Body.GetComponent<CHuman>().CarryHuman = human.gameObject;
            }
            human.CarryHuman = human.Find.GetComponent<CFind>().Body;
            human.transform.Translate(human.MoveDirection * human.MoveSpeed * 0.5f * Time.deltaTime * human.HunmanTime * 10, Space.World);
            //   human.CarryHuman.transform.Translate((human.transform.position - human.CarryHuman.transform.position) * human.CarryMove * human.MoveSpeed * 0.5f * Time.deltaTime * human.HunmanTime * 10, Space.World);
            Quaternion TargetRotation_P = Quaternion.LookRotation(human.CarryHuman.transform.position - human.transform.position, Vector3.up);
            //   Quaternion TargetRotation_B = Quaternion.LookRotation(human.transform.position - human.CarryHuman.transform.position, Vector3.up);
            human.transform.rotation = Quaternion.Slerp(human.transform.rotation, TargetRotation_P, Time.deltaTime * human.HunmanTime * 10f);
            //  human.CarryHuman.transform.rotation = Quaternion.Slerp(human.CarryHuman.transform.rotation, TargetRotation_B, Time.deltaTime * human.HunmanTime * 10f);
            human.AnimatorEDT.SetInteger("Act", 1);
        }
    }

    public override void Exit(CHuman human)
    {
        human.CarryFlag = false;
        human.AnimatorEDT.SetInteger("Act", 0);
    }
}

public class CHumanState_Dead : CState<CHuman>
{
    int count = 0;
    public static CHumanState_Dead instance;
    public static CHumanState_Dead Instance()
    {
        if (instance == null)
            instance = new CHumanState_Dead();
        
        return instance;
    }

    public override void Enter(CHuman human)
    {
        //if (human.IsLocal )
        //    CSoundManager.Instance.PlaySE(EAudioList.SE_HumanScream);
        //死亡シン
        
        human.Dead = true;
        IsEnd = true;
        ID = (int)StateID.DEAD;
        CSoundManager.Instance.PlaySE(EAudioList.SE_HumanScream);
    }

    public override void Execute(CHuman human)
    {
        count++;
        if (human.LightOn == true)
        {
            human.ReLifeOn();

            if (human.IsReLifeScoreOn==false)
            {
                if (human.CarryHuman!=null)
                {
                    human.CarryHuman.GetComponent<CHuman>().ShumanScore.resuscitation++;
                    human.IsReLifeScoreOn = true;
                }
               
            }
           

        }
        if (human.CarryHuman!=null)
        {
           if( human.CarryHuman.GetComponent<CHuman>().CarryFlag==true)
           {
               human.transform.Translate(((human.CarryHuman.transform.position + human.CarryHuman.transform.forward*0.7f)- human.transform.position) * Time.deltaTime * human.HunmanTime * 10, Space.World);

               Quaternion TargetRotation_B = Quaternion.LookRotation(human.CarryHuman.transform.position-human.transform.position, Vector3.up);
              
               human.transform.rotation = Quaternion.Slerp(human.transform.rotation, TargetRotation_B, Time.deltaTime * human.HunmanTime * 10f);
           }
        }
        human.AnimatorEDT.SetInteger("State", 3);
        if(count==10)
        {
             human.Chikin.gameObject.SetActive(true);
        }
        else if (count == 80 )
        {
            CSoundManager.Instance.PlaySE(EAudioList.SE_HumanSwoon,CSoundManager.ESEChannelList._7,true);
           
        }
        else if (count>=150)
        {
            CSoundManager.Instance.StopSE(CSoundManager.ESEChannelList._7);
        }
    }

    public override void Exit(CHuman human)
    {
        //復活シン
        count = 0;
        human.Dead = false;
        human.IsReLifeScoreOn = false;
        CSoundManager.Instance.PlaySE(EAudioList.SE_HumanResurrection);
        human.Chikin.gameObject.SetActive(false);
    }
}

public class CHumanState_Item : CState<CHuman>
{

    public static CHumanState_Item instance;
    public static CHumanState_Item Instance()
    {
        if (instance == null)
            instance = new CHumanState_Item();
        return instance;
    }

    public override void Enter(CHuman human)
    {
        
        ID = (int)StateID.ITEM;
        IsEnd = false;

        human.AnimatorEDT.SetInteger("Act", 2);

        CSoundManager.Instance.PlaySE(EAudioList.SE_HumanGetItem);
    }

    public override void Execute(CHuman human)
    {
        if (human.Find.GetComponent<CFind>().Candle != null)
        {
            human.Candle = human.Find.GetComponent<CFind>().Candle;
            if (human.Candle.GetComponent<CCandle>() != null)
            {
                human.Candle.GetComponent<CCandle>().GetCandle();

            }
        }

        if (human.IsMotionEnd("Item"))
        {
            IsEnd = true;
        }
    }

    public override void Exit(CHuman human)
    {
        human.AnimatorEDT.SetInteger("Act", 0);
    }
}

public class CHumanState_Get : CState<CHuman>
{
    public static CHumanState_Get instance;
    public static CHumanState_Get Instance()
    {
        if (instance == null)
            instance = new CHumanState_Get();
        return instance;
    }

    public override void Enter(CHuman human)
    {

        ID = (int)StateID.GET;
        IsEnd = false;
        human.AnimatorEDT.SetInteger("Act", 2);
        CSoundManager.Instance.PlaySE(EAudioList.SE_HumanGetItem);
    }

    public override void Execute(CHuman human)
    {
        if (human.Find.GetComponent<CFind>().Candy != null)
        {
            human.Candy = human.Find.GetComponent<CFind>().Candy;

            if (human.Candy.GetComponent<CCandy>() != null)
            {
                human.Candy.GetComponent<CCandy>().GetCandy();
            }
        }
        if (human.IsMotionEnd("Item"))
        {
            IsEnd = true;
        }
    }

    public override void Exit(CHuman human)
    {
        human.AnimatorEDT.SetInteger("Act", 0);
    }
}

public class CHumanState_Set : CState<CHuman>
{
    public static CHumanState_Set instance;
    public static CHumanState_Set Instance()
    {
        if (instance == null)
            instance = new CHumanState_Set();
        return instance;
    }

    public override void Enter(CHuman human)
    {
        ID = (int)StateID.SET;
        human.AnimatorEDT.SetInteger("Act", 3);
        human.ShumanScore.setCandle++;
        IsEnd = false;
    }

    public override void Execute(CHuman human)
    {

        
        if (human.IsMotionEnd("Set"))
        {
            IsEnd = true;
        }
        
    }

    public override void Exit(CHuman human)
    {
        if (human.Candle != null)
        {
            human.Candle.GetComponent<CCandle>().PutCandle(human.transform.position + human.transform.forward, human);
            human.Find.GetComponent<CFind>().Candle = null;
        }

        //human.PStateMachine.ChangeState(CHumanState_Main.Instance());
        human.Candle = null;
        human.ItemFlag = false;
        human.AnimatorEDT.SetInteger("Act", 0);
        CSoundManager.Instance.PlaySE(EAudioList.SE_CandlePut);
    }
}

public class CHumanState_Use : CState<CHuman>
{
    public static CHumanState_Use instance;
    public static CHumanState_Use Instance()
    {
        if (instance == null)
            instance = new CHumanState_Use();
        return instance;
    }

    public override void Enter(CHuman human)
    {
        ID = (int)StateID.USE;
        human.AnimatorEDT.SetInteger("Act", 4);
        IsEnd = false;
    }

    public override void Execute(CHuman human)
    {

        if (human.IsMotionEnd("Use"))
        {
            IsEnd = true;
        }
       
    }

    public override void Exit(CHuman human)
    {
        human.Candy.GetComponent<CCandy>().ShotCandy(human.transform.position + human.transform.forward, human.transform.forward);
        human.CandyFlag = false;
        human.AnimatorEDT.SetInteger("Act", 0);
        CSoundManager.Instance.PlaySE(EAudioList.SE_CandyThrow);
        
    }
}

public class CHumanState_Bikuri : CState<CHuman>
{
    public static CHumanState_Bikuri instance;
    public static CHumanState_Bikuri Instance()
    {
        if (instance == null)
            instance = new CHumanState_Bikuri();
        return instance;
    }

    public override void Enter(CHuman human)
    {
        ID = (int)StateID.BIKURI;
        IsEnd = true;
        human.ExMark.Play();
    }

    public override void Execute(CHuman human)
    {
        

    }

    public override void Exit(CHuman human)
    {

    }
}

public class CHumanState_Move_Motion : CState<CHuman>
{
    public static CHumanState_Move_Motion instance;
    public static CHumanState_Move_Motion Instance()
    {
        if (instance == null)
            instance = new CHumanState_Move_Motion();

        return instance;
    }

    public override void Enter(CHuman human)
    {
    }

    public override void Execute(CHuman human)
    {
        human.AnimatorEDT.SetInteger("State", 1);
    }

    public override void Exit(CHuman human)
    {

    }
}

public class CHumanState_Dash_Motion : CState<CHuman>
{
    public static CHumanState_Dash_Motion instance;
    public static CHumanState_Dash_Motion Instance()
    {
        if (instance == null)
            instance = new CHumanState_Dash_Motion();

        return instance;
    }

    public override void Enter(CHuman human)
    {
        ID = (int)StateID.DASH;
    }

    public override void Execute(CHuman human)
    {

        human.MinusStrength(0.2f);
        human.AnimatorEDT.SetInteger("State", 2);
    }

    public override void Exit(CHuman human)
    {

    }
}

public class CHumanState_Carry_Motion : CState<CHuman>
{
    public static CHumanState_Carry_Motion instance;
    public static CHumanState_Carry_Motion Instance()
    {
        if (instance == null)
            instance = new CHumanState_Carry_Motion();

        return instance;
    }

    public override void Enter(CHuman human)
    {
        human.CarryFlag = true;
        ID = (int)StateID.CARRY;
    }

    public override void Execute(CHuman human)
    {
        if (human.Find.GetComponent<CFind>()!=null)
        {
            if (human.Find.GetComponent<CFind>().Body!=null)
            {
                human.Find.GetComponent<CFind>().Body.GetComponent<CHuman>().CarryHuman = human.gameObject;
            }
        }
        human.AnimatorEDT.SetInteger("Act", 1);
    }

    public override void Exit(CHuman human)
    {
        if (human.Find.GetComponent<CFind>() != null)
        {
            if (human.Find.GetComponent<CFind>().Body != null)
            {
                human.Find.GetComponent<CFind>().Body.GetComponent<CHuman>().CarryHuman = null;
            }
        }
        human.CarryFlag = false;
        human.AnimatorEDT.SetInteger("Act", 0);
    }
}