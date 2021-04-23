/*
    [�Q�l�T�C�g] https://qiita.com/yoship1639/items/1d49d5b481f988da7142 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //�V�[���J�ڂ�����ꍇ�ɕK�v

public class MultifunctionFollowingCamera : MonoBehaviour
{
    //////////////////////////
    /// �ϐ��錾
    ////////////////////////

    // �C���X�y�N�^�[�ŕҏW�\
    [SerializeField] public         Transform target;               // �����_�ɂ���I�u�W�F�N�g
    [SerializeField] public bool    enableInput         = true;     // �J�����̑��쌠�� (true = ����\)
    [SerializeField] public bool    simulateFixedUpdate = false;    // FixedUpdate �̐؂�ւ� (true = ���b��, false = ���t���[��) 
    [SerializeField] public bool    enableDollyZoom     = true;     // �h���[�Y�[��(true = ON)�@����ʑ̂����̂܂܂̃T�C�Y�ňڂ��A�w�i�Ɉ����������炷�B�e���@    [SerializeField]
    [SerializeField] public bool    enableWallDetection = true;     // �ǌ��m(���܂�h�~)�@(true = ���܂�Ȃ�)
    [SerializeField] public bool    enableFixedPoint    = false;    // ��_�J�����̐؂�ւ� (true = ��_)
    [SerializeField] public float   inputSpeed          = 4.0f;     // ���_���슴�x
    [SerializeField] public Vector3 freeLookRotation;               // �t���[�J���� (�C�ӂ̈ʒu�Ɉړ��\)
    [SerializeField] public float   height   = 1.5f;                            // �J�����̍��� (Y��)  -
    [SerializeField] public float   distance = 5.0f;                            // target�Ƃ̋���       |���@�J�����̈ʒu�𒲐�
    [SerializeField] public Vector3 rotation = new Vector3(10.0f, 0.0f, 0.0f);  // �J�����̊p�x        -
    [SerializeField] [Range(0.01f, 100.0f)] public float positionDamping = 16.0f;   // �J�����̒x�� (���W)
    [SerializeField] [Range(0.01f, 100.0f)] public float rotationDamping = 16.0f;   // �@�@�@�V     (�p�x)�@�߂Â����x�̒����\
    [SerializeField] [Range(0.1f, 0.99f)]   public float dolly = 0.34f;             // dolly ���ʗ�
    [SerializeField] public float       noise = 0.0f;                   // ��u����
    [SerializeField] public float       noiseZ = 0.0f;                  // �O��̃u��
    [SerializeField] public float       noiseSpeed = 1.0f;              // �u���̑��x
    [SerializeField] public Vector3     vibration = Vector3.zero;       // �U�� (�J�����̗h��)
    [SerializeField] public float       wallDetectionDistance = 0.3f;   // �ǂƂ̋���
    [SerializeField] public LayerMask   wallDetectionMask = 1;          // ���C�L���X�g�̑I���@�� 1 = TransparentFX(��������)�@(0 = �S�R���C�_�[�ɓ�����Ȃ� )

    
    // �R�[�h���Ŏg�p
    private Camera  cam;                // �J�����̏��i�[�p
    private float   targetDistance;     // target�Ƃ̋���
    private Vector3 targetPosition;     // �@�V�@�̍��W
    private Vector3 targetRotation;     //�@ �V�@�̊p�x
    private Vector3 targetFree;         // �t���[�J�����̊p�x
    private float   targetHeight;       // �J�����̍���
    private float   targetDolly;        // �J�����̒x��


    void Awake()
    {
        // ���g�̃f�[�^����������
        DontDestroyOnLoad(this);
    }

    ///////////////////////////////////////
    /// ������
    //////////////////////////////////////

    void Start()
    {

        // �J�����̏����擾
        cam = GetComponent<Camera>();

        // target �I�u�W�F�N�g�Ɋւ�������擾
        targetDistance = distance;          // ����
        targetRotation = rotation;          // �J�����̊p�x
        targetFree     = freeLookRotation;  // �t���[�J�����̊p�x
        targetHeight   = height;            // ����
        targetDolly    = dolly;             // �x��

        // �x�����̋������i�[
        var dollyDist = targetDistance;

        // �h���[�Y�[���� ON �Ȃ�
        if (enableDollyZoom)
        {
            // �w�肵�������Ŏw�萍��̍����𓾂邽�߂ɕK�v�� FOV ���v�Z
            var dollyFoV = GetDollyFoV(Mathf.Pow(1.0f / targetDolly, 2.0f), targetDistance);
            /// �x���������擾
            dollyDist = GetDollyDistance(dollyFoV, targetDistance);
            // �J�����̎���p�ɑ��
            cam.fieldOfView = dollyFoV;
        }

        // target���������͈ȍ~�̏��������Ȃ�
        if (target == null) return;

        // �v�Z�p�� target �I�u�W�F�N�g�����ɍ��W��ێ�
        var pos = target.position + Vector3.up * targetHeight;

        // �J�����̍��W�������� �� target �𒆐S�ɃJ�������w��̈ʒu�ֈړ��@��������]�p�x * �㉺��荞��
        var offset = Vector3.zero;
        offset.x += Mathf.Sin(targetRotation.y * Mathf.Deg2Rad) * Mathf.Cos(targetRotation.x * Mathf.Deg2Rad) * dollyDist;
        offset.z += -Mathf.Cos(targetRotation.y * Mathf.Deg2Rad) * Mathf.Cos(targetRotation.x * Mathf.Deg2Rad) * dollyDist;
        offset.y += Mathf.Sin(targetRotation.x * Mathf.Deg2Rad) * dollyDist;
        targetPosition = pos + offset;
    }

    ///////////////////////////////////////
    /// �X�V (���t���[��)
    //////////////////////////////////////
    
    void Update()
    {
        // deltaTime �� ���O�̃t���[���ƍ��̃t���[���ԂŌo�߂�������[�b] ��Ԃ�
        if (!simulateFixedUpdate) Simulate(Time.deltaTime);
    }

    ///////////////////////////////////////
    /// �X�V (���b��) �� �uEdit�v���uProject Setting�v���uTime�v���uFixed Timestep�v�ɂĊԊu�̕ύX�\
    //////////////////////////////////////

    void FixedUpdate()
    {
        // fixedDeltaTime �� �ʏ펞�͍Ō�̃t���[������̌o�ߎ��Ԃ����AFixedUpdate���ŌĂ΂��ꍇ�̂݁A�Q�[�������Ԃ�i�߂�b���Ɏd�l���ς��
        // �������������āA�Ō�̃t���[������̌o�ߎ��Ԃ�Maximum Allowed Timestep(���)�𒴂����ꍇ�A���ۂ̌o�ߎ��Ԃł͂Ȃ�Maximum Allowed Timestep�Ɠ����l
        if (simulateFixedUpdate) Simulate(Time.fixedDeltaTime);
    }

    ///////////////////////////////////////
    /// �X�V����
    ///////////////////////////////////////

    private void Simulate(float deltaTime)
    {
        // �J�����͑���\�H
        if (enableInput)
        {
            // �J������target�Ƃ̋�����ύX
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                dolly += Input.GetAxis("Mouse ScrollWheel") * 0.2f;
                dolly = Mathf.Clamp(dolly, 0.1f, 0.99f);
            }
            else
            {
                distance *= 1.0f - Input.GetAxis("Mouse ScrollWheel");
                distance = Mathf.Clamp(distance, 0.01f, 1000.0f);
            }
            // target�𒆐S�ɃJ��������]�ړ�(���N���b�N)
            if (Input.GetMouseButton(0))
            {
                rotation.x -= Input.GetAxis("Mouse Y") * inputSpeed;
                rotation.x = Mathf.Clamp(rotation.x, -89.9f, 89.9f);
                rotation.y -= Input.GetAxis("Mouse X") * inputSpeed;
            }
            // �J�����̊p�x��ύX(�E�N���b�N)
            if (Input.GetMouseButton(1))
            {
                freeLookRotation.x -= Input.GetAxis("Mouse Y") * inputSpeed * 0.2f;
                freeLookRotation.y += Input.GetAxis("Mouse X") * inputSpeed * 0.2f;
            }
            // �J�����̊p�x��������
            if (Input.GetMouseButtonDown(2))
            {
                freeLookRotation = Vector3.zero;
            }
        }

        // �x�������p�̕ϐ���錾
        // Mathf.Lerp��rate�l�����Ԃɂ��Ă����鎖�ŁA���Ԃ����ɂ��targetRot��rot�ɋ߂Â��悤�ɂȂ�
        // rotationDamping �� �߂Â����x

        var posDampRate = Mathf.Clamp01(deltaTime * 100.0f / positionDamping);  // ���W
        var rotDampRate = Mathf.Clamp01(deltaTime * 100.0f / rotationDamping);  // �p�x

        targetDistance = Mathf.Lerp(targetDistance, distance, posDampRate);
        targetRotation = Vector3.Lerp(targetRotation, rotation, rotDampRate);
        targetFree     = Vector3.Lerp(targetFree, freeLookRotation, rotDampRate);
        targetHeight   = Mathf.Lerp(targetHeight, height, posDampRate);
        targetDolly    = Mathf.Lerp(targetDolly, dolly, posDampRate);


        // �^�[�Q�b�g�Ƃ̍����l�߂�
        if (Mathf.Abs(targetDolly - dolly) > 0.005f)
        {
            targetDistance = distance;
        }

        // �h���[�Y�[���̋���
        var dollyDist = targetDistance;

        // �h���[�Y�[���͗L���H
        if (enableDollyZoom)
        {
            // ����̍������擾
            var dollyFoV = GetDollyFoV(Mathf.Pow(1.0f / targetDolly, 2.0f), targetDistance);
            dollyDist = GetDollyDistance(dollyFoV, targetDistance);
            cam.fieldOfView = dollyFoV;     // �V������p���i�[
        }

        // target���Ȃ���Ώ����͍s��Ȃ�
        if (target == null) return;

        // �J�����̈ʒu�𒲐� (�^�[�Q�b�g�𒆐S�ɂ��ĉ�� + ����) 
        var pos = target.position + Vector3.up * targetHeight;

        // �J�����̕ǖ��܂�h�~���L�����H
        if (enableWallDetection)
        {
            // ���C�����쐬
            RaycastHit hit;
            
            // target�Ƃ̍����𐳋K�� 
            var dir = (targetPosition - pos).normalized;

            // �C�ӂ̃R���C�_�[�Ƃ̃��C�̓����蔻�� (true = �������Ă���) �� �R���C�_�[�̓��C���[�ʂɑI���\
            if (Physics.SphereCast(pos, wallDetectionDistance, dir, out hit, dollyDist, wallDetectionMask))
            {
                // ���܂������������߂�
                dollyDist = hit.distance;
            }
        }

        // �J�����I�t�Z�b�g������ + �Đݒ�
        var offset = Vector3.zero;  // ������]�p�x              *                �㉺��荞�݊p�x             * �J��������
        offset.x +=  Mathf.Sin(targetRotation.y * Mathf.Deg2Rad) * Mathf.Cos(targetRotation.x * Mathf.Deg2Rad) * dollyDist;
        offset.z += -Mathf.Cos(targetRotation.y * Mathf.Deg2Rad) * Mathf.Cos(targetRotation.x * Mathf.Deg2Rad) * dollyDist;
        offset.y +=  Mathf.Sin(targetRotation.x * Mathf.Deg2Rad) * dollyDist;


        // �^�[�Q�b�g�Ƃ̍����l�߂�
        if (Mathf.Abs(targetDolly - dolly) > 0.005f)
        {
            // ���_ + ���[�J�����W
            targetPosition = offset + pos;
        }
        else
        {
            // ���X�ɋl�߂�
            targetPosition = Vector3.Lerp(targetPosition, offset + pos, posDampRate);
        }

        // ��_�J�����ł͂Ȃ��� �� �Ǐ]�J����
        if (!enableFixedPoint) cam.transform.position = targetPosition;    // ���W
        cam.transform.LookAt(pos, Quaternion.Euler(0.0f, 0.0f, targetRotation.z) * Vector3.up); // ��p
        cam.transform.Rotate(targetFree);   // �J����

        // ��u��(�p�[�����m�C�Y) �� �ɂ₩
        // �� Z�����ʂ̕ϐ��ɂ��Ă���̂́Acamera��up�̕ϓ����傫���ƌ����炭�Ȃ��Ă��܂��̂Ōʂɒ����ł���悤�ɂ��邽��
        if (noise > 0.0f || noiseZ > 0.0f)
        {
            // �m�C�Y�f�[�^�쐬
            var rotNoise = Vector3.zero;
            rotNoise.x = (Mathf.PerlinNoise(Time.time * noiseSpeed, 0.0f) - 0.5f) * noise;
            rotNoise.y = (Mathf.PerlinNoise(Time.time * noiseSpeed, 0.4f) - 0.5f) * noise;
            rotNoise.z = (Mathf.PerlinNoise(Time.time * noiseSpeed, 0.8f) - 0.5f) * noiseZ;
            cam.transform.Rotate(rotNoise);
        }

        // �U���͂��邩�H�@�� �������@.sqrMagnitude �� �x�N�g����2���̒����ŕԂ�
        if (vibration.sqrMagnitude > 0.0f)
        {
            // �����_���ɉ�]�ʂ�ǉ�
            cam.transform.Rotate(new Vector3(Random.Range(-1.0f, 1.0f) * vibration.x, Random.Range(-1.0f, 1.0f) * vibration.y, Random.Range(-1.0f, 1.0f) * vibration.z));
        }
    }

    // �x���������擾�� Dolly Zoom ���ʂ��J�n
    private float GetDollyDistance(float fov, float distance)
    {
        return distance / (2.0f * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad));
    }

    // �J��������w��̋����ɂ��鐍��̍������v�Z
    private float GetFrustomHeight(float distance, float fov)
    {
        return 2.0f * distance * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
    }

    // �w�肵�������Ŏw��̐���̍����𓾂邽�߂ɕK�v�� FOV ���v�Z
    private float GetDollyFoV(float dolly, float distance)
    {
        return 2.0f * Mathf.Atan(distance * 0.5f / dolly) * Mathf.Rad2Deg;
    }
}
