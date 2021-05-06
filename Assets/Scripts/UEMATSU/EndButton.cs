using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    public void OnClickStartButton()
    {
        //タイトルに戻る処理
        SceneManager.LoadScene("NewTitle");
    }

    void OnEnter()
    {
        //ボタンを表示
        this.gameObject.SetActive(true);
    }
}
