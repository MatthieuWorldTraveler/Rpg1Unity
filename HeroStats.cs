using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroStats : MonoBehaviour
{
    public float xpforlvlUp = 400;
    float xpmultiplicator1;
    float xpmultiplicator2;
    public int lvlStats;
    public TMP_Text lvlTxt;

    public GameObject XpBar1;
    public GameObject XpBar2;
    public RectTransform BarProgress1;
    public RectTransform BarProgress2;

    public float rightScale1;
    public float rightScale2;


    public int xpStats;
    public int goldStats;


    public TMP_Text goldTxt;

    private void Start()
    {
        xpmultiplicator1 = (76 - 3) / xpforlvlUp;
        xpmultiplicator2 = (78 - 6) / xpforlvlUp;

        rightScale1 = (xpStats * xpmultiplicator1)-76;
        rightScale2 = (xpStats * xpmultiplicator2)-78;

        BarProgress1.offsetMax = new Vector2(rightScale1, BarProgress1.offsetMax.y);
        BarProgress2.offsetMax = new Vector2(rightScale2, BarProgress2.offsetMax.y);

        // Xp setup Barre 2 plage : 6 right max 78 right no xp
        // XP setup Barre 1 plage : 3 right max 76 right no xp
    }

    public void XpSetup()
    {
        xpmultiplicator1 = (76 - 3) / xpforlvlUp;
        xpmultiplicator2 = (78 - 6) / xpforlvlUp;

        rightScale1 = (xpStats * xpmultiplicator1) - 76;
        rightScale2 = (xpStats * xpmultiplicator2) - 78;

        BarProgress1.offsetMax = new Vector2(rightScale1, BarProgress1.offsetMax.y);
        BarProgress2.offsetMax = new Vector2(rightScale2, BarProgress2.offsetMax.y);
        goldTxt.text = goldStats.ToString();
    }
}
