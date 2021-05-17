using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaUIHelp.Core
{
    /// <summary>
    /// 进程间通讯类型
    /// </summary>

    public enum ProgressCommunication
    {
        None = 0x00,

        /// <summary>
        /// 本客户端内部通信(事件的内核)
        /// </summary>
        InnerCommunication = 0x01,

        /// <summary>
        /// 客户端间通信(例如手机A打手机B 不支持的)
        /// </summary>
        ClientToClient = 0x02,

        /// <summary>
        /// 客户端到服务器通信(手机A联系总部)
        /// </summary>
        ClientToServer = 0x04,

        /// <summary>
        /// 服务端到客户端的通信(例如学校广播 所有同学注意了 你注意不注意是你的事)
        /// </summary>
        ServerToClient = 0x08,
    }

    /// <summary>
    /// MessageAggregator(消息--实现发布订阅模式)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageAggregator<T>
    {
        private static Action<T> m_MessageAction;

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="action"></param>
        public static void Subscribe(Action<T> action)
        {
            m_MessageAction += action;
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="action"></param>
        public static void UnSubscribe(Action<T> action)
        {
            m_MessageAction -= action;
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="progressCommunication"></param>
        public static void Publish(T t, ProgressCommunication progressCommunication = ProgressCommunication.InnerCommunication)
        {
            switch (progressCommunication)
            {
                case ProgressCommunication.InnerCommunication:
                    DealMessage(t);
                    break;
                case ProgressCommunication.ClientToServer:
                    //Todo 岳志峰 是否需要进程间通信待定(进程间通信之前只实现了Windows,虚拟机如何实现待定 考虑管道通信)
                    break;
            }
        }

        /// <summary>
        /// 处理接收的数据(不能删,不能重命名，虽然没有引用,通过反射访问这个方法)
        /// </summary>
        /// <param name="t"></param>
        private static void DealMessage(T t)
        {
            if (m_MessageAction != null)
            {
                m_MessageAction.Invoke(t);
            }
        }
    }
}
