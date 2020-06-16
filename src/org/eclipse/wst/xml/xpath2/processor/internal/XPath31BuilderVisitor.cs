﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using BigDecimal = java.math.BigDecimal;
using BigInteger;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Tree.Xpath;
using org.eclipse.wst.xml.xpath2.processor.@internal.ast;
using XPath = org.eclipse.wst.xml.xpath2.processor.ast.XPath;
using AddExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.AddExpr;
using AndExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.AndExpr;
using AnyKindTest = org.eclipse.wst.xml.xpath2.processor.@internal.ast.AnyKindTest;
using AttributeTest = org.eclipse.wst.xml.xpath2.processor.@internal.ast.AttributeTest;
using AxisStep = org.eclipse.wst.xml.xpath2.processor.@internal.ast.AxisStep;
using BinExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.BinExpr;
using CastExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.CastExpr;
using CastableExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.CastableExpr;
using CmpExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.CmpExpr;
using CntxItemExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.CntxItemExpr;
using CommentTest = org.eclipse.wst.xml.xpath2.processor.@internal.ast.CommentTest;
using DecimalLiteral = org.eclipse.wst.xml.xpath2.processor.@internal.ast.DecimalLiteral;
using DivExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.DivExpr;
using DocumentTest = org.eclipse.wst.xml.xpath2.processor.@internal.ast.DocumentTest;
using DoubleLiteral = org.eclipse.wst.xml.xpath2.processor.@internal.ast.DoubleLiteral;
using ElementTest = org.eclipse.wst.xml.xpath2.processor.@internal.ast.ElementTest;
using ExceptExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.ExceptExpr;
using Expr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.Expr;
using FilterExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.FilterExpr;
using ForExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.ForExpr;
using ForwardStep = org.eclipse.wst.xml.xpath2.processor.@internal.ast.ForwardStep;
using FunctionCall = org.eclipse.wst.xml.xpath2.processor.@internal.ast.FunctionCall;
using IDivExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.IDivExpr;
using IfExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.IfExpr;
using InstOfExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.InstOfExpr;
using IntegerLiteral = org.eclipse.wst.xml.xpath2.processor.@internal.ast.IntegerLiteral;
using IntersectExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.IntersectExpr;
using ItemType = org.eclipse.wst.xml.xpath2.processor.@internal.ast.ItemType;
using MinusExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.MinusExpr;
using ModExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.ModExpr;
using MulExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.MulExpr;
using NameTest = org.eclipse.wst.xml.xpath2.processor.@internal.ast.NameTest;
using OrExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.OrExpr;
using PITest = org.eclipse.wst.xml.xpath2.processor.@internal.ast.PITest;
using ParExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.ParExpr;
using PipeExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.PipeExpr;
using PlusExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.PlusExpr;
using QuantifiedExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.QuantifiedExpr;
using RangeExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.RangeExpr;
using ReverseStep = org.eclipse.wst.xml.xpath2.processor.@internal.ast.ReverseStep;
using SchemaAttrTest = org.eclipse.wst.xml.xpath2.processor.@internal.ast.SchemaAttrTest;
using SchemaElemTest = org.eclipse.wst.xml.xpath2.processor.@internal.ast.SchemaElemTest;
using SequenceType = org.eclipse.wst.xml.xpath2.processor.@internal.ast.SequenceType;
using SingleType = org.eclipse.wst.xml.xpath2.processor.@internal.ast.SingleType;
using StepExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.StepExpr;
using StringLiteral = org.eclipse.wst.xml.xpath2.processor.@internal.ast.StringLiteral;
using SubExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.SubExpr;
using TextTest = org.eclipse.wst.xml.xpath2.processor.@internal.ast.TextTest;
using TreatAsExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.TreatAsExpr;
using UnionExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.UnionExpr;
using VarExprPair = org.eclipse.wst.xml.xpath2.processor.@internal.ast.VarExprPair;
using VarRef = org.eclipse.wst.xml.xpath2.processor.@internal.ast.VarRef;
using XPathExpr = org.eclipse.wst.xml.xpath2.processor.@internal.ast.XPathExpr;
using XPathNode = org.eclipse.wst.xml.xpath2.processor.@internal.ast.XPathNode;
using XPathVisitor = org.eclipse.wst.xml.xpath2.processor.@internal.ast.XPathVisitor;
using QName = org.eclipse.wst.xml.xpath2.processor.@internal.types.QName;
using LiteralUtils = org.eclipse.wst.xml.xpath2.processor.@internal.utils.LiteralUtils;

namespace xpath.org.eclipse.wst.xml.xpath2.processor.@internal
{

