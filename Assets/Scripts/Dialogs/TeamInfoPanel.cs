using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfoPanel : MonoBehaviour {
    public enum DisplayM { Team, Game }
    DisplayM _displayMode = DisplayM.Team;
    public Team AttachedTeam;
    public bool Visible;
    GameProduct _game;
    Text _txtBox;
    Image _bubble;
	// Use this for initialization
	void Start () {
        SetVisible(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (_displayMode == DisplayM.Game)
            UpdateDisplay();
	}

    void DisplayTeamInfo()
    {
        GetTxt().text = "";
        if (AttachedTeam == null) return;
        foreach(TeamSlot sl in AttachedTeam.TeamSlots)
        {
            if(sl.Person == null)
            {
                GetTxt().text += "Open team slot\n";
            } else
            {
                GetTxt().text += (sl.Person as Recruitable).Role + "\n";
            }
        }
    }

    void DisplayGameInfo()
    {
        GetTxt().text = "Developing...\n";
        string pStr = "[";
        int progressLength = 20;
        int progress = (int)(_game.Progress * progressLength);
        for(int i = 0; i < progressLength; i++)
        {
            if (i == progress) pStr += "|";
            else pStr += " ";
        }
        pStr += "]";

        GetTxt().text += pStr + "\nQuality: " + _game.Quality.ToString("00")+"%";
    }

    Text GetTxt()
    {
        if(_txtBox == null)
        {
            _txtBox = GetComponentInChildren<Text>();
        }
        return _txtBox;
    }
    public void StartGameDevelopment(GameProduct g)
    {
        _game = g;
        _displayMode = DisplayM.Game;
        UpdateDisplay();
    }
    public void FinishDevelopment()
    {
        _displayMode = DisplayM.Team;
        _game = null;
        UpdateDisplay();
    }
    public void Display()
    {
        GetTxt();
        transform.position = AttachedTeam.transform.position + Vector3.up;
        if (_displayMode == DisplayM.Team)
        {
            DisplayTeamInfo();
        } else if (_displayMode == DisplayM.Game)
        {
            DisplayGameInfo();
        }

    }
    public void UpdateDisplay()
    {
        if (_displayMode == DisplayM.Team)
        {
            DisplayTeamInfo();
        }
        else if (_displayMode == DisplayM.Game)
        {
            DisplayGameInfo();
        }
    }

    public void SetVisible(bool visible)
    {
        if(transform.childCount > 0)
            transform.GetChild(0).gameObject.SetActive(visible);

        if (visible) UpdateDisplay();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") SetVisible(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player") SetVisible(false);
    }
}
