﻿using System;
using System.Collections.Generic;
using System.Linq;
using ExternalLibrary.FraudDetector;

namespace ContractAnalyzerKata.ContractAnalyzer
{
    public class ContractAnalyzer
    {
        private readonly IList<Violation> _violations = new List<Violation>();
        private readonly Rule _fraudRule;
        private readonly Rule _underAgeRule = new UnderAgeRule();

        public ContractAnalyzer() : this(new FraudDetectorAdapter()) { }

        public ContractAnalyzer(IFraudDetector fraudDetector) => _fraudRule = new FraudRule(fraudDetector);

        public IEnumerable<Violation> Violations => _violations;

        public void Analyze(Contract contract)
        {
            UnderAgeRuleCheck(contract);
            FraudRuleCheck(contract);
        }

        private void FraudRuleCheck(Contract contract)
        {
            _fraudRule.Check(contract);
            if (_fraudRule.HasViolation)
            {
                _violations.Add(_fraudRule.Violation);
            }
        }

        private void UnderAgeRuleCheck(Contract contract)
        {
            _underAgeRule.Check(contract);
            if (_underAgeRule.HasViolation)
            {
                _violations.Add(_underAgeRule.Violation);
            }
        }
    }
}
