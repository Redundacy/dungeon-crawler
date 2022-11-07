using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public int roomId;
    public void Return(int roomId){
        SceneManager.LoadSceneAsync(roomId);
    }
}
