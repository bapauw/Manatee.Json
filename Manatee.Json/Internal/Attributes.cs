﻿using System;

namespace Manatee.Json.Internal
{
#if !NET45
	[CompilerAttributes.GeneratesWarning("This constructor is provided for deserialization purposes only.  Please use the parameterized one instead.")]
#endif
	internal class DeserializationUseOnlyAttribute : Attribute
	{
	}
}
