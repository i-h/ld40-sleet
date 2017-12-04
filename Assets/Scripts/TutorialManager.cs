using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
    public static TutorialManager Instance;
    public Transform GuideUI;
    public TutorialHint[] Hints;
    public float HintDelay = 15;
    Dictionary<string, string> _hints = new Dictionary<string, string>();
    Transform _guideInstance;

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        for(int i = 0; i < Hints.Length; i++)
        {
            _hints.Add(Hints[i].Key, Hints[i].Text);
        }        
	}

    public void ShowHint(string hintKey)
    {
        if (PlayerPrefs.GetInt(hintKey, 0) == 1) return;
        GameObject screenCanvas = GameObject.FindWithTag("ScreenCanvas");
        if(screenCanvas != null)
        {
            _guideInstance = Instantiate(GuideUI, screenCanvas.transform);
            Text guideText = _guideInstance.GetComponentInChildren<Text>();
            if(guideText != null)
            {
                guideText.text = _hints[hintKey];
            }

            PlayerPrefs.SetInt(hintKey, 1);
            //StartCoroutine(FadeIn());
            Invoke("DestroyHint", HintDelay);


        } else
        {
            Debug.LogWarning("Guide could not be instantiated, ScreenCanvas missing!");
        }
    }

    IEnumerator FadeIn()
    {
        if (_guideInstance != null)
        {
            Vector2 targetPos = _guideInstance.position;
            Vector2 initialPosition;
            float t = 0;
            _guideInstance.position = targetPos + Vector2.left * 800;
            initialPosition = _guideInstance.position;

            while (t < 1)
            {
                _guideInstance.position = Vector2.Lerp(initialPosition, targetPos, t);
                t += Time.deltaTime / 1.0f;
                yield return null;
            }
            _guideInstance.position = targetPos;
        }
        
    }

    public void DestroyHint()
    {
        if (_guideInstance == null) return;
        Destroy(_guideInstance.gameObject);
    }

    [System.Serializable]
    public struct TutorialHint
    {
        public string Key;
        public string Text;
    }
}
