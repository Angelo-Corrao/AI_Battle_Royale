using UnityEngine;

namespace DBGA.AI.Storm
{
    public class VisualStorm : MonoBehaviour
    {
        private Material visualStormMaterial;
        private float myXDimension;
        private float myZDimension;

        private void Awake()
        {
            visualStormMaterial = gameObject.GetComponent<MeshRenderer>().material;
            myXDimension = gameObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * gameObject.transform.lossyScale.x;
            myZDimension = gameObject.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * gameObject.transform.lossyScale.z;
        }
        public void SetVisualCenter(Vector3 newCenter)
        {
            Vector2 visualCenter = new Vector2(newCenter.x / myXDimension, newCenter.z / myZDimension);
            visualStormMaterial.SetVector("_Center", -visualCenter);
        }

        public void SetVisualRadious(float newRadious)
        {
            float minimalDimension = Mathf.Min(myZDimension, myXDimension);
            float visualRadious = newRadious / minimalDimension;
            visualStormMaterial.SetFloat("_Radious", visualRadious);
        }
    }
}
