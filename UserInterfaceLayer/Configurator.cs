﻿using BusinessLogicLayer;
using BusinessLogicLayer.Services.Print;
using BusinessLogicLayer.Services.Repositories.Interfaces.ConcreteDefinitions;
using System;
using System.Collections.Generic;
using UserInterfaceLayer.Clients.Print;
using UserInterfaceLayer.Clients.Repositories.Classes.ConcreteDefinitions;
using UserInterfaceLayer.Forms.Views;

namespace UserInterfaceLayer
{
    public class Configurator
    {
        public static Container _Container;
        public static UserInfo _UserInfo;
        public static ConnectInfoClientService _ConnectInfo;
        public static Dictionary<Type, string> _HostNames;

        public Configurator(ConnectInfoClientService connectInfo)
        {
            _Container = new Container();
            _ConnectInfo = connectInfo;
            _UserInfo = new UserInfo("LoginX", "PasswordX");
            _HostNames = new Dictionary<Type, string>();

            RegisterTypes();
            FillHostNamesDictionary(GetContractsServicesInterfacesTypes());
        }

        private void RegisterTypes()
        {
            _Container.RegisterType<DetailEditor>(constructorParams: new object[]{
                new DetailRelationRepositoryClient(),
                new PrintClient()});
        }

        private IEnumerable<Type> GetContractsServicesInterfacesTypes()
        {
            return
                new List<Type>()
                {
                    typeof(IDetailRelationRepositoryService),
                    typeof(IPrintService)
                };
        }

        public void FillHostNamesDictionary(IEnumerable<Type> contractsServicesInterfacesTypes)
        {
            foreach(Type contractServiceInterfaceType in contractsServicesInterfacesTypes)
                _HostNames.Add(contractServiceInterfaceType, contractServiceInterfaceType.Name);
        }
    }
}
