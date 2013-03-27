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
        private readonly Eles _eles;

        /// <summary>
        /// 当前楼层
        /// </summary>
        public int NowFloor;

        /// <summary>
        /// 当前状态
        /// </summary>
        public EleStatus NowStatus;

        /// <summary>
        /// 运行到下一层或上一层还要等待时间
        /// </summary>
        public int NextFloorTime;

        /// <summary>
        /// 当前电梯请求信息
        /// </summary>
        public ArrayList Requests;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="eles"></param>
        /// <param name="eleid"></param>
        /// <param name="initFloor"></param>
        public Ele(Eles eles,int eleid,int initFloor)
        {
            _eles = eles;
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
                            NextFloorTime = _eles.OneFloorRunTime;
                        }else if (NowFloor > ri.Floor)
                        {
                            NowStatus = EleStatus.EsRunDown;
                            NextFloorTime = _eles.OneFloorRunTime;   
                        }
                    }
                    break;
                case EleStatus.EsRunUp:
                    NowFloor++;
                    if (HasInOrOutNowFloor())
                    {
                        RemoveAllNowFloorRequest();
                        NowStatus = EleStatus.EsRunUpWait;
                        NextFloorTime = _eles.StopOverTime;
                    }
                    else
                    {
                        NowStatus = EleStatus.EsRunUp;
                        NextFloorTime = _eles.OneFloorRunTime;
                    }
                    break;
                case EleStatus.EsRunUpWait:
                    NowStatus = EleStatus.EsRunUp;
                    NextFloorTime = _eles.OneFloorRunTime;
                    break;
                case EleStatus.EsRunDown:
                    NowFloor--;
                    if (HasInOrOutNowFloor())
                    {
                        RemoveAllNowFloorRequest();
                        NowStatus = EleStatus.EsRunDownWait;
                        NextFloorTime = _eles.StopOverTime;
                    }
                    else
                    {
                        NowStatus = EleStatus.EsRunDown;
                        NextFloorTime = _eles.OneFloorRunTime;
                    }
                    break;
                case EleStatus.EsRunDownWait:
                    NowStatus = EleStatus.EsRunDown;
                    NextFloorTime = _eles.OneFloorRunTime;
                    break;
            }
            //判断到顶层或底层及是否到请求的最高或最低层
            if (NowStatus == EleStatus.EsRunUp || NowStatus == EleStatus.EsRunUpWait)
            {
                if (NowFloor == _eles.AllFloor || NowFloor >=
                    GetNeedRunMaxFloor())
                {
                    NowStatus = NowStatus == EleStatus.EsRunUp ? EleStatus.EsRunDown : EleStatus.EsRunDownWait;
                }
            }
            else
            {
                if (NowFloor == 1 || NowFloor >=
                    GetNeedRunMinFloor())
                {
                    NowStatus = NowStatus == EleStatus.EsRunDown ? EleStatus.EsRunUp : EleStatus.EsRunUpWait;
                }  
            }

        }

        /// <summary>
        /// 增加请求信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="to"></param>
        public bool AddUserRequest(User user, int to)
        {
            if (Requests == null || user == null)
                return false;
            //只有用户非在电梯内
            if (user.Status != UserStatus.UsInEle)
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

            AddRequest(newri);
        }

        private void AddRequest(RequestInfo newri)
        {
            //计算插入位置
            bool hasinsert = false;
            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo)Requests[i];
                if (NowStatus == EleStatus.EsWait)
                {
                    //找到比用户现在楼层高的，在之前位置插入请求
                    if (ri.Floor < newri.Floor) continue;
                    hasinsert = true;
                    Requests.Insert(i, newri);
                }
                else if (NowStatus == EleStatus.EsRunUp || NowStatus == EleStatus.EsRunUpWait)
                {
                    //向上运行时，找到比用户现在楼层高的，在之前位置插入请求
                    if (ri.Floor < newri.Floor) continue;
                    hasinsert = true;
                    Requests.Insert(i, newri);
                }
                else if (NowStatus == EleStatus.EsRunDown || NowStatus == EleStatus.EsRunDownWait)
                {
                    //向下运行时，找到比用户现在楼层低的，在之前位置插入请求
                    if (ri.Floor > newri.Floor) continue;
                    hasinsert = true;
                    Requests.Insert(i, newri);
                }
            }

            if (!hasinsert)
                Requests.Insert(0, newri);
        }

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
                }
                else if (ri.Oper == OperType.OtOut)
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
                return maxFloor;
            
            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo)Requests[i];
                if (ri.Floor > maxFloor)
                    maxFloor = ri.Floor;
            }
            return maxFloor;
        }

        private int GetNeedRunMinFloor()
        {
            int minFloor = _eles.AllFloor;
            if (Requests == null || Requests.Count == 0)
                return minFloor;

            for (int i = 0; i < Requests.Count; i++)
            {
                RequestInfo ri = (RequestInfo)Requests[i];
                if (ri.Floor < minFloor)
                    minFloor = ri.Floor;
            }
            return minFloor;
        }
    }
}
