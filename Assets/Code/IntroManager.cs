using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    void Start() { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; }
    public void StartGame() { SceneManager.LoadScene("Level1"); }
}