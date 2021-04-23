using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public GameObject LookPlayer;        //見るプレイヤー
    public GameObject PosPlayer;        //プレイヤーの場所

    public GameObject LookTarget;        //見るプレイヤー
    public GameObject PosTarget;        //プレイヤーの場所

    public float rotateSpeed = 3.0f; // 回転させるスピード

    bool targetChange = false;

    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            targetChange = false;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            targetChange = true;
        }

        if (targetChange)
        {
            transform.LookAt(Vector3.Lerp(transform.forward + transform.position, LookTarget.transform.position, 0.02f), Vector3.up);
            transform.position = Vector3.Lerp(transform.position, PosTarget.transform.position, 0.04f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, PosPlayer.transform.position, 0.5f);
            transform.LookAt(Vector3.Lerp(transform.forward + transform.position, LookPlayer.transform.position, 0.02f), Vector3.up);
        }


    }


}