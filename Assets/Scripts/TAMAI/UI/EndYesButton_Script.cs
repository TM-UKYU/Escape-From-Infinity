using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndYesButton_Script : MonoBehaviour
{
    // ボタンをクリックした時の処理
    public void OnClick()
    {
        // 内部時間を稼働
        Time.timeScale = 1.0f;

        // マウスカーソルを表示する
        Cursor.visible = true;

        //タイトルに戻る
        SceneManager.LoadScene("NewTitle");
    }
}