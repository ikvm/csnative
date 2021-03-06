﻿// Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Microsoft.CodeAnalysis.Syntax
{
    internal abstract class AbstractWarningStateMap
    {
        /// <summary>
        /// List of entries sorted in source order, each of which captures a
        /// position in the supplied syntax tree and the set of diagnostics (warnings)
        /// whose reporting should either be suppressed or enabled at this position.
        /// </summary>
        private readonly WarningStateMapEntry[] warningStateMapEntries;

        protected AbstractWarningStateMap(SyntaxTree syntaxTree)
        {
            warningStateMapEntries = CreateWarningStateMapEntries(syntaxTree);
        }

        /// <summary>
        /// Returns list of entries sorted in source order, each of which captures a
        /// position in the supplied syntax tree and the set of diagnostics (warnings)
        /// whose reporting should either be suppressed or enabled at this position.
        /// </summary>
        protected abstract WarningStateMapEntry[] CreateWarningStateMapEntries(SyntaxTree syntaxTree);

        /// <summary>
        /// Returns the reporting state for the supplied diagnostic id at the supplied position
        /// in the associated syntax tree.
        /// </summary>
        public ReportDiagnostic GetWarningState(string id, int position)
        {
            var entry = GetEntryAtOrBeforePosition(position);

            ReportDiagnostic state;
            if (entry.SpecificWarningOption.TryGetValue(id, out state))
            {
                return state;
            }

            return entry.GeneralWarningOption;
        }

        /// <summary>
        /// Gets the entry with the largest position less than or equal to supplied position.
        /// </summary>
        private WarningStateMapEntry GetEntryAtOrBeforePosition(int position)
        {
            Debug.Assert(warningStateMapEntries != null && warningStateMapEntries.Length > 0);
            int r = Array.BinarySearch(warningStateMapEntries, new WarningStateMapEntry(position));
            return warningStateMapEntries[r >= 0 ? r : ((~r) - 1)];
        }

        /// <summary>
        /// Struct that represents an entry in the warning state map. Sorts by position in the associated syntax tree.
        /// </summary>
        protected struct WarningStateMapEntry : IComparable<WarningStateMapEntry>
        {
            // 0-based position in the associated syntax tree
            public readonly int Position;

            // the general option applicable to all warnings, accumulated of all #pragma up to the current Line.
            public readonly ReportDiagnostic GeneralWarningOption;

            // the mapping of the specific warning to the option, accumulated of all #pragma up to the current Line.
            public readonly ImmutableDictionary<string, ReportDiagnostic> SpecificWarningOption;

            public WarningStateMapEntry(int position)
            {
                this.Position = position;
                this.GeneralWarningOption = ReportDiagnostic.Default;
                this.SpecificWarningOption = ImmutableDictionary.Create<string, ReportDiagnostic>();
            }

            public WarningStateMapEntry(int position, ReportDiagnostic general, ImmutableDictionary<string, ReportDiagnostic> specific)
            {
                this.Position = position;
                this.GeneralWarningOption = general;
                this.SpecificWarningOption = specific ?? ImmutableDictionary.Create<string, ReportDiagnostic>();
            }

            public int CompareTo(WarningStateMapEntry other)
            {
                return this.Position - other.Position;
            }
        }
    }
}
