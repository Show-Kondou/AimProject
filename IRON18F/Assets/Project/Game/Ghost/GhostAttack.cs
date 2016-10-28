using UnityEngine;
using System.Collections;

public class GhostAttack : MonoBehaviour
{

    GhostMain m_ghost;

    // Use this for initialization
    void Start()
    {
        
        m_ghost = gameObject.transform.parent.gameObject.GetComponent<GhostMain>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Human")
        {
            if (m_ghost.IsAttack)
            {
                //プレイヤーへの攻撃
                other.GetComponent<CHuman>().Damage();
                m_ghost.GhostScore.knockOut++;
            }
        }
    
        
    }

}