# CounterStrike2GSI Callback Guide

Source: `Server/CounterStrike2GSI.xml` and the event-handling logic in the matching source code. This document translates the English XML comments and adds the actual triggering conditions inferred from the library.

## Part 1: Callbacks grouped by data perspective

### 1. Primarily dependent on global spectator / GOTV / broadcast data

These callbacks need CS2 to send global data. In a normal first-person match, the requested nodes may be missing even when they are enabled in the CFG, so these callbacks may not fire.

| Data source | Callbacks | Why |
|---|---|---|
| `allplayers` | `AllPlayersUpdated`, `PlayerConnected`, `PlayerDisconnected` | They operate on the complete player collection. |
| `allgrenades` | `AllGrenadesUpdated`, `NewGrenade`, `GrenadeUpdated`, `ExpiredGrenade` | They operate on every active grenade. `NewGrenade` does not mean “the local player pressed the throw key.” |
| Killer and victim state | `KillFeed` | The library must receive both sides of a kill to assemble the feed entry. |
| Top-level `bomb` data and complete actor information | `BombUpdated`, `BombPickedup`, `BombPlanting`, `BombDefusing` | They require the top-level `bomb` node; the last three also require the library to resolve the acting player. |

### 2. Primarily triggered from the current `player` perspective

The following `Player*` callbacks are based on the GSI `player` node. While alive, that node normally represents the local player. After death, it may represent the current spectated player. To handle only the local player, compare `gameEvent.Player.SteamID` with a SteamID captured once from `Provider.SteamID`.

| Category | Callbacks |
|---|---|
| Overall player state | `PlayerUpdated`, `PlayerTeamChanged`, `PlayerActivityChanged`, `PlayerStateChanged` |
| Health and state | `PlayerHealthChanged`, `PlayerTookDamage`, `PlayerDied`, `PlayerRespawned`, `PlayerArmorChanged`, `PlayerHelmetChanged`, `PlayerFlashAmountChanged`, `PlayerSmokedAmountChanged`, `PlayerBurningAmountChanged`, `PlayerMoneyAmountChanged`, `PlayerRoundKillsChanged`, `PlayerRoundHeadshotKillsChanged`, `PlayerRoundTotalDamageChanged`, `PlayerEquipmentValueChanged`, `PlayerDefusekitChanged` |
| Weapons and personal match stats | `PlayerWeaponChanged`, `PlayerActiveWeaponChanged`, `PlayerWeaponsPickedUp`, `PlayerWeaponsDropped`, `PlayerStatsChanged`, `PlayerKillsChanged`, `PlayerGotKill`, `PlayerAssistsChanged`, `PlayerDeathsChanged`, `PlayerMVPsChanged`, `PlayerScoreChanged` |

When global spectator data is available, the library may also use `allplayers` updates to emit some `Player*` callbacks for other players. Do not assume a `Player*` callback always belongs to the local player.

### 3. Global match state: normally independent of spectating

These callbacks concern map, round, score, countdown, and final bomb states. They normally work from a player perspective as well, provided that CS2 sends the relevant GSI nodes.

`NewGameState`, `AuthUpdated`, `ProviderUpdated`, `MapUpdated`, `RoundUpdated`, `PhaseCountdownsUpdated`, `BombStateUpdated`, `BombPlanted`, `BombDefused`, `BombDropped`, `BombExploded`, `GamemodeChanged`, `TeamStatisticsUpdated`, `TeamScoreChanged`, `TeamRemainingTimeoutsChanged`, `RoundChanged`, `RoundConcluded`, `RoundStarted`, `LevelChanged`, `MapPhaseChanged`, `WarmupStarted`, `WarmupOver`, `IntermissionStarted`, `IntermissionOver`, `FreezetimeStarted`, `FreezetimeOver`, `PauseStarted`, `PauseOver`, `TimeoutStarted`, `TimeoutOver`, `MatchStarted`, `Gameover`, `RoundPhaseUpdated`, `TeamRoundVictory`, `TeamRoundLoss`.

### 4. Common callback fields

