using Forlend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Forlend.Dac
{
    public interface IDac
    {
        void CreateLocker(Locker locker);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        IEnumerable<Locker> ListLockers(Expression<Func<Locker, bool>> expression);
        IEnumerable<Item> ListItems(Expression<Func<Item, bool>> expression);
        Item GetItem(Expression<Func<Item, bool>> expression);
        Locker GetLocker(Expression<Func<Locker, bool>> expression);

        void CreateLendItem(LendItem lendItem);
        void UpdtaeLendItem(LendItem lendItem);
        LendItem GetLendItem(Expression<Func<LendItem, bool>> expression);
    }
}
