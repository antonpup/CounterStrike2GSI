Counter-Strike 2 GSI (Game State Integration)
=============================================

[![Counter-Strike 2](https://img.shields.io/badge/CS2-243779?style=flat&label=Game&labelColor=fbab1d)](https://store.steampowered.com/app/730/)
[![.NET](https://github.com/antonpup/CounterStrike2GSI/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/antonpup/CounterStrike2GSI/actions/workflows/dotnet.yml)
[![GitHub Release](https://img.shields.io/github/v/release/antonpup/CounterStrike2GSI)](https://github.com/antonpup/CounterStrike2GSI/releases/latest)
[![NuGet Version](https://img.shields.io/nuget/v/CounterStrike2GSI)](https://www.nuget.org/packages/CounterStrike2GSI)
[![NuGet Downloads](https://img.shields.io/nuget/dt/CounterStrike2GSI?label=nuget%20downloads)](https://www.nuget.org/packages/CounterStrike2GSI)

A C# library to interface with the Game State Integration found in Counter-Strike 2.

## About Counter-Strike 2 GSI

This library provides an easy way to implement Game State Integration from Counter-Strike 2 into C# applications through exposing a number of events.

Underneath the hood, once the library is started, it continuously listens for HTTP POST requests made by the game on a specific address and port. When a request is received, the JSON data is parsed into [GameState object](#game-state-structure) and is offered to your C# application through the `NewGameState` event. The library also subscribes to `NewGameState` to determine more granular changes to raise more specific events _(such as `BombStateUpdated`, `PlayerWeaponsPickedUp`, or `RoundConcluded` to name a few)_. A full list of exposed Game Events can be found in the [Implemented Game Events](#implemented-game-events) section.

## About Game State Integration

Game State Integration is Valve's implementation for exposing current game state _(such as player health, mana, ammo, etc.)_ and game events without the need to read game memory or risking anti-cheat detection. The information exposed by GSI is limited to what Valve has determined to expose. For example, the game can expose information about all players in the game while spectating a match, but will only expose local player's information when playing a game. While the information is limited, there is enough information to create a live game analysis tool, create custom RGB lighting effects, or create a live streaming plugin to show additional game information. For example, GSI can be seen used during competitive tournament live streams to show currently spectated player's information and in-game statistics.

You can read about Game State Integration for Counter-Strike: Global Offensive [here](https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration).

## Installation

Install from [nuget](https://www.nuget.org/packages/CounterStrike2GSI).

## Building Counter-Strike 2 GSI
### On Windows
1. Make sure you have Visual Studio installed with `.NET desktop development` workload and `.Net 8.0 Runtime` individual component.
2. Make sure you have CMake 3.26 or later installed from [https://cmake.org/](https://cmake.org/).
3. In the repository root directory run: `cmake -B build/ .` to generate the project solution file.
4. Open the project solution located in `build/CounterStrike2GSI.sln`.
### On MACOS
1. Install the .NET SDK. If it is not installed, download and install it from [https://dotnet.microsoft.com/download].
2. In the repository root directory run: 
```
# Navigate to the CounterStrike2GSI directory
cd CounterStrike2GSI

# Build the main project
dotnet build

# Below is optional
# Navigate to the Example Program directory
cd "CounterStrike2GSI Example Program"

# Build the macOS-specific solution file
dotnet build "CounterStrike2GSI Example Program_macos.sln"

# Run the example program
cd "CounterStrike2GSI Example Program"
dotnet run
```

## How to use

1. After installing the [CounterStrike2GSI nuget package](https://www.nuget.org/packages/CounterStrike2GSI) in your project, create an instance of `GameStateListener`, providing a custom port or custom URI.
```C#
GameStateListener gsl = new GameStateListener(3000); //http://localhost:3000/
```
or
```C#
GameStateListener gsl = new GameStateListener("http://127.0.0.1:1234/");
```
> **Please note**: If your application needs to listen to a URI other than `http://localhost:*/` (for example `http://192.168.0.2:100/`), you will need to run your application with administrator privileges.

2. Create a Game State Integration configuration file. This can either be done manually by creating a file `<PATH TO GAME DIRECTORY>/game/csgo/cfg/gamestate_integration_<CUSTOM NAME>.cfg` where `<CUSTOM NAME>` should be the name of your application (it can be anything). Or you can use the built-in function `GenerateGSIConfigFile()` to automatically locate the game directory and generate the file. The function will automatically take into consideration the URI specified when a `GameStateListener` instance was created in the previous step.
```C#
if (!gsl.GenerateGSIConfigFile("Example"))
{
    Console.WriteLine("Could not generate GSI configuration file.");
}
```
The function `GenerateGSIConfigFile` takes a string `name` as the parameter. This is the `<CUSTOM NAME>` mentioned earlier. The function will also return `True` when file generation was successful, and `False` otherwise. The resulting file from the above code should look like this:
```
"Example Integration Configuration"
{
    "uri"          "http://localhost:3000/"
    "timeout"      "5.0"
    "buffer"       "0.1"
    "throttle"     "0.1"
    "heartbeat"    "10.0"
    "data"
    {
        "provider"                  "1"
        "tournamentdraft"           "1"
        "map"                       "1"
        "map_round_wins"            "1"
        "round"                     "1"
        "player_id"                 "1"
        "player_state"              "1"
        "player_weapons"            "1"
        "player_match_stats"        "1"
        "player_position"           "1"
        "phase_countdowns"          "1"
        "allplayers_id"             "1"
        "allplayers_state"          "1"
        "allplayers_match_stats"    "1"
        "allplayers_weapons"        "1"
        "allplayers_position"       "1"
        "allgrenades"               "1"
        "bomb"                      "1"
    }
}
```

3. Create handlers and subscribe for events your application will be using. (A full list of exposed Game Events can be found in the [Implemented Game Events](#implemented-game-events) section.)
If your application just needs `GameState` information, this is done by subscribing to `NewGameState` event and creating a handler for it:
```C#
...
gsl.NewGameState += OnNewGameState;
...

void OnNewGameState(GameState gs)
{
    // Read information from the game state.
}
```
If you would like to utilize `Game Events` in your application, this is done by subscribing to an event from the [Implemented Game Events](#implemented-game-events) list and creating a handler for it:
```C#
...
gsl.GameEvent += OnGameEvent; // Will fire on every GameEvent
gsl.BombStateUpdated += OnBombStateUpdated; // Will only fire on BombStateUpdated events.
gsl.PlayerWeaponsPickedUp += OnPlayerWeaponsPickedUp; // Will only fire on PlayerWeaponsPickedUp events.
gsl.RoundConcluded += OnRoundConcluded; // Will only fire on RoundConcluded events.
...

void OnGameEvent(CS2GameEvent game_event)
{
    // Read information from the game event.
    
    if (game_event is PlayerTookDamage player_took_damage)
    {
        Console.WriteLine($"The player {player_took_damage.Player.Name} took {player_took_damage.Previous - player_took_damage.New} damage!");
    }
    else if (game_event is PlayerActiveWeaponChanged active_weapon_changed)
    {
        Console.WriteLine($"The player {active_weapon_changed.Player.Name} changed their active weapon to {active_weapon_changed.New.Name} from {active_weapon_changed.Previous.Name}.");
    }
}

void OnBombStateUpdated(BombStateUpdated game_event)
{
    Console.WriteLine($"The bomb is now {game_event.New}.");
}

void OnPlayerWeaponsPickedUp(PlayerWeaponsPickedUp game_event)
{
    Console.WriteLine($"The player {game_event.Player.Name} picked up the following weapons:");
    foreach (var weapon in game_event.Weapons)
    {
        Console.WriteLine($"\t{weapon.Name}");
    }
}

void OnRoundConcluded(RoundConcluded game_event)
{
    Console.WriteLine($"Round {game_event.Round} concluded by {game_event.WinningTeam} for reason: {game_event.RoundConclusionReason}");
}
```
Both `NewGameState` and `Game Events` can be used alongside one another. The `Game Events` are generated based on the `GameState`, and are there to provide ease of use.

4. Finally you want to start the `GameStateListener` to begin capturing HTTP POST requests. This is done by calling the `Start()` method of `GameStateListener`. The method will return `True` if started successfully, or `False` when failed to start. Often the failure to start is due to insufficient permissions or another application is already using the same port.
```C#
if (!gsl.Start())
{
    // GameStateListener could not start.
}
// GameStateListener started and is listening for Game State requests.
```

## Implemented Game Events

* `GameEvent` The base game event, will fire for all other listed events.

### AllGrenades Events

* `AllGrenadesUpdated`
* `GrenadeUpdated`
* `NewGrenade`
* `ExpiredGrenade`

### AllPlayers Events

* `AllPlayersUpdated`
* `PlayerConnected`
* `PlayerDisconnected`

### Auth Events

* `AuthUpdated`

### Bomb Events

* `BombUpdated`
* `BombPlanting`
* `BombPlanted`
* `BombDefused`
* `BombDefusing`
* `BombDropped`
* `BombPickedup`
* `BombExploded`

### Killfeed Events

* `KillFeed`

### Map Events

* `MapUpdated`
* `GamemodeChanged`
* `TeamStatisticsUpdated`
* `TeamScoreChanged`
* `TeamRemainingTimeoutsChanged`
* `RoundChanged`
* `RoundConcluded`
* `RoundStarted`
* `LevelChanged`
* `MapPhaseChanged`
* `WarmupStarted`
* `WarmupOver`
* `IntermissionStarted`
* `IntermissionOver`
* `FreezetimeStarted`
* `FreezetimeOver`
* `PauseStarted`
* `PauseOver`
* `TimeoutStarted`
* `TimeoutOver`
* `MatchStarted`
* `Gameover`

### PhaseCountdown Events

* `PhaseCountdownsUpdated`

### Player Events

* `PlayerUpdated`
* `PlayerTeamChanged`
* `PlayerActivityChanged`
* `PlayerStateChanged`
* `PlayerHealthChanged`
* `PlayerDied`
* `PlayerRespawned`
* `PlayerTookDamage`
* `PlayerArmorChanged`
* `PlayerHelmetChanged`
* `PlayerFlashAmountChanged`
* `PlayerSmokedAmountChanged`
* `PlayerBurningAmountChanged`
* `PlayerMoneyAmountChanged`
* `PlayerRoundKillsChanged`
* `PlayerRoundHeadshotKillsChanged`
* `PlayerRoundTotalDamageChanged`
* `PlayerEquipmentValueChanged`
* `PlayerDefusekitChanged`
* `PlayerWeaponChanged`
* `PlayerActiveWeaponChanged`
* `PlayerWeaponsPickedUp`
* `PlayerStatsChanged`
* `PlayerKillsChanged`
* `PlayerGotKill`
* `PlayerAssistsChanged`
* `PlayerDeathsChanged`
* `PlayerMVPsChanged`
* `PlayerScoreChanged`

### Provider Events

* `ProviderUpdated`

### Round Events
* `RoundUpdated`
* `RoundPhaseUpdated`
* `BombStateUpdated`
* `TeamRoundVictory`
* `TeamRoundLoss`

## Game State Structure

```
GameState
+-- Auth
|   +-- ...
+-- Provider
|   +-- Name
|   +-- AppID
|   +-- Version
|   +-- SteamID
+-- Map
|   +-- Mode
|   +-- Name
|   +-- Phase
|   +-- Round
|   +-- CTStatistics
|   |   +-- Score
|   |   +-- Name
|   |   +-- Flag
|   |   +-- ConsecutiveRoundLosses
|   |   +-- RemainingTimeouts
|   |   +-- MatchesWonThisSeries
|   +-- TStatistics
|   |   +-- ...
|   +-- NumberOfMatchesToWinSeries
|   +-- RoundWins
+-- Round
|   +-- Phase
|   +-- BombState
|   +-- WinningTeam
+-- Player
|   +-- SteamID
|   +-- Name
|   +-- Clan
|   +-- ObserverSlot
|   +-- Team
|   +-- Activity
|   +-- State
|   |   +-- Health
|   |   +-- Armor
|   |   +-- HasHelmet
|   |   +-- FlashAmount
|   |   +-- SmokedAmount
|   |   +-- BurningAmount
|   |   +-- Money
|   |   +-- RoundKills
|   |   +-- RoundHSKills
|   |   +-- RoundTotalDamage
|   |   +-- EquipmentValue
|   |   +-- HasDefuseKit
|   +-- Weapons[]
|   |   +-- Name
|   |   +-- PaintKit
|   |   +-- Type
|   |   +-- AmmoClip
|   |   +-- AmmoClipMax
|   |   +-- AmmoReserve
|   |   +-- State
|   +-- MatchStats
|   |   +-- Kills
|   |   +-- Assists
|   |   +-- Deaths
|   |   +-- MVPs
|   |   +-- Score
|   +-- SpectationTarget
|   +-- Position
|   +-- ForwardDirection
|   +-- GetActiveWeapon()
+-- PhaseCountdowns
|   +-- Phase
|   +-- PhaseEndTime
+-- AllPlayers
|   \
|   (Map of player ID to Player structure)
|   ...
+-- AllGrenades
|   \
|   (Map of grenade ID to Grenade structure)
|   +-- Owner
|   +-- Position
|   +-- Velocity
|   +-- Lifetime
|   +-- Type
|   +-- Flames
|   +-- EffectTime
+-- Bomb
|   +-- State
|   +-- Position
|   +-- Player
|   +-- Countdown
+-- TournamentDraft
|   +-- State
|   +-- EventID
|   +-- StageID
|   +-- FirstTeamID
|   +-- SecondTeamID
|   +-- Event
|   +-- Stage
|   +-- FirstTeamName
|   +-- SecondTeamName
+-- Previously (Previous information from Game State)
```

## Null value handling

In the event that the game state is missing information, a default value will be returned:

| Type     | Default value  |
|:---------|:---------------|
| `bool`   | `False`        |
| `int`    | `-1`           |
| `long`   | `-1`           |
| `float`  | `-1`           |
| `string` | `String.Empty` |
| `enum`   | `Undefined`    |

## Weapon names

A full list of weapon names can be found [here](https://counterstrike.fandom.com/wiki/Developer_console#Available_entities).