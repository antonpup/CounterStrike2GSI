# CounterStrike2GSI 回调中文说明

来源：`Server/CounterStrike2GSI.xml` 与同版本源码的事件处理逻辑。本文把 XML 中的英文事件/字段说明翻译成中文，并补充实际触发条件。

## 第一部分：回调按数据视角分类

### 1. 全局观战 / GOTV / 赛事数据主要依赖

这类回调需要 CS2 实际发送全局数据。普通第一人称中即使 CFG 请求了节点，也可能没有数据，因此不会触发。

| 数据来源 | 回调 | 说明 |
|---|---|---|
| `allplayers` | `AllPlayersUpdated`、`PlayerConnected`、`PlayerDisconnected` | 全场玩家集合的增减或变化。 |
| `allgrenades` | `AllGrenadesUpdated`、`NewGrenade`、`GrenadeUpdated`、`ExpiredGrenade` | 全场活动投掷物。`NewGrenade` 不等于“自己按下投掷键”。 |
| 凶手与死者的全局状态 | `KillFeed` | 库需要同时拿到击杀者和死者，普通视角通常无法凑齐。 |
| 顶层 `bomb` 与完整操作信息 | `BombUpdated`、`BombPickedup`、`BombPlanting`、`BombDefusing` | 需要 CS2 发送顶层 `bomb` 节点；后三者还需要库定位操作玩家。普通视角下经常缺失。 |

### 2. 当前玩家视角主要触发

以下 `Player*` 回调基于 GSI 的 `player` 节点。正常存活时通常代表自己；死亡后的观战阶段，`player` 可能变为当前观察对象。要只处理自己，统一比较 `gameEvent.Player.SteamID == mySteamId`，其中 `mySteamId` 应从 `Provider.SteamID` 初始化一次。

| 类别 | 回调 |
|---|---|
| 玩家总体 | `PlayerUpdated`、`PlayerTeamChanged`、`PlayerActivityChanged`、`PlayerStateChanged` |
| 生命与状态 | `PlayerHealthChanged`、`PlayerTookDamage`、`PlayerDied`、`PlayerRespawned`、`PlayerArmorChanged`、`PlayerHelmetChanged`、`PlayerFlashAmountChanged`、`PlayerSmokedAmountChanged`、`PlayerBurningAmountChanged`、`PlayerMoneyAmountChanged`、`PlayerRoundKillsChanged`、`PlayerRoundHeadshotKillsChanged`、`PlayerRoundTotalDamageChanged`、`PlayerEquipmentValueChanged`、`PlayerDefusekitChanged` |
| 武器与个人战绩 | `PlayerWeaponChanged`、`PlayerActiveWeaponChanged`、`PlayerWeaponsPickedUp`、`PlayerWeaponsDropped`、`PlayerStatsChanged`、`PlayerKillsChanged`、`PlayerGotKill`、`PlayerAssistsChanged`、`PlayerDeathsChanged`、`PlayerMVPsChanged`、`PlayerScoreChanged` |

> 全局观战中，如果 `allplayers` 可用，库也可能用其他玩家的更新触发部分 `Player*` 回调；不要仅凭回调名称假定它永远属于自己。

### 3. 全局比赛状态：通常不依赖观战

这些回调面向地图、回合、比分、倒计时和炸弹最终状态。普通玩家视角通常可用，但仍以前提是 GSI 实际发送对应 JSON 节点。

`NewGameState`、`AuthUpdated`、`ProviderUpdated`、`MapUpdated`、`RoundUpdated`、`PhaseCountdownsUpdated`、`BombStateUpdated`、`BombPlanted`、`BombDefused`、`BombDropped`、`BombExploded`、`GamemodeChanged`、`TeamStatisticsUpdated`、`TeamScoreChanged`、`TeamRemainingTimeoutsChanged`、`RoundChanged`、`RoundConcluded`、`RoundStarted`、`LevelChanged`、`MapPhaseChanged`、`WarmupStarted`、`WarmupOver`、`IntermissionStarted`、`IntermissionOver`、`FreezetimeStarted`、`FreezetimeOver`、`PauseStarted`、`PauseOver`、`TimeoutStarted`、`TimeoutOver`、`MatchStarted`、`Gameover`、`RoundPhaseUpdated`、`TeamRoundVictory`、`TeamRoundLoss`。

### 4. 通用字段

