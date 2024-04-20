using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public Button upgradeButton;
    public GameObject specialButtons;
    public TextMeshProUGUI upgradeCost;
    public TextMeshProUGUI sellAmount;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        SetButtonSprites(specialButtons, target.turretBlueprint.specialSprites);

        if (target.upgradeLevel < target.turretBlueprint.upgradedPrefab.Length)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost[target.upgradeLevel];
            upgradeButton.interactable = true;
            specialButtons.SetActive(false);
        }

        else if (target.upgradeLevel == target.turretBlueprint.upgradedPrefab.Length)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost[target.upgradeLevel];
            upgradeButton.interactable = false;
            specialButtons.SetActive(true);
        }

        else
        {
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
            specialButtons.SetActive(false);
        }

        sellAmount.text = "$" + target.GetSellAmount();
        
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }

    public void SpecialUpgrade(int index)
    {
        target.SpecialUpgradeTurret(index);
        BuildManager.Instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }

    public void SetButtonSprites(GameObject specialButton, Sprite[] specialSprites)
    {
        // Get the children buttons of the specialButton GameObject
        Button[] buttons = specialButton.GetComponentsInChildren<Button>();

        // Loop through the buttons and the sprites array
        for (int i = 0; i < buttons.Length && i < specialSprites.Length; i++)
        {
            // Get the Image component of the button
            UnityEngine.UI.Image buttonImage = buttons[i].GetComponent<Image>();

            if (buttonImage != null)
            {
                // Set the sprite of the button's Image component
                buttonImage.sprite = specialSprites[i];
            }
        }
    }
}
