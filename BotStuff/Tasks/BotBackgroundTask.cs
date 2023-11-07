using Microsoft.Extensions.DependencyInjection;

using TelegramBotBase;
using TelegramBotBase.Builder;

namespace BotAndWebApplication.BotStuff.Tasks
{
    public class BotBackgroundTask : IHostedService
    {
        public BotBackgroundTask(IServiceCollection services)
        {
            // t.me/BotAndWebApplicationTestBot
            
            var serviceProvider = services.BuildServiceProvider();

            BotBaseInstance = BotBaseBuilder.Create()
                                .WithAPIKey("6966935614:AAHoXDM1Rj_voraeGT4saf_uBF-3xeB2ZtE")
                                .DefaultMessageLoop()
                                .WithServiceProvider<StartForm>(serviceProvider)
                                .NoProxy()
                                .DefaultCommands()
                                .NoSerialization()
                                .UseEnglish()
                                .Build();
        }

        /*
        public BotBackgroundTask(IServiceCollection services, ILogger<BotBackgroundTask> logger) : this(services)
        {
            Logger = logger;
        }
        */

        public ILogger<BotBackgroundTask>? Logger { get; }

        public BotBase BotBaseInstance { get; private set; }




        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (BotBaseInstance == null)
                return;

            Logger?.LogInformation("Bot is starting.");

            await BotBaseInstance.UploadBotCommands();

            await BotBaseInstance.Start();


            Logger?.LogInformation("Bot has been started.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (BotBaseInstance == null)
                return;

            Logger?.LogInformation("Bot will shut down.");

            //Let all users know that the bot will shut down.

            await BotBaseInstance.SentToAll("Bot will shut down now.");

            await BotBaseInstance.Stop();

            Logger?.LogInformation("Bot has shutted down.");
        }
    }
}
