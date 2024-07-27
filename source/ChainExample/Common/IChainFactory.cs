// <copyright file="IChainFactory.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainExample.Common
{
    /// <summary>
    /// An interface for a chain factory.
    /// </summary>
    /// <typeparam name="T">The type of the payload.</typeparam>
    public interface IChainFactory<T>
        where T : ChainPayload
    {
        /// <summary>
        /// Starts a chain.
        /// </summary>
        /// <param name="payload">The payload to execute.</param>
        /// <returns>The final payload object.</returns>
        Task<T> ExecuteChain(T payload);
    }
}
