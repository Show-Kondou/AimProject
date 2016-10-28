using UnityEngine;
using System.Collections;

public class CAttckRotation : MonoBehaviour
{
    [SerializeField]
    public Vector3 rotate;
    [SerializeField]
    public bool loop_f;

    public int loop_num;
    // Use this for initialization
    void Start()
    {
        loop_num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (loop_num<9)
        {
            transform.Rotate(rotate.x, rotate.y, rotate.z);
        }
        //else
        //{
        //    this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        //    loop_num = 0;
        //}
        loop_num++;
    }

}
