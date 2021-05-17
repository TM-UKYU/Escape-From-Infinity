using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    //�J�n�ʒu�ƍŏI�ʒu
    public Transform StartPos;
    public Transform EndPos;
    //�ړ��ɂ����鎞��
    public int Duration;

    //��_�Ԃ̋���������
    //���݂̈ʒu
    private float NowPos = 0;

    // Update is called once per frame
    void Update()
    {
        //�ǂꂭ�炢���Ԃ��o�߂����̂�������
        NowPos = Time.time / Duration;

        //�C�[�W���O�֐��ɂ��ɋ}��t����
        NowPos = EaseOutBounce(NowPos);

        //�I�u�W�F�N�g�̈ړ�
        transform.position = Vector2.Lerp(StartPos.position, EndPos.position, NowPos);

        //���̈ʒu�ɃX�s�[�h�𑫂��ē�����
        //NowPos += speed;
    }

    private float EaseOutBounce(float t)
    {
        if (t < (1f / 2.75f))
        {
            return 7.5625f * t * t;
        }
        else if (t < (2f / 2.75f))
        {
            t -= (1.5f / 2.75f);
            return 7.5625f * (t) * t + .75f;
        }
        else if (t < (2.5f / 2.75))
        {
            t -= (2.25f / 2.75f);
            return 7.5625f * (t) * t + .9375f;
        }
        else
        {
            t -= (2.625f / 2.75f);
            return 7.5625f * (t) * t + .984375f;
        }
    }
}
