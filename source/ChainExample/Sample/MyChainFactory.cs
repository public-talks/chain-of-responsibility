// <copyright file="MyChainFactory.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using ChainExample.Common;

namespace ChainExample.Sample
{
    /// <inheritdoc />
    public class MyChainFactory : IChainFactory<MyPayload>
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyChainFactory"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of the <see cref="HttpClient"/> class.</param>
        public MyChainFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc/>
        public async Task<MyPayload> ExecuteChain(MyPayload payload)
        {
            var second = new SecondHandler(null, _httpClient);
            var first = new FirstHandler(second);

            return await first.Execute(payload);
        }
    }
}
