﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Konves.Nbt.Serialization;
using System.Reflection;
using System.IO;
using Konves.Testing;

namespace Konves.Nbt.Tests.Serialization
{
	[TestClass]
	public class SerializationContextTests
	{
		[TestMethod]
		public void SerializeObject_Byte()
		{
			Do_SerializeObject(NbtTagType.Byte, "asdf", 123, null, true, new byte[] { 0x01, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x7B });
		}

		[TestMethod]
		public void SerializeObject_Byte_StringValue()
		{
			Do_SerializeObject(NbtTagType.Byte, "asdf", "123", null, true, new byte[] { 0x01, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x7B });
		}

		[TestMethod]
		public void SerializeObject_Short()
		{
			Do_SerializeObject(NbtTagType.Short, "asdf", 12345, null, true, new byte[] { 0x02, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x30, 0x39 });
		}

		[TestMethod]
		public void SerializeObject_Short_StringValue()
		{
			Do_SerializeObject(NbtTagType.Short, "asdf", "12345", null, true, new byte[] { 0x02, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x30, 0x39 });
		}

		[TestMethod]
		public void SerializeObject_Int()
		{
			Do_SerializeObject(NbtTagType.Int, "asdf", 305419896, null, true, new byte[] { 0x03, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x12, 0x34, 0x56, 0x78 });
		}

		[TestMethod]
		public void SerializeObject_Int_StringValue()
		{
			Do_SerializeObject(NbtTagType.Int, "asdf", "305419896", null, true, new byte[] { 0x03, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x12, 0x34, 0x56, 0x78 });
		}

		[TestMethod]
		public void SerializeObject_Long()
		{
			Do_SerializeObject(NbtTagType.Long, "asdf", 81985529216486895, null, true, new byte[] { 0x04, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
		}

		[TestMethod]
		public void SerializeObject_Long_StringValue()
		{
			Do_SerializeObject(NbtTagType.Long, "asdf", "81985529216486895", null, true, new byte[] { 0x04, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF });
		}

		[TestMethod]
		public void SerializeObject_Float()
		{
			Do_SerializeObject(NbtTagType.Float, "asdf", -3.1415927F, null, true, new byte[] { 0x05, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0xC0, 0x49, 0x0F, 0xDB });
		}

		[TestMethod]
		public void SerializeObject_Float_StringValue()
		{
			Do_SerializeObject(NbtTagType.Float, "asdf", "-3.1415927", null, true, new byte[] { 0x05, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0xC0, 0x49, 0x0F, 0xDB });
		}

		[TestMethod]
		public void SerializeObject_Double()
		{
			Do_SerializeObject(NbtTagType.Double, "asdf", 3.14159265358979311599796346854E0, null, true, new byte[] { 0x06, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18 });
		}

		[TestMethod]
		public void SerializeObject_Double_StringValue()
		{
			Do_SerializeObject(NbtTagType.Double, "asdf", "3.14159265358979311599796346854", null, true, new byte[] { 0x06, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18 });
		}

		[TestMethod]
		public void SerializeObject_ByteArray_FromByte()
		{
			Do_SerializeObject(NbtTagType.ByteArray, "asdf", new List<byte> { 0x6A, 0x6B, 0x6C, 0x3B }, null, true, new byte[] { 0x07, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x00, 0x00, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B });
		}

		[TestMethod]
		public void SerializeObject_ByteArray_FromNonByte()
		{
			Do_SerializeObject(NbtTagType.ByteArray, "asdf", new List<int> { 0x6A, 0x6B, 0x6C, 0x3B }, null, true, new byte[] { 0x07, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x00, 0x00, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B });
		}

		[TestMethod]
		public void SerializeObject_String()
		{
			Do_SerializeObject(NbtTagType.String, "asdf", "jkl;", null, true, new byte[] { 0x08, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x00, 0x04, 0x6A, 0x6B, 0x6C, 0x3B });
		}

		[TestMethod]
		public void SerializeObject_List_FromList()
		{
			Do_SerializeObject(NbtTagType.List, "asdf", new List<string> { "jkl;1", "jkl;2", "jkl;3", "jkl;4" }, NbtTagType.String, true, new byte[] { 0x09, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x08, 0x00, 0x00, 0x00, 0x04, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x31, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x32, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x33, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x34 });
		}

		[TestMethod]
		public void SerializeObject_List_FromArray()
		{
			Do_SerializeObject(NbtTagType.List, "asdf", new string[] { "jkl;1", "jkl;2", "jkl;3", "jkl;4" }, NbtTagType.String, true, new byte[] { 0x09, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x08, 0x00, 0x00, 0x00, 0x04, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x31, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x32, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x33, 0x00, 0x05, 0x6A, 0x6B, 0x6C, 0x3B, 0x34 });
		}

		[TestMethod]
		public void SerializeObject_Compound()
		{
			Do_SerializeObject(NbtTagType.Compound, "asdf", new TestClass { Double = 3.14159265358979311599796346854E0, Int = 12345, Ignore = "Ignored" }, NbtTagType.String, true, new byte[] { 0x0A, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x06, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18, 0x02, 0x00, 0x04, 0x41, 0x53, 0x44, 0x46, 0x30, 0x39, 0x00 });
		}

		[TestMethod]
		public void SerializeObject_IntArray_FromInt()
		{
			Do_SerializeObject(NbtTagType.IntArray, "asdf", new List<int> { 12345, 1337, 123456789, 55555555 }, null, true, new byte[] { 0x0B, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x30, 0x39, 0x00, 0x00, 0x05, 0x39, 0x07, 0x5B, 0xCD, 0x15, 0x03, 0x4F, 0xB5, 0xE3 });
		}

		[TestMethod]
		public void SerializeObject_IntArray_FromNonInt()
		{
			Do_SerializeObject(NbtTagType.IntArray, "asdf", new List<long> { 12345, 1337, 123456789, 55555555 }, null, true, new byte[] { 0x0B, 0x00, 0x04, 0x61, 0x73, 0x64, 0x66, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x30, 0x39, 0x00, 0x00, 0x05, 0x39, 0x07, 0x5B, 0xCD, 0x15, 0x03, 0x4F, 0xB5, 0xE3 });
		}

		public void Do_SerializeObject(NbtTagType tagType, string name, object value, NbtTagType? elementType, bool writeHeader, byte[] expected)
		{
			// Arrange
			MemoryStream stream = new MemoryStream();
			NbtWriter writer = new NbtWriter(stream);
			InstanceProxy contextProxy = InstanceProxy.For("Konves.Nbt", "Konves.Nbt.Serialization.SerializationContext", writer);

			// Act
			contextProxy.Invoke("SerializeObject", tagType, name, value, elementType, writeHeader);
			byte[] result = stream.ToArray();

			// Assert
			CollectionAssert.AreEqual(expected, result);
		}

		public class TestClass
		{
			[NbtDouble("asdf")]
			public double Double { get; set; }

			[NbtShort("ASDF")]
			public int Int { get; set; }

			[NbtIgnore]
			public string Ignore { get; set; }
		}
	}
}
