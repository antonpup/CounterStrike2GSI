using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Information about all players. Key is the Steam ID, Value is the player data.
    /// </summary>
    public class AllPlayers : NodeMap<string, Player>
    {
        private Regex _player_steamid_regex = new Regex(@"(\d+)");

        internal AllPlayers(JObject parsed_data = null)
        {
            if (parsed_data != null)
            {
                foreach (var property in parsed_data.Properties())
                {
                    string property_name = property.Name;

                    if (_player_steamid_regex.IsMatch(property_name) && property.Value.Type == JTokenType.Object)
                    {
                        var match = _player_steamid_regex.Match(property_name);
                        var player_steam_id = match.Groups[1].Value;
                        var player_data = new Player(property.Value as JObject, player_steam_id);

                        if (!ContainsKey(player_steam_id))
                        {
                            Add(player_steam_id, player_data);
                        }
                        else
                        {
                            this[player_steam_id] = player_data;
                        }
                    }
                }
            }
        }

        public Player GetPlayer(string player_id)
        {
            foreach (var player_kvp in this)
            {
                if (player_kvp.Key.Equals(player_id))
                {
                    return player_kvp.Value;
                }
            }

            return new Player();
        }
    }
}
