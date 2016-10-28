using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CEditerCameraManager : MonoBehaviour {
    [SerializeField]
    [Header("カメラ")]
    List<GameObject> m_cameras =null;

    int m_index=0;
	// Use this for initialization
	void Start ()
    {
        UpdateActive();
	}
	void UpdateActive()
    {
        for (int i = 0; i < m_cameras.Count; i++)
            if (i == m_index)
                m_cameras[i].SetActive(true);
            else
                m_cameras[i].SetActive(false);
    }
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            m_index = (m_index + 1) % m_cameras.Count;
            UpdateActive();
        }
	
	}
}
