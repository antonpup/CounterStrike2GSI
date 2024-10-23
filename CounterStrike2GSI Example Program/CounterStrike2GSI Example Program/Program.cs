using CounterStrike2GSI;
using CounterStrike2GSI.EventMessages;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CounterStrike2GSI_Example_program
{
    class Program
    {
        static GameStateListener? _gsl;
        static string specificAllySteamID = "1234567890"; // Replace with the specific ally's Steam ID
        static string discordBotToken = "YOUR_DISCORD_BOT_TOKEN"; // Replace with your Discord bot token
        static string discordGuildId = "YOUR_DISCORD_GUILD_ID"; // Replace with your Discord guild ID
        static string discordUserId = "YOUR_DISCORD_USER_ID"; // Replace with the Discord user ID of the specific ally

        static void Main(string[] args)
        {
            _gsl = new GameStateListener(4000);

            if (!_gsl.GenerateGSIConfigFile("Example"))
            {
                Console.WriteLine("Could not generate GSI configuration file.");
            }

            // There are many callbacks that can be subscribed.
            // This example shows a few.
            _gsl.NewGameState += OnNewGameState;
            _gsl.GameEvent += OnGameEvent;
            _gsl.BombStateUpdated += OnBombStateUpdated;
            _gsl.PlayerGotKill += OnPlayerGotKill;
            _gsl.PlayerDied += OnPlayerDied;
            _gsl.KillFeed += OnKillFeed;
            _gsl.PlayerWeaponsPickedUp += OnPlayerWeaponsPickedUp;
            _gsl.PlayerWeaponsDropped += OnPlayerWeaponsDropped;
            _gsl.RoundStarted += OnRoundStarted;
            _gsl.RoundConcluded += OnRoundConcluded;

            if (!_gsl.Start())
            {
                Console.WriteLine("GameStateListener could not start. Try running this program as Administrator. Exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("Listening for game integration calls...");

            Console.WriteLine("Press ESC to quit");
            do
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(1000);
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void OnNewGameState(GameState gamestate)
        {
            // Guaranteed to fire before CS2GameEvent events.
        }

        private static void OnGameEvent(CS2GameEvent game_event)
        {
            if (game_event is PlayerTookDamage player_took_damage)
            {
                Console.WriteLine($"The player {player_took_damage.Player.Name} took {player_took_damage.Previous - player_took_damage.New} damage!");
            }
            else if (game_event is PlayerActiveWeaponChanged active_weapon_changed)
            {
                Console.WriteLine($"The player {active_weapon_changed.Player.Name} changed their active weapon to {active_weapon_changed.New.Name} from {active_weapon_changed.Previous.Name}!");
            }
        }

        private static void OnBombStateUpdated(BombStateUpdated game_event)
        {
            Console.WriteLine($"The bomb is now {game_event.New}.");
        }

        private static void OnPlayerGotKill(PlayerGotKill game_event)
        {
            Console.WriteLine($"The player {game_event.Player.Name} earned a {(game_event.IsHeadshot ? "headshot " : "")}kill with {game_event.Weapon.Name}!" + (game_event.IsAce ? " And it was an ACE!" : ""));
        }

        private static void OnPlayerDied(PlayerDied game_event)
        {
            Console.WriteLine($"The player {game_event.Player.Name} died.");

            if (game_event.Player.SteamID == specificAllySteamID)
            {
                MuteAllyOnDiscord();
            }
        }

        private static void OnKillFeed(KillFeed game_event)
        {
            Console.WriteLine($"{game_event.Killer.Name} killed {game_event.Victim.Name} with {game_event.Weapon.Name}{(game_event.IsHeadshot ? " as a headshot." : ".")}");
        }

        private static void OnPlayerWeaponsPickedUp(PlayerWeaponsPickedUp game_event)
        {
            Console.WriteLine($"The player {game_event.Player.Name} picked up the following weapons:");
            foreach (var weapon in game_event.Weapons)
            {
                Console.WriteLine($"\t{weapon.Name}");
            }
        }

        private static void OnPlayerWeaponsDropped(PlayerWeaponsDropped game_event)
        {
            Console.WriteLine($"The player {game_event.Player.Name} dropped the following weapons:");
            foreach (var weapon in game_event.Weapons)
            {
                Console.WriteLine($"\t{weapon.Name}");
            }
        }

        private static void OnRoundStarted(RoundStarted game_event)
        {
            if (game_event.IsFirstRound)
            {
                Console.WriteLine($"First round {game_event.Round} started.");
            }
            else if (game_event.IsLastRound)
            {
                Console.WriteLine($"Last round {game_event.Round} started.");
            }
            else
            {
                Console.WriteLine($"A new round {game_event.Round} started.");
            }
        }

        private static void OnRoundConcluded(RoundConcluded game_event)
        {
            Console.WriteLine($"Round {game_event.Round} concluded by {game_event.WinningTeam} for reason: {game_event.RoundConclusionReason}");
        }

        private static async void MuteAllyOnDiscord()
        {
            Console.WriteLine("Muting specific ally on Discord...");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bot {discordBotToken}");
                client.DefaultRequestHeaders.Add("Content-Type", "application/json");

                var muteData = new
                {
                    mute = true
                };

                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(muteData), Encoding.UTF8, "application/json");

                var response = await client.PatchAsync($"https://discord.com/api/v9/guilds/{discordGuildId}/members/{discordUserId}", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Successfully muted the specific ally on Discord.");
                }
                else
                {
                    Console.WriteLine($"Failed to mute the specific ally on Discord. Status code: {response.StatusCode}");
                }
            }
        }
    }
}
