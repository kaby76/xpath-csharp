﻿using System.Collections;
using System.Collections.Generic;

/// <summary>
///*****************************************************************************
/// Copyright (c) 2009, 2011 Jesper Moller, and others
/// All rights reserved. This program and the accompanying materials
/// are made available under the terms of the Eclipse Public License 2.0
/// which accompanies this distribution, and is available at
/// https://www.eclipse.org/legal/epl-2.0/
/// 
/// SPDX-License-Identifier: EPL-2.0
/// 
/// Contributors:
///     Jesper Moller - initial API and implementation
/// ******************************************************************************
/// </summary>

namespace org.eclipse.wst.xml.xpath2.processor
{

	/// <summary>
	/// Service provider interface for looking up collations from within the dynamic context.
	/// @since 1.1
	/// </summary>
	/// @deprecated Use org.eclipse.wst.xml.xpath2.api.CollationProvider instead 
	public interface CollationProvider
	{
		/// <summary>
		/// Gets the named collator. W3C does not define collation names (yet?) so we are constrained to using an
		/// implementation-defined naming scheme.
		/// </summary>
		/// <param name="name"> A URI designating the collation to use </param>
		/// <returns> The collation to use, or null if no such collation exists by this provider </returns>
		IComparer<string> get_collation(string name);
	}

}