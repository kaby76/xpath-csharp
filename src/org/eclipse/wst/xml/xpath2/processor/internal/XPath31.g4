// XPath v3.1
// Author--Ken Domino
// Date--4 June 2020
// 
// This is a faithful implementation of the XPath version 3.1 gramar
// from the spec at https://www.w3.org/TR/2017/REC-xpath-31-20170321/
// I have gone through this grammar very, very carefully to make sure
// that it is essentially identical to the spec, that it implements
// the lexical and parsing structures as stated in the spec.
// All others that I have found on the web (i.e., github.com) do not
// faithfully implement the spec, or are incomplete. This implementation
// is complete.
//
// I have verified this grammar against thousands of test expressions,
// which also differentiates this implementation for all others.
//
// Note, for testing using online xpath parsers xpath expressions,
// all require a data set. This is a pure, no bullshit parser. I am
// working on an xpath engine in C#.
//
// A note on the implementation of this grammar. I copied the grammar directly
// from the spec then employed Antlrvsix to refactor the grammar. I have not
// optimized it for speed, but that obviously could be done. The parser
// and lexer are combined because there is no super classing, no modes,
// no special lexer channels. If you want these, again Antlrvsix is the
// only tool that can do that quickly for you.
//
// Note on alternatives:
// https://github.com/exquery/xpath3-parser/blob/master/src/main/antlr4/org/exquery/xpath/parser/XPath3.g4
// This grammar does not implement correctly:
// * the lexical structure of the comment token;
// * the lexical structure of the QName token;
// * the lexical structure of the NCName token.
// For example, it says very, very clearly in the spec: "A.2.1 Terminal Symbols ... [122]   	QName".
// QName is a TOKEN. QName is a LEXICAL SYMBOL. QName is produced by the lexer, not the parser.
//
// https://github.com/antlr/grammars-v4/blob/master/xpath/xpath.g4
// This grammar is for version 1.

grammar XPath31;

