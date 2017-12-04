using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamInfoPanel : MonoBehaviour {
    public enum DisplayM { Team, Game }
    public DisplayM DisplayMode = DisplayM.Team;
    public Team AttachedTeam;
    public bool Visible;
    Text _txtBox;
    Image _bubble;
	// Use this for initialization
	void Start () {
        SetVisible(false);
    }
	
	// Update is called once per frame
	void Update () {

	}

    void DisplayTeamInfo()
    {
        GetTxt().text = "";
        if (AttachedTeam == null) return;
        foreach(TeamSlot sl in AttachedTeam.TeamSlots)
        {
            if(sl.Person == null)
            {
                GetTxt().text += "<Open team slot>\n";
            } else
            {
                GetTxt().text += sl.Person.name + "\n";
            }
        }
    }

    void DisplayGameInfo()
    {
        GetTxt().text = "";

    }

    Text GetTxt()
    {
        if(_txtBox == null)
        {
            _txtBox = GetComponentInChildren<Text>();
        }
        return _txtBox;
    }

    public void Display()
    {
        GetTxt();
        transform.position = AttachedTeam.transform.position + Vector3.up;
        if (DisplayMode == DisplayM.Team)
        {
            DisplayTeamInfo();
        } else if (DisplayMode == DisplayM.Game)
        {
            DisplayGameInfo();
        }

    }
    public void UpdateDisplay()
    {
        if (DisplayMode == DisplayM.Team)
        {
            DisplayTeamInfo();
        }
        else if (DisplayMode == DisplayM.Game)
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
        SetVisible(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SetVisible(false);
    }
}
