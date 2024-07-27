// <copyright file="IChainHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainExample.Common
{
    /// <summary>
    /// Common interface for each handler.
    /// </summary>
    /// <typeparam name="T">The type of the payload.</typeparam>
    public interface IChainHandler<T>
        where T : ChainPayload
    {
        /// <summary>
        /// Executes a chain handler.
        /// </summary>
        /// <param name="payload">The payload to execute against.</param>
        /// <returns>An updated <see cref="ChainPayload"/> object.</returns>
        Task<T> Execute(T payload);
    }
}
