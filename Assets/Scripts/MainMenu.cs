using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject introPrefab;
    Countdown cnt;
    private void Start()
    {
        cnt = gameObject.AddComponent<Countdown>();
        cnt.SetCuntdown(3f);
    }
    public void EscenaJuego()
    {
        introPrefab.GetComponent<Animator>().SetBool("start",true);
        cnt.StartTimer();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (cnt.HasCompleted())
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

}
