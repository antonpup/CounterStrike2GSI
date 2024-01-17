[![.NET](https://github.com/antonpup/CounterStrike2GSI/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/antonpup/CounterStrike2GSI/actions/workflows/dotnet.yml) ![GitHub Release](https://img.shields.io/github/v/release/antonpup/CounterStrike2GSI) [![NuGet Version](https://img.shields.io/nuget/v/CounterStrike2GSI)](https://www.nuget.org/packages/CounterStrike2GSI) [GitHub top language](https://img.shields.io/github/languages/top/antonpup/CounterStrike2GSI)

# Counter-Strike 2 GSI (Game State Integration)
A C# library to interface with the Game State Integration found in Counter-Strike 2.

## What is Game State Integration
You can read about Game State Integration for Counter-Strike: Global Offensive [here](https://developer.valvesoftware.com/wiki/Counter-Strike:_Global_Offensive_Game_State_Integration).

## About Counter-Strike 2 GSI
This library provides easy means of implementing Game State Integration from Counter-Strike: 2 into C# applications. Library listens for HTTP POST requests made by the game on a specific address and port. Upon receiving a request, the game state is parsed and can be used.

After starting the `GameStateListener` instance, it will continuously listen for incoming HTTP requests. Upon a received request, the contents will be parsed into a `GameState` object.
Additionally this library exposes a number of events that can be subscribed to for ease of use.

## Installation
Install from [nuget](https://www.nuget.org/packages/CounterStrike2GSI).

## Build Counter-Strike 2 GSI
1. Make sure you have Visual Studio installed with `.NET desktop development` workload and `.Net 8.0 Runtime` individual component.
2. Make sure you have CMake 3.26 or later installed from [https://cmake.org/](https://cmake.org/).
3. In the repository root directory run: `cmake -B build/ .` to generate the project solution file.
4. Open the project solution located in `build/CounterStrike2GSI.sln`.

## Usage
1. Create a `GameStateListener` instance by providing a port or passing a specific URI:

```C#
GameStateListener gsl = new GameStateListener(4000); //http://localhost:4000/
GameStateListener gsl = new GameStateListener("http://127.0.0.1:1234/");
```

**Please note**: If your application needs to listen to a URI other than `http://localhost:*/` (for example `http://192.168.2.2:100/`), you need to ensure that it is run with administrator privileges.  
In this case, `http://127.0.0.1:*/` is **not** equivalent to `http://localhost:*/`.

2. Create a handler for GameState updates or GameEvent updates:

GameState updates:
```C#
void OnNewGameState(GameState game_state)
{
    // Do stuff with game_state
}
```

GameEvent updates:
```C#
void OnGameEvent(CS2GameEvent game_event)
{
    // Do stuff with game_event
    if (game_event is PlayerTookDamage player_took_damage)
    {
        Console.WriteLine($"The player {player_took_damage.PlayerID} took {player_took_damage.Previous - player_took_damage.New} damage!");
    }
}
```

Specific GameEvent updates:
```C#
void OnKillFeed(KillFeed game_event)
{
    // Do stuff with KillFeed game_event
    Console.WriteLine($"{game_event.Killer.Name} killed {game_event.Victim.Name} with {game_event.Weapon.Name}{(game_event.IsHeadshot ? " as a headshot." : ".")}");
}
```

3. Subscribe to the `NewGameState` event or GameEvent updates:

```C#
gsl.NewGameState += OnNewGameState; // Receive Game State updates
gsl.GameEvent += OnGameEvent; // Receive Game Event updates
gsl.KillFeed += OnKillFeed; // Receive KillFeed Game Events only
```

4. Use `GameStateListener.Start()` to start listening for HTTP POST requests from the game client. This method will return `false` if starting the listener fails (most likely due to insufficient privileges).

## Implemented Game Events
* AllGrenadesEvents
  * `AllGrenadesUpdated`
  * `GrenadeUpdated`
* AllPlayersEvents
  * `AllPlayersUpdated`
* AuthEvents
  * `AuthUpdated`
* BombEvents
  * `BombUpdated`
* KillfeedEvents
  * `KillFeed`
* MapEvents
  * `MapUpdated`
  * `TeamStatisticsUpdated`
  * `RoundChanged`
  * `RoundConcluded`
  * `RoundStarted`
* PhaseCountdownEvents
  * `PhaseCountdownsUpdated`
* PlayerEvents
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
* ProviderEvents
  * `ProviderUpdated`
* RoundEvents
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
|   +-- GameState
|   +-- IsPaused
|   +-- Winningteam
|   +-- CustomGameName
|   +-- RadiantWardPurchaseCooldown
|   +-- DireWardPurchaseCooldown
|   +-- RoshanState
|   +-- RoshanStateEndTime
|   +-- WardPurchaseCooldown
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
|   +-- Weapons
|   |   \
|   |   (Map of weapon slot to Weapon structure)
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
|   +-- GetActiveWeaponSlot()
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
In case the JSON did not contain the requested information, these values will be returned:

Type    |Default value
--------|-------------
bool    | false
int     | -1
long    | -1
float   | -1
string  | String.Empty

All Enums have a value `enum.Undefined` that serves the same purpose.


You will also need to create a custom `gamestate_integration_*.cfg` in `game/csgo/cfg/`, for example:  
`gamestate_integration_test.cfg`:  
```
"CS2 Integration v1.0"
{
    "uri" "http://localhost:4000"
    "timeout" "5.0"
    "buffer"  "0.1"
    "throttle" "0.5"
    "heartbeat" "10.0"
    "auth"
    {
        "key1" "AUTHKEY" 
    }
    "data"
    {
        "provider"               "1"
        "tournamentdraft"        "1"
        "map"                    "1"
        "map_round_wins"         "1"
        "round"                  "1"
        "player_id"              "1"
        "player_state"           "1"
        "player_weapons"         "1"
        "player_match_stats"     "1"
        "player_position"        "1"
        "phase_countdowns"       "1"
        "allplayers_id"          "1"
        "allplayers_state"       "1"
        "allplayers_match_stats" "1"
        "allplayers_weapons"     "1"
        "allplayers_position"    "1"
        "allgrenades"            "1"
        "bomb"                   "1"
    }
}
```

**Please note**: In order to run this test application without explicit administrator privileges, you need to use the URI `http://localhost:<port>` in this configuration file.