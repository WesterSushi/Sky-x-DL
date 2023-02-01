using UnityEngine;
using MaxIceFlameTemplate.Basic;

namespace MaxIceFlameTemplate.Camera
{
    public class CameraProjectionDynamicChanging : MonoBehaviour
    {
        [HideInInspector] public bool ChangeProjection = false;
        private bool _changing = false;
        public float ProjectionChangeTime = 1f;
        private float _currentT = 0.0f;

        private void Update()
        {
            if (_changing)
            {
                ChangeProjection = false;
            }
            else if (ChangeProjection)
            {
                _changing = true;
                _currentT = 0.0f;
            }
        }
        private void LateUpdate()
        {
            if (!_changing)
            {
                return;
            }
            bool currentlyOrthographic = UnityEngine.Camera.main.orthographic;
            Matrix4x4 orthoMat, persMat;
            if (currentlyOrthographic)
            {
                orthoMat = UnityEngine.Camera.main.projectionMatrix;
                UnityEngine.Camera.main.orthographic = false;
                UnityEngine.Camera.main.ResetProjectionMatrix();
                persMat = UnityEngine.Camera.main.projectionMatrix;
            }
            else
            {
                persMat = UnityEngine.Camera.main.projectionMatrix;
                UnityEngine.Camera.main.orthographic = true;
                UnityEngine.Camera.main.ResetProjectionMatrix();
                orthoMat = UnityEngine.Camera.main.projectionMatrix;
            }
            UnityEngine.Camera.main.orthographic = currentlyOrthographic;
            _currentT += Time.deltaTime / (ProjectionChangeTime * 10);
            if (_currentT < 1.0f)
            {
                if (currentlyOrthographic)
                {
                    UnityEngine.Camera.main.projectionMatrix = MatrixLerp(orthoMat, persMat, _currentT * _currentT);
                }
                else
                {
                    UnityEngine.Camera.main.projectionMatrix = MatrixLerp(persMat, orthoMat, Mathf.Sqrt(_currentT));
                }
            }
            else
            {
                _changing = false;
                UnityEngine.Camera.main.orthographic = !currentlyOrthographic;
                UnityEngine.Camera.main.ResetProjectionMatrix();
            }
        }
        private Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float t)
        {
            t = Mathf.Clamp(t, 0.0f, 1.0f);
            Matrix4x4 newMatrix = new Matrix4x4();
            newMatrix.SetRow(0, Vector4.Lerp(from.GetRow(0), to.GetRow(0), t));
            newMatrix.SetRow(1, Vector4.Lerp(from.GetRow(1), to.GetRow(1), t));
            newMatrix.SetRow(2, Vector4.Lerp(from.GetRow(2), to.GetRow(2), t));
            newMatrix.SetRow(3, Vector4.Lerp(from.GetRow(3), to.GetRow(3), t));
            return newMatrix;
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<MainLine>())
            {
                ChangeProjection = true;
            }
        }
    }
}