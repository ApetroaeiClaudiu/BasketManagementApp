using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient {
    public enum UserEvent {
        Refresh
    };
    public class UserEventArgs : EventArgs {
        private readonly UserEvent userEvent;
        private readonly Object data;
        //private readonly Object data;

        public UserEventArgs(UserEvent userEvent,object data) {
            this.userEvent = userEvent;
            this.data = data;
        }

        public UserEvent UserEventType {
            get { return userEvent; }
        }

        public Object Data {
            get { return data; }
        }

        //public object Data {
        //    get { return data; }
        //}
    }
}
