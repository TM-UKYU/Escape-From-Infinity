using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameButtn_Script : MonoBehaviour
{
    // �{�^�����N���b�N�������̏���
    public void OnClick()
    {
        PauseScript.changeEndUI = true;
    }
}