    /**
     *
     * @author sam
     */
    public class XPathBuilderVisitor : XPath31BaseVisitor<object>
    {
        public static XPathBuilderVisitor INSTANCE = new XPathBuilderVisitor();

        public override object /* AnyKindTest */VisitAnykindtest(XPath31Parser.AnykindtestContext ctx)
        {
            return new AnyKindTest();
        }

        //public override DecimalLiteral visitDecimalLiteral(XPath31Parser.DecimalLiteralContext ctx)
        //{
        //    return new DecimalLiteral(new BigDecimal(ctx.DECIMAL().getText()));
        //}

        //public override String visitUnreservedNCName(XPath31Parser.UnreservedNCNameContext ctx)
        //{
        //    return ctx.getChild(0).getText();
        //}

        public override object /* Expr */ VisitInstanceofexpr(XPath31Parser.InstanceofexprContext ctx)
        {
            Expr treatExpr = (Expr)VisitTreatexpr(ctx.treatexpr());
            if (ctx.INSTANCE() == null)
            {
                return treatExpr;
            }

            return new InstOfExpr(treatExpr, VisitSequencetype(ctx.sequencetype()));
        }

        public override object /* Integer */ VisitForwardaxis(XPath31Parser.ForwardaxisContext ctx)
        {
            switch (((TerminalNode) ctx.GetChild(0)).getSymbol().getType())
            {
                case XPath31Lexer.KW_CHILD:
                    return ForwardStep.CHILD;
                case XPath31Lexer.KW_DESCENDANT:
                    return ForwardStep.DESCENDANT;
                case XPath31Lexer.KW_ATTRIBUTE:
                    return ForwardStep.ATTRIBUTE;
                case XPath31Lexer.KW_SELF:
                    return ForwardStep.SELF;
                case XPath31Lexer.KW_DESCENDANT_OR_SELF:
                    return ForwardStep.DESCENDANT_OR_SELF;
                case XPath31Lexer.KW_FOLLOWING_SIBLING:
                    return ForwardStep.FOLLOWING_SIBLING;
                case XPath31Lexer.KW_FOLLOWING:
                    return ForwardStep.FOLLOWING;
                case XPath31Lexer.KW_NAMESPACE:
                    return ForwardStep.NAMESPACE;
                default:
                    Debug.Assert(false);
                    return ForwardStep.NONE;
            }
        }

        public override object /* ElementTest */ VisitElementtest(XPath31Parser.ElementtestContext ctx)
        {
            if (ctx.elementnameorwildcard() != null)
            {
                QName elementNameOrWildcard = VisitElementNameOrWildcard(ctx.elementnameorwildcard());
                bool wildcard = elementNameOrWildcard == null;
                if (ctx.typename() != null)
                {
                    QName typeName = visitTypeName(ctx.typename());
                    if (ctx.QM() != null)
                    {
                        return new ElementTest(elementNameOrWildcard, wildcard, typeName, true);
                    }
                    else
                    {
                        return new ElementTest(elementNameOrWildcard, wildcard, typeName);
                    }
                }
                else
                {
                    return new ElementTest(elementNameOrWildcard, wildcard);
                }
            }
            else
            {
                return new ElementTest();
            }
        }

        public override object /* Integer */ VisitReverseaxis(XPath31Parser.ReverseaxisContext ctx)
        {
            switch (((TerminalNode) ctx.GetChild(0)).getSymbol().getType())
            {
                case XPath31Lexer.KW_PARENT:
                    return ReverseStep.PARENT;
                case XPath31Lexer.KW_ANCESTOR:
                    return ReverseStep.ANCESTOR;
                case XPath31Lexer.KW_PRECEDING_SIBLING:
                    return ReverseStep.PRECEDING_SIBLING;
                case XPath31Lexer.KW_RECEDING:
                    return ReverseStep.PRECEDING;
                case XPath31Lexer.KW_ANCESTOR_OR_SELF:
                    return ReverseStep.ANCESTOR_OR_SELF;
                default:
                    Debug.Assert(false);
                    return ReverseStep.DOTDOT;
            }
        }

        //public override object /* String */ VisitPrefix(XPath31Parser. PrefixContext ctx)
        //{
        //    return visitNCName(ctx.nCName());
        //}

        public override object /* Expr */ VisitTreatexpr(XPath31Parser.TreatexprContext ctx)
        {
            Expr castableExpr = visitCastableExpr(ctx.castableexpr()());
            if (ctx.KW_TREAT() == null)
            {
                return castableExpr;
            }

            return new TreatAsExpr(castableExpr, visitSequenceType(ctx.sequencetype()));
        }

        public override object /* NameTest */ VisitNametest(XPath31Parser.NametestContext ctx)
        {
            if (ctx.qName() != null)
            {
                return new NameTest(visitQName(ctx.qName()));
            }
            else
            {
                return new NameTest(visitWildcard(ctx.wildcard()));
            }
        }

