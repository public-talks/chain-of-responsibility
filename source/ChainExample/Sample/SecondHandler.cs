// <copyright file="SecondHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Net.Http.Json;
using ChainExample.Common;

namespace ChainExample.Sample
{
    /// <inheritdoc />
    public class SecondHandler : ChainHandler<MyPayload>
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecondHandler"/> class.
        /// </summary>
        /// <param name="chainHandler">An instance of the <see cref="IChainHandler{T}"/> interface.</param>
        /// <param name="httpClient">An instance of the <see cref="HttpClient"/> class.</param>
        public SecondHandler(IChainHandler<MyPayload>? chainHandler, HttpClient httpClient)
            : base(chainHandler)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc />
        protected override async Task<MyPayload> DoWork(MyPayload payload)
        {
            try
            {
                var message = await _httpClient.GetAsync(new Uri("https://swapi.dev/api/planets"));

                var response = await message.Content.ReadFromJsonAsync<WebResponse>();

                payload.Value += response?.Count ?? 10;

                return payload;
            }
            catch
            {
                payload.IsFaulted = true;

                return payload;
            }
        }
    }
}
