using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagWin : MonoBehaviour
{
    public bool canwin = false;
    public void Win()
    {
        if (canwin)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
