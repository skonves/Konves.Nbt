﻿/*
  Copyright 2014 Steve Konves

	Licensed under the Apache License, Version 2.0 (the "License");
	you may not use this file except in compliance with the License.
	You may obtain a copy of the License at

		http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License. 
*/

namespace Konves.Nbt
{
	/// <summary>
	/// Represents a named binary tag whose value is an array of named binary tags.
	/// </summary>
	public sealed class NbtCompound : NbtTag<NbtTag[]>
	{
		public NbtCompound(string name, NbtTag[] value)
			: base(name, NbtTagType.Compound, value) { }
	}
}
