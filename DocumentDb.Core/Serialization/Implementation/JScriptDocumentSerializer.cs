﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.Script.Serialization;
using DocumentDb.Core.Model;
using DocumentDb.Core.Reflection;

namespace DocumentDb.Core.Serialization.Implementation
{
    public class JScriptDocumentSerializer : IDocumentSerializer
    {
        private readonly JavaScriptSerializer _serializer;

        public JScriptDocumentSerializer()
        {
            _serializer = new JavaScriptSerializer();
        }

        public DocumentSerializer SerializerType => DocumentSerializer.JScript;

        public void Serialize(object obj, ref Document doc)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var objType = obj.GetType();
            doc.Content = _serializer.Serialize(obj);
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
                var des = _serializer.Deserialize(doc.Content, type);

                return des;
            }

            var obj = _serializer.DeserializeObject(doc.Content) as IDictionary<String, object>;

            if (obj == null)
            {
                throw new InvalidCastException("Could not convert serialized object");
            }

            return ConvertToDynamic(obj);
        }

        private static object ConvertToDynamic(IEnumerable<KeyValuePair<string, object>> obj)
        {
            var dyn = new ExpandoObject() as IDictionary<String, object>;
            foreach (var pair in obj)
            {
                if (pair.Value is IEnumerable<KeyValuePair<string, object>>)
                {
                    dyn[pair.Key] = ConvertToDynamic((IEnumerable<KeyValuePair<string, object>>) pair.Value);
                }
                else
                {
                    dyn[pair.Key] = pair.Value;
                }
            }

            return dyn;
        }
    }
}