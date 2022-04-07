using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryProject.BLL
{
    public class Result<T>
    {
        private List<string> messages = new List<string>();
        public bool Success { get; set; }
        public List<string> Messages => new List<string>(messages);
        public T Data { get; set; }
        
        public void AddMessage(string message)
        {
            messages.Add(message);
        }
    }
}
