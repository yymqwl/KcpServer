//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: proto/Login.proto
namespace Game_Msg
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Req_Ping")]
  public partial class Req_Ping : global::ProtoBuf.IExtensible
  {
    public Req_Ping() {}
    
    private int _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private string _des;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"des", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string des
    {
      get { return _des; }
      set { _des = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}