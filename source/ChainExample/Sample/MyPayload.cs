// <copyright file="MyPayload.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using ChainExample.Common;

namespace ChainExample.Sample
{
    /// <summary>
    /// Example payload.
    /// </summary>
    public class MyPayload : ChainPayload
    {
        /// <summary>
        /// Gets or sets the payload value.
        /// </summary>
        public int Value { get; set; }
    }
}
