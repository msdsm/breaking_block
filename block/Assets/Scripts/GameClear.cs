using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
class GameClear : MonoBehaviour
{
    Transform myTransform;
    Text gameClearMessage;
    bool isGameClear = false;
    void Start()
    {
        // Transformコンポーネントを保持しておく
        // Updateで何度もtransformコンポーネントにアクセスするから
        myTransform = transform;
        GameObject go = GameObject.Find("Canvas/Text");
        gameClearMessage = go.GetComponent<Text>();
    }

    void Update()
    {
        // 子供がいなくなったらゲームを停止する
        if (myTransform.childCount == 0)
        {
            gameClearMessage.text = "Game Clear";
            Time.timeScale = 0f;
            isGameClear = true;
        }

        if(isGameClear && Input.GetButtonDown("Submit"))
        {
            Time.timeScale = 1f; // timeScaleを1に戻す
            SceneManager.LoadScene("Play");//シーンのロード
        }
    }
}