using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        //IApp app;
        //Platform platform;

        //public Tests(Platform platform)
        //{
        //    this.platform = platform;
        //}

        //[SetUp]
        //public void BeforeEachTest()
        //{
        //    app = ConfigureApp.Android
        //            .InstalledApp("com.companyname.awesomeapp")
        //            .StartApp();
        //}

        //[Test]
        //public void ShouldBeAbleToRegister()
        //{
        //    // Arrange 
        //    app.WaitForElement("NewButton");
        //    app.Tap("NewButton");

        //    app.WaitForElement("FirstNameBox");
        //    app.Tap("FirstNameBox");
        //    app.EnterText("Marcelo");
        //    app.DismissKeyboard();

        //    app.Tap("AgeBox");
        //    app.EnterText("23");
        //    app.DismissKeyboard();

        //    // Act
        //    app.Tap("ConfirmButton");
        //    app.WaitForElement("AccountCreated");

        //    // Assert
        //    bool result = app.Query(e => e.Marked("AccountCreated")).Any();

        //    Assert.IsTrue(result);
        //}

        //[Test]
        //public void ShouldBeAbleToDelete()
        //{
        //    // Arrange 
        //    app.WaitForElement("UserBox");
        //    app.Tap("UserBox");
        //    app.WaitForElement("DeleteButton");

        //    // Act
        //    app.Tap("DeleteButton");
        //    app.WaitForElement("UserDeleted");

        //    // Assert
        //    bool result = app.Query(e => e.Marked("UserDeleted")).Any();

        //    Assert.IsTrue(result);
        //}

        //[Test]
        //public void ShouldBeAbleToUpdate()
        //{
        //    // Arrange 
        //    app.WaitForElement("UserBox");
        //    app.Tap("UserBox");
        //    app.WaitForElement("UpdateButton");

        //    app.Tap("UpdateButton");
        //    app.WaitForElement("SaveBox");

        //    app.Tap("FirstNameBox");
        //    app.ClearText();
        //    app.EnterText("Gustavo");
        //    app.DismissKeyboard();

        //    app.Tap("SurnameBox");
        //    app.ClearText();
        //    app.EnterText("Silva");
        //    app.DismissKeyboard();

        //    app.Tap("AgeBox");
        //    app.ClearText();
        //    app.EnterText("35");
        //    app.DismissKeyboard();

        //    // Act
        //    app.Tap("SaveButton");
        //    app.WaitForElement("UserSaved");

        //    // Assert
        //    bool result = app.Query(e => e.Marked("UserSaved")).Any();

        //    Assert.IsTrue(result);
        //}
    }
}
