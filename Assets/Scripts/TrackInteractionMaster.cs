using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrackInteractionMaster : MonoBehaviour
{
    public Text timeText, healthText, gameOverText;
    public GameObject destroyable1_1, destroyable1_2, destroyable2, destroyable3_1, destroyable3_2, gameOverButton;
    float time = 60;
    // Start is called before the first frame update

    private void Start()
    {
        gameOverText.enabled = false;
        gameOverButton.SetActive(false);
    }
    void Update()
    {

        time -= Time.deltaTime;
        timeText.text = time.ToString();
        if (time < 50)
        {
            Debug.Log("Caida");
            //DropBlock(destroyable1_1);
            //DropBlock(destroyable1_2);
        }
        if (time < 40)
        {
            Destroy(destroyable2);
        }

        if (time < 30)
        {
            Rigidbody destroyRigid1 = destroyable3_1.AddComponent<Rigidbody>();
            Rigidbody destroyRigid2 = destroyable3_2.AddComponent<Rigidbody>();
            destroyRigid1.useGravity = true;
            destroyRigid2.useGravity = true;
        }

        if (time < 0)
        {
            gameOver();
        }
    }

    public void gameOver()
    {
        gameOverText.enabled = true;
        gameOverButton.SetActive(true);
        healthText.enabled = false;
        timeText.enabled = false;
    }

    public void winner()
    {
        gameOverText.text = "Winner!";
        gameOverText.enabled = true;
        gameOverButton.SetActive(true);
        healthText.enabled = false;
        timeText.enabled = false;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
