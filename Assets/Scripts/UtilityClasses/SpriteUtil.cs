using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteUtil
{
    #region Color
    //Update a color while maintaining its alpha value.
    public static void UpdateColor(SpriteRenderer sprite, Color color)
    {
        Color tmp = new Color(color.r, color.g, color.b, sprite.color.a);
        sprite.color = tmp;
    }

    //Update the alpha value of a color
    public static void UpdateAlpha(SpriteRenderer sprite, float alpha)
    {
        Debug.Assert(alpha >= 0f && alpha <= 1f);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }
    #endregion
}
