using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    private static UIController instance;
    public static UIController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("UIController").AddComponent<UIController>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private TextMeshProUGUI stackCountText;

    public void SetStackCountText()
    {
        stackCountText.text = CharacterManager.Instance.GetCollectedCubeCount() + " / " + CharacterManager.Instance.GetCapacity();
    }
}