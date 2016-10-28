using UnityEngine;
using System.Collections;
using System;

static public class CInputComponent  {

    public enum KEY_ACTION{
            TRIGGER = 0,
            PRESS,
            RELEASE
    };

    public delegate void Func();
    public delegate void Func<in var>();

    /// <summary>
    ///　入力用メソッド
    /// </summary>
    /// <param name="keyaction">キーアクション</param>
    /// <param name="keycode">キーコード</param>
    /// <param name="func">実行するメソッド（可変）</param>
    static public bool InputAction(KEY_ACTION keyaction, KeyCode keycode, params Func[] func)
    {
        //キーアクションを選別
        switch (keyaction)
        {
            //プレス
            case KEY_ACTION.PRESS:
                if (Input.GetKey(keycode))
                {
                    
                    for (int i = 0; i < func.Length; i++)
                    {
                        func[i]();
                    }
                    return true;
                }
                break;
                //トリガー
            case KEY_ACTION.TRIGGER:
                if (Input.GetKeyDown(keycode))
                {
                    for (int i = 0; i < func.Length; i++)
                    {
                        func[i]();
                    }
                    return true;
                }
                break;
                //リリース
            case KEY_ACTION.RELEASE:
                if (Input.GetKeyUp(keycode))
                {
                    for (int i = 0; i < func.Length; i++)
                    {
                        func[i]();
                    }
                    return true;
                }
                break;

        }
        return false;
}

    internal static void InputAction(KEY_ACTION pRESS, KeyCode d, object v)
    {
        throw new NotImplementedException();
    }
}
