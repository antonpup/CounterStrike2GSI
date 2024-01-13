using Newtonsoft.Json.Linq;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Information about grenades. Key is grenade ID, Value is grenade information.
    /// </summary>
    public class AllGrenades : NodeMap<string, Grenade>
    {

        internal AllGrenades(JObject parsed_data = null) : base()
        {
            if (parsed_data != null)
            {
                foreach (var property in parsed_data.Properties())
                {
                    string property_name = property.Name;

                    if (property.Value.Type == JTokenType.Object)
                    {
                        var grenade_id = property_name;
                        var grenade_data = new Grenade(property.Value as JObject);

                        if (!ContainsKey(grenade_id))
                        {
                            Add(grenade_id, grenade_data);
                        }
                        else
                        {
                            this[grenade_id] = grenade_data;
                        }
                    }
                }
            }
        }
    }
}
