using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelect_Yes_Script : MonoBehaviour
{
    // �{�^�����N���b�N�������̏���
    public void OnClick()
    {
        PauseScript.changeEndUI = true;
    }

    // Title ��I��
    public void Select_Title()
    {
        PauseScript.sceneSelect = true;
    }

    // End ��I��
    public void Select_End()
    {
        PauseScript.sceneSelect = false;
    }
}