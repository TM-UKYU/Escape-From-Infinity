using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    //�{�^���������΂��̊֐����Ă�
    public void OnClickStartButton()
    {
        //�X�e�[�W1�����[�h���邽�߂̏���
        SceneManager.LoadScene("StageOne");
    }
}
