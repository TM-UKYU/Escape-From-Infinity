using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndYesButton_Script : MonoBehaviour
{
    // �{�^�����N���b�N�������̏���
    public void OnClick()
    {
        // Title ��I�����Ă��鎞
        if (PauseScript.sceneSelect)
        {
            // �������Ԃ��ғ�
            Time.timeScale = 1.0f;

            // �}�E�X�J�[�\����\������
            Cursor.visible = true;

            //�^�C�g���ɖ߂�
            SceneManager.LoadScene("Title");
        }
        // �Q�[���I����
        else
        {
            #if UNITY_EDITOR
                            //�Q�[�����I�������鏈��
                            UnityEditor.EditorApplication.isPlaying = false;
            #else
                        Application.Quit();
            #endif
        }
    }
}