using System;
using TMPro;
using UnityEngine;

public class GoldSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI GoldText;
    private int _goldCount = 0;

    private void OnEnable()
    {
        GoldObject.OnGoldCollected += GoldObject_OnGoldCollected;
        UIManager.OnGameStart += UIManager_OnGameStart;
    }
    private void OnDisable()
    {
        GoldObject.OnGoldCollected -= GoldObject_OnGoldCollected;
        UIManager.OnGameStart -= UIManager_OnGameStart;
    }
    private void UIManager_OnGameStart()
    {
        _goldCount = 0;
        GoldText.text = _goldCount.ToString();
    }
    private void GoldObject_OnGoldCollected()
    {
        _goldCount++;
        GoldText.text = _goldCount.ToString();
    }
}
