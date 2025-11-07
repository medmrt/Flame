
Imports System.Reflection
Imports Irony.Ast
Imports Irony.Parsing
Public Class FlameLang
    Inherits Grammar
    Public Shared Extension As String = "flame"
    Public SyntaxErrors As New List(Of ErrorInformation)
    Public SemanticErrors As New List(Of ErrorInformation)
    Sub New()
        Me.NonGrammarTerminals.Add(New CommentTerminal("LINE_COMMENT", "//", vbNewLine))
        Me.NonGrammarTerminals.Add(New CommentTerminal("BLOCK_COMMENT", "/*", "*/"))
        Me.LanguageFlags = LanguageFlags.CreateAst
        Dim RulesDic As New Hashtable
        Language.ResetAll()
        Me.Root = Language.BuildRule(Me, RulesDic)
    End Sub

    Function Parse(SourceCode As String, Optional SourceFileFolder As String = "") As Language
        Dim y As New Irony.Parsing.Parser(Me)
        Dim x As ParseTree = y.Parse(SourceCode)
        If x.HasErrors Then
            For Each Errs In x.ParserMessages
                SyntaxErrors.Add(New ErrorInformation(Errs.Message, Errs.Location))
            Next
            Return Nothing
        Else
            If Not AllFileExist(SourceFileFolder) Then
                Return Nothing
            End If
            If Not AllFolderExist(SourceFileFolder) Then
                Return Nothing
            End If
            If TypeOf x.Root.AstNode Is Language Then
                Dim N As Language = x.Root.AstNode
                RaiseEvent OnRelink(N)
                Return N
            Else
                Return Nothing
            End If
        End If
    End Function
    Function AllFileExist(SourceFileFolder As String) As Boolean
      'Check Files Existance.
      'Convert Relative Path To Absolute Path.
        Return True
    End Function
   Function AllFolderExist(SourceFileFolder As String) As Boolean
      'Check Files Existance.
      'Convert Relative Path To Absolute Path.
        Return True
    End Function
    Public Event OnRelink(ByRef Language As Language)
    

    Private WithEvents Language_Rule As New Language
    Private _LanguageHash As New Hashtable
    Sub WhenLanguageCreated(ByRef _Language As Language) Handles Language_Rule.LanguageCreated
        If _LanguageHash.ContainsKey(_Language.Name) Then
            Me.SemanticErrors.Add(New ErrorInformation($"Language name '{_Language.Name}' is dublicated!", _Language.Location))
        Else
            _LanguageHash(_Language.Name) = _Language
        End If
    End Sub


    Private WithEvents Rule_Rule As New Rule
    Private _RuleHash As New Hashtable
    Sub WhenRuleCreated(ByRef _Rule As Rule) Handles Rule_Rule.RuleCreated
        If _RuleHash.ContainsKey(_Rule.Name) Then
            Me.SemanticErrors.Add(New ErrorInformation($"Rule name '{_Rule.Name}' is dublicated!", _Rule.Location))
        Else
            _RuleHash(_Rule.Name) = _Rule
        End If
    End Sub


    Private WithEvents KeywordItem_Rule As New KeywordItem
    Private _KeywordItemList As New List(Of KeywordItem)
    Sub WhenKeywordItemCreated(ByRef _KeywordItem As KeywordItem) Handles KeywordItem_Rule.KeywordItemCreated
        _KeywordItemList.Add( _KeywordItem) 
    End Sub


    Private WithEvents KeyItem_Rule As New KeyItem
    Private _KeyItemList As New List(Of KeyItem)
    Sub WhenKeyItemCreated(ByRef _KeyItem As KeyItem) Handles KeyItem_Rule.KeyItemCreated
        _KeyItemList.Add( _KeyItem) 
    End Sub


    Private WithEvents IdentifierItem_Rule As New IdentifierItem
    Private _IdentifierItemList As New List(Of IdentifierItem)
    Sub WhenIdentifierItemCreated(ByRef _IdentifierItem As IdentifierItem) Handles IdentifierItem_Rule.IdentifierItemCreated
        _IdentifierItemList.Add( _IdentifierItem) 
    End Sub


    Private WithEvents IntItem_Rule As New IntItem
    Private _IntItemList As New List(Of IntItem)
    Sub WhenIntItemCreated(ByRef _IntItem As IntItem) Handles IntItem_Rule.IntItemCreated
        _IntItemList.Add( _IntItem) 
    End Sub


    Private WithEvents FloatItem_Rule As New FloatItem
    Private _FloatItemList As New List(Of FloatItem)
    Sub WhenFloatItemCreated(ByRef _FloatItem As FloatItem) Handles FloatItem_Rule.FloatItemCreated
        _FloatItemList.Add( _FloatItem) 
    End Sub


    Private WithEvents StringItem_Rule As New StringItem
    Private _StringItemList As New List(Of StringItem)
    Sub WhenStringItemCreated(ByRef _StringItem As StringItem) Handles StringItem_Rule.StringItemCreated
        _StringItemList.Add( _StringItem) 
    End Sub


    Private WithEvents FlagItem_Rule As New FlagItem
    Private _FlagItemList As New List(Of FlagItem)
    Sub WhenFlagItemCreated(ByRef _FlagItem As FlagItem) Handles FlagItem_Rule.FlagItemCreated
        _FlagItemList.Add( _FlagItem) 
    End Sub


    Private WithEvents EnumItem_Rule As New EnumItem
    Private _EnumItemList As New List(Of EnumItem)
    Sub WhenEnumItemCreated(ByRef _EnumItem As EnumItem) Handles EnumItem_Rule.EnumItemCreated
        _EnumItemList.Add( _EnumItem) 
    End Sub


    Private WithEvents DefItem_Rule As New DefItem
    Private _DefItemList As New List(Of DefItem)
    Sub WhenDefItemCreated(ByRef _DefItem As DefItem) Handles DefItem_Rule.DefItemCreated
        _DefItemList.Add( _DefItem) 
    End Sub


    Private WithEvents RefItem_Rule As New RefItem
    Private _RefItemList As New List(Of RefItem)
    Sub WhenRefItemCreated(ByRef _RefItem As RefItem) Handles RefItem_Rule.RefItemCreated
        _RefItemList.Add( _RefItem) 
    End Sub


    Private WithEvents RegexItem_Rule As New RegexItem
    Private _RegexItemList As New List(Of RegexItem)
    Sub WhenRegexItemCreated(ByRef _RegexItem As RegexItem) Handles RegexItem_Rule.RegexItemCreated
        _RegexItemList.Add( _RegexItem) 
    End Sub


    Private WithEvents ListOption_Rule As New ListOption
    Private _ListOptionList As New List(Of ListOption)
    Sub WhenListOptionCreated(ByRef _ListOption As ListOption) Handles ListOption_Rule.ListOptionCreated
        _ListOptionList.Add( _ListOption) 
    End Sub


    Private WithEvents Compile_Rule As New Compile
    Private _CompileHash As New Hashtable
    Sub WhenCompileCreated(ByRef _Compile As Compile) Handles Compile_Rule.CompileCreated
        If _CompileHash.ContainsKey(_Compile.Name) Then
            Me.SemanticErrors.Add(New ErrorInformation($"Compile name '{_Compile.Name}' is dublicated!", _Compile.Location))
        Else
            _CompileHash(_Compile.Name) = _Compile
        End If
    End Sub


    Private WithEvents TypeCompile_Rule As New TypeCompile
    Private _TypeCompileHash As New Hashtable
    Sub WhenTypeCompileCreated(ByRef _TypeCompile As TypeCompile) Handles TypeCompile_Rule.TypeCompileCreated
        If _TypeCompileHash.ContainsKey(_TypeCompile.Name) Then
            Me.SemanticErrors.Add(New ErrorInformation($"TypeCompile name '{_TypeCompile.Name}' is dublicated!", _TypeCompile.Location))
        Else
            _TypeCompileHash(_TypeCompile.Name) = _TypeCompile
        End If
    End Sub


    Private WithEvents SimpleText_Rule As New SimpleText
    Private _SimpleTextList As New List(Of SimpleText)
    Sub WhenSimpleTextCreated(ByRef _SimpleText As SimpleText) Handles SimpleText_Rule.SimpleTextCreated
        _SimpleTextList.Add( _SimpleText) 
    End Sub


    Private WithEvents InitialTemplate_Rule As New InitialTemplate
    Private _InitialTemplateList As New List(Of InitialTemplate)
    Sub WhenInitialTemplateCreated(ByRef _InitialTemplate As InitialTemplate) Handles InitialTemplate_Rule.InitialTemplateCreated
        _InitialTemplateList.Add( _InitialTemplate) 
    End Sub

