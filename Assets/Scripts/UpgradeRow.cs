using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//TODO: Add text which says what the upgrade will do
//I could also just leave it blank as the upgrade wil most likely be an overall one
public class UpgradeRow : MonoBehaviour {

	[SerializeField]
	private TextMeshProUGUI nameText, costText, costHeader, upgradeNumberText;

	public Button upgradeButton;

	public void SetText(string name, int cost,int upgradeNum)
	{
		nameText.text = name;
		costText.text = cost.ToString();
		upgradeNumberText.text = upgradeNum + "/" + Store.MaxUpgradeLevel;
	}

	public void OnUpgradeLimitReached()
	{
        upgradeButton.enabled = false;
        TextMeshProUGUI buttonText = upgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		buttonText.text = "Fully Upgraded!";
		costHeader.gameObject.SetActive(false);
		costText.gameObject.SetActive(false);
	}


}
