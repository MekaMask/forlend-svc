using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forlend.Dac;
using Forlend.Models;

namespace Forlend.Facade
{
    public class Facade : IFacade
    {
        private IDac dac;

        public Facade(IDac dac)
        {
            this.dac = dac;
        }
        public void CreateItem(CreateItemRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LockerId))
                {
                    throw new Exception("กรุณาระบุชื่อ หรือ locker");
                }

                var locker = dac.GetLocker(x => x._id == request.LockerId);
                Item newItem = new Item()
                {

                    _id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    CreateDate = DateTime.UtcNow,
                    DeleteDate = null,
                    Canlend = true,
                    CanSendBack = false,
                    locker = locker
                };

                LendItem newLenItem = new LendItem()
                {
                    _id = Guid.NewGuid().ToString(),
                    ItemId = newItem._id,
                    CreateDate = DateTime.UtcNow,
                    Log = new List<LendLog>(),
                };


                dac.CreateItem(newItem);
                dac.CreateLendItem(newLenItem);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CreateLocker(Locker locker)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(locker.Row) || string.IsNullOrWhiteSpace(locker.Col))
                {
                    throw new Exception("Row  Col ผิดพลาด");
                }

                locker._id = Guid.NewGuid().ToString();
                locker.CreateDate = DateTime.UtcNow;

                dac.CreateLocker(locker);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public void DeleteItem(string itemid)
        {
            try
            {
                var item = dac.GetItem(x => x._id == itemid);
                item.DeleteDate = DateTime.UtcNow;

                dac.UpdateItem(item);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public Item GetItem(string itemid)
        {
            try
            {
                var item = dac.GetItem(x => x._id == itemid);

                return item;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public LendItem GetLendItem(string itemid)
        {
            try
            {
                var data = dac.GetLendItem(x => x.ItemId == itemid);

                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void LendItem(string itemid, string lendby, string witnessBy)
        {
            var item = dac.GetItem(x => x._id == itemid);

            item.Canlend = false;
            item.CanSendBack = true;

            var lenitem = dac.GetLendItem(x => x.ItemId == item._id);
            LendLog newLog = new LendLog()
            {
                ActionBy = lendby,
                ActionDate = DateTime.UtcNow,
                Status = "lend",
                WitnessBy = witnessBy,
            };
            var logs = lenitem.Log.ToList();
            logs.Add(newLog);
            lenitem.Log = logs;


            dac.UpdateItem(item);
            dac.UpdtaeLendItem(lenitem);
        }

        public IEnumerable<Item> ListItemForLend(string username)
        {
            try
            {
                var data = dac.ListItems(x => x.DeleteDate == null).OrderByDescending(x => x.CreateDate);

                foreach (var item in data)
                {
                    if (item.CanSendBack)
                    {
                        var lenitem = dac.GetLendItem(x => x.ItemId == item._id);
                        var action = lenitem.Log.Where(x => x.Status == "lend").OrderByDescending(x=>x.ActionDate).FirstOrDefault();
                        if (action.ActionBy == username || action.WitnessBy == username)
                        {
                            item.CanSendBack = true;
                        }
                        else
                        {
                            item.CanSendBack = false;
                        }
                    }
                }

                return data.ToList();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public IEnumerable<Locker> ListLocker()
        {
            try
            {
                var data = dac.ListLockers(x => true);
                return data.ToList().OrderByDescending(x => x.CreateDate);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public void SendbackItem(string itemid, string sendbackby, string witnessBy)
        {
            var item = dac.GetItem(x => x._id == itemid);

            item.Canlend = true;
            item.CanSendBack = false;

            var lenitem = dac.GetLendItem(x => x.ItemId == item._id);
            LendLog newLog = new LendLog()
            {
                ActionBy = sendbackby,
                ActionDate = DateTime.UtcNow,
                Status = "sendback",
                WitnessBy = witnessBy,
            };
            var logs = lenitem.Log.ToList();
            logs.Add(newLog);
            lenitem.Log = logs;

            dac.UpdateItem(item);
            dac.UpdtaeLendItem(lenitem);
        }

        public void UpdateItem(UpdateItemRequest request)
        {
            try
            {
                var item = dac.GetItem(x => x._id == request.Itemid);
                item.Name = request.Name;
                var locker = dac.GetLocker(x => x._id == request.LockerId);
                item.locker = locker;

                dac.UpdateItem(item);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
