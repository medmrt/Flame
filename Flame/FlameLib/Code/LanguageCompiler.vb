Imports System.Text
Imports Irony.Ast
Imports Irony.Parsing
Imports Newtonsoft.Json.Linq
Imports Scriban
Imports Scriban.Runtime

Public Class LanguageCompiler
    Inherits Grammar

    Dim RulesDic As New Hashtable
    Public SyntaxErrors As New List(Of ErrorInformation)
    ' Public SemanticErrors As New List(Of ErrorInformation)
    Dim CurrentLanguage As Language
    Sub New(P As Language)
        Me.NonGrammarTerminals.Add(New CommentTerminal("LINE_COMMENT", "//", vbNewLine))
        Me.NonGrammarTerminals.Add(New CommentTerminal("BLOCK_COMMENT", "/*", "*/"))
        Me.LanguageFlags = LanguageFlags.CreateAst
        CurrentLanguage = P
        LanguageCompiler.RefHash = New Hashtable
        Me.Root = JsonObject.Create(P.Rules.First, Me, RulesDic)

    End Sub

    Function Compile(Prg As Language, tree As ParseTree) As List(Of OutputFile)
        Dim _OutputFiles As New List(Of OutputFile)

        For Each Comp In Prg.Compilers.OfType(Of Compile)
            Dim Lines As New List(Of String)
            For Each ln In Comp.Body
                Lines.Add(ln.Text)
            Next
            Dim TemplateText As String = Join(Lines.ToArray, vbNewLine)
            Dim obj = ScriptObject.From(tree.Root.AstNode.RuleObject)
            Dim x = Template.Parse(TemplateText)


            _OutputFiles.Add(New OutputFile(Comp.OutputFile, x.Render(obj)))


            ' txtOuput.Text = $"{Comp.OutputFile}{vbNewLine }" & x.Render(obj) & $"{vbNewLine}{obj.ToString()}"

        Next

        Return _OutputFiles
    End Function


    Function ParseTemplate(Prg As Language) As TemplateError
        Dim _OutputFiles As New List(Of OutputFile)
        'Dim tree As ParseTree = Nothing

        For Each Comp In Prg.Compilers.OfType(Of Compile)
            Dim Lines As New List(Of String)
            For Each ln In Comp.Body
                Lines.Add(ln.Text)
            Next
            Dim TemplateText As String = Join(Lines.ToArray, vbNewLine)
            Dim obj = ScriptObject.From(New EmptyItem)
            Dim x = Template.Parse(TemplateText)

            If x.HasErrors Then
                Dim firstLineOfBody As SimpleText = Comp.Body.First
                Return New TemplateError With {.Message = x.Messages.First.Message, .Line = x.Messages.First.Span.Start.Line + (firstLineOfBody.Location.Line)}

            End If



        Next

        Return Nothing
    End Function


    Public Class TemplateError
        Property Message As String = ""
        Property Line As Integer = 0
    End Class

    Public Class EmptyItem

    End Class

    Function Parce(SourceCode As String) As Object
        Dim y As New Irony.Parsing.Parser(Me)
        Dim x As ParseTree = y.Parse(SourceCode)
        If x.HasErrors Then


            For Each Errs In x.ParserMessages
                'SyntaxErrors.Add(New ErrorInformation(Errs.Message, Errs.Location))



                'For Each Er In Errs.ParserState.ExpectedTerminals

                '    SyntaxErrors.Add(New ErrorInformation(Er.ErrorAlias & $"{Er.}", Errs.Location))
                'Next




                SyntaxErrors.Add(New ErrorInformation(Errs.Message, Errs.Location))
            Next


            Return SyntaxErrors
        End If

        'RefProperty.StartBining()
        Rebind(x)
        Return x

    End Function


    Sub Rebind(ByRef Obj As Object)


        RebindJObject(Obj.Root.AstNode.RuleObject)

    End Sub

    Sub RebindJObject(ByRef Obj As JObject)
        'MsgBox(Obj("Type"))
        If Obj.ContainsKey("ReferenceType") And Obj.ContainsKey("ReferenceName") And LanguageCompiler.RefHash.ContainsKey($"{Obj("ReferenceType")}@{Obj("ReferenceName")}") Then
            Obj = LanguageCompiler.RefHash($"{Obj("ReferenceType")}@{Obj("ReferenceName")}")
            Exit Sub
        End If

        For Each it In Obj.Properties
            If TypeOf it.Value Is JObject Then
                RebindJObject(it.Value)
            End If
            If TypeOf it.Value Is JArray Then
                Dim Ar As JArray = it.Value
                Dim NewArr As New JArray
                For Each ArItem In Ar
                    If TypeOf ArItem Is JObject Then
                        RebindJObject(ArItem)

                    End If
                    NewArr.Add(ArItem)
                Next
                it.Value = NewArr
            End If
        Next
    End Sub

    Public Shared RefHash As Hashtable