| Event data type | Fields |
|---|---|
| Most `...Changed` / `...Updated` events | `New` (new value), `Previous` (old value). |
| Player field-change events | Also contain `Player`. |
| Team field-change events | Also contain `Team` (`CT` or `T`). |
| Global grenade events | `EntityID`; creation/removal events use `Value`, update events use `New` and `Previous`. |

---

## Part 2: Trigger conditions and returned data, grouped by prefix

### New / Auth / Provider callbacks

| Callback | Trigger condition | Returned data |
|---|---|---|
| `NewGameState` | A GSI JSON payload is received, parsed successfully, and differs from the prior state. Movement, timers, health, and many other updates can cause it. | `GameState`: the complete current state. |
| `AuthUpdated` | The `auth` node changes. | `New`, `Previous`: authentication objects. |
| `ProviderUpdated` | The `provider` node changes, such as timestamp or SteamID information. | `New`, `Previous`: `Provider`; `New.SteamID` is the reliable local-account identifier. |

### All / Grenade callbacks (global spectator data)

| Callback | Trigger condition | Returned data |
|---|---|---|
| `AllPlayersUpdated` | The `allplayers` collection changes. | `New`, `Previous`: complete player dictionaries. |
| `PlayerConnected` | A SteamID appears in `allplayers` that was not present before. | `Value`: that player. This is not a low-level Steam connection event. |
| `PlayerDisconnected` | A previously present SteamID disappears from `allplayers`. | `Value`: that player. |
| `AllGrenadesUpdated` | The `allgrenades` collection changes. | `New`, `Previous`: complete grenade dictionaries. |
| `NewGrenade` | A new entity ID appears in `allgrenades`. | `Value`: grenade; `EntityID`: entity ID. |
| `GrenadeUpdated` | State or position of an existing grenade entity changes. | `New`, `Previous`: grenade; `EntityID`. |
| `ExpiredGrenade` | An existing grenade entity disappears from `allgrenades`. | `Value`: expired grenade; `EntityID`. |

### Kill callbacks (primarily global spectator data)

| Callback | Trigger condition | Returned data |
|---|---|---|
| `KillFeed` | The library first receives `PlayerGotKill` (killer, weapon, headshot flag) and `PlayerDied` (victim). When both are valid and not the same SteamID, it emits a feed entry. A round change clears incomplete data. | `Killer`, `Victim`, `Weapon`, `IsHeadshot`. |

This is not a raw CS2 global kill feed. A normal player perspective usually cannot provide both killer and victim data, so this callback is generally unavailable or unreliable outside global spectator data.

### Bomb callbacks

| Callback | Trigger condition | Returned data |
|---|---|---|
| `BombUpdated` | Any top-level `bomb` field changes: state, position, carrier, or countdown. It does not fire when the top-level node is absent. | `New`, `Previous`: `Bomb`. |
| `BombStateUpdated` | `round.bomb` or `bomb.state` changes. Either source can emit it. | `New`, `Previous`: `BombState`. |
| `BombPlanting` | New bomb state is `Planting` and the library can find `bomb.player` in its player cache. It mainly needs complete spectator data. | `Player`: player planting the bomb. |
| `BombPlanted` | New bomb state is `Planted`. This is the most reliable “plant complete” signal in a normal player perspective. | No additional fields. |
| `BombDefusing` | New bomb state is `Defusing` and the library can find `bomb.player`. It mainly needs complete spectator data. | `Player`: player defusing the bomb. |
| `BombDefused` | New bomb state is `Defused`. This is the most reliable “defuse complete” signal in a normal player perspective. | No additional fields. |
| `BombDropped` | New bomb state is `Dropped`. It can be derived from either bomb-state source. | No additional fields. |
| `BombPickedup` | New top-level bomb state is `Carried` and the library can resolve the carrier. | `Player`: C4 carrier. |
| `BombExploded` | New bomb state is `Exploded`. | No additional fields. |

In a normal player perspective, common observed transitions are `Undefined → Planted` and `Planted → Defused`. Those emit `BombStateUpdated` and the completion events, but not `BombPlanting` / `BombDefusing`. If the final state arrives through `round.bomb` instead of top-level `bomb`, `BombUpdated` may not fire.

### Map / Game / Team callbacks