End Class
Public Class ErrorInformation
    Property Description As String = ""
    Property Location As SourceLocation
    Sub New(_Description As String, _Location As SourceLocation)
        Me.Description = _Description
        Me.Location = _Location
    End Sub
    Sub New()
    End Sub
End Class

Partial Class Language
    Public Shared All As New List(Of Object)
 Public Shared LanguageList As New List(Of Language)
 Public Shared RuleList As New List(Of Rule)
 Public Shared KeywordItemList As New List(Of KeywordItem)
 Public Shared KeyItemList As New List(Of KeyItem)
 Public Shared IdentifierItemList As New List(Of IdentifierItem)
 Public Shared IntItemList As New List(Of IntItem)
 Public Shared FloatItemList As New List(Of FloatItem)
 Public Shared StringItemList As New List(Of StringItem)
 Public Shared FlagItemList As New List(Of FlagItem)
 Public Shared EnumItemList As New List(Of EnumItem)
 Public Shared DefItemList As New List(Of DefItem)
 Public Shared RefItemList As New List(Of RefItem)
 Public Shared RegexItemList As New List(Of RegexItem)
 Public Shared ListOptionList As New List(Of ListOption)
 Public Shared CompileList As New List(Of Compile)
 Public Shared TypeCompileList As New List(Of TypeCompile)
 Public Shared SimpleTextList As New List(Of SimpleText)
 Public Shared InitialTemplateList As New List(Of InitialTemplate)
    Public Shared Sub ResetAll()
        All.Clear()
         LanguageList.Clear()
         RuleList.Clear()
         KeywordItemList.Clear()
         KeyItemList.Clear()
         IdentifierItemList.Clear()
         IntItemList.Clear()
         FloatItemList.Clear()
         StringItemList.Clear()
         FlagItemList.Clear()
         EnumItemList.Clear()
         DefItemList.Clear()
         RefItemList.Clear()
         RegexItemList.Clear()
         ListOptionList.Clear()
         CompileList.Clear()
         TypeCompileList.Clear()
         SimpleTextList.Clear()
         InitialTemplateList.Clear()
    End Sub
End Class


Partial Class FlameLang
    Private Sub Relink_All_OnRelink(ByRef _Language As Language) Handles Me.OnRelink
'****ONE CODE****
    If _Language.InitialTemplateDef IsNot Nothing '**

    End If '***
For Each _Rules As Object In _Language.Rules
If TypeOf  _Rules Is Rule Then
    Dim _Rule As Rule = _Rules
For Each _Items As Object In _Rules.Items
If TypeOf  _Items Is KeywordItem Then
    Dim _KeywordItem As KeywordItem = _Items

End If
If TypeOf  _Items Is KeyItem Then
    Dim _KeyItem As KeyItem = _Items

End If
If TypeOf  _Items Is IdentifierItem Then
    Dim _IdentifierItem As IdentifierItem = _Items
'****ONE CODE****
    If _Items.ListOption IsNot Nothing '**

    End If '***
End If
If TypeOf  _Items Is IntItem Then
    Dim _IntItem As IntItem = _Items
'****ONE CODE****
    If _Items.ListOption IsNot Nothing '**

    End If '***
End If
If TypeOf  _Items Is FloatItem Then
    Dim _FloatItem As FloatItem = _Items
'****ONE CODE****
    If _Items.ListOption IsNot Nothing '**

    End If '***
End If
If TypeOf  _Items Is StringItem Then
    Dim _StringItem As StringItem = _Items
'****ONE CODE****
    If _Items.ListOption IsNot Nothing '**

    End If '***
End If
If TypeOf  _Items Is FlagItem Then
    Dim _FlagItem As FlagItem = _Items