// [1]
xpath : expr EOF ;
paramlist : param ( COMMA param)* ;
param : DOLLAR eqname typedeclaration? ;
functionbody : enclosedexpr ;
// [5]
enclosedexpr : OC expr? CC ;
expr : exprsingle ( COMMA exprsingle)* ;
exprsingle : forexpr | letexpr | quantifiedexpr | ifexpr | orexpr ;
forexpr : simpleforclause KW_RETURN exprsingle ;
simpleforclause : KW_FOR simpleforbinding ( COMMA simpleforbinding)* ;
// [10]
simpleforbinding : DOLLAR varname KW_IN exprsingle ;
letexpr :    simpleletclause KW_RETURN exprsingle ;
simpleletclause : KW_LET simpleletbinding ( COMMA simpleletbinding)* ;
simpleletbinding : DOLLAR varname CEQ exprsingle ;
quantifiedexpr :    ( KW_SOME | KW_EVERY) DOLLAR varname KW_IN exprsingle ( COMMA DOLLAR varname KW_IN exprsingle)* KW_SATISFIES exprsingle ;
// [15]
ifexpr : KW_IF OP expr CP KW_THEN exprsingle KW_ELSE exprsingle ;
orexpr : andexpr ( KW_OR andexpr )* ;
andexpr : comparisonexpr ( KW_AND comparisonexpr )* ;
comparisonexpr : stringconcatexpr ( (valuecomp | generalcomp | nodecomp) stringconcatexpr )? ;
stringconcatexpr : rangeexpr ( PP rangeexpr )* ;
// [20]
rangeexpr : additiveexpr ( KW_TO additiveexpr )? ;
additiveexpr : multiplicativeexpr ( (PLUS | MINUS) multiplicativeexpr )* ;
multiplicativeexpr : unionexpr ( (STAR | KW_DIV | KW_IDIV | KW_MOD) unionexpr )* ;
unionexpr : intersectexceptexpr ( (KW_UNION | P) intersectexceptexpr )* ;
intersectexceptexpr : instanceofexpr ( ( KW_INTERSECT | KW_EXCEPT) instanceofexpr )* ;
// [25]
instanceofexpr : treatexpr ( KW_INSTANCE KW_OF sequencetype )? ;
treatexpr : castableexpr ( KW_TREAT KW_AS sequencetype )? ;
castableexpr : castexpr ( KW_CASTABLE KW_AS singletype )? ;
castexpr : arrowexpr ( KW_CAST KW_AS singletype )? ;
arrowexpr : unaryexpr ( EG arrowfunctionspecifier argumentlist )* ;
// [30]
unaryexpr : ( MINUS | PLUS)* valueexpr ;
valueexpr : simplemapexpr ;
generalcomp : EQ | NE | LT | LE | GT | GE ;
valuecomp : KW_EQ | KW_NE | KW_LT | KW_LE | KW_GT | KW_GE ;
nodecomp : KW_IS | LL | GG ;
// [35]
simplemapexpr : pathexpr ( BANG pathexpr)* ;
pathexpr : ( SLASH relativepathexpr?) | ( SS relativepathexpr) | relativepathexpr ;
relativepathexpr : stepexpr (( SLASH | SS) stepexpr)* ;
stepexpr : postfixexpr | axisstep ;
axisstep : (reversestep | forwardstep) predicatelist ;
forwardstep : (forwardaxis nodetest) | abbrevforwardstep ;
forwardaxis : ( KW_CHILD COLONCOLON) | ( KW_DESCENDANT COLONCOLON) | ( KW_ATTRIBUTE COLONCOLON) | ( KW_SELF COLONCOLON) | ( KW_DESCENDANT_OR_SELF COLONCOLON) | ( KW_FOLLOWING_SIBLING COLONCOLON) | ( KW_FOLLOWING COLONCOLON) | ( KW_NAMESPACE COLONCOLON) ;
abbrevforwardstep : AT? nodetest ;
reversestep : (reverseaxis nodetest) | abbrevreversestep ;
reverseaxis : ( KW_PARENT COLONCOLON) | ( KW_ANCESTOR COLONCOLON) | ( KW_PRECEDING_SIBLING COLONCOLON) | ( KW_PRECEDING COLONCOLON) | ( KW_ANCESTOR_OR_SELF COLONCOLON) ;
abbrevreversestep : DD ;
nodetest : kindtest | nametest ;
nametest : eqname | wildcard ;
wildcard : STAR | (NCName CS) | ( SC NCName) | (BracedURILiteral STAR) ;
postfixexpr : primaryexpr (predicate | argumentlist | lookup)* ;
argumentlist : OP (argument ( COMMA argument)*)? CP ;
predicatelist : predicate* ;
predicate : OB expr CB ;
lookup : QM keyspecifier ;
keyspecifier : NCName | IntegerLiteral | parenthesizedexpr | STAR ;
arrowfunctionspecifier : eqname | varref | parenthesizedexpr ;
primaryexpr : literal | varref | parenthesizedexpr | contextitemexpr | functioncall | functionitemexpr | mapconstructor | arrayconstructor | unarylookup ;
literal : numericliteral | StringLiteral ;
numericliteral : IntegerLiteral | DecimalLiteral | DoubleLiteral ;
varref : DOLLAR varname ;
varname : eqname ;
parenthesizedexpr : OP expr? CP ;
contextitemexpr : D ;
functioncall : eqname argumentlist ;
argument : exprsingle | argumentplaceholder ;
argumentplaceholder : QM ;
functionitemexpr : namedfunctionref | inlinefunctionexpr ;
namedfunctionref : eqname POUND IntegerLiteral /* xgc: reserved-function-names */;
inlinefunctionexpr : KW_FUNCTION OP paramlist? CP ( KW_AS sequencetype)? functionbody ;
mapconstructor : KW_MAP OC (mapconstructorentry ( COMMA mapconstructorentry)*)? CC ;
mapconstructorentry : mapkeyexpr COLON mapvalueexpr ;
mapkeyexpr : exprsingle ;
mapvalueexpr : exprsingle ;
arrayconstructor : squarearrayconstructor | curlyarrayconstructor ;
squarearrayconstructor : OB (exprsingle ( COMMA exprsingle)*)? CB ;
curlyarrayconstructor : KW_ARRAY enclosedexpr ;
unarylookup : QM keyspecifier ;
singletype : simpletypename QM? ;
typedeclaration : KW_AS sequencetype ;
sequencetype : ( KW_EMPTY_SEQUENCE OP CP) | (itemtype occurrenceindicator?) ;
occurrenceindicator : QM | STAR | PLUS ;
itemtype : kindtest | ( KW_ITEM OP CP) | functiontest | maptest | arraytest | atomicoruniontype | parenthesizeditemtype ;
atomicoruniontype : eqname ;
kindtest : documenttest | elementtest | attributetest | schemaelementtest | schemaattributetest | pitest | commenttest | texttest | namespacenodetest | anykindtest ;
anykindtest : KW_NODE OP CP ;
documenttest : KW_DOCUMENT_NODE OP (elementtest | schemaelementtest)? CP ;
texttest : KW_TEXT OP CP ;
commenttest : KW_COMMENT OP CP ;
namespacenodetest : KW_NAMESPACE_NODE OP CP ;
pitest : KW_PROCESSING_INSTRUCTION OP (NCName | StringLiteral)? CP ;
attributetest : KW_ATTRIBUTE OP (attribnameorwildcard ( COMMA typename)?)? CP ;
attribnameorwildcard : attributename | STAR ;
schemaattributetest : KW_SCHEMA_ATTRIBUTE OP attributedeclaration CP ;
attributedeclaration : attributename ;
elementtest : KW_ELEMENT OP (elementnameorwildcard ( COMMA typename QM?)?)? CP ;
elementnameorwildcard : elementname | STAR ;
schemaelementtest : KW_SCHEMA_ELEMENT OP elementdeclaration CP ;
elementdeclaration : elementname ;
attributename : eqname ;
elementname : eqname ;
simpletypename : typename ;
typename : eqname ;
functiontest : anyfunctiontest | typedfunctiontest ;
anyfunctiontest : KW_FUNCTION OP STAR CP ;
typedfunctiontest : KW_FUNCTION OP (sequencetype ( COMMA sequencetype)*)? CP KW_AS sequencetype ;
maptest : anymaptest | typedmaptest ;
anymaptest : KW_MAP OP STAR CP ;
typedmaptest : KW_MAP OP atomicoruniontype COMMA sequencetype CP ;
arraytest : anyarraytest | typedarraytest ;
anyarraytest : KW_ARRAY OP STAR CP ;
typedarraytest : KW_ARRAY OP sequencetype CP ;
parenthesizeditemtype : OP itemtype CP ;

