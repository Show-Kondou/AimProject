using UnityEngine;
using System.Collections;

public class CFind : MonoBehaviour {
    private bool m_findBodyFlag;
    private bool m_findCandleFlag;
    private bool m_findCandyFlag;
    private GameObject m_body;
    private GameObject m_candle;
    private GameObject m_candy;
    public bool FindBodyFlag
    {
        get { return m_findBodyFlag; }
        set { m_findBodyFlag = value; }
    }
    public bool FindCandleFlag
    {
        get { return m_findCandleFlag; }
        set { m_findCandleFlag = value; }
    }
    public bool FindCandyFlag
    {
        get { return m_findCandyFlag; }
        set { m_findCandyFlag = value; }
    }
    public GameObject Body
    {
        get { return m_body; }
        set { m_body = value; }
    }

    public GameObject Candle
    {
        get { return m_candle; }
        set { m_candle = value; }
    }

    public GameObject Candy
    {
        get { return m_candy; }
        set { m_candy = value; }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerStay(Collider collider)
    {
        if (collider.transform.tag == "Candle")
        {
            if (collider.GetComponent<CCandle>().IsFire == false)
            {
                m_findCandleFlag = true;
                m_candle = collider.gameObject;
            }
        }

        if (collider.transform.tag == "Candy")
        {
            if (collider.GetComponent<CCandy>().m_isShot == false)
            {
                m_findCandyFlag = true;
                m_candy = collider.gameObject;
            }
        }
    }

    public void OnTriggerEnter(Collider collider)
    {

        if (collider.transform.tag == "Human")
        {
            if (collider.GetComponent<CHuman>().Dead == true)
            {
                m_findBodyFlag = true;
                m_body = collider.gameObject;
            }
        }
    }

    public void OnTriggerExit(Collider collider)
    {

        if (collider.transform.tag == "Human")
        {
            if (collider.GetComponent<CHuman>().Dead == true)
            {
                m_findBodyFlag = false;
            }
        }
    }

}