| Callback | Trigger condition | Returned data |
|---|---|---|
| `MapUpdated` | Any field in `map` changes. | `New`, `Previous`: `Map`. |
| `GamemodeChanged` | `map.mode` changes. | `New`, `Previous`: game mode. |
| `LevelChanged` | `map.name` changes, for example after loading a different map. | `New`, `Previous`: map name. |
| `MapPhaseChanged` | `map.phase` changes. | `New`, `Previous`: match phase. |
| `TeamStatisticsUpdated` | Any field in CT or T team statistics changes. | `New`, `Previous`: team statistics; `Team`. |
| `TeamScoreChanged` | A team's `score` changes, normally after a round win. | `New`, `Previous`: score; `Team`. |
| `TeamRemainingTimeoutsChanged` | A team's `remaining_timeouts` changes, normally after calling a timeout. | `New`, `Previous`: remaining timeouts; `Team`. |
| `WarmupStarted` / `WarmupOver` | `map.phase` enters / leaves `Warmup`. | No additional fields. |
| `IntermissionStarted` / `IntermissionOver` | `map.phase` enters / leaves `Intermission`. | No additional fields. |
| `FreezetimeStarted` / `FreezetimeOver` | `map.phase` enters / leaves `Freezetime`. | No additional fields. |
| `PauseStarted` / `PauseOver` | `map.phase` enters / leaves `Paused`. | No additional fields. |
| `TimeoutStarted` / `TimeoutOver` | `map.phase` enters / leaves `Timeout_T` or `Timeout_CT`. | `Team`: team that called the timeout. |
| `MatchStarted` | `map.phase` enters `Live`; match start or resume. | No additional fields. |
| `Gameover` | `map.phase` enters `Gameover`. | No additional fields. |

### Round / Phase callbacks

| Callback | Trigger condition | Returned data |
|---|---|---|
| `RoundUpdated` | Any field in `round` changes. | `New`, `Previous`: `Round`. |
| `RoundChanged` | `map.round` changes. | `New`, `Previous`: round number. |
| `RoundStarted` | `map.round` increases. | `Round`, `IsFirstRound`, `IsLastRound`. |
| `RoundConcluded` | `map.round` increases and `map.round_wins` contains a conclusion for the round that just ended. It usually arrives when entering the next round. | `Round`, `WinningTeam`, `RoundConclusionReason`, `IsFirstRound`, `IsLastRound`. |
| `RoundPhaseUpdated` | `round.phase` or `phase_countdowns.phase` changes. Both sources can cause duplicates. | `New`, `Previous`: phase. |
| `PhaseCountdownsUpdated` | Any field in `phase_countdowns` changes; high frequency during countdowns. | `New`, `Previous`: countdown object. |
| `TeamRoundVictory` | `round.win_team` changes to CT or T. | `Team`: winner; `Value`: current round number. |
| `TeamRoundLoss` | Derived alongside `TeamRoundVictory`; its team is the opposite side. | `Team`: loser; `Value`: current round number. |

`RoundConcluded.RoundConclusionReason`: `T_Win_Elimination` (T elimination), `T_Win_Bomb` (C4 explosion), `T_Win_Time` (T time win), `CT_Win_Elimination` (CT elimination), `CT_Win_Defuse` (CT defuse), `CT_Win_Rescue` (CT hostage rescue), and `CT_Win_Time` (CT time win).

### Player callbacks: overall state, health, and state values

