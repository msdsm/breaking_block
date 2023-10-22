using UnityEngine;
class Ball : MonoBehaviour
{
    // ボールの移動の速さを指定する変数
    public float speed = 5f;
    // 速さの最大値を指定する変数を追加
    public float minSpeed = 5f;
    // 速さの最小値を指定する変数を追加
    public float maxSpeed = 10f;

    Rigidbody myRigidbody;
    Transform myTransform;

    void Start()
    {
        // Rigidbodyにアクセスして変数に保持しておく
        myRigidbody = GetComponent<Rigidbody>();
        // 右斜めした45度に進む
        myRigidbody.velocity = new Vector3(speed, -speed, 0f);
        myTransform = transform;
    }

     // 毎フレーム速度をチェックする
    void Update()
    {
        // 現在の速度を取得
        Vector3 velocity = myRigidbody.velocity;
        // 速さを計算
        float clampedSpeed = Mathf.Clamp(velocity.magnitude, minSpeed, maxSpeed);
        // 速度を変更
        myRigidbody.velocity = velocity.normalized * clampedSpeed;
    }

     // 衝突したときに呼ばれる
    void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに当たったときに、跳ね返る方向を変える
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーの位置を取得
            Vector3 playerPos = collision.transform.position;
            // ボールの位置を取得
            Vector3 ballPos = myTransform.position;
            // プレイヤーから見たボールの方向を計算
            Vector3 direction = (ballPos - playerPos).normalized;
            // 現在の速さを取得
            float speed = myRigidbody.velocity.magnitude;
            // 速度を変更
            myRigidbody.velocity = direction * speed;
        }
    }
}