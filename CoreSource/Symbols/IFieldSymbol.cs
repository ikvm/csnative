// Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Text;
namespace Microsoft.CodeAnalysis
{
    /// <summary>
    /// Represents a field in a class, struct or enum.
    /// </summary>
    public interface IFieldSymbol : ISymbol
    {
        /// <summary>
        /// If this field serves as a backing variable for an automatically generated
        /// property or a field-like event or a Primary Constructor parameter, returns that 
        /// property/event/parameter. Otherwise returns null.
        /// Note, the set of possible associated symbols might be expanded in the future to 
        /// reflect changes in the languages.
        /// </summary>
        ISymbol AssociatedSymbol { get; }

        /// <summary>
        /// Returns true if this field was declared as "const" (i.e. is a constant declaration).
        /// Also returns true for an enum member.
        /// </summary>
        bool IsConst { get; }

        /// <summary>
        /// Returns true if this field was declared as "readonly". 
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Returns true if this field was declared as "volatile". 
        /// </summary>
        bool IsVolatile { get; }

        /// <summary>
        /// Gets the type of this field.
        /// </summary>
        ITypeSymbol Type { get; }

        /// <summary>
        /// Returns false if the field wasn't declared as "const", or constant value was omitted or errorneous.
        /// True otherwise.
        /// </summary>
        bool HasConstantValue { get; }

        /// <summary>
        /// Gets the constant value of this field
        /// </summary>
        object ConstantValue { get; }

        /// <summary>
        /// Gets the list of custom modifiers, if any, associated with the field.
        /// </summary>
        ImmutableArray<CustomModifier> CustomModifiers { get; }

        /// <summary>
        /// Get the original definition of this symbol. If this symbol is derived from another
        /// symbol by (say) type substitution, this gets the original symbol, as it was defined in
        /// source or metadata.
        /// </summary>
        new IFieldSymbol OriginalDefinition { get; }
    }
}