using UnityEngine;
using System.Collections;

public class CTest : MonoBehaviour
{
    [SerializeField]
    float speed=0;
    bool anan = false;
	// Use this for initialization
	void Start ()
    {

    }
    bool muteTest = false;
	// Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            anan^=true;
            if (anan)
                CSoundManager.Instance.PlayBGM(EAudioList.BGM_Game2, CSoundManager.EFadeType.Cross, 1);
            else
                CSoundManager.Instance.PlayBGM(EAudioList.BGM_Title, CSoundManager.EFadeType.Cross, 1);
        }
         if (Input.GetKeyDown(KeyCode.Q))
            CSoundManager.Instance.ChangeVolBGM(0.3f, 1, true);
        if(Input.GetKeyDown(KeyCode.P))
        {
            muteTest ^= true;
            CSoundManager.Instance.MuteBgm(muteTest);
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            speed +=0.05f;
            if(speed >1)
                speed =1;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            speed -=0.05f;
            if(speed <0)
                speed =0;
        }
        if (Input.GetKeyDown(KeyCode.F))
            CSoundManager.Instance.NormalSpeedBGM(1, true);
        if (Input.GetKeyDown(KeyCode.G))
            CSoundManager.Instance.RewindBGM(speed, 1, true);
        if (Input.GetKeyDown(KeyCode.H))
            CSoundManager.Instance.SlowBGM(speed, 1, true);
        if (Input.GetKeyDown(KeyCode.J))
            CSoundManager.Instance.TempoUpBGM(speed, 1, true);
        if (Input.GetKeyDown(KeyCode.M))
            CSoundManager.Instance.PlaySE(EAudioList.SE_GhostLaugh);

        if (Input.GetKeyDown(KeyCode.C))
            CSoundManager.Instance.PlaySE(EAudioList.SE_HumanExclamation, CSoundManager.ESEChannelList._1,true);
        if (Input.GetKeyDown(KeyCode.V))
            CSoundManager.Instance.PlaySE(EAudioList.SE_Finish, CSoundManager.ESEChannelList._2, false);
            
	}
}
