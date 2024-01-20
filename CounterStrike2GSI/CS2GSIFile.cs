using CounterStrike2GSI.Utils;
using System;
using System.IO;

namespace CounterStrike2GSI
{
    /// <summary>
    /// Class handling Game State Integration configuration file generation.
    /// </summary>
    public class CS2GSIFile
    {
        /// <summary>
        /// Attempts to create a Game State Integration configuraion file.<br/>
        /// The configuration will target <c>http://localhost:{port}/</c> address.<br/>
        /// Returns true on success, false otherwise.
        /// </summary>
        /// <param name="name">The name of your integration.</param>
        /// <param name="port">The port for your integration.</param>
        /// <returns>Returns true on success, false otherwise.</returns>
        public static bool CreateFile(string name, int port)
        {
            return CreateFile(name, $"http://localhost:{port}/");
        }

        /// <summary>
        /// Attempts to create a Game State Integration configuraion file.<br/>
        /// The configuration will target the specified URI address.<br/>
        /// Returns true on success, false otherwise.
        /// </summary>
        /// <param name="name">The name of your integration.</param>
        /// <param name="uri">The URI for your integration.</param>
        /// <returns>Returns true on success, false otherwise.</returns>
        public static bool CreateFile(string name, string uri)
        {
            string csgo_path = SteamUtils.GetGamePath(730);

            try
            {
                if (!string.IsNullOrWhiteSpace(csgo_path))
                {
                    string gsifolder = csgo_path + @"\game\csgo\cfg\";
                    Directory.CreateDirectory(gsifolder);
                    string gsifile = gsifolder + @$"gamestate_integration_{name}.cfg";

                    ACF provider_configuration = new ACF();
                    provider_configuration.Items["provider"] = "1";
                    provider_configuration.Items["tournamentdraft"] = "1";
                    provider_configuration.Items["map"] = "1";
                    provider_configuration.Items["map_round_wins"] = "1";
                    provider_configuration.Items["round"] = "1";
                    provider_configuration.Items["player_id"] = "1";
                    provider_configuration.Items["player_state"] = "1";
                    provider_configuration.Items["player_weapons"] = "1";
                    provider_configuration.Items["player_match_stats"] = "1";
                    provider_configuration.Items["player_position"] = "1";
                    provider_configuration.Items["phase_countdowns"] = "1";
                    provider_configuration.Items["allplayers_id"] = "1";
                    provider_configuration.Items["allplayers_state"] = "1";
                    provider_configuration.Items["allplayers_match_stats"] = "1";
                    provider_configuration.Items["allplayers_weapons"] = "1";
                    provider_configuration.Items["allplayers_position"] = "1";
                    provider_configuration.Items["allgrenades"] = "1";
                    provider_configuration.Items["bomb"] = "1";

                    ACF gsi_configuration = new ACF();
                    gsi_configuration.Items["uri"] = uri;
                    gsi_configuration.Items["timeout"] = "5.0";
                    gsi_configuration.Items["buffer"] = "0.1";
                    gsi_configuration.Items["throttle"] = "0.1";
                    gsi_configuration.Items["heartbeat"] = "10.0";
                    gsi_configuration.Children["data"] = provider_configuration;

                    ACF gsi = new ACF();
                    gsi.Children[$"{name} Integration Configuration"] = gsi_configuration;

                    File.WriteAllText(gsifile, gsi.ToString());

                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}
