﻿using System;
using NJasmine;
using NJasmineTests.Export;
using NUnit.Framework;

namespace NJasmineTests.Specs
{
    [Explicit]
    public class expect : GivenWhenThenFixture, INJasmineInternalRequirement
    {
        public override void Specify()
        {
            it("can pass tests", delegate
            {
                expect(() => true);
            });

            it("can fail tests", delegate
            {
                expect(() => false);
            });

            describe("expect can be called during discovery", delegate
            {
                expect(() => true);
                expect(() => false);

                it("doesnt prevent discovery", delegate
                {
                });
            });
        }

        public void Verify(FixtureResult fixtureResult)
        {
            fixtureResult.failed();

            fixtureResult.hasTest("NJasmineTests.Specs.expect, can pass tests").thatSucceeds();
            fixtureResult.hasTest("NJasmineTests.Specs.expect, can fail tests").thatErrors();
            fixtureResult.hasTest("NJasmineTests.Specs.expect, expect can be called during discovery, doesnt prevent discovery").thatErrors();
        }
    }
}
