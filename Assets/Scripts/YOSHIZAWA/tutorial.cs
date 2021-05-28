using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    //�`���[�g���A���p�l����ǉ�����
    public GameObject TutorialCanvas;

    public GameObject PointCanvas;

    public GameObject MebiusCanvas;

    // �O���N���X�Ŏg�p
    [HideInInspector]
    public static bool isTutorialCanvas; // TutorialCanvas ���\������ (true = �\����)

    // Start is called before the first frame update
    void Start()
    {
        PointCanvas.SetActive(false);
        MebiusCanvas.SetActive(false);

        isTutorialCanvas = TutorialCanvas.activeSelf;

        if (TutorialCanvas.activeSelf)
        {
            Time.timeScale = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �\�����Ă��鎞�� TAB �ŕ���
        if (Input.GetKeyDown(KeyCode.Tab) && TutorialCanvas.activeSelf)
        {
            TutorialCanvas_SetActive(false);
            PauseScript.changeTutorialUI = false;

            isTutorialCanvas = false;
            
            if (PauseScript.isPouse == false)
            {
                Time.timeScale = 1.0f;
            }
        }

        if (PauseScript.changeTutorialUI != isTutorialCanvas)
        {
            if (TutorialCanvas.activeSelf) { return; }
            
            TutorialCanvas_SetActive(true);
            isTutorialCanvas = true;
        }
    }

    //�`���[�g���A���̃L�����o�X��\���ؑ�
    public void TutorialCanvas_SetActive(bool isActive)
    {
        TutorialCanvas.SetActive(isActive);
        PointCanvas.SetActive(!isActive);
        MebiusCanvas.SetActive(!isActive);

        //TutorialCanvas.SetActive(!TutorialCanvas.activeSelf);
        //PointCanvas.SetActive(!PointCanvas.activeSelf);
        //MebiusCanvas.SetActive(!MebiusCanvas.activeSelf);
        //MebiusCanvas.SetActive(!MebiusCanvas.activeSelf);
    }
}
