using UnityEngine;
using System.Collections;

public class DummyGhost : MonoBehaviour {

    public enum GHOTS_VIEW_STATUS
    {
        VISIBLE,
        INVISIBLE
    };


    private GameObject ghost=null;

    private GhostEffect m_GhostEffect=null;

    private CServer.CONTROL_TYPE m_ControlType;

    public GHOTS_VIEW_STATUS m_viewStatus;

    private Transform matPath;

    private MeshRenderer meshrender;

    [SerializeField]
    private int lifeTime;
    [SerializeField]
    private int life;

    public bool IsAct=false;

    public float alha;

    //[Header("ダッシュ中アルファ値")]
    //public float m_dashAlha;
    // Use this for initialization
    void Start () {

        //ゲットー
        // m_ControlType = gameObject.GetComponent<>();
        m_viewStatus = GHOTS_VIEW_STATUS.INVISIBLE;
        matPath = this.transform.FindChild("polySurface21");
        meshrender = matPath.GetComponent<MeshRenderer>();
        life = lifeTime;
        GetComponent<CapsuleCollider>().enabled = false;
        
	}
	
	// Update is called once per frame
	void Update () {

        if(ghost==null)
        {
            ghost = GameObject.Find("Ghost(Clone)");
            if(ghost==null)
            {
                ghost = GameObject.Find("Ghost");
            }
            
        }
        else
        {
            m_GhostEffect = ghost.GetComponent<GhostEffect>();
        }
        
        
        alha = meshrender.material.color.a;
        if (m_GhostEffect!=null)
        {
            m_ControlType = m_GhostEffect.ControlType;
        }

        if (life > 0 && IsAct==true)
        {
            life--;
        }

        if(IsAct==false)
        {
            if (meshrender.material.color.a > 0)
            {
                meshrender.material.color -= new Color(0, 0, 0, 0.1f);
            }
        }

        switch (m_ControlType)
        {
            case CServer.CONTROL_TYPE.GHOST:
                //常に見える状態
                UpdateGhost();
                break;
            case CServer.CONTROL_TYPE.HUMAN:
                //見えない状態
                UpdateHuman();

                break;

        }
	}

    void UpdateGhost()
    {
        if (IsAct==true)
        {
            if (life == 0)
            {
                IsAct = false;
                GetComponent<CapsuleCollider>().enabled = false;
                
            }
            if (meshrender.material.color.a < 1)
            {
                meshrender.material.color += new Color(0, 0, 0, 0.1f);

            }
        }
        else
        {
            life = lifeTime;
            if (meshrender.material.color.a > 0)
            {
                meshrender.material.color -= new Color(0, 0, 0, 0.1f);

            }
        }
       
    }


    /// <summary> Humanが選択された時の処理 </summary>
    void UpdateHuman()
    {
        if (IsAct == true)
        {
            if (life == 0)
            {
                IsAct = false;
                GetComponent<CapsuleCollider>().enabled = false;
            }
          
        }
        else
        {
            life = lifeTime;
        }
         switch (this.m_viewStatus)
         {
             //見えていない状態であれば　見えないようにする
             case GHOTS_VIEW_STATUS.INVISIBLE:
                 //値がマイナスにしないようにする
                 if (meshrender.material.color.a > 0)
                 {
                     meshrender.material.color -= new Color(0, 0, 0, 0.1f);

                 }

                 break;

             //見えている状態であれば　見えるようにする
             case GHOTS_VIEW_STATUS.VISIBLE:
                 if (IsAct == true)
                 {
                     if (meshrender.material.color.a < 1.0f)
                     {
                         meshrender.material.color += new Color(0, 0, 0, 0.05f);
                     }
                 }
                 else
                 {
                     if (meshrender.material.color.a > 0)
                     {
                         meshrender.material.color -= new Color(0, 0, 0, 0.1f);

                     }
                 }

                 break;
         }
        
       
    }
}