        public override object /* CntxItemExpr */ VisitContextitemexpr(XPath31Parser.ContextitemexprContext ctx)
        {
            return new CntxItemExpr();
        }

        public override object /* Expr */ VisitAdditiveexpr(XPath31Parser.AdditiveexprContext ctx)
        {
            Expr multiplicativeExpr = visitMultiplicativeExpr(ctx.multiplicativeExpr());
            if (ctx.additiveExpr() == null)
            {
                return multiplicativeExpr;
            }

            if (ctx.PLUS() != null)
            {
                return new AddExpr(visitAdditiveExpr(ctx.additiveExpr()), multiplicativeExpr);
            }
            else
            {
                return new SubExpr(visitAdditiveExpr(ctx.additiveExpr()), multiplicativeExpr);
            }
        }

        public override object /* VarRef */ VisitVarref(XPath31Parser.VarrefContext ctx)
        {
            return new VarRef(visitVarName(ctx.varName()));
        }

        public override object /* QName */ VisitElementname(XPath31Parser.ElementnameContext ctx)
        {
            return visitQName(ctx.qName());
        }

        public override Collection<Expr> visitPredicate(XPath31Parser.PredicateContext ctx)
        {
            return visitExpr(ctx.expr());
        }

        public override object /* Collection<Collection<Expr>> */ VisitPredicatelist(XPath31Parser.PredicatelistContext ctx)
        {
            Collection<Collection<Expr>> result = new ArrayList<Collection<Expr>>();
            //for (XPath31Parser.PredicateContext predicate :
            //ctx.predicate())
            //{
            //    result.add(visitPredicate(predicate));
            //}

            return result;
        }

        public override object /* SchemaAttrTest */ VisitSchemaattributetest(XPath31Parser.SchemaattributetestContext ctx)
        {
            return new SchemaAttrTest(visitAttributeDeclaration(ctx.attributeDeclaration()));
        }

        public override object /* Collection<Expr> */ VisitParenthesizedexpr(XPath31Parser.ParenthesizedexprContext ctx)
        {
            if (ctx.expr() == null)
            {
                return new ArrayList<Expr>();
            }

            return visitExpr(ctx.expr());
        }

        public override object /* NumericLiteral */ VisitNumericliteral(XPath31Parser.NumericliteralContext ctx)
        {
            if (ctx.integerLiteral() != null)
            {
                return visitIntegerLiteral(ctx.integerLiteral());
            }
            else if (ctx.decimalLiteral() != null)
            {
                return visitDecimalLiteral(ctx.decimalLiteral());
            }
            else
            {
                return visitDoubleLiteral(ctx.doubleLiteral());
            }
        }

        public override object /* QName */ VisitElementdeclaration(XPath31Parser.ElementdeclarationContext ctx)
        {
            return visitElementName(ctx.elementName());
        }

        public override object /* Expr */ VisitExprsingle(XPath31Parser.ExprsingleContext ctx)
        {
            if (ctx.forexpr() != null)
            {
                return VisitForexpr(ctx.forexpr());
            }
            else if (ctx.quantifiedexpr() != null)
            {
                return VisitQuantifiedexpr(ctx.quantifiedexpr());
            }
            else if (ctx.ifexpr() != null)
            {
                return VisitIfexpr(ctx.ifexpr());
            }
            else
            {
                return VisitOrexpr(ctx.orexpr());
            }
        }

        public override object /* Expr */ VisitOrexpr(XPath31Parser.OrexprContext ctx)
        {
            Expr all = null;
            foreach (var a in ctx.andexpr())
            {
                Expr andExpr = (Expr)VisitAndexpr(a);
                if (all != null)
                {
                    all = new OrExpr(all, andExpr);
                }
                else
                {
                    all = andExpr;
                }
            }
            return all;
        }

        public override object /* Integer */ VisitOccurrenceindicator(XPath31Parser.OccurrenceindicatorContext ctx)
        {
            if (ctx.QUESTIONMARK() != null)
            {
                return SequenceType.QUESTION;
            }
            else if (ctx.STAR() != null)
            {
                return SequenceType.STAR;
            }
            else
            {
                return SequenceType.PLUS;
            }
        }

