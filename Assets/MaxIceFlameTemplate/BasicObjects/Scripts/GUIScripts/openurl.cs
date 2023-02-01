using UnityEngine;

namespace MaxIceFlameTemplate.UI
{
    public class openurl : MonoBehaviour
    {
        public string url = "";

        public void click()
        {
            Application.OpenURL(url);
        }
    }
}