using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class GMTests {
    [Test]
    public void ListTests() {
     //Ensure list functions are ok   
    }

    [Test]
    public void RecieverTest() {
        //Ensure that the GM Correctly grabs data from the server and distributes it to pawns
    }
    [Test]
    public void SenderTests() {
        //Ensure that the GM grabs the order correctly from the client and sends it
    }
    [Test]
    public void TurnTester() {
        //ensure that turns are properly passed
    }
}
