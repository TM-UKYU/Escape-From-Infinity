using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorl : MonoBehaviour
{
    //�N���A�p�l����ǉ�����
    public GameObject ClearCanvas;

    //�v���C���[�������蔻��ɓ��������̏���
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            ClearCanvas.SetActive(true);
        }
    }
}