End Class


Public Class JsonObject
    Inherits AstNode

    Public RuleObject As New JObject



    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        RuleObject("Type") = $"{Term.Name}"
        RuleObject($"{Term.Name}") = True

        For Each it In treeNode.ChildNodes
            If TypeOf it.AstNode Is KeyProperty Then
                Dim Prop As KeyProperty = it.AstNode
                RuleObject("Name") = $"{Prop.Value}"
                LanguageCompiler.RefHash($"{Term.Name}@{Prop.Value}") = RuleObject
            End If

            If TypeOf it.AstNode Is StringProperty Then
                Dim Prop As StringProperty = it.AstNode
                RuleObject(Prop.Name) = $"{Prop.Value}"
            End If

            If TypeOf it.AstNode Is StringListProperty Then
                Dim Prop As StringListProperty = it.AstNode
                Dim Ar As New JArray
                For Each item In Prop.Values
                    Ar.Add(item)
                Next
                RuleObject(Prop.Name) = Ar
            End If


            If TypeOf it.AstNode Is IdentifierProperty Then
                Dim Prop As IdentifierProperty = it.AstNode
                RuleObject(Prop.Name) = $"{Prop.Value}"
            End If

            If TypeOf it.AstNode Is IdentifierListProperty Then
                Dim Prop As IdentifierListProperty = it.AstNode
                Dim Ar As New JArray
                For Each item In Prop.Values
                    Ar.Add(item)
                Next
                RuleObject(Prop.Name) = Ar
            End If


            If TypeOf it.AstNode Is IntProperty Then
                Dim Prop As IntProperty = it.AstNode
                RuleObject(Prop.Name) = Prop.Value
            End If

            If TypeOf it.AstNode Is IntListProperty Then
                Dim Prop As IntListProperty = it.AstNode
                Dim Ar As New JArray
                For Each item In Prop.Values
                    Ar.Add(item)
                Next
                RuleObject(Prop.Name) = Ar
            End If

            If TypeOf it.AstNode Is FloatProperty Then
                Dim Prop As FloatProperty = it.AstNode
                RuleObject(Prop.Name) = Prop.Value
            End If

            If TypeOf it.AstNode Is FloatListProperty Then
                Dim Prop As FloatListProperty = it.AstNode
                Dim Ar As New JArray
                For Each item In Prop.Values
                    Ar.Add(item)
                Next
                RuleObject(Prop.Name) = Ar
            End If

            If TypeOf it.AstNode Is FlagProperty Then
                Dim Prop As FlagProperty = it.AstNode
                RuleObject(Prop.Name) = Prop.Value
            End If
            If TypeOf it.AstNode Is EnumProperty Then
                Dim Prop As EnumProperty = it.AstNode
                RuleObject(Prop.Name) = Prop.Value
            End If

            If TypeOf it.AstNode Is DefProperty Then
                Dim Prop As DefProperty = it.AstNode
                RuleObject(Prop.Name) = Prop.RuleObject
            End If

            If TypeOf it.AstNode Is DefListProperty Then
                Dim Prop As DefListProperty = it.AstNode
                RuleObject(Prop.Name) = Prop.RuleObject
            End If



            If TypeOf it.AstNode Is RefProperty Then
                Dim Prop As RefProperty = it.AstNode
                RuleObject(Prop.Name) = Prop.Value
            End If

            If TypeOf it.AstNode Is RefListProperty Then
                Dim Prop As RefListProperty = it.AstNode
                Dim Ar As New JArray
                For Each item In Prop.Values
                    Ar.Add(item)
                Next
                RuleObject(Prop.Name) = Ar
            End If

            If TypeOf it.AstNode Is RegProperty Then
                Dim Prop As RegProperty = it.AstNode
                RuleObject(Prop.Name) = Prop.Value
            End If

            If TypeOf it.AstNode Is RegListProperty Then
                Dim Prop As RegListProperty = it.AstNode
                Dim Ar As New JArray
                For Each item In Prop.Values
                    Ar.Add(item)
                Next
                RuleObject(Prop.Name) = Ar
            End If
        Next

    End Sub

    Public Shared Function Create(Rule As Rule, G As Grammar, ByRef Rules As Hashtable) As NonTerminal
        If Rules.ContainsKey($"{Rule.Name}") Then
            Return Rules($"{Rule.Name}")
        End If

        Dim ConceptRule As New NonTerminal($"{Rule.Name}", GetType(JsonObject))
        Rules($"{Rule.Name}") = ConceptRule

        ConceptRule.ErrorAlias = $"{Rule.Name}:{Rule.Description}"


        For Each it In Rule.Items
            If TypeOf it Is DefItem Then
                Dim DefItem As DefItem = it
                If DefItem.ListOption Is Nothing Then
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += DefProperty.Create(it, G, Rules)
                    Else
                        ConceptRule.Rule = DefProperty.Create(it, G, Rules)
                    End If
                Else
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += DefListProperty.Create(it, G, Rules)
                    Else
                        ConceptRule.Rule = DefListProperty.Create(it, G, Rules)
                    End If
                End If
            End If



            If TypeOf it Is KeywordItem Then
                If ConceptRule.Rule IsNot Nothing Then
                    ConceptRule.Rule += G.ToTerm(DirectCast(it, KeywordItem).KeywordText)
                Else
                    ConceptRule.Rule = G.ToTerm(DirectCast(it, KeywordItem).KeywordText)
                End If
            End If

            If TypeOf it Is KeyItem Then
                If ConceptRule.Rule IsNot Nothing Then
                    ConceptRule.Rule += KeyProperty.Create(it)
                Else
                    ConceptRule.Rule = KeyProperty.Create(it)
                End If
            End If

            If TypeOf it Is StringItem Then
                Dim StItem As StringItem = it
                If StItem.ListOption Is Nothing Then
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += StringProperty.Create(it, G)
                    Else
                        ConceptRule.Rule = StringProperty.Create(it, G)
                    End If
                Else
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += StringListProperty.Create(it, G)
                    Else
                        ConceptRule.Rule = StringListProperty.Create(it, G)
                    End If
                End If

            End If

            If TypeOf it Is IdentifierItem Then
                Dim IdItem As IdentifierItem = it
                If IdItem.ListOption Is Nothing Then
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += IdentifierProperty.Create(it, G)
                    Else
                        ConceptRule.Rule = IdentifierProperty.Create(it, G)
                    End If
                Else
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += IdentifierListProperty.Create(it, G)
                    Else
                        ConceptRule.Rule = IdentifierListProperty.Create(it, G)
                    End If
                End If
            End If

            If TypeOf it Is IntItem Then
                Dim IntItemObj As IntItem = it
                If IntItemObj.ListOption Is Nothing Then
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += IntProperty.Create(it, G)
                    Else
                        ConceptRule.Rule = IntProperty.Create(it, G)
                    End If
                Else
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += IntListProperty.Create(it, G)
                    Else
                        ConceptRule.Rule = IntListProperty.Create(it, G)
                    End If
                End If
            End If

            If TypeOf it Is FloatItem Then
                Dim FloatItemObj As FloatItem = it
                If FloatItemObj.ListOption Is Nothing Then
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += FloatProperty.Create(it, G)
                    Else
                        ConceptRule.Rule = FloatProperty.Create(it, G)
                    End If
                Else
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += FloatListProperty.Create(it, G)
                    Else
                        ConceptRule.Rule = FloatListProperty.Create(it, G)
                    End If
                End If
            End If

            If TypeOf it Is FlagItem Then
                If ConceptRule.Rule IsNot Nothing Then
                    ConceptRule.Rule += FlagProperty.Create(it, G)
                Else
                    ConceptRule.Rule = FlagProperty.Create(it, G)
                End If
            End If

            If TypeOf it Is EnumItem Then
                If ConceptRule.Rule IsNot Nothing Then
                    ConceptRule.Rule += EnumProperty.Create(it, G)
                Else
                    ConceptRule.Rule = EnumProperty.Create(it, G)
                End If
            End If


            If TypeOf it Is RefItem Then
                Dim R As RefItem = it
                If R.ListOption Is Nothing Then
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += RefProperty.Create(it, G)
                    Else
                        ConceptRule.Rule = RefProperty.Create(it, G)
                    End If
                Else
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += RefListProperty.Create(it, G)
                    Else
                        ConceptRule.Rule = RefListProperty.Create(it, G)
                    End If
                End If
            End If


            If TypeOf it Is RegexItem Then
                Dim R As RegexItem = it
                If R.ListOption Is Nothing Then
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += RegProperty.Create(it, G)
                    Else
                        ConceptRule.Rule = RegProperty.Create(it, G)
                    End If
                Else
                    If ConceptRule.Rule IsNot Nothing Then
                        ConceptRule.Rule += RegListProperty.Create(it, G)
                    Else
                        ConceptRule.Rule = RegListProperty.Create(it, G)
                    End If
                End If
            End If



        Next


        Return ConceptRule
    End Function

