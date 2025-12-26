using UnityEngine;
using UnityEngine.UI;
public class ChaosManager : MonoBehaviour
{
    public Sprite level0; //zielony
    public Sprite level1; //zolty
    public Sprite level2; //pomaranczowy
    public Sprite level3; //czerwony
    public Sprite level4; //czarny warning

    public float chaosLevel = 0; // poziom chaosu od 0 do 4

    public SpriteRenderer sprite;

    public void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        UpdateChaos(chaosLevel);
    }

    public void UpdateChaos(float level)
    {
        chaosLevel = Mathf.Clamp(level, 0, 4);
        if (chaosLevel < 1)
        {
            sprite.sprite = level0;
        }
        else if(chaosLevel < 2)
        {
            sprite.sprite = level1;
        }
        else if(chaosLevel < 3)
        {
            sprite.sprite = level2;
        }
        else if(chaosLevel < 4)
        {
            sprite.sprite = level3;
        }
        else
        {
            sprite.sprite = level4; 
        }
    }

}
