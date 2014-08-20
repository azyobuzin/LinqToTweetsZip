# LINQ to tweets.zip #
Twitter の全ツイートダウンロードできるやつがあるじゃろ？それをこうして、こうして、こうじゃ。

# 動作環境 #
.NET Framework 4.5

ほかは面倒臭くなった。

# 特徴 #
- 読み込みを極力遅延させることで最低限の I/O を実現
- zip ファイル、展開後のディレクトリ、どっちでも使える

# 使い方 #
## zip ファイルから読み込む ##
```csharp
using (var source = TweetsZipFileSource(@"C:\tweets.zip"))
{
    var tweetsZip = new TweetsZip(source);
}
```

## ディレクトリから読み込む ##
```csharp
var source = new TweetsZipDirectorySource(@"C:\tweets");
var tweetsZip = new TweetsZip(source);
```

## 月ごとに処理する ##
`TweetsZip.Months` は月ごとのツイートのリストになっています。ツイートの順番は tweets.zip に含まれる JavaScript ファイルのそのままです。たぶん新しい順。

```csharp
// 2014年8月の「寝る」が含まれるツイートを取得
var tweets = tweetsZip.Months
	.First(m => m.Year == 2014 && m.Month == 8)
	.Where(t => t.Text.Contains("寝る"));
// この例では2014年8月のツイートのみを読み込みます
```

## 全ツイートを処理する ##
`TweetsZip.Tweets` はすべてのツイートを処理するための `IQueryable<Tweet>` です。順番は `Months` の並び順になっています。たぶん全部新しい順になってるはず。

全ツイートの処理はできるだけ読み込みを減らすために日付処理を加工しています。
`CreatedAt.Year` と `CreatedAt.Month` へのアクセスは実際の値を取得せず、月ごとの情報から取得してきます。また、 `CreatedAt` と他の `DateTimeOffset` を比較する場合も、年と月を先に判断するようにしています。これらを活用することで読み込みを極力減らし、高速にクエリを実行できます。

```csharp
// 2014/6/1 から 7/20 までの「暑い」を含むツイートを取得
var tweets = tweetsZip.Tweets
	.Where(t =>
		new DateTimeOffset(2014, 6, 1, 0, 0, 0, TimeSpan.Zero) <= t.CreatedAt
		&& t.CreatedAt < new DateTimeOffset(2014, 7, 21, 0, 0, 0, TimeSpan.Zero)
		&& t.Text.Contains("暑い")
	);
// この例では2014年6月と7月のツイートのみを読み込みます
```
