using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    GameObject fpsCam;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void ResetScene()
    {
        Invoke("ShutdownCamera", 1.5f);
        LoadScene1();
    }
    private void ShutdownCamera()
    {
        fpsCam.SetActive(false);
    }
    private void LoadScene1()
    {
        SceneManager.LoadScene("EscenaDePrueba");
    }
}
