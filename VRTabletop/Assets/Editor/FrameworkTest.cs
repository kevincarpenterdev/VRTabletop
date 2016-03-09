using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using VRTabletop.Clients;
using VRTabletop.Communications;
using VRTabletop.Pawns;

[TestFixture]
public class FrameworkTest {

    [Test]
    public void TestTest() {
        //Arrange
        var gameObject = new GameObject();

        //Act
        //Try to rename the GameObject
        var newGameObjectName = "My game object";
        gameObject.name = newGameObjectName;

        //Assert
        //The object has a new name
        Assert.AreEqual(newGameObjectName, gameObject.name);
    }
    [Test]
    public void OrderTest() {
     //Format and send orders
    }

    [Test]
    public void PawnTest() {
        //Pass order from Source to pawn
    }
}
