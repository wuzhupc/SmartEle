using System;
using System.Collections;

namespace SmartEle
{
    /// <summary>
    /// 电梯状态
    /// </summary>
    public enum EleStatus
    {
        /// <summary>
        /// 停止运行状态，即不可运行
        /// </summary>
        EsStop = 0,
        /// <summary>
        /// 无请求等待状态
        /// </summary>
        EsWait = 1,
        /// <summary>
        /// 正在向上运行
        /// </summary>
        EsRunUp = 2,
        /// <summary>
        /// 正在等待用户中（方向：向上运行）
        /// </summary>
        EsRunUpWait = 3,
        /// <summary>
        /// 正在向下运行
        /// </summary>
        EsRunDown = 4,
        /// <summary>
        /// 正在等待用户中（方向：向下运行）
        /// </summary>
        EsRunDownWait = 5
    }

    /// <summary>
    /// 电梯
    /// </summary>
    public class Ele
    {
        /// <summary>
        /// 电梯号
        /// </summary>
        public int Eleid;

        /// <summary>
        /// 所属电梯组
        /// </summary>
        private readonly ElevatorBank _elevatorBank;

        /// <summary>
        /// 当前楼层
        /// </summary>
        public int NowFloor;

        /// <summary>
        /// 当前状态
        /// </summary>
        public EleStatus NowStatus;

        public string Name
        {
            get { return "电梯" + Eleid; }
        }

        public string NowStatusDesc
        {
            get
            {
                switch (NowStatus)
                {
                        case EleStatus.EsStop:
                        return "停止运行";
                        case EleStatus.EsWait:
                        return "等待中";
                        case EleStatus.EsRunUp:
                        return "向上运行";
                        case EleStatus.EsRunUpWait:
                        return "向上运行(停留)";
                        case EleStatus.EsRunDown:
                        return "向下运行";
                        case EleStatus.EsRunDownWait:
                        return "向下运行(停留)";
                }
                return "";
            }
        }

        /// <summary>
        /// 运行到下一层或上一层还要等待时间
        /// </summary>
        public int NextFloorTime;

        /// <summary>
        /// 当前电梯请求信息
        /// </summary>
        public ArrayList Requests;


