using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    //�{�^�����������炱�̊֐�������
    public void OnClickStartButton()
    {
        //�^�C�g���ɖ߂鏈��
        SceneManager.LoadScene("Title");
    }
}