| 回调数据类型 | 可读取字段 |
|---|---|
| 大多数 `...Changed` / `...Updated` | `New`（新值）、`Previous`（旧值）。 |
| `Player...` 字段变化事件 | 还包含 `Player`。 |
| `Team...` 字段变化事件 | 还包含 `Team`（`CT` 或 `T`）。 |
| 全体投掷物事件 | `EntityID`；新增/消失事件用 `Value`，更新事件用 `New`、`Previous`。 |

---

## 第二部分：按回调前缀说明触发条件与返回值

### New / Auth / Provider 回调

| 回调 | 具体触发条件 | 回调数据 |
|---|---|---|
| `NewGameState` | 收到且成功解析一个与上一状态不同的 GSI JSON。移动、计时、血量等都可能触发。 | 参数 `GameState`：完整当前状态。 |
| `AuthUpdated` | `auth` 节点改变。 | `New`、`Previous`：认证对象。 |
| `ProviderUpdated` | `provider` 节点改变，例如 Provider 的时间戳或 SteamID 信息变化。 | `New`、`Previous`：Provider；`New.SteamID` 是识别本机账户的可靠来源。 |

### All / Grenade 回调（全局观战数据）

| 回调 | 具体触发条件 | 回调数据 |
|---|---|---|
| `AllPlayersUpdated` | `allplayers` 整体发生变化。 | `New`、`Previous`：全体玩家字典。 |
| `PlayerConnected` | `allplayers` 出现此前不存在的 SteamID。 | `Value`：该玩家。 |
| `PlayerDisconnected` | `allplayers` 中一个原有 SteamID 消失。 | `Value`：该玩家。 |
| `AllGrenadesUpdated` | `allgrenades` 整体发生变化。 | `New`、`Previous`：全体投掷物字典。 |
| `NewGrenade` | `allgrenades` 出现新实体 ID。 | `Value`：投掷物；`EntityID`：实体 ID。 |
| `GrenadeUpdated` | 同一投掷物实体的状态或位置改变。 | `New`、`Previous`：投掷物；`EntityID`。 |
| `ExpiredGrenade` | 原有投掷物实体从 `allgrenades` 消失。 | `Value`：消失的投掷物；`EntityID`。 |

### Kill 回调（全局观战主要依赖）

| 回调 | 具体触发条件 | 回调数据 |
|---|---|---|
| `KillFeed` | 库先收到一次 `PlayerGotKill`（凶手、武器、爆头），再收到一次 `PlayerDied`（死者）；两者有效且不是同一 SteamID 时组合触发。回合变化会清空未完成的组合。 | `Killer`、`Victim`、`Weapon`、`IsHeadshot`。 |

这不是 CS2 原始全局击杀列表；普通玩家视角一般只能看到自己侧的数据，故通常不触发或不可靠。

### Bomb 回调

| 回调 | 具体触发条件 | 回调数据 |
|---|---|---|
| `BombUpdated` | 顶层 `bomb` 对象的状态、位置、携带者或倒计时变化。顶层节点缺失时不触发。 | `New`、`Previous`：`Bomb`。 |
| `BombStateUpdated` | `round.bomb` 或 `bomb.state` 改变；两个来源都可触发。 | `New`、`Previous`：`BombState`。 |
| `BombPlanting` | 新炸弹状态为 `Planting`，且库能从玩家缓存找到 `bomb.player`。主要依赖完整观战数据。 | `Player`：正在下包的玩家。 |
| `BombPlanted` | 新炸弹状态为 `Planted`。普通视角中最可靠的“下包完成”事件。 | 无额外字段。 |
| `BombDefusing` | 新炸弹状态为 `Defusing`，且库能找到 `bomb.player`。主要依赖完整观战数据。 | `Player`：正在拆包的玩家。 |
| `BombDefused` | 新炸弹状态为 `Defused`。普通视角中最可靠的“拆包完成”事件。 | 无额外字段。 |
| `BombDropped` | 新炸弹状态为 `Dropped`。 | 无额外字段。 |
| `BombPickedup` | 新炸弹状态为 `Carried`，且库能找到携带者。 | `Player`：携带 C4 的玩家。 |
| `BombExploded` | 新炸弹状态为 `Exploded`。 | 无额外字段。 |

普通玩家实测常见序列为 `Undefined → Planted` 或 `Planted → Defused`：这会触发 `BombStateUpdated` 与完成事件，但不会触发 `BombPlanting` / `BombDefusing`。如果最终状态来自 `round.bomb` 而非顶层 `bomb`，`BombUpdated` 也可能不触发。

### Map / Game / Team 回调

