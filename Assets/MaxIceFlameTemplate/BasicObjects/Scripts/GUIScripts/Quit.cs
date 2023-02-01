using UnityEngine;

namespace MaxIceFlameTemplate.UI
{
    public class Quit : MonoBehaviour
    {
        public void click()
        {
            PlayerPrefs.Save();
            Application.Quit();
        }
    }
}