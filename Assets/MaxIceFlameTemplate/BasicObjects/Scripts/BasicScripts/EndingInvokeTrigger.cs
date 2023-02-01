using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class EndingInvokeTrigger : MonoBehaviour
    {
        public Ending Ending;
        private MainLine Line;
        [HideInInspector] public GameObject crowns1, crowns2, crowns3;

        void Start()
        {
            Line = FindObjectOfType<MainLine>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                Ending.InvokeWin();
                Line.mainObjects.Percentage = 100;
                Line.gameEvents.OnGameWin.Invoke();
            }
        }
        public void playsound()
        {
            if (Line.GetComponent<MainLine>().CrownCount == 1)
            {
                Instantiate(crowns1, transform.position, transform.rotation);
            }
            if (Line.GetComponent<MainLine>().CrownCount == 2)
            {
                Instantiate(crowns2, transform.position, transform.rotation);
            }
            if (Line.GetComponent<MainLine>().CrownCount >= 3)
            {
                Instantiate(crowns3, transform.position, transform.rotation);
            }
        }
    }
}