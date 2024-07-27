// <copyright file="ChainPayload.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainExample.Common
{
    /// <summary>
    /// Common base class for a chain payload.
    /// </summary>
    public abstract class ChainPayload
    {
        /// <summary>
        /// Gets or sets a value indicating whether the process has faulted.
        /// </summary>
        public bool IsFaulted { get; set; }
    }
}
