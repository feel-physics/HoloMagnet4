# HoloMagnet4

## 概要

[HoloMagnet3](https://github.com/feel-physics/HoloMagnet3)の体験をベースに、複数人で体験を共有することに特化させたHoloLens用アプリです。  
旧HoloToolkitのSharing機能を利用し、サーバー用PCを用意することで、簡単に複数人で体験を共有させることができます。  

![App_Cap](https://user-images.githubusercontent.com/14026964/90363866-5c869580-e09e-11ea-9d55-b729ef38f568.png)
![App_Cap](https://user-images.githubusercontent.com/14026964/90363870-5e505900-e09e-11ea-9359-f9fe283a3c90.png)

※HoloLens 2には対応しておりません。  
※Unityのバージョンは2017.4.xを使用しています。  

## 環境構築について

アプリを実行するHoloLensとは別に、サーバーとなるWindows 10のPCを1台用意し、同じネットワークに接続する必要があります。
※LANのファイアウォール設定によっては正常に通信を行うことができないことがあります。

## Sharingサーバ起動

#### Unityを経由して起動する場合

UnityEditorを起動後、上部メニューの[Mixed Reality Toolkit]メニューより[Sharing Service]→[Launch Sharing Service]と選択し、Sharingサーバー用プログラムを起動させます。  
![Launch_Sharing_Service](https://user-images.githubusercontent.com/14026964/90336462-04f11700-e017-11ea-8b19-33a81efb7740.png)

#### コマンドラインを経由して起動する場合

HoloMagnet4/External/HoloToolkit/Sharing/Serverでコマンドプロンプトを起動します。  
起動したら以下のコマンドを実行します。  
```SharingService.exe -local```

### IPアドレスを確認する

Sharingサーバーを起動すると、以下の記載の下にIPアドレスが表示されます。  
```Local IP addresses are:```
![IP_Address](https://user-images.githubusercontent.com/14026964/90336465-06224400-e017-11ea-8777-6c56318bda34.png)

## セットアップ

現在、UnityEditor上で特定のIPアドレスを指定したものをパッケージ化し、HoloLensにインストール後、動作させるような形になっています。  

### HoloMagnet4

#### IPアドレスの設定

Unityエディタでプロジェクトを開き、Projectタブより以下のパスのScene2Dを開きます。  
```HoloMagnet4/Assets/HoloMagnet36/Scenes```
※Unityのバージョンは2017.4.xを使用しています。  
![Open_Scene](https://user-images.githubusercontent.com/14026964/90336466-06224400-e017-11ea-96d2-b7479efea62c.png)

Hierarchyタブから「Managers」の下にある「Sharing」を選択し、  
Inspectorタブから、SharingStageコンポーネントにある「Server Address」に、IPアドレスの確認手順にて確認したIPアドレスを設定します。  
![Setting_IP_Address](https://user-images.githubusercontent.com/14026964/90336467-06bada80-e017-11ea-944f-0da1bb2e19aa.png)

### ビルド

上部メニューの[File]メニューより[Build Settings ...]を選択し、Build Settingsウィンドウを表示します。  
Platformの項目で、Universal Windows Platformが選択され、Unityのアイコンが表示されていることを確認します。  
もしUniversal Windows Platformの項目にUnityのアイコンが表示されていない場合は、Universal Windows Platformを選択し、Switch Platformボタンを押して、プラットフォームを切り替えます。  
![Build_Settings](https://user-images.githubusercontent.com/14026964/90336468-07537100-e017-11ea-84f2-f908b442bc3a.png)

Universal Windows Platformの項目にUnityのアイコンが表示されていることが確認できたら、ウィンドウ下部のBuildボタンを押し、出力先を指定してUnity上でのビルドを実行します。  
出力先にはプロジェクトを開く際に指定したフォルダ内に、新規でフォルダを作り、そのフォルダを指定するようにします。  
(保存先となるフォルダやフォルダ内の既存のファイルの影響で、正常にビルドができない可能性があるため、出力先には、プロジェクトフォルダ内に新規で専用に作成したフォルダを指定することが推奨されます。)  
![Build_Settings_Window](https://user-images.githubusercontent.com/14026964/90336469-07537100-e017-11ea-9337-29ddcc5abf70.png)

Unity上でのビルドが完了したら、出力先に指定したフォルダのあるフォルダを開いた状態でエクスプローラーが起動します。  
出力先フォルダ内に.slnファイルが生成されているので、Visual Studio 2017にて開きます。  
(現在デフォルトでは「181026-HoloMagnet41rt」という名前で.slnファイルが生成されます。)  

### 各HoloLensへのデプロイ

#### Visual Studioから直接HoloLensへインストールする場合

データ転送可能なUSBケーブルにて、PCとHoloLensを接続します。  
Visual Studioの配置ターゲットをx86、Deviceに変更します。  
![VisualStudio_x86](https://user-images.githubusercontent.com/14026964/90338515-17724d00-e025-11ea-8121-4a322dbcd14b.png)
![VisualStudio_Device](https://user-images.githubusercontent.com/14026964/90338516-17724d00-e025-11ea-9b3f-1832a34251e8.png)

Deviceと書かれた再生マークのあるボタンをクリックし、実機へのインストールとデバッグ実行を行います。  

初めてのPCとHoloLensの組み合わせで転送を行う場合は、PINを入力する必要があります。  
![VisualStudio_Pin](https://user-images.githubusercontent.com/14026964/90338519-180ae380-e025-11ea-9093-e95e8c8f9916.png)

HoloLens上で、設定アプリを起動し、「更新とセキュリティ」の項目を選択します。  
![HoloLens_Setting](https://user-images.githubusercontent.com/14026964/90338510-15a88980-e025-11ea-8370-d952b300c2c9.jpg)
左側のメニューから「開発者向け」を選択し、デバイスの検出の項目にある、「ペアリング」ボタンをAirTapします。  
![HoloLens_Pairing](https://user-images.githubusercontent.com/14026964/90338513-16d9b680-e025-11ea-94bc-0219b353a819.jpg)

「デバイスのペアリング」と表示され、下に6桁の数字が表示されるので、Visual Studio側に表示されているPINの入力ウィンドウにて同じ数字を入力して、OKをクリックします。  
![HoloLens_Pin](https://user-images.githubusercontent.com/14026964/90338623-e5151f80-e025-11ea-95fc-ec719aa0125d.jpg)

#### パッケージデータを作成し、デバイスポータル経由でHoloLensへインストールする場合

Visual Studioの上部メニューの[プロジェクト]より[ストア]→[アプリパッケージの作成]を選択します。  
![App_Package](https://user-images.githubusercontent.com/14026964/90338520-18a37a00-e025-11ea-82a7-81679fff8d42.png)

アプリパッケージの作成ウィンドウで、「サイドロード用のパッケージを作成します。」を選択し、次へボタンをクリックします。  
![App_Package_Window](https://user-images.githubusercontent.com/14026964/90338521-193c1080-e025-11ea-927d-f88b20e8f30f.png)

「作成するパッケージとソリューション構成マッピングを選択する」の項目内で、アーキテクチャが「x86」となっている項目のみにチェックを入れ、作成ボタンをクリックします。  
![App_Package_Target](https://user-images.githubusercontent.com/14026964/90338522-193c1080-e025-11ea-8a20-c7fc311bb65c.png)

パッケージの作成が完了すると、パッケージの出力先が表示されます。  
出力先には「.appxbundle」ファイル等が生成されています。  

パッケージの作成が完了したら、HoloLensにデバイスポータル経由でインストールを行います。  
HoloLensをPCにUSBケーブルで有線接続し、「localhost:10080」とブラウザのURLに入力します。  
デバイスポータルの概要、セットアップ手順については、[公式ドキュメント](https://docs.microsoft.com/ja-jp/windows/mixed-reality/using-the-windows-device-portal)を参照。  

ブラウザからHoloLensのデバイスポータルにアクセスしたら、左側のメニューより「Apps」を選択します。  
![Device_Portal_Apps](https://user-images.githubusercontent.com/14026964/90345540-64bde100-e05c-11ea-9175-ee0d141f99ad.png)

Deploy Appsの項目にて、「ファイルの選択」ボタンから、先ほど生成された「.appxbundle」ファイルを選択します。  

また、初回インストール時等、必要に応じて「Allow me to select framework packages」にチェックを入れ、「Next」ボタンをクリックします。  
「Allow me to select framework packages」にチェックを入れた場合は、「Choose any necessary dependencies:」の「ファイルの選択」ボタンから、「.appxbundle」ファイルの入っているフォルダにある、「Dependencies」→「x86」とフォルダを開き、その中にある「.appx」ファイルを全て選択します。  
![Device_Portal_Apps_Dependencies](https://user-images.githubusercontent.com/14026964/90345542-65ef0e00-e05c-11ea-97cb-872c457e6d64.png)

最後に「Install」ボタンをクリックし、HoloLensへのインストールを行います。  
  
※デバイスポータルの画面レイアウトはHoloLensのOSバージョンによって多少異なります。  

[デプロイの参考URL(公式ドキュメント)](https://docs.microsoft.com/ja-jp/windows/mixed-reality/using-visual-studio)

## 操作方法

### シーン遷移方法

HoloLens上でアプリを起動後、正面を向いたまま、視点を上にあげていくと、ボタンがあります。  
このボタンに視線方向正面の丸いカーソルを合わせてAirTap動作をすることで、シーンを順番に遷移させていき、体験できるコンテンツを切り替えていくことができます。  

### シーン説明

#### 2次元

上下左右方向に展開したコンパスに対して、棒磁石を上下左右方向に移動させ、奥行のない平面空間で、磁力が、どの位置にどの向きで働いているのか見ることができます。  
正面方向に人差し指を立てて手を構え、親指と人差し指でつまみ、手を動かすことで、棒磁石をつまんで平行移動させることができます。  
移動することのできる棒磁石には、自分の位置からピンク色のラインがつながっている棒磁石のみとなります。  

#### 3次元

上下左右方向に加え、奥行方向も展開したコンパス内を棒磁石が移動し、2次元のシーンで確認した磁力が3次元的にはどのように働いているのかを見ることができます。  
このシーンでは棒磁石を動かすことはできず、左右に往復移動する棒磁石を様々な方向から見ることができるシーンとなっています。  

#### 磁力線

棒磁石に対して磁力線を表示し、磁力線を見ることができます。  
体験者1人に対して棒磁石が表示されているため、複数人で体験することにより、複数の棒磁石が表示され、それぞれの棒磁石が磁力線にどのような影響を与えるのかを確認することもできます。  