End If
If TypeOf  _Items Is EnumItem Then
    Dim _EnumItem As EnumItem = _Items

End If
If TypeOf  _Items Is DefItem Then
    Dim _DefItem As DefItem = _Items
    Dim LstRule As New List(Of Rule)
    For Each Item In _DefItem.Rules
        LstRule.Add(_RuleHash(Item.Name))
    Next
    _DefItem.Rules = LstRule
'****ONE CODE****
    If _Items.ListOption IsNot Nothing '**

    End If '***
End If
If TypeOf  _Items Is RefItem Then
    Dim _RefItem As RefItem = _Items
    'My edit!
    If _RefItem.RuleRef IsNot Nothing Then _RefItem.RuleRef = _RuleHash(_RefItem.RuleRef.Name)
'****ONE CODE****
    If _Items.ListOption IsNot Nothing '**

    End If '***
End If
If TypeOf  _Items Is RegexItem Then
    Dim _RegexItem As RegexItem = _Items
'****ONE CODE****
    If _Items.ListOption IsNot Nothing '**

    End If '***
End If
Next
End If
Next
For Each _Compilers As Object In _Language.Compilers
If TypeOf  _Compilers Is Compile Then
    Dim _Compile As Compile = _Compilers
For Each _Body As Object In _Compilers.Body
If TypeOf  _Body Is SimpleText Then
    Dim _SimpleText As SimpleText = _Body

End If
Next
End If
If TypeOf  _Compilers Is TypeCompile Then
    Dim _TypeCompile As TypeCompile = _Compilers
    'My edit!
    If _TypeCompile.RuleRef IsNot Nothing Then _TypeCompile.RuleRef = _RuleHash(_TypeCompile.RuleRef.Name)
For Each _Body As Object In _Compilers.Body
If TypeOf  _Body Is SimpleText Then
    Dim _SimpleText As SimpleText = _Body

End If
Next
End If
Next
    End Sub
End Class



