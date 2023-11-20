using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public readonly struct NetworkInterfaceInfo
{
    public NetworkInterfaceInfo(string name, string ip)
    {
        InterfaceName = name;
        IPString = ip;
    }

    public string InterfaceName { get; }
    public string IPString { get; }
}

class Utility
{
    public static List<NetworkInterfaceInfo> Networks
    {
        get
        {
            var values = new List<NetworkInterfaceInfo>();

            // All interfaces that are up and have a default gateway
            var validInterfaces =
                NetworkInterface.GetAllNetworkInterfaces()
                .Where((nic) => nic.OperationalStatus == OperationalStatus.Up)
                .Where((nic) => nic.GetIPProperties()?.GatewayAddresses?.Count > 0);

            foreach (var nic in validInterfaces)
            {
                var validIP = nic.GetIPProperties().UnicastAddresses
                    .Where((ip) => ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .Select((ip) => ip.Address.ToString())
                    .DefaultIfEmpty("0.0.0.0").First();

                //Debug.Log($"{nic.Name} {validIP.Address.ToString()}");
                values.Add(new NetworkInterfaceInfo(nic.Name, validIP));
            }

            return values;
        }
    }
}
