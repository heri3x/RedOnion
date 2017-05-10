# Unityメモ：新規プロジェクト

新規プロジェクト作成時に設定しておくことメモ。


## エディタ設定

[Edit > Project Settings... > Editor] メニューを開く。

- Asset Serialization : [Force Text] に設定

    シーン配置情報などをテキスト形式で保存するようにする。デフォルトではバイナリ混在で保存されるが、これはGitなどでマージする時に使いづらい。

## プレイヤー設定

「Edit」→「Project Settings...」→「Player」を開く。

- Windows/Mac用ゲームの場合:

    ウインドウモードで動作するゲームなら下記の没入設定を外しておく。

    - Default Is Full Screen : フルスクリーンで起動するか（チェックを外す）
    - Run In Background : バックグランドに回ってもゲームの実行を続けるか（チェックする）


- 解像度と画面比率を固定したい場合:

    - Default Screen Width : 1280
    - Default Screen Height : 720
    - Supported Aspect Ratios : 16:9 だけチェック状態にして、他の解像度比率はチェックを外す
