﻿using System.Collections.Generic;
using Manatee.Json.Schema.Generation;

namespace Manatee.Json.Tests.Test_References
{
	public class SchemaGenerationTarget
	{
		[MinLength(10)]
		[MaxLength(20)]
		public string StringProp { get; set; }
		[Minimum(0)]
		[Maximum(50)]
		public int IntProp { get; set; }
		[ExclusiveMinimum(0)]
		[ExclusiveMaximum(50)]
		public double DoubleProp { get; set; }
		public bool BoolProp { get; set; }
		[Required]
		public TestEnum EnumProp { get; set; }
		public FlagsEnum FlagsEnumProp { get; set; }
		public int MappedProp { get; set; }
		[Minimum(10)]
		public List<int> ReadOnlyListProp { get; set; }
		[MaxLength(10)]
		public Dictionary<string, string> ReadOnlyDictionaryProp { get; set; }
		[Format("email")]
		public string Email { get; set; }
		[Regex("^[a-zA-Z0-9]*$")]
		[MinLength(10)]
		[MaxLength(20)]
		public string Alphanumeric { get; set; }
		[Format("uri")]
		public string Website { get; set; }
		public SchemaGenerationTarget Recurse { get; set; }
		public SchemaGenerationSimpleTarget Simple { get; set; }
	}

	public class SchemaGenerationSimpleTarget
	{
		public int Integer { get; set; }
		public string String { get; set; }
	}
}