        /// <summary>
        ///电梯中的用户信息，用;分隔开
        /// </summary>
        public string InEleUsers
        {
            get
            {
                string result = "";
                if (Requests != null && Requests.Count > 0)
                {
                    for (int i = 0; i < Requests.Count; i++)
                    {
                        RequestInfo ri = (RequestInfo) Requests[i];
                        if (ri.Oper == OperType.OtOut&&ri.RequestUser.Status==UserStatus.UsInEle)
                        {
                            result += ri.RequestUser.Name+";";
                        }
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 等待电梯的用户信息，用;分隔开
        /// </summary>
        public string WaitEleUsers
        {
            get
            {
                string result = "";
                if (Requests != null && Requests.Count > 0)
                {
                    for (int i = 0; i < Requests.Count; i++)
                    {
                        RequestInfo ri = (RequestInfo)Requests[i];
                        if (ri.Oper == OperType.OtIn)
                        {
                            result += ri.RequestUser.Name+";";
                        }
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="elevatorBank"></param>
        /// <param name="eleid"></param>
        /// <param name="initFloor"></param>
        public Ele(ElevatorBank elevatorBank,int eleid,int initFloor)
        {
            _elevatorBank = elevatorBank;
            Eleid = eleid;
            NowFloor = initFloor;
            NowStatus = EleStatus.EsWait;
            Requests = new ArrayList();
            NextFloorTime = 1;
        }

        /// <summary>
        /// 电梯运行,每秒将被调用一次
        /// </summary>
        public void OnRun()
        {
            if(NowStatus == EleStatus.EsStop)
                return;
            if (Requests == null || Requests.Count == 0)
            {
                NextFloorTime = 1;
                NowStatus = EleStatus.EsWait;
                return;
            }
            //每次都检查一下当前层相关的操作//可能非必须?
            RemoveAllNowFloorRequest();

            TickAllUserTime();
            //
            NextFloorTime--;
            if (NextFloorTime > 0) return;
            //执行下一步的操作
            //根据状态进行处理
            switch (NowStatus)
            {
                case EleStatus.EsWait:
                    {
                        //如果是从等待状态到有请求状态
                        RequestInfo ri = (RequestInfo)Requests[0];

                        if (NowFloor < ri.Floor)
                        {
                            NowStatus = EleStatus.EsRunUp;
                            NextFloorTime = _elevatorBank.OneFloorRunTime;
                        }else if (NowFloor > ri.Floor)
                        {
                            NowStatus = EleStatus.EsRunDown;
                            NextFloorTime = _elevatorBank.OneFloorRunTime;   
                        }
                    }
                    break;
                case EleStatus.EsRunUp:
                    NowFloor++;
                    if (HasInOrOutNowFloor())
                    {
                        RemoveAllNowFloorRequest();
                        NowStatus = EleStatus.EsRunUpWait;
                        NextFloorTime = _elevatorBank.StopOverTime;
                    }
                    else
                    {
                        NowStatus = EleStatus.EsRunUp;
                        NextFloorTime = _elevatorBank.OneFloorRunTime;
                    }
                    break;
                case EleStatus.EsRunUpWait:
                    NowStatus = EleStatus.EsRunUp;
                    NextFloorTime = _elevatorBank.OneFloorRunTime;
                    break;
                case EleStatus.EsRunDown:
                    NowFloor--;
                    if (HasInOrOutNowFloor())
                    {
                        RemoveAllNowFloorRequest();
                        NowStatus = EleStatus.EsRunDownWait;
                        NextFloorTime = _elevatorBank.StopOverTime;
                    }
                    else
                    {
                        NowStatus = EleStatus.EsRunDown;
                        NextFloorTime = _elevatorBank.OneFloorRunTime;
                    }
                    break;
                case EleStatus.EsRunDownWait:
                    NowStatus = EleStatus.EsRunDown;
                    NextFloorTime = _elevatorBank.OneFloorRunTime;
                    break;
            }
            //
            ChangeAllUserToNewFloor();
            //判断到顶层或底层及是否到请求的最高或最低层
            if (NowStatus == EleStatus.EsRunUp || NowStatus == EleStatus.EsRunUpWait)
            {
                if (NowFloor == _elevatorBank.AllFloor || NowFloor >=
                    GetNeedRunMaxFloor())
                {
                    NowStatus = (NowStatus == EleStatus.EsRunUp ? EleStatus.EsRunDown : EleStatus.EsRunDownWait);
                }
            }
            else if (NowStatus == EleStatus.EsRunDown || NowStatus == EleStatus.EsRunDownWait)
            {
                if (NowFloor == 1 || NowFloor <=
                    GetNeedRunMinFloor())
                {
                    NowStatus = (NowStatus == EleStatus.EsRunDown ? EleStatus.EsRunUp : EleStatus.EsRunUpWait);
                }  
            }
            //重新计算所有请求还需要运行楼层数
            UpdateAllRequestRunFloor();
        }

        private void UpdateAllRequestRunFloor()
        {
            if (Requests == null || Requests.Count == 0)
                return;
            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo) Requests[i];
                if (ri.Oper == OperType.OtIn)
                    ri.RunFloor = ComRunToFloorNum(NowFloor, ri.Floor, ri.Floor, NowStatus);
                else
                {
                    if (ri.RequestUser.Status == UserStatus.UsInEle)
                        ri.RunFloor = ComRunToFloorNum(NowFloor, ri.Floor, ri.Floor, NowStatus);
                    else
                        ri.RunFloor = ComRunToFloorNum(NowFloor, ri.RequestUser.FromFloor, ri.Floor, NowStatus);
                }
            }
        }

        /// <summary>
        /// 增加请求信息
        /// </summary>
        /// <param name="user"></param>
        public bool AddUserRequest(User user,int to)
        {
            if (Requests == null || user == null)
                return false;
            //只有用户非在电梯内
            if (user.Status == UserStatus.UsInEle)
                return false;
            AddInRequest(user);
            AddOutRequest(user,to);
            return true;
        }

        /// <summary>
        /// 用户不在电梯内时变更目的楼层时移除请求操作
        /// </summary>
        /// <param name="user"></param>
        public bool RemoveUserRequest(User user)
        {
            if (Requests == null || user == null)
                return false;                    
            //只有用户不在电梯内时才能用移除方式
            if (user.Status == UserStatus.UsInEle)
                return false;
            RemoveInRequest(user);
            RemoveOutRequest(user);
            return true;
        }

        /// <summary>
        /// 用户在电梯内时变更请求
        /// </summary>
        /// <param name="user"></param>
        /// <param name="to"></param>
        public bool ResetUserRequest(User user, int to)
        {
            if (Requests == null || user == null)
                return false;
            //只有用户在电梯内状态时才能用移除方式
            if (user.Status != UserStatus.UsInEle)
                return false;
            //暂定用户变更楼层时移除旧请求信息
            RemoveOutRequest(user);
            AddOutRequest(user,to);
            return true;
        }

        /// <summary>
        /// 增加进电梯请求
        /// </summary>
        /// <param name="user"></param>
        private void AddInRequest(User user)
        {
            //判断是否已经有存在用户,且是在等待进电梯状态，有则先移除旧的
            RemoveInRequest(user);

            RequestInfo newri = new RequestInfo(user, OperType.OtIn, user.FromFloor);
            newri.RunFloor = ComRunToFloorNum(NowFloor, newri.Floor, newri.Floor, NowStatus);
            AddRequest(newri);

        }

        /// <summary>
        /// 增加出电梯请求
        /// </summary>
        /// <param name="user"></param>
        /// <param name="to"></param>
        private void AddOutRequest(User user, int to)
        {
            RemoveOutRequest(user);

            RequestInfo newri = new RequestInfo(user, OperType.OtOut, to);
            if (newri.RequestUser.Status == UserStatus.UsInEle)
                newri.RunFloor = ComRunToFloorNum(NowFloor, newri.Floor, newri.Floor, NowStatus);
            else
                newri.RunFloor = ComRunToFloorNum(NowFloor, newri.RequestUser.FromFloor, newri.Floor, NowStatus);
                
            AddRequest(newri);
        }

        /// <summary>
        /// 将用户请求加入请求列表
        /// </summary>
        /// <param name="newri"></param>
        private void AddRequest(RequestInfo newri)
        {
            //计算插入位置
            bool hasinsert = false;
            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo)Requests[i];
                if (ri.RunFloor <= newri.RunFloor) continue;
                hasinsert = true;
                Requests.Insert(i,newri);
                break;
            }

            if (!hasinsert)
                Requests.Add(newri);
            UpdateAllRequestRunFloor();
        }

        /// <summary>
        /// 移除某用户的进电梯请求
        /// </summary>
        /// <param name="user"></param>
        private void RemoveInRequest(User user)
        {
            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo)Requests[i];
                if (user.UserId != ri.RequestUser.UserId) continue;
                if (ri.Oper == OperType.OtIn)
                    Requests.RemoveAt(i);
            }
        }

        /// <summary>
        /// 移除某用户出电梯请求
        /// </summary>
        /// <param name="user"></param>
        private void RemoveOutRequest(User user)
        {
            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo)Requests[i];
                if (user.UserId != ri.RequestUser.UserId) continue;
                if (ri.Oper == OperType.OtOut)
                    Requests.RemoveAt(i);
            }
        }

