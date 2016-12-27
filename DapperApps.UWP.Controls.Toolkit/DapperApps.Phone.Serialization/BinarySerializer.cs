/*
 * Copyright (c) Dapper Apps.  All rights reserved.
 * Use of this source code is subject to the terms of the Dapper Apps license 
 * agreement under which you licensed this sample source code and is provided AS-IS.
 * If you did not accept the terms of the license agreement, you are not authorized 
 * to use this sample source code.  For the terms of the license, please see the 
 * license agreement between you and Dapper Apps.
 *
 * To see the article about this app, visit http://www.dapper-apps.com/DapperToolkit
 */

//namespace DapperApps.Phone.Serialization
//{
//    using System;
//    using System.Collections.Generic;
//    using System.IO;
//    using System.Linq;
//    using System.Reflection;
//    using System.Runtime.Serialization;
//    using Windows.UI.Xaml.Media.Imaging;

//    /// <summary> 
//    /// Represents a class that performs BinarySerialization on a number of different objects.
//    /// Supported objects include:
//    ///     The safe primitive types, e.g. Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, Char, Double, and Single.
//    ///     All of the primitive array types.
//    ///     String type.
//    ///     WriteableBitmap and derived types.
//    ///     IList<primitive> and derived types.
//    ///     IList<string> and derived types.
//    ///     IList<WriteableBitmap> and derived types.
//    /// TODO Write code to recursively serialize IList<Ilist<Ilist...
//    /// TODO Any class that might be serialized must be marked with the SerializableAttribute
//    /// </summary>
//    public class BinarySerializer
//    {
//        // All of the properties with the DataMemberAttribute.
//        private readonly IEnumerable<PropertyInfo> _dmAttributes;

//        // The type of the object being serialized and deserialized.
//        private readonly Type _itemType;

//        /// <summary>
//        /// Constructs a BinarySerializer to serialize objects of the given type.
//        /// Note:
//        ///     If the provided type has no marked DataMemberAttributes, any provided object
//        ///     itself is Serialized/Deserialized. Otherwise, it's DataMemberAttribute marked properties
//        ///     are Serialized/Deserialized.
//        /// </summary>
//        /// <param name="itemType">The type of object to Serialize/Deserialize.</param>
//        public BinarySerializer(Type itemType)
//        {
//            if (null == itemType)
//                throw new ArgumentNullException("itemType");

//            _itemType = itemType;

//            _dmAttributes = from property in _itemType.GetProperties()
//                            where property.GetCustomAttributes(true).Any(x => x is DataMemberAttribute)
//                            select property;
//        }

//        /// <summary>
//        /// Serializes the provided object type into the given stream.
//        /// If the object's type has no marked DataMemberAttributes, the object itself
//        /// is Serialized. Otherwise, it's DataMemberAttribute marked properties
//        /// are Serialized.
//        /// </summary>
//        /// <param name="stream">The stream to Serialize the object to.</param>
//        /// <param name="itemValue">The object to Serialize to the stream.</param>
//        public void Serialize(Stream stream, object itemValue)
//        {
//            if (null == itemValue)
//                throw new ArgumentNullException("item");

//            var bw = new BinaryWriter(stream);

//            var hasAttributes = false;
//            foreach (var property in _dmAttributes)
//            {
//                // The compile-time type of the property
//                var propertyType = property.PropertyType;

//                // The runtime-value of the property
//                var propertyValue = property.GetValue(itemValue);

//                Write(bw, propertyType, propertyValue);

//                hasAttributes = true;
//            }

//            // If the item has no serializable attributes, the try to write the object itself.
//            if (!hasAttributes)
//            {
//                Write(bw, _itemType, itemValue);
//            }

//            bw.Flush();
//        }

//        private void Write(BinaryWriter bw, Type type, object value)
//        {
//            // TODO indexed properties
//            // bw.Write(byte[] buffer, int index, int count);
//            // bw.Write(char[] chars, int index, int count);

