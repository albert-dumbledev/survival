using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeTile : MonoBehaviour
{
    [SerializeField]
    private UpgradeType upgradeType;
    [SerializeField]
    private string effectText;
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

    // Start is called before the first frame update
    void Start()
    {
        titleTextbox.text = upgradeType.ToString();
        effectTextBox.text = effectText;
        imagePanel.GetComponent<Image>().sprite = image;
    }

    // Update is called once per frame
    void Update()   
    {
        
    }

    enum UpgradeType {
        Health,
        Range,
        Movespeed
    }
}
