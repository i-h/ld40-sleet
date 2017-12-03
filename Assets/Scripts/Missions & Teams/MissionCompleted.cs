using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionCompleted : MonoBehaviour
{
    public enum Effects { ZoomIn, PopUp }
    public Sprite MissionCompletedSprite;
    public Vector3 targetScale = Vector3.one;
    public AnimationCurve RevealCurve = new AnimationCurve();
    public AnimationCurve AlphaCurve = new AnimationCurve();
    public bool CenterToScreen = true;
    public Effects Effect = Effects.ZoomIn;
    public float EffectDuration = 1.0f;
    public Color MainColor = new Color();
    public float DeleteAfter = -1;
    public int OrderInLayer = 0;
    public string TransferTo = "";
    SpriteRenderer _sr;
    Vector3 _spritePos;
	// Use this for initialization
	void Start () {
        _sr = gameObject.AddComponent<SpriteRenderer>();
        _sr.sprite = MissionCompletedSprite;
        _sr.sortingLayerName = "Effects";
        _sr.sortingOrder = OrderInLayer;
        if (CenterToScreen)
        {
            _spritePos = Camera.main.ViewportToWorldPoint(Vector2.one / 2);
            _spritePos.z = 0;
            transform.position = _spritePos;
            transform.parent = Camera.main.transform;
        }
        StartCoroutine(Effect.ToString(), EffectDuration);
        
	}
    IEnumerator ZoomIn(float duration)
    {
        float rOffset = Random.value;
        float d = 0;
        Vector3 startingScale = Vector3.zero;
        transform.localScale = startingScale;
        while (d < 1)
        {
            transform.localScale = Vector3.SlerpUnclamped(startingScale, targetScale, RevealCurve.Evaluate(d));
            float h = (d + rOffset * 2) % 1;
            h = Mathf.Round(h * 32) / 32;
            _sr.color = Color.HSVToRGB(h, 1-d*d*d/2, 1);           
            d += Time.deltaTime / duration;
            yield return null;
        }
        DeleteAfterTime();
    }
    IEnumerator PopUp(float duration)
    {
        transform.localScale = targetScale;
        float d = 0;
        Vector2 startPos = (Vector2)transform.position - Vector2.up * 2;
        Vector2 endPos = transform.position;
        while (d < 1.0f)
        {
            transform.position = Vector3.LerpUnclamped(startPos, endPos, RevealCurve.Evaluate(d));
            MainColor.a = AlphaCurve.Evaluate(d);
            _sr.color = MainColor;
            d += Time.deltaTime / duration;
            yield return null;
        }
        DeleteAfterTime();
    }

    private void DeleteAfterTime()
    {
        if(DeleteAfter >= 0)
            Invoke("OnDeletingMessage", DeleteAfter);
    }
    private void OnDeletingMessage()
    {
        if(TransferTo.Length > 0)
        {
            SceneManager.LoadScene(TransferTo);
        }
        Destroy(gameObject);
    }
    private void Update()
    {
    }
}
