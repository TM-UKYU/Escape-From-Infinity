using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorl : MonoBehaviour
{
    //�N���A�p�l����ǉ�����
    public GameObject ClearCanvas;
    //�X�R�A���Ǘ�����I�u�W�F�N�g��ǉ�
    public GameObject ScoreManager;

    private void Start()
    {
        ScoreManager = GameObject.Find("ScoreManeger");
    }

    //�v���C���[�������蔻��ɓ��������̏���
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            ClearCanvas.SetActive(true);
            ScoreManager.GetComponent<ScoreManeger>().StopTime(true);
            ScoreManager.GetComponent<ScoreManeger>().DisplayScore();
        }
    }
}
