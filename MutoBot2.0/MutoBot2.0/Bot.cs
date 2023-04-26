using DiscordBot.Commands;
using DSharpPlus;
using DSharpPlus.AsyncEvents;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Bot
    {
        public DiscordClient? Client { get; private set; }
        public InteractivityExtension? Interactivity { get; private set; }
        public CommandsNextExtension? Commands { get; private set; }


        public async Task RunAsync()
        {
            var jsonConfig = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))

            jsonConfig = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(jsonConfig);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            Client.UseInteractivity(new InteractivityConfiguration
            { 
               Timeout= TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] {configJson.Prefix},
                EnableMentionPrefix = true,
                EnableDms = false,
                EnableDefaultHelp = true,
                DmHelp = false,
                
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<TestCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private Task OnClientReady(DiscordClient sender, ReadyEventArgs args)
        {
            Console.WriteLine("Bot is running");
            return Task.CompletedTask;
        }
    }
}