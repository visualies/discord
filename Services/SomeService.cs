using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseBot.Services
{
    public class SomeService
    {
        private readonly DiscordClient Bot;
        public SomeService(DiscordClient bot)
        {
            Bot = bot;
        }

        public async Task InitializeAsync()
        {
            //example event handler
            Bot.MessageCreated += async (client, args) =>
            {
                _ = Task.Run(() => Example());
                await Task.CompletedTask;
            };
            await Task.CompletedTask;
        }

        private async Task Example()
        {
            Console.WriteLine("MessageCreated");
            await Task.CompletedTask;
        }
    }
}
