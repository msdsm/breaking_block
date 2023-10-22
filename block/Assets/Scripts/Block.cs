using UnityEngine;

class Block : MonoBehaviour
{
    // 何かとぶつかった時に呼ばれるビルトインメソッド
    void OnCollisionEnter(Collision collision)
    {
        // ゲームオブジェクトを削除するメソッド
        Destroy(gameObject);
    }
}