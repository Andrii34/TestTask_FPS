using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject _gameOverCanvas;
    

    //private bool _isPausa;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Time.timeScale = 1f;
    }

    public void GameOver()
    {

        _gameOverCanvas?.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadStartLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); 
#endif
    }
    //public void pausa()
    //{
    //    if (!_isPausa)
    //    {
    //        _pausaCanvas?.SetActive(true);
    //        Time.timeScale = 0f;
    //        _isPausa = true;
    //    }
    //    else if (_isPausa)
    //    {
    //        _pausaCanvas?.SetActive(false);
    //        Time.timeScale = 1f;
    //        _isPausa = false;
    //    }
    //}


}