| 回调 | 具体触发条件 | 回调数据 |
|---|---|---|
| `MapUpdated` | `map` 节点任一字段改变。 | `New`、`Previous`：`Map`。 |
| `GamemodeChanged` | `map.mode` 改变。 | `New`、`Previous`：游戏模式。 |
| `LevelChanged` | `map.name` 改变，例如载入另一张地图。 | `New`、`Previous`：地图名。 |
| `MapPhaseChanged` | `map.phase` 改变。 | `New`、`Previous`：比赛阶段。 |
| `TeamStatisticsUpdated` | CT 或 T 的队伍统计对象任一字段改变。 | `New`、`Previous`：队伍统计；`Team`。 |
| `TeamScoreChanged` | 某队 `score` 改变，通常回合结算加分。 | `New`、`Previous`：比分；`Team`。 |
| `TeamRemainingTimeoutsChanged` | 某队 `remaining_timeouts` 改变，通常叫暂停消耗一次。 | `New`、`Previous`：剩余暂停数；`Team`。 |
| `WarmupStarted` / `WarmupOver` | `map.phase` 进入 / 离开 `Warmup`。 | 无额外字段。 |
| `IntermissionStarted` / `IntermissionOver` | `map.phase` 进入 / 离开 `Intermission`。 | 无额外字段。 |
| `FreezetimeStarted` / `FreezetimeOver` | `map.phase` 进入 / 离开 `Freezetime`。 | 无额外字段。 |
| `PauseStarted` / `PauseOver` | `map.phase` 进入 / 离开 `Paused`。 | 无额外字段。 |
| `TimeoutStarted` / `TimeoutOver` | `map.phase` 进入 / 离开 `Timeout_T` 或 `Timeout_CT`。 | `Team`：叫暂停的队伍。 |
| `MatchStarted` | `map.phase` 进入 `Live`；表示比赛开始或恢复。 | 无额外字段。 |
| `Gameover` | `map.phase` 进入 `Gameover`。 | 无额外字段。 |

### Round / Phase 回调

| 回调 | 具体触发条件 | 回调数据 |
|---|---|---|
| `RoundUpdated` | `round` 节点任一字段改变。 | `New`、`Previous`：`Round`。 |
| `RoundChanged` | `map.round` 改变。 | `New`、`Previous`：回合号。 |
| `RoundStarted` | `map.round` 增加。 | `Round`、`IsFirstRound`、`IsLastRound`。 |
| `RoundConcluded` | `map.round` 增加，且 `map.round_wins` 包含刚结束回合的结算结果。通常在进入下一回合时才触发。 | `Round`、`WinningTeam`、`RoundConclusionReason`、`IsFirstRound`、`IsLastRound`。 |
| `RoundPhaseUpdated` | `round.phase` 或 `phase_countdowns.phase` 改变；两个来源可能造成重复。 | `New`、`Previous`：阶段。 |
| `PhaseCountdownsUpdated` | `phase_countdowns` 节点任一字段改变；倒计时期间高频。 | `New`、`Previous`：倒计时对象。 |
| `TeamRoundVictory` | `round.win_team` 改变为 CT 或 T。 | `Team`：胜方；`Value`：当前回合号。 |
| `TeamRoundLoss` | 随 `TeamRoundVictory` 派生，队伍是胜方的另一侧。 | `Team`：负方；`Value`：当前回合号。 |

`RoundConcluded.RoundConclusionReason` 的含义：`T_Win_Elimination`（T 全歼）、`T_Win_Bomb`（C4 爆炸）、`T_Win_Time`（T 时间条件获胜）、`CT_Win_Elimination`（CT 全歼）、`CT_Win_Defuse`（CT 拆包）、`CT_Win_Rescue`（CT 营救人质）、`CT_Win_Time`（CT 时间条件获胜）。

### Player 回调：总体、队伍、生命与状态

