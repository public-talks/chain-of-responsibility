// <copyright file="FirstHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using ChainExample.Common;

namespace ChainExample.Sample
{
    /// <inheritdoc />
    public class FirstHandler : ChainHandler<MyPayload>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstHandler"/> class.
        /// </summary>
        /// <param name="chainHandler">The next handler in the chain.</param>
        public FirstHandler(IChainHandler<MyPayload>? chainHandler)
            : base(chainHandler)
        {
        }

        /// <inheritdoc/>
        protected override Task<MyPayload> DoWork(MyPayload payload)
        {
            ++payload.Value;

            return Task.FromResult(payload);
        }
    }
}