// Error in the spec. EQName also includes acceptable keywords.
eqname : QName | URIQualifiedName
 | KW_RETURN
 | KW_FOR
 | KW_IN
 | KW_LET
 | KW_SOME
 | KW_EVERY
 | KW_SATISFIES
 | KW_IF
 | KW_THEN
 | KW_ELSE
 | KW_OR
 | KW_AND
 | KW_DIV
 | KW_IDIV
 | KW_MOD
 | KW_UNION
 | KW_INTERSECT
 | KW_EXCEPT
 | KW_INSTANCE
 | KW_OF
 | KW_TREAT
 | KW_AS
 | KW_CASTABLE
 | KW_CAST
 | KW_EQ
 | KW_NE
 | KW_LT
 | KW_LE
 | KW_GT
 | KW_GE
 | KW_IS
 | KW_CHILD
 | KW_DESCENDANT
 | KW_ATTRIBUTE
 | KW_SELF
 | KW_DESCENDANT_OR_SELF
 | KW_FOLLOWING_SIBLING
 | KW_FOLLOWING
 | KW_NAMESPACE
 | KW_PARENT
 | KW_ANCESTOR
 | KW_PRECEDING_SIBLING
 | KW_PRECEDING
 | KW_ANCESTOR_OR_SELF
 | KW_FUNCTION
 | KW_MAP
 | KW_ARRAY
 | KW_EMPTY_SEQUENCE
 | KW_ITEM
 | KW_NODE
 | KW_DOCUMENT_NODE
 | KW_TEXT
 | KW_COMMENT
 | KW_NAMESPACE_NODE
 | KW_PROCESSING_INSTRUCTION
 | KW_SCHEMA_ATTRIBUTE
 | KW_ELEMENT
 | KW_SCHEMA_ELEMENT
 ;

AT : '@' ;
BANG : '!' ;
CB : ']' ;
CC : '}' ;
CEQ : ':=' ;
COLON : ':' ;
COLONCOLON : '::' ;
COMMA : ',' ;
CP : ')' ;
CS : ':*' ;
D : '.' ;
DD : '..' ;
DOLLAR : '$' ;
EG : '=>' ;
EQ : '=' ;
GE : '>=' ;
GG : '>>' ;
GT : '>' ;
LE : '<=' ;
LL : '<<' ;
LT : '<' ;
MINUS : '-' ;
NE : '!=' ;
OB : '[' ;
OC : '{' ;
OP : '(' ;
P : '|' ;
PLUS : '+' ;
POUND : '#' ;
PP : '||' ;
QM : '?' ;
SC : '*:' ;
SLASH : '/' ;
SS : '//' ;
STAR : '*' ;

// KEYWORDS