End Class

Public Class DefProperty
    Inherits AstNode
    Property Name As String = ""
    Property RuleObject As New JObject

    Public Shared Event BindingStarted()
    Public Shared Sub StartBining()
        RaiseEvent BindingStarted()
    End Sub

    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        Name = treeNode.Term.Name
        'RuleObject("Type") = Name 'treeNode.ChildNodes(0).AstNode

        For Each itm In treeNode.ChildNodes
            'MsgBox(itm.ToString)
            RuleObject("Type") = itm.ToString
            RuleObject($"{itm.ToString}") = True
            For Each it In itm.ChildNodes
                If TypeOf it.AstNode Is KeyProperty Then
                    Dim Prop As KeyProperty = it.AstNode
                    RuleObject("Name") = $"{Prop.Value}"
                    LanguageCompiler.RefHash($"{itm.ToString}@{Prop.Value}") = RuleObject
                End If

                If TypeOf it.AstNode Is StringProperty Then
                    Dim Prop As StringProperty = it.AstNode
                    RuleObject(Prop.Name) = $"{Prop.Value}"
                End If

                If TypeOf it.AstNode Is StringListProperty Then
                    Dim Prop As StringListProperty = it.AstNode
                    Dim Ar As New JArray
                    For Each item In Prop.Values
                        Ar.Add(item)
                    Next
                    RuleObject(Prop.Name) = Ar
                End If


                If TypeOf it.AstNode Is IdentifierProperty Then
                    Dim Prop As IdentifierProperty = it.AstNode
                    RuleObject(Prop.Name) = $"{Prop.Value}"
                End If

                If TypeOf it.AstNode Is IdentifierListProperty Then
                    Dim Prop As IdentifierListProperty = it.AstNode
                    Dim Ar As New JArray
                    For Each item In Prop.Values
                        Ar.Add(item)
                    Next
                    RuleObject(Prop.Name) = Ar
                End If


                If TypeOf it.AstNode Is IntProperty Then
                    Dim Prop As IntProperty = it.AstNode
                    RuleObject(Prop.Name) = Prop.Value
                End If

                If TypeOf it.AstNode Is IntListProperty Then
                    Dim Prop As IntListProperty = it.AstNode
                    Dim Ar As New JArray
                    For Each item In Prop.Values
                        Ar.Add(item)
                    Next
                    RuleObject(Prop.Name) = Ar
                End If

                If TypeOf it.AstNode Is FloatProperty Then
                    Dim Prop As FloatProperty = it.AstNode
                    RuleObject(Prop.Name) = Prop.Value
                End If

                If TypeOf it.AstNode Is FloatListProperty Then
                    Dim Prop As FloatListProperty = it.AstNode
                    Dim Ar As New JArray
                    For Each item In Prop.Values
                        Ar.Add(item)
                    Next
                    RuleObject(Prop.Name) = Ar
                End If

                If TypeOf it.AstNode Is FlagProperty Then
                    Dim Prop As FlagProperty = it.AstNode
                    RuleObject(Prop.Name) = Prop.Value
                End If
                If TypeOf it.AstNode Is EnumProperty Then
                    Dim Prop As EnumProperty = it.AstNode
                    RuleObject(Prop.Name) = Prop.Value
                End If

                If TypeOf it.AstNode Is DefProperty Then
                    Dim Prop As DefProperty = it.AstNode
                    RuleObject(Prop.Name) = Prop.RuleObject
                End If

                If TypeOf it.AstNode Is DefListProperty Then
                    Dim Prop As DefListProperty = it.AstNode
                    RuleObject(Prop.Name) = Prop.RuleObject
                End If

                If TypeOf it.AstNode Is RefProperty Then
                    Dim Prop As RefProperty = it.AstNode
                    RuleObject(Prop.Name) = Prop.Value
                End If

                If TypeOf it.AstNode Is RefListProperty Then
                    Dim Prop As RefListProperty = it.AstNode
                    Dim Ar As New JArray
                    For Each item In Prop.Values
                        Ar.Add(item)
                    Next
                    RuleObject(Prop.Name) = Ar
                End If

                If TypeOf it.AstNode Is RegProperty Then
                    Dim Prop As RegProperty = it.AstNode
                    RuleObject(Prop.Name) = Prop.Value
                End If

                If TypeOf it.AstNode Is RegListProperty Then
                    Dim Prop As RegListProperty = it.AstNode
                    Dim Ar As New JArray
                    For Each item In Prop.Values
                        Ar.Add(item)
                    Next
                    RuleObject(Prop.Name) = Ar
                End If

            Next
        Next

        If RuleObject.ToString = "{}" Then
            RuleObject = Nothing
        End If

    End Sub

    Shared Function Create(it As DefItem, G As Grammar, ByRef Rules As Hashtable) As NonTerminal
        Dim IDRuleExpr As New NonTerminal(it.Name, GetType(DefProperty))
        Dim StrBuilder As New StringBuilder

        For Each R In it.Rules
            StrBuilder.AppendLine($"{R.Name}:{R.Description}")
        Next

        IDRuleExpr.ErrorAlias = StrBuilder.ToString

        If it.IsOptional Then
            IDRuleExpr.Rule = G.Empty
        End If
        For Each Defs In it.Rules
            If IDRuleExpr.Rule Is Nothing Then
                IDRuleExpr.Rule = JsonObject.Create(Defs, G, Rules)
            Else
                IDRuleExpr.Rule = IDRuleExpr.Rule Or JsonObject.Create(Defs, G, Rules)
            End If
        Next
        Return IDRuleExpr
    End Function

