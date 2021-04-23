using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    private GameObject Player;

    public MöbiusSystem MSystem;

    private Vector3 moveTo;

    private float RayDir;

    private bool beRay = false;

    private bool rotflg;

    public Camera RayforCamera;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        RayDir = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RayCheck();
        }

        if (beRay)
        {
            MovePoisition();
            MoveRotation();
        }

        if (Input.GetMouseButtonUp(0))
        {
            beRay = false;
        }
    }


    private void RayCheck()
    {
        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ray = RayforCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * RayDir, Color.red, 2, false);
        Debug.Log("あほ");
        if (Physics.Raycast(ray.origin, ray.direction, out hit, RayDir) && hit.collider == gameObject.GetComponent<Collider>())
        {
            if (MSystem.CatchObject == null)
            {
                MSystem.CatchObject = gameObject;
            }
            beRay = true;
        }
        else
        {
            beRay = false;
        }

    }

    private void MovePoisition()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = RayDir - 1;

        moveTo = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = moveTo;

    }

    private void MoveRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotflg = true;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            rotflg = false;
        }

        if (rotflg)
        {
            transform.Rotate(new Vector3(0, 0, 1));
        }
    }
}
