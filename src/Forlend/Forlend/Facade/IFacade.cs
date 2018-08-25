using Forlend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forlend.Facade
{
    public interface IFacade
    {
        void CreateLocker(Locker locker);
        void CreateItem(CreateItemRequest request);
        IEnumerable<Item> ListItemForLend(string username);
        IEnumerable<Locker> ListLocker();

        Item GetItem(string itemid);
        void UpdateItem(UpdateItemRequest request);
        void DeleteItem(string itemid);

        void LendItem(string itemid, string lendby, string witnessBy);
        void SendbackItem(string itemid, string sendbackby, string witnessBy);

        LendItem GetLendItem(string itemid);
    }
}
