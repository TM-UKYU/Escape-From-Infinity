using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool is_Grab;

    // 3D�̏ꍇ�̓����蔻��
    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            is_Grab = true;
        }
        else if(Input.GetKey(KeyCode.R))
        {
            is_Grab = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // ���̂����ꂽ�Ƃ��A�P�x�����Ă΂��
        is_Grab = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
