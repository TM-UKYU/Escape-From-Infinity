using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShader : MonoBehaviour
{
    public ObjectScript objScript;

    // �V�F�[�_�[��ύX������
    private bool changeShaderFlg;

    // Start is called before the first frame update
    void Start()
    {
        changeShaderFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ���C�������邩�͂�ł���
        if (objScript.RayCheck() || objScript.GetIsCatch())
        {
            if (!changeShaderFlg)
            {
                // �S�Ẵ}�e���A���̃V�F�[�_�[��LimOutLine�ɕύX
                for (int i = 0; i < gameObject.GetComponent<Renderer>().materials.Length; i++)
                {
                    gameObject.GetComponent<Renderer>().materials[i].shader = Shader.Find("Custom/LimOutline");
                    gameObject.GetComponent<Renderer>().materials[i].SetColor("_OutlineColor", Color.green);
                }
                changeShaderFlg = true;
            }
        }
        else
        {
            // �S�Ẵ}�e���A���̃V�F�[�_�[��Standard�ɕύX
            for (int i = 0; i < gameObject.GetComponent<Renderer>().materials.Length; i++)
            {
                gameObject.GetComponent<Renderer>().materials[i].shader = Shader.Find("Standard");
            }
            changeShaderFlg = false;
        }
    }
}
