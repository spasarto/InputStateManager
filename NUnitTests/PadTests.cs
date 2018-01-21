﻿// *************************************************************************** 
// This is free and unencumbered software released into the public domain.
// 
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
// 
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>
// ***************************************************************************

using InputStateManager;
using InputStateManager.Inputs.InputProviders.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Moq;
using NUnit.Framework;

namespace NUnitTests
{
    [TestFixture]
    [Category("InputStateManager.Pad")]
    public class PadTests
    {
        private InputManager input;
        private Mock<IPadInputProvider> providerMock;

        [SetUp]
        public void Setup()
        {
            providerMock = new Mock<IPadInputProvider>();
            input = new InputManager(null, null, providerMock.Object, null);
        }

        private static GamePadState IdleState => new GamePadState(new GamePadThumbSticks(Vector2.Zero, Vector2.Zero),
            new GamePadTriggers(0f, 0f), new GamePadButtons(0),
            new GamePadDPad(ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released));

        [Test]
        public void OnlyOneGamepadIsConnected()
        {
            providerMock.SetupSequence(o => o.GetState(0))
                .Returns(IdleState)
                .Returns(GamePadState.Default)
                .Returns(GamePadState.Default)
                .Returns(GamePadState.Default)
                .Returns(IdleState);
            providerMock.SetupSequence(o => o.GetState(1))
                .Returns(GamePadState.Default)
                .Returns(IdleState)
                .Returns(GamePadState.Default)
                .Returns(GamePadState.Default)
                .Returns(IdleState);
            providerMock.SetupSequence(o => o.GetState(2))
                .Returns(GamePadState.Default)
                .Returns(GamePadState.Default)
                .Returns(IdleState)
                .Returns(GamePadState.Default)
                .Returns(IdleState);
            providerMock.SetupSequence(o => o.GetState(3))
                .Returns(GamePadState.Default)
                .Returns(GamePadState.Default)
                .Returns(GamePadState.Default)
                .Returns(IdleState)
                .Returns(IdleState);
            input.Update();
            Assert.IsTrue(input.Pad.Is.Connected());
            Assert.IsFalse(input.Pad.Is.Connected(PlayerIndex.Two));
            Assert.IsFalse(input.Pad.Is.Connected(PlayerIndex.Three));
            Assert.IsFalse(input.Pad.Is.Connected(PlayerIndex.Four));
            input.Update();
            Assert.IsFalse(input.Pad.Is.Connected());
            Assert.IsTrue(input.Pad.Is.Connected(PlayerIndex.Two));
            Assert.IsFalse(input.Pad.Is.Connected(PlayerIndex.Three));
            Assert.IsFalse(input.Pad.Is.Connected(PlayerIndex.Four));
            input.Update();
            Assert.IsFalse(input.Pad.Is.Connected());
            Assert.IsFalse(input.Pad.Is.Connected(PlayerIndex.Two));
            Assert.IsTrue(input.Pad.Is.Connected(PlayerIndex.Three));
            Assert.IsFalse(input.Pad.Is.Connected(PlayerIndex.Four));
            input.Update();
            Assert.IsFalse(input.Pad.Is.Connected());
            Assert.IsFalse(input.Pad.Is.Connected(PlayerIndex.Two));
            Assert.IsFalse(input.Pad.Is.Connected(PlayerIndex.Three));
            Assert.IsTrue(input.Pad.Is.Connected(PlayerIndex.Four));
            input.Update();
            Assert.IsTrue(input.Pad.Is.Connected());
            Assert.IsTrue(input.Pad.Is.Connected(PlayerIndex.Two));
            Assert.IsTrue(input.Pad.Is.Connected(PlayerIndex.Three));
            Assert.IsTrue(input.Pad.Is.Connected(PlayerIndex.Four));
        }
    }
}