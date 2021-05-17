using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    //�n�C�X�R�A��\������p�l��
    public GameObject RecordPanel;
    //�n�C�X�R�A�Ǘ�����I�u�W�F�N�g
    public Recordholder RecordHolder;
    //���̃X�R�A���Ǘ�����I�u�W�F�N�g
    public GameObject ScorePanel;

    public void OnClickStartButton()
    {
        //�X�R�A���O�̕����ǂ���ΕύX����
        RecordHolder.ChangeHigeScore();
        //�n�C�X�R�A��\�����鏈��
        RecordHolder.DisplayHigeScore();
        //�n�C�X�R�A�̃p�l����\������
        RecordPanel.SetActive(true);
        //�O�̃X�R�A�̃p�l��������
        ScorePanel.SetActive(false);
    }
}
