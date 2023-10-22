using UnityEngine;

using UnityEngine.UI; // for text

using UnityEngine.SceneManagement;

class GameOver : MonoBehaviour
{
	public Text gameOverMessage;
	bool isGameOver = false;//Gameoverしたかどうか
    // 衝突時に呼ばれる

    void Update()
    {
    	if(isGameOver && Input.GetButtonDown("Submit"))
    	{
    		SceneManager.LoadScene("Play");
    	}
    }

    void OnCollisionEnter(Collision collision)
    {
    	gameOverMessage.text = "Game Over";
        // 当たったゲームオブジェクトを削除する
        Destroy(collision.gameObject); 
        isGameOver = true;
    }
}