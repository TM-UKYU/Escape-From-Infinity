using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPanel : MonoBehaviour
{
    //�p�l����o�^����
    public GameObject Panel;


    // Start is called before the first frame update
    private void Start()
    {
        //�p�l��������
        this.gameObject.SetActive(false);
    }
}
