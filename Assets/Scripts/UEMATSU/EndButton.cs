using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    public void OnClickStartButton()
    {
        //タイトルに戻るための処理
        SceneManager.LoadScene("NewTitle");
    }
}
