using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ChatServer {
	class User {
		public User ( string username, TcpClient client, string sessionkey ) {
			this.Username = username;
			this.Client = client;
			this.SessionKey = sessionkey;
		}

		public string Username {
			get;
			set;
		}

		public TcpClient Client {
			get;
			set;
		}

		public string SessionKey {
			get;
			set;
		}
	}
}
