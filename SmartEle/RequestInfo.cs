using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEle
{
    /// <summary>
    /// 电梯操作类型
    /// </summary>
    public enum OperType
    {
        /// <summary>
        /// 进电梯 
        /// </summary>
        OtIn = 0,
        /// <summary>
        /// 出电梯
        /// </summary>
        OtOut = 1
    }

    /// <summary>
    /// 请求信息类
    /// </summary>
    public class RequestInfo
    {
        /// <summary>
        /// 请求的用户
        /// </summary>
        public User RequestUser;
        /// <summary>
        /// 类型
        /// </summary>
        public OperType Oper;

        /// <summary>
        /// 请求目的楼层
        /// </summary>
        public int Floor;

        /// <summary>
        /// 是否计算过
        /// </summary>
        public bool HasCom;

        public RequestInfo(User user, OperType oper, int floor)
        {
            RequestUser = user;
            Oper = oper;
            Floor = floor;
            HasCom = false;
        }
    }
}
