using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton_Script : MonoBehaviour
{
    // �{�^�����N���b�N�������̏���
    public void OnClick()
    {
        if (PauseScript.changeTitleorEndUI)
        {
            PauseScript.changeTitleorEndUI = false;
        }
        else
        {
            // �|�[�Y��ʂ����
            PauseScript.changePouse = true;
        }
    }
}