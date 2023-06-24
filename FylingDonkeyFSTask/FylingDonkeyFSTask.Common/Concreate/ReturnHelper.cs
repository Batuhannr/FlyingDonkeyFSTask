using FylingDonkeyFSTask.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FylingDonkeyFSTask.Common.Concreate
{
    public class ReturnHelper<T>
    {
        public ReturnHelper()
        {
            Messages = new List<string>();
        }

        public ReturnHelper(bool state, T data, List<string> message)
        {
            Messages = new List<string>();
            this.Data = data;
            this.Status = state;
            this.Messages = message;
        }

        public bool Status { get; set; }
        public T Data { get; set; }

        public List<string> Messages { get; set; }
        public string AddMessage(string messsage)
        {
            Messages.Add(messsage);
            return messsage;
        }
    }
}
