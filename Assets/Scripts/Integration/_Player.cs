using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Player : MonoBehaviour
{
    // ���� �ϐ�//////////////////////////////////////
    public bool is_Grab;    //����

    ///////////////////////////////////

    // ���Y �ϐ�//////////////////////////////////////
    private float inputHorizontal;
    private float inputVertical;
    private Rigidbody rb;
    public GameObject camerapos;
    [SerializeField] private Vector3 velocity;              // �ړ�����
    [SerializeField] private float moveSpeed = 5.0f;

    //////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        // ���Y������ ///////////////////////
        rb = GetComponent<Rigidbody>();
        /////////////////////////////////////
    }

    // Update is called once per frame
    void Update()
    {
        // ���Y �X�V///////////////////////////////////////////////////////////////////////////////////////////
        ///�J�����̐��ʂɌ������Ĉړ�
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = Vector3.Scale(camerapos.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveForward = cameraForward * inputVertical + camerapos.transform.right * inputHorizontal;

        velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    // �����蔻�蒆////////////////////////////////////
    private void OnCollisionStay(Collision collision)
    {
        // ����///////////////////////////////
        if (Input.GetKey(KeyCode.E))
        {
            is_Grab = true;
        }
        else if (Input.GetKey(KeyCode.R))
        {
            is_Grab = false;
        }
        //////////////////////////////////////
    }

    // �����蔻�肩�痣�ꂽ��////////////////////////////
    private void OnCollisionExit(Collision collision)
    {
        // ����///////////////////////////////////
        // ���̂����ꂽ�Ƃ��A�P�x�����Ă΂��
        is_Grab = false;
        /////////////////////////////////////////
    }
}
