

using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(CTween))]



//!  CTweenDataEditor.cs
/*!
 * \details CTweenDataEditor	CTweenGUI拡張エディタ
 * \author  Shoki Tsuneyama
 * \date    2016/10/11	新規作成

 */
public class CTweenDataEditor
    : Editor
{

    /*!  CreateArray
    *!   \details	配列分の入力スペースを確保
    *!
    *!   \return	
    */
    CTween.TweenData[] CreateArray(CTween tween)
    {
        CTween.TweenData[] data = new CTween.TweenData[tween.m_arraySize];
        for (int i = 0; i < data.Length; ++i)
        {
            try
            {
                data[i] = tween.m_tweenData[i];
            }
            catch
            {
                data[i] = new CTween.TweenData();
            }
        }
        for (int i = 0; i < data.Length; ++i)
        {
            if (data[i] == null)
                data[i] = new CTween.TweenData();
        }
        return data;
    }

    //  エディタ内部
    public override void OnInspectorGUI()
    {
        CTween tween = target as CTween;
        tween.m_isLocal = EditorGUILayout.Toggle("ローカルモード", tween.m_isLocal);
        tween.m_isAwake = EditorGUILayout.Toggle("自動開始", tween.m_isAwake);

        tween.m_arraySize = EditorGUILayout.IntField("サイズ", tween.m_arraySize);

        if (tween.m_tweenData.Length != tween.m_arraySize) 
        {
           tween.m_tweenData = CreateArray(tween);
        }
        foreach( var obj in tween.m_tweenData)
        {
            if(obj==null)
            {
                tween.m_tweenData = CreateArray(tween);
                break;
            }
        }

        for (int i = 0; i < tween.m_tweenData.Length; ++i)
        {
            tween.m_tweenData[i].m_isInspecter = EditorGUILayout.Foldout(tween.m_tweenData[i].m_isInspecter, "トゥイーン" + i);
            if (tween.m_tweenData[i].m_isInspecter == false)
                continue;

            tween.m_tweenData[i].m_animationType = (CTween.EAnimationTYpe)EditorGUILayout.EnumPopup("　動作",  tween.m_tweenData[i].m_animationType);
			string op = tween.m_isLocal ? "(ローカル座標)" : "(ワールド座標)";

            switch (tween.m_tweenData[i].m_animationType)
            {
                case CTween.EAnimationTYpe.Move:
                    tween.m_tweenData[i].m_pos = EditorGUILayout.Vector3Field("　目的地"+op, tween.m_tweenData[i].m_pos);
                    break;
                case CTween.EAnimationTYpe.Rotate:
                    tween.m_tweenData[i].m_angleAxis = EditorGUILayout.Vector3Field("　各軸の角度", tween.m_tweenData[i].m_angleAxis);
                    break;
                case CTween.EAnimationTYpe.Scale:
                    tween.m_tweenData[i].m_scale = EditorGUILayout.Vector3Field("　拡縮倍率", tween.m_tweenData[i].m_scale);
                    break;
                case CTween.EAnimationTYpe.Shake:
                    tween.m_tweenData[i].m_magnitude = EditorGUILayout.Vector3Field("　揺れの強さ", tween.m_tweenData[i].m_magnitude);
                    break;
                case CTween.EAnimationTYpe.Wait:
                    break;
                case CTween.EAnimationTYpe.Color:
                    tween.m_tweenData[i].m_color = EditorGUILayout.ColorField("　色" , tween.m_tweenData[i].m_color);
                    break;
				case CTween.EAnimationTYpe.MoveBy:
					tween.m_tweenData[i].m_pos = EditorGUILayout.Vector3Field("　相対値"+op, tween.m_tweenData[i].m_pos);
					break;
            }
            string timename = "　時間";
            if (tween.m_tweenData[i].m_animationType == CTween.EAnimationTYpe.Wait)
                timename = "　停止時間";
            tween.m_tweenData[i].m_time = EditorGUILayout.FloatField(timename, tween.m_tweenData[i].m_time);

            tween.m_tweenData[i].m_isDetail = EditorGUILayout.Foldout(tween.m_tweenData[i].m_isDetail, "　詳細");
            if (tween.m_tweenData[i].m_isDetail == false)
            {
                EditorGUILayout.Space();
                continue;
            }
            tween.m_tweenData[i].m_easeType = (iTween.EaseType)EditorGUILayout.EnumPopup("　補完方法", tween.m_tweenData[i].m_easeType);
            tween.m_tweenData[i].m_loopType = (iTween.LoopType)EditorGUILayout.EnumPopup("　ループ設定", tween.m_tweenData[i].m_loopType);
            tween.m_tweenData[i].m_isSomeBefore = EditorGUILayout.Toggle("　一つ前と同時に開始", tween.m_tweenData[i].m_isSomeBefore);

            EditorGUILayout.Space();

        }

        EditorUtility.SetDirty(target);
    }    

}

#endif
