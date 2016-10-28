using UnityEngine;
using System.Collections;

public class CPodium : MonoBehaviour
{
    //終わってるかのフラグ
    public bool isEnd
    {
        get { return iTween.Count(gameObject) == 0; }
    }

    //表彰台の動きをスタート
    public void StartTween(float time, float score)
    {
        Vector3 temp = transform.position;
        temp.y += score / 10;
        Hashtable hash = new Hashtable();

        hash.Add("time", time);
        hash.Add(CiTweenEx.EASE_TYPE, iTween.EaseType.easeInExpo);
        hash.Add("position", temp);
        iTween.MoveTo(gameObject, hash);
    }
}
