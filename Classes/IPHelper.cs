using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

namespace FileReadToSQLDB
{
    class IPHelper
    {
        /// <summary>
        /// check if host is alive
        /// </summary>
        /// <param name="host_name"></param>
        /// <returns></returns>
        public IPAddress get_ip_from_host_name(string host_name)
        {
            //Debug.write_debug("Converting Host To IP: " + host_name);
            try
            {
                IPAddress ip_address = null;

                using (var p = new Ping())
                {
                    var reply = p.Send(host_name);

                    if (reply.Status.Equals(IPStatus.Success))
                    {
                        ip_address = get_ipv4_address(host_name);
                    }
                }
                return ip_address;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// get the ipv4 address instead of ipv6
        /// </summary>
        /// <param name="hostname"></param>
        /// <returns></returns>
        private IPAddress get_ipv4_address(string host_name)
        {
            //Debug.write_debug("Using IPv4 Address");
            try
            {
                IPAddress ip_address = null;

                var result = Dns.GetHostAddresses(host_name)
                    .Where(ip => ip.AddressFamily.Equals(AddressFamily.InterNetwork));

                if (result != null)
                {
                    foreach (var ip in result)
                    {
                        //the list contains numbers that aren't proper so we check for one formatted correctly
                        if (ip.ToString().Contains('.'))
                        {
                            ip_address = ip;
                            break;
                        }
                    }
                }
                return ip_address;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}