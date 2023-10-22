# Unityでブロック崩しゲーム作成

## プロジェクトの場所
- `block`

## 遊び方
- 左右の矢印キーで移動させる
- ゲームクリアまたはゲームオーバー後はキーをどこか押すことでもう一度プレイできる

## オブジェクト一覧
- Main Camera
- Wall Left
- Wall right
- Wall Top
- Wall Bottom
- Ball
- Player
- Blocks
    - Block
    - Block(2)
    - Block(3)
    - Block(4)
    - Block(5)
- Canvas
    - Text
- EventSystem


## 各Assetsの説明
- `Materials`
    - 各マテリアルをまとめた
    - 主に色を変更するために使用した
- `PhysicsMaterials`
    - 物理演算のためのマテリアルをまとめた
- `Prefabs`
    - ブロックをプレハブにしたもの
- `Scripts`
    - C#をここに保存した
    - 以下が各ファイルの適用先である
        - `Ball.cs`:Ball
        - `Block.cs`:Block
        - `GameClear.cs`:Blocks
        - `GameOver.cs`:Wall Bottom
        - `Player.cs`:Player
        - `ResetText.cs`:Text

## 今後やること
- 作って学べるUnity本格入門を読み終える
- スマブラみたいなやつ作ってみる
- `Input`についての勉強
    - https://hu-gsd.com/lecture/unity-input-key/ 
- ブラウザ上にビルドしてみる


## 作成過程
- 以下を参考
- https://hu-gsd.com/lecture/unity-block-1/


### 2023/10/15
- Sublime Text 3 install
- `.bashrc`を編集してpath通す
    - `'alias subl='C:/Program\ Files/Sublime\ Text\ 3/sublime_text.exe'`
    - `source .bashrc`
    - これによって`subl`コマンドで開けるようになる
- 本読み進めながらUnity触ってみる
    - 1章:導入
    - 2章:跳ね返るボール
        - planeに画像を挿入する際に繰り返しにできなかった
    - 3章:C#
        - 基本的な文法について学んだ
        - 下の自分用メモに残した
        - いろいろ実際に手を動かして書いてみないとよくわからないと思った
    - 4章:開発の基本
        - 特に内容ない

### 2023/10/17
- 実際にブロック崩し作る
- 2Dで新規プロジェクト作成
    - Desktopに`block`という名前で保存
- Assets/ScenesをPlayという名前に変更
- 以下Playでの処理
    - Main Cameraの設定
        - TransformコンポーネントのPositionを(0,0,-20)に変更
        - Cameraコンポーネントのsizeを12に変更
        - CameraコンポーネントのBackgroundから背景色変更

    - 壁の作成
        - 3D ObjectのCubeを4つ作成
        - 座標を計算してPositionとScaleを変更
        - AssetsにMaterialsフォルダを作成
        - Materialsフォルダ内にWallマテリアルを作成
            - Shederの箇所をUnlit/Colorに変更
            - 4つの壁に適用
                - 各オブジェクトをクリックしてInspectorで選択
                - または、WallマテリアルをHierarchyビューにドラッグアンドドロップしてもできる
    - ボールの作成
        - 3D ObjectのSphereを作成
            - PositionとScaleを変更
            - 壁と同様にBallマテリアルをAssets/Materialsに作成して適用(色を自由に決めるため) 
        - Rigidbodyを設定する
            - Inspectorビューの下にあるAdd ComponentからPhysics/Rigidbodyを選択
                - Drag:0
                - Angular Drag:0
                - Use Gravity:off
                - Constraints Freeze position z:on(z方向に動かさないため)
                - Constraints Freeze rotation x,y,z:on(回転させないため)
        - Physics Materialを設定する
            - Assets/PhysicsMaterialsにPhysics Materialを作成(NoFrictionという名前)
                - Dynamic Friction:0
                - Static Friction:0
                - Bounciness:1
                - Friction Combine:Minimum(任意のobjectとの衝突で摩擦を発生させないため)
                - Bounce Combine:Maximum(任意のobjectの衝突で減衰せずに反発させるため)
            - BallのSpher ColliderのMaterialに設定
        - スクリプトの作成
            - Assets/Scriptsを作成して、その中に`Ball.cs`を作成
                - Ball.csの中身の理解が浅い。Unityの本を最後までやってC#の理解深める
            - 45度斜めに進むように速度設定
            - Ballに適用
    - プレイヤーの作成
        - Cubeを作成
            - Position,Scale変更
        - Rigidbodyを設定
            - Drag,Angular Drag:0
            - Use Gravity:off
            - Constraints Freeze position y,z:on(x方向のみ動かす)
            - Constraints Freeze rotation x,y,z:on(回転させない)
            - Mass:100(ボールを跳ね返すために重くする)
        - スクリプトの作成
            - `Player.cs`をAssets/Scriptsに作成
            - `Update()`で毎フレームごとに入力受け取って動かすようにする
    - ブロックの作成
        - Cubeを作成
            - Position,Scale調整
        - プレハブにする
            - Assets/Prefabsを作成
            - Prefabsにblockを作成してドラッグアンドドロップで複製
                - Prefabsのblockを変更すればblock全部変更できて便利
        - スクリプトの作成
            - `Block.cs`を作成
            - `OnCollisionEnter()`で衝突を検出した際に`Destroy`でゲームオブジェクトを削除する
    - ゲームオーバーの実装
        - `GameOver.cs`を作成してWall Bottomに適用
            - 衝突を検知したら衝突した相手のオブジェクトを削除する
    - ゲームクリアの実装
        - Blocksというempty objectを作成
        - その中にBlock全部入れる
        - Blocksに`GameClear.cs`を適用
            - `Transform.childCount`で子オブジェクトの個数をフレームごとにcheck
            - 0になったらゲーム終了
                - Time.timeScaleを0にする
    - テキスト表示
        - UI/Textを作成(Legacy)
            - Canvas/Textが作成される
            - EventSystemが作成される
                -  ナニコレ？
            - Canvasの設定
                - Render ModeをScreen Space-Overlayに変更
                    - Screen Space - Overlay : 常に画面手前に描画
                    - Screen Space - Camera : 指定したカメラの前に描画
                    - World Space : シーン内の平面オブジェクトであるかのように描画
                - UI Scale ModeをScreen With Screen Sizeに変更
            - Textの設定
                - 位置調整、色調整
                - `ResetText.cs`を適用
                    - 文字列を空で初期化
            - ゲームオーバー時の設定
                - `GameOver.cs`を編集
                    - `public Text`をグローバルフィールドとして宣言してInspectorから設定できるようにする
                    - `OnCollisionEnter(Collision collision)`内でテキスト内容を変更
            - ゲームクリア時の設定
                - `GameClear.cs`を編集
                    - `GameOver.cs`とは違う方法でやってみた
                    - `GameObject.Find(path文字列)`で`Text`オブジェクトを取得(path:Canvas/Text)
                    - `GameObject.GetComponent<Text>()`でTextコンポーネント取得
                    - `GameObject.GetComponent<Text>().text`で文字列変更

