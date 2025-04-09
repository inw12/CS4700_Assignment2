using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialHearts();
    }

    void Update()
    {
        UpdateHearts();
    }

    public void InitialHearts()
    {
        for (int i = 0; i < heartContainers.value; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }
    public void UpdateHearts()
    {
        float tempHealth = currentHealth.runtimeValue / 2;
        for (int i = 0; i < heartContainers.value; i++)
        {
            // Full Heart
            if (i <= tempHealth - 1) {
                hearts[i].sprite = fullHeart;
            }
            // Empty Heart
            else if(i >= tempHealth) {
                hearts[i].sprite = emptyHeart;
            }
            // Half Heart
            else {
                hearts[i].sprite = halfHeart;
            }
        }
    }
}
