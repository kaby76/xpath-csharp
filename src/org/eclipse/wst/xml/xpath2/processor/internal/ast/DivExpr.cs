﻿/// <summary>
///*****************************************************************************
/// Copyright (c) 2005, 2011 Andrea Bittau, University College London, and others
/// All rights reserved. This program and the accompanying materials
/// are made available under the terms of the Eclipse Public License 2.0
/// which accompanies this distribution, and is available at
/// https://www.eclipse.org/legal/epl-2.0/
/// 
/// SPDX-License-Identifier: EPL-2.0
/// 
/// Contributors:
///     Andrea Bittau - initial API and implementation from the PsychoPath XPath 2.0 
///     Mukul Gandhi - bug 280798 - PsychoPath support for JDK 1.4
/// ******************************************************************************
/// </summary>

namespace org.eclipse.wst.xml.xpath2.processor.@internal.ast
{

	/// <summary>
	/// Class for Division expressions.
	/// </summary>
	public class DivExpr : BinExpr
	{
		/// <summary>
		/// Constructor for DivExpr.
		/// </summary>
		/// <param name="l">
		///            input1 xpath expression/variable. </param>
		/// <param name="r">
		///            input2 xpath expression/variable. </param>
		public DivExpr(Expr l, Expr r) : base(l, r)
		{
		}

		/// <summary>
		/// Support for Visitor interface.
		/// </summary>
		/// <returns> Result of Visitor operation. </returns>
		public override object accept(XPathVisitor v)
		{
			return v.visit(this);
		}
	}

}