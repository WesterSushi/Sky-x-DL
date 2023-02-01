using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class CrownIcon : MonoBehaviour
    {
        public enum Type { Black, White }
        public Type IconType = Type.Black;
        [HideInInspector] public Material WhiteOn, WhiteOff, BlackOn, BlackOff;

        void Awake()
        {
            switch (IconType)
            {
                case Type.Black:
                    GetComponent<MeshRenderer>().material = BlackOff;
                    transform.GetChild(0).GetComponent<MeshRenderer>().material = BlackOn;
                    break;
                case Type.White:
                    GetComponent<MeshRenderer>().material = WhiteOff;
                    transform.GetChild(0).GetComponent<MeshRenderer>().material = WhiteOn;
                    break;
            }
        }
    }
}