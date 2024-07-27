// <copyright file="ChainHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainExample.Common
{
    /// <inheritdoc />
    public abstract class ChainHandler<T> : IChainHandler<T>
        where T : ChainPayload
    {
        private readonly IChainHandler<T>? _chainHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainHandler{T}"/> class.
        /// </summary>
        /// <param name="chainHandler">An instance of an <see cref="IChainHandler{T}"/> interface.</param>
        protected ChainHandler(IChainHandler<T>? chainHandler)
        {
            _chainHandler = chainHandler;
        }

        /// <inheritdoc/>
        public async Task<T> Execute(T payload)
        {
            if (payload.IsFaulted)
            {
                return payload;
            }

            var result = await DoWork(payload);

            return _chainHandler == null ? result : await _chainHandler.Execute(result);
        }

        /// <summary>
        /// A function to implement for each handler.
        /// </summary>
        /// <param name="payload">The <see cref="ChainPayload"/> object to execute.</param>
        /// <returns>The updated <see cref="ChainPayload"/> object.</returns>
        protected abstract Task<T> DoWork(T payload);
    }
}
