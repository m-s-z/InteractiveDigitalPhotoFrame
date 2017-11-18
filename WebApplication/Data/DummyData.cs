using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class DummyData
    {
        public static List<Account> getAccounts()
        {
            List<Device> devices1 = new List<Device>()
            {
                new Device("Grandma's tablet"),
                new Device("Mati's tablet")
            };
            List<Device> devices2 = new List<Device>()
            {
                new Device("Grandma's tablet"),
                new Device("Mik's phone")
            };
            
            List<Account> accounts = new List<Account>()
            {
                new Account("Mati","aaa",devices1),
                new Account("Mik", "qqq",devices2)
            };
            return accounts;
        }
        public static List<Device> getDevices()
        {
            List<Account> accounts1 = new List<Account>()
            {
                new Account("Mati","aaa"),
                new Account("Mik", "qqq")
            };
            List<Account> accounts2 = new List<Account>()
            {
                new Account("Mik", "qqq")
            };
            List<Account> accounts3 = new List<Account>()
            {
                new Account("Mati", "aaa")
            };
            List<Device> devices = new List<Device>()
            {
                new Device("Grandma's tablet", accounts1),
                new Device("Mik's phone", accounts2),
                new Device("Mati's tablet", accounts3)
            };
            return devices;
        }
        public static List<Cloud> getClouds(ApplicationContext context)
        {
            List<Cloud> clouds = new List<Cloud>()
            {
                new Cloud("aa",ProviderType.DropBox,"mik",context.Accounts.First( p => p.Login == "mik").Id),
                new Cloud("aa",ProviderType.Flicker,"mik",context.Accounts.First( p => p.Login == "mikflick").Id),
                new Cloud("aa",ProviderType.DropBox,"mati",context.Accounts.First( p => p.Login == "mati").Id),
            };
            return clouds;
        }
        public static List<Folder> getFolders(ApplicationContext context)
        {
            List<Folder> folders = new List<Folder>()
            {
                new Folder("winter Photos",context.Devices.First(p => p.Name == "Mik's phone").DeviceId,context.Clouds.First(p => p.Login == "mikflick").CloudId),
                new Folder("winter Photos",context.Devices.First(p => p.Name == "Grandma's tablet").DeviceId,context.Clouds.First(p => p.Login == "mikflick").CloudId),
                new Folder("Summer Photos",context.Devices.First(p => p.Name == "Mik's phone").DeviceId,context.Clouds.First(p => p.Login == "mik").CloudId),
                new Folder("Birthday Photos",context.Devices.First(p => p.Name == "Mati's tablet").DeviceId,context.Clouds.First(p => p.Login == "mati").CloudId),
                new Folder("Censored Birthday Photos",context.Devices.First(p => p.Name == "Grandma's tablet").DeviceId,context.Clouds.First(p => p.Login == "mati").CloudId),
            };
            return folders;
        }
    }
}