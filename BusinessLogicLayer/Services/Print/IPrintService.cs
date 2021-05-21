﻿using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.ServiceModel;

namespace BusinessLogicLayer.Services.Print
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IPrintService
    {
        [OperationContract]
        byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string warningMessage);
    }
}
