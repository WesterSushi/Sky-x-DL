using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaxIceFlameTemplate.UI
{
    public class SceneLoader : MonoBehaviour
    {
        public int LoadSceneID;
        public bool Self;

        public void Click()
        {
            if (Self == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                SceneManager.LoadSceneAsync(LoadSceneID);
            }
        }
    }
}