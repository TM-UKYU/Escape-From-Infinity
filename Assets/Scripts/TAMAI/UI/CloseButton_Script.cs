using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton_Script : MonoBehaviour
{
    // ボタンをクリックした時の処理
    public void OnClick()
    {
        PauseScript.changePouse = true;
    }
}