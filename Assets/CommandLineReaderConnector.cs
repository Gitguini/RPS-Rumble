using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Transports.UTP;

public class CommandLineReaderConnector : MonoBehaviour
{
    /*
     * used/adapted from the unity multiplayer docs
     */

    private NetworkManager netManager;

    //exactly once for each instance, upon its opening
    void Start()
    {
        //read and parse command line arguments for connection mode and use
        //NetworkManager.Singleton.StartHost / StartClient
        //... and possibly
        //

        netManager = GetComponentInParent<NetworkManager>();

        if (Application.isEditor) return;

        var args = GetCommandLineArgs();

        if (args.TryGetValue("-ip", out string ip))
        {
            var trans = gameObject.transform.parent.GetComponent<UnityTransport>();
            ushort port = trans.ConnectionData.Port;
            netManager.GetComponent<UnityTransport>().SetConnectionData(ip, port);
        }

        if (args.TryGetValue("-mode", out string mode))
        {
            switch(mode)
            {
                case "server":
                    netManager.StartServer();
                    break;
                case "host":
                    netManager.StartHost();
                    break;
                case "client":
                    netManager.StartClient();
                    break;
                default:
                    netManager.StartClient();
                    break;
            }
        } else
        {
            netManager.StartClient();
        }


    }

    private Dictionary<string, string> GetCommandLineArgs()
    {
        Dictionary<string, string> argDictionary = new Dictionary<string, string>();

        var args = System.Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; ++i)
        {
            var thisArg = args[i].ToLower();
            if(thisArg.StartsWith("-"))
            {
                var value = i < args.Length - 1 ? args[i + 1].ToLower() : null;
                value = (value?.StartsWith("-") ?? false) ? null : value;

                argDictionary.Add(thisArg, value);
            }
        }

        return argDictionary;
    }

}
