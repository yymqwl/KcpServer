using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{
    public class Node<T>
    {
        public virtual string Name
        {
            get;
            set;
        }
        public virtual Node<T> Parent
        {
          
            get
            {
                return this.m_parent;
            }
        }


        LinkedList<Node<T>> m_childs;
        Node<T> m_parent;

        public Node()
        {
            Reset();
        }


        public virtual void Reset()
        {
            m_childs = new LinkedList<Node<T>>();
            m_parent = null;
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="node"></param>
        public virtual void Add(Node<T> node)
        {
            if (node != null && node.Parent == null)
            {
                node.SetParent(this);
                m_childs.AddLast(node);
                node.OnEnter();
            }
            else
            {
                DebugHandler.LogError("node.Parent not null");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void SetParent(Node<T> node)
        {
            this.m_parent = node;
        }
        /// <summary>
        /// 删除子节点
        /// </summary>
        public virtual  void Remove(Node<T> node)
        {
            if (m_childs.Contains(node))
            {
                m_childs.Remove(node);
                node.SetParent(null);
                node.OnExit();
            }
            else
            {
                DebugHandler.LogError("child not contain node");
            }

        }
        /// <summary>
        /// 移除自身
        /// </summary>
        /// 
        public void RemoveSelf()
        {
            if (Parent == null)
            {
                DebugHandler.LogError(" null  parent");
                return;
            }
            Parent.Remove(this);
        }

        public LinkedList<Node<T>> GetChilds()
        {
            return m_childs;
        }



        public Node<T> GetFirstChild()
        {
            return m_childs.First.Value;
        }

        public Node<T> GetChildByName(string name)
        {
            return m_childs.Find_Lk_First((x) => { return x.Name == name; });
        }

        public virtual void OnEnter()
        {

        }
        public virtual void OnExit()
        {

        }


        public virtual void OnUpdate(float elapsedTime)
        {

        }

    }
}