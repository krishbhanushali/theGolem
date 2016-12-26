using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelLoad : MonoBehaviour {
    void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
