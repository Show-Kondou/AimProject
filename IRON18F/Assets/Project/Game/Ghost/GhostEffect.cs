using UnityEngine;
using System.Collections;

public class GhostEffect : MonoBehaviour {


    [SerializeField]
    private CServer.CONTROL_TYPE m_ControlType;

    private GhostMain m_ghostMain;

    private Transform matPath_head;

    private Transform matPath_body;

    private SkinnedMeshRenderer meshrender_head;

    private SkinnedMeshRenderer meshrender_body;
    public float alha;

    [Header("ダッシュ中アルファ値")]
    public float m_dashAlha;

    public CServer.CONTROL_TYPE ControlType
    {
        get { return m_ControlType; }
        set { m_ControlType = value; }
    }
    // Use this for initialization
    void Start () {
        //ゲットー
        // m_ControlType = gameObject.GetComponent<>();
        m_ghostMain = gameObject.GetComponent<GhostMain>();
        matPath_head = this.transform.FindChild("polySurface16");
        meshrender_head = matPath_head.GetComponent<SkinnedMeshRenderer>();

        matPath_body = this.transform.FindChild("polySurface18");
        meshrender_body = matPath_body.GetComponent<SkinnedMeshRenderer>();


    }
	
	// Update is called once per frame
	void Update () {
        alha = meshrender_head.material.color.a;


        if (m_ghostMain.IsLocalPlayer)
        {
            m_ControlType = CServer.CONTROL_TYPE.GHOST;
        }
        else {
            m_ControlType = CServer.CONTROL_TYPE.HUMAN;
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

    /// <summary> Humanが選択された時の処理 </summary>
    void UpdateGhost()
    {

        if (meshrender_head.material.color.a < 1.0f)
        {
            meshrender_head.material.color += new Color(0, 0, 0, 0.05f);
            meshrender_body.material.color += new Color(0, 0, 0, 0.05f);
        }
    }


    void UpdateHuman()
    {

            switch (m_ghostMain.m_viewStatus)
            {
            //見えていない状態であれば　見えないようにする
                case GhostMain.GHOTS_VIEW_STATUS.INVISIBLE:
                //値がマイナスにしないようにする
                if (meshrender_head.material.color.a > 0)
                {
                    meshrender_head.material.color -= new Color(0, 0, 0, 0.1f);
                    meshrender_body.material.color -= new Color(0, 0, 0, 0.1f);
                }
               
                    
                    break;

                //見えている状態であれば　見えるようにする
                case GhostMain.GHOTS_VIEW_STATUS.VISIBLE:

                //ダッシュしているか/いないか
                if (m_ghostMain.m_Status == GhostMain.GHOTS_STATUS.DASH)
                {
                    //trueならば　ダッシュ中の最大アルファ値を適用させる
                    if (meshrender_head.material.color.a < m_dashAlha)
                    {
                        meshrender_head.material.color += new Color(0, 0, 0, 0.05f);
                        meshrender_body.material.color += new Color(0, 0, 0, 0.05f);
                    }
                   
                }
                else
                {
                    //false ならば最大アルファ値を１にする
                    if (meshrender_head.material.color.a < 1.0f)
                    {
                        meshrender_head.material.color += new Color(0, 0, 0, 0.05f);
                        meshrender_body.material.color += new Color(0, 0, 0, 0.05f);
                    }
                  
                }

                break;
            }
    }

    void IsDash()
    {

    }
}