        public override object /* KindTest */ VisitKindtest(XPath31Parser.KindtestContext ctx)
        {
            if (ctx.documentTest() != null)
            {
                return visitDocumentTest(ctx.documentTest());
            }
            else if (ctx.elementTest() != null)
            {
                return visitElementTest(ctx.elementTest());
            }
            else if (ctx.attributeTest() != null)
            {
                return visitAttributeTest(ctx.attributeTest());
            }
            else if (ctx.schemaElementTest() != null)
            {
                return visitSchemaElementTest(ctx.schemaElementTest());
            }
            else if (ctx.schemaAttributeTest() != null)
            {
                return visitSchemaAttributeTest(ctx.schemaAttributeTest());
            }
            else if (ctx.pITest() != null)
            {
                return visitPITest(ctx.pITest());
            }
            else if (ctx.commentTest() != null)
            {
                return visitCommentTest(ctx.commentTest());
            }
            else if (ctx.textTest() != null)
            {
                return visitTextTest(ctx.textTest());
            }
            else
            {
                return visitAnyKindTest(ctx.anyKindTest());
            }
        }

        //public override object /* StringLiteral */ VisitStringliteral(XPath31Parser. ctx)
        //{
        //    return new StringLiteral(LiteralUtils.unquote(ctx.STRING().getText()));
        //}

        public override object /* Expr */ VisitPathexpr(XPath31Parser.PathexprContext ctx)
        {
            if (ctx.relativePathExpr() != null)
            {
                XPathExpr relativePathExpr = visitRelativePathExpr(ctx.relativePathExpr());
                if (ctx.FORWARD_SLASH() != null)
                {
                    relativePathExpr.set_slashes(1);
                }
                else if (ctx.FORWARD_SLASHSLASH() != null)
                {
                    relativePathExpr.set_slashes(2);
                }

                return relativePathExpr;
            }
            else
            {
                return new XPathExpr(1, null);
            }
        }

        public override object /* FunctionCall */ VisitFunctioncall(XPath31Parser.FunctioncallContext ctx)
        {
            if (ctx.functionCallMiddle() != null)
            {
                return new FunctionCall(visitUnreservedQName(ctx.unreservedQName()),
                    visitFunctionCallMiddle(ctx.functionCallMiddle()));
            }
            else
            {
                return new FunctionCall(visitUnreservedQName(ctx.unreservedQName()), new ArrayList<Expr>());
            }
        }

        //public override object /* DoubleLiteral */ VisitDoubleliteral(XPath31Parser.dou ctx)
        //{
        //    return new DoubleLiteral(new Double(ctx.DOUBLE().getText()));
        //}

        public override object /* ForwardStep */ VisitForwardstep(XPath31Parser.ForwardstepContext ctx)
        {
            if (ctx.forwardAxis() != null)
            {
                return new ForwardStep(visitForwardAxis(ctx.forwardAxis()), visitNodeTest(ctx.nodeTest()));
            }
            else
            {
                return visitAbbrevForwardStep(ctx.abbrevForwardStep());
            }
        }

        public override object /* SequenceType */ VisitSequencetype(XPath31Parser.SequencetypeContext ctx)
        {
            if (ctx.itemType() != null)
            {
                ItemType itemType = visitItemType(ctx.itemType());
                if (ctx.occurrenceIndicator() != null)
                {
                    return new SequenceType(visitOccurrenceIndicator(ctx.occurrenceIndicator()), itemType);
                }
                else
                {
                    return new SequenceType(SequenceType.NONE, itemType);
                }
            }
            else
            {
                return new SequenceType(SequenceType.EMPTY, null);
            }
        }

        public override object /* PITest */ Visitpitest(XPath31Parser.PitestContext ctx)
        {
            if (ctx.nCName() != null)
            {
                return new PITest(visitNCName(ctx.nCName()));
            }
            else if (ctx.stringLiteral() != null)
            {
                return new PITest(visitStringLiteral(ctx.stringLiteral()).ToString());
            }
            else
            {
                return new PITest();
            }
        }

        //public override QName visitQName(XPath31Parser.QNameContext ctx)
        //{
        //    if (ctx.COLON() == null)
        //    {
        //        return new QName(visitNCName(ctx.nCName(0)));
        //    }
        //    else
        //    {
        //        return new QName(visitNCName(ctx.nCName(0)), visitNCName(ctx.nCName(1)));
        //    }
        //}

        public override object /* ItemType */ VisitItemtype(XPath31Parser.ItemtypeContext ctx)
        {
            if (ctx.atomicType() != null)
            {
                return new ItemType(ItemType.QNAME, visitAtomicType(ctx.atomicType()));
            }
            else if (ctx.kindTest() != null)
            {
                return new ItemType(ItemType.KINDTEST, visitKindTest(ctx.kindTest()));
            }
            else
            {
                return new ItemType(ItemType.ITEM, null);
            }
        }

        //public override object /* QName */ VisitAtomictype(XPath31Parser.AtomicoruniontypeContext ctx)
        //{
        //    return visitQName(ctx.qName());
        //}


        public override object /* QName */ VisitVarname(XPath31Parser.VarnameContext ctx)
        {
            return visitQName(ctx.qName());
        }

