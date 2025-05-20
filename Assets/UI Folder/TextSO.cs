using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Unity.Burst.Intrinsics.X86.Avx;

[CreateAssetMenu(menuName = "CustomUI/TextS0", fileName = "Text")]
public class TextSO : ScriptableObject
{
    
    public ThemeSO theme;
    public TMP_FontAsset font;

    public float size;
}