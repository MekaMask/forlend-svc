using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Forlend.Dac;
using Forlend.Models;
using Moq;
using Xunit;
using FluentAssertions;

namespace Forlend.Test
{
    public class ListItemForLendTest
    {
        Mock<IDac> dac { get; set; }
        Forlend.Facade.Facade facade { get; set; }

        public ListItemForLendTest()
        {
            var mock = new MockRepository(MockBehavior.Strict);

            dac = mock.Create<IDac>();

            facade = new Forlend.Facade.Facade(dac.Object);
        }

        [Theory(DisplayName = "ดึงข้อมูลของที่ต้องการยืม แสดงสถานะถูกต้อง")]
        [InlineData("ake")]
        public void GetItemForRent(string username)
        {
            List<Item> itemList = new List<Item>()
            {
                new Item(){
                    _id ="01" ,
                    Name ="Pen",
                    locker = new Locker(){
                        _id ="01",
                        Name ="Asset",
                        Col ="9",
                        Row = "65",
                        CreateDate = DateTime.MinValue
                    },
                    Canlend = true,
                    CanSendBack = false,
                    DeleteDate = null,
                    CreateDate = DateTime.MaxValue
                },
            };

            List<LendItem> lendItem = new List<LendItem>()
            {
                new LendItem(){
                    _id ="0009",
                    ItemId ="01",
                    CreateDate = DateTime.MinValue,
                    Log = new List<LendLog>(),
                },
            };

            dac.Setup(dac => dac.ListItems(It.IsAny<Expression<Func<Item, bool>>>()))
                .Returns<Expression<Func<Item, bool>>>((expression) => itemList.Where(expression.Compile()));

            dac.Setup(dac => dac.GetLendItem(It.IsAny<Expression<Func<LendItem, bool>>>()))
               .Returns<Expression<Func<LendItem, bool>>>((expression) => lendItem.FirstOrDefault(expression.Compile()));


            var result = facade.ListItemForLend(username);

            var expected = new List<Item>()
            {
                new Item(){
                    _id ="01" ,
                    Name ="Pen",
                    locker = new Locker(){
                        _id ="01",
                        Name ="Asset",
                        Col ="9",
                        Row = "65",
                        CreateDate = DateTime.MinValue
                    },
                    Canlend = true,
                    CanSendBack = false,
                    DeleteDate = null,
                    CreateDate = DateTime.MaxValue
                },
            };

            expected.Should().BeEquivalentTo(result, option =>
                option.Excluding(x => x.CreateDate));

        }

        [Theory(DisplayName = "ดึงข้อมูลของที่ต้องการยืม แสดงสถานะถูกต้อง มีคนยืมแล้วไม่สามารถยืมได้")]
        [InlineData("ake")]
        public void GetItemForRentCantlend(string username)
        {
            List<Item> itemList = new List<Item>()
            {
                new Item(){
                    _id ="01" ,
                    Name ="Pen",
                    locker = new Locker(){
                        _id ="01",
                        Name ="Asset",
                        Col ="9",
                        Row = "65",
                        CreateDate = DateTime.MinValue
                    },
                    Canlend = false,
                    CanSendBack = true,
                    DeleteDate = null,
                    CreateDate = DateTime.MaxValue
                },
            };

            List<LendItem> lendItem = new List<LendItem>()
            {
                new LendItem(){
                    _id ="0009",
                    ItemId ="01",
                    CreateDate = DateTime.MinValue,
                    Log = new List<LendLog>() {
                        new LendLog(){Status ="lend",WitnessBy ="au",ActionBy ="nook",ActionDate = DateTime.MinValue },
                    },
                },
            };

            dac.Setup(dac => dac.ListItems(It.IsAny<Expression<Func<Item, bool>>>()))
                .Returns<Expression<Func<Item, bool>>>((expression) => itemList.Where(expression.Compile()));

            dac.Setup(dac => dac.GetLendItem(It.IsAny<Expression<Func<LendItem, bool>>>()))
               .Returns<Expression<Func<LendItem, bool>>>((expression) => lendItem.FirstOrDefault(expression.Compile()));


            var result = facade.ListItemForLend(username);

            var expected = new List<Item>()
            {
                new Item(){
                    _id ="01" ,
                    Name ="Pen",
                    locker = new Locker(){
                        _id ="01",
                        Name ="Asset",
                        Col ="9",
                        Row = "65",
                        CreateDate = DateTime.MinValue
                    },
                    Canlend = false,
                    CanSendBack = false,
                    DeleteDate = null,
                    CreateDate = DateTime.MaxValue
                },
            };

            expected.Should().BeEquivalentTo(result, option =>
                option.Excluding(x => x.CreateDate));

        }

        [Theory(DisplayName = "ดึงข้อมูลของที่ต้องการยืม แสดงสถานะถูกต้อง เป็น 1 ในคนยืม สามารถคืนได้")]
        [InlineData("au")]
        public void GetItemForRentCanlend(string username)
        {
            List<Item> itemList = new List<Item>()
            {
                new Item(){
                    _id ="01" ,
                    Name ="Pen",
                    locker = new Locker(){
                        _id ="01",
                        Name ="Asset",
                        Col ="9",
                        Row = "65",
                        CreateDate = DateTime.MinValue
                    },
                    Canlend = false,
                    CanSendBack = true,
                    DeleteDate = null,
                    CreateDate = DateTime.MaxValue
                },
            };

            List<LendItem> lendItem = new List<LendItem>()
            {
                new LendItem(){
                    _id ="0009",
                    ItemId ="01",
                    CreateDate = DateTime.MinValue,
                    Log = new List<LendLog>() {
                        new LendLog(){Status ="lend",WitnessBy ="au",ActionBy ="nook",ActionDate = DateTime.MinValue },
                    },
                },
            };

            dac.Setup(dac => dac.ListItems(It.IsAny<Expression<Func<Item, bool>>>()))
                .Returns<Expression<Func<Item, bool>>>((expression) => itemList.Where(expression.Compile()));

            dac.Setup(dac => dac.GetLendItem(It.IsAny<Expression<Func<LendItem, bool>>>()))
               .Returns<Expression<Func<LendItem, bool>>>((expression) => lendItem.FirstOrDefault(expression.Compile()));


            var result = facade.ListItemForLend(username);

            var expected = new List<Item>()
            {
                new Item(){
                    _id ="01" ,
                    Name ="Pen",
                    locker = new Locker(){
                        _id ="01",
                        Name ="Asset",
                        Col ="9",
                        Row = "65",
                        CreateDate = DateTime.MinValue
                    },
                    Canlend = false,
                    CanSendBack = true,
                    DeleteDate = null,
                    CreateDate = DateTime.MaxValue
                },
            };

            expected.Should().BeEquivalentTo(result, option =>
                option.Excluding(x => x.CreateDate));

        }
    }
}