        //public override object /* Collection<VarExprPair> */ VisitQuantifiedexprmiddle(XPath31Parser.qu ctx)
        //{
        //    Collection<VarExprPair> result;
        //    if (ctx.quantifiedExprMiddle() != null)
        //    {
        //        result = visitQuantifiedExprMiddle(ctx.quantifiedExprMiddle());
        //    }
        //    else
        //    {
        //        result = new ArrayList<VarExprPair>();
        //    }

        //    result.add(new VarExprPair(visitVarName(ctx.varName()), visitExprSingle(ctx.exprSingle())));
        //    return result;
        //}

        public override object /* NodeTest */ VisitNodetest(XPath31Parser.NodetestContext ctx)
        {
            if (ctx.kindTest() != null)
            {
                return visitKindTest(ctx.kindTest());
            }
            else
            {
                return visitNameTest(ctx.nameTest());
            }
        }

        public override object /* SchemaElemTest */ VisitSchemaelementtest(XPath31Parser.SchemaelementtestContext ctx)
        {
            return new SchemaElemTest(visitElementDeclaration(ctx.elementDeclaration()));
        }

        public override object /* Expr */ VisitCastexpr(XPath31Parser.CastexprContext ctx)
        {
            Expr unaryExpr = visitUnaryExpr(ctx.unaryExpr());
            if (ctx.CAST() == null)
            {
                return unaryExpr;
            }

            return new CastExpr(unaryExpr, visitSingleType(ctx.singleType()));
        }

        public override object /* QName */ VisitTypename(XPath31Parser.TypenameContext ctx)
        {
            return visitQName(ctx.qName());
        }

        //public override QName visitUnreservedQName(XPath31Parser.UnreservedQNameContext ctx)
        //{
        //    if (ctx.COLON() != null)
        //    {
        //        return new QName(visitNCName(ctx.nCName(0)), visitNCName(ctx.nCName(1)));
        //    }
        //    else
        //    {
        //        return new QName(visitUnreservedNCName(ctx.unreservedNCName()));
        //    }
        //}

        //public override String visitNCName(XPath31Parser.NCNameContext ctx)
        //{
        //    return ctx.getChild(0).getText();
        //}

        public override object /* Integer */ VisitGeneralcomp(XPath31Parser.GeneralcompContext ctx)
        {
            switch (((TerminalNode) ctx.getChild(0)).getSymbol().getType())
            {
                case XPath31Lexer.EQUALS:
                    return CmpExpr.EQUALS;
                case XPath31Lexer.NOTEQUALS:
                    return CmpExpr.NOTEQUALS;
                case XPath31Lexer.LESSTHAN:
                    return CmpExpr.LESSTHAN;
                case XPath31Lexer.LESSEQUAL:
                    return CmpExpr.LESSEQUAL;
                case XPath31Lexer.GREATER:
                    return CmpExpr.GREATER;
                case XPath31Lexer.GREATEREQUAL:
                    return CmpExpr.GREATEREQUAL;
                default:
                    Debug.Assert(false);
                    return 0;
            }
        }

        public override object /* QName */ VisitWildcard(XPath31Parser.WildcardContext ctx)
        {
            if (ctx.COLON() == null)
            {
                return new QName("*", "*");
            }
            else if (ctx.GetChild(0) is TerminalNode) {
                return new QName("*", visitNCName(ctx.nCName()));
            }
            else
            {
                return new QName(visitNCName(ctx.nCName()), "*");
            }
        }

        public override object /* Literal */ VisitLiteral(XPath31Parser.LiteralContext ctx)
        {
            if (ctx.numericLiteral() != null)
            {
                return visitNumericLiteral(ctx.numericLiteral());
            }
            else
            {
                return visitStringLiteral(ctx.stringLiteral());
            }
        }

        public override object /* ForwardStep */ VisitAbbrevforwardstep(XPath31Parser.AbbrevforwardstepContext ctx)
        {
            NodeTest nodeTest = visitNodeTest(ctx.nodeTest());
            if (ctx.AT_SYM() != null)
            {
                return new ForwardStep(ForwardStep.AT_SYM, nodeTest);
            }
            else
            {
                return new ForwardStep(ForwardStep.NONE, nodeTest);
            }
        }

        public override object /* ForExpr */ VisitForexpr(XPath31Parser.ForexprContext ctx)
        {
            return new ForExpr(visitSimpleForClause(ctx.simpleForClause()), visitExprSingle(ctx.exprSingle()));
        }

        public override object /* Expr */ VisitUnaryexpr(XPath31Parser.UnaryexprContext ctx)
        {
            if (ctx.MINUS() != null)
            {
                return new MinusExpr(visitUnaryExpr(ctx.unaryExpr()));
            }
            else if (ctx.PLUS() != null)
            {
                return new PlusExpr(visitUnaryExpr(ctx.unaryExpr()));
            }
            else
            {
                return visitValueExpr(ctx.valueExpr());
            }
        }

