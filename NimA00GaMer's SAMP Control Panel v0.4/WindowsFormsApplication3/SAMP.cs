//**************************************//
//**********SAMP QUERY CLASS************//
//**Originally made by SAMP developers**//
//**************************************//

///////////////////////////////////////////
//Modified, optimized, recoded by NanoCat//
///////////////////////////////////////////

///////////////////////////////////////////
//***Released on UnknownCheats.Me only***//
///////////////////////////////////////////

//*************************************************//
//!!!DO NOT RELEASE OR RE-RELEASE ON OTHER SITES!!!//
//*************************************************//

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SAMP
{
    /// <summary>
    /// RCON query system
    /// </summary>

    class RCONQuery : IDisposable
    {
        Socket qSocket;
        IPAddress address;
        int _port = 0;
        string _password = null;

        string[] results = new string[50];
        int _count = 0;

        /// <summary>
        /// Sets up information about the remote server.
        /// </summary>
        /// <param name="addr">Remote address, if DNS, set "dns" to true</param>
        /// <param name="port">Port</param>
        /// <param name="password">RCON password</param>
        /// <param name="dns">Is "addr" DNS address?</param>

        public RCONQuery(string addr, int port, string password, bool dns = false)
        {
            qSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); // Creating a new socket
            qSocket.SendTimeout = 5000;
            qSocket.ReceiveTimeout = 5000;

            if (dns) //Checking for IP address, if not, then translating DNS into it
                try
                {
                    address = Dns.GetHostAddresses(addr)[0];
                }
                catch { }
            else
                try
                {
                    address = IPAddress.Parse(addr);
                }
                catch { }

            _port = port;
            _password = password;
        }

        /// <summary>
        /// Sends console command to the remote server.
        /// Server information must be set up.
        /// </summary>
        /// <param name="command">Console command</param>
        /// <returns>If sending was successful, returns true, else false.</returns>

        public bool Send(string command)
        {
            try
            {
                IPEndPoint endpoint = new IPEndPoint(address, _port);

                using (MemoryStream stream = new MemoryStream()) // Creating a new memory stream
                {
                    using (BinaryWriter writer = new BinaryWriter(stream)) // SAMP likes to send\recieve data in binary format :/
                    {
                        writer.Write("SAMP".ToCharArray()); // Paket prefix

                        string[] SplitIP = address.ToString().Split('.'); // Splitting ipadress into array

                        writer.Write(Convert.ToByte(SplitIP[0]));
                        writer.Write(Convert.ToByte(SplitIP[1]));
                        writer.Write(Convert.ToByte(SplitIP[2]));
                        writer.Write(Convert.ToByte(SplitIP[3]));

                        writer.Write((ushort)_port); // Port

                        writer.Write('x'); // opcode, 'x' MEANS RCON

                        writer.Write((ushort)_password.Length); //Password length
                        writer.Write(_password.ToCharArray()); // Password itself

                        writer.Write((ushort)command.Length); // Command length
                        writer.Write(command.ToCharArray()); // Command length
                    }

                    if (qSocket.SendTo(stream.ToArray(), endpoint) > 0) // Sending our stream to the server
                        return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Waits for reply from server and then pushes it to the store.
        /// </summary>
        /// <returns>Count of strings that were received.</returns>

        public int Recieve()
        {
            try
            {
                for (int i = 0; i < results.GetLength(0); i++)
                    results.SetValue(null, i);

                _count = 0;

                EndPoint endpoint = new IPEndPoint(address, _port);

                byte[] rBuffer = new byte[500]; // Create a buffer to store received data

                int count = qSocket.ReceiveFrom(rBuffer, ref endpoint); // Receive data from server

                using (MemoryStream stream = new MemoryStream(rBuffer)) // Create a new memory stream to send it later
                using (BinaryReader reader = new BinaryReader(stream)) // SAMP likes to send\recieve data in binary format :/
                {
                    if (stream.Length <= 11) // Wrong length check
                        return _count;

                    reader.ReadBytes(11); // Skipping first 11 bytes 
                    short len;

                    try
                    {
                        while ((len = reader.ReadInt16()) != 0)
                            results[_count++] = new string(reader.ReadChars((int)len)); // Getting recieved anwser
                    }
                    catch
                    {
                        return _count;
                    }
                }
            }
            catch
            {
                return _count;
            }

            return _count;
        }

        /// <summary>
        /// Returns received strings from the store.
        /// If count is set to -1, will return all the store.
        /// </summary>
        /// <param name="count">Count of lines to read</param>
        /// <returns>Lines that were read from the store.</returns>

        public string[] Store(int count = -1)
        {
            string[] rString = new string[count != -1 ? count : _count];

            for (int i = 0; (i < count || count == -1) && i < _count; i++)
                rString[i] = results[i];

            _count = 0;

            return rString;
        }

        /// <summary>
        /// Closes the socket.
        /// </summary>

        public void Dispose()
        {
            try
            {
                qSocket.Dispose();
            }
            catch { }
        }
    }

    /// <summary>
    /// Server information query system
    /// </summary>

    class Query : IDisposable
    {
        Socket qSocket;
        IPAddress address;
        int _port = 0;

        string[] results;
        int _count = 0;

        DateTime[] timestamp = new DateTime[2];

        public enum PaketOpcode
        {
            Info = 'i', // total info about server
            Rules = 'r', // rules, also known as key values or params (gravity, website) etc
            ClientList = 'c', // basic client list
            DetailedClientList = 'd', // detailed (advanced) client list
            Ping = 'p' // ping paket
        }

        /// <summary>
        /// Sets up information about the remote server.
        /// </summary>
        /// <param name="addr">Remote address, if DNS, set "dns" to true</param>
        /// <param name="port">Port</param>
        /// <param name="dns">Is "addr" DNS address?</param>

        public Query(string addr, int port, bool dns = false)
        {
            qSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); // New socket

            qSocket.SendTimeout = 5000;
            qSocket.ReceiveTimeout = 5000;
            //Same routine**************************************************************************************************
            if (dns)                                                                                                   //
                try                                                                                                   //
                {                                                                                                    //
                    address = Dns.GetHostAddresses(addr)[0]; // try to translate                                    //
                }                                                                                                  //
                catch { }                                                                                         //
            else                                                                                                 //
                try                                                                                             //       
                {                                                                                              //
                    address = IPAddress.Parse(addr); // check, is ip right                                    //
                }                                                                                            //
                catch { }                                                                                   //
            //***************************************************************************************************
            _port = port;
        }

        /// <summary>
        /// Sends query request paket to the remote server.
        /// Server information must be set up.
        /// </summary>
        /// <param name="opcode">Paket opcode</param>
        /// <param name="sign">Paket signature, any 4 characters, only used in PING opcode</param>
        /// <returns>If sending was successful, returns true, else false.</returns>

        public bool Send(char opcode, string sign = "1337")
        {
            try
            {
                EndPoint endpoint = new IPEndPoint(address, _port);

                using (MemoryStream stream = new MemoryStream()) //Saame...
                {
                    using (BinaryWriter writer = new BinaryWriter(stream)) //And this too
                    {
                        writer.Write("SAMP".ToCharArray()); //Prefix

                        string[] SplitIP = address.ToString().Split('.');

                        writer.Write(Convert.ToByte(SplitIP[0]));
                        writer.Write(Convert.ToByte(SplitIP[1]));
                        writer.Write(Convert.ToByte(SplitIP[2]));
                        writer.Write(Convert.ToByte(SplitIP[3]));

                        writer.Write((ushort)_port);

                        writer.Write(opcode); //Writing an opcode

                        if (opcode == 'p') // If opcode was 'p' write paket signature
                            writer.Write(sign.ToCharArray());

                        timestamp[0] = DateTime.Now;
                    }

                    if (qSocket.SendTo(stream.ToArray(), endpoint) > 0)
                        return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Waits for reply from server and then pushes it to the store.
        /// </summary>
        /// <returns>Count of strings that were received.</returns>

        public int Recieve()
        {
            try
            {
                _count = 0;

                EndPoint endpoint = new IPEndPoint(address, _port);

                byte[] rBuffer = new byte[500];
                qSocket.ReceiveFrom(rBuffer, ref endpoint);

                timestamp[1] = DateTime.Now;

                using (MemoryStream stream = new MemoryStream(rBuffer))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    if (stream.Length <= 10)
                        return _count;

                    reader.ReadBytes(10);

                    switch (reader.ReadChar())
                    {
                        case 'i': // Information
                            {
                                results = new string[6];

                                results[_count++] = reader.ReadByte().ToString(); // either 0 or 1, depending whether if the password has been set.
                                results[_count++] = reader.ReadInt16().ToString(); // current amount of players online on the server.
                                results[_count++] = reader.ReadInt16().ToString(); // maximum amount of players that can join the server.
                                results[_count++] = new string(reader.ReadChars(reader.ReadInt32())); // hostname
                                results[_count++] = new string(reader.ReadChars(reader.ReadInt32())); // gamemode
                                results[_count++] = new string(reader.ReadChars(reader.ReadInt32())); // mapname

                                return _count;
                            }

                        case 'r': // Rules
                            {
                                int rulecount = reader.ReadInt16(); // read, how many rules we've received

                                results = new string[rulecount * 2];

                                for (int i = 0; i < rulecount; i++)
                                {
                                    results[_count++] = new string(reader.ReadChars(reader.ReadByte())); // rule name (key)
                                    results[_count++] = new string(reader.ReadChars(reader.ReadByte())); // rule value (value)
                                }

                                return _count;
                            }

                        case 'c': // Client list
                            {
                                int playercount = reader.ReadInt16(); // read, how many players we've received

                                results = new string[playercount * 2];

                                for (int i = 0; i < playercount; i++)
                                {
                                    results[_count++] = new string(reader.ReadChars(reader.ReadByte())); // nickname
                                    results[_count++] = reader.ReadInt32().ToString(); // score
                                }

                                return _count;
                            }

                        case 'd': // Detailed player information
                            {
                                int playercount = reader.ReadInt16();

                                results = new string[playercount * 4];

                                for (int i = 0; i < playercount; i++)
                                {
                                    results[_count++] = reader.ReadByte().ToString(); // player id
                                    results[_count++] = new string(reader.ReadChars(reader.ReadByte())); // nick
                                    results[_count++] = reader.ReadInt32().ToString(); // score
                                    results[_count++] = reader.ReadInt32().ToString(); // ping
                                }

                                return _count;
                            }

                        case 'p': // Ping
                            {
                                results = new string[1];
                                results[_count++] = timestamp[1].Subtract(timestamp[0]).Milliseconds.ToString(); // time difference
                                results[_count++] = new string(reader.ReadChars(4)); // paket signature

                                return _count;
                            }

                        default:
                            return _count;
                    }
                }
            }
            catch
            {
                return _count;
            }
        }

        /// <summary>
        /// Returns received strings from the store.
        /// If count is set to -1, will return all the store.
        /// </summary>
        /// <param name="count">Count of lines to read</param>
        /// <returns>Lines that were read from the store.</returns>

        public string[] Store(int count)
        {
            string[] rString = new string[count];

            for (int i = 0; i < count && i < _count; i++)
                rString[i] = results[i];

            _count = 0;

            return rString;
        }

        /// <summary>
        /// Closes the socket.
        /// </summary>

        public void Dispose()
        {
            try
            {
                qSocket.Dispose();
            }
            catch { }
        }
    }
}