| Callback | Trigger condition | Returned data |
|---|---|---|
| `PlayerUpdated` | The overall `player` node changes. With global `allplayers` data, it may also represent any player. | `New`, `Previous`: `Player`; `PlayerID`. |
| `PlayerTeamChanged` | `player.team` changes. | `New`, `Previous`: team; `Player`. |
| `PlayerActivityChanged` | `player.activity` changes. | `New`, `Previous`: activity; `Player`. |
| `PlayerStateChanged` | The overall `player.state` changes. | `New`, `Previous`: `PlayerState`; `Player`. |
| `PlayerHealthChanged` | `player.state.health` changes. | `New`, `Previous`: health; `Player`. |
| `PlayerTookDamage` | Health decreases. | `New`, `Previous`: health; `Player`. |
| `PlayerDied` | New health equals `0`. It does not include killer information. | `New`, `Previous`: health; `Player` (victim). |
| `PlayerRespawned` | Previous health is `0` and new health is greater than `0`. | `New`, `Previous`: health; `Player`. |
| `PlayerArmorChanged` | Armor changes. | `New`, `Previous`: armor; `Player`. |
| `PlayerHelmetChanged` | Helmet possession changes. | `New`, `Previous`: Boolean; `Player`. |
| `PlayerFlashAmountChanged` | Flash effect amount changes. | `New`, `Previous`: XML documents an integer range of `0–255`, but current CS2 may report only `0 → 1 → 0`; treat any non-zero value as flashed and rely on raw JSON for the real resolution; `Player`. |
| `PlayerSmokedAmountChanged` | Smoke effect amount changes. | `New`, `Previous`: XML documents `0–255`; `0` is unaffected and larger values mean stronger effect. Verify against raw JSON; `Player`. |
| `PlayerBurningAmountChanged` | Burning effect amount changes. | `New`, `Previous`: XML documents `0–255`; `0` is not burning and larger values mean stronger effect. Verify against raw JSON; `Player`. |
| `PlayerMoneyAmountChanged` | Money changes. | `New`, `Previous`: money; `Player`. |
| `PlayerRoundKillsChanged` | Round kill count changes. | `New`, `Previous`: round kills; `Player`. |
| `PlayerRoundHeadshotKillsChanged` | Round headshot kill count changes. | `New`, `Previous`: round headshot kills; `Player`. |
| `PlayerRoundTotalDamageChanged` | Round total damage changes. | `New`, `Previous`: damage; `Player`. |
| `PlayerEquipmentValueChanged` | Equipment value changes. | `New`, `Previous`: equipment value; `Player`. |
| `PlayerDefusekitChanged` | Defuse-kit possession changes. | `New`, `Previous`: Boolean; `Player`. |

### Weapon callbacks

| Callback | Trigger condition | Returned data |
|---|---|---|
| `PlayerWeaponChanged` | The weapon collection changes and the library identifies a changed weapon while the player is alive. The library's name-matching condition is strict, so this may fire less often than its name suggests. | `New`, `Previous`: `Weapon`; `Player`. |
| `PlayerActiveWeaponChanged` | The weapon collection changes, active weapon name differs from the previous one, and the player is alive. | `New`, `Previous`: active weapon; `Player`. |
| `PlayerWeaponsPickedUp` | One or more non-knife, non-fists weapons are newly present. | `Weapons`: picked-up weapon list; `Player`. |
| `PlayerWeaponsDropped` | One or more non-knife, non-fists weapons disappear. | `Weapons`: lost weapon list; `Player`. |

### Player match-stat and kill callbacks

| Callback | Trigger condition | Returned data |
|---|---|---|
| `PlayerStatsChanged` | The overall `player.match_stats` object changes. | `New`, `Previous`: `MatchStats`; `Player`. |
| `PlayerKillsChanged` | Total kills change. | `New`, `Previous`: kills; `Player`. |
| `PlayerGotKill` | Round kill count increases and its previous value is not `-1`. | `Player` (killer), `Weapon`, `IsHeadshot`, `IsAce`. |
| `PlayerAssistsChanged` | Assists change. | `New`, `Previous`: assists; `Player`. |
| `PlayerDeathsChanged` | Total deaths change. | `New`, `Previous`: deaths; `Player`. |
| `PlayerMVPsChanged` | MVP count changes. | `New`, `Previous`: MVPs; `Player`. |
| `PlayerScoreChanged` | Score changes. | `New`, `Previous`: score; `Player`. |

## Practical recommendations

- To learn why a round ended, subscribe to `RoundConcluded` and read `RoundConclusionReason`.
- To detect the local player's kills or deaths, use `PlayerGotKill` and `PlayerDied`, filtering with `Provider.SteamID`.
- To detect completed plants/defuses, use `BombPlanted` and `BombDefused`; do not rely on `BombPlanting` or `BombDefusing`.
- To test global events, use a global spectator view first and confirm that raw GSI JSON contains `allplayers` and `allgrenades`.
