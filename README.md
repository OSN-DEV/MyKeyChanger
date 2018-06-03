# MyKeyChanger
キーマッピングの変更ツール


## 概要
現在はHHKBの日本語配列のキーボードを使用していますが、Planckキーボードのように少ないキー配列でキー入力を行えるよう、ソフトウェアでキー配置を変更するためのツールとなります。


## やっていること
入力されたキーをフックして別のキーを代わりに送信


## キーマッピング
![00_default](https://user-images.githubusercontent.com/31182578/40883648-6d1bf9c6-673d-11e8-998c-f8639c6db23f.jpg)
![01_F14](https://user-images.githubusercontent.com/31182578/40883651-7c3bac62-673d-11e8-86df-37788d5878c3.jpg)
![02_F13](https://user-images.githubusercontent.com/31182578/40883653-87ed45c0-673d-11e8-9710-d4a5bac17507.jpg)
![03_FN2](https://user-images.githubusercontent.com/31182578/40883654-9281ac2e-673d-11e8-9ad6-081ac967c812.jpg)



## 制限
* キー入力を監視しているのでキー入力の反応が遅れる可能性があります。 
* 十分なデバッグは行っていないので予期せぬ動作を起こす可能性があります。
* Altとの組み合わせ(例えばAlt + F4)が動作しないことを確認しています。


## その他
以下の単一キー変換はレジストリの設定をいじっています。あとキーボードのDipスイッチもデフォルトから変更して左FnをCtrlに変更しています
* CapsLock → F13
* Tab → F14
* Windows(左) → Alt(左)
* 半角/全角 → Windows(左)
* Shift(右) → Tab

