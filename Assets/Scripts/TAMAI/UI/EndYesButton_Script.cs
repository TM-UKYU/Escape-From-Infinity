using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndYesButton_Script : MonoBehaviour
{
    // �{�^�����N���b�N�������̏���
    public void OnClick()
    {
        // �������Ԃ��ғ�
        Time.timeScale = 1.0f;

        // �}�E�X�J�[�\����\������
        Cursor.visible = true;

        //�^�C�g���ɖ߂�
        SceneManager.LoadScene("NewTitle");
    }
}