KW_ANCESTOR : 'ancestor' ;
KW_ANCESTOR_OR_SELF : 'ancestor-or-self' ;
KW_AND : 'and' ;
KW_ARRAY : 'array' ;
KW_AS : 'as' ;
KW_ATTRIBUTE : 'attribute' ;
KW_CAST : 'cast' ;
KW_CASTABLE : 'castable' ;
KW_CHILD : 'child' ;
KW_COMMENT : 'comment' ;
KW_DESCENDANT : 'descendant' ;
KW_DESCENDANT_OR_SELF : 'descendant-or-self' ;
KW_DIV : 'div' ;
KW_DOCUMENT_NODE : 'document-node' ;
KW_ELEMENT : 'element' ;
KW_ELSE : 'else' ;
KW_EMPTY_SEQUENCE : 'empty-sequence' ;
KW_EQ : 'eq' ;
KW_EVERY : 'every' ;
KW_EXCEPT : 'except' ;
KW_FOLLOWING : 'following' ;
KW_FOLLOWING_SIBLING : 'following-sibling' ;
KW_FOR : 'for' ;
KW_FUNCTION : 'function' ;
KW_GE : 'ge' ;
KW_GT : 'gt' ;
KW_IDIV : 'idiv' ;
KW_IF : 'if' ;
KW_IN : 'in' ;
KW_INSTANCE : 'instance' ;
KW_INTERSECT : 'intersect' ;
KW_IS : 'is' ;
KW_ITEM : 'item' ;
KW_LE : 'le' ;
KW_LET : 'let' ;
KW_LT : 'lt' ;
KW_MAP : 'map' ;
KW_MOD : 'mod' ;
KW_NAMESPACE : 'namespace' ;
KW_NAMESPACE_NODE : 'namespace-node' ;
KW_NE : 'ne' ;
KW_NODE : 'node' ;
KW_OF : 'of' ;
KW_OR : 'or' ;
KW_PARENT : 'parent' ;
KW_PRECEDING : 'preceding' ;
KW_PRECEDING_SIBLING : 'preceding-sibling' ;
KW_PROCESSING_INSTRUCTION : 'processing-instruction' ;
KW_RETURN : 'return' ;
KW_SATISFIES : 'satisfies' ;
KW_SCHEMA_ATTRIBUTE : 'schema-attribute' ;
KW_SCHEMA_ELEMENT : 'schema-element' ;
KW_SELF : 'self' ;
KW_SOME : 'some' ;
KW_TEXT : 'text' ;
KW_THEN : 'then' ;
KW_TO : 'to' ;
KW_TREAT : 'treat' ;
KW_UNION : 'union' ;

// A.2.1. TEMINAL SYMBOLS
// This isn't a complete list of tokens in the language.
// Keywords and symbols are terminals.

IntegerLiteral : FragDigits ;
DecimalLiteral : ('.' FragDigits) | (FragDigits '.' [0-9]*) ;
DoubleLiteral : (('.' FragDigits) | (FragDigits ('.' [0-9]*)?)) [eE] [+-]? FragDigits ;
StringLiteral : ('"' (FragEscapeQuot | ~[^"])*? '"') | ('\'' (FragEscapeApos | ~['])*? '\'') ;
URIQualifiedName : BracedURILiteral NCName ;
BracedURILiteral : 'Q' '{' [^{}]* '}' ;
// Error in spec: EscapeQuot and EscapeApos are not terminals!
fragment FragEscapeQuot : '""' ; 
fragment FragEscapeApos : '\'';
// Error in spec: Comment isn't really a terminal, but an off-channel object.
Comment : '(:' (Comment | CommentContents)*? ':)' -> skip ;
QName  : FragQName ;
NCName : FragmentNCName ;
// Error in spec: Char is not a terminal!
fragment Char : FragChar ;
fragment FragDigits : [0-9]+ ;
fragment CommentContents : Char ;
// https://www.w3.org/TR/REC-xml-names/#NT-QName
fragment FragQName : FragPrefixedName | FragUnprefixedName ;
fragment FragPrefixedName : FragPrefix ':' FragLocalPart ;
fragment FragUnprefixedName : FragLocalPart ;
fragment FragPrefix : FragmentNCName ;
fragment FragLocalPart : FragmentNCName ;
fragment FragNCNameStartChar
  :  'A'..'Z'
  |  '_'
  | 'a'..'z'
  | '\u00C0'..'\u00D6'
  | '\u00D8'..'\u00F6'
  | '\u00F8'..'\u02FF'
  | '\u0370'..'\u037D'
  | '\u037F'..'\u1FFF'
  | '\u200C'..'\u200D'
  | '\u2070'..'\u218F'
  | '\u2C00'..'\u2FEF'
  | '\u3001'..'\uD7FF'
  | '\uF900'..'\uFDCF'
  | '\uFDF0'..'\uFFFD'
  | '\u{10000}'..'\u{EFFFF}'
  ;
fragment FragNCNameChar
  :  FragNCNameStartChar | '-' | '.' | '0'..'9'
  |  '\u00B7' | '\u0300'..'\u036F'
  |  '\u203F'..'\u2040'
  ;
fragment FragmentNCName  :  FragNCNameStartChar FragNCNameChar*  ;

// https://www.w3.org/TR/REC-xml/#NT-Char

fragment FragChar : '\u0009' | '\u000a' | '\u000d'
  | '\u0020'..'\ud7ff'
  | '\ue000'..'\ufffd'
  | '\u{10000}'..'\u{10ffff}'
 ;

Whitespace :  ('\u0020' | '\u0009' | '\u000d' | '\u000a')+ -> skip ;