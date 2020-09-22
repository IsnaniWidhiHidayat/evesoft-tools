using System.Collections.Generic;
using UnityEngine;

namespace EveSoft.MeshCombiner
{
    public class GroupMesh
    {
        #region Field
        public Material material;
        public List<CombineInstance> combineInstance = new List<CombineInstance>();
        public Mesh _mesh; 
        #endregion

        public Mesh Combine()
        {
            if (_mesh == null)
            {
                _mesh = new Mesh();
            }
            _mesh.CombineMeshes(combineInstance.ToArray());
            return _mesh;
        }
    }
}