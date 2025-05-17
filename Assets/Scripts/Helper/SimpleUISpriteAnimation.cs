using UnityEngine;
using UnityEngine.UI;

public class SimpleUISpriteAnimation : MonoBehaviour
{
    public Sprite[] frames; // Deine zwei geschnittenen Sprites
    public float frameRate = 0.5f; // Sekunden pro Frame

    private Image image;
    private int currentFrame;
    private float timer;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length;
            image.sprite = frames[currentFrame];
        }
    }
}