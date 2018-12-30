﻿using System;
using System.Collections.Generic;
using System.Linq;
using ExternalLibrary.FraudDetector;

namespace ContractAnalyzerKata.ContractAnalyzer
{
    public class ContractAnalyzer
    {
        private readonly IList<Violation> _violations;
        private readonly IFraudDetector _fraudDetector;

        public ContractAnalyzer() : this(new FraudDetectorAdapter())
        {
        }

        public ContractAnalyzer(IFraudDetector fraudDetector)
        {
            _violations = new List<Violation>();
            _fraudDetector = fraudDetector;
        }

        public IEnumerable<Violation> Violations => _violations;

        public void Analyze(Contract contract)
        {
            if (contract.User.DateOfBirth > DateTime.Today.AddYears(-18))
            {
                _violations.Add(new UnderAgeViolation());
            }

            if (_fraudDetector.IsFraudDetected(contract))
            {
                _violations.Add(new FraudViolation());
            }
        }
    }
}