### 2023/10/22
- リトライの実装
    - ゲームオーバー時にやり直せるようにする
        - `GameOver.cs`を編集
            - `Input.GetButtonDown("Submit)`でキー入力を受け取る
            - ゲームオーバー時にsceneをloadするようにする
        - File->Build Settingswを開いてPlayシーンをScenes In Buildにドラッグアンドドロップで追加
    - ゲームクリア時にやり直せるようにする
        - `GameClear.cs`を編集

- ボールの改善
    - `Ball.cs`を編集
        - スピード調整
            - 最小値と最大値を設定できるようにする
            - Updateで毎フレームごとにスピードチェック
        - 反射の方向調整
            - 左から来たときは左に反射
            - 右から来たときは右に反射
            - Playerと衝突したときを検知して速度ベクトルを変更する
    - プレイヤーオブジェクトにplayerタグを追加
         - Inspectorビューから変更可能

### 2023/10/23
- Unity本
- `Input`についての勉強
    - https://hu-gsd.com/lecture/unity-input-key/ 
- ブラウザ上にビルドしてみる

## 自分用メモ
### Unity
#### Sceneビューでの操作
- 視点上下移動:スクロール
- その場回転:Ctrl+Altでドラッグ
- 平行移動:Altでドラッグ

#### Rigidbody
- ComponentのPhysicsの中にある
- プロパティは以下
    - Mass:オブジェクトの重さ(kg)
    - Drag:オブジェクトの空気抵抗
    - Angular Drag:回転に対する空気抵抗
    - Use Gravity:重力を適用するかどうか
    - Is Kinematic:固定されたものに使用する。スクリプトで動かさない限り動かなくなる
    - Interpolate・Extrapolate:描画と物理演算に生じるズレを軽減する
    - Collisions Detection:壁などを貫通しなくなる
        - Continuous:動かないオブジェクトと衝突する場合
        - Continuous Dynamic:動くオブジェクトと衝突する場合
    - Constraints:制御
        - Freeze Position:移動しなくなる
        - Freeze Rotation:回転しなくなる

#### Physics Material
- Assets内にある
- プロパティは以下
        - Dynamic Friction:動摩擦係数[0,1]
        - Static Friction:静止摩擦係数[0,1]
        - Bounciness:弾性率
        - Friction Combine:2つのオブジェクトが接しているときに摩擦係数をどのように使うか決定する
                - Average:平均
                - Maximum:最大
                - Minimum:最小
                - Multiply:乗算
        - Bounce Combine:Friction Combineの弾性率version