End Class
Public Class DefListProperty
    Inherits AstNode
    Property Name As String = ""
    Property RuleObject As New JArray

    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        Name = treeNode.Term.Name
        For Each itm In treeNode.ChildNodes
            RuleObject.Add(itm.AstNode.RuleObject)
        Next
    End Sub

    Shared Function Create(it As DefItem, G As Grammar, ByRef Rules As Hashtable) As NonTerminal
        Dim IDRuleListExpr As New NonTerminal(it.Name, GetType(DefListProperty))

        Dim IDRuleExpr As New NonTerminal($"{it.Name}Item", GetType(DefProperty))

        Dim StrBuilder As New StringBuilder

        For Each R In it.Rules
            StrBuilder.AppendLine($"{R.Name}:{R.Description}")
        Next

        IDRuleExpr.ErrorAlias = StrBuilder.ToString


        For Each Defs In it.Rules
            If IDRuleExpr.Rule Is Nothing Then
                IDRuleExpr.Rule = JsonObject.Create(Defs, G, Rules)
            Else
                IDRuleExpr.Rule = IDRuleExpr.Rule Or JsonObject.Create(Defs, G, Rules)
            End If
        Next

        If it.IsOptional Then
            IDRuleListExpr.Rule = Grammar.MakeStarRule(IDRuleListExpr, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), IDRuleExpr)

        Else
            IDRuleListExpr.Rule = Grammar.MakePlusRule(IDRuleListExpr, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), IDRuleExpr)

        End If

        Return IDRuleListExpr
    End Function

