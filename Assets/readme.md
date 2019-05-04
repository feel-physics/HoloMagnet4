### 起動のし方

私はUnity2017.2.2を使っています

1. 新規プロジェクトを作り、MRTK（私は2017.2.1.4）を入れます
3. Assetフォルダにリポジトリのフォルダ（HoloMagnet36）を放り込みます
4. ScenesフォルダのScene2Dを開きます
5. Managersオブジェクトの子のSharingオブジェクトを選択します
6. Sharing StageのServer Addressを設定します
7. 実行します

### 操作方法

- 上の方にボタンが並んでいます。
- 並び順はオブジェクトのボタンの並び順と同じで、左から…

1. Grobal Paramsオブジェクト生成（まず最初に行ってください）
2. 磁石のz座標を1.5に固定・解除のトグル
3. 平面状の方位磁針を生成・削除のトグル
4. 3次元の方位磁針を生成・削除のトグル
5. 磁力線の表示・非表示のトグル
6. デバッグログの表示・非表示のトグル

Grobal Paramの持っている変数はUnity Playerで実行したときの
Sharingオブジェクトのインスペクタに表示されます

### Sharing関係の参考になりそうなスクリプト

- SyncSpawnedGlobalParams.cs
  - GlobalParamsオブジェクトを定義します
  - GlobalParamsオブジェクトは、後からSharingに参加したアプリ内にも自動的に生成されます
  - Sharing全体の変数（平面状の方位磁針の生成・非表示など）を持ちます
- CompassPlacer2DSpawner.cs
  - 平面状の方位磁針を生成・削除のトグルを行います
  - GlobalParamsが保持するShouldShowCompass2D変数のtrue/falseを切り替えます
  - trueにしたときは、CompassPlacer2Dオブジェクトを生成します。後からSharingに参加したアプリ内にも自動的に生成されます。
  - falseにしたときは、CompassPlacer2Dオブジェクトを削除し、方位磁針も削除を行います
- GoAndBackMovement.cs
  - 棒磁石を行ったり来たりさせます
  - GlobalParamsが保持するShouldShowCompass3D変数を監視し、trueになったら往復運動を始めます
  - ShouldShowCompass3Dがfalseになった場合は何もしません

### Sharing関係の参考になりそうなコンポーネント

- SharingStageオブジェクトのPrefabSpawnManagerコンポーネントのフィールド
- Spawnさせたいオブジェクトのクラスを指定します