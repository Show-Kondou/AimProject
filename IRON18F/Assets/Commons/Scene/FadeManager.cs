using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum SCENE_RAVEL
{
    TITLE,
    GAME,
    RESULT,
    MATCHING
}


/// <summary>
/// シーン遷移時のフェードイン・アウトを制御するためのクラス .
/// </summary>
public class FadeManager : MonoBehaviour
{

    #region Singleton

    [SerializeField][Header("現在のシーン番号")]
    SCENE_RAVEL m_nowIndex;
    private static FadeManager instance;
    [SerializeField][Header("各シーン")]
    List<CBaseScene> m_scenes=null;
    [SerializeField][Header("各シーンの名前")]
    List<string> m_sceneNames=null;
    CBaseScene m_nowScene;
    string GetNextSceneName() { return m_sceneNames[(int)m_nowIndex]; }
    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(FadeManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    /// <summary>
    /// デバッグモード .
    /// </summary>
    public bool DebugMode = false;
    /// <summary>フェード中の透明度</summary>
    private float fadeAlpha = 0;
    /// <summary>フェード中かどうか</summary>
    private bool isFading = false;
    /// <summary>フェード色</summary>
    public Color fadeColor ;

    /// <summary>テクスチャ画像</summary>
    private Texture m_texture;

    /// <summary>isFadingのGet</summary>
    public bool IsFading
    {
        get { return isFading; }
    }


    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        m_texture = null;
        DontDestroyOnLoad(this.gameObject);
        m_nowScene = Instantiate(m_scenes[(int)m_nowIndex]);
        m_nowScene.name = m_sceneNames[(int)m_nowIndex];
        m_nowScene.transform.parent = transform;
    }

    public void OnGUI()
    {
        // Fade .
        if (this.isFading)
        {
            //色と透明度を更新して白テクスチャを描画 .
            this.fadeColor.a = this.fadeAlpha;
            GUI.color = this.fadeColor;

            if (m_texture)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), m_texture);
            }
            else {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
            }
        }

        if (this.DebugMode)
        {
            if (!this.isFading)
            {
                //Scene一覧を作成 .
                //(UnityEditor名前空間を使わないと自動取得できなかったので決めうちで作成) .
                List<string> scenes = new List<string>();
                scenes.Add("SampleScene");
                //scenes.Add ("SomeScene1");
                //scenes.Add ("SomeScene2");


                //Sceneが一つもない .
                if (scenes.Count == 0)
                {
                    GUI.Box(new Rect(10, 10, 200, 50), "Fade Manager(Debug Mode)");
                    GUI.Label(new Rect(20, 35, 180, 20), "Scene not found.");
                    return;
                }


                GUI.Box(new Rect(10, 10, 300, 50 + scenes.Count * 25), "Fade Manager(Debug Mode)");
                GUI.Label(new Rect(20, 30, 280, 20), "Current Scene : " + Application.loadedLevelName);

                int i = 0;
                foreach (string sceneName in scenes)
                {
                    if (GUI.Button(new Rect(20, 55 + i * 25, 100, 20), "Load Level"))
                    {
                        LoadLevel(SCENE_RAVEL.TITLE, 1.0f, null);
                    }
                    GUI.Label(new Rect(125, 55 + i * 25, 1000, 20), sceneName);
                    i++;
                }
            }
        }



    }

    /// <summary>
    /// 画面遷移 .
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    public void LoadLevel(SCENE_RAVEL scene, float interval, Texture texture)
    {
        m_texture = texture;
        m_nowIndex = scene;
        StartCoroutine(TransScene(m_nowIndex ,interval));
    }

    /// <summary>
    /// シーン遷移用コルーチン .
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransScene(SCENE_RAVEL scene, float interval)
    {
        //だんだん暗く .
        //GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = false;
        m_nowScene.FadeOutBefore();
        this.isFading = true;
        float time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }
        m_nowScene.FadeOutAfter();
        //シーン切替 .
        //Application.LoadLevel (scene);
        Debug.Log("buildID = " + scene);
        Destroy(m_nowScene.gameObject);
        m_nowScene = Instantiate(m_scenes[(int)scene]);
        m_nowScene.name = m_sceneNames[(int)scene];
        m_nowScene.transform.parent = transform;
        SceneManager.LoadScene(m_sceneNames[(int)scene]);


//        Pauser.Pause();
        //だんだん明るく .
        m_nowScene.FadeInBefore();
        time = 0;

        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }
        m_nowScene.FadeInAfter();
        //GameObject.Find("EventSystem").GetComponent<EventSystem>().enabled = true;
        this.isFading = false;

    }



    /// <summary>
    /// 画面遷移 .
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    /// <prama name= ''>
    public void LoadScene(SCENE_RAVEL scene, float interval,Texture texture)
    {
        m_texture = texture;
        m_nowIndex = scene;
        StartCoroutine(TransSceneID(interval));
    }

    /// <summary>
    /// シーン遷移用コルーチン .
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransSceneID( float interval)
    {
        //だんだん暗く .
        GameObject.Find("EventSystem").SetActive(false);
        m_nowScene.FadeOutBefore();
        float time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }
        m_nowScene.FadeOutAfter();
        //シーン切替 .
        //Application.LoadLevel (scene);
        Destroy(m_nowScene.gameObject);
        m_nowScene = Instantiate(m_scenes[(int)m_nowIndex]);
        m_nowScene.name = m_sceneNames[(int)m_nowIndex];
        m_nowScene.transform.parent = transform;
        Debug.Log("buildID = " + GetNextSceneName());
        SceneManager.LoadScene(GetNextSceneName());

        //だんだん明るく
        m_nowScene.FadeInBefore();
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }
        m_nowScene.FadeInAfter();
        GameObject.Find("EventSystem").SetActive(true);

    }
}

