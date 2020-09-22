using System.Collections.Generic;
using UnityEngine;

namespace EveSoft.MeshCombiner
{
    public static class MeshCombiner
    {
        public static List<GameObject> CombineMesh(List<GameObject> targets)
        {
            //Remove ALl Null
            targets.RemoveAll(x => x == null);

            //Result
            List<GameObject> result = new List<GameObject>();

            //Lists that holds mesh data that belongs to each submesh
            List<GroupMesh> groups = new List<GroupMesh>();

            //Loop through the array with trees
            for (int i = 0; i < targets.Count; i++)
            {
                GameObject currentObject = targets[i];
                if (currentObject == null)
                    continue;

                //Deactivate the tree 
                currentObject.SetActive(false);

                //Get all meshfilters from this tree, true to also find deactivated children
                MeshFilter[] meshFilters = currentObject.GetComponentsInChildren<MeshFilter>(true);

                //Loop through all children
                for (int j = 0; j < meshFilters.Length; j++)
                {
                    MeshFilter currentMeshFilter = meshFilters[j];
                    CombineInstance combine = new CombineInstance();

                    //Is it wood or leaf?
                    MeshRenderer meshRender = currentMeshFilter.GetComponent<MeshRenderer>();

                    if (meshRender == null)
                        continue;

                    combine.mesh = currentMeshFilter.mesh;
                    combine.transform = currentMeshFilter.transform.localToWorldMatrix;

                    //Modify the material name, because Unity adds (Instance) to the end of the name
                    Material material = meshRender.sharedMaterial;
                    GroupMesh group = groups.Find(x => x.material == material);

                    //Create New Group
                    if (group == null)
                    {
                        var newGroup = new GroupMesh();
                        newGroup.material = material;
                        newGroup.combineInstance.Add(combine);
                        groups.Add(newGroup);
                    }
                    else
                    {
                        group.combineInstance.Add(combine);
                    }
                }
            }

            //Remove all targets
            for (int i = 0; i < targets.Count; i++)
            {
                GameObject.Destroy(targets[i]);
            }

            //Combine all Group mesh
            for (int i = 0; i < groups.Count; i++)
            {
                GameObject newCombine = new GameObject(groups[i].material.name);
                newCombine.transform.localPosition = Vector3.zero;
                newCombine.transform.rotation = Quaternion.identity;
                newCombine.AddComponent<MeshFilter>().mesh = groups[i].Combine();
                newCombine.AddComponent<MeshRenderer>().sharedMaterial = groups[i].material;
                newCombine.AddComponent<MeshCollider>();           
                result.Add(newCombine);
            }

            return result.IsNullOrEmpty() ? null : result;
        }
    }
}

