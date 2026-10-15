using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace SuikaClone.Managers
{
    public class SceneManager : MonoBehaviour
    {
        public void LoadNextScene()
        {
            UnitySceneManager.LoadScene("SampleScene");
        }

        public void ReloadScene()
        {
            UnitySceneManager.LoadScene("SampleScene");
        }
    }
}