        public override object /* Expr */ VisitValueexpr(XPath31Parser.ValueexprContext ctx)
        {
            return visitPathExpr(ctx.pathExpr());
        }

        public override object /* PrimaryExpr */ VisitPrimaryexpr(XPath31Parser.PrimaryexprContext ctx)
        {
            if (ctx.literal() != null)
            {
                return visitLiteral(ctx.literal());
            }
            else if (ctx.varRef() != null)
            {
                return visitVarRef(ctx.varRef());
            }
            else if (ctx.parenthesizedExpr() != null)
            {
                return new ParExpr(visitParenthesizedExpr(ctx.parenthesizedExpr()));
            }
            else if (ctx.contextItemExpr() != null)
            {
                return visitContextItemExpr(ctx.contextItemExpr());
            }
            else
            {
                return visitFunctionCall(ctx.functionCall());
            }
        }

        public override object /* Collection<VarExprPair> */ VisitSimpleforclause(XPath31Parser.SimpleforclauseContext ctx)
        {
            Collection<VarExprPair> result;
            if (ctx.simpleForClause() != null)
            {
                result = visitSimpleForClause(ctx.simpleForClause());
            }
            else
            {
                result = new ArrayList<VarExprPair>();
            }

            result.add(new VarExprPair(visitVarName(ctx.varName()), visitExprSingle(ctx.exprSingle())));
            return result;
        }

        public override object /* QName */ VisitAttributedeclaration(XPath31Parser.AttributedeclarationContext ctx)
        {
            return visitAttributeName(ctx.attributeName());
        }

        //public override object /* String */ VisitLocalpart(XPath31Parser.local ctx)
        //{
        //    return visitNCName(ctx.nCName());
        //}

        public override object /* SingleType */ VisitSingletype(XPath31Parser.SingletypeContext ctx)
        {
            QName atomicType = visitAtomicType(ctx.atomicType());
            if (ctx.QUESTIONMARK() != null)
            {
                return new SingleType(atomicType, true);
            }
            else
            {
                return new SingleType(atomicType);
            }
        }

        public override object /* Integer */ VisitNodecomp(XPath31Parser.NodecompContext ctx)
        {
            if (ctx.IS() != null)
            {
                return CmpExpr.IS;
            }
            else if (ctx.LESS_LESS() != null)
            {
                return CmpExpr.LESS_LESS;
            }
            else
            {
                return CmpExpr.GREATER_GREATER;
            }
        }

        public override object /* QName */ VisitElementnameorwildcard(XPath31Parser.ElementnameorwildcardContext ctx)
        {
            if (ctx.elementName() != null)
            {
                return visitElementName(ctx.elementName());
            }
            else
            {
                return null;
            }
        }

        public override object /* IfExpr */ VisitIfexpr(XPath31Parser.IfexprContext ctx)
        {
            Collection<Expr> condition = visitExpr(ctx.expr());
            Expr then = visitExprSingle(ctx.exprSingle(0));
            Expr els = visitExprSingle(ctx.exprSingle(1));
            return new IfExpr(condition, then, els);
        }

        public override object /* Collection<Expr> */ VisitExpr(XPath31Parser.ExprContext ctx)
        {
            Collection<Expr> result = new ArrayList<Expr>();
            foreach (XPath31Parser.ExprSingleContext ex in ctx.exprSingle())
            {
                result.add(visitExprSingle(ex));
            }

            return result;
        }

        public override object /* QName */ VisitAttributename(XPath31Parser.AttributenameContext ctx)
        {
            return visitQName(ctx.qName());
        }

        public override object /* ReverseStep */ VisitReversestep(XPath31Parser.ReversestepContext ctx)
        {
            if (ctx.reverseAxis() != null)
            {
                return new ReverseStep(visitReverseAxis(ctx.reverseAxis()), visitNodeTest(ctx.nodeTest()));
            }
            else
            {
                return visitAbbrevReverseStep(ctx.abbrevReverseStep());
            }
        }

        public override object /* Expr */ VisitCastableexpr(XPath31Parser.CastableexprContext ctx)
        {
            Expr castExpr = visitCastExpr(ctx.castExpr());
            if (ctx.CASTABLE() == null)
            {
                return castExpr;
            }

            return new CastableExpr(castExpr, visitSingleType(ctx.singleType()));
        }

        public override object /* QuantifiedExpr */ VisitQuantifiedexpr(XPath31Parser.QuantifiedexprContext ctx)
        {
            if (ctx.SOME() != null)
            {
                return new QuantifiedExpr(QuantifiedExpr.SOME, visitQuantifiedExprMiddle(ctx.quantifiedExprMiddle()),
                    visitExprSingle(ctx.exprSingle()));
            }
            else
            {
                return new QuantifiedExpr(QuantifiedExpr.ALL, visitQuantifiedExprMiddle(ctx.quantifiedExprMiddle()),
                    visitExprSingle(ctx.exprSingle()));
            }
        }