Public Class Language
    Inherits AstNode
    Public Shared Event LanguageCreated(ByRef _Language As Language)
      Property Version As Double = 0
      Property Name As String = ""
      Property Description As String = ""
      Property Creator As String = ""
      Property CreationDate As String = ""
      Property InitialTemplateDef As InitialTemplate
      Property Rules As New List(Of Rule)
      Property Compilers As New List(Of Object)
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      Version = treeNode.ChildNodes(2).Token.Value
      Name = treeNode.ChildNodes(4).Token.Value
      Description = treeNode.ChildNodes(5).Token.Value
      Creator = treeNode.ChildNodes(7).Token.Value
      CreationDate = treeNode.ChildNodes(9).Token.Value
      If treeNode.ChildNodes(11).ChildNodes.Count > 0 Then InitialTemplateDef = treeNode.ChildNodes(11).ChildNodes(0).AstNode
      If treeNode.ChildNodes(12).ChildNodes.Count > 0 Then
         For I As Integer = 0  To treeNode.ChildNodes(12).ChildNodes.Count -1
             Rules.Add(treeNode.ChildNodes(12).ChildNodes(I).ChildNodes(0).AstNode)
         Next
      End If
      If treeNode.ChildNodes(13).ChildNodes.Count > 0 Then
         For I As Integer = 0  To treeNode.ChildNodes(13).ChildNodes.Count -1
             Compilers.Add(treeNode.ChildNodes(13).ChildNodes(I).ChildNodes(0).AstNode)
         Next
      End If
        RaiseEvent LanguageCreated(Me)
        Language.LanguageList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("Language") Then
            Return Rules("Language")
        End If
        Dim _Language As New NonTerminal("Language", GetType(Language))
        Rules("Language") = _Language
       Dim _Version As New  NumberLiteral("Version" ,  NumberOptions.AllowSign)
       Dim _Name As New IdentifierTerminal("Name")
       Dim _Description As New  StringLiteral("Description", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
       Dim _Creator As New  StringLiteral("Creator", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
       Dim _CreationDate As New  StringLiteral("CreationDate", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
      'Optional Object
       Dim _InitialTemplateDef As New  NonTerminal("InitialTemplateDef")
       Dim _InitialTemplateDefItem As New  NonTerminal("InitialTemplateDefItem")
       _InitialTemplateDef.Rule = G.Empty Or InitialTemplate.BuildRule(G, Rules)
      '++ Required List ++
       Dim _RulesItem As New  NonTerminal("RulesItem")
       _RulesItem.Rule = Rule.BuildRule(G, Rules)
       Dim _Rules As New  NonTerminal("Rules")
       _Rules.Rule = Grammar.MakePlusRule(_Rules, Nothing, _RulesItem)
      '++ Required List ++
       Dim _CompilersItem As New  NonTerminal("CompilersItem")
       _CompilersItem.Rule = Compile.BuildRule(G, Rules) Or TypeCompile.BuildRule(G, Rules)
       Dim _Compilers As New  NonTerminal("Compilers")
       _Compilers.Rule = Grammar.MakePlusRule(_Compilers, Nothing, _CompilersItem)
        _Language.Rule = G.ToTerm("language") + G.ToTerm("(") + _Version + G.ToTerm(")") + _Name + _Description + G.ToTerm("by") + _Creator + G.ToTerm("on") + _CreationDate + G.ToTerm("{") + _InitialTemplateDef + _Rules + _Compilers + G.ToTerm("}")
        Return _Language
    End Function
End Class


Public Class Rule
    Inherits AstNode
    Public Shared Event RuleCreated(ByRef _Rule As Rule)
      Property Name As String = ""
      Property Description As String = ""
      Property Items As New List(Of Object)
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      Name = treeNode.ChildNodes(1).Token.Value
      Description = treeNode.ChildNodes(2).Token.Value
      If treeNode.ChildNodes(4).ChildNodes.Count > 0 Then
         For I As Integer = 0  To treeNode.ChildNodes(4).ChildNodes.Count -1
             Items.Add(treeNode.ChildNodes(4).ChildNodes(I).ChildNodes(0).AstNode)
         Next
      End If
        RaiseEvent RuleCreated(Me)
        Language.RuleList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("Rule") Then
            Return Rules("Rule")
        End If
        Dim _Rule As New NonTerminal("Rule", GetType(Rule))
        Rules("Rule") = _Rule
       Dim _Name As New IdentifierTerminal("Name")
       Dim _Description As New  StringLiteral("Description", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
      '++ Required List ++
       Dim _ItemsItem As New  NonTerminal("ItemsItem")
       _ItemsItem.Rule = KeywordItem.BuildRule(G, Rules) Or KeyItem.BuildRule(G, Rules) Or IdentifierItem.BuildRule(G, Rules) Or IntItem.BuildRule(G, Rules) Or FloatItem.BuildRule(G, Rules) Or StringItem.BuildRule(G, Rules) Or FlagItem.BuildRule(G, Rules) Or EnumItem.BuildRule(G, Rules) Or DefItem.BuildRule(G, Rules) Or RefItem.BuildRule(G, Rules) Or RegexItem.BuildRule(G, Rules)
       Dim _Items As New  NonTerminal("Items")
       _Items.Rule = Grammar.MakePlusRule(_Items, Nothing, _ItemsItem)
        _Rule.Rule = G.ToTerm("rule") + _Name + _Description + G.ToTerm("{") + _Items + G.ToTerm("}")
        Return _Rule
    End Function
End Class


Public Class KeywordItem
    Inherits AstNode
    Public Shared Event KeywordItemCreated(ByRef _KeywordItem As KeywordItem)
      Property KeywordText As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      KeywordText = treeNode.ChildNodes(0).Token.Value
        RaiseEvent KeywordItemCreated(Me)
        Language.KeywordItemList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("KeywordItem") Then
            Return Rules("KeywordItem")
        End If
        Dim _KeywordItem As New NonTerminal("KeywordItem", GetType(KeywordItem))
        Rules("KeywordItem") = _KeywordItem
       Dim _KeywordText As New  StringLiteral("KeywordText", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _KeywordItem.Rule = _KeywordText
        Return _KeywordItem
    End Function
End Class


Public Class KeyItem
    Inherits AstNode
    Public Shared Event KeyItemCreated(ByRef _KeyItem As KeyItem)
      Property Description As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      Description = treeNode.ChildNodes(2).Token.Value
        RaiseEvent KeyItemCreated(Me)
        Language.KeyItemList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("KeyItem") Then
            Return Rules("KeyItem")
        End If
        Dim _KeyItem As New NonTerminal("KeyItem", GetType(KeyItem))
        Rules("KeyItem") = _KeyItem
       Dim _Description As New  StringLiteral("Description", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _KeyItem.Rule = G.ToTerm("key") + G.ToTerm("desc") + _Description
        Return _KeyItem
    End Function
End Class


Public Class IdentifierItem
    Inherits AstNode
    Public Shared Event IdentifierItemCreated(ByRef _IdentifierItem As IdentifierItem)
      Property IsOptional As Boolean = False
      Property Name As String = ""
      Property ListOption As ListOption
      Property Description As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      IsOptional = treeNode.ChildNodes(0).ChildNodes.Count > 0
      Name = treeNode.ChildNodes(2).Token.Value
      If treeNode.ChildNodes(3).ChildNodes.Count > 0 Then ListOption = treeNode.ChildNodes(3).ChildNodes(0).AstNode
      Description = treeNode.ChildNodes(5).Token.Value
        RaiseEvent IdentifierItemCreated(Me)
        Language.IdentifierItemList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("IdentifierItem") Then
            Return Rules("IdentifierItem")
        End If
        Dim _IdentifierItem As New NonTerminal("IdentifierItem", GetType(IdentifierItem))
        Rules("IdentifierItem") = _IdentifierItem
       Dim _IsOptional As New NonTerminal("IsOptional")
       _IsOptional.Rule = G.ToTerm("opt") Or G.Empty
       Dim _Name As New  IdentifierTerminal("Name")
      'Optional Object
       Dim _ListOption As New  NonTerminal("ListOption")
       Dim _ListOptionItem As New  NonTerminal("ListOptionItem")
       _ListOption.Rule = G.Empty Or ListOption.BuildRule(G, Rules)
       Dim _Description As New  StringLiteral("Description", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _IdentifierItem.Rule = _IsOptional + G.ToTerm("id") + _Name + _ListOption + G.ToTerm("desc") + _Description
        Return _IdentifierItem
    End Function
End Class


Public Class IntItem
    Inherits AstNode
    Public Shared Event IntItemCreated(ByRef _IntItem As IntItem)
      Property IsOptional As Boolean = False
      Property Name As String = ""
      Property ListOption As ListOption
      Property Description As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      IsOptional = treeNode.ChildNodes(0).ChildNodes.Count > 0
      Name = treeNode.ChildNodes(2).Token.Value
      If treeNode.ChildNodes(3).ChildNodes.Count > 0 Then ListOption = treeNode.ChildNodes(3).ChildNodes(0).AstNode
      Description = treeNode.ChildNodes(5).Token.Value
        RaiseEvent IntItemCreated(Me)
        Language.IntItemList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("IntItem") Then
            Return Rules("IntItem")
        End If
        Dim _IntItem As New NonTerminal("IntItem", GetType(IntItem))
        Rules("IntItem") = _IntItem
       Dim _IsOptional As New NonTerminal("IsOptional")
       _IsOptional.Rule = G.ToTerm("opt") Or G.Empty
       Dim _Name As New  IdentifierTerminal("Name")
      'Optional Object
       Dim _ListOption As New  NonTerminal("ListOption")
       Dim _ListOptionItem As New  NonTerminal("ListOptionItem")
       _ListOption.Rule = G.Empty Or ListOption.BuildRule(G, Rules)
       Dim _Description As New  StringLiteral("Description", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _IntItem.Rule = _IsOptional + G.ToTerm("int") + _Name + _ListOption + G.ToTerm("desc") + _Description
        Return _IntItem
    End Function
End Class


Public Class FloatItem
    Inherits AstNode
    Public Shared Event FloatItemCreated(ByRef _FloatItem As FloatItem)
      Property IsOptional As Boolean = False
      Property Name As String = ""
      Property ListOption As ListOption
      Property Description As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      IsOptional = treeNode.ChildNodes(0).ChildNodes.Count > 0
      Name = treeNode.ChildNodes(2).Token.Value
      If treeNode.ChildNodes(3).ChildNodes.Count > 0 Then ListOption = treeNode.ChildNodes(3).ChildNodes(0).AstNode
      Description = treeNode.ChildNodes(5).Token.Value
        RaiseEvent FloatItemCreated(Me)
        Language.FloatItemList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("FloatItem") Then
            Return Rules("FloatItem")
        End If
        Dim _FloatItem As New NonTerminal("FloatItem", GetType(FloatItem))
        Rules("FloatItem") = _FloatItem
       Dim _IsOptional As New NonTerminal("IsOptional")
       _IsOptional.Rule = G.ToTerm("opt") Or G.Empty
       Dim _Name As New  IdentifierTerminal("Name")
      'Optional Object
       Dim _ListOption As New  NonTerminal("ListOption")
       Dim _ListOptionItem As New  NonTerminal("ListOptionItem")
       _ListOption.Rule = G.Empty Or ListOption.BuildRule(G, Rules)
       Dim _Description As New  StringLiteral("Description", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _FloatItem.Rule = _IsOptional + G.ToTerm("float") + _Name + _ListOption + G.ToTerm("desc") + _Description
        Return _FloatItem
    End Function
End Class


Public Class StringItem
    Inherits AstNode
    Public Shared Event StringItemCreated(ByRef _StringItem As StringItem)
      Property IsOptional As Boolean = False
      Property Name As String = ""
      Property ListOption As ListOption
      Property Description As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      IsOptional = treeNode.ChildNodes(0).ChildNodes.Count > 0
      Name = treeNode.ChildNodes(2).Token.Value
      If treeNode.ChildNodes(3).ChildNodes.Count > 0 Then ListOption = treeNode.ChildNodes(3).ChildNodes(0).AstNode
      Description = treeNode.ChildNodes(5).Token.Value
        RaiseEvent StringItemCreated(Me)
        Language.StringItemList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("StringItem") Then
            Return Rules("StringItem")
        End If
        Dim _StringItem As New NonTerminal("StringItem", GetType(StringItem))
        Rules("StringItem") = _StringItem
       Dim _IsOptional As New NonTerminal("IsOptional")
       _IsOptional.Rule = G.ToTerm("opt") Or G.Empty
       Dim _Name As New  IdentifierTerminal("Name")
      'Optional Object
       Dim _ListOption As New  NonTerminal("ListOption")
       Dim _ListOptionItem As New  NonTerminal("ListOptionItem")
       _ListOption.Rule = G.Empty Or ListOption.BuildRule(G, Rules)
       Dim _Description As New  StringLiteral("Description", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _StringItem.Rule = _IsOptional + G.ToTerm("string") + _Name + _ListOption + G.ToTerm("desc") + _Description
        Return _StringItem
    End Function
End Class


Public Class FlagItem
    Inherits AstNode
    Public Shared Event FlagItemCreated(ByRef _FlagItem As FlagItem)
      Property Name As String = ""
      Property FlagText As String = ""
      Property Description As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      Name = treeNode.ChildNodes(1).Token.Value
      FlagText = treeNode.ChildNodes(2).Token.Value
      Description = treeNode.ChildNodes(4).Token.Value
        RaiseEvent FlagItemCreated(Me)
        Language.FlagItemList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("FlagItem") Then
            Return Rules("FlagItem")
        End If
        Dim _FlagItem As New NonTerminal("FlagItem", GetType(FlagItem))
        Rules("FlagItem") = _FlagItem
       Dim _Name As New  IdentifierTerminal("Name")
       Dim _FlagText As New  StringLiteral("FlagText", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
       Dim _Description As New  StringLiteral("Description", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _FlagItem.Rule = G.ToTerm("flag") + _Name + _FlagText + G.ToTerm("desc") + _Description
        Return _FlagItem
    End Function
End Class


Public Class EnumItem
    Inherits AstNode
    Public Shared Event EnumItemCreated(ByRef _EnumItem As EnumItem)
      Property IsOptional As Boolean = False
      Property Name As String = ""
      Property EnumerationItems As New List(Of String)
      Property Description As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      IsOptional = treeNode.ChildNodes(0).ChildNodes.Count > 0
      Name = treeNode.ChildNodes(2).Token.Value
      If treeNode.ChildNodes(4).ChildNodes.Count > 0 Then
         For I As Integer = 0  To treeNode.ChildNodes(4).ChildNodes.Count -1
             EnumerationItems.Add(treeNode.ChildNodes(4).ChildNodes(I).Token.Value)
         Next
      End If
      Description = treeNode.ChildNodes(7).Token.Value
        RaiseEvent EnumItemCreated(Me)
        Language.EnumItemList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("EnumItem") Then
            Return Rules("EnumItem")
        End If
        Dim _EnumItem As New NonTerminal("EnumItem", GetType(EnumItem))
        Rules("EnumItem") = _EnumItem
       Dim _IsOptional As New NonTerminal("IsOptional")
       _IsOptional.Rule = G.ToTerm("opt") Or G.Empty
       Dim _Name As New  IdentifierTerminal("Name")
       Dim _EnumerationItems As New  NonTerminal("EnumerationItems")
       _EnumerationItems.Rule = Grammar.MakePlusRule(_EnumerationItems, G.ToTerm(",") , New IdentifierTerminal("EnumerationItems"))
       Dim _Description As New  StringLiteral("Description", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _EnumItem.Rule = _IsOptional + G.ToTerm("enum") + _Name + G.ToTerm("(") + _EnumerationItems + G.ToTerm(")") + G.ToTerm("desc") + _Description
        Return _EnumItem
    End Function
End Class


Public Class DefItem
    Inherits AstNode
    Public Shared Event DefItemCreated(ByRef _DefItem As DefItem)
      Property IsOptional As Boolean = False
      Property Name As String = ""
      Property Rules As New List(Of Rule)
      Property ListOption As ListOption
      Property Description As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      IsOptional = treeNode.ChildNodes(0).ChildNodes.Count > 0
      Name = treeNode.ChildNodes(2).Token.Value
      If treeNode.ChildNodes(4).ChildNodes.Count > 0 Then
         For I As Integer = 0  To treeNode.ChildNodes(4).ChildNodes.Count -1
             Rules.Add(New Rule With {.Name = treeNode.ChildNodes(4).ChildNodes(I).Token.Value})
         Next
      End If
      If treeNode.ChildNodes(6).ChildNodes.Count > 0 Then ListOption = treeNode.ChildNodes(6).ChildNodes(0).AstNode
      Description = treeNode.ChildNodes(8).Token.Value
        RaiseEvent DefItemCreated(Me)
        Language.DefItemList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("DefItem") Then
            Return Rules("DefItem")
        End If
        Dim _DefItem As New NonTerminal("DefItem", GetType(DefItem))
        Rules("DefItem") = _DefItem
       Dim _IsOptional As New NonTerminal("IsOptional")
       _IsOptional.Rule = G.ToTerm("opt") Or G.Empty
       Dim _Name As New  IdentifierTerminal("Name")
       Dim _Rules As New  NonTerminal("Rules")
       _Rules.Rule = Grammar.MakePlusRule(_Rules, G.ToTerm(",") ,New IdentifierTerminal("Rules"))
      'Optional Object
       Dim _ListOption As New  NonTerminal("ListOption")
       Dim _ListOptionItem As New  NonTerminal("ListOptionItem")
       _ListOption.Rule = G.Empty Or ListOption.BuildRule(G, Rules)
       Dim _Description As New  StringLiteral("Description", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _DefItem.Rule = _IsOptional + G.ToTerm("def") + _Name + G.ToTerm("(") + _Rules + G.ToTerm(")") + _ListOption + G.ToTerm("desc") + _Description
        Return _DefItem
    End Function
End Class


Public Class RefItem
    Inherits AstNode
    Public Shared Event RefItemCreated(ByRef _RefItem As RefItem)
      Property IsOptional As Boolean = False
      Property Name As String = ""
      Property RuleRef As Rule
      Property ListOption As ListOption
      Property Description As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      IsOptional = treeNode.ChildNodes(0).ChildNodes.Count > 0
      Name = treeNode.ChildNodes(2).Token.Value
      RuleRef = New Rule With { .Name = treeNode.ChildNodes(4).ChildNodes(0).Token.Value }
      If treeNode.ChildNodes(6).ChildNodes.Count > 0 Then ListOption = treeNode.ChildNodes(6).ChildNodes(0).AstNode
      Description = treeNode.ChildNodes(8).Token.Value
        RaiseEvent RefItemCreated(Me)
        Language.RefItemList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("RefItem") Then
            Return Rules("RefItem")
        End If
        Dim _RefItem As New NonTerminal("RefItem", GetType(RefItem))
        Rules("RefItem") = _RefItem
       Dim _IsOptional As New NonTerminal("IsOptional")
       _IsOptional.Rule = G.ToTerm("opt") Or G.Empty
       Dim _Name As New  IdentifierTerminal("Name")
       Dim _RuleRef As New  NonTerminal("RuleRef")
       _RuleRef.Rule = New IdentifierTerminal("RuleRef")
      'Optional Object
       Dim _ListOption As New  NonTerminal("ListOption")
       Dim _ListOptionItem As New  NonTerminal("ListOptionItem")
       _ListOption.Rule = G.Empty Or ListOption.BuildRule(G, Rules)
       Dim _Description As New  StringLiteral("Description", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _RefItem.Rule = _IsOptional + G.ToTerm("ref") + _Name + G.ToTerm("(") + _RuleRef + G.ToTerm(")") + _ListOption + G.ToTerm("desc") + _Description
        Return _RefItem
    End Function
End Class


Public Class RegexItem
    Inherits AstNode
    Public Shared Event RegexItemCreated(ByRef _RegexItem As RegexItem)
      Property IsOptional As Boolean = False
      Property Pattern As String = ""
      Property Name As String = ""
      Property ListOption As ListOption
      Property Description As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      IsOptional = treeNode.ChildNodes(0).ChildNodes.Count > 0
      Pattern = treeNode.ChildNodes(3).Token.Value
      Name = treeNode.ChildNodes(5).Token.Value
      If treeNode.ChildNodes(6).ChildNodes.Count > 0 Then ListOption = treeNode.ChildNodes(6).ChildNodes(0).AstNode
      Description = treeNode.ChildNodes(8).Token.Value
        RaiseEvent RegexItemCreated(Me)
        Language.RegexItemList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("RegexItem") Then
            Return Rules("RegexItem")
        End If
        Dim _RegexItem As New NonTerminal("RegexItem", GetType(RegexItem))
        Rules("RegexItem") = _RegexItem
       Dim _IsOptional As New NonTerminal("IsOptional")
       _IsOptional.Rule = G.ToTerm("opt") Or G.Empty
       Dim _Pattern As New  StringLiteral("Pattern", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
       Dim _Name As New  IdentifierTerminal("Name")
      'Optional Object
       Dim _ListOption As New  NonTerminal("ListOption")
       Dim _ListOptionItem As New  NonTerminal("ListOptionItem")
       _ListOption.Rule = G.Empty Or ListOption.BuildRule(G, Rules)
       Dim _Description As New  StringLiteral("Description", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _RegexItem.Rule = _IsOptional + G.ToTerm("regex") + G.ToTerm("(") + _Pattern + G.ToTerm(")") + _Name + _ListOption + G.ToTerm("desc") + _Description
        Return _RegexItem
    End Function
End Class


Public Class ListOption
    Inherits AstNode
    Public Shared Event ListOptionCreated(ByRef _ListOption As ListOption)
      Property Separator As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      If treeNode.ChildNodes(1).ChildNodes.Count > 0 Then Separator = treeNode.ChildNodes(1).ChildNodes(0).Token.Value
        RaiseEvent ListOptionCreated(Me)
        Language.ListOptionList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("ListOption") Then
            Return Rules("ListOption")
        End If
        Dim _ListOption As New NonTerminal("ListOption", GetType(ListOption))
        Rules("ListOption") = _ListOption
       Dim _Separator As New  NonTerminal("Separator")
       _Separator.Rule = G.Empty Or New StringLiteral("Separator", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _ListOption.Rule = G.ToTerm("[") + _Separator + G.ToTerm("]")
        Return _ListOption
    End Function
End Class


Public Class Compile
    Inherits AstNode
    Public Shared Event CompileCreated(ByRef _Compile As Compile)
      Property Name As String = ""
      Property OutputFile As String = ""
      Property Body As New List(Of SimpleText)
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      Name = treeNode.ChildNodes(1).Token.Value
      OutputFile = treeNode.ChildNodes(3).Token.Value
      If treeNode.ChildNodes(5).ChildNodes.Count > 0 Then
         For I As Integer = 0  To treeNode.ChildNodes(5).ChildNodes.Count -1
             Body.Add(treeNode.ChildNodes(5).ChildNodes(I).ChildNodes(0).AstNode)
         Next
      End If
        RaiseEvent CompileCreated(Me)
        Language.CompileList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("Compile") Then
            Return Rules("Compile")
        End If
        Dim _Compile As New NonTerminal("Compile", GetType(Compile))
        Rules("Compile") = _Compile
       Dim _Name As New IdentifierTerminal("Name")
       Dim _OutputFile As New  StringLiteral("OutputFile", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
      '++ Required List ++
       Dim _BodyItem As New  NonTerminal("BodyItem")
       _BodyItem.Rule = SimpleText.BuildRule(G, Rules)
       Dim _Body As New  NonTerminal("Body")
       _Body.Rule = Grammar.MakePlusRule(_Body, Nothing, _BodyItem)
        _Compile.Rule = G.ToTerm("compile") + _Name + G.ToTerm("to") + _OutputFile + G.ToTerm("{") + _Body + G.ToTerm("}")
        Return _Compile
    End Function
End Class


Public Class TypeCompile
    Inherits AstNode
    Public Shared Event TypeCompileCreated(ByRef _TypeCompile As TypeCompile)
      Property RuleRef As Rule
      Property Name As String = ""
      Property OutputFile As String = ""
      Property Body As New List(Of SimpleText)
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      RuleRef = New Rule With { .Name = treeNode.ChildNodes(2).ChildNodes(0).Token.Value }
      Name = treeNode.ChildNodes(4).Token.Value
      OutputFile = treeNode.ChildNodes(6).Token.Value
      If treeNode.ChildNodes(8).ChildNodes.Count > 0 Then
         For I As Integer = 0  To treeNode.ChildNodes(8).ChildNodes.Count -1
             Body.Add(treeNode.ChildNodes(8).ChildNodes(I).ChildNodes(0).AstNode)
         Next
      End If
        RaiseEvent TypeCompileCreated(Me)
        Language.TypeCompileList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("TypeCompile") Then
            Return Rules("TypeCompile")
        End If
        Dim _TypeCompile As New NonTerminal("TypeCompile", GetType(TypeCompile))
        Rules("TypeCompile") = _TypeCompile
       Dim _RuleRef As New  NonTerminal("RuleRef")
       _RuleRef.Rule = New IdentifierTerminal("RuleRef")
       Dim _Name As New IdentifierTerminal("Name")
       Dim _OutputFile As New  StringLiteral("OutputFile", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
      '++ Required List ++
       Dim _BodyItem As New  NonTerminal("BodyItem")
       _BodyItem.Rule = SimpleText.BuildRule(G, Rules)
       Dim _Body As New  NonTerminal("Body")
       _Body.Rule = Grammar.MakePlusRule(_Body, Nothing, _BodyItem)
        _TypeCompile.Rule = G.ToTerm("compile") + G.ToTerm("(") + _RuleRef + G.ToTerm(")") + _Name + G.ToTerm("to") + _OutputFile + G.ToTerm("{") + _Body + G.ToTerm("}")
        Return _TypeCompile
    End Function
End Class


Public Class SimpleText
    Inherits AstNode
    Public Shared Event SimpleTextCreated(ByRef _SimpleText As SimpleText)
      Property Text As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      Text = treeNode.ChildNodes(0).Token.Value
        RaiseEvent SimpleTextCreated(Me)
        Language.SimpleTextList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("SimpleText") Then
            Return Rules("SimpleText")
        End If
        Dim _SimpleText As New NonTerminal("SimpleText", GetType(SimpleText))
        Rules("SimpleText") = _SimpleText
       Dim _Text As New  StringLiteral("Text", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak)
        _SimpleText.Rule = _Text
        Return _SimpleText
    End Function
End Class


Public Class InitialTemplate
    Inherits AstNode
    Public Shared Event InitialTemplateCreated(ByRef _InitialTemplate As InitialTemplate)
      Property Lines As New List(Of String)
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
      If treeNode.ChildNodes(2).ChildNodes.Count > 0 Then
         For I As Integer = 0  To treeNode.ChildNodes(2).ChildNodes.Count -1
             Lines.Add(treeNode.ChildNodes(2).ChildNodes(I).Token.Value)
         Next
      End If
        RaiseEvent InitialTemplateCreated(Me)
        Language.InitialTemplateList.Add(Me)
        Language.All.Add(Me)
    End Sub
    Public Shared Function BuildRule(G As Grammar , ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey("InitialTemplate") Then
            Return Rules("InitialTemplate")
        End If
        Dim _InitialTemplate As New NonTerminal("InitialTemplate", GetType(InitialTemplate))
        Rules("InitialTemplate") = _InitialTemplate
       Dim _Lines As New  NonTerminal("Lines")
       _Lines.Rule = Grammar.MakePlusRule(_Lines, Nothing ,New StringLiteral("Lines", Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak))
        _InitialTemplate.Rule = G.ToTerm("template") + G.ToTerm("{") + _Lines + G.ToTerm("}")
        Return _InitialTemplate
    End Function
End Class


'#START
Public Class  FlameLangCompiler
   Implements ICompiler
   Public Sub Compile(_Language As Language, OutputFolder As String, SourceFileFolder As String) Implements ICompiler.Compile
       Dim ProjAssm = Assembly.GetExecutingAssembly()
       Dim Items = From AMS In ProjAssm.GetTypes Where AMS.Namespace = Assembly.GetExecutingAssembly().GetName().Name & ".Generators"
       Dim Generators As New List(Of IGenerator)
       For Each _GeneratorClass As Type In Items
           If _GeneratorClass.FullName.Contains("+") Then Continue For
           Generators.Add(Activator.CreateInstance(_GeneratorClass))
       Next
       Dim TypeItems = From AMS In ProjAssm.GetTypes Where AMS.Namespace = Assembly.GetExecutingAssembly().GetName().Name & ".TypeGenerators"
       Dim TypeGenerators As New List(Of ITypeGenerator)
       For Each _TypeGeneratorClass As Type In TypeItems
           If _TypeGeneratorClass.FullName.Contains("+") Then Continue For
           TypeGenerators.Add(Activator.CreateInstance(_TypeGeneratorClass))
       Next
       Dim OutputItems = From AMS In ProjAssm.GetTypes Where AMS.Namespace = Assembly.GetExecutingAssembly().GetName().Name & ".OutputGenerators"
       Dim OutputGenerators As New List(Of IOutputGenerator)
       For Each _OutputGeneratorClass As Type In OutputItems
           If _OutputGeneratorClass.FullName.Contains("+") Then Continue For
           OutputGenerators.Add(Activator.CreateInstance(_OutputGeneratorClass))
       Next
       Dim FileDic As New Hashtable
       For Each G In From Gn In Generators Order By Gn.Order 
            If FileDic.ContainsKey(G.FileName) Then
                Dim B As System.Text.StringBuilder = FileDic(G.FileName)
                B.AppendLine(G.Generate(_Language))
            Else
                Dim NewB As New System.Text.StringBuilder
                FileDic(G.FileName) = NewB
                NewB.AppendLine(G.Generate(_Language))
            End If
       Next
       For Each TypeObject As Object In Language.All
           For Each TG As ITypeGenerator In TypeGenerators
               Dim TGF As TypeFile = TG.Generate(TypeObject ,_Language)
               If TGF IsNot Nothing Then FileDic(TGF.FileName) =  TGF.SourceCode
           Next
       Next
        For Each TypeObject As Object In Language.All
            For Each TG As IOutputGenerator In OutputGenerators
                Dim TGF As TypeFile = TG.Generate(TypeObject, _Language)
                If TGF IsNot Nothing Then
                    If FileDic.ContainsKey(TGF.FileName) Then
                        FileDic(TGF.FileName) = FileDic(TGF.FileName) & vbNewLine & TGF.SourceCode
                    Else
                        FileDic(TGF.FileName) = TGF.SourceCode
                    End If
                End If
            Next
        Next
       Dim GeneratedLinesOfCode As Integer = 0
       Dim GeneratedFiles As Integer = 0
       For Each X As DictionaryEntry In FileDic
            Dim FullOutputPath As String
            If $"{X.Key}".Contains(":") Then
                FullOutputPath = $"{X.Key}"
            Else
               FullOutputPath = $"{OutputFolder}\{X.Key}"
            End If
            CreatePathIfRequired(FullOutputPath)
            My.Computer.FileSystem.WriteAllText(FullOutputPath, X.Value.ToString, False , New System.Text.UTF8Encoding(False))
            GeneratedLinesOfCode += Split(X.Value.ToString, vbNewLine).Count
            GeneratedFiles +=1
       Next
        Dim Batches = From AMS In ProjAssm.GetTypes Where AMS.Namespace = Assembly.GetExecutingAssembly().GetName().Name & ".Batches"
        Dim BatchesList As New List(Of ITypeGenerator)
        For Each _Batch As Type In Batches
            If _Batch.FullName.Contains("+") Then Continue For
            Dim B As IBatch = Activator.CreateInstance(_Batch)
            For Each _Object As Object In Language.All
                B.Run(_Object, OutputFolder,SourceFileFolder)
           Next
       Next
       MsgBox($"Code Generation Finished ({GeneratedFiles} files ,{GeneratedLinesOfCode} lines of code)")
   End Sub
   Sub CreatePathIfRequired(Path As String)
       Dim X = Split(Path, "\").ToList
       X.RemoveAt(X.Count - 1)
       Dim PurePath As String = Join(X.ToArray, "\")
       If Not IO.Directory.Exists(PurePath) Then
            IO.Directory.CreateDirectory(PurePath)
       End If
   End Sub
End Class
Public Interface ICompiler
    Sub Compile(_Language As Language, OutputFolder As String, SourceFileFolder As String)
End Interface
Public Interface IGenerator
    Property FileName As String
    Property Order As Integer
    Function Generate(_Language As Language) As String
End Interface
Public Interface IBuilder
    Function Generate() As String
    Function IsValid() As Boolean
End Interface
Public Class CodeRegion
    Inherits List(Of String)
    Enum Separators
        None
        Comma
        Line
    End Enum
    Private Separator As Separators = Separators.None
    Private Items As New List(Of String)
    Sub New(Optional _Separator As Separators = Separators.None)
        Separator = _Separator
    End Sub
    Public Overrides Function ToString() As String
        Select Case Me.Separator
            Case Separators.Comma
                Return String.Join(",", ToArray)
            Case Separators.Line
                Return String.Join(vbNewLine, ToArray)
            Case Else
                Return String.Join("", ToArray)
        End Select
    End Function

    Public Shared Operator +(_CodeRegion As CodeRegion, Item As String) As CodeRegion
        _CodeRegion.Add(Item)
        Return _CodeRegion
    End Operator
End Class
Public Interface ITypeGenerator
    Function Generate(_Type As Object,_Language As Language) As TypeFile
End Interface
Public Interface IOutputGenerator
    Function Generate(_Type As Object,_Language As Language) As TypeFile
End Interface
Public Class TypeFile
    Property FileName As String = ""
    Property SourceCode As String = ""
End Class
Namespace TypeGenerators

End Namespace 'TypeGenerators
Namespace OutputGenerators

End Namespace 'TypeGenerators
Namespace Generators

End Namespace 'Generators
'Buildes -------------------------------------------------------

'End Builders --------------------------------------------------
'#END

Public Interface IBatch
    Sub Run(Root As Object, OutputFolder As String, SourceFileFolder As String)
End Interface
Namespace Batches

End Namespace
