using UnityEngine;

public class Interactable : MonoBehaviour
{
    private void ChangeSprites(Transform t, float a)
    {
        var sr = t.GetComponent<SpriteRenderer>();
        if (sr)
        {
            Color spriteColor = sr.color;
            spriteColor.a = a;
            sr.color = spriteColor;
        }

        for (int i = 0; i < t.childCount; i++)
        {
            ChangeSprites(t.GetChild(i), a);
        }
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            ChangeSprites(transform, 0.5f);
        }
    }

    void OnTriggerExit2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            ChangeSprites(transform, 1);
        }
    }
}
