using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=7ETvfej59yQ

public class FadeDestroy : MonoBehaviour
{

    public float fadeDelay = 0.3f;
    public float alphaValue = 0;
    public bool destroyGameObject = false;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeTo(alphaValue, fadeDelay));
    }
    IEnumerator FadeTo(float aValue, float fadeTime){
        float alpha = spriteRenderer.color.a;
        for (float t = 0.0f; t < 1.0f; t+= Time.deltaTime / fadeTime){
            Color newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(alpha, aValue, t));
            spriteRenderer.color = newColor;
            yield return null;
        }
        if (destroyGameObject)
            Destroy(gameObject);
            //gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
