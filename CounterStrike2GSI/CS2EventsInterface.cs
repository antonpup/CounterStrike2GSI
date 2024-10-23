using CounterStrike2GSI.EventMessages;

namespace CounterStrike2GSI
{
    public class CS2EventsInterface : EventsInterface<CS2GameEvent>
    {
        #region AllGrenadesEvents

        public delegate void AllGrenadesUpdatedHandler(AllGrenadesUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.AllGrenadesUpdated" />
        public event AllGrenadesUpdatedHandler AllGrenadesUpdated = delegate { };

        public delegate void GrenadeUpdatedHandler(GrenadeUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.GrenadeUpdated" />
        public event GrenadeUpdatedHandler GrenadeUpdated = delegate { };

        public delegate void NewGrenadeHandler(NewGrenade game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.NewGrenade" />
        public event NewGrenadeHandler NewGrenade = delegate { };

        public delegate void ExpiredGrenadeHandler(ExpiredGrenade game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.ExpiredGrenade" />
        public event ExpiredGrenadeHandler ExpiredGrenade = delegate { };

        #endregion

        #region AllPlayersEvents

        public delegate void AllPlayersUpdatedHandler(AllPlayersUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.AllPlayersUpdated" />
        public event AllPlayersUpdatedHandler AllPlayersUpdated = delegate { };

        public delegate void PlayerJoinedHandler(PlayerConnected game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerConnected" />
        public event PlayerJoinedHandler PlayerConnected = delegate { };

        public delegate void PlayerDisconnectedHandler(PlayerDisconnected game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerDisconnected" />
        public event PlayerDisconnectedHandler PlayerDisconnected = delegate { };

        #endregion

        #region AuthEvents

        public delegate void AuthUpdatedHandler(AuthUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.AuthUpdated" />
        public event AuthUpdatedHandler AuthUpdated = delegate { };

        #endregion

        #region BombEvents

        public delegate void BombUpdatedHandler(BombUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.BombUpdated" />
        public event BombUpdatedHandler BombUpdated = delegate { };

        public delegate void BombPlantingHandler(BombPlanting game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.BombPlanting" />
        public event BombPlantingHandler BombPlanting = delegate { };

        public delegate void BombPlantedHandler(BombPlanted game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.BombPlanted" />
        public event BombPlantedHandler BombPlanted = delegate { };

        public delegate void BombDefusedHandler(BombDefused game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.BombDefused" />
        public event BombDefusedHandler BombDefused = delegate { };

        public delegate void BombDefusingHandler(BombDefusing game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.BombDefusing" />
        public event BombDefusingHandler BombDefusing = delegate { };

        public delegate void BombDroppedHandler(BombDropped game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.BombDropped" />
        public event BombDroppedHandler BombDropped = delegate { };

        public delegate void BombPickedupHandler(BombPickedup game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.BombPickedup" />
        public event BombPickedupHandler BombPickedup = delegate { };

        public delegate void BombExplodedHandler(BombExploded game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.BombExploded" />
        public event BombExplodedHandler BombExploded = delegate { };

        #endregion

        #region KillfeedEvents

        public delegate void KillFeedHandler(KillFeed game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.KillFeed" />
        public event KillFeedHandler KillFeed = delegate { };

        #endregion

        #region MapEvents

        public delegate void MapUpdatedHandler(MapUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.MapUpdated" />
        public event MapUpdatedHandler MapUpdated = delegate { };

        public delegate void GamemodeChangedHandler(GamemodeChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.GamemodeChanged" />
        public event GamemodeChangedHandler GamemodeChanged = delegate { };

        public delegate void TeamStatisticsUpdatedHandler(TeamStatisticsUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.TeamStatisticsUpdated" />
        public event TeamStatisticsUpdatedHandler TeamStatisticsUpdated = delegate { };

        public delegate void TeamScoreChangedHandler(TeamScoreChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.TeamScoreChanged" />
        public event TeamScoreChangedHandler TeamScoreChanged = delegate { };

        public delegate void TeamRemainingTimeoutsChangedHandler(TeamRemainingTimeoutsChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.TeamRemainingTimeoutsChanged" />
        public event TeamRemainingTimeoutsChangedHandler TeamRemainingTimeoutsChanged = delegate { };

        public delegate void RoundChangedHandler(RoundChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.RoundChanged" />
        public event RoundChangedHandler RoundChanged = delegate { };

        public delegate void RoundConcludedHandler(RoundConcluded game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.RoundConcluded" />
        public event RoundConcludedHandler RoundConcluded = delegate { };

        public delegate void RoundStartedHandler(RoundStarted game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.RoundStarted" />
        public event RoundStartedHandler RoundStarted = delegate { };

        public delegate void LevelChangedHandler(LevelChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.LevelChanged" />
        public event LevelChangedHandler LevelChanged = delegate { };

        public delegate void MapPhaseChangedHandler(MapPhaseChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.MapPhaseChanged" />
        public event MapPhaseChangedHandler MapPhaseChanged = delegate { };

        public delegate void WarmupStartedHandler(WarmupStarted game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.WarmupStarted" />
        public event WarmupStartedHandler WarmupStarted = delegate { };

        public delegate void WarmupOverHandler(WarmupOver game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.WarmupOver" />
        public event WarmupOverHandler WarmupOver = delegate { };

        public delegate void IntermissionStartedHandler(IntermissionStarted game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.IntermissionStarted" />
        public event IntermissionStartedHandler IntermissionStarted = delegate { };

        public delegate void IntermissionOverHandler(IntermissionOver game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.IntermissionOver" />
        public event IntermissionOverHandler IntermissionOver = delegate { };

        public delegate void FreezetimeStartedHandler(FreezetimeStarted game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.FreezetimeStarted" />
        public event FreezetimeStartedHandler FreezetimeStarted = delegate { };

        public delegate void FreezetimeOverHandler(FreezetimeOver game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.FreezetimeOver" />
        public event FreezetimeOverHandler FreezetimeOver = delegate { };

        public delegate void PauseStartedHandler(PauseStarted game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PauseStarted" />
        public event PauseStartedHandler PauseStarted = delegate { };

        public delegate void PauseOverHandler(PauseOver game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PauseOver" />
        public event PauseOverHandler PauseOver = delegate { };

        public delegate void TimeoutStartedHandler(TimeoutStarted game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.TimeoutStarted" />
        public event TimeoutStartedHandler TimeoutStarted = delegate { };

        public delegate void TimeoutOverHandler(TimeoutOver game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.TimeoutOver" />
        public event TimeoutOverHandler TimeoutOver = delegate { };

        public delegate void MatchStartedHandler(MatchStarted game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.MatchStarted" />
        public event MatchStartedHandler MatchStarted = delegate { };

        public delegate void GameoverHandler(Gameover game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.Gameover" />
        public event GameoverHandler Gameover = delegate { };

        #endregion

        #region PhaseCountdownEvents

        public delegate void PhaseCountdownsUpdatedHandler(PhaseCountdownsUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PhaseCountdownsUpdated" />
        public event PhaseCountdownsUpdatedHandler PhaseCountdownsUpdated = delegate { };

        #endregion

        #region PlayerEvents

        public delegate void PlayerUpdatedHandler(PlayerUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerUpdated" />
        public event PlayerUpdatedHandler PlayerUpdated = delegate { };

        public delegate void PlayerTeamChangedHandler(PlayerTeamChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerTeamChanged" />
        public event PlayerTeamChangedHandler PlayerTeamChanged = delegate { };

        public delegate void PlayerActivityChangedHandler(PlayerActivityChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerActivityChanged" />
        public event PlayerActivityChangedHandler PlayerActivityChanged = delegate { };

        public delegate void PlayerStateChangedHandler(PlayerStateChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerStateChanged" />
        public event PlayerStateChangedHandler PlayerStateChanged = delegate { };

        public delegate void PlayerHealthChangedHandler(PlayerHealthChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerHealthChanged" />
        public event PlayerHealthChangedHandler PlayerHealthChanged = delegate { };

        public delegate void PlayerDiedHandler(PlayerDied game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerDied" />
        public event PlayerDiedHandler PlayerDied = delegate { };

        public delegate void PlayerRespawnedHandler(PlayerRespawned game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerRespawned" />
        public event PlayerRespawnedHandler PlayerRespawned = delegate { };

        public delegate void PlayerTookDamageHandler(PlayerTookDamage game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerTookDamage" />
        public event PlayerTookDamageHandler PlayerTookDamage = delegate { };

        public delegate void PlayerArmorChangedHandler(PlayerArmorChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerArmorChanged" />
        public event PlayerArmorChangedHandler PlayerArmorChanged = delegate { };

        public delegate void PlayerHelmetChangedHandler(PlayerHelmetChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerHelmetChanged" />
        public event PlayerHelmetChangedHandler PlayerHelmetChanged = delegate { };

        public delegate void PlayerFlashAmountChangedHandler(PlayerFlashAmountChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerFlashAmountChanged" />
        public event PlayerFlashAmountChangedHandler PlayerFlashAmountChanged = delegate { };

        public delegate void PlayerSmokedAmountChangedHandler(PlayerSmokedAmountChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerSmokedAmountChanged" />
        public event PlayerSmokedAmountChangedHandler PlayerSmokedAmountChanged = delegate { };

        public delegate void PlayerBurningAmountChangedHandler(PlayerBurningAmountChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerBurningAmountChanged" />
        public event PlayerBurningAmountChangedHandler PlayerBurningAmountChanged = delegate { };

        public delegate void PlayerMoneyAmountChangedHandler(PlayerMoneyAmountChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerMoneyAmountChanged" />
        public event PlayerMoneyAmountChangedHandler PlayerMoneyAmountChanged = delegate { };

        public delegate void PlayerRoundKillsChangedHandler(PlayerRoundKillsChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerRoundKillsChanged" />
        public event PlayerRoundKillsChangedHandler PlayerRoundKillsChanged = delegate { };

        public delegate void PlayerRoundHeadshotKillsChangedHandler(PlayerRoundHeadshotKillsChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerRoundHeadshotKillsChanged" />
        public event PlayerRoundHeadshotKillsChangedHandler PlayerRoundHeadshotKillsChanged = delegate { };

        public delegate void PlayerRoundTotalDamageChangedHandler(PlayerRoundTotalDamageChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerRoundTotalDamageChanged" />
        public event PlayerRoundTotalDamageChangedHandler PlayerRoundTotalDamageChanged = delegate { };

        public delegate void PlayerEquipmentValueChangedHandler(PlayerEquipmentValueChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerRoundTotalDamageChanged" />
        public event PlayerEquipmentValueChangedHandler PlayerEquipmentValueChanged = delegate { };

        public delegate void PlayerDefusekitChangedHandler(PlayerDefusekitChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerDefusekitChanged" />
        public event PlayerDefusekitChangedHandler PlayerDefusekitChanged = delegate { };

        public delegate void PlayerWeaponChangedHandler(PlayerWeaponChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerWeaponChanged" />
        public event PlayerWeaponChangedHandler PlayerWeaponChanged = delegate { };

        public delegate void PlayerActiveWeaponChangedHandler(PlayerActiveWeaponChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerActiveWeaponChanged" />
        public event PlayerActiveWeaponChangedHandler PlayerActiveWeaponChanged = delegate { };

        public delegate void PlayerWeaponsPickedUpHandler(PlayerWeaponsPickedUp game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerWeaponsPickedUp" />
        public event PlayerWeaponsPickedUpHandler PlayerWeaponsPickedUp = delegate { };

        public delegate void PlayerWeaponsDroppedHandler(PlayerWeaponsDropped game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerWeaponsDropped" />
        public event PlayerWeaponsDroppedHandler PlayerWeaponsDropped = delegate { };

        public delegate void PlayerStatsChangedHandler(PlayerStatsChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerStatsChanged" />
        public event PlayerStatsChangedHandler PlayerStatsChanged = delegate { };

        public delegate void PlayerKillsChangedHandler(PlayerKillsChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerKillsChanged" />
        public event PlayerKillsChangedHandler PlayerKillsChanged = delegate { };

        public delegate void PlayerGotKillHandler(PlayerGotKill game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerGotKill" />
        public event PlayerGotKillHandler PlayerGotKill = delegate { };

        public delegate void PlayerAssistsChangedHandler(PlayerAssistsChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerAssistsChanged" />
        public event PlayerAssistsChangedHandler PlayerAssistsChanged = delegate { };

        public delegate void PlayerDeathsChangedHandler(PlayerDeathsChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerDeathsChanged" />
        public event PlayerDeathsChangedHandler PlayerDeathsChanged = delegate { };

        public delegate void PlayerMVPsChangedHandler(PlayerMVPsChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerMVPsChanged" />
        public event PlayerMVPsChangedHandler PlayerMVPsChanged = delegate { };

        public delegate void PlayerScoreChangedHandler(PlayerScoreChanged game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.PlayerScoreChanged" />
        public event PlayerScoreChangedHandler PlayerScoreChanged = delegate { };

        #endregion

        #region ProviderEvents

        public delegate void ProviderUpdatedHandler(ProviderUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.ProviderUpdated" />
        public event ProviderUpdatedHandler ProviderUpdated = delegate { };

        #endregion

        #region RoundEvents

        public delegate void RoundUpdatedHandler(RoundUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.RoundUpdated" />
        public event RoundUpdatedHandler RoundUpdated = delegate { };

        public delegate void RoundPhaseUpdatedHandler(RoundPhaseUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.RoundPhaseUpdated" />
        public event RoundPhaseUpdatedHandler RoundPhaseUpdated = delegate { };

        public delegate void BombStateUpdatedHandler(BombStateUpdated game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.BombStateUpdated" />
        public event BombStateUpdatedHandler BombStateUpdated = delegate { };

        public delegate void TeamRoundVictoryHandler(TeamRoundVictory game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.TeamRoundVictory" />
        public event TeamRoundVictoryHandler TeamRoundVictory = delegate { };

        public delegate void TeamRoundLossHandler(TeamRoundLoss game_event);

        /// <inheritdoc cref="CounterStrike2GSI.EventMessages.TeamRoundLoss" />
        public event TeamRoundLossHandler TeamRoundLoss = delegate { };

        #endregion

        public CS2EventsInterface()
        {
        }

        public override void OnNewGameEvent(CS2GameEvent e)
        {
            base.OnNewGameEvent(e);

            if (e is AllGrenadesUpdated)
            {
                RaiseEvent(AllGrenadesUpdated, e);
            }

            if (e is GrenadeUpdated)
            {
                RaiseEvent(GrenadeUpdated, e);
            }

            if (e is NewGrenade)
            {
                RaiseEvent(NewGrenade, e);
            }

            if (e is ExpiredGrenade)
            {
                RaiseEvent(ExpiredGrenade, e);
            }

            if (e is AllPlayersUpdated)
            {
                RaiseEvent(AllPlayersUpdated, e);
            }

            if (e is PlayerConnected)
            {
                RaiseEvent(PlayerConnected, e);
            }

            if (e is PlayerDisconnected)
            {
                RaiseEvent(PlayerDisconnected, e);
            }

            if (e is AuthUpdated)
            {
                RaiseEvent(AuthUpdated, e);
            }

            if (e is BombUpdated)
            {
                RaiseEvent(BombUpdated, e);
            }

            if (e is BombPlanting)
            {
                RaiseEvent(BombPlanting, e);
            }

            if (e is BombPlanted)
            {
                RaiseEvent(BombPlanted, e);
            }

            if (e is BombDefused)
            {
                RaiseEvent(BombDefused, e);
            }

            if (e is BombDefusing)
            {
                RaiseEvent(BombDefusing, e);
            }

            if (e is BombDropped)
            {
                RaiseEvent(BombDropped, e);
            }

            if (e is BombPickedup)
            {
                RaiseEvent(BombPickedup, e);
            }

            if (e is BombExploded)
            {
                RaiseEvent(BombExploded, e);
            }

            if (e is KillFeed)
            {
                RaiseEvent(KillFeed, e);
            }

            if (e is MapUpdated)
            {
                RaiseEvent(MapUpdated, e);
            }

            if (e is GamemodeChanged)
            {
                RaiseEvent(GamemodeChanged, e);
            }

            if (e is TeamStatisticsUpdated)
            {
                RaiseEvent(TeamStatisticsUpdated, e);
            }

            if (e is TeamScoreChanged)
            {
                RaiseEvent(TeamScoreChanged, e);
            }

            if (e is TeamRemainingTimeoutsChanged)
            {
                RaiseEvent(TeamRemainingTimeoutsChanged, e);
            }

            if (e is RoundChanged)
            {
                RaiseEvent(RoundChanged, e);
            }

            if (e is RoundConcluded)
            {
                RaiseEvent(RoundConcluded, e);
            }

            if (e is RoundStarted)
            {
                RaiseEvent(RoundStarted, e);
            }

            if (e is LevelChanged)
            {
                RaiseEvent(LevelChanged, e);
            }

            if (e is MapPhaseChanged)
            {
                RaiseEvent(MapPhaseChanged, e);
            }

            if (e is WarmupStarted)
            {
                RaiseEvent(WarmupStarted, e);
            }

            if (e is WarmupOver)
            {
                RaiseEvent(WarmupOver, e);
            }

            if (e is IntermissionStarted)
            {
                RaiseEvent(IntermissionStarted, e);
            }

            if (e is IntermissionOver)
            {
                RaiseEvent(IntermissionOver, e);
            }

            if (e is FreezetimeStarted)
            {
                RaiseEvent(FreezetimeStarted, e);
            }

            if (e is FreezetimeOver)
            {
                RaiseEvent(FreezetimeOver, e);
            }

            if (e is PauseStarted)
            {
                RaiseEvent(PauseStarted, e);
            }

            if (e is PauseOver)
            {
                RaiseEvent(PauseOver, e);
            }

            if (e is TimeoutStarted)
            {
                RaiseEvent(TimeoutStarted, e);
            }

            if (e is TimeoutOver)
            {
                RaiseEvent(TimeoutOver, e);
            }

            if (e is MatchStarted)
            {
                RaiseEvent(MatchStarted, e);
            }

            if (e is Gameover)
            {
                RaiseEvent(Gameover, e);
            }

            if (e is PhaseCountdownsUpdated)
            {
                RaiseEvent(PhaseCountdownsUpdated, e);
            }

            if (e is PlayerUpdated)
            {
                RaiseEvent(PlayerUpdated, e);
            }

            if (e is PlayerTeamChanged)
            {
                RaiseEvent(PlayerTeamChanged, e);
            }

            if (e is PlayerActivityChanged)
            {
                RaiseEvent(PlayerActivityChanged, e);
            }

            if (e is PlayerStateChanged)
            {
                RaiseEvent(PlayerStateChanged, e);
            }

            if (e is PlayerHealthChanged)
            {
                RaiseEvent(PlayerHealthChanged, e);
            }

            if (e is PlayerDied)
            {
                RaiseEvent(PlayerDied, e);
            }

            if (e is PlayerRespawned)
            {
                RaiseEvent(PlayerRespawned, e);
            }

            if (e is PlayerTookDamage)
            {
                RaiseEvent(PlayerTookDamage, e);
            }

            if (e is PlayerArmorChanged)
            {
                RaiseEvent(PlayerArmorChanged, e);
            }

            if (e is PlayerHelmetChanged)
            {
                RaiseEvent(PlayerHelmetChanged, e);
            }

            if (e is PlayerFlashAmountChanged)
            {
                RaiseEvent(PlayerFlashAmountChanged, e);
            }

            if (e is PlayerSmokedAmountChanged)
            {
                RaiseEvent(PlayerSmokedAmountChanged, e);
            }

            if (e is PlayerBurningAmountChanged)
            {
                RaiseEvent(PlayerBurningAmountChanged, e);
            }

            if (e is PlayerMoneyAmountChanged)
            {
                RaiseEvent(PlayerMoneyAmountChanged, e);
            }

            if (e is PlayerRoundKillsChanged)
            {
                RaiseEvent(PlayerRoundKillsChanged, e);
            }

            if (e is PlayerRoundHeadshotKillsChanged)
            {
                RaiseEvent(PlayerRoundHeadshotKillsChanged, e);
            }

            if (e is PlayerRoundTotalDamageChanged)
            {
                RaiseEvent(PlayerRoundTotalDamageChanged, e);
            }

            if (e is PlayerEquipmentValueChanged)
            {
                RaiseEvent(PlayerEquipmentValueChanged, e);
            }

            if (e is PlayerDefusekitChanged)
            {
                RaiseEvent(PlayerDefusekitChanged, e);
            }

            if (e is PlayerWeaponChanged)
            {
                RaiseEvent(PlayerWeaponChanged, e);
            }

            if (e is PlayerActiveWeaponChanged)
            {
                RaiseEvent(PlayerActiveWeaponChanged, e);
            }

            if (e is PlayerWeaponsPickedUp)
            {
                RaiseEvent(PlayerWeaponsPickedUp, e);
            }

            if (e is PlayerWeaponsDropped)
            {
                RaiseEvent(PlayerWeaponsDropped, e);
            }

            if (e is PlayerStatsChanged)
            {
                RaiseEvent(PlayerStatsChanged, e);
            }

            if (e is PlayerKillsChanged)
            {
                RaiseEvent(PlayerKillsChanged, e);
            }

            if (e is PlayerGotKill)
            {
                RaiseEvent(PlayerGotKill, e);
            }

            if (e is PlayerAssistsChanged)
            {
                RaiseEvent(PlayerAssistsChanged, e);
            }

            if (e is PlayerDeathsChanged)
            {
                RaiseEvent(PlayerDeathsChanged, e);
            }

            if (e is PlayerMVPsChanged)
            {
                RaiseEvent(PlayerMVPsChanged, e);
            }

            if (e is PlayerScoreChanged)
            {
                RaiseEvent(PlayerScoreChanged, e);
            }

            if (e is ProviderUpdated)
            {
                RaiseEvent(ProviderUpdated, e);
            }

            if (e is RoundUpdated)
            {
                RaiseEvent(RoundUpdated, e);
            }

            if (e is RoundPhaseUpdated)
            {
                RaiseEvent(RoundPhaseUpdated, e);
            }

            if (e is BombStateUpdated)
            {
                RaiseEvent(BombStateUpdated, e);
            }

            if (e is TeamRoundVictory)
            {
                RaiseEvent(TeamRoundVictory, e);
            }

            if (e is TeamRoundLoss)
            {
                RaiseEvent(TeamRoundLoss, e);
            }
        }
    }
}
