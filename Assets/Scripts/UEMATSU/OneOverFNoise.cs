using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneOverFNoise : MonoBehaviour
{
    //�J�n�ʒu�ƍŏI�ʒu
    private Vector3 StartEmission;
    private Vector3 EndEmission;
    //�ړ��ɂ����鎞��
    public int Duration;

    //���C�g�̃I�u�W�F�N�g
    public Light slight;
    //���C�g�̋���
    public float LightPowor;

    //��_�Ԃ̋���������
    //���݂̈ʒu
    private float NowEmission = 0;

    //���݉��b�o�߂��Ă��邩
    private float NowTime = 0;

    //�ǂ��瑤�Ɉړ����邩�Ǘ�����ϐ�
    private bool DoMove = false;

    //�Q�[���I�u�W�F�N�g�̃����_�[
    private Renderer m_GameObjectRender;

    // Start is called before the first frame update
    void Start()
    {
        StartEmission.x = 0;
        EndEmission.x = 1;
        m_GameObjectRender = GetComponent<Renderer>();
    }

    // �X�V
    void Update()
    {
        //�i�߂�
        if (DoMove)
        {
            //�ǂꂭ�炢���Ԃ��o�߂����̂�������
            NowEmission = NowTime / Duration;

            //�C�[�W���O�֐��ɂ��ɋ}��t����
            NowEmission = SineInOut(NowEmission, Duration, StartEmission.x, EndEmission.x);

            //�I�u�W�F�N�g�̈ړ�
            Vector3 v = Vector3.Lerp(StartEmission, EndEmission, NowEmission);

            //Shader�ɓn��
            m_GameObjectRender.material.SetFloat("_Emission00FN", v.x);
            //���C�g�̌����������Ă���
            slight.GetComponent<Light>().intensity = v.x * LightPowor;

            if (v.x>=1)
            {
                InversionFlg();
            }
        }
        //�߂�
        else
        {
            //�ǂꂭ�炢���Ԃ��o�߂����̂�������
            NowEmission = NowTime / Duration;

            //�C�[�W���O�֐��ɂ��ɋ}��t����
            NowEmission = SineInOut(NowEmission, Duration, EndEmission.x, StartEmission.x);

            //�I�u�W�F�N�g�̈ړ�
            Vector3 v = Vector3.Lerp( EndEmission, StartEmission, NowEmission);

            m_GameObjectRender.material.SetFloat("_Emission00FN", v.x);
            //���C�g�̌����キ���Ă���
            slight.GetComponent<Light>().intensity = v.x * 5.0f;

            if (v.x<=0)
            {
                InversionFlg();
            }
        }

        NowTime += Time.fixedDeltaTime;
    }

    public static float SineInOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        return -max / 2 * (Mathf.Cos(t * Mathf.PI / totaltime) - 1) + min;
    }

    private void InversionFlg()
    {
        DoMove = !DoMove;
    }
}
