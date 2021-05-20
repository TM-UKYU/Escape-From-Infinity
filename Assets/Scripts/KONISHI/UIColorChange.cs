using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColorChange : MonoBehaviour
{
    public ObjectScript objScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���C�������邩�͂�ł���
        if (objScript.RayCheck() || objScript.GetIsCatch())
        {
            // GameObject�̎擾
            GameObject target = GameObject.Find("RayImage");
            // Image�̎擾
            Image image = target.GetComponent<Image>();
            // �F�̕ύX
            image.color = Color.green;
        }
        else
        {
            // GameObject�̎擾
            GameObject target = GameObject.Find("RayImage");
            // Image�̎擾
            Image image = target.GetComponent<Image>();
            // �F�̕ύX
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