| 回调 | 具体触发条件 | 回调数据 |
|---|---|---|
| `PlayerUpdated` | `player` 整体改变；全局 `allplayers` 可用时也可能来自任意玩家。 | `New`、`Previous`：`Player`；`PlayerID`。 |
| `PlayerTeamChanged` | `player.team` 改变。 | `New`、`Previous`：队伍；`Player`。 |
| `PlayerActivityChanged` | `player.activity` 改变。 | `New`、`Previous`：活动状态；`Player`。 |
| `PlayerStateChanged` | `player.state` 整体改变。 | `New`、`Previous`：`PlayerState`；`Player`。 |
| `PlayerHealthChanged` | `player.state.health` 改变。 | `New`、`Previous`：生命值；`Player`。 |
| `PlayerTookDamage` | 生命值降低。 | `New`、`Previous`：生命值；`Player`。 |
| `PlayerDied` | 新生命值为 `0`。不含凶手信息。 | `New`、`Previous`：生命值；`Player`（死者）。 |
| `PlayerRespawned` | 旧生命为 `0` 且新生命大于 `0`。 | `New`、`Previous`：生命值；`Player`。 |
| `PlayerArmorChanged` | 护甲值改变。 | `New`、`Previous`：护甲；`Player`。 |
| `PlayerHelmetChanged` | 是否有头盔改变。 | `New`、`Previous`：布尔值；`Player`。 |
| `PlayerFlashAmountChanged` | 闪光影响量改变。 | `New`、`Previous`：XML 注释定义为 `0–255` 整数，`0` 表示未受闪；但当前 CS2 实测可能只上报 `0 → 1 → 0`，此时应将**非零视为正在受闪**，不要假设能得到连续强度；`Player`。 |
| `PlayerSmokedAmountChanged` | 烟雾影响量改变。 | `New`、`Previous`：XML 注释定义为 `0–255` 整数，`0` 为未受烟雾影响，数值越大表示影响越强；应以实际原始 JSON 为准；`Player`。 |
| `PlayerBurningAmountChanged` | 燃烧影响量改变。 | `New`、`Previous`：XML 注释定义为 `0–255` 整数，`0` 为未燃烧，数值越大表示燃烧影响越强；应以实际原始 JSON 为准；`Player`。 |
| `PlayerMoneyAmountChanged` | 金钱改变。 | `New`、`Previous`：金钱；`Player`。 |
| `PlayerRoundKillsChanged` | 本回合击杀数改变。 | `New`、`Previous`：击杀数；`Player`。 |
| `PlayerRoundHeadshotKillsChanged` | 本回合爆头击杀数改变。 | `New`、`Previous`：爆头击杀数；`Player`。 |
| `PlayerRoundTotalDamageChanged` | 本回合总伤害改变。 | `New`、`Previous`：伤害；`Player`。 |
| `PlayerEquipmentValueChanged` | 装备价值改变。 | `New`、`Previous`：装备价值；`Player`。 |
| `PlayerDefusekitChanged` | 是否拥有拆弹器改变。 | `New`、`Previous`：布尔值；`Player`。 |

### Weapon 回调

| 回调 | 具体触发条件 | 回调数据 |
|---|---|---|
| `PlayerWeaponChanged` | 武器集合改变后，库识别到一把武器变化，且玩家存活。此库的名称匹配条件严格，可能较少出现。 | `New`、`Previous`：`Weapon`；`Player`。 |
| `PlayerActiveWeaponChanged` | 武器集合改变，活动武器名称与上次不同，且玩家存活。 | `New`、`Previous`：活动武器；`Player`。 |
| `PlayerWeaponsPickedUp` | 出现此前没有的非刀、非拳套武器。 | `Weapons`：拾取的武器列表；`Player`。 |
| `PlayerWeaponsDropped` | 消失此前拥有的非刀、非拳套武器。 | `Weapons`：丢失的武器列表；`Player`。 |

### Player 战绩与击杀回调

| 回调 | 具体触发条件 | 回调数据 |
|---|---|---|
| `PlayerStatsChanged` | `player.match_stats` 整体改变。 | `New`、`Previous`：`MatchStats`；`Player`。 |
| `PlayerKillsChanged` | 总击杀数改变。 | `New`、`Previous`：击杀数；`Player`。 |
| `PlayerGotKill` | 本回合击杀数增加，且旧回合击杀数不为 `-1`。 | `Player`（击杀者）、`Weapon`、`IsHeadshot`、`IsAce`。 |
| `PlayerAssistsChanged` | 助攻数改变。 | `New`、`Previous`：助攻数；`Player`。 |
| `PlayerDeathsChanged` | 总死亡数改变。 | `New`、`Previous`：死亡数；`Player`。 |
| `PlayerMVPsChanged` | MVP 数量改变。 | `New`、`Previous`：MVP 数；`Player`。 |
| `PlayerScoreChanged` | 得分改变。 | `New`、`Previous`：得分；`Player`。 |

## 使用建议

- 要判断回合为什么结束：订阅 `RoundConcluded`，读取 `RoundConclusionReason`。
- 要判断自己击杀/死亡：使用 `PlayerGotKill`、`PlayerDied`，并以 `Provider.SteamID` 过滤。
- 要判断下包/拆包成功：使用 `BombPlanted`、`BombDefused`；不要依赖 `BombPlanting`、`BombDefusing`。
- 要测试全局事件：先用全局观战视角，并在原始 GSI JSON 中确认存在 `allplayers` / `allgrenades`。
