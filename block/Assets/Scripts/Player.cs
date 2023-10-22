using UnityEngine;

class Player : MonoBehaviour
{
    // プレイヤーの移動の速さ
    public float speed = 10f;
    Rigidbody myRigidbody;

    void Start()
    {
        // Rigidbodyにアクセスして変数に保持
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 左右のキー入力により速度を変更する
        myRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, 0f, 0f);
    }
}