using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Considering if this should be a parent class and extensions of it should apply certain stats.
public class UpgradeTile : MonoBehaviour
{
    [SerializeField]
    private UpgradeType upgradeType;
    [SerializeField]
    private float magnitude;
    [SerializeField]
    private string effectText;
    private int cost;
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private TextMeshProUGUI titleTextbox;
    [SerializeField]
    private TextMeshProUGUI effectTextBox;  
    [SerializeField]
    private GameObject imagePanel;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Button upgradeButton;
    [SerializeField]
    private HUDController hud;

    // Start is called before the first frame update
    void Start()
    {
        titleTextbox.text = upgradeType.ToString();
        effectTextBox.text = '+' + magnitude.ToString() + " " + effectText;
        imagePanel.GetComponent<Image>().sprite = image;
        upgradeButton.onClick.AddListener(ApplyUpgrade);
    }


    void ApplyUpgrade() {
        switch (upgradeType) {
            case UpgradeType.Health:
                player.increaseMaxHealth(Mathf.RoundToInt(magnitude));
                break;
            case UpgradeType.Movespeed:
                player.increaseMoveSpeed(magnitude);
                break;
            case UpgradeType.Damage:
                player.increaseDamage(Mathf.RoundToInt(magnitude));
                break;
        }

        hud.CloseLevelUp();
    }

    enum UpgradeType {
        Health,
        Movespeed,
        Damage,
    }
}  
