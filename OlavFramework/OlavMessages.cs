using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlavFramework
{
    public class OlavMessages
    {
        private IList<string> _messages;

        public OlavMessages()
        {
            _messages = new List<string>();
        }

        public IList<string> Messages()
        {
            var newMessages = _messages;
            _messages = new List<string>();
            return newMessages;
        }

        public void AddMessage(string newMessage)
        {
            _messages.Add(newMessage);
        }
    }
}
