﻿// Permission is hereby granted, free of charge, to any person obtaining a copy
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
    using System.Collections.Generic;
    using System.Reflection;
    using Newtonsoft.Json;

    /// <summary>
    /// Converts a DripCampaignActivate object to JSON.
    /// </summary>
    public class DripCampaignActivateRequestConverter : JsonConverter
    {
        public static class PropertyNames
        {
            public const string ProviderId = "esp_account";
            public const string Locale = "locale";
            public const string Recipient = "recipient";
            public const string Name = "name";
            public const string Address = "address";
            public const string Data = "email_data";
            public const string CopyTo = "cc";
            public const string BlindCopyTo = "bcc";
            public const string Tags = "tags";
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IDripCampaignActivateRequest).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // KLUGE: JsonSerializer is not mockable, so we resort to the bad practice of inserting
            // code (i.e., the proxy) merely to support unit testing.
            this.WriteJson(writer, value, new SerializerProxy(serializer));
        }

        protected internal virtual void WriteJson(JsonWriter writer, object value, SerializerProxy serializer)
        {
            EnsureArgument.NotNull(writer, "writer");
            EnsureArgument.NotNull(serializer, "serializer");

            var request = EnsureArgument.Of<IDripCampaignActivateRequest>(value, "value");

            writer.WriteStartObject();

            this.WriteProperty(writer, serializer, PropertyNames.ProviderId, request.ProviderId, true);
            this.WriteProperty(writer, serializer, PropertyNames.Locale, request.Locale, true);
            this.WriteProperty(writer, serializer, PropertyNames.Data, request.Data, true);
            this.WriteProperty(writer, serializer, PropertyNames.Tags, request.Tags, true);
            this.WritePrimaryRecipient(writer, serializer, request);
            this.WriteCcRecipients(writer, serializer, request);
            this.WriteBccRecipients(writer, serializer, request);

            writer.WriteEndObject();
        }

        protected internal virtual void WritePrimaryRecipient(JsonWriter writer, SerializerProxy serializer, IDripCampaignActivateRequest request)
        {
            writer.WritePropertyName(PropertyNames.Recipient);
            writer.WriteStartObject();
            this.WriteProperty(writer, serializer, PropertyNames.Name, request.RecipientName, true);
            this.WriteProperty(writer, serializer, PropertyNames.Address, request.RecipientAddress, false);
            writer.WriteEndObject();
        }

        protected internal virtual void WriteCcRecipients(JsonWriter writer, SerializerProxy serializer, IDripCampaignActivateRequest request)
        {
            this.WriteRecipientsList(writer, serializer, PropertyNames.CopyTo, request.CopyTo);
        }

        protected internal virtual void WriteBccRecipients(JsonWriter writer, SerializerProxy serializer, IDripCampaignActivateRequest request)
        {
            this.WriteRecipientsList(writer, serializer, PropertyNames.BlindCopyTo, request.BlindCopyTo);
        }

        protected internal virtual void WriteRecipientsList(JsonWriter writer, SerializerProxy serializer, string name, IEnumerable<string> recipients)
        {
            if (recipients == null)
            {
                return;
            }

            writer.WritePropertyName(name);
            writer.WriteStartArray();

            foreach (var address in recipients)
            {
                writer.WriteStartObject();
                this.WriteProperty(writer, serializer, PropertyNames.Address, address, false);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }

        protected internal virtual void WriteProperty(JsonWriter writer, SerializerProxy serializer, string name, object value, bool isOptional)
        {
            if (isOptional && value == null)
            {
                return;
            }

            writer.WritePropertyName(name);
            serializer.Serialize(writer, value);
        }

        protected internal virtual void WriteProperty(JsonWriter writer, SerializerProxy serializer, string name, string value, bool isOptional)
        {
            if (isOptional && String.IsNullOrEmpty(value))
            {
                return;
            }

            writer.WritePropertyName(name);
            serializer.Serialize(writer, value);
        }
    }
}