End Class

Public Class KeyProperty
    Inherits AstNode
    Property Value As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)

        Value = treeNode.ChildNodes(0).AstNode.ToString
    End Sub

    Shared Function Create(It As KeyItem) As NonTerminal
        Dim IDRuleExpr As New NonTerminal("Key", GetType(KeyProperty))


        IDRuleExpr.Rule = New IdentifierTerminal("Name") With {.ErrorAlias = $"Name:{It.Description}"}
        Return IDRuleExpr
    End Function

End Class


Public Class StringProperty
    Inherits AstNode
    Property Name As String = ""
    Property Value As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        Name = treeNode.Term.Name
        If treeNode.ChildNodes.Count > 0 Then
            Dim Temp As String = treeNode.ChildNodes(0).AstNode.ToString
            If Temp.Length > 2 Then
                Value = Temp.Substring(1, Temp.Length - 2)
            End If
        End If
    End Sub
    Shared Function Create(it As StringItem, G As Grammar) As NonTerminal
        Dim StrineRuleExpr As New NonTerminal($"{it.Name}", GetType(StringProperty))
        StrineRuleExpr.ErrorAlias = $"{it.Name}:{it.Description}"
        If it.IsOptional Then
            StrineRuleExpr.Rule = G.Empty Or New StringLiteral(it.Name, Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak) With {.ErrorAlias = $"{it.Name}:{it.Description}"}
        Else
            StrineRuleExpr.Rule = New StringLiteral(it.Name, Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak) With {.ErrorAlias = $"{it.Name}:{it.Description}"}
        End If
        Return StrineRuleExpr
    End Function
End Class

Public Class StringListProperty
    Inherits AstNode
    Property Name As String = ""
    Property Values As New List(Of String)
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)

        Name = treeNode.Term.Name

        For Each N In treeNode.ChildNodes(0).ChildNodes
            Dim Temp As String = N.AstNode.ToString
            If Temp.Length > 2 Then
                Values.Add(Temp.Substring(1, Temp.Length - 2))
            End If
        Next

    End Sub


    Shared Function Create(it As StringItem, G As Grammar) As NonTerminal
        Dim StrineRuleExpr As New NonTerminal($"{it.Name}", GetType(StringListProperty))
        Dim StringList As New NonTerminal($"{it.Name}List")
        If it.IsOptional Then
            StringList.Rule = Grammar.MakeStarRule(StringList, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), New StringLiteral(it.Name, Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak) With {.ErrorAlias = $"{it.Name}:{it.Description}"})

        Else
            StringList.Rule = Grammar.MakePlusRule(StringList, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), New StringLiteral(it.Name, Chr(34).ToString, StringOptions.AllowsAllEscapes Or StringOptions.AllowsLineBreak) With {.ErrorAlias = $"{it.Name}:{it.Description} (At Least One)"})

        End If
        StrineRuleExpr.Rule = StringList
        Return StrineRuleExpr
    End Function

