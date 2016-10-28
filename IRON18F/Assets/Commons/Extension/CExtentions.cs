//!  CExtentions.cs
/*!
 * \details Unityのクラスの拡張
 * \author  Shoki Tsuneyama
 * \date    2015/10/15　新規作成
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

//! Transform 拡張
public static class CExtentionsForTransform
{
    //! 個別位置の設定
    public static void SetX(this Transform transform, float value)
    {
        var pos = transform.position;
        pos.x = value;
        transform.position = pos;
    }
    public static void SetY(this Transform transform, float value)
    {
        var pos = transform.position;
        pos.y = value;
        transform.position = pos;
    }
    public static void SetZ(this Transform transform, float value)
    {
        var pos = transform.position;
        pos.z = value;
        transform.position = pos;
    }

    //! 個別位置の加算
    public static void AddX(this Transform transform, float value)
    {
        var pos = transform.position;
        pos.x += value;
        transform.position = pos;
    }
    public static void AddY(this Transform transform, float value)
    {
        var pos = transform.position;
        pos.y += value;
        transform.position = pos;
    }
    public static void AddZ(this Transform transform, float value)
    {
        var pos = transform.position;
        pos.z += value;
        transform.position = pos;
    }
    public static void AddXYZ(this Transform transform, Vector3 value)
    {
        var pos = transform.position;
        pos += value;
        transform.position = pos;
    }

    //! 個別回転の設定
    public static void SetLocalRotationX(this Transform transform, float value)
    {
        var localEulerAngles = transform.localEulerAngles;
        localEulerAngles.x = value;
        transform.localEulerAngles = localEulerAngles;
    }
    public static void SetLocalRotationY(this Transform transform, float value)
    {
        var localEulerAngles = transform.localEulerAngles;
        localEulerAngles.y = value;
        transform.localEulerAngles = localEulerAngles;
    }
    public static void SetLocalRotationZ(this Transform transform, float value)
    {
        var localEulerAngles = transform.localEulerAngles;
        localEulerAngles.z = value;
        transform.localEulerAngles = localEulerAngles;
    }

    //! メソッド
    public static void MoveTowards(this Transform transform, Vector3 target, float maxDistanceDelta)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, maxDistanceDelta);
    }
    public static void LerpPos(this Transform transform, Vector3 v0, Vector3 v1, float t)
    {
        transform.position = Vector3.Lerp(v0, v1, t);
    }
    public static void MoveTowardsLocal(this Transform transform, Vector3 target, float maxDistanceDelta)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, maxDistanceDelta);
    }
    public static void MoveTowards2D(this Transform transform, Vector2 target, float maxDistanceDelta)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, maxDistanceDelta);
    }
    public static void MoveTowardsLocal2D(this Transform transform, Vector2 target, float maxDistanceDelta)
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, target, maxDistanceDelta);
    }

}

//! Vectorクラスの拡張
public static class CExtentionsVector
{

    public static Vector2 XZ(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }
}

//! MonoBehaviourの拡張
public static class CExtentionsForMonoBehaviour
{
    //TODO 未実装
}

//! Debug　機能の拡張
public static class CExtentionsForDebug
{

}