        public override object /* Expr */ VisitComparisonexpr(XPath31Parser.ComparisonexprContext ctx)
        {
            if (ctx.rangeExpr(1) == null)
            {
                return visitRangeExpr(ctx.rangeExpr(0));
            }

            if (ctx.valueComp() != null)
            {
                return new CmpExpr(visitRangeExpr(ctx.rangeExpr(0)), visitRangeExpr(ctx.rangeExpr(1)),
                    visitValueComp(ctx.valueComp()));
            }
            else if (ctx.generalComp() != null)
            {
                return new CmpExpr(visitRangeExpr(ctx.rangeExpr(0)), visitRangeExpr(ctx.rangeExpr(1)),
                    visitGeneralComp(ctx.generalComp()));
            }
            else
            {
                return new CmpExpr(visitRangeExpr(ctx.rangeExpr(0)), visitRangeExpr(ctx.rangeExpr(1)),
                    visitNodeComp(ctx.nodeComp()));
            }
        }

        //public override object /* FilterExpr */ VisitFilterexpr(XPath31Parser.filter ctx)
        //{
        //    return new FilterExpr(visitPrimaryExpr(ctx.primaryExpr()), visitPredicateList(ctx.predicateList()));
        //}

        public override object /* Expr */ VisitUnionexpr(XPath31Parser.UnionexprContext ctx)
        {
            Expr intersectExceptExpr = visitIntersectExceptExpr(ctx.intersectExceptExpr());
            if (ctx.unionExpr() == null)
            {
                return intersectExceptExpr;
            }

            if (ctx.KW_UNION() != null)
            {
                return new UnionExpr(visitUnionExpr(ctx.unionExpr()), intersectExceptExpr);
            }
            else
            {
                return new PipeExpr(visitUnionExpr(ctx.unionExpr()), intersectExceptExpr);
            }
        }

        public override object /* AxisStep */ VisitAxisstep(XPath31Parser.AxisstepContext ctx)
        {
            if (ctx.forwardStep() != null)
            {
                return new AxisStep(visitForwardStep(ctx.forwardStep()), visitPredicateList(ctx.predicateList()));
            }
            else
            {
                return new AxisStep(visitReverseStep(ctx.reverseStep()), visitPredicateList(ctx.predicateList()));
            }
        }

        public override object /* ReverseStep */ VisitAbbrevreversestep(XPath31Parser.AbbrevreversestepContext ctx)
        {
            return new ReverseStep(ReverseStep.DOTDOT, null);
        }

        public override object /* AttributeTest */ VisitAttributetest(XPath31Parser.AttributetestContext ctx)
        {
            if (ctx.attribNameOrWildcard() != null)
            {
                QName attribNameOrWildcard = visitAttribNameOrWildcard(ctx.attribNameOrWildcard());
                boolean wildcard = attribNameOrWildcard == null;
                if (ctx.typeName() != null)
                {
                    return new AttributeTest(attribNameOrWildcard, wildcard, visitTypeName(ctx.typeName()));
                }
                else
                {
                    return new AttributeTest(attribNameOrWildcard, wildcard);
                }
            }
            else
            {
                return new AttributeTest();
            }
        }

        public override object /* XPathExpr */ VisitRelativepathexpr(XPath31Parser.RelativepathexprContext ctx)
        {
            StepExpr stepExpr = visitStepExpr(ctx.stepExpr());
            if (ctx.FORWARD_SLASH() != null)
            {
                XPathExpr relativePathExpr = visitRelativePathExpr(ctx.relativePathExpr());
                relativePathExpr.add_tail(1, stepExpr);
                return relativePathExpr;
            }
            else if (ctx.FORWARD_SLASHSLASH() != null)
            {
                XPathExpr relativePathExpr = visitRelativePathExpr(ctx.relativePathExpr());
                relativePathExpr.add_tail(2, stepExpr);
                return relativePathExpr;
            }
            else
            {
                return new XPathExpr(0, stepExpr);
            }
        }

        public override object /* CommentTest */ VisitCommenttest(XPath31Parser.CommenttestContext ctx)
        {
            return new CommentTest();
        }

        public override object /* QName */ VisitAttribnameorwildcard(XPath31Parser.AttribnameorwildcardContext ctx)
        {
            if (ctx.attributeName() != null)
            {
                return visitAttributeName(ctx.attributeName());
            }
            else
            {
                return null;
            }
        }

        public override object /* TextTest */ VisitTexttest(XPath31Parser.TexttestContext ctx)
        {
            return new TextTest();
        }

