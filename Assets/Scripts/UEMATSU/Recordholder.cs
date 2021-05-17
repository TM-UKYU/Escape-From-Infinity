using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recordholder : MonoBehaviour
{
    //���̎��Ԃ��擾���邽�߂̃I�u�W�F�N�g
    public GameObject ScoreManaget = null;

    //����
    public Text HighTimeText = null;    // ���܂łň�ԑ��������^�C����\������e�L�X�g
    private float ClearTime;            // ���݂̃Q�[���̃X�R�A�^�C��
    private float HighSeconds = 1000;      // �n�C�X�R�A�̎���

    //�g�p��
    public Text HighTryText = null;     // ��ԃ��r�E�X�V�X�e�����g�����񐔂����Ȃ����̉񐔂�\������e�L�X�g
    private int ClearNum;               // ���݂̃Q�[���̎g�p��
    private int HighTryNum = 1000;         // ��ԏ��Ȃ��g�p������

    // Start is called before the first frame update
    void Start()
    {
        //���݂̃X�R�A���Ǘ����Ă���X�N���v�g���쐬
        ScoreManaget = GameObject.Find("ScoreManeger");

        if (!ScoreManaget) { Debug.Log("Record holder;ScoreManager �擾���s"); }

        //�n�C�X�R�A���擾����e�L�X�g���쐬
        //HighTimeText = GameObject.Find("HighTime").GetComponent<Text>();
        //HighTryText = GameObject.Find("HighTryNum").GetComponent<Text>();

        //if (!HighTimeText) { Debug.Log("Record holder:HighTimeText �擾���s"); }
        //else { HighTimeText.text = "High Score Time" + "\u00A0" + "00:00:00"; }//\u00A0 �c �m�[�u���C�N�X�y�[�X(�������s����Ȃ���)

        //if (!HighTryText) { Debug.Log("Record holder�FHighTryText �擾���s"); }
        //else { HighTryText.text = "High Score Try" + "\u00A0" + "0"; }

        //�n�C�X�R�A�����Z�b�g���Ă���f�o�b�O����(�Q�[���ɂ���ۂ͂����͍폜)
        PlayerPrefs.SetFloat("SECONDS", 1000);
        PlayerPrefs.SetInt("TRYNUM", 1000);

        //�n�C�X�R�A�̃��[�h
        HighSeconds = PlayerPrefs.GetFloat("SECONDS", 1000);
        HighTryNum = PlayerPrefs.GetInt("TRYNUM", 1000);
    }

    private void OnDestroy()
    {
        //�X�R�A��ۑ�
        PlayerPrefs.SetFloat("SECONDS", HighSeconds);
        PlayerPrefs.SetInt("TRYNUM", HighTryNum);

        PlayerPrefs.Save();
    }


    public void DisplayHigeScore()
    {
        HighTimeText.text = GetTime().x.ToString("00") + ":" + GetTime().y.ToString("00") + ":" + GetTime().z.ToString("00");
        HighTryText.text = HighTryNum.ToString("00") + "Num";
    }

    public void ChangeHigeScore()
    {
        ClearTime = ScoreManaget.GetComponent<ScoreManeger>().GetSeconds();
        ClearNum = ScoreManaget.GetComponent<ScoreManeger>().GetNum();

        if(HighSeconds>ClearTime)
        {
            HighSeconds = ClearTime;
        }

        if(HighTryNum>ClearNum)
        {
            HighTryNum = ClearNum;
        }
    }

    public Vector3 GetTime()
    {
        Vector3 time;

        time.x = HighSeconds / 3600;        // ��(h)
        time.y = HighSeconds % 3600 / 60;   // ��(m)
        time.z = HighSeconds % 60;          // �b(s)

        time.x = (int)time.x;
        time.y = (int)time.y;
        time.z = (int)time.z;

        return time;
    }
}
