using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands
{
    public class TestCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Returns pong")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong!").ConfigureAwait(false);
        }

        [Command("add")]
        [Description("Adds two numbers together")]
        public async Task Add(CommandContext ctx, 
            [Description("First number")] int numberOne, 
            [Description("Second number")] int numberTwo)
        {
            await ctx.Channel.SendMessageAsync((numberOne + numberTwo).ToString()).ConfigureAwait(false);
        }

        [Command("response")]
        public async Task Response(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
        }

    }
}