//            // Compile-time Interface types of the property.
//            var interfaceTypes = type.GetInterfaces();

//            if (!type.IsGenericType)
//            {
//                if (TryWritePrimitives(bw, type, value))
//                {
//                }
//                else if (TryWriteArrayPrimitives(bw, type, value))
//                {
//                }
//                else if (type == typeof(string))
//                {
//                    var isNull = (null == value);
//                    bw.Write(isNull);
//                    if (!isNull)
//                    {
//                        bw.Write((string)value);
//                    }
//                }
//                else if (type == typeof(WriteableBitmap) || interfaceTypes.Contains(typeof(WriteableBitmap)))
//                {
//                    WriteWriteableBitmap(bw, (WriteableBitmap)value);
//                }
//                else
//                {
//                    throw new NotImplementedException("Serialization for the marked properties, or provided object is not supported.");
//                }
//            }
//            else
//            {
//                if (TryWriteIListPrimitives(bw, type, value))
//                {
//                }
//                else if (type == typeof(IList<string>) || interfaceTypes.Contains(typeof(IList<string>)))
//                {
//                    WriteStringIList(bw, (IList<string>)value);
//                }
//                else if (type == typeof(IList<WriteableBitmap>) || interfaceTypes.Contains(typeof(IList<WriteableBitmap>)))
//                {
//                    WriteWriteableBitmapIList(bw, (IList<WriteableBitmap>)value);
//                }
//                else
//                {
//                    throw new NotImplementedException("Serialization for the marked properties, or provided object is not supported.");
//                }
//            }
//        }

//        private bool TryWrite<T>(BinaryWriter bw, Type type, object value)
//        {
//            if (type == typeof(T))
//            {
//                dynamic tVal = (T)value;
//                bw.Write(tVal);
//                return true;
//            }
//            return false;
//        }

//        private bool TryWritePrimitives(BinaryWriter bw, Type type, object value)
//        {
//            if (TryWrite<bool>(bw, type, value))
//                return true;
//            else if (TryWrite<byte>(bw, type, value))
//                return true;
//            else if (TryWrite<char>(bw, type, value))
//                return true;
//            else if (TryWrite<decimal>(bw, type, value))
//                return true;
//            else if (TryWrite<double>(bw, type, value))
//                return true;
//            else if (TryWrite<float>(bw, type, value))
//                return true;
//            else if (TryWrite<int>(bw, type, value))
//                return true;
//            else if (TryWrite<long>(bw, type, value))
//                return true;
//            else if (TryWrite<sbyte>(bw, type, value))
//                return true;
//            else if (TryWrite<short>(bw, type, value))
//                return true;
//            else if (TryWrite<uint>(bw, type, value))
//                return true;
//            else if (TryWrite<ulong>(bw, type, value))
//                return true;
//            else
//                if (TryWrite<ushort>(bw, type, value))
//                    return true;
//            return false;
//        }

//        private bool TryWriteArray<T>(BinaryWriter bw, Type type, object value)
//        {
//            if (type == typeof(T))
//            {
//                dynamic tVal = (T)value;
//                var length = tVal.Length;
//                if (length == 0)
//                {
//                    bw.Write(0);
//                }
//                else
//                {
//                    bw.Write(length);
//                    foreach (var item in tVal)
//                    {
//                        bw.Write(item);
//                    }
//                }
//                return true;
//            }
//            return false;
//        }

//        private bool TryWriteArrayPrimitives(BinaryWriter bw, Type type, object value)
//        {
//            if (TryWriteArray<bool[]>(bw, type, value))
//                return true;
//            else if (TryWriteArray<byte[]>(bw, type, value))
//                return true;
//            else if (TryWriteArray<char[]>(bw, type, value))
//                return true;
//            else if (TryWriteArray<decimal[]>(bw, type, value))
//                return true;
//            else if (TryWriteArray<double[]>(bw, type, value))
//                return true;
//            else if (TryWriteArray<float[]>(bw, type, value))
//                return true;
//            else if (TryWriteArray<int[]>(bw, type, value))
//                return true;
//            else if (TryWriteArray<long[]>(bw, type, value))
//                return true;
//            else if (TryWriteArray<sbyte[]>(bw, type, value))
//                return true;
//            else if (TryWriteArray<short[]>(bw, type, value))
//                return true;
//            else if (TryWriteArray<uint[]>(bw, type, value))
//                return true;
//            else if (TryWriteArray<ulong[]>(bw, type, value))
//                return true;
//            else
//                if (TryWriteArray<ushort[]>(bw, type, value))
//                    return true;
//            return false;
//        }

