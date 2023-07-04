using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour {
    [SerializeField]
    private Image currentHealthBar;
    [SerializeField]
    private List<Image> splashImages;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject startScreenPanel;
    [SerializeField]
    private TextMeshProUGUI finalCodePoints;
    [SerializeField]
    private TextMeshProUGUI charlieMessage;
    [SerializeField]
    private TextMeshProUGUI health;
    [SerializeField]
    private TextMeshProUGUI codePoints;
    [SerializeField]
    private TextMeshProUGUI gameTimer;
    private float gameTime;

    void Start() {
        Time.timeScale = 0;
        gameTime = 0f;
    }
    void Update() {
        if (startScreenPanel.activeSelf && Input.anyKeyDown) {
            startScreenPanel.SetActive(false);
            Time.timeScale = 1;
        }
        gameTime += Time.deltaTime;
        gameTimer.text = "" + (int)gameTime + "s";

        if (gameOverPanel.activeSelf && Input.GetButtonDown("Submit")) {
            gameTime = 0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth) {
        health.text = currentHealth + " / " + maxHealth;
        currentHealthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void UpdateCodePoints(int codePoints) {
        this.codePoints.text = "" + codePoints;
    }

    public void ShowGameOver(int codePoints) {
        gameOverPanel.SetActive(true);
        finalCodePoints.text = "" + codePoints;
        if (codePoints < 20) {
            charlieMessage.text = "Maybe you should \"stick\" to sticky notes";
            splashImages[0].gameObject.SetActive(true);
        } else if (codePoints < 69) {
            charlieMessage.text = "500 Internal server error";
            splashImages[1].gameObject.SetActive(true);
        } else if (codePoints == 69) {
            charlieMessage.text = "Nice";
            splashImages[2].gameObject.SetActive(true);
        } else if (codePoints < 149) {
            charlieMessage.text = "Jira Service Management? I run my sprints with it!";
            splashImages[3].gameObject.SetActive(true);
        } else if (codePoints < 249) {
            charlieMessage.text = "Atlassian Certified Expert";
            splashImages[4].gameObject.SetActive(true);
        } else if (codePoints < 419) {
            charlieMessage.text = "Find out where a career at Atlassian could take you!";
            splashImages[5].gameObject.SetActive(true);
        } else if (codePoints == 420) {
            charlieMessage.text = "You're on fire!";
            splashImages[6].gameObject.SetActive(true);
        } else {
            charlieMessage.text = "Uh oh...";
            splashImages[7].gameObject.SetActive(true);
        }
    }
}