        public override object /* Expr */ VisitIntersectexceptexpr(XPath31Parser.IntersectexceptexprContext ctx)
        {
            Expr instanceOfExpr = visitInstanceOfExpr(ctx.instanceOfExpr());
            if (ctx.intersectExceptExpr() == null)
            {
                return instanceOfExpr;
            }

            if (ctx.INTERSECT() != null)
            {
                return new IntersectExpr(visitIntersectExceptExpr(ctx.intersectExceptExpr()), instanceOfExpr);
            }
            else
            {
                return new ExceptExpr(visitIntersectExceptExpr(ctx.intersectExceptExpr()), instanceOfExpr);
            }
        }

        public override object /* Integer */ VisitValuecomp(XPath31Parser.ValuecompContext ctx)
        {
            switch (((TerminalNode) ctx.getChild(0)).getSymbol().getType())
            {
                case XPath31Lexer.EQ:
                    return CmpExpr.EQ;
                case XPath31Lexer.NE:
                    return CmpExpr.NE;
                case XPath31Lexer.LT:
                    return CmpExpr.LT;
                case XPath31Lexer.LE:
                    return CmpExpr.LE;
                case XPath31Lexer.GT:
                    return CmpExpr.GT;
                case XPath31Lexer.GE:
                    return CmpExpr.GE;
                default:
                    Debug.Assert(false);
                    return 0;
            }
        }

        //public override object /* IntegerLiteral */ VisitIntegerliteral(XPath31Parser.integ ctx)
        //{
        //    return new IntegerLiteral(new BigInteger(ctx.INTEGER().getText()));
        //}

        public override object /* XPath */ VisitXpath(XPath31Parser.XpathContext ctx)
        {
            return new XPath(visitExpr(ctx.expr()));
        }

        public override object /* Expr */ VisitMultiplicativeexpr(XPath31Parser.MultiplicativeexprContext ctx)
        {
            Expr unionExpr = visitUnionExpr(ctx.unionExpr());
            if (ctx.multiplicativeExpr() == null)
            {
                return unionExpr;
            }

            if (ctx.STAR() != null)
            {
                return new MulExpr(visitMultiplicativeExpr(ctx.multiplicativeExpr()), unionExpr);
            }
            else if (ctx.DIV() != null)
            {
                return new DivExpr(visitMultiplicativeExpr(ctx.multiplicativeExpr()), unionExpr);
            }
            else if (ctx.IDIV() != null)
            {
                return new IDivExpr(visitMultiplicativeExpr(ctx.multiplicativeExpr()), unionExpr);
            }
            else
            {
                return new ModExpr(visitMultiplicativeExpr(ctx.multiplicativeExpr()), unionExpr);
            }
        }

        public override object /* DocumentTest */ VisitDocumenttest(XPath31Parser.DocumenttestContext ctx)
        {
            if (ctx.elementTest() != null)
            {
                return new DocumentTest(DocumentTest.ELEMENT, visitElementTest(ctx.elementTest()));
            }
            else if (ctx.schemaElementTest() != null)
            {
                return new DocumentTest(DocumentTest.SCHEMA_ELEMENT, visitSchemaElementTest(ctx.schemaElementTest()));
            }
            else
            {
                return new DocumentTest();
            }
        }

        public override object /* Expr */ VisitRangeexpr(XPath31Parser.RangeexprContext ctx)
        {
            if (ctx.TO() == null)
            {
                return visitAdditiveExpr(ctx.additiveExpr(0));
            }

            return new RangeExpr(visitAdditiveExpr(ctx.additiveExpr(0)), visitAdditiveExpr(ctx.additiveExpr(1)));
        }

        public override object /* Expr */ VisitAndexpr(XPath31Parser.AndexprContext ctx)
        {
            Expr comparisonExpr = visitComparisonExpr(ctx.comparisonExpr());
            if (ctx.AND() == null)
            {
                return comparisonExpr;
            }

            return new AndExpr(visitAndExpr(ctx.andExpr()), comparisonExpr);
        }

        public override object /* StepExpr */ VisitStepexpr(XPath31Parser.StepexprContext ctx)
        {
            if (ctx.axisStep() != null)
            {
                return visitAxisStep(ctx.axisStep());
            }
            else
            {
                return visitFilterExpr(ctx.filterExpr());
            }
        }

        public override object /* Collection<Expr> */ VisitFunctioncall(XPath31Parser.FunctioncallContext ctx)
        {
            Collection<Expr> result;
            if (ctx.functionCallMiddle() != null)
            {
                result = visitFunctionCallMiddle(ctx.functionCallMiddle());
            }
            else
            {
                result = new ArrayList<Expr>();
            }

            result.add(visitExprSingle(ctx.exprSingle()));
            return result;
        }

    }
}
