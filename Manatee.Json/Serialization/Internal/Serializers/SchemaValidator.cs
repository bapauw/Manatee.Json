﻿using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;
using Manatee.Json.Internal;
using Manatee.Json.Schema;

namespace Manatee.Json.Serialization.Internal.Serializers
{
	internal class SchemaValidator : ISerializer
	{
		private static readonly ConcurrentDictionary<TypeInfo, JsonSchema> _schemas
			= new ConcurrentDictionary<TypeInfo, JsonSchema>();

		private readonly ISerializer _innerSerializer;

		public bool ShouldMaintainReferences => _innerSerializer.ShouldMaintainReferences;

		public SchemaValidator(ISerializer innerSerializer)
		{
			_innerSerializer = innerSerializer;
		}

		public bool Handles(SerializationContext context)
		{
			return true;
		}
		public JsonValue Serialize(SerializationContext context)
		{
			return _innerSerializer.Serialize(context);
		}
		public object Deserialize(SerializationContext context)
		{
			var typeInfo = context.InferredType.GetTypeInfo();
			var schema = _GetSchema(typeInfo);
			if (schema != null)
			{
				var results = schema.Validate(context.LocalValue);
				if (!results.IsValid)
					throw new JsonSerializationException($"JSON did not pass schema defined by type '{context.InferredType}'.\n" +
					                                     "Errors:\n" +
														 context.RootSerializer.Serialize(results));
			}

			return _innerSerializer.Deserialize(context);
		}
		private static JsonSchema _GetSchema(TypeInfo typeInfo)
		{
			return _schemas.GetOrAdd(typeInfo, _GetSchemaSlow);
		}
		private static JsonSchema _GetSchemaSlow(TypeInfo typeInfo)
		{
			var attribute = typeInfo.GetCustomAttribute<SchemaAttribute>();
			if (attribute == null)
				return null;

			Exception exception = null;
			JsonSchema schema = null;
			try
			{
				schema = _GetPropertySchema(typeInfo, attribute) ?? _GetFileSchema(attribute);
			}
			catch (FileNotFoundException e)
			{
				exception = e;
			}
			catch (UriFormatException e)
			{
				exception = e;
			}

			if (schema == null)
				throw new JsonSerializationException($"The value '{attribute.Source}' could not be translated into a valid schema. " +
				                                     $"This value should represent either a public static property on the {typeInfo.Name} type " +
				                                     $"or a file with this name should exist at the execution path.", exception);

			return schema;
		}
		private static JsonSchema _GetPropertySchema(TypeInfo typeInfo, SchemaAttribute attribute)
		{
			var propertyName = attribute.Source;
			var property = typeInfo.GetAllProperties()
			                       .FirstOrDefault(p => typeof(JsonSchema).GetTypeInfo().IsAssignableFrom(p.PropertyType.GetTypeInfo()) &&
			                                            p.GetMethod.IsStatic && p.Name == propertyName);
			var schema = (JsonSchema) property?.GetMethod.Invoke(null, new object[] { });
			return schema;
		}
		private static JsonSchema _GetFileSchema(SchemaAttribute attribute)
		{
			var uri = attribute.Source;
			if (!Uri.TryCreate(uri, UriKind.Absolute, out _))
			{
				uri = System.IO.Path.Combine(Directory.GetCurrentDirectory(), uri);
			}
			var schema = JsonSchemaRegistry.Get(uri);

			return schema;
		}
	}
}
