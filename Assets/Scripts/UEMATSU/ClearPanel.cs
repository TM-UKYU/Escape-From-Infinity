using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPanel : MonoBehaviour
{
    //�p�l����o�^����
    public GameObject Panel;


    // Start is called before the first frame update
    void Start()
    {
        //�p�l��������
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        //�S�[���̃p�l�����o�����Ă���Œ���Enter�������ƃ^�C�g���ɖ߂鏈��
        if(Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("NewTitle");
        }
    }

    void OnEnter()
    {
        //�p�l����\��
        this.gameObject.SetActive(true);
    }
}
