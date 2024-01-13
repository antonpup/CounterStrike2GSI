using Newtonsoft.Json.Linq;

namespace CounterStrike2GSI.Nodes
{
    /// <summary>
    /// Information about the authentication of this GameState.
    /// </summary>
    public class Auth : NodeMap<string, string>
    {
        internal Auth(JObject parsed_data = null) : base()
        {
            if (parsed_data != null)
            {
                foreach (var property in parsed_data.Properties())
                {
                    string property_name = property.Name;

                    if (property.Value.Type == JTokenType.String)
                    {
                        var auth_id = property_name;
                        var auth_data = property.Value.ToString();

                        if (!ContainsKey(auth_id))
                        {
                            Add(auth_id, auth_data);
                        }
                        else
                        {
                            this[auth_id] = auth_data;
                        }
                    }
                }
            }
        }
    }
}
