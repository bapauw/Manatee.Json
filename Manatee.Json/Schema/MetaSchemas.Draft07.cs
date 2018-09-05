﻿namespace Manatee.Json.Schema
{
	public static partial class MetaSchemas
	{
		public static JsonSchema Draft07 =
			new JsonSchema()
				.Schema("http://json-schema.org/draft-07/schema#")
				.Id("http://json-schema.org/draft-07/schema#")
				.Title("Core schema meta-schema")
				.Definition("schemaArray", new JsonSchema()
								.Type(JsonSchemaType.Array)
								.MinItems(1)
								.Items(new JsonSchema().RefRoot()))
				.Definition("nonNegativeInteger", new JsonSchema()
								.Type(JsonSchemaType.Integer)
								.Minimum(0))
				.Definition("nonNegativeIntegerDefault0", new JsonSchema()
								.AllOf(new JsonSchema().Ref("#/definitions/nonNegativeInteger"),
									   new JsonSchema().Default(0)))
				.Definition("simpleTypes", new JsonSchema()
								.Enum("array", "boolean", "integer", "null", "number", "object", "string"))
				.Definition("stringArray", new JsonSchema()
								.Type(JsonSchemaType.Array)
								.Items(new JsonSchema().Type(JsonSchemaType.String))
								.UniqueItems(true)
								.Default(new JsonArray()))
				.Type(JsonSchemaType.Object | JsonSchemaType.Boolean)
				.Property("$id", new JsonSchema()
							  .Type(JsonSchemaType.String)
							  .Format(StringFormat.UriReference))
				.Property("$schema", new JsonSchema()
							  .Type(JsonSchemaType.String)
							  .Format(StringFormat.Uri))
				.Property("$ref", new JsonSchema()
							  .Type(JsonSchemaType.String)
							  .Format(StringFormat.UriReference))
				.Property("$comment", new JsonSchema().Type(JsonSchemaType.String))
				.Property("title", new JsonSchema().Type(JsonSchemaType.String))
				.Property("description", new JsonSchema().Type(JsonSchemaType.String))
				.Property("default", true)
				.Property("readonly", new JsonSchema()
					          .Type(JsonSchemaType.Boolean)
					          .Default(false))
				.Property("examples", new JsonSchema()
							  .Type(JsonSchemaType.Array)
							  .Items(true))
				.Property("multipleOf", new JsonSchema()
							  .Type(JsonSchemaType.Number)
							  .ExclusiveMinimum(0))
				.Property("maximum", new JsonSchema().Type(JsonSchemaType.Number))
				.Property("exclusiveMaximum", new JsonSchema().Type(JsonSchemaType.Number))
				.Property("minimum", new JsonSchema().Type(JsonSchemaType.Number))
				.Property("exclusiveMinimum", new JsonSchema().Type(JsonSchemaType.Number))
				.Property("maxLength", new JsonSchema().Ref("#/definitions/nonNegativeInteger"))
				.Property("minLength", new JsonSchema().Ref("#/definitions/nonNegativeIntegerDefault0"))
				.Property("pattern", new JsonSchema()
							  .Type(JsonSchemaType.String)
							  .Format(StringFormat.Regex))
				.Property("additionalItems", new JsonSchema().RefRoot())
				.Property("items", new JsonSchema()
							  .AnyOf(new JsonSchema().RefRoot(),
									 new JsonSchema().Ref("#/definitions/schemaArray"))
							  .Default(true))
				.Property("maxItems", new JsonSchema().Ref("#/definitions/nonNegativeInteger"))
				.Property("minItems", new JsonSchema().Ref("#/definitions/nonNegativeIntegerDefault0"))
				.Property("uniqueItems", new JsonSchema()
							  .Type(JsonSchemaType.Boolean)
							  .Default(false))
				.Property("contains", new JsonSchema().RefRoot())
				.Property("maxProperties", new JsonSchema().Ref("#/definitions/nonNegativeInteger"))
				.Property("minProperties", new JsonSchema().Ref("#/definitions/nonNegativeIntegerDefault0"))
				.Property("required", new JsonSchema().Ref("#/definitions/stringArray"))
				.Property("additionalProperties", new JsonSchema().RefRoot())
				.Property("definitions", new JsonSchema()
							  .Type(JsonSchemaType.Object)
							  .AdditionalProperties(new JsonSchema().RefRoot())
							  .Default(new JsonObject()))
				.Property("properties", new JsonSchema()
							  .Type(JsonSchemaType.Object)
							  .AdditionalProperties(new JsonSchema().RefRoot())
							  .Default(new JsonObject()))
				.Property("patternProperties", new JsonSchema()
							  .Type(JsonSchemaType.Object)
							  .AdditionalProperties(new JsonSchema().RefRoot())
					          .PropertyNames(new JsonSchema().Format(StringFormat.Regex))
							  .Default(new JsonObject()))
				.Property("dependencies", new JsonSchema()
							  .Type(JsonSchemaType.Object)
							  .AdditionalProperties(new JsonSchema()
														.AnyOf(new JsonSchema().RefRoot(),
															   new JsonSchema().Ref("#/definitions/stringArray"))))
				.Property("propertyNames", new JsonSchema().RefRoot())
				.Property("const", true)
				.Property("enum", new JsonSchema()
							  .Type(JsonSchemaType.Array)
					          .Items(true)
							  .MinItems(1)
							  .UniqueItems(true))
				.Property("type", new JsonSchema()
							  .AnyOf(new JsonSchema().Ref("#/definitions/simpleTypes"),
									 new JsonSchema()
										 .Type(JsonSchemaType.Array)
										 .Items(new JsonSchema().Ref("#/definitions/simpleTypes"))
										 .MinItems(1)
										 .UniqueItems(true)))
				.Property("format", new JsonSchema().Type(JsonSchemaType.String))
				.Property("contentMediaType", new JsonSchema().Type(JsonSchemaType.String))
				.Property("contentEncoding", new JsonSchema().Type(JsonSchemaType.String))
				.Property("if", new JsonSchema().RefRoot())
				.Property("then", new JsonSchema().RefRoot())
				.Property("else", new JsonSchema().RefRoot())
				.Property("allOf", new JsonSchema().Ref("#/definitions/schemaArray"))
				.Property("anyOf", new JsonSchema().Ref("#/definitions/schemaArray"))
				.Property("oneOf", new JsonSchema().Ref("#/definitions/schemaArray"))
				.Property("not", new JsonSchema().RefRoot())
				.Default(true);
	}
}