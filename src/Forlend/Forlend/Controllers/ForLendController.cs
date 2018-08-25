using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forlend.Facade;
using Forlend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forlend.Controllers
{
    [Route("api/[controller]")]
    public class ForLendController : Controller
    {
        private IFacade facade;

        public ForLendController(IFacade facade)
        {
            this.facade = facade;
        }

        [Route("GetLockers")]
        [HttpGet]
        public IEnumerable<Locker> GetLockers()
        {

            return facade.ListLocker();

        }

        [Route("GetItems/{username}")]
        [HttpGet]
        public IEnumerable<Item> GetItems(string username)
        {

            return facade.ListItemForLend(username);

        }

        [Route("CreateItem")]
        [HttpPost]
        public void CreateItem([FromBody]CreateItemRequest request)
        {
            facade.CreateItem(request);
        }

        [Route("CreateLocker")]
        [HttpPost]
        public void CreateLocker([FromBody]Locker locker)
        {
            facade.CreateLocker(locker);
        }

        [Route("UpdateItem")]
        [HttpPost]
        public void UpdateItem([FromBody]UpdateItemRequest request)
        {
            facade.UpdateItem(request);
        }

        [Route("DeleteItem")]
        [HttpGet]
        public void DeleteItem(string itemId)
        {
            facade.DeleteItem(itemId);
        }

        [Route("GetItem/{itemid}")]
        [HttpGet]
        public Item GetItem(string itemid)
        {
            return facade.GetItem(itemid);
        }

        [Route("GetLendItem/{itemid}")]
        [HttpGet]
        public LendItem GetLendItem(string itemid)
        {
            return facade.GetLendItem(itemid);
        }

        [Route("LendItem")]
        [HttpPost]
        public void LendItem([FromBody]LendAndSendBackItemRequest request)
        {
            facade.LendItem(request.ItemId, request.LendBy, request.WitnessBy);
        }

        [Route("SendBackItem")]
        [HttpPost]
        public void SendBackItem([FromBody]LendAndSendBackItemRequest request)
        {
            facade.SendbackItem(request.ItemId, request.LendBy, request.WitnessBy);
        }

    }
}