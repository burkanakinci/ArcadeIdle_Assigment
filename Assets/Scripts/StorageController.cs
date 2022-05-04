using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StorageController : MonoBehaviour
{
    [SerializeField] private TextMeshPro storageText;
    private List<CubeController> cubesOnStorage = new List<CubeController>();
}
