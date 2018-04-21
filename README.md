# MyKeyChanger
キーマッピングの変更ツール


## 概要
現在はHHKBの日本語配列のキーボードを使用していますが、Planckキーボードのように少ないキー配列でキー入力を行えるよう、ソフトウェアでキー配置を変更するためのツールとなります。


## やっていること
入力されたキーをフックして別のキーを代わりに送信


## キーマッピング
![00_planck_default](https://user-images.githubusercontent.com/31182578/39084000-2063fc7e-45a9-11e8-9cb0-3d8f4ba00f5d.jpg)
![01_planck_user1](https://user-images.githubusercontent.com/31182578/39083996-1fc9283e-45a9-11e8-986e-2ff5a29ec5a8.jpg)
![02_planck_user2](https://user-images.githubusercontent.com/31182578/39083997-1fefd9e8-45a9-11e8-893f-5a482997ff90.jpg)
![03_planck_user3](https://user-images.githubusercontent.com/31182578/39083998-20132a4c-45a9-11e8-834c-93045ccc6389.jpg)
![04_planck_user4](https://user-images.githubusercontent.com/31182578/39083999-203f69e0-45a9-11e8-8263-31e7f6738b3f.jpg)



## 制限
* キー入力を監視しているのでキー入力の反応が遅れる可能性があります。 
* 十分なデバッグは行っていないので予期せぬ動作を起こす可能性があります。
* Altとの組み合わせ(例えばAlt + F4)が動作しないことを確認しています。


## その他
以下の単一キー変換はレジストリの設定をいじっています。あとキーボードのDipスイッチもデフォルトから変更して左FnをCtrlに変更しています
* CapsLock → F13
* Windows(左) → Alt(左)
* 半角/全角 → Windows(左)
