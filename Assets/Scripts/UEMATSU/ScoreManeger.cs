using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManeger : MonoBehaviour
{
    //�����������Ԃ�\������e�L�X�g
    public Text TimeText;
    private float time = 0;
    //���r�E�X�V�X�e�����g�����񐔂�\������e�L�X�g
    public Text TryText = null;
    private int TryNum = 0;

    //���Ԃ̃J�E���g���~�߂邽�߂̕ϐ�
    private bool FreezTime = false;

    // Start is called before the first frame update
    void Start()
    {
        //���Ԃƒ���񐔂̃��Z�b�g
        time = 0;
        TryNum = 0;
        FreezTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԃ�i�߂Ȃ��Ƃ��͂����ŋA��
        if (FreezTime) { return; }

        //���Ԃ�i�߂�
        time += Time.deltaTime;
    }

    public void StopTime(bool isStop)
    {
        //���Ԃ̃J�E���g���~�߂�
        FreezTime = isStop;
    }

    public void AddTryNum()
    {
        //����񐔂���񑝂₷
        TryNum += 1;
    }

    public void DisplayScore()
    {
        //���Ԃƒ���̉񐔂�UI�̃e�L�X�g�ɕ\������
        TimeText.text = time.ToString("N0")+"Count";
        TryText.text = TryNum.ToString("N0")+"Num";
    }
}
