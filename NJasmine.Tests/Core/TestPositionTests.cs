﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NJasmine;
using NJasmine.Core;
using NUnit.Framework;

namespace NJasmineTests.Core
{
    [TestFixture]
    public class TestPositionTests : PowerAssertFixture
    {
        [Test]
        public void can_be_used_with_dictionaries()
        {
            var position1 = new TestPosition(1, 2, 3);
            var position2 = new TestPosition(4, 5, 6);
            var position3 = new TestPosition(7, 8, 9);

            expect(() => position1.Equals(new TestPosition(1, 2, 3)));

            Dictionary<TestPosition, int> dictionary = new Dictionary<TestPosition, int>();

            dictionary[position1] = 1;
            dictionary[position2] = 2;
            dictionary[position3] = 3;

            expect(() => dictionary[new TestPosition(1,2,3)] == 1);
            expect(() => dictionary[new TestPosition(4,5,6)] == 2);
            expect(() => dictionary[new TestPosition(7, 8, 9)] == 3);
        }

        [Test]
        public void test_Parent()
        {
            var position = new TestPosition(1, 2, 3);

            expect(() => position.Parent.Equals(new TestPosition(1, 2)));
        }

        [Test]
        public void test_IsAncestorOf()
        {
            var position = new TestPosition(1, 2, 3);

            expect(() => position.IsAncestorOf(new TestPosition(1, 2, 3, 4)));
            expect(() => position.IsAncestorOf(new TestPosition(1, 2, 3, 4, 0, 1, 2)));
            expect(() => !position.IsAncestorOf(new TestPosition(1, 2)));
            expect(() => !position.IsAncestorOf(new TestPosition(3, 2, 1, 4)));
        }

        [Test]
        public void GetFirstChildPosition()
        {
            expect(() => new TestPosition(0).GetFirstChildPosition().Equals(new TestPosition(0, 0)));
            expect(() => new TestPosition(3, 1, 0, 10, 93).GetFirstChildPosition().Equals(new TestPosition(3, 1, 0, 10, 93, 0)));
        }

        [Test]
        public void GetNextSiblingPosition()
        {
            expect(() => new TestPosition(0).GetNextSiblingPosition().Equals(new TestPosition(1)));
            expect(() => new TestPosition(3, 1, 0, 10, 93).GetNextSiblingPosition().Equals(new TestPosition(3, 1, 0, 10, 94)));
        }

        [Test]
        public void IsInScopeFor()
        {
            expect(() => !new TestPosition(0).IsOnPathTo(new TestPosition()));

            expect(() => new TestPosition(0).IsOnPathTo(new TestPosition(0, 1)));
            expect(() => new TestPosition(0).IsOnPathTo(new TestPosition(1, 2)));
            expect(() => new TestPosition(0).IsOnPathTo(new TestPosition(5)));
            expect(() => new TestPosition(0).IsOnPathTo(new TestPosition(5, 123)));

            expect(() => !new TestPosition(0, 5).IsOnPathTo(new TestPosition(0, 2)));
            expect(() => new TestPosition(0, 5).IsOnPathTo(new TestPosition(0, 5)));
            expect(() => new TestPosition(0, 5).IsOnPathTo(new TestPosition(0, 5, 0)));
            expect(() => new TestPosition(0, 5).IsOnPathTo(new TestPosition(0, 5, 3)));
            expect(() => new TestPosition(0, 5).IsOnPathTo(new TestPosition(0, 7)));
            expect(() => new TestPosition(0, 5).IsOnPathTo(new TestPosition(0, 7, 0)));
            expect(() => new TestPosition(0, 5).IsOnPathTo(new TestPosition(0, 7, 3)));

            expect(() => !new TestPosition(1, 2, 3).IsOnPathTo(new TestPosition(0)));
            expect(() => !new TestPosition(1, 2, 3).IsOnPathTo(new TestPosition(0, 2, 3)));
            expect(() => !new TestPosition(1, 2, 3).IsOnPathTo(new TestPosition(2, 2, 3)));
            expect(() => !new TestPosition(1, 2, 3).IsOnPathTo(new TestPosition(1, 0, 3)));
            expect(() => !new TestPosition(1, 2, 3).IsOnPathTo(new TestPosition(1, 3, 3)));
            expect(() => !new TestPosition(1, 2, 3).IsOnPathTo(new TestPosition(2, 2, 2, 10)));
        }
    }
}
