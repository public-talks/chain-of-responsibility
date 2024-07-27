// <copyright file="Program.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using ChainExample.Common;
using ChainExample.Sample;
using Microsoft.Extensions.DependencyInjection;

namespace ChainExample
{
    /// <summary>
    /// Entry class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main()
        {
            var collection = new ServiceCollection();

            collection.AddSingleton<HttpClient>();
            collection.AddTransient<IChainFactory<MyPayload>, MyChainFactory>();

            var provider = collection.BuildServiceProvider();

            var factory = provider.GetRequiredService<IChainFactory<MyPayload>>();

            var result = await factory.ExecuteChain(new MyPayload());

            Console.WriteLine(result.Value);
        }
    }
}
