using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CAlterUI : MonoBehaviour
{
    /*!  Create
    *!   \details	アルターのUIいくつつくるか※1~6
    */
    public void Create(int num)
    {
        for(int i=0;i<num;i++)
        {
            GameObject back = Instantiate(m_alterBack);
            back.transform.parent = transform;
            back.name = "AlterBackUI"+i;
            back.transform.AddX(-i * 0.55f);
        }
    }
    /*!  Create
    *!   \details	着火(何個着火してるか)※0~6
    */
    public void Ignition(int num)
    {
        for(int i=0;i<6;i++)
        {
            if (i < num)
            {
                SafeDestroy(i);
                m_alterInstance[i] = Instantiate(m_alter);
                m_alterInstance[i].transform.parent = transform;
                m_alterInstance[i].name = "Alter" + i;
                m_alterInstance[i].transform.AddX(-i * 0.55f);

            }
            else
                SafeDestroy(i);
        }
    }

    #region Private

    [SerializeField]
    GameObject m_alterBack = null;

    [SerializeField]
    GameObject m_alter = null;

    GameObject[] m_alterInstance = null;
    // Use this for initialization
    void Start()
    {
        m_alterInstance = new GameObject[6];
    }
    void SafeDestroy(int index)
    {
        if (m_alterInstance[index] != null)
            Destroy(m_alterInstance[index]);
    }
    #endregion
}
