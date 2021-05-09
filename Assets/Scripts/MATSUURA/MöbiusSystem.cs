using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MöbiusSystem : MonoBehaviour
{
    private List<Vector3> l_GameObj_Pos = new List<Vector3>();          // 記憶媒体(List型 Pos)
    private List<Quaternion> l_GameObj_Rot = new List<Quaternion>();    // 記憶媒体(List型 Rot)
    private List<Vector3> l_GameObj_Scale = new List<Vector3>();        // 記憶媒体(List型 Scale)

    private bool Repetition;                // 往復フラグ(false:逆進行 true:正規進行)

    private int CountFrame;                 // 収録フレーム総数カウンター

    private bool Recood;                    // 収録するフラグ

    private bool Section;                   // 動かせるフラグ

    private Quaternion q_DefRot;

    private Effekseer.EffekseerEmitter EE_effekSeerEmi;   // エフェクト変数(EffekSeer)

    [SerializeField]
    private float seconds;                  // 収録可能な最大秒数

    public bool Destory;                    // 記憶した動きを破壊するフラグ

    public bool Stop;                       // 動きの停止フラグ

    public GameObject MöbiusSprite;

    public GameObject CatchObject;          // 対象オブジェクト変数

    // Start is called before the first frame update
    void Start()
    {
        Section = false;
        Recood = false;
        Repetition = false;
        Destory = false;
        Stop = false;
        seconds = 10f;
        EE_effekSeerEmi = GetComponent<Effekseer.EffekseerEmitter>();
        q_DefRot = MöbiusSprite.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Recood = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (CatchObject == GameObject.Find("Cube"))
            {
                Debug.Log("IN");
            }
            MöbiusSprite.transform.rotation = q_DefRot;

            Section = true;
        }

        Recoding();
        Moving();
        RecetList();
    }


    // 動き（Transform）を記憶する関数
    private void Recoding()
    {
        if (!CatchObject) { return; }
        if (!Recood) { return; }

        if (MöbiusSprite)
        {
            MöbiusSprite.transform.Rotate(new Vector3(0, 0.2f, 0));
        }

        Vector3 pos = CatchObject.transform.position;
        Quaternion Rot = CatchObject.transform.rotation;
        Vector3 Scale = CatchObject.transform.localScale;

        l_GameObj_Pos.Add(pos);
        l_GameObj_Rot.Add(Rot);
        l_GameObj_Scale.Add(Scale);

        seconds -= Time.deltaTime;

        Debug.Log("入ってる");
        if (seconds <= 0)
        {
            Recood = false;
            CountFrame = l_GameObj_Pos.Count - 1;
            Debug.Log("終わり");
        }
    }

    //記憶させた動きを往復させる関数
    private void Moving()
    {
        if (!CatchObject) { return; }
        if (!Section) { return; }
        if (Stop) { return; }

        if (!Repetition)
        {
            ObjectMove(CountFrame);

            CountFrame--;
            if (CountFrame <= 0)
            {
                CountFrame = 0;
                Repetition = true;
            }
        }
        else
        {
            ObjectMove(CountFrame);

            CountFrame++;

            if (CountFrame >= l_GameObj_Pos.Count)
            {
                CountFrame = l_GameObj_Pos.Count - 1;
                Repetition = false;
            }
        }

        void ObjectMove(int Frame)
        {
            EE_effekSeerEmi.transform.position = CatchObject.transform.position;
            if (!EE_effekSeerEmi.exists)
            {
                EE_effekSeerEmi.Play(EE_effekSeerEmi.effectAsset);
            }

            Vector3 pos = CatchObject.transform.position;
            Quaternion rot = CatchObject.transform.rotation;
            Vector3 scale = CatchObject.transform.localScale;

            pos = l_GameObj_Pos[Frame];
            rot = l_GameObj_Rot[Frame];
            scale = l_GameObj_Scale[Frame];

            CatchObject.transform.position = pos;
            CatchObject.transform.rotation = rot;
            CatchObject.transform.localScale = scale;
        }

    }

    //記憶したものを破壊する
    public void RecetList()
    {
        if (!Destory) { return; }

        l_GameObj_Pos.Clear();
        l_GameObj_Rot.Clear();
        l_GameObj_Scale.Clear();

        Destory = false;
    }
}
