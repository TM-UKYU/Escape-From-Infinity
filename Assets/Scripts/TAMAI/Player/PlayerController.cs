using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //�V�[���J�ڂ�����ꍇ�ɕK�v

public class PlayerController : Singleton<PlayerController>
{
    //////////////////////////
    /// �ϐ��錾
    ////////////////////////

    // �C���X�y�N�^�[�ŕҏW�\
    [SerializeField] private float  jumpPower = 5.0f;    // �W�����v��
    [SerializeField] private bool   isGrounded;          // �n�ʂɐڒn���Ă��邩�ǂ���
    [SerializeField] private float  walkSpeed = 1.5f;    // ��������
    [SerializeField] private float  turnSpeed = 5.0f;    // ��]�̑���
    [SerializeField] private int    layerMask;           // Player���C���[�ȊO�̃��C���[�}�X�N

    // ��Ԉړ�
    private enum EpatialMovementPattern
    {
        Teleportation,      // �u�Ԉړ� �@�� �v���C���[�̍��W���ړ�
        SceneMovement,      // �V�[���ړ� �� �ʂ̃V�[���ֈړ��ړ�
        ObjectSwitching     // �I�u�W�F�N�g��؂�ւ�  ���ݒu���Ă���I�u�W�F�N�g��ύX
    }
    [SerializeField] private EpatialMovementPattern eatialMovementPattern;
    [SerializeField] private GameObject gateA;
    [SerializeField] private GameObject gateB;

    [SerializeField] private SceneObject sceneA;
    [SerializeField] private SceneObject sceneB;
    
    [SerializeField] private GameObject firstRoom;  // �ŏ��ɕ\������I�u�W�F�N�g    
    [SerializeField] private GameObject room1;      // ����1   
    [SerializeField] private GameObject room2;      // ����2


    // �R�[�h���Ŏg�p
    private Animator     animator;  // �A�j���[�V����
    private Vector3      input;     // ���͒l
    private Vector3      velocity;  // �x�N�g��
    private Rigidbody    rigid;     // rigidbody (�����蔻�蓙�Ɏg�p)
    private GameObject[] players;   // �v���C���[
    private GameObject[] cameras;   // ���C���J����
    private GameObject[] room;      // �\������I�u�W�F�N�g
    private GameObject[] ActiveRoom;// �\�����̃I�u�W�F�N�g

    private Vector3 centor = new Vector3(0.0f, 0.8f, 0.0f);

    ///////////////////////////////////////
    /// ������ (����������) �� Start()�̑O�Ɏ��s�����
    //////////////////////////////////////

    void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");

        // ���g�̃f�[�^����������
        if (players.Length <= 1)
        {
            Debug.Log("Awake");
            DontDestroyOnLoad(this.gameObject);
        }
        // �d�����Ă��镪���폜
        // �v���C���[
        else
        {
            for (int i = 1; i < cameras.Length; i++)
            {
                //Debug.Log((i + 1) + "�l�ڍ폜");
                Destroy(players[i]);
            }

            Debug.Log("�d�����Ă���v���C���[���폜");
        }
        // ���C���J����
        if (cameras.Length > 1)
        {
            for(int i = 1; i < cameras.Length; i++)
            {
                //Debug.Log((i+1) + "��ڍ폜");
                Destroy(cameras[i]);
            }

            Debug.Log("�d�����Ă��郁�C���J�������폜");
        }

