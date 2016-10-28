using UnityEngine;
using System.Collections;

public class AltarManager : MonoBehaviour {

    public int m_maxAltarNum;

	public int m_winAltarNum;

    int m_nowLightAltar;


    
	// Use this for initialization
	void Start (){
        
	}
	
	// Update is called once per frame
	void Update () {
        m_nowLightAltar = 0;
        m_maxAltarNum = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            m_maxAltarNum++;

            if (transform.GetChild(i).GetComponent<Altar>().isLight)
            {
                m_nowLightAltar++;
            }
        }
        //全部灯せばゲームクリア
        if ( m_winAltarNum >= m_maxAltarNum)
        {
			//追加
			//ゲーム終了処理
			GameObject.Find( "EndMgr" ).GetComponent<CGameEnd>().WinHuman();
        }
	}
}
