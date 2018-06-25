/*
Author:		Augustine
History:	11.06.2015 创建
note   :    基于模板的工厂方法组件,曾为极光所有项目服务了5年.目前我写个c#版本的，我为北京西山居写的最后的代码了。。。。。
 */

/*
例子:

class A
{ 
}

class B : A
{ 

}

class C : A
{ 
    
}


class D : B
{ 
    
}
 
            例子1 用int做key
            CTFactoryMethod<A,int> TestFactoryMethod = new CTFactoryMethod<A,int>();
            
            // 注册类型和零件的对应关系，初始化使用
            TestFactoryMethod.RegisterClass<A>(1); // 1 造 A的对象
            TestFactoryMethod.RegisterClass<B>(2); // 2 造 B的对象
            TestFactoryMethod.RegisterClass<C>(3); // 3 造 C的对象
            TestFactoryMethod.RegisterClass<D>(4); // 4 造 D的对象

            // 应用层使用
            A a1 = TestFactoryMethod.CreateObject(1); //  A obj
            A a2 = TestFactoryMethod.CreateObject(1); //  A obj

            A b = TestFactoryMethod.CreateObject(2); //  B obj
            A c = TestFactoryMethod.CreateObject(3); //  C obj

            A d = TestFactoryMethod.CreateObject(4); //  D obj

            例子2 用string做key
             
            // 应用层使用
            A a1 = TestFactoryMethod.CreateObject("a"); //  A obj
            A a2 = TestFactoryMethod.CreateObject("a"); //  A obj

            A b = TestFactoryMethod.CreateObject("b"); //  B obj
            A c = TestFactoryMethod.CreateObject("c"); //  C obj

            A d = TestFactoryMethod.CreateObject("d"); 
 
 
 */


using System.Collections.Generic;

namespace GameFramework
{
    public abstract class CBaseTypeObjectFactoryUnit<BaseObjType, FactoryType>
         where BaseObjType : class
    {
        public CBaseTypeObjectFactoryUnit(FactoryType tType)
        {
            m_Type = tType;
        }
        public abstract BaseObjType createObject();
        public FactoryType m_Type { get; protected set; }
    }


    public class CTypeObjectFactoryUnit<ObjType, BaseObjType, FactoryType> : CBaseTypeObjectFactoryUnit<BaseObjType, FactoryType>
        where BaseObjType : class
        where ObjType : BaseObjType, new()
    {
        public CTypeObjectFactoryUnit(FactoryType tType)
            : base(tType)
        {

        }

        public override BaseObjType createObject()
        {
            return new ObjType();
        }

    };

    public class CTFactoryMethod<BaseObjType, FactoryType>
        where BaseObjType : class
    {
        public CTFactoryMethod()
        {
            m_FactoryDict = new Dictionary<FactoryType, CBaseTypeObjectFactoryUnit<BaseObjType, FactoryType>>();
        }

        public bool RegisterClass<ObjType>(FactoryType tType)
            where ObjType : BaseObjType, new()
        {
            CTypeObjectFactoryUnit<ObjType, BaseObjType, FactoryType> FactoryUnit = new CTypeObjectFactoryUnit<ObjType, BaseObjType, FactoryType>(tType);
            return RegisterClassImp(FactoryUnit);
        }

        public void UnRegisterClass(FactoryType tType)
        {
            CBaseTypeObjectFactoryUnit<BaseObjType, FactoryType> FactoryUnit;
            if (m_FactoryDict.TryGetValue(tType, out FactoryUnit))
            {
                m_FactoryDict.Remove(tType);
            }
        }

        public void UnRegisterAllClass()
        {
            m_FactoryDict.Clear();
        }

        public BaseObjType CreateObject(FactoryType tType)
        {
            BaseObjType Ret = default(BaseObjType);
            CBaseTypeObjectFactoryUnit<BaseObjType, FactoryType> FactoryUnit;
            if (m_FactoryDict.TryGetValue(tType, out FactoryUnit))
            {
                Ret = FactoryUnit.createObject();
            }
            return Ret;
        }

        protected bool RegisterClassImp(CBaseTypeObjectFactoryUnit<BaseObjType, FactoryType> FactoryUnit)
        {
            m_FactoryDict[FactoryUnit.m_Type] = FactoryUnit;
            return true;
        }
        protected Dictionary<FactoryType, CBaseTypeObjectFactoryUnit<BaseObjType, FactoryType>> m_FactoryDict;
    }

}