        /// <summary>
        /// 对所有的用户增加请求用时
        /// </summary>
        private void TickAllUserTime()
        {
            if (Requests == null || Requests.Count == 0)
                return;
            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo) Requests[i];
                if(ri.Oper==OperType.OtOut)
                    ri.RequestUser.TickUseTime();
            }
        }

        /// <summary>
        /// 变更电梯中所有用户的楼层信息
        /// </summary>
        private void ChangeAllUserToNewFloor()
        {
            if (Requests == null || Requests.Count == 0)
                return;
            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo) Requests[i];
                if(ri.Oper == OperType.OtOut &&
                    ri.RequestUser.Status==UserStatus.UsInEle)
                    ri.RequestUser.EleToNewFloor(NowFloor);
            }
        }

        /// <summary>
        /// 对本层所有请求进行处理
        /// </summary>
        private void RemoveAllNowFloorRequest()
        {
            if (Requests == null || Requests.Count == 0)
                return;
            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo)Requests[i];
                
                if (ri.Floor != NowFloor) continue;//可直接return;?

                if (ri.Oper == OperType.OtIn)
                {
                    ri.RequestUser.UserInFloor();
                    RemoveInRequest(ri.RequestUser);
                    //AddOutRequest(ri.RequestUser,ri.RequestUser.ToFloor);
                }
                else if (ri.Oper == OperType.OtOut&&ri.RequestUser.Status == UserStatus.UsInEle)
                {
                    ri.RequestUser.UserToFloor();
                    RemoveOutRequest(ri.RequestUser);
                }
            }
        }
        /// <summary>
        /// 判断本层是否有请求
        /// </summary>
        /// <returns></returns>
        private bool HasInOrOutNowFloor()
        {
            if (Requests == null || Requests.Count == 0)
                return false;

            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo) Requests[i];
                if (ri.Floor != NowFloor) continue;
                return true;
            }
            return false;
        }

        private int GetNeedRunMaxFloor()
        {
            int maxFloor = 1;
            if (Requests == null || Requests.Count == 0)
                return 1;
            
            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo)Requests[i];
                if(ri==null)
                    continue;
                if (ri.Floor > maxFloor&&(ri.Oper==OperType.OtIn||
                    ri.Oper==OperType.OtOut&&ri.RequestUser.Status==UserStatus.UsInEle))
                    maxFloor = ri.Floor;
            }
            return maxFloor;
        }

        private int GetNeedRunMinFloor()
        {
            int minFloor = _elevatorBank.AllFloor;
            if (Requests == null || Requests.Count == 0)
                return 1;

            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo)Requests[i];
                if (ri.Floor < minFloor&&(ri.Oper == OperType.OtIn ||
                    ri.Oper == OperType.OtOut && ri.RequestUser.Status == UserStatus.UsInEle))
                    minFloor = ri.Floor;
            }
            return minFloor;
        }

        /// <summary>
        /// 计算now楼层到from楼层再到to楼层需要经过多少楼层
        /// </summary>
        /// <param name="now">当前楼层</param>
        /// <param name="from">起始楼层</param>
        /// <param name="to">目的楼层</param>
        /// <param name="status"></param>
        /// <returns></returns>
        private int ComRunToFloorNum(int now,int from, int to,EleStatus status)
        {
            if(status==EleStatus.EsRunDownWait)
                status = EleStatus.EsRunDown;
            if(status==EleStatus.EsRunUpWait)
                status = EleStatus.EsRunUp;
            if (status == EleStatus.EsStop || status == EleStatus.EsWait)
            {
                status = now > from ? EleStatus.EsRunDown : EleStatus.EsRunUp;
            }
            if (now == from)//只要计算起点到终点
            {
                if (from == to) return 0;
                if (from < to)
                {
                    if (status == EleStatus.EsRunUp)
                        return to - from;
                    //向下运行时
                    //计算要到达的最低楼层
                    int min = GetNeedRunMinFloor();
                    if (min > from)
                        return to - from;
                    return from - min + to - min;
                }
                //from>to
                if (status == EleStatus.EsRunDown)
                    return from - to;
                int max = GetNeedRunMaxFloor();
                if (max < from)
                    return from - to;
                return max - from + max - to;
            }
            
            //now->from
           int count = ComRunToFloorNum(now, now, from, status);
           //from->to
           count += ComRunToFloorNum(from, from, to, status);
           return count;
        }

        /// <summary>
        /// 设置请求的状态是否被计算过
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="hascom"></param>
        private void SetAllRequsetComStatus(int min, int max, bool hascom)
        {
            if (Requests == null || Requests.Count == 0 || min>max)
                return;
            for (var i = 0; i < Requests.Count; i++)
            {
                var ri = (RequestInfo) Requests[i];
                if(ri.Floor<min||ri.Floor>max||ri.HasCom==hascom) continue;
                ri.HasCom = hascom;
            }
        }


        /// <summary>
        /// 计算当前层到目的层需要停留几次，注意计算前调用SetAllRequsetComStatus(min,max,false);
        /// </summary>
        /// <param name="now"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private int ComRunToFloorStopNum(int now ,int from,int to,EleStatus status)
        {
            if (Requests == null || Requests.Count == 0)
                return 0;

            if (status == EleStatus.EsRunDownWait)
                status = EleStatus.EsRunDown;
            if (status == EleStatus.EsRunUpWait)
                status = EleStatus.EsRunUp;
            if (status == EleStatus.EsStop || status == EleStatus.EsWait)
            {
                status = now > from ? EleStatus.EsRunDown : EleStatus.EsRunUp;
            }
            
            int count=0;
            if (now == from)//只要计算起点到终点
            {
                if (from == to) return 0;

                if (from < to)
                {
                    count = 0;
                    int tfloor = -1;
                    if (status == EleStatus.EsRunUp)
                    {
                        for (int i = 0; i < Requests.Count; i++)
                        {
                            RequestInfo ri = (RequestInfo)Requests[i];
                            if (!ri.HasCom&&
                                tfloor!=ri.Floor&&ri.Floor >= from && ri.Floor <= to)
                                count++;
                            tfloor = ri.Floor;
                        }
                    }
                    else //向下运行时
                    {
                        //计算要到达的最低楼层
                        int min = GetNeedRunMinFloor();
                        if (min > from)
                        {
                            return ComRunToFloorStopNum(from, from, to, EleStatus.EsRunUp);
                        }
                        //先运行到最低层
                        count += ComRunToFloorStopNum(from,from, min, EleStatus.EsRunDown);
                        SetAllRequsetComStatus(min,from,true);//防止重复计算，因为这些楼层已经到过
                        //再运行到目的层
                        count += ComRunToFloorStopNum(min, min, to, EleStatus.EsRunUp);
                        
                    }
                }
                else //from>to
                {
                    count = 0;
                    int tfloor = -1;
                    if (status == EleStatus.EsRunDown)
                    {
                        for (int i = 0; i < Requests.Count; i++)
                        {
                            RequestInfo ri = (RequestInfo)Requests[i];
                            if (!ri.HasCom&&
                                tfloor!=ri.Floor&&ri.Floor <= from && ri.Floor >= to)
                                count++;
                            tfloor = ri.Floor;
                        }
                    }
                    else //向上运行时
                    {
                        //计算要到达的最高楼层
                        int max = GetNeedRunMaxFloor();
                        if (max<from)
                        {
                            return ComRunToFloorStopNum(from, from, to, EleStatus.EsRunDown);
                        }
                        //先运行到最高层
                        count += ComRunToFloorStopNum(from,from, max, EleStatus.EsRunUp);
                        SetAllRequsetComStatus(from,max,true);//防止重复计算，因为这些楼层已经到过
                        //再运行到目的层
                        count += ComRunToFloorStopNum(max, max, to, EleStatus.EsRunDown);
                    }
                }
                

                return count;
            }
            
            //now->from
           count = ComRunToFloorStopNum(now, now, from, status);
           SetAllRequsetComStatus(now>from?from:now,now>from?now:from,true);
           //from->to
           count += ComRunToFloorStopNum(from, from, to, status);
            //
           return count;
        }

        /// <summary>
        /// 计算电梯到用户起始层再到目的层需要的时间
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int ComRunToFloorTime(int from,int to)
        {
            if (NowStatus == EleStatus.EsStop)
                return int.MaxValue;

            int usetime = 0;
            switch (NowStatus)
            {
                case EleStatus.EsRunUp:
                    int floorcount = ComRunToFloorNum(NowFloor, from, to, EleStatus.EsRunUp);
                    usetime += (floorcount - 1)*_elevatorBank.OneFloorRunTime + NextFloorTime;
                    SetAllRequsetComStatus(1,_elevatorBank.AllFloor,false);
                    int stopnum = ComRunToFloorStopNum(NowFloor, from, to, EleStatus.EsRunUp);
                    usetime += (stopnum>0?stopnum-1:0)*_elevatorBank.StopOverTime;
                    break;
                case EleStatus.EsWait:
                    //usetime += (Math.Abs(NowFloor - from)+Math.Abs(to-from))*_elevatorBank.OneFloorRunTime
                    //+_elevatorBank.StopOverTime;
                    floorcount = ComRunToFloorNum(NowFloor, from, to, EleStatus.EsWait);
                    usetime += floorcount * _elevatorBank.OneFloorRunTime;
                    SetAllRequsetComStatus(1, _elevatorBank.AllFloor, false);
                    stopnum = ComRunToFloorStopNum(NowFloor, from, to, EleStatus.EsWait);
                    usetime += (stopnum > 0 ? stopnum - 1 : 0) * _elevatorBank.StopOverTime + NextFloorTime;
                    break;
                case EleStatus.EsRunUpWait:
                    floorcount = ComRunToFloorNum(NowFloor, from, to, EleStatus.EsRunUp);
                    usetime += floorcount* _elevatorBank.OneFloorRunTime;
                    SetAllRequsetComStatus(1, _elevatorBank.AllFloor, false);
                    stopnum = ComRunToFloorStopNum(NowFloor, from, to, EleStatus.EsRunUp);
                    usetime += (stopnum > 0 ? stopnum - 1 : 0) * _elevatorBank.StopOverTime + NextFloorTime;
                    break;
                case EleStatus.EsRunDown:
                    floorcount = ComRunToFloorNum(NowFloor, from, to, EleStatus.EsRunDown);
                    usetime += (floorcount-1) * _elevatorBank.OneFloorRunTime + NextFloorTime;
                    SetAllRequsetComStatus(1, _elevatorBank.AllFloor, false);
                    stopnum = ComRunToFloorStopNum(NowFloor, from, to, EleStatus.EsRunDown);
                    usetime += (stopnum > 0 ? stopnum - 1 : 0) * _elevatorBank.StopOverTime;
                    break;
                case EleStatus.EsRunDownWait:
                    floorcount = ComRunToFloorNum(NowFloor, from, to, EleStatus.EsRunDown);
                    usetime += floorcount * _elevatorBank.OneFloorRunTime;
                    SetAllRequsetComStatus(1, _elevatorBank.AllFloor, false);
                    stopnum = ComRunToFloorStopNum(NowFloor, from, to, EleStatus.EsRunDown);
                    usetime += (stopnum > 0 ? stopnum - 1 : 0) * _elevatorBank.StopOverTime + NextFloorTime;
                    break;
            }
            return usetime;
        }
    }
}
