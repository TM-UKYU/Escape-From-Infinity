using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    public void OnClickStartButton()
    {
        //�^�C�g���ɖ߂鏈��
        SceneManager.LoadScene("NewTitle");
    }

    void OnEnter()
    {
        //�{�^����\��
        this.gameObject.SetActive(true);
    }
}
