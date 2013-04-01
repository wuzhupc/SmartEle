namespace SmartEle
{
    /// <summary>
    /// 用户当前状态
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// 用户无请求状态
        /// </summary>
        UsNone = 0,
        /// <summary>
        /// 用户正在等待电梯
        /// </summary>
        UsWait = 1,
        /// <summary>
        /// 用户在电梯中
        /// </summary>
        UsInEle = 2
    }
    /// <summary>
    /// 用户类
    /// </summary>
    public class User
    {
        private readonly int _userid;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId{
            get { return _userid; }
        }

        /// <summary>
        /// 用户起始楼层
        /// </summary>
        public int FromFloor ;

        /// <summary>
        /// 用户当前楼层
        /// </summary>
        public int NowFloor;

        /// <summary>
        /// 目的楼层
        /// </summary>
        public int ToFloor;

        /// <summary>
        /// 用户使用的电梯组
        /// </summary>
        public ElevatorBank InElevatorBank;

        /// <summary>
        /// 用户当前状态
        /// </summary>
        public UserStatus Status;

        /// <summary>
        /// 用户正在等待或在某个电梯
        /// </summary>
        public Ele InOrWaitEle;

        /// <summary>
        /// 目前用户已经用时
        /// </summary>
        public int UseTime;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="from"></param>
        /// <param name="elevatorBank"></param>
        public User(int id,int from,ElevatorBank elevatorBank)
        {
            _userid = id;
            Status = UserStatus.UsNone;
            FromFloor = from;
            InElevatorBank = elevatorBank;
        }

        /// <summary>
        /// 用户请求到达楼层（在那个楼层，并到那个楼层
        /// </summary>
        /// <param name="to">用户目的楼层</param>
        /// <returns>返回空字符串表示请求成功，非空表示返回请求失败原因</returns>
        public string UserRequestToFloor(int to)
        {
            //TODO 应该修改为请求加入到电梯组请求队列，由电梯组安排
            if (InElevatorBank==null)
                return "暂无可使用电梯组！";
            switch (Status)
            {
                case UserStatus.UsNone:
                    {
                        if (FromFloor == to)
                            return "您已经在目的楼层";
                        ToFloor = to;
                        //用户请求
                        Ele ele = InElevatorBank.Request(this, to);
                        if (ele == null)
                        {
                            //请求电梯失败
                            return "暂无可使用电梯！";
                        }
                        return UserWaitFloor(ele) ? string.Empty : "等待电梯失败！";
                    }
                case UserStatus.UsWait:
                    {
                        //用户在等待电梯状态下重新请求时，先移除原目的
                        if (InOrWaitEle == null)
                            return "无效请求信息！";
                        InOrWaitEle.RemoveUserRequest(this);
                        //重新生成请地注信息
                        if (FromFloor == to)
                            return "您已经在目的楼层";
                        ToFloor = to;
                        //用户请求
                        Status = UserStatus.UsNone;
                        Ele ele = InElevatorBank.Request(this, to);
                        if (ele == null)
                        {
                            //请求电梯失败
                            return "暂无可使用电梯！";
                        }
                        return UserWaitFloor(ele) ? string.Empty : "等待电梯失败！";
                    }
                case UserStatus.UsInEle:
                    if (InOrWaitEle == null)
                        return "无效请求信息！";
                    InOrWaitEle.ResetUserRequest(this,to);
                    ToFloor = to;
                    return string.Empty;
            }
            return string.Empty;
        }

        /// <summary>
        /// 用户等待电梯
        /// </summary>
        /// <param name="waitEle">等待的电梯</param>
        /// <returns>设置成功或失败，只有在用户在等待电梯或无请求状态时才能设置为等待电梯状态</returns>
        private bool UserWaitFloor(Ele waitEle)
        {
            if (Status == UserStatus.UsNone ||
                Status == UserStatus.UsWait)
            {
                if (Status == UserStatus.UsNone)
                    UseTime = 0;
                Status = UserStatus.UsWait;
                InOrWaitEle = waitEle;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 累加用户用时
        /// </summary>
        public void TickUseTime()
        {
            if (Status != UserStatus.UsNone)
                UseTime++;
        }

        /// <summary>
        /// 用户还需要等待多久才能进入电梯
        /// </summary>
        /// <returns></returns>
        public int GetNeedWaitTime()
        {
            if (Status == UserStatus.UsInEle)
                return 0;
            if (Status == UserStatus.UsNone||InOrWaitEle==null)
                return int.MaxValue;
            return InOrWaitEle.ComNowRunToFloor(ToFloor);
        }

        /// <summary>
        /// 获取到目的楼层需要时间
        /// </summary>
        /// <returns></returns>
        public int GetNeedAllTime()
        {
            if (Status == UserStatus.UsNone || InOrWaitEle == null)
                return int.MaxValue;

            return InOrWaitEle.ComRunToFloor(Status == UserStatus.UsInEle?NowFloor:FromFloor,ToFloor);
        }

        /// <summary>
        /// 设置用户进入楼层
        /// </summary>
        /// <returns></returns>
        public bool UserInFloor()
        {
            if (Status != UserStatus.UsWait)
                return false;
            Status = UserStatus.UsInEle;
            NowFloor = FromFloor;
            return true;
        }

        /// <summary>
        /// 电梯载到新的楼层
        /// </summary>
        /// <param name="newfloor"></param>
        public void EleToNewFloor(int newfloor)
        {
            //只在用户在电梯中时才会改变当前层
            if (Status == UserStatus.UsInEle)
            {
                NowFloor = newfloor;
            }
        }

        /// <summary>
        /// 用户到达楼层
        /// </summary>
        /// <returns>返回所有用时，包括等待电梯及在电梯中运行时间</returns>
        public int UserToFloor()
        {
            Status = UserStatus.UsNone;
            NowFloor = ToFloor;
            return UseTime;
        }

    }
}
