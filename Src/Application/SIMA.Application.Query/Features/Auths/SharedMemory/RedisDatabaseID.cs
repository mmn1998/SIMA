using System;
using System.Collections.Generic;
using System.Text;

namespace SIMA.Application.Query.Features.Auths.SharedMemory
{
    public class RedisDatabaseID
    {
        public int CardDB { set; get; }
        public int AccountDB { set; get; }
        public int OrganizationDB { set; get; }
        public int ServiceDB { set; get; }
        public int OperationTypeDB { set; get; }
        public int TransactionDB { set; get; }
        public int WalletDB { set; get; }
        public int UniqueKey { set; get; }
        public int ConsumerDB { set; get; }
        public int OperationWage { set; get; }
        public int RefundConstraint { set; get; }
    }
}

