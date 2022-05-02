using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Presenters.Interfaces.Repositories.ConcreteDefinitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RestService.Controllers.ConcreteDefinitions
{
    [Route("api/detail")]
    [ApiController]
    public class DetailRelationRepositoryController :
        AbstractRepositoryController<DetailRelationEntity, long, IDetailRelationRepositoryPresenter>
    {
        public DetailRelationRepositoryController(IDetailRelationRepositoryPresenter detailRelationRepositoryPresenter) : base(detailRelationRepositoryPresenter) {}

        [HttpPost("add")]
        [Authorize(Roles = "admin")]
        public async Task<JsonResult> Add()
        {
            string json = GetPostBody();
            if (string.IsNullOrEmpty(json))
                return Json(string.Empty); // Нужно придумать описание ошибки.

            var pattern = new {
                selectedDetail = new DetailRelationEntity(),
                isRoot = new bool(),
                name = string.Empty,
                amount = string.Empty };
            var paramsObj = JsonConvert.DeserializeAnonymousType(json, pattern);

            string warningMessage = await Task.Run(() => _repositoryPresenter.Add(paramsObj?.selectedDetail, (paramsObj?.isRoot ?? false), paramsObj?.name, paramsObj?.amount));

            return Json(warningMessage);
        }

        [HttpPost("edit")]
        [Authorize(Roles = "admin")]
        public async Task<JsonResult> Edit()
        {
            string json = GetPostBody();
            if (string.IsNullOrEmpty(json))
                return Json(string.Empty); // Нужно придумать описание ошибки.

            var pattern = new {
                selectedDetail = new DetailRelationEntity(),
                name = string.Empty,
                amount = string.Empty };
            var paramsObj = JsonConvert.DeserializeAnonymousType(json, pattern);

            string warningMessage = await Task.Run(() => _repositoryPresenter.Edit(paramsObj?.selectedDetail, paramsObj?.name, paramsObj?.amount));

            return Json(warningMessage);
        }
    }
}