//        private bool TryWriteIList<T, TLIST>(BinaryWriter bw, Type type, object value)
//        {
//            // Compile-time Interface types of the property.
//            var propertyInterfaceTypes = type.GetInterfaces();

//            if (type == typeof(TLIST) || propertyInterfaceTypes.Contains(typeof(TLIST)))
//            {
//                dynamic list = (TLIST)value;
//                var isNull = (null == list);
//                bw.Write(isNull);
//                if (!isNull)
//                {
//                    var count = list.Count;
//                    bw.Write(count);
//                    for (int i = 0; i < count; i++)
//                    {
//                        dynamic item = (T)list[i];
//                        bw.Write(item);
//                    }
//                    return true;
//                }
//            }
//            return false;
//        }

//        private bool TryWriteIListPrimitives(BinaryWriter bw, Type type, object value)
//        {
//            if (TryWriteIList<bool, IList<bool>>(bw, type, value))
//                return true;
//            else if (TryWriteIList<byte, IList<byte>>(bw, type, value))
//                return true;
//            else if (TryWriteIList<char, IList<char>>(bw, type, value))
//                return true;
//            else if (TryWriteIList<decimal, IList<decimal>>(bw, type, value))
//                return true;
//            else if (TryWriteIList<double, IList<double>>(bw, type, value))
//                return true;
//            else if (TryWriteIList<float, IList<float>>(bw, type, value))
//                return true;
//            else if (TryWriteIList<int, IList<int>>(bw, type, value))
//                return true;
//            else if (TryWriteIList<long, IList<long>>(bw, type, value))
//                return true;
//            else if (TryWriteIList<sbyte, IList<sbyte>>(bw, type, value))
//                return true;
//            else if (TryWriteIList<short, IList<short>>(bw, type, value))
//                return true;
//            else if (TryWriteIList<uint, IList<uint>>(bw, type, value))
//                return true;
//            else if (TryWriteIList<ulong, IList<ulong>>(bw, type, value))
//                return true;
//            else
//                if (TryWriteIList<ushort, IList<ushort>>(bw, type, value))
//                    return true;
//            return false;
//        }

//        private void WriteStringIList(BinaryWriter bw, IList<string> value)
//        {
//            var isNull = (null == value);
//            bw.Write(isNull);
//            if (!isNull)
//            {
//                var count = value.Count;
//                bw.Write(count);
//                foreach (var str in value)
//                {
//                    var strIsNull = (null == str);
//                    bw.Write(strIsNull);
//                    if (!strIsNull)
//                    {
//                        bw.Write(str);
//                    }
//                }
//            }
//        }

//        private void WriteWriteableBitmap(BinaryWriter bw, WriteableBitmap value)
//        {
//            if (null != value)
//            {
//                int pixelWidth = value.PixelWidth;
//                int pixelHeight = value.PixelHeight;
//                if (pixelWidth > 0 && pixelHeight > 0)
//                {
//                    bw.Write(false);
//                    bw.Write(pixelWidth);
//                    bw.Write(pixelHeight);
//                    using (var memStream = new MemoryStream())
//                    {
//                        value.SaveJpeg(memStream, pixelWidth, pixelHeight, 0, 100);
//                        var buffer = memStream.GetBuffer();
//                        var length = buffer.Length;
//                        bw.Write(length);
//                        bw.Write(buffer);
//                    }
//                    return;
//                }
//            }
//            bw.Write(true);
//        }

