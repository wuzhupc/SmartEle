using System.Collections;

namespace SmartEle
{
    /// <summary>
    /// 应用程序信息，单例模式，使用GetInfo()方法获取
    /// </summary>
    public class ApplicationInfo
    {
        
        private ElevatorBank _elevatorBank;

        /// <summary>
        /// 用户
        /// </summary>
        private readonly ArrayList _users;

        public ArrayList Users
        {
            get { return _users; }
        }

        private static ApplicationInfo _info;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ApplicationInfo GetInfo()
        {
            if (_info == null)
                _info = new ApplicationInfo();
            return _info;
        }

        private ApplicationInfo()
        {
            _users = new ArrayList();
        }

        public ElevatorBank GetEles()
        {
            return _elevatorBank;
        }

        /// <summary>
        /// 设置电梯组
        /// </summary>
        /// <param name="elevatorBank"></param>
        public void SetElevatorBank(ElevatorBank elevatorBank)
        {
            if (_elevatorBank != null)
            {
                if (_elevatorBank.RunStatus != ElevatorBankStauts.EssStop)
                {
                    //原来电梯组在运行中，要先停止所有电梯
                    _elevatorBank.StopElevatorBank();
                }
                //移除原来所有的用户
                _users.Clear();
            }
            _elevatorBank = elevatorBank;
        }

        private int GetNewUserId()
        {
            if (_users == null || _users.Count <= 0)
                return 1;
            return _users.Count + 1;
        }

        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="from">用户当前所在楼层</param>
        public User NewUser(int from)
        {
            User user = new User(GetNewUserId(),from,_elevatorBank);
            _users.Add(user);
            return user;
        }

    }
}
