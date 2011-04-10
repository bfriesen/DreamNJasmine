﻿using NJasmine;
using NUnit.Framework;

namespace NJasmineTests.Specs
{
    [Explicit]
    [RunExternal(false, VerificationScript = @"

param ($consoleOutput, $xmlFile);

import-module .\lib\PSUpdateXML\PSUpdateXML.psm1

update-xml $xmlFile {

    $null = get-xml -exactlyOnce ""//test-case[@name='NJasmineTests.Specs.waitUntil_waits_for_a_condition, given a condition that eventually evalutes to true, a normal expect works when no waits are left'][@result='Success']""
    $null = get-xml -exactlyOnce ""//test-case[@name='NJasmineTests.Specs.waitUntil_waits_for_a_condition, given a condition that eventually evalutes to true, a normal expect fails when waits are left'][@result='Error']""
    $null = get-xml -exactlyOnce ""//test-case[@name='NJasmineTests.Specs.waitUntil_waits_for_a_condition, given a condition that eventually evalutes to true, waitUntil will try multiple times'][@result='Success']""
    $null = get-xml -exactlyOnce ""//test-case[@name='NJasmineTests.Specs.waitUntil_waits_for_a_condition, waitUntil can be called during discovery, doesnt prevent discovery'][@result='Error']""
}
")]
    public class waitUntil_waits_for_a_condition : GivenWhenThenFixture
    {
        public int WaitsLeft;

        public bool Ready()
        {
            return WaitsLeft-- <= 0;
        }

        public override void Specify()
        {
            describe("given a condition that eventually evalutes to true", delegate
            {
                it("a normal expect works when no waits are left", delegate
                {
                    WaitsLeft = 0;
                    expect(() => Ready());
                });

                it("a normal expect fails when waits are left", delegate
                {
                    WaitsLeft = 1;
                    expect(() => Ready());
                });

                it("waitUntil will try multiple times", delegate
                {
                    setWaitTimeouts(100, 5);

                    WaitsLeft = 1;

                    waitUntil(() => Ready());

                    waitUntil(() => Ready(), 2000);
                });
            });

            describe("waitUntil can be called during discovery", delegate
            {
                setWaitTimeouts(5, 3);

                waitUntil(() => true);
                waitUntil(() => false);

                it("doesnt prevent discovery", delegate
                {
                    
                });
                
            });
        }
    }
}