//        private void WriteWriteableBitmapIList(BinaryWriter bw, IList<WriteableBitmap> value)
//        {
//            var isNull = (null == value);
//            bw.Write(isNull);
//            if (!isNull)
//            {
//                var count = value.Count;
//                bw.Write(count);
//                foreach (var wb in value)
//                {
//                    WriteWriteableBitmap(bw, wb);
//                }
//            }
//        }

//        /// <summary>
//        /// Deserializes the object type denoted by this BinarySerializer from the given stream.
//        /// If the object type has no marked DataMemberAttributes, the object itself
//        /// is Deserialized. Otherwise, it's DataMemberAttribute marked properties
//        /// are Deserialized.
//        /// </summary>
//        /// <param name="stream">The stream to Deserialize the object from.</param>
//        /// <returns>The object Deserialized from the stream.</returns>
//        public object Deserialize(Stream stream)
//        {
//            if (null == stream)
//                throw new ArgumentNullException("stream");

//            var br = new BinaryReader(stream);

//            var deserializedObject = Activator.CreateInstance(_itemType);

//            var hasAttributes = false;
//            object result = null;
//            foreach (var property in _dmAttributes)
//            {
//                // The type of the property
//                var propertyType = property.PropertyType;

//                Read(br, propertyType, out result);
//                property.SetValue(deserializedObject, result, null);

//                hasAttributes = true;
//            }

//            // If the item has no deserializable attributes, the try to read the object itself.
//            if (!hasAttributes)
//            {
//                Read(br, _itemType, out result);
//                deserializedObject = result;
//            }

//            br.Close();
//            return deserializedObject;
//        }

//        private void Read(BinaryReader br, Type type, out object result)
//        {
//            // TODO indexed properties
//            //var indexedParams = property.GetIndexParameters();
//            //var paramsLength = indexedParams.Length;

//            // Interface types of the property.
//            var interfaceTypes = type.GetInterfaces();

//            if (!type.IsGenericType)
//            {
//                if (TryReadPrimitives(br, type, out result))
//                {
//                }
//                else if (TryReadArrayPrimitives(br, type, out result))
//                {
//                }
//                else if (type == typeof(string))
//                {
//                    var isNull = br.ReadBoolean();
//                    result = (!isNull) ? (object)br.ReadString() : null;
//                }
//                else if (type == typeof(WriteableBitmap))
//                {
//                    ReadWriteableBitmap(br, out result);
//                }
//                else
//                {
//                    throw new NotImplementedException("Serialization for the marked properties, or provided object is not supported.");
//                }
//            }
//            else
//            {
//                if (TryReadIListPrimitives(br, type, out result))
//                {
//                }
//                else if (type == typeof(IList<string>) || interfaceTypes.Contains(typeof(IList<string>)))
//                {
//                    ReadStringIList(br, out result);
//                }
//                else if (type == typeof(IList<WriteableBitmap>) || interfaceTypes.Contains(typeof(IList<WriteableBitmap>)))
//                {
//                    ReadWriteableBitmapIList(br, out result);
//                }
//                else
//                {
//                    throw new NotImplementedException("Serialization for the marked properties, or provided object is not supported.");
//                }
//            }
//        }

