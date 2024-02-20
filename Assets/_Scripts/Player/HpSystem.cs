using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HpSystem : MonoBehaviour
{
    public static Action OnGameOver;
    
    [SerializeField] private Image[] hpImages;
    [SerializeField] private SkinnedMeshRenderer MeshRenderer;
    [SerializeField] private CapsuleCollider PlayerCollider;
    
    private int index = 2; 
    private bool isInvincible = false;

    private void OnEnable()
    {
        PlayerCollision.OnObstacleHit += PlayerCollision_OnObstacleHit;
        UIManager.OnGameReset += UIManager_OnGameStart;
    }
    private void OnDisable()
    {
        PlayerCollision.OnObstacleHit -= PlayerCollision_OnObstacleHit;
        UIManager.OnGameReset -= UIManager_OnGameStart;
    }
    private void PlayerCollision_OnObstacleHit()
    {
        if (!isInvincible)
        {
            hpImages[index].enabled = false;
            index--;
            if (index < 0)
            {
                OnGameOver?.Invoke();
                GameManager.OnGameStateChanged?.Invoke(GameState.GameOver);
            }
            else
            {
                StartCoroutine(InvincibilityTimer());
            }
        }
    }
    private IEnumerator InvincibilityTimer()
    {
        isInvincible = true;
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            PlayerCollider.enabled = !PlayerCollider.enabled;
            MeshRenderer.enabled = !MeshRenderer.enabled;
            yield return new WaitForSeconds(0.05f); 
            elapsedTime += 0.05f;
        }
        PlayerCollider.enabled = true;
        MeshRenderer.enabled = true;
        isInvincible = false;
    }
    private void UIManager_OnGameStart()
    {
        foreach (var t in hpImages)
        {
            t.enabled = true;
        }
        index = 2;
        isInvincible = false;
        PlayerCollider.enabled = true;
        MeshRenderer.enabled = true;
    }
}
