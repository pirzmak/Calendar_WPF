﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendar.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using Calendar.Model;
using System.Globalization;

namespace Calendar.ModelView.Tests
{
    [TestClass()]
    public class MainWindowViewModelTests
    {
        private MainWindowViewModel vm;
        [TestInitialize()]
        public void Initialize()
        {
            vm = new MainWindowViewModel();
        }
        [TestMethod()]
        public void NextClickTest()
        {
            vm.ActualDay = DateTime.ParseExact("01-01-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture);

            vm.NextClickCommand.Execute("");
            Assert.AreEqual(vm.ActualDay, DateTime.Parse("08-01-2000"), "01-01-2000 add 7 days is 08-01-2000");
        }
        [TestMethod()]
        public void PrevClickTest()
        {
            vm.ActualDay = DateTime.ParseExact("11-01-2000", "dd-MM-yyyy", CultureInfo.InvariantCulture);
            vm.PrevClickCommand.Execute("");
            Assert.AreEqual(vm.ActualDay, DateTime.Parse("04-01-2000"), "10-01-2000 minus 7 days is 04-01-2000");
        }
    }
}