﻿using System.Linq;
using NJasmine;
using NJasmine.Core;
using NJasmineTests.Export;
using NUnit.Framework;

namespace SomeOtherNamespace
{
    [Explicit]
    public class stacktrace_has_NJasmine_internal_calls_removed : GivenWhenThenFixture, INJasmineInternalRequirement
    {
        public override void Specify()
        {
            trace("note this test must be in the same namespace as other tests");

            given("some context", delegate
            {
                when("some action", delegate
                {
                    then("it fails", delegate()
                    {
                        expect(() => 1 + 2 == 4);
                    });
                });
            });
        }

        public void Verify_NJasmine_implementation(FixtureResult fixtureResult)
        {
            fixtureResult.failed();

            var stackTrace = fixtureResult.withStackTraces().Single();
            Assert.That(stackTrace, Is.Not.StringContaining("NJasmine.Core"));

            Assert.That(!TestResultUtil.PatternForNJasmineAnonymousMethod.IsMatch(stackTrace));
        }
    }
}
