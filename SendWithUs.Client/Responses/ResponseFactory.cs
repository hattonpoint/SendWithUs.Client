﻿// Copyright © 2014 Mimeo, Inc.

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
    using System;
    using System.Globalization;
    using System.Net;
    using System.Reflection;
    using Newtonsoft.Json.Linq;

    public class ResponseFactory : IResponseFactory
    {
        public T Create<T>(HttpStatusCode statusCode, JToken json) where T : class, IResponse
        {
            return this.Create(typeof(T), statusCode, json) as T;
        }

        public virtual IResponse Create(Type responseType, HttpStatusCode statusCode, JToken json)
        {
            EnsureArgument.NotNull(responseType, "responseType");

            if (!typeof(IResponse).GetTypeInfo().IsAssignableFrom(responseType.GetTypeInfo()))
            {
                throw new InvalidOperationException(
                    String.Format(CultureInfo.InvariantCulture, "Type '{0}' does not implement IResponse.", responseType.FullName));
            }

            return ((IResponse)Activator.CreateInstance(responseType)).Initialize(statusCode, json);
        }
    }
}
