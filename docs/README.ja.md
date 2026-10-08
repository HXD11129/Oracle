<p align="center">
  <img src="../Oracle/Data/plugin-icon.png" alt="Oracle アイコン" width="128" height="128">
</p>

<h1 align="center">Oracle</h1>

<p align="center">コンテンツ中に、使うスキルとタイミングを表示します。</p>

<p align="center">
  <a href="../README.md">English</a>
</p>

<p align="center">
  <a href="https://github.com/exatrines/Oracle/releases/latest">
    <img src="https://img.shields.io/github/v/release/exatrines/Oracle?style=for-the-badge&label=Release" alt="Release">
  </a>
  <a href="../CHANGELOG.md">
    <img src="https://img.shields.io/badge/Changelog-Keep%20a%20Changelog-informational?style=for-the-badge" alt="Changelog">
  </a>
  <a href="../LICENSE">
    <img src="https://img.shields.io/badge/License-AGPL--3.0--or--later-blue?style=for-the-badge" alt="AGPL-3.0-or-later">
  </a>
</p>

<p align="center">
  <img src="screenshots/major-hotbar-highlight-730x380.png" alt="Major オーバーレイとホットバーのアイコンハイライト">
</p>

Oracle は、コンテンツ中に「どのスキルを、いつ使うか」を表示する Dalamud プラグインです。

タイムラインは **FFLogs** のレポートから取り込むほか、選んだコンテンツで自分の行動を記録する **AutoRecord** からも作れます。ゾーンとジョブが合うと、カウントダウンまたは戦闘開始で時計が動き、これから使うアクションをオーバーレイに出します。ホットバーのアイコンを光らせることもできます。任意で、ボスの DataID プリセットによる Auto Load も使えます。

## インストール

1. `/xlsettings` を実行し、**試験的機能**タブを開く
2. **カスタムプラグインリポジトリ** に次の URL を追加する:

```
https://raw.githubusercontent.com/exatrines/DalamudPlugins/refs/heads/main/pluginmaster.json
```

3. `/xlplugins` を実行し、**Oracle** をインストールする

## 機能

- **タイムライン** — どのスキルをいつ使うかを設定できます
- **FFLogs 取り込み** — レポートから自分のスキルを読み込めます
- **AutoRecord** — 戦闘を記録してタイムラインにできます
- **Auto Load** — コンテンツ開始時に対応するタイムラインを読み込みます
- **オーバーレイ** — これから使うスキルをリストやスクロールするアイコンで確認できます
- **ホットバーハイライト** — 使うタイミングに合わせてホットバーのスキルを点灯させます

## コマンド

| コマンド | 説明 |
| --- | --- |
| `/oracle` | タイムライン設定の表示切替 |
| `/oracle config` | プラグイン設定の表示切替 |
| `/oracle overlay timeline` | タイムラインオーバーレイの表示切替 |
| `/oracle overlay major` | Major オーバーレイの表示切替 |
| `/oracle overlay icon` | アイコンハイライトの表示切替 |
| `/oracle autorecord` | AutoRecord の有効切替 |
| `/oracle load <name>` | タイムラインを読み込む |
| `/oracle unload` | タイムラインを外す |
| `/oracle preview start [sec]` | プレビューのカウントダウンを開始（省略時は 21 秒） |
| `/oracle preview pause` | プレビューの一時停止／再開 |
| `/oracle preview stop` | プレビューを停止 |

## 開発者向け

1. `git submodule update --init --recursive`
2. ビルド: `dotnet build Oracle.sln -c Release -p:Platform=x64`
3. Dalamud の **dev plugin** に `Oracle/bin/Release/` を指定する
4. プラグインインストーラ（dev）で **Oracle** を有効にする

共有 UI キットの [MirageUI](https://github.com/exatrines/MirageUI) を git サブモジュールとして同梱しています。

## コントリビューション

コントリビューションは大歓迎です！[貢献ガイド](../CONTRIBUTING.md)をご覧ください。
