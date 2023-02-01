using UnityEngine;

namespace MaxIceFlameTemplate.Basic
{
    public class RoadMaker : MonoBehaviour
    {
        public GameObject cube;
        public float roadWidth;
        private MainLine MainLineCom;
        private GameObject road;

        void Start()
        {
            MainLineCom = GetComponent<MainLine>();
        }

        void Update()
        {
            if ((Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space)) && !MainLineCom.keydown)
            {
                road = Instantiate(cube, new Vector3(MainLineCom.LineBody.transform.position.x, MainLineCom.LineBody.transform.position.y - 1, MainLineCom.LineBody.transform.position.z), MainLineCom.LineBody.transform.rotation);
                road.transform.localScale = new Vector3(MainLineCom.LineBody.transform.localScale.x + roadWidth, 1f, MainLineCom.LineBody.transform.localScale.z + roadWidth);
            }
        }
    }
}