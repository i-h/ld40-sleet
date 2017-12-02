using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recruitable : Character, Interactable {
    bool _available = true;
    public RectTransform RecruitDialog;
    public void OnTouch(Touch t)
    {
        if (!_available) return;
        if(RecruitDialog != null && !RecruitDialog.gameObject.activeSelf)
        {
            RecruitDialog.gameObject.SetActive(true);
            Button[] buttons = RecruitDialog.GetComponentsInChildren<Button>();
            foreach(Button btn in buttons)
            {
                btn.onClick.RemoveAllListeners();
                switch (btn.tag) {
                    case "YesButton":
                            btn.onClick.AddListener(RecruitSuccess);
                        break;
                    case "NoButton":
                            btn.onClick.AddListener(RecruitFailure);
                        break;
                }
            }
        }


    }

    public void RecruitSuccess()
    {
        Debug.Log("Recruit success");
        RecruitDialog.gameObject.SetActive(false);
        Player.Instance.Recruit(this);
        _available = false;
    }

    public void RecruitFailure()
    {
        Debug.Log("Recruit Failure");
        RecruitDialog.gameObject.SetActive(false);
    }
}
