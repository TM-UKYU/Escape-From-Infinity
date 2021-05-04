using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    public void OnClickStartButton()
    {
        //ƒ^ƒCƒgƒ‹‚É–ß‚é‚½‚ß‚Ìˆ—
        SceneManager.LoadScene("NewTitle");
    }
}