End Class


Public Class IdentifierProperty
    Inherits AstNode
    Property Name As String = ""
    Property Value As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        Name = treeNode.Term.Name
        If treeNode.ChildNodes.Count > 0 Then
            Value = treeNode.ChildNodes(0).AstNode.ToString
        End If

    End Sub
    Shared Function Create(it As IdentifierItem, G As Grammar) As NonTerminal
        If it.IsOptional Then
            Dim IDExpr As New NonTerminal($"{it.Name}", GetType(IdentifierProperty))
            IDExpr.Rule = G.Empty Or New IdentifierTerminal(it.Name) With {.ErrorAlias = $"{it.Name}:{it.Description}"}
            Return IDExpr
        Else
            Dim IDExpr As New NonTerminal($"{it.Name}", GetType(IdentifierProperty))
            IDExpr.Rule = New IdentifierTerminal(it.Name) With {.ErrorAlias = $"{it.Name}:{it.Description}"}
            Return IDExpr
        End If
    End Function
End Class

Public Class IdentifierListProperty
    Inherits AstNode
    Property Name As String = ""
    Property Values As New List(Of String)
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)

        Name = treeNode.Term.Name
        For Each N In treeNode.ChildNodes
            Values.Add(N.AstNode.ToString)
        Next

    End Sub
    Shared Function Create(it As IdentifierItem, G As Grammar) As NonTerminal
        If it.IsOptional Then
            Dim IDExpr As New NonTerminal($"{it.Name}", GetType(IdentifierListProperty))
            IDExpr.Rule = Grammar.MakeStarRule(IDExpr, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), New IdentifierTerminal(it.Name) With {.ErrorAlias = $"{it.Name}:{it.Description}"})
            Return IDExpr
        Else
            Dim IDExpr As New NonTerminal($"{it.Name}", GetType(IdentifierListProperty))
            IDExpr.Rule = Grammar.MakePlusRule(IDExpr, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), New IdentifierTerminal(it.Name) With {.ErrorAlias = $"{it.Name}:{it.Description} (One At Least)"})
            Return IDExpr
        End If
    End Function
End Class


Public Class IntProperty
    Inherits AstNode
    Property Name As String = ""
    Property Value As Integer = 0
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        Name = treeNode.Term.Name
        If treeNode.ChildNodes.Count > 0 Then
            Value = Integer.Parse(treeNode.ChildNodes(0).AstNode.ToString)
        End If

    End Sub
    Shared Function Create(it As IntItem, G As Grammar) As NonTerminal

        If it.IsOptional Then
            Dim IntPropertyExprs As New NonTerminal($"{it.Name}", GetType(IntProperty))
            IntPropertyExprs.Rule = G.Empty Or New NumberLiteral(it.Name, NumberOptions.IntOnly Or NumberOptions.AllowSign) With {.ErrorAlias = $"{it.Name}:{it.Description}"}
            Return IntPropertyExprs
        Else
            Dim IntPropertyExprs As New NonTerminal($"{it.Name}", GetType(IntProperty))
            IntPropertyExprs.Rule = New NumberLiteral(it.Name, NumberOptions.IntOnly Or NumberOptions.AllowSign) With {.ErrorAlias = $"{it.Name}:{it.Description}"}
            Return IntPropertyExprs
        End If
    End Function
End Class

Public Class IntListProperty
    Inherits AstNode
    Property Name As String = ""
    Property Values As New List(Of Integer)

    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        Name = treeNode.Term.Name
        For Each N In treeNode.ChildNodes
            Values.Add(Integer.Parse(N.AstNode.ToString))
        Next

    End Sub
    Shared Function Create(it As IntItem, G As Grammar) As NonTerminal
        If it.IsOptional Then
            Dim IntListPropertyExpr As New NonTerminal($"{it.Name}", GetType(IntListProperty))
            IntListPropertyExpr.Rule = Grammar.MakeStarRule(IntListPropertyExpr, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), New NumberLiteral(it.Name, NumberOptions.IntOnly Or NumberOptions.AllowSign) With {.ErrorAlias = $"{it.Name}:{it.Description}"})
            Return IntListPropertyExpr
        Else
            Dim IntListPropertyExpr As New NonTerminal($"{it.Name}", GetType(IntListProperty))
            IntListPropertyExpr.Rule = Grammar.MakePlusRule(IntListPropertyExpr, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), New NumberLiteral(it.Name, NumberOptions.IntOnly Or NumberOptions.AllowSign) With {.ErrorAlias = $"{it.Name}:{it.Description} (One At Least)"})
            Return IntListPropertyExpr
        End If

    End Function
