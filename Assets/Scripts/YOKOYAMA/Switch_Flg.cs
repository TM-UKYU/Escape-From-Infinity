using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Flg : MonoBehaviour
{
    public bool is_Open; //�h�A���J�������ǂ����̕ϐ�
    public bool ItemFlg = true;

    public KeyCode keyCode;�@//�ǂ̃L�[����͂��邩�̕ϐ�
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            //����̃{�^���������Ă���is_Open��false�̎��A�A�C�e���̃t���O��true
            if (Input.GetKey(keyCode) && !is_Open && ItemFlg) is_Open = true;
        }

    }
}
