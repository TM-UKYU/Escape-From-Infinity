using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    //�`���[�g���A���p�l����ǉ�����
    public GameObject TutorialCanvas;


    

// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�`���[�g���A���̃L�����o�X��\��
            TutorialCanvas.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TutorialCanvas.SetActive(false);
        }
    }
}
