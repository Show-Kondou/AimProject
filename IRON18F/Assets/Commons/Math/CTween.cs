//!  CTween.cs
/*!
 * \details 簡易演出スクリプト
 * \author  Shoki Tsuneyama
 * \date    15.11.04　新規作成
 * \date    15.11.05　地震を修正
 */

using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CTween : MonoBehaviour 
{
    //! 挙動
    public enum EAnimationTYpe
    {
        Move,
        Rotate,
        Scale,
        Color,
        Shake,
        Wait,
		MoveBy,
    };

    //! 動作パラメータ一覧
    [System.Serializable]
    public class TweenData
    {
        //!　インスペクタ表示用変数
        public bool m_isInspecter = true;
        public bool m_isDetail = false;

        //! 内部データ
        public EAnimationTYpe m_animationType;
        public iTween.EaseType m_easeType = iTween.EaseType.easeInOutCubic;
        public iTween.LoopType m_loopType = iTween.LoopType.none;
        public bool m_isSomeBefore = false;

        public Vector3 m_angleAxis = Vector3.forward;
        public Vector3 m_magnitude = Vector3.up;
        public Vector3 m_pos = Vector3.zero;
        public Vector3 m_scale = Vector3.one;
        public Color m_color = Color.white;

        public float m_time = 1;
    }

    //! 配列の要素数
    public int m_arraySize = 0;

    //! 全体設定用のフラグ
    public bool m_isLocal = false;
    public bool m_isAwake = true;

    //! トゥイーン配列
    public TweenData[] m_tweenData = new TweenData[3];

	// Use this for initialization
	void Start () 
    {
        
        if (m_isAwake)
            Run();

        Pauser.Pause();
    }

    public void Run()
    {
        float delay = 0;
		Vector3 end = transform.position;
        for (int i = 0; i < m_tweenData.Length; ++i)
        {
            //float time = delay + m_tweenData[i].m_time;
            Hashtable hash = new Hashtable();

            var obj = m_tweenData[i];

			if(m_tweenData[i].m_animationType != EAnimationTYpe.MoveBy)
	            hash.Add("islocal", m_isLocal);
            hash.Add("looptype", obj.m_loopType.ToString());
            hash.Add("time", m_tweenData[i].m_time);
            hash.Add("delay", delay);
            hash.Add(CiTweenEx.EASE_TYPE, obj.m_easeType);

            switch (m_tweenData[i].m_animationType)
            {
                case EAnimationTYpe.Move:
                    hash.Add("position", obj.m_pos);

					if( m_isLocal )
						end = transform.position + obj.m_pos;
					else
						end = obj.m_pos;
                    iTween.MoveTo(gameObject, hash);
                    break;
                case EAnimationTYpe.Color:
                    hash.Add("color", obj.m_color);
                    iTween.ColorTo(gameObject, hash);
                    break;
                case EAnimationTYpe.Rotate:
                    hash.Add("rotation", obj.m_angleAxis);
                    iTween.RotateTo(gameObject, hash);
                    break;
                case EAnimationTYpe.Scale:
                    hash.Add("scale", obj.m_scale);
                    iTween.ScaleTo(gameObject, hash);
                    break;
                case EAnimationTYpe.Shake:
                    hash.Add("amount", obj.m_magnitude);
                    iTween.ShakePosition(gameObject, hash);
                    break;
				case EAnimationTYpe.MoveBy:
					hash.Add("position", end + obj.m_pos);
						end += obj.m_pos;
					iTween.MoveTo(gameObject, hash);
					break;
                default:
                    break;
            }


            //ディレイの更新
            if (i + 1 < m_tweenData.Length &&
                m_tweenData[i + 1].m_isSomeBefore == false)
                delay += m_tweenData[i].m_time;
        }
    }

    public void Stop()
    {
        iTween.Stop(this.gameObject);
    }

    public bool isEnd
    {
        get { return iTween.Count(gameObject) == 0; }
    }
}
