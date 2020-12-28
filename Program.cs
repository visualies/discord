using BaseBot.Commands;
using BaseBot.Models;
using BaseBot.Services;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BaseBot
{
    public class Program
    {
        private DiscordClient Bot;
        private CommandsNextExtension Commands;
        private FileService FileManager;
        private Config Config;

        static void Main(string[] args)
        {
            var program = new Program();
            program.MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            FileManager = new FileService();
            Config = FileManager.GetConfig();


            //bot setup configuration
            Bot = new DiscordClient(new DiscordConfiguration()
            {
                Token = Config.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,

            });

            var services = ConfigureServices();

            //commands setup configuration
            var ccfg = new CommandsNextConfiguration
            {
                StringPrefixes = new[] { Config.Prefix },
                EnableDms = true,
                EnableMentionPrefix = true,
                Services = services,
                EnableDefaultHelp = false
            };

            Commands = Bot.UseCommandsNext(ccfg);

            //register commands
            Commands.RegisterCommands<ExampleCommands>();

            await services.GetRequiredService<SomeService>().InitializeAsync();

            //login
            await Bot.ConnectAsync();
            await Task.Delay(Timeout.Infinite);

        }


        //dependency injection
        public IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<FileService>()
                .AddSingleton<SomeService>()
                .AddSingleton(Bot)
                .AddSingleton(Config)
                .BuildServiceProvider();
        }
    }
}
