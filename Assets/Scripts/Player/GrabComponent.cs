using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabComponent : MonoBehaviour
{
    [SerializeField]
    Color GrabColor = Color.red;
    [SerializeField]
    Color HightlightColor = Color.green;

    [SerializeField]
    SpriteRenderer sprite;
    Color normalColor;
    bool isHighlited;
    bool isSelected;

    private void Awake()
    {
        if (sprite)
        {
            normalColor = sprite.color;
        }
    }

    private void Update()
    {
        if( !isHighlited && !isSelected)
        {
            if( sprite)
                sprite.color = normalColor;
        }
    }

    public void Select()
    {
        if (sprite)
        {
            isSelected = true;
            sprite.color = GrabColor;
        }
    }

    public void Highlight()
    {
        if (sprite)
        {
            isHighlited = true;
            sprite.color = HightlightColor;
        }
    }

    private void LateUpdate()
    {
        isSelected = false;
        isHighlited = false;
    }
}