//        private bool TryReadPrimitives(BinaryReader br, Type type, out object result)
//        {
//            if (type == typeof(bool))
//            {
//                result = (object)br.ReadBoolean();
//                return true;
//            }
//            else if (type == typeof(byte))
//            {
//                result = (object)br.ReadByte();
//                return true;
//            }
//            else if (type == typeof(char))
//            {
//                result = (object)br.ReadChar();
//                return true;
//            }
//            else if (type == typeof(decimal))
//            {
//                result = (object)br.ReadDecimal();
//                return true;
//            }
//            else if (type == typeof(double))
//            {
//                result = (object)br.ReadDouble();
//                return true;
//            }
//            else if (type == typeof(float))
//            {
//                result = (object)br.ReadSingle();
//                return true;
//            }
//            else if (type == typeof(int))
//            {
//                result = (object)br.ReadInt32();
//                return true;
//            }
//            else if (type == typeof(long))
//            {
//                result = (object)br.ReadInt64();
//                return true;
//            }
//            else if (type == typeof(sbyte))
//            {
//                result = (object)br.ReadSByte();
//                return true;
//            }
//            else if (type == typeof(short))
//            {
//                result = (object)br.ReadInt16();
//                return true;
//            }
//            else if (type == typeof(uint))
//            {
//                result = (object)br.ReadUInt32();
//                return true;
//            }
//            else if (type == typeof(ulong))
//            {
//                result = (object)br.ReadUInt64();
//                return true;
//            }
//            else
//            {
//                if (type == typeof(ushort))
//                {
//                    result = (object)br.ReadUInt16();
//                    return true;
//                }
//            }
//            result = null;
//            return false;
//        }

//        private bool TryReadArray<T>(BinaryReader br, Type type, Func<T> del, out object result)
//        {
//            if (type == typeof(T[]))
//            {
//                var length = br.ReadInt32();
//                var list = new T[length];
//                var index = 0;
//                while (length > 0)
//                {
//                    list[index++] = del();
//                    index--;
//                }
//                result = list;
//                return true;
//            }
//            result = null;
//            return false;
//        }

//        private bool TryReadArrayPrimitives(BinaryReader br, Type type, out object result)
//        {
//            if (TryReadArray<bool>(br, type, () => { return br.ReadBoolean(); }, out result))
//            {
//                return true;
//            }
//            else if (TryReadArray<byte>(br, type, () => { return br.ReadByte(); }, out result))
//            {
//                return true;
//            }
//            else if (TryReadArray<char>(br, type, () => { return br.ReadChar(); }, out result))
//            {
//                return true;
//            }
//            else if (TryReadArray<decimal>(br, type, () => { return br.ReadDecimal(); }, out result))
//            {
//                return true;
//            }
//            else if (TryReadArray<double>(br, type, () => { return br.ReadDouble(); }, out result))
//            {
//                return true;
//            }
//            else if (TryReadArray<float>(br, type, () => { return br.ReadSingle(); }, out result))
//            {
//                return true;
//            }
//            else if (TryReadArray<int>(br, type, () => { return br.ReadInt32(); }, out result))
//            {
//                return true;
//            }
//            else if (TryReadArray<long>(br, type, () => { return br.ReadInt64(); }, out result))
//            {
//                return true;
//            }
//            else if (TryReadArray<sbyte>(br, type, () => { return br.ReadSByte(); }, out result))
//            {
//                return true;
//            }
//            else if (TryReadArray<short>(br, type, () => { return br.ReadInt16(); }, out result))
//            {
//                return true;
//            }
//            else if (TryReadArray<uint>(br, type, () => { return br.ReadUInt32(); }, out result))
//            {
//                return true;
//            }
//            else if (TryReadArray<ulong>(br, type, () => { return br.ReadUInt64(); }, out result))
//            {
//                return true;
//            }
//            else if (TryReadArray<ushort>(br, type, () => { return br.ReadUInt16(); }, out result))
//            {
//                return true;
//            }
//            else if ((type == typeof(char[])))
//            {
//                var length = br.ReadInt32();
//                result = (object)br.ReadChars(length);
//                return true;
//            }
//            else
//            {
//                if ((type == typeof(byte[])))
//                {
//                    var length = br.ReadInt32();
//                    result = (object)br.ReadBytes(length);
//                    return true;
//                }
//            }
//            result = null;
//            return false;
//        }

//        private bool TryReadIList<T, TLIST>(BinaryReader br, Type type, Func<T> del, out object result)
//        {
//            // Interface types of the property.
//            var interfaceTypes = type.GetInterfaces();

