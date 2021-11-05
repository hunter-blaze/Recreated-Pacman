using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
        Debug.Log(scene);
    }
}
