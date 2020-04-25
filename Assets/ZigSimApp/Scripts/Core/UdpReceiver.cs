using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ZigSimTools
{
    public class UdpReceiver
    {
        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler Disconnected;

        private int _port;
        private UdpClient _udpClient;
        private Task _receiveTask;
        private bool _isReceiving = false;

        public UdpReceiver (int port)
        {
            _port = port;
        }

        public void StartReceiving ()
        {
            if (_isReceiving)
            {
                return;
            }

            _udpClient = new UdpClient (_port);
            _isReceiving = true;
            _receiveTask = DataReceiveTask ();
        }

        public async Task StopReceiving ()
        {
            if (!_isReceiving)
            {
                return;
            }

            _isReceiving = false;

            if (_receiveTask != null && _receiveTask.Status == TaskStatus.Running)
            {
                await _receiveTask;
            }
            _udpClient.Close ();
            OnDisconnectrd (EventArgs.Empty);
        }

        private async Task DataReceiveTask ()
        {
            await Task.Run (() =>
            {
                while (_isReceiving)
                {
                    System.Net.IPEndPoint remoteEP = null;
                    byte[] data = _udpClient.Receive (ref remoteEP);
                    string text = System.Text.Encoding.ASCII.GetString (data);
                    OnMessageReceived (new MessageEventArgs (text));
                }
            });
        }

        protected virtual void OnMessageReceived (MessageEventArgs e)
        {
            MessageReceived?.Invoke (this, e);
        }

        protected virtual void OnDisconnectrd (EventArgs e)
        {
            Disconnected?.Invoke (this, e);
        }
    }

    public class MessageEventArgs : EventArgs
    {
        public string Message { get; }

        public MessageEventArgs (string message) : base ()
        {
            Message = message;
        }
    }
}