End Class


'******Float


Public Class FloatProperty
    Inherits AstNode
    Property Name As String = ""
    Property Value As Double = 0
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        Name = treeNode.Term.Name
        If treeNode.ChildNodes.Count > 0 Then
            Value = Double.Parse(treeNode.ChildNodes(0).AstNode.ToString)
        End If

    End Sub
    Shared Function Create(it As FloatItem, G As Grammar) As NonTerminal

        If it.IsOptional Then
            Dim IntPropertyExprs As New NonTerminal($"{it.Name}", GetType(FloatProperty))
            IntPropertyExprs.Rule = G.Empty Or New NumberLiteral(it.Name, NumberOptions.AllowSign) With {.ErrorAlias = $"{it.Name}:{it.Description}"}
            Return IntPropertyExprs
        Else
            Dim IntPropertyExprs As New NonTerminal($"{it.Name}", GetType(FloatProperty))
            IntPropertyExprs.Rule = New NumberLiteral(it.Name, NumberOptions.AllowSign) With {.ErrorAlias = $"{it.Name}:{it.Description}"}
            Return IntPropertyExprs
        End If
    End Function
End Class

Public Class FloatListProperty
    Inherits AstNode
    Property Name As String = ""
    Property Values As New List(Of Double)

    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)

        Name = treeNode.Term.Name
        For Each N In treeNode.ChildNodes
            Values.Add(Double.Parse(N.AstNode.ToString))
        Next
    End Sub
    Shared Function Create(it As FloatItem, G As Grammar) As NonTerminal
        If it.IsOptional Then
            Dim IntListPropertyExpr As New NonTerminal($"{it.Name}", GetType(FloatListProperty))
            IntListPropertyExpr.Rule = Grammar.MakeStarRule(IntListPropertyExpr, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), New NumberLiteral(it.Name, NumberOptions.AllowSign) With {.ErrorAlias = $"{it.Name}:{it.Description}"})
            Return IntListPropertyExpr
        Else
            Dim IntListPropertyExpr As New NonTerminal($"{it.Name}", GetType(FloatListProperty))
            IntListPropertyExpr.Rule = Grammar.MakePlusRule(IntListPropertyExpr, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), New NumberLiteral(it.Name, NumberOptions.AllowSign) With {.ErrorAlias = $"{it.Name}:{it.Description} (One At Least)"})
            Return IntListPropertyExpr
        End If

    End Function
End Class

Public Class FlagProperty
    Inherits AstNode
    Property Name As String = ""
    Property Value As Boolean = False

    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        Name = treeNode.Term.Name
        Value = treeNode.ChildNodes.Count > 0
    End Sub

    Shared Function Create(it As FlagItem, G As Grammar) As NonTerminal
        Dim IDRuleExpr As New NonTerminal(it.Name, GetType(FlagProperty))
        IDRuleExpr.Rule = G.Empty Or G.ToTerm($"{it.FlagText}")
        Return IDRuleExpr
    End Function

End Class

Public Class EnumProperty
    Inherits AstNode
    Property Name As String = ""
    Property Value As String = ""

    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        Name = treeNode.Term.Name
        If treeNode.ChildNodes.Count > 0 Then
            Value = treeNode.ChildNodes(0).Token.Text
        End If

    End Sub

    Shared Function Create(it As EnumItem, G As Grammar) As NonTerminal
        Dim IDRuleExpr As New NonTerminal(it.Name, GetType(EnumProperty))

        If it.IsOptional Then
            IDRuleExpr.Rule = G.Empty
        Else

        End If
        IDRuleExpr.ErrorAlias = $"{it.Name}:{it.Description}"
        For Each enumIt In it.EnumerationItems
            If IDRuleExpr.Rule Is Nothing Then
                Dim Itm = G.ToTerm($"{enumIt}")
                Itm.ErrorAlias = $"{enumIt} | {it.Name}:{it.Description}"
                IDRuleExpr.Rule = Itm ' G.ToTerm($"{enumIt}")
            Else
                Dim Itm = G.ToTerm($"{enumIt}")
                Itm.ErrorAlias = $"{enumIt} | {it.Name}:{it.Description}"
                IDRuleExpr.Rule = IDRuleExpr.Rule Or Itm
            End If
        Next

        'IDRuleExpr.Rule = G.Empty Or G.ToTerm($"{it.FlagText}")

        Return IDRuleExpr
    End Function

