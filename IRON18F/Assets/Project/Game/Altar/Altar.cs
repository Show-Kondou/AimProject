using UnityEngine;
using System.Collections;

public class Altar :MonoBehaviour {

    public bool IsLight ;

    /// <summary>　ゲッター　</summary>
    public bool isLight
    {
        get { return IsLight; }
        set { IsLight = value; }
    }

    void Start()
    {

        IsLight = false;
    }

    void Update()
    {

    }

    /// <summary>　ライトをオンにする </summary>
    public void OnLight()
    {
        
        IsLight = true;

    }
}
