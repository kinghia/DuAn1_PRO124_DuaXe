using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSpawn : MonoBehaviour
{
    public GameObject clonePrefab;  // Prefab của đối tượng clone

void CloneObject()
{
    GameObject clone = Instantiate(clonePrefab, new Vector3(0, 0, 0), Quaternion.identity);
    clone.tag = "Player";  // Gán thẻ cho đối tượng clone
}
}
