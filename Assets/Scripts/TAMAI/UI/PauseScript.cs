using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [HideInInspector]
    public static bool isPouse;          // ��~����

    // �O���N���X�Ŏg�p
    public static bool changePouse;      // ��~������؂�ւ��� 
    public static bool changeEndUI;      // �I���m�F��ʂ�\�����邩�؂�ւ��� ()
    public static bool changeTutorialUI; // �`���[�g���A�����(��{����) ��\�����邩�؂�ւ��� (�O���N���X�Ŏg�p)

    // �\������UI
    [SerializeField]
    private GameObject   pauseUIPrefab;       // ��~���ɕ\������UI�I�u�W�F�N�g
    
    [SerializeField]
    private GameObject   endConfirmationUI;   // �{���ɏI�����ėǂ����m�F����ׂ�UI�I�u�W�F�N�g
    
    public GameObject    pauseUIInstance;      // PauseUI�̃C���X�^���X
    
    [SerializeField]
    private GameObject[] stopObjects;       // ��~����I�u�W�F�N�g �� timeScale �Ŏ~�߂��Ȃ�����

    [SerializeField]
    private Button[] interactableButtons;   // �ꎞ�I�ɑ��삪�o���Ȃ��悤�ɂ���{�^��

    // ���������Ŏg�p
    private GameObject endConfirmationUIInstance;   // EndUI�̃C���X�^���X

    private static bool OldChangeTutorialUI;

    public void SetActive_EndUIInstance( bool flg )
    {
        endConfirmationUIInstance.SetActive(flg);
    }

    // ������
    void Start()
    {
        // �t���O������
        isPouse             = false;
        changePouse         = false;
        changeEndUI         = false;
        changeTutorialUI    = false;
        OldChangeTutorialUI = false;
        endConfirmationUIInstance = GameObject.Instantiate(endConfirmationUI) as GameObject;

        pauseUIInstance.SetActive(false);
        endConfirmationUIInstance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //�@Escape�L�[ or UI��́~�{�^���������ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.Escape) || changePouse )
        {
            if (tutorial.isTutorialCanvas) { return; }

            isPouse = !isPouse;

            // �|�[�Y��ʂ�\������H
            if (isPouse)
            {
                // �|�[�Y��ʂɈړ�
                PauseMenu();
            }
            else
            {
                // �Q�[����ʂɈړ�
                GameMenu();
            }
        }
        
        if (isPouse)
        {
            // �؂�ւ��������1�x�������s
            if (changeEndUI != endConfirmationUIInstance.activeSelf)
            {
                //Debug.Log("EndUi �ؑցF" + changeEndUI);

                change_InteractableButtons(!changeEndUI);

                foreach (var ib in interactableButtons)
                {
                    ib.interactable = !changeEndUI;
                }

                endConfirmationUIInstance.SetActive(changeEndUI);
            }

            if (changeTutorialUI != OldChangeTutorialUI)
            {
                //Debug.Log("��{������ �ؑցF" + changeTutorialUI);
                change_InteractableButtons(!changeTutorialUI);
                OldChangeTutorialUI = changeTutorialUI;
            }
        }
    }

    // �{�^�����L������؂�ւ���
    void change_InteractableButtons(bool changeFlg)
    {
        foreach (var ib in interactableButtons)
        {
            ib.interactable = changeFlg;
        }
    }

    // �Q�[����ʂɈړ�
    protected void GameMenu()
    {
        if (!pauseUIInstance.activeSelf) { return; }

        // Pause��ʂ��\��
        pauseUIInstance.SetActive(false);

        // �������Ԃ��ғ�        
        Time.timeScale = 1.0f;

        // �A�^�b�`����Ă�������X�V����
        AttachUpdate(true);

        // �J�[�\������ʒ����ɌŒ�
        Cursor.lockState = CursorLockMode.Locked;

        /// �}�E�X�J�[�\�����\����
        Cursor.visible = false;

        changePouse = false;
    }

    // �|�[�Y��ʂɈړ�
    protected void PauseMenu()
    {
        if (pauseUIInstance.activeSelf) { return; }

        // Pause��ʂ�\��
        pauseUIInstance.SetActive(true);

        // �������Ԃ��~
        Time.timeScale = 0.0f;

        // �A�^�b�`����Ă�������X�V���Ȃ�
        AttachUpdate(false);

        // �}�E�X�J�[�\���̂����R�ɓ�������
        Cursor.lockState = CursorLockMode.None;

        // �}�E�X�J�[�\����\��
        Cursor.visible = true;
    }

    // stopObjects �� �w�肵���I�u�W�F�N�g�ɃA�^�b�`����Ă�������X�V����H
    // �� timeScale �ɂĎ~�܂�Ȃ����̗p
    public void AttachUpdate(bool updateFlg)
    {
        foreach (var stopObj in stopObjects)
        {
            MonoBehaviour[] monoBehaviours = stopObj.GetComponents<MonoBehaviour>();
            foreach (var monoBehaviour in monoBehaviours)
            {
                // �|�X�g�v���Z�X�ȊO��ύX
                if (!monoBehaviour.GetType().Name.StartsWith("PostProcess"))
                {
                    monoBehaviour.enabled = updateFlg;
                }
            }
        }
    }
}