        room = GameObject.FindGameObjectsWithTag("Room");
        if (firstRoom != null)
        {
            Debug.Log("firstRoom = " + firstRoom.name);

            for (int i = 0; i < room.Length; i++)
            {
                //Debug.Log((i+1) + "���");

                // �ŏ��̕����Ȃ�\������
                if (room[i].name == firstRoom.name)
                {
                    Debug.Log("firstRoom�\��");
                    room[i].SetActive(true);
                }
                // ���̑��͔�\��
                else
                {
                    Debug.Log("���̑���\��");
                    room[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("firstRoom�Ȃ�");
        }
    }

    ///////////////////////////////////////
    /// ������
    //////////////////////////////////////

    void Start()
    {
        //DontDestroyOnLoad(gameObject);

        if (players.Length <= 1)
        {
            Debug.Log("Start");

            animator = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody>();
            rigid.centerOfMass = centor;
            layerMask = ~(1 << LayerMask.NameToLayer("Player"));
        }
    }

    ///////////////////////////////////////
    /// �X�V
    //////////////////////////////////////

    void Update()
    {

        //�@�L�����N�^�[���ڒn���Ă���ꍇ
        if (isGrounded)
        {
            //�@�ڒn�����̂ňړ����x��0�ɂ���
            velocity = Vector3.zero;
            input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            //�@�����L�[������������Ă���
            if (input.magnitude > 0f)
            {
                animator.SetFloat("Speed", input.magnitude);

                // ������ύX
                //transform.LookAt(transform.position + input); 

                Quaternion targetRotation = Quaternion.LookRotation(input);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

                velocity += transform.forward * walkSpeed;

            //�@�L�[�̉���������������ꍇ�͈ړ����Ȃ�
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }

            //�@�W�����v
            if (Input.GetButtonDown("Jump")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")
                && !animator.IsInTransition(0)      //�@�J�ړr���ɃW�����v�����Ȃ�����
            )
            {
                animator.SetBool("Jump", true);

                //�@�W�����v������ڒn���Ă��Ȃ���Ԃɂ���
                isGrounded = false;
                velocity.y += jumpPower;
            }
        }
    }
    void FixedUpdate()
    {
        //�@�L�����N�^�[���ړ������鏈��
        rigid.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�@�n�ʂɒ��n�������ǂ����̔���
        if (Physics.CheckSphere(transform.position, 0.3f, layerMask))
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
            velocity.y = 0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //�@�n�ʂ���~�肽���̏���
        //�@Field���C���[�̃Q�[���I�u�W�F�N�g���痣�ꂽ��
        if (1 << collision.gameObject.layer != layerMask)
        {
            //�@�������Ƀ��C���[���΂�Field���C���[�ƐڐG���Ȃ���Βn�ʂ��痣�ꂽ�Ɣ��肷��
            if (!Physics.Linecast(transform.position + Vector3.up * 0.2f, transform.position + Vector3.down * 0.1f, LayerMask.GetMask("Field", "Block")))
            {
                isGrounded = false;
            }
        }
    }

    // ��Ԉړ�
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("gate��������");

        // �e���|�[�g
        if (other.gameObject.tag == "Gate")
        {
            switch (eatialMovementPattern)
            {
                case EpatialMovementPattern.Teleportation:   Rum_Teleportation(other); break;
                case EpatialMovementPattern.SceneMovement:   Rum_SceneMovement(other); break;
                case EpatialMovementPattern.ObjectSwitching: Rum_ObjectSwitching(other); break;
            }
        }
    }

    // �u�Ԉړ�
    void Rum_Teleportation(Collider other)
    {
        if (other.gameObject == gateA)
        {
            Debug.Log("gateA");

            transform.position = gateB.transform.position + new Vector3(0, 2, 0);
        }

        if (other.gameObject == gateB)
        {
            Debug.Log("gateB");
            transform.position = gateA.transform.position + new Vector3(0, 2, 0);
        }
    }

    // �V�[���ړ�
    void Rum_SceneMovement(Collider other)
    {
        // ���݂̃V�[�������擾
        string sceneName = SceneManager.GetActiveScene().name;

        // A �� B
        if (sceneName == sceneA.m_SceneName)
        {
            Debug.Log(sceneName + "��" + sceneB.m_SceneName);

            // �ړ���
            SceneManager.LoadScene(sceneB);
        }

        // B �� A
        else if (sceneName == sceneB.m_SceneName)
        {
            Debug.Log(sceneName + "��" + sceneA.m_SceneName);

            SceneManager.LoadScene(sceneA);
        }
    }

    // �I�u�W�F�N�g�؂�ւ�
    void Rum_ObjectSwitching(Collider other)
    {
        if(room == null) { return; }

        if(room.Length <= 1)
        {
            Debug.Log("�؂�ւ��悪�Ȃ��I");
            return;
        }

        // �\�����̃I�u�W�F�N�g������
        ActiveRoom = GameObject.FindGameObjectsWithTag("Room");

        for (int i = 0; i < ActiveRoom.Length; i++)
        {
            // Room1 �� 2
            if (ActiveRoom[i] == room1)
            {
                Debug.Log("Room1 �� 2");

                room1.SetActive(false);
                room2.SetActive(true);
            }

            // Room2 �� 1
            else if (ActiveRoom[i] == room2)
            {
                Debug.Log("Room2 �� 1");

                room1.SetActive(true);
                room2.SetActive(false);
            }
        }
    }
}