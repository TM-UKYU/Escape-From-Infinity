using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{

    public GameObject LookPlayer;        //����v���C���[
    public GameObject PosPlayer;        //�v���C���[�̏ꏊ

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
        transform.position = Vector3.Lerp(transform.position, PosPlayer.transform.position, 0.5f);
        transform.LookAt(Vector3.Lerp(transform.forward + transform.position, LookPlayer.transform.position, 0.02f), Vector3.up);
    }
}