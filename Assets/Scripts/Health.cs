using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 3;

    int health;

    [SerializeField]
    AudioClip hurtSound;

    AudioSource audioSource;

    [SerializeField]
    string gameOverSceneName;

    [SerializeField]
    Image healthbar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        if (healthbar != null)
        {
            healthbar.fillAmount = health / (float)maxHealth;
        }   
    }

    public void Hurt(int amount)
    {

        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        health -= amount;

        if(healthbar != null)
        {
            healthbar.fillAmount = health / (float)maxHealth;
        }
        
        if (health <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        print(gameObject.name + " lost!");
        SceneManager.LoadScene(gameOverSceneName);
    }
}
