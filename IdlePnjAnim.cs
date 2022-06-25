using UnityEngine;

public class IdlePnjAnim : MonoBehaviour
{
    public Vector2 amount;
    public float time;

    void Start()
    {
        iTween.ScaleTo(gameObject, iTween.Hash(
            "x", amount.x,
            "y", amount.y,
            "time", time,
            "looptype", iTween.LoopType.pingPong

        ));
    }
}
