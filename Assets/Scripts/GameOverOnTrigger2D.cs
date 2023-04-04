using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverOnTrigger2D : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    string triggeringTag;

    private int lives = 3;

    [SerializeField]
    TextMeshPro livesText;

    [SerializeField]
    TextMeshPro gameOverText;

    [SerializeField]
    float respawnTime = 3.0f;

    [SerializeField]
    float respawnInvulnerabillityTime = 3.0f;

    private void Awake()
    {
        gameOverText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == triggeringTag && enabled)
        {
            this.lives--;
            livesText.text = "Lives : " + lives.ToString();
            Destroy(other.gameObject);
            this.player.gameObject.SetActive(false);

            if (this.lives <= 0)
            {
                gameOverText.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Invoke(nameof(Respawn), this.respawnTime);
            }
        }
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnInvulnerabillityTime);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
