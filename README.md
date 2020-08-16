# HoloMagnet4

## 概要
[HoloMagnet3](https://github.com/feel-physics/HoloMagnet3)の体験をベースに、複数人で体験を共有することに特化させたHoloLens用アプリです。<br>
旧HoloToolkitのSharing機能を利用し、サーバー用PCを用意することで、簡単に複数人で体験を共有させることができます。<br>
※HoloLens 2には対応しておりません。<br>
※Unityのバージョンは2017.4.xを使用しています。<br>

## 環境構築について
アプリを実行するHoloLensとは別に、サーバーとなるWindows 10のPCを1台用意し、同じネットワークに接続する必要があります。
※LANのファイアウォール設定によっては正常に通信を行うことができないことがあります。

## Sharingサーバ起動

#### Unityを経由して起動する場合
UnityEditorを起動後、上部メニューの[Mixed Reality Toolkit]メニューより[Sharing Service]→[Launch Sharing Service]と選択し、Sharingサーバー用プログラムを起動させます。<br>
![Launch_Sharing_Service](https://user-images.githubusercontent.com/14026964/90336462-04f11700-e017-11ea-8b19-33a81efb7740.png)

#### コマンドラインを経由して起動する場合
HoloMagnet4/External/HoloToolkit/Sharing/Serverでコマンドプロンプトを起動します。<br>
起動したら以下のコマンドを実行します。<br>
```SharingService.exe -local```

### IPアドレスを確認する
Sharingサーバーを起動すると、以下の記載の下にIPアドレスが表示されます。<br>
```Local IP addresses are:```
![IP_Address](https://user-images.githubusercontent.com/14026964/90336465-06224400-e017-11ea-8777-6c56318bda34.png)

## セットアップ
現在、UnityEditor上で特定のIPアドレスを指定したものをパッケージ化し、HoloLensにインストール後、動作させるような形になっています。<br>

### HoloMagnet4

#### IPアドレスの設定
Unityエディタでプロジェクトを開き、Projectタブより以下のパスのScene2Dを開きます。<br>
```HoloMagnet4/Assets/HoloMagnet36/Scenes```
※Unityのバージョンは2017.4.xを使用しています。<br>
![Open_Scene](https://user-images.githubusercontent.com/14026964/90336466-06224400-e017-11ea-96d2-b7479efea62c.png)

Hierarchyタブから「Managers」の下にある「Sharing」を選択し、<br>
Inspectorタブから、SharingStageコンポーネントにある「Server Address」に、IPアドレスの確認手順にて確認したIPアドレスを設定します。<br>
![Setting_IP_Address](https://user-images.githubusercontent.com/14026964/90336467-06bada80-e017-11ea-944f-0da1bb2e19aa.png)

### ビルド
上部メニューの[File]メニューより[Build Settings ...]を選択し、Build Settingsウィンドウを表示します。<br>
Platformの項目で、Universal Windows Platformが選択され、Unityのアイコンが表示されていることを確認します。<br>
もしUniversal Windows Platformの項目にUnityのアイコンが表示されていない場合は、Universal Windows Platformを選択し、Switch Platformボタンを押して、プラットフォームを切り替えます。<br>
![Build_Settings](https://user-images.githubusercontent.com/14026964/90336468-07537100-e017-11ea-84f2-f908b442bc3a.png)

Universal Windows Platformの項目にUnityのアイコンが表示されていることが確認できたら、ウィンドウ下部のBuildボタンを押し、出力先を指定してUnity上でのビルドを実行します。<br>
出力先にはプロジェクトを開く際に指定したフォルダ内に、新規でフォルダを作り、そのフォルダを指定するようにします。<br>
(保存先となるフォルダやフォルダ内の既存のファイルに影響で、正常にビルドができない可能性があるため、出力先には、プロジェクトフォルダ内に新規で専用に作成したフォルダを指定することが推奨されます。)<br>
![Build_Settings_Window](https://user-images.githubusercontent.com/14026964/90336469-07537100-e017-11ea-9337-29ddcc5abf70.png)

Unity上でのビルドが完了したら、出力先に指定したフォルダのあるフォルダを開いた状態でエクスプローラーが起動します。<br>
出力先フォルダ内に.slnファイルが生成されているので、Visual Studio 2017にて開きます。<br>
(現在デフォルトでは「181026-HoloMagnet41rt」という名前で.slnファイルが生成されます。)<br>

### 各HoloLensへのデプロイ

#### Visual Studioから直接HoloLensへインストールする場合
データ転送可能なUSBケーブルにて、PCとHoloLensを接続します。<br>
Visual Studioの配置ターゲットをx86、Deviceに変更します。<br>
Deviceと書かれた再生マークのあるボタンをクリックし、実機へのインストールとデバッグ実行を行います。<br>
<br>
初めてのPCとHoloLensの組み合わせで転送を行う場合は、PINを入力する必要があります。<br>

HoloLens上で、設定アプリを起動し、「更新とセキュリティ」の項目を選択します。<br>
左側のメニューから「開発者向け」を選択し、デバイスの検出の項目にある、「ペアリング」ボタンをAirTapします。<br>
「デバイスのペアリング」と表示され、下に6桁の数字が表示されるので、Visual Studio側に表示されているPINの入力ウィンドウにて同じ数字を入力して、OKをクリックします。<br>

#### パッケージデータを作成し、デバイスポータル経由でHoloLensへインストールする場合
Visual Studioの上部メニューの[プロジェクト]より[ストア]→[アプリパッケージの作成]を選択します。<br>
アプリパッケージの作成ウィンドウで、「サイドロード用のパッケージを作成します。」を選択し、次へボタンをクリックします。<br>
「作成するパッケージとソリューション構成マッピングを選択する」の項目内で、アーキテクチャが「x86」となっている項目のみにチェックを入れ、作成ボタンをクリックします。<br>


[デプロイの参考URL(公式ドキュメント)](https://docs.microsoft.com/ja-jp/windows/mixed-reality/using-visual-studio)

## 操作方法

### シーン遷移方法
HoloLens上でアプリを起動後、正面を向いたまま、視点を上にあげていくと、ボタンがあります。<br>
このボタンに視線方向正面の丸いカーソルを合わせてAirTap動作をすることで、シーンを順番に遷移させていき、体験できるコンテンツを切り替えていくことができます。<br>

### シーン説明

#### 2次元
上下左右方向に展開したコンパスに対して、棒磁石を上下左右方向に移動させ、奥行のない平面空間で、磁力が、どの位置にどの向きで働いているのか見ることができます。<br>
正面方向に人差し指を立てて手を構え、親指と人差し指でつまみ、手を動かすことで、棒磁石をつまんで平行移動させることができます。<br>
移動することのできる棒磁石には、自分の位置からピンク色のラインがつながっている棒磁石のみとなります。<br>

#### 3次元
上下左右方向に加え、奥行方向も展開したコンパス内を棒磁石が移動し、2次元のシーンで確認した磁力が3次元的にはどのように働いているのかを見ることができます。<br>
このシーンでは棒磁石を動かすことはできず、左右に往復移動する棒磁石を様々な方向から見ることができるシーンとなっています。<br>

#### 磁力線
棒磁石に対して磁力線を表示し、磁力線を見ることができます。<br>
体験者1人に対して棒磁石が表示されているため、複数人で体験することにより、複数の棒磁石が表示され、それぞれの棒磁石が磁力線にどのような影響を与えるのかを確認することもできます。<br>
