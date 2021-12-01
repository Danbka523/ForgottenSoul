using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public GameObject skills;
    public GameObject mainUI;

    public void onSkillsClick()
    {
        skills.SetActive(true);
        mainUI.SetActive(false);
    }

    public void onBackClick() {
        skills.SetActive(false);
        mainUI.SetActive(true);
    }
}
