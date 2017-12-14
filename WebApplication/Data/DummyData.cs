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
                new Device(1,"Grandma's tablet"),
                new Device(3,"Mati's tablet")
            };
            List<Device> devices2 = new List<Device>()
            {
                new Device(1,"Grandma's tablet"),
                new Device(2,"Mik's phone")
            };
            
            List<Account> accounts = new List<Account>()
            {
                new Account(1,"Mati","aaa",devices1),
                new Account(2,"Mik", "qqq",devices2)
            };
            return accounts;
        }
        public static List<Device> getDevices()
        {
            List<Account> accounts1 = new List<Account>()
            {
                new Account(1,"Mati","aaa"),
                new Account(2,"Mik", "qqq")
            };
            List<Account> accounts2 = new List<Account>()
            {
                new Account(2,"Mik", "qqq")
            };
            List<Account> accounts3 = new List<Account>()
            {
                new Account(1,"Mati","aaa"),
            };
            List<Device> devices = new List<Device>()
            {
                new Device(1,"Grandma's tablet", accounts1),
                new Device(2,"Mik's phone", accounts2),
                new Device(3,"Mati's tablet", accounts3)
            };
            return devices;
        }
        public static List<Cloud> getClouds(ApplicationContext context)
        {
            List<Cloud> clouds = new List<Cloud>()
            {
                new Cloud(1,"aa",ProviderType.DropBox,"mik",context.Accounts.First( p => p.Login == "Mik").Id),
                new Cloud(2,"aa",ProviderType.Flicker,"mikflick",context.Accounts.First( p => p.Login == "Mik").Id),
                new Cloud(3,"aa",ProviderType.DropBox,"mati",context.Accounts.First( p => p.Login == "Mati").Id),
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

        public static List<DeviceName> getDeviceNames(ApplicationContext context)
        {
            List<DeviceName> deviceNames = new List<DeviceName>()
            {
                new DeviceName(1, context.Accounts.First(a=> a.Login == "Mati"),context.Devices.First(p => p.Name == "Grandma's tablet"),"Grandma's Tablet"),
                new DeviceName(1, context.Accounts.First(a=> a.Login == "Mik"),context.Devices.First(p => p.Name == "Grandma's tablet"),"Mom's Tablet"),
            };
            return deviceNames;
        }
    }
}