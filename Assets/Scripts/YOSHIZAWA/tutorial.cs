using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    //�`���[�g���A���p�l����ǉ�����
    public GameObject TutorialCanvas;

    public GameObject PointCanvas;

    public GameObject MebiusCanvas;


// Start is called before the first frame update
    void Start()
    {
        PointCanvas.SetActive(false);


        MebiusCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //�`���[�g���A���̃L�����o�X��\��
            TutorialCanvas.SetActive(!TutorialCanvas.activeSelf);

            PointCanvas.SetActive(!PointCanvas.activeSelf);


            MebiusCanvas.SetActive(!MebiusCanvas.activeSelf);

        }
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    TutorialCanvas.SetActive(false);


        //    PointCanvas.SetActive(true);


        //    MebiusCanvas.SetActive(true);
        //}
    }
}
