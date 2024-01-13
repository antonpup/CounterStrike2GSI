using CounterStrike2GSI.Nodes;

namespace CounterStrike2GSI.EventMessages
{
    /// <summary>
    /// Event for overall All Grenades update. 
    /// </summary>
    public class AllGrenadesUpdated : UpdateEvent<AllGrenades>
    {
        public AllGrenadesUpdated(AllGrenades new_value, AllGrenades previous_value) : base(new_value, previous_value)
        {
        }
    }

    /// <summary>
    /// Event for a Grenade update. 
    /// </summary>
    public class GrenadeUpdated : EntityUpdateEvent<Grenade>
    {
        public GrenadeUpdated(Grenade new_value, Grenade previous_value, string entity_id) : base(new_value, previous_value, entity_id)
        {
        }
    }
}
