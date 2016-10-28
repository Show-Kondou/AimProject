using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EGridSize
{
    _5x6 = 1,
    _10x12 = 2,
    _20x24 = 3,
    _40x48 = 4,
};
public class CStageGrid : MonoBehaviour
{
    [SerializeField][Header("グリッド横")]
    GameObject m_yoko=null;
    [SerializeField]
    [Header("グリッド縦")]
    GameObject m_tate =null;

    [SerializeField]
    [Header("グリッドサイズ")]
    EGridSize m_size = EGridSize._20x24;

    const int GRID_HEIGHT = 49;
    const int GRID_WIDTH = 41;
    GameObject[] m_yokos = new GameObject[GRID_WIDTH];
    GameObject[] m_tates = new GameObject[GRID_HEIGHT];
    bool m_isActive =true;
	// Use this for initialization
	void Start ()
    {
        CreateGrid();
	}
	void CreateGrid()
    {
        for(int y=0; y<GRID_HEIGHT;y++)
        {
            m_tates[y] = Instantiate(m_tate);
            m_tates[y].transform.AddX( y*0.5f - (GRID_HEIGHT/4));
            m_tates[y].name = ("Tate") + y;
            m_tates[y].transform.parent = transform;

        }
        for(int x=0; x<GRID_WIDTH;x++)
        {
            m_yokos[x] = Instantiate(m_yoko);
            m_yokos[x].transform.AddZ(x * 0.5f - (GRID_WIDTH /4));
            m_yokos[x].name = ("Yoko") + x;
            m_yokos[x].transform.parent = transform;
        }
    }
    //呼ぶたびにオンオフ切り替わる
    public void SwitchDraw()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            m_isActive ^= true;
            gameObject.SetActive(m_isActive);
        }
    }
	// Update is called once per frame
	void Update ()
    {
	}
}
