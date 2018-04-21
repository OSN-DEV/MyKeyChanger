# MyKeyChangerJP
MyKeyChangerの日本語配列版


## 概要
MyKeyChangerと考え方は一緒です。違いはWindowsの設定が日本語配列(106/109)である点だけです。  
バージョン1.1で修飾キーに「:」「\」を追加しました。


## やっていること
入力されたキーをフックして別のキーを代わりに送信


## キーマッピング
![muhenkan](https://user-images.githubusercontent.com/31182578/36090214-d99eb6dc-1022-11e8-8848-39de832a4d23.jpg)
![henkan](https://user-images.githubusercontent.com/31182578/36093173-30cea110-102d-11e8-8cd6-1234811938fa.jpg)
![muhenkanhenkan](https://user-images.githubusercontent.com/31182578/36090385-8e069554-1023-11e8-8381-0d58a42d137d.jpg)
![user1](https://user-images.githubusercontent.com/31182578/36093184-4104d7a2-102d-11e8-9733-828d52f94bed.jpg)
![user2](https://user-images.githubusercontent.com/31182578/36090399-9d3e64ca-1023-11e8-9b3a-ddaf595820c2.jpg)


## 制限
* キー入力を監視しているのでキー入力の反応が遅れる可能性があります。 
* 十分なデバッグは行っていないので予期せぬ動作を起こす可能性があります。
* Altとの組み合わせ(例えばAlt + F4)が動作しないことを確認しています。


## その他
以下の単一キー変換はレジストリの設定をいじっています。
* CapsLock → F13
* かな → Shift
* Fn1/Fn2は無視してください