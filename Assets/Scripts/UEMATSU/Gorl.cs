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
            //�Q�[���N���A�̃L�����o�X��\��
            ClearCanvas.SetActive(true);
            //�o�ߎ��Ԃ��v������^�C�}�[���~�߂�
            ScoreManager.GetComponent<ScoreManeger>().StopTime(true);
            //���Ԃƒ���񐔂��Ǘ�����Script������o�ߎ��Ԃƒ���񐔂��擾����
            ScoreManager.GetComponent<ScoreManeger>().DisplayScore();
            //�}�E�X�J�[�\�����o��������
            Cursor.visible = true;
            //�}�E�X�J�[�\�������R�ɓ�������悤�ɂ���
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
