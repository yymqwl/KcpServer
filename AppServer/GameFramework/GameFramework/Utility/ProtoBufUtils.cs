/****************************************************************************
Copyright (c) 2013-2015 scutgame.com

http://www.scutgame.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime;
using ProtoBuf;
using ProtoBuf.Meta;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// Protobuf序列化与反序列化
    /// </summary>
    public static class ProtoBufUtils
    {


        private static RuntimeTypeModel typeModel;//TypeModel.Create();




        static ProtoBufUtils()
        {
            Initialize();
        }

        public static void Initialize()
        {
            typeModel = TypeModel.Create();
            typeModel.UseImplicitZeroDefaults = false;

            MetaType metatype = null;
            metatype = typeModel.Add(typeof(Vector3), true);
            metatype.AddField(1, "x");
            metatype.AddField(2, "y");
            metatype.AddField(3, "z");

            metatype = typeModel.Add(typeof(Vector2), true);
            metatype.AddField(1, "x");
            metatype.AddField(2, "y");

            metatype = typeModel.Add(typeof(Color), true);
            metatype.AddField(1, "a");
            metatype.AddField(2, "b");
            metatype.AddField(3, "g");
            metatype.AddField(4, "r");

            
            metatype = typeModel.Add(typeof(Resolution), true);
            metatype.AddField(1, "width");
            metatype.AddField(2, "height");
            metatype.AddField(3, "refreshRate");


        }





        public static Byte[] ProtobufSerialize(object obj)
        {
            return Serialize(obj);

        }

        public static Byte[] ProtobufSerialize(object obj, MemoryStream ms)
        {
            /*
            if (ms != null)
            {
                typeModel.Serialize(ms, obj);
                Byte[] bytes = ms.ToArray();
                return bytes;
            }*/
            return null;
        }
        public static void ProtobufSerializeByStream(object obj, MemoryStream ms)
        {
            if (ms != null)
            {
                Serializer.Serialize(ms, obj);
            }
        }

        public static T ProtobufDeserializeByStream<T>(MemoryStream ms)
        {
            
            try
            {
                return Serializer.Deserialize<T>(ms);
            }
            catch (Exception ex)
            {
                Type tp = typeof(T);
                throw new GameFrameworkException("ProtoBuf deserialize type: " + tp.FullName + " error" + ex.ToString());

                //AppModuleManager.Instance.LogModule.Log.Error("ProtoBuf deserialize type: " + tp.FullName + " error" + ex.ToString());
            }
            //return default(T);
         
        }


        public static object ProtobufDeserializeByStream(MemoryStream ms, Type tp)
        {


            try
            {

              
                return typeModel.Deserialize(ms, null, tp);
                


            }
            catch (Exception ex)
            {

                throw new GameFrameworkException("ProtoBuf deserialize type: " + tp.FullName + " error" + ex.ToString());
                //AppModuleManager.Instance.LogModule.Log.Error("ProtoBuf deserialize type: " + tp.FullName + " error" + ex.ToString());
            }

            //return null;

        }

        public static T ProtobufDeserialize<T>(byte[]  bufs)
        {
      

            
            try
            {

                using (var memory = new MemoryStream(bufs))
                {
                    return  (T)typeModel.Deserialize(memory, null, typeof(T));
                    //return typeModel.Deserialize<T>(memory);
                }

               
            }
            catch (Exception ex)
            {
                Type tp = typeof(T);
                throw new GameFrameworkException("ProtoBuf deserialize type: " + tp.FullName + " error" + ex.ToString());
            }
            //return default(T);

            
        }



        private static Byte[] Serialize(object obj)
        {
            using (var memory = new MemoryStream())
            {
                typeModel.Serialize(memory, obj);
                Byte[] bytes = memory.ToArray();
                return bytes;
            }
        }



        /// <summary>
        /// 动态绑定实体对象的子类类型
        /// </summary>
        /// <param name="assembly"></param>
        /*
        public static void LoadProtobufType(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }
            Dictionary<Type, int> typeMemberTags = new Dictionary<Type, int>();
            var types = assembly.GetTypes().Where(p => p.GetCustomAttributes(typeof(ProtoContractAttribute), false).Count() > 0).ToList();
            for (int i = 0; i < types.Count; i++)
            {
                var myEntity = types[i];

                //获得所有属性
                var Properties = myEntity.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase).Where(p => p.GetCustomAttributes(typeof(ProtoMemberAttribute), true).Count() > 0).ToList();
                var Fields = myEntity.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase).Where(p => p.GetCustomAttributes(typeof(ProtoMemberAttribute), true).Count() > 0).ToList();
                try
                {
                    if (typeModel.CanSerializeContractType(myEntity))
                    {
                        var metaType = typeModel.Add(myEntity, true);
                        int maxMemberTag;
                        LoadProtoTypeMember(myEntity, Properties, metaType, Fields, out maxMemberTag);
                        typeMemberTags[myEntity] = maxMemberTag;

                        //增加继承的子类
                        if (myEntity.BaseType != null)
                        {
                            var parentMetaType = typeModel[myEntity.BaseType];
                            if (parentMetaType != null && typeMemberTags.ContainsKey(myEntity.BaseType))
                            {
                                var parentMemberTag = typeMemberTags[myEntity.BaseType] + 1;
                                parentMetaType.AddSubType(parentMemberTag, myEntity);
                                typeMemberTags[myEntity.BaseType] = parentMemberTag;
                            }
                        }
                    }
                }
                //忽略异常
                catch (Exception e)
                {
                    AppModuleManager.Instance.LogModule.Log.Error(string.Format( "Loading protobuf type \"{0}\" error:{1}", myEntity.FullName, e));
                }

            }

        }*/
        /*
        private static void LoadProtoTypeMember(Type myEntity, List<PropertyInfo> Properties, MetaType metaType, List<FieldInfo> Fields, out int maxMemberTag)
        {
            int memberTag = 0;
            Properties.ForEach((o) =>
            {
                try
                {
                    var fieldNumber = (o.GetCustomAttributes(typeof(ProtoMemberAttribute), false)[0] as ProtoMemberAttribute).Tag;
                    if (memberTag < fieldNumber)
                    {
                        memberTag = fieldNumber;
                    }
                    if (metaType[fieldNumber] == null)
                    {
                        metaType.Add(fieldNumber, o.Name);
                    }
                }
                //忽略异常
                catch (Exception ex)
                {
                    AppModuleManager.Instance.LogModule.Log.Error( string.Format( "Loading protobuf type \"{0}.{1}\" property error:{2}", myEntity.FullName, o.Name, ex ));
                }
            });
            Fields.ForEach((o) =>
            {
                try
                {
                    var fieldNumber = (o.GetCustomAttributes(typeof(ProtoMemberAttribute), false)[0] as ProtoMemberAttribute).Tag;
                    if (memberTag < fieldNumber)
                    {
                        memberTag = fieldNumber;
                    }
                    if (metaType[fieldNumber] == null)
                    {
                        metaType.AddField(fieldNumber, o.Name);
                    }
                }
                //忽略异常
                catch (Exception ex)
                {
                    AppModuleManager.Instance.LogModule.Log.Error(string.Format("Loading protobuf type \"{0}.{1}\" field error:{2}", myEntity.FullName, o.Name, ex));
                }
            });
            maxMemberTag = memberTag;
        }*/
    }
}