#### ゲームオブジェクトの親子関係
- 親のゲームオブジェクトが動くと、子供のゲームオブジェクトも一緒に動く
- 子供のゲームオブジェクトが動いても、親のゲームオブジェクトは動かない


### C#
- Javaのようにオブジェクト指向でかける

#### 型一覧
- bool:`bool`
- 整数:`int`
- 小数:`float`
- 文字列:`string`
- 静的配列:`int[]`など
- 動的配列:`List`
- 連想配列(key-value構造):`Dictionary`
- 列挙型:`enum`
- 定数:`const int`など
- ベクトル型:`Vector2`,`Vector3`
        - 数学のベクトルと同じ扱い
        - C++でいう`pair<int,int>`みたいなもの
- 型推論:`var`

#### 構文一覧
- `if`,`else if`, `else`,`for`,`while`,`switch`:すべてCと同じ
- `foreach`:Pythonのforみたいなやつ、JSと同じ

#### オブジェクト指向
- アクセス修飾子
        - `public`:どこからでもアクセス可能
        - `protected`:そのクラス及び継承したクラスからアクセス可能
        - `private`:そのクラスの中からのみアクセス可能、C#ではデフォルトでこれ
- 継承関連
        - `abstract`:抽象クラス。継承専用でありそのクラス自体はインスタンス化できない
        - `sealed`:クラスを継承させないときに使う
- 抽象メソッド、オーバーライド関連
        - `abstract`とついたメソッドは抽象メソッド
                - 中身はかけない
                - 子クラスで継承して中身をかける
        - 子クラスでメソッドをオーバーライドするには親クラスでメソッドに`virtual`をつける
        - 親クラスのメソッドは`base.メソッド名()`で呼び出せる

#### ライフサイクル
- ゲームオブジェクトの生成から破棄までの一連の流れのことをライフサイクルという
- ゲームオブジェクト用のスクリプトを各場合、`UnityEngine.MonoBehaviour`というクラスを継承
        - これによりゲームオブジェクトのライフサイクルに応じて特定のメソッドが呼び出される
- 以下が代表的ななメソッド

##### `void Awake()`
- ゲームオブジェクトが生成される際に最初に一度だけ呼び出される
- そのゲームオブジェクトが無効だった場合は有効になるまで呼び出されない

##### `void Start()`
- ゲームオブジェクトが生成されたあとに`Update()`コールが始まる前に一度だけ呼び出される
- 返り値の型を`void`と`IEnumerator`の2種類のうちいずれかを宣言できる
        - `IEnumerator`にするとコルーチンとして実行される

##### `void Update()`
- ゲーム実行中毎フレーム呼び出される
- 時間のかかる処理をかくと処理落ちが発生
        - 時間のかかる処理はここに書いてはいけない

##### `void FixedUpdate()`
- 物理エンジンの演算が行われるタイミングで呼び出される

##### `void OnDestroy()`
- ゲームオブジェクトが破棄されるタイミングで呼び出される

##### `void OnEnabled()`
- ゲームオブジェクトが有効になる際に呼び出される

##### `void OnDisabled()`
- ゲームオブジェクトが無効になる際に呼び出される

##### `void OnBecameInvisible()`
- ゲームオブジェクトがカメラの撮影範囲から出た際に呼び出される

##### `void OnBecameVisible()`
- カメラの撮影範囲に入った際に呼び出される

##### `OnCollisionEnter(Collision collision)`
- なにかとぶつかったときに呼ばれるビルドインメソッド
        - `collision.gameObject`で衝突した相手のゲームオブジェクトを得られる

#### その他
##### `Input.GetAxis()`
- `Update()`内で使うと舞フレームごとに入力受け取れる
- `Input.GetAxis("Horizontal")`
        - 右入力の時1
        - 左入力の時-1
        - 入力なしの時0

##### `Destroy(gameObject)`
- ゲームオブジェクトを削除するメソッド

##### `Transform.childCount`
- 子オブジェクトの個数

##### `Time.timsScale`
- 1:通常
- 1より大:倍速
- 1より小:スロー再生
- 0:ゲーム停止
        - RigidBbodyを用いて物理演算で動かしているオブジェクト停止
        - Updateメソッドは実行される

##### `Mathf.Clamp(x,left,right)`
- xがleftより小さいならleftを返す
- xがrightより大きいならrightを返す
- それ以外はxを返す

##### Vector3.magnitude
- `Vector3`のフィールドとして`magnitude`がある(`float`)
- ベクトルの大きさを取得できる

##### Vector3.normalized
- `Vector3`のフィールドとして`normalized`がある
- ベクトルの単位ベクトルを取得できる(方向を得たいときに使える)