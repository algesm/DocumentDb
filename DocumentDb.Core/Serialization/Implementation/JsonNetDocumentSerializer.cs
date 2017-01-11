using System;
using DocumentDb.Core.Model;
using DocumentDb.Core.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DocumentDb.Core.Serialization.Implementation
{
    public class JsonNetDocumentSerializer : IDocumentSerializer
    {
        public DocumentSerializer SerializerType => DocumentSerializer.JsonNet;

        public void Serialize(object obj, ref Document doc)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var objType = obj.GetType();
            doc.Content = JsonConvert.SerializeObject(obj, new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto});
            doc.Type = objType.IsAnonymousType() ? String.Empty : objType.SimplifiedTypeName();
        }

        public object Deserialize(Document doc)
        {
            if (String.IsNullOrEmpty(doc.Content))
            {
                return null;
            }

            if (!String.IsNullOrEmpty(doc.Type))
            {
                var type = Type.GetType(doc.Type, false);
                var des = JsonConvert.DeserializeObject(doc.Content, type, new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto});

                return des;
            }

            return JObject.Parse(doc.Content);
        }
    }
}