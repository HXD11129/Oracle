<p align="center">
  <img src="Oracle/Data/plugin-icon.png" alt="Oracle icon" width="128" height="128">
</p>

<h1 align="center">Oracle</h1>

<p align="center">
  English | <a href="docs/README.ja.md">日本語</a>
</p>

<p align="center">
  <a href="https://github.com/exatrines/Oracle/releases/latest">
    <img src="https://img.shields.io/github/v/release/exatrines/Oracle?label=Release&amp;labelColor=F4A7C5&amp;color=FFFFFF&amp;style=flat" alt="Release">
  </a>
  <a href="CHANGELOG.md">
    <img src="https://img.shields.io/badge/Changelog-view-FFFFFF?labelColor=F4A7C5&amp;style=flat" alt="Changelog">
  </a>
  <a href="LICENSE">
    <img src="https://img.shields.io/badge/License-AGPL--3.0--or--later-FFFFFF?labelColor=F4A7C5&amp;style=flat" alt="AGPL-3.0-or-later">
  </a>
</p>

<p align="center">
  <img src="docs/screenshots/oracle-overlays-combo-1280x720.png" alt="Major and Minor overlays">
</p>

Oracle is a Dalamud plugin that shows duty timeline cues—so you know which skill to use, and when.

A main way to build timelines is importing casts from **FFLogs**. You can also **record your own actions** in selected duties (AutoRecord) and turn that into a timeline. Match zone and job, optionally an Auto Load boss preset; when countdown or combat starts, Oracle runs a clock and surfaces upcoming actions on overlays, with optional hotbar icon highlights.

## Install

1. Run `/xlsettings` and open the **Experimental** tab
2. Add this URL under **Custom Plugin Repositories**:

```
https://raw.githubusercontent.com/exatrines/DalamudPlugins/refs/heads/main/pluginmaster.json
```

3. Run `/xlplugins` and install **Oracle**

## Features

- **Timeline** — Set up which skills to use and when
- **FFLogs import** — Import your skills from a report
- **AutoRecord** — Record a pull in-game and turn it into a timeline
- **Auto Load** — Load a matching timeline when the duty starts
- **Overlays** — See upcoming skills as a list or a scrolling row of icons
- **Hotbar highlight** — Light up the skill on your hotbar when it's time to use it

## Commands

| Command | Description |
| --- | --- |
| `/oracle` | Toggle timeline settings |
| `/oracle config` | Toggle plugin settings |
| `/oracle overlay timeline` | Toggle timeline overlay |
| `/oracle overlay major` | Toggle major overlay |
| `/oracle overlay icon` | Toggle icon highlight |
| `/oracle autorecord` | Toggle AutoRecord enabled |
| `/oracle load <name>` | Load a timeline |
| `/oracle unload` | Unload the timeline |
| `/oracle preview start [sec]` | Start preview countdown (default 21) |
| `/oracle preview pause` | Pause or resume preview |
| `/oracle preview stop` | Stop preview |

## For developers

1. `git submodule update --init --recursive`
2. Build: `dotnet build Oracle.sln -c Release -p:Platform=x64`
3. Point Dalamud’s **dev plugin** path at `Oracle/bin/Release/`
4. Enable **Oracle** in the plugin installer (dev)

[MirageUI](https://github.com/exatrines/MirageUI) is included as a git submodule for the shared UI kit.

## Contributing

Contributions are always welcome! Please see the [contribution guide](CONTRIBUTING.md).
