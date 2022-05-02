using BusinessLogicLayer.LogicMain.Presenters.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RestService.Controllers
{
    [ApiController]
    public class AbstractRepositoryController<TEntity, TId, TInterfaceRepositoryPresenter> : AbstractController
            where TInterfaceRepositoryPresenter : IAbstractRepositoryPresenter<TEntity, TId>
    {
        protected readonly TInterfaceRepositoryPresenter _repositoryPresenter;

        public AbstractRepositoryController(TInterfaceRepositoryPresenter repositoryPresenter) => _repositoryPresenter = repositoryPresenter;

        [HttpGet("all")]
        [Authorize(Roles = "admin")]
        public async Task<JsonResult> GetAll() => Json(await Task.Run(() => _repositoryPresenter.AbstractRepositoryManager.GetAll().ToArray()));

        [HttpPost("delete")]
        [Authorize(Roles = "admin")]
        public async Task<JsonResult> Delete()
        {
            string json = GetPostBody();
            if (string.IsNullOrEmpty(json))
                return Json(string.Empty); // Нужно придумать описание ошибки.

            TEntity? selectedDetail = JsonConvert.DeserializeObject<TEntity>(json);

            string? warningMessage = null;
            if (null != selectedDetail)
                warningMessage = await Task.Run(() => _repositoryPresenter.Delete(selectedDetail));

            return Json(warningMessage);
        }
    }
}