//            if (type == typeof(TLIST) || interfaceTypes.Contains(typeof(TLIST)))
//            {
//                var isNull = br.ReadBoolean();
//                if (!isNull)
//                {
//                    var list = new List<T>();
//                    var count = br.ReadInt32();
//                    var index = count;
//                    while (index > 0)
//                    {
//                        list.Add(del());
//                        index--;
//                    }
//                    result = list;
//                    return true;
//                }
//                else
//                {
//                    result = null;
//                }
//            }
//            result = null;
//            return false;
//        }

//        private bool TryReadIListPrimitives(BinaryReader br, Type type, out object result)
//        {
//            if (TryReadIList<bool, IList<bool>>(br, type, () => { return br.ReadBoolean(); }, out result))
//                return true;
//            else if (TryReadIList<byte, IList<byte>>(br, type, () => { return br.ReadByte(); }, out result))
//                return true;
//            else if (TryReadIList<char, IList<char>>(br, type, () => { return br.ReadChar(); }, out result))
//                return true;
//            else if (TryReadIList<decimal, IList<decimal>>(br, type, () => { return br.ReadDecimal(); }, out result))
//                return true;
//            else if (TryReadIList<double, IList<double>>(br, type, () => { return br.ReadDouble(); }, out result))
//                return true;
//            else if (TryReadIList<float, IList<float>>(br, type, () => { return br.ReadSingle(); }, out result))
//                return true;
//            else if (TryReadIList<int, IList<int>>(br, type, () => { return br.ReadInt32(); }, out result))
//                return true;
//            else if (TryReadIList<long, IList<long>>(br, type, () => { return br.ReadInt64(); }, out result))
//                return true;
//            else if (TryReadIList<sbyte, IList<sbyte>>(br, type, () => { return br.ReadSByte(); }, out result))
//                return true;
//            else if (TryReadIList<short, IList<short>>(br, type, () => { return br.ReadInt16(); }, out result))
//                return true;
//            else if (TryReadIList<uint, IList<uint>>(br, type, () => { return br.ReadUInt32(); }, out result))
//                return true;
//            else if (TryReadIList<ulong, IList<ulong>>(br, type, () => { return br.ReadUInt64(); }, out result))
//                return true;
//            else
//                if (TryReadIList<ushort, IList<ushort>>(br, type, () => { return br.ReadUInt16(); }, out result))
//                    return true;
//            result = null;
//            return false;
//        }

//        private void ReadStringIList(BinaryReader br, out object result)
//        {
//            var isNull = br.ReadBoolean();
//            if (!isNull)
//            {
//                var list = new List<string>();
//                var count = br.ReadInt32();
//                var index = count;
//                while (index > 0)
//                {
//                    var strIsNull = br.ReadBoolean();
//                    list.Add((!strIsNull) ? br.ReadString() : null);
//                    index--;
//                }
//                result = list;
//            }
//            else
//            {
//                result = null;
//            }
//        }

//        private void ReadWriteableBitmap(BinaryReader br, out object result)
//        {
//            var isNull = br.ReadBoolean();
//            if (!isNull)
//            {
//                var pixelWidth = br.ReadInt32();
//                var pixelHeight = br.ReadInt32();
//                var length = br.ReadInt32();
//                var buffer = br.ReadBytes(length);
//                var wb = new WriteableBitmap(pixelWidth, pixelHeight);
//                using (var memStream = new MemoryStream(buffer))
//                {
//                    wb.LoadJpeg(memStream);
//                }
//                result = wb;
//            }
//            else
//            {
//                result = null;
//            }
//        }

//        private void ReadWriteableBitmapIList(BinaryReader br, out object result)
//        {
//            var isNull = br.ReadBoolean();
//            if (!isNull)
//            {
//                var list = new List<WriteableBitmap>();
//                var count = br.ReadInt32();
//                var index = count;
//                while (index > 0)
//                {
//                    object wb;
//                    ReadWriteableBitmap(br, out wb);
//                    list.Add((WriteableBitmap)wb);
//                    index--;
//                }
//                result = list;
//            }
//            else
//            {
//                result = null;
//            }
//        }
//    }
//}