End Class


Public Class RefProperty
    Inherits AstNode
    Property Name As String = ""
    'Property Value As String = ""
    Property RefObjectType As String = ""
    Property Value As New JObject




    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        Name = treeNode.Term.Name
        'RefObjectType = treeNode.ReduceProduction.RValues(0).Name

        If treeNode.ChildNodes.Count > 0 Then
            'Value("Type") = "Reference"
            Value("ReferenceType") = treeNode.ReduceProduction.RValues(0).Name
            Value("ReferenceName") = $"{treeNode.ChildNodes(0).AstNode.ToString}"
        Else
            Value = Nothing
        End If

    End Sub



    Shared Function Create(it As RefItem, G As Grammar) As NonTerminal


        If it.IsOptional Then
            Dim IDExpr As New NonTerminal($"{it.Name}", GetType(RefProperty))
            IDExpr.Rule = G.Empty Or New IdentifierTerminal(it.RuleRef.Name)
            Return IDExpr
        Else
            Dim IDExpr As New NonTerminal($"{it.Name}", GetType(RefProperty))
            IDExpr.Rule = New IdentifierTerminal(it.RuleRef.Name) With {.ErrorAlias = $"{it.Name}:{it.Description}"}
            Return IDExpr
        End If

    End Function


End Class


Public Class RefListProperty
    Inherits AstNode
    Property Name As String = ""

    Property Values As New JArray

    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)

        Name = treeNode.Term.Name
        For Each N In treeNode.ChildNodes
            Values.Add(N.AstNode.Value)
        Next
    End Sub
    Shared Function Create(it As RefItem, G As Grammar) As NonTerminal
        If it.IsOptional Then
            Dim IDExpr As New NonTerminal($"{it.Name}", GetType(RefListProperty))
            IDExpr.Rule = Grammar.MakeStarRule(IDExpr, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), RefProperty.Create(it, G))
            Return IDExpr
        Else
            Dim IDExpr As New NonTerminal($"{it.Name}", GetType(RefListProperty))
            IDExpr.Rule = Grammar.MakePlusRule(IDExpr, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), RefProperty.Create(it, G))
            Return IDExpr
        End If
    End Function
End Class



Public Class RegProperty
    Inherits AstNode
    Property Name As String = ""
    Property Value As String = ""
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        Name = treeNode.Term.Name
        If treeNode.ChildNodes.Count > 0 Then
            Value = treeNode.ChildNodes(0).Token.Text

        End If
    End Sub
    Shared Function Create(it As RegexItem, G As Grammar) As NonTerminal
        Dim StrineRuleExpr As New NonTerminal($"{it.Name}", GetType(RegProperty))
        StrineRuleExpr.ErrorAlias = $"{it.Name}:{it.Description} | {it.Pattern}"
        If it.IsOptional Then
            StrineRuleExpr.Rule = G.Empty Or New RegexBasedTerminal(it.Name, it.Pattern) With {.ErrorAlias = $"{it.Name}:{it.Description}  | {it.Pattern}"}
        Else
            StrineRuleExpr.Rule = New RegexBasedTerminal(it.Name, it.Pattern) With {.ErrorAlias = $"{it.Name}:{it.Description}  | {it.Pattern}"}

        End If
        Return StrineRuleExpr
    End Function
End Class


Public Class RegListProperty
    Inherits AstNode
    Property Name As String = ""
    Property Values As New List(Of String)
    Public Overrides Sub Init(context As ParsingContext, treeNode As ParseTreeNode)
        MyBase.Init(context, treeNode)
        Name = treeNode.Term.Name
        For Each N In treeNode.ChildNodes(0).ChildNodes
            Dim Temp As String = N.Token.Text
            Values.Add(Temp)
        Next
    End Sub

    Shared Function Create(it As RegexItem, G As Grammar) As NonTerminal
        Dim StrineRuleExpr As New NonTerminal($"{it.Name}", GetType(RegListProperty))
        Dim StringList As New NonTerminal($"{it.Name}List")
        If it.IsOptional Then
            StringList.Rule = Grammar.MakeStarRule(StringList, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), New RegexBasedTerminal(it.Name, it.Pattern) With {.ErrorAlias = $"{it.Name}:{it.Description} (One At Least)"})

        Else
            StringList.Rule = Grammar.MakePlusRule(StringList, If(it.ListOption.Separator.Trim = "", Nothing, G.ToTerm(it.ListOption.Separator)), New RegexBasedTerminal(it.Name, it.Pattern) With {.ErrorAlias = $"{it.Name}:{it.Description} (One At Least)"})
        End If
        StrineRuleExpr.Rule = StringList
        Return StrineRuleExpr
    End Function

End Class

