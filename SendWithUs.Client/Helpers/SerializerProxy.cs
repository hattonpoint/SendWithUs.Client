﻿// Copyright © 2015 Mimeo, Inc.

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace SendWithUs.Client
{
    using Newtonsoft.Json;

    /// <summary>
    /// Provides a mockable proxy over JsonSerializer.
    /// </summary>
    public class SerializerProxy
    {
        /// <summary>
        /// Gets or sets the serializer implementation.
        /// </summary>
        protected JsonSerializer Implementation { get; set; }

        /// <summary>
        /// Initializes a new instance of the SerializerProxy class.
        /// </summary>
        /// <param name="implementation">The serializer implementation.</param>
        public SerializerProxy(JsonSerializer implementation)
        {
            this.Implementation = implementation;
        }

        /// <summary>
        /// Serializes the specified object and writes the Json structure to a Stream using the specified JsonWriter.
        /// </summary>
        /// <param name="writer">The JsonWriter used to write the Json structure.</param>
        /// <param name="value">The object to serialize.</param>
        public virtual void Serialize(JsonWriter writer, object value)
        {
            this.Implementation.Serialize(writer, value);
        }
    }
}
