using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCompleted : MonoBehaviour {
    public Sprite MissionCompletedSprite;
    public Vector3 targetScale = Vector3.one;
    public AnimationCurve RevealCurve = new AnimationCurve();
    SpriteRenderer _sr;
    Vector3 _spritePos;
    string[] effects = { "ZoomIn" };
	// Use this for initialization
	void Start () {
        _sr = gameObject.AddComponent<SpriteRenderer>();
        _sr.sprite = MissionCompletedSprite;
        _sr.sortingLayerName = "Effects";
        _spritePos= Camera.main.ViewportToWorldPoint(Vector2.one / 2);
        _spritePos.z = 0;
        transform.parent = Camera.main.transform;
        transform.position = _spritePos;
        StartCoroutine("ZoomIn", 1f);
        
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
    }
    private void Update()
    {
    }
}
