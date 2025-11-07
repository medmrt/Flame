Imports System.Text
Imports System.Text.RegularExpressions

Partial Class LanguageCompiler

    Dim PrimaryColor As String = "#21618c"
    Dim AccentColor As String = "#99004d"
    Dim StringsColor As String = "#669900"
    Dim NumbersColor As String = "#d0900f"
    Dim TabSize As Integer = 3
    Function CreateHTMLHelpFile() As String

        Dim HTML As New StringBuilder


        HTML.AppendLine($"<html>")
        HTML.AppendLine($"  <head>")
        HTML.AppendLine($"      <title>📕 {Me.CurrentLanguage.Name} | Language Reference</title>")
        HTML.AppendLine($"      <style>")
        HTML.AppendLine($"          *{{box-sizing: border-box;font-size:18px;}}")
        HTML.AppendLine($"          body{{max-width:960px;margin:10px auto;}}")
        HTML.AppendLine($"          .primary{{color:{PrimaryColor};}}")
        HTML.AppendLine($"          a:link ,a:visited ,a:hover ,a:active {{ text-decoration:none;color:{PrimaryColor} }}")
        HTML.AppendLine($"          .lanuagetitle{{padding:10px;margin:10px 5px;font-size:1.8rem;background-color:{AccentColor};color:white}}")
        HTML.AppendLine($"          .desc{{padding:5px;margin:10px 5px;color:{PrimaryColor};}}")
        'HTML.AppendLine($"          .desc::before{{content:'\201c';font-size:3rem;display:block;margin-bottom:-25px;margin-top:-20px;color:gray}}")
        'HTML.AppendLine($"          .desc::after{{content:'\201d';font-size:3rem;display:block;color:gray;text-align:right}}")
        HTML.AppendLine($"          .rulebody{{padding:5px;margin:10px 0px;background-color:white;color:{PrimaryColor};}}")
        HTML.AppendLine($"          .rule{{padding:10px;margin:10px 0px;background-color:#f5f4f2;border-left:5px solid {AccentColor};}}")
        HTML.AppendLine($"          .ruleheader{{background-color:{AccentColor};color:white;padding:5px}}")
        'HTML.AppendLine($"          .ruleheader{{border-bottom:1px solid {AccentColor};color:{AccentColor};padding:5px}}")

        HTML.AppendLine($"          .keyword{{padding:5px;margin:10px 0px;color:{AccentColor};}}")

        HTML.AppendLine($"          .strings{{padding:5px;margin:10px 0px;color:{StringsColor};}}")
        HTML.AppendLine($"          .numbers{{padding:5px;margin:10px 0px;color:{NumbersColor};}}")
        HTML.AppendLine($"          .def{{padding:5px;margin:10px 0px;color:{PrimaryColor};}}")
        HTML.AppendLine($"          .field{{padding:5px;margin:10px 0px;color:{PrimaryColor};border-left:5px solid {AccentColor};}}")
        HTML.AppendLine($"          .itemdesc{{color:gray;margin-left:1.5rem;}}")
        HTML.AppendLine($"          .requiredtagline{{color:{AccentColor};font-size:0.75rem;margin-left:1.5rem;margin-bottom:0.5rem;}}")
        HTML.AppendLine($"          .optionaltagline{{color:green;font-size:0.75rem;margin-left:1.5rem;margin-bottom:0.5rem;}}")
        HTML.AppendLine($"          .fieldtype{{color:darkorange;font-size:0.75rem;margin:0px 4px}}")
        HTML.AppendLine($"          .plaintext{{padding:5px}}")
        HTML.AppendLine($"          .rulelistitem{{margin-left:2.5rem;margin-top: 0.5em;}}")
        HTML.AppendLine($"          pre {{-moz-tab-size:{TabSize};-o-tab-size:{TabSize};tab-size:{TabSize};margin:0px}}")

        HTML.AppendLine($"          .treerulelistitem{{margin-left:1.5rem;margin-top: 0.5em;}}")

        HTML.AppendLine($"      </style>")
        HTML.AppendLine($"      <script>const dq=(it)=>'""' + it + '""';</script")
        HTML.AppendLine($"  </head>")
        HTML.AppendLine($"  <body>")

        HTML.AppendLine($"      <div style='padding:10px 0px;margin:0px 5px;border-bottom:2px solid {AccentColor}'>")

        HTML.AppendLine($"         <div style='font-size:1.3rem;color:{AccentColor}'>📕{Me.CurrentLanguage.Name} <span style='font-size:1rem;color:darkgray'>| Language Reference</span></div>")
        ' HTML.AppendLine($"         <div style='font-size:0.9rem;color:gray;margin-left: 2.5rem;'>Language Reference</div>")
        HTML.AppendLine($"      </div>")




        HTML.AppendLine($"          <div style='display:flex;justify-content:space-around;padding:25px;margin:0px 5px;background-color:#f5f4f2';>")



        HTML.AppendLine($"              <div style='width:30%;display:flex;flex-direction:column;align-items: center'>")
        HTML.AppendLine($"                  <div style='font-size:2rem;color:gray;margin-bottom:7px'>💾</div>")
        HTML.AppendLine($"                  <div style='font-size:1rem;text-transform: uppercase;color:{AccentColor}'>{Me.CurrentLanguage.Version.ToString("N1")}</div>")
        HTML.AppendLine($"                  <div style='font-size:0.8rem;text-transform: uppercase;color:gray'>Version</div>")
        HTML.AppendLine($"              </div>")


        HTML.AppendLine($"              <div style='width:30%;display:flex;flex-direction:column;align-items: center'>")
        HTML.AppendLine($"                  <div style='font-size:2rem;color:gray;margin-bottom:7px'>📅</div>")
        HTML.AppendLine($"                  <div style='font-size:1rem;text-transform: uppercase;color:{AccentColor}'>{Me.CurrentLanguage.CreationDate}</div>")
        HTML.AppendLine($"                  <div style='font-size:0.8rem;text-transform: uppercase;color:gray'>Creation Date</div>")
        HTML.AppendLine($"              </div>")


        HTML.AppendLine($"              <div style='width:30%;display:flex;flex-direction:column;align-items: center'>")
        HTML.AppendLine($"                  <div style='font-size:2rem;color:gray;margin-bottom:7px'>📇</div>")
        HTML.AppendLine($"                  <div style='font-size:1rem;;color:{AccentColor}'>{Me.CurrentLanguage.Creator}</div>")
        HTML.AppendLine($"                  <div style='font-size:0.8rem;text-transform: uppercase;color:gray'>Creator</div>")
        HTML.AppendLine($"              </div>")


        HTML.AppendLine($"          </div>")
        HTML.AppendLine($"      </div>")

        HTML.AppendLine($"      <div class='desc'>{Me.CurrentLanguage.Description}</div>")

        HTML.AppendLine(GenerateLanguageTree())

        If Me.CurrentLanguage.InitialTemplateDef IsNot Nothing Then

            Dim InitCode As String = Join(CurrentLanguage.InitialTemplateDef.Lines.ToArray, vbNewLine)

            If InitCode IsNot Nothing AndAlso InitCode.Trim <> "" Then
                For Each r In CurrentLanguage.Rules
                    For Each K In r.Items
                        If TypeOf K Is KeywordItem Then
                            InitCode = Replace(InitCode, DirectCast(K, KeywordItem).KeywordText, $"<span style='font-weight: bold;color:{AccentColor};'>{DirectCast(K, KeywordItem).KeywordText}</span>")
                            InitCode = Regex.Replace(InitCode, "\""([^\""]*)\""", $"<span style='color:{StringsColor};'>""$1""</span>")
                        End If
                    Next
                Next



                HTML.AppendLine($"      <div style='color:gray;margin:0px 5px'>Initial Template :</div>")
                HTML.AppendLine($"      <div style='margin:5px' class='rule'><pre>{InitCode}</pre></div>")
            End If




        End If




        For Each r In CurrentLanguage.Rules

            HTML.AppendLine(GenerateRuleBody(r))



        Next

        HTML.AppendLine($"  </body>")

        HTML.AppendLine($"</html>")

        Return HTML.ToString
    End Function

    Function GenerateLanguageTree() As String
        'CurrentLanguage
        Dim HTML As New StringBuilder

        HTML.AppendLine("<div style='display:flex'>")

        HTML.AppendLine($"<div style='width:20%;display:flex;flex-direction:column;align-items: center;background-color:{AccentColor};margin:5px;padding:10px;border-radius:0.5rem 0rem 0rem 0.5rem;'><div style='font-size:2rem;color:white;margin-bottom:7px;margin-top:1rem;'>📑</div><div style='font-size:0.8rem;text-transform: uppercase;color:white;margin-bottom:1rem;'>Language Tree</div></div>")

        HTML.AppendLine("<div  style='width:80%;margin:5px;padding:10px;background-color: #f5f4f2;border-left: 5px solid #99004d;border-radius: 0rem 0.5rem 0.5rem 0rem;'>")
        HTML.Append(GenerateRuleItem(CurrentLanguage.Rules.First, True))
        HTML.AppendLine("</div>")

        HTML.AppendLine("</div>")
        Return HTML.ToString
    End Function

    Private Function GenerateRuleItem(R As Rule, Optional First As Boolean = False) As String
        Dim HTML As New StringBuilder

        'HTML.AppendLine($"         <div class='rulelistitem'>📑 <a href='#{en.Name}'>{en.Name}</a>{If(en.Description.Trim = "", "", $"<span style='color:gray'> : {en.Description}</span>")}</div>")
        HTML.AppendLine($"<div {If(First, "style='margin-left:0px;margin-top:0px;'", "")} class='treerulelistitem'>📑 <a href='#{R.Name}'>{R.Name}</a>{If(R.Description.Trim = "", "", $"<span style='color:gray'> : {R.Description}</span>")}")
        'HTML.AppendLine($"<a href='#{R.Name}'>{R.Name}</a>")

        For Each it In R.Items
            If TypeOf it Is DefItem Then
                For Each R In DirectCast(it, DefItem).Rules
                    HTML.AppendLine(GenerateRuleItem(R))
                Next
            End If
        Next

        HTML.AppendLine($"</div>")

        Return HTML.ToString

    End Function


    Private Function GenerateRuleBody(R As Rule) As String
        Dim HTML As New StringBuilder
        HTML.AppendLine($"      <div class='rulebody'>")
        HTML.AppendLine($"      <div id='{R.Name}' class='ruleheader'>📑 {R.Name}</div>")

        If R.Description.Trim <> "" Then
            HTML.AppendLine($"      <div class='plaintext'>{R.Description}</div>")
        End If
        HTML.AppendLine($"      <div class='rule'>")
        Dim CopyText As New StringBuilder
        For Each it In R.Items
            'KeywordItem,KeyItem,IdentifierItem,IntItem,FloatItem,StringItem,FlagItem,EnumItem,DefItem,RefItem,RegexItem
            If TypeOf it Is KeyItem Then
                HTML.AppendLine($"         <span class='primary'>Name</span>")
                CopyText.Append("Name ")
            End If

            If TypeOf it Is KeywordItem Then
                Dim Kwrd As KeywordItem = it
                HTML.AppendLine($"         <span class='keyword'>{Kwrd.KeywordText}</span>")
                CopyText.Append($"{Kwrd.KeywordText} ")
            End If
            If TypeOf it Is StringItem Then
                Dim IntItm As StringItem = it
                HTML.AppendLine($"         <span class='strings'>""{IntItm.Name}""</span>")
                CopyText.Append($"' + dq('{IntItm.Name}') + '")
            End If
            If TypeOf it Is RegexItem Then
                Dim regItm As RegexItem = it
                HTML.AppendLine($"         <span class='primary'>{regItm.Name}</span>")
                CopyText.Append($"{regItm.Name} ")
            End If
            If TypeOf it Is FlagItem Then
                Dim IntItm As FlagItem = it
                HTML.AppendLine($"         <span class='keyword'>{IntItm.Name}</span>")
                CopyText.Append($"{IntItm.Name} ")
            End If
            If TypeOf it Is IdentifierItem Then
                Dim IntItm As IdentifierItem = it
                HTML.AppendLine($"         <span class='primary'>""{IntItm.Name}""</span>")
                CopyText.Append($"{IntItm.Name} ")
            End If

            If TypeOf it Is IntItem Then
                Dim IntItm As IntItem = it
                HTML.AppendLine($"         <span class='primary'>{IntItm.Name}</span>")
                CopyText.Append($"{IntItm.Name} ")
            End If

            If TypeOf it Is FloatItem Then
                Dim IntItm As FloatItem = it
                HTML.AppendLine($"         <span class='primary'>{IntItm.Name}</span>")
                CopyText.Append($"{IntItm.Name} ")
            End If

            If TypeOf it Is EnumItem Then
                Dim IntItm As EnumItem = it
                HTML.AppendLine($"         <span class='keyword'>{IntItm.Name}</span>")
                CopyText.Append($"{IntItm.Name} ")
            End If

            If TypeOf it Is RefItem Then
                Dim IntItm As RefItem = it
                HTML.AppendLine($"         <span style='text-decoration: underline;' class='primary'>{IntItm.Name}</span>")
                CopyText.Append($"{IntItm.Name} ")
            End If

            If TypeOf it Is DefItem Then
                Dim IntItm As DefItem = it
                HTML.AppendLine($"         <span style='text-decoration: underline;' class='primary'>{IntItm.Name}</span>")
                CopyText.Append($"{IntItm.Name} ")
            End If
        Next

        'Create Copy Rule ICON
        Dim ruleCopyBody As String = CopyText.ToString.Trim


        HTML.AppendLine($"          <span title='Click to copy this code' style='cursor:pointer;'  onclick="" navigator.clipboard.writeText('{ruleCopyBody}')"">📄</span>")

        'Close Rule DIV
        HTML.AppendLine($"     </div>")

        '***********
        HTML.AppendLine($"      <div>")
        HTML.AppendLine($"              <div class='plaintext'>Syntax :</div>")
        For Each it In R.Items
            'KeywordItem,KeyItem,IdentifierItem,IntItem,FloatItem,StringItem,FlagItem,EnumItem,DefItem,RefItem,RegexItem
            If TypeOf it Is KeyItem Then
                HTML.AppendLine(CreateKeyParamater(it))
            End If
            If TypeOf it Is StringItem Then
                HTML.AppendLine(CreateStringParamater(it))
            End If
            If TypeOf it Is RegexItem Then
                HTML.AppendLine(CreateRegexParamater(it))
            End If
            If TypeOf it Is FlagItem Then
                HTML.AppendLine(CreateFlagParamater(it))
            End If
            If TypeOf it Is IdentifierItem Then
                HTML.AppendLine(CreateIdentifierParamater(it))
            End If
            If TypeOf it Is IntItem Then
                HTML.AppendLine(CreateIntParamater(it))
            End If

            If TypeOf it Is FloatItem Then
                HTML.AppendLine(CreateFloatParamater(it))
            End If
            If TypeOf it Is EnumItem Then
                HTML.AppendLine(CreateEnumParamater(it))
            End If
            If TypeOf it Is RefItem Then
                HTML.AppendLine(CreateRefParamater(it))
            End If
            If TypeOf it Is DefItem Then
                HTML.AppendLine(CreateDefParamater(it))
            End If
        Next
        HTML.AppendLine($"          </div>")
        HTML.AppendLine($"      </div>")

        Return HTML.ToString
    End Function
    'KeywordItem,KeyItem,IdentifierItem,IntItem,FloatItem,StringItem,FlagItem,EnumItem,DefItem,RefItem,RegexItem
    Private Function CreateKeyParamater(obj As KeyItem) As String
        Dim HTML As New StringBuilder

        HTML.Append($"<div class='field'>")
        HTML.Append($"  <div>🔑 Name <sup class='fieldtype'>(Key)</sup></div>")
        HTML.AppendLine($"              <div class='requiredtagline'>Required</div>")
        If obj.Description.Trim <> "" Then
            HTML.AppendLine($"              <div class='itemdesc'>{obj.Description}</div>")
        End If
        HTML.Append($"</div>")

        Return HTML.ToString
    End Function

    Private Function CreateIdentifierParamater(obj As IdentifierItem) As String
        Dim HTML As New StringBuilder
        HTML.Append($"<div class='field'>")
        HTML.Append($"  <div>🏷️ {obj.Name} <sup class='fieldtype'>(Identifier)</sup></div>")
        If obj.ListOption IsNot Nothing Then
            If obj.IsOptional Then
                HTML.AppendLine($"              <div class='optionaltagline'>Optional List</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required List</div>")
            End If
        Else
            If obj.IsOptional Then
                HTML.AppendLine($"              <div class='optionaltagline'>Optional</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required</div>")
            End If
        End If
        If obj.Description.Trim <> "" Then
            HTML.AppendLine($"              <div class='itemdesc'>{obj.Description}</div>")
        End If
        HTML.Append($"</div>")

        Return HTML.ToString
    End Function

    Private Function CreateIntParamater(obj As IntItem) As String
        Dim HTML As New StringBuilder
        HTML.Append($"<div class='field'>")
        HTML.Append($"  <div>🏷️ {obj.Name} <sup class='fieldtype'>(Integer)</sup></div>")
        If obj.ListOption IsNot Nothing Then
            If obj.IsOptional Then
                HTML.AppendLine($"              <div class='optionaltagline'>Optional List</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required List</div>")
            End If
        Else
            If obj.IsOptional Then
                HTML.AppendLine($"              <div class='optionaltagline'>Optional</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required</div>")
            End If
        End If
        If obj.Description.Trim <> "" Then
            HTML.AppendLine($"              <div class='itemdesc'>{obj.Description}</div>")
        End If
        HTML.Append($"</div>")
        Return HTML.ToString
    End Function

    Private Function CreateFloatParamater(obj As FloatItem) As String
        Dim HTML As New StringBuilder
        HTML.Append($"<div class='field'>")
        HTML.Append($"  <div>🏷️ {obj.Name} <sup class='fieldtype'>(Float)</sup></div>")
        If obj.ListOption IsNot Nothing Then
            If obj.IsOptional Then
                HTML.AppendLine($"              <div class='optionaltagline'>Optional List</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required List</div>")
            End If
        Else
            If obj.IsOptional Then
                HTML.AppendLine($"              <div class='optionaltagline'>Optional</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required</div>")
            End If
        End If
        If obj.Description.Trim <> "" Then
            HTML.AppendLine($"              <div class='itemdesc'>{obj.Description}</div>")
        End If
        HTML.Append($"</div>")
        Return HTML.ToString
    End Function

    Private Function CreateStringParamater(obj As StringItem) As String
        Dim HTML As New StringBuilder
        HTML.Append($"<div class='field'>")
        HTML.Append($"  <div>🏷️ {obj.Name} <sup class='fieldtype'>(String)</sup></div>")
        If obj.ListOption IsNot Nothing Then
            If obj.IsOptional Then
                HTML.AppendLine($"              <div class='optionaltagline'>Optional List</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required List</div>")
            End If
        Else
            If obj.IsOptional Then
                HTML.AppendLine($"              <div class='optionaltagline'>Optional</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required</div>")
            End If
        End If
        If obj.Description.Trim <> "" Then
            HTML.AppendLine($"              <div class='itemdesc'>{obj.Description}</div>")
        End If
        HTML.Append($"</div>")
        Return HTML.ToString
    End Function

    Private Function CreateFlagParamater(obj As FlagItem) As String
        Dim HTML As New StringBuilder
        HTML.Append($"<div class='field'>")
        HTML.Append($"  <div>🏷️ {obj.Name} <sup class='fieldtype'>(Flag)</sup></div>")
        If obj.Description.Trim <> "" Then
            HTML.AppendLine($"              <div class='itemdesc'>{obj.Description}</div>")
        End If
        HTML.Append($"</div>")
        Return HTML.ToString
    End Function
    Private Function CreateEnumParamater(obj As EnumItem) As String
        Dim HTML As New StringBuilder

        HTML.Append($"<div class='field'>")
        HTML.Append($"  <div>🏷️ {obj.Name} <sup class='fieldtype'>(Enum)</sup></div>")

        If obj.IsOptional Then
            HTML.AppendLine($"              <div class='optionaltagline'>Optional</div>")
        Else
            HTML.AppendLine($"              <div class='requiredtagline'>Required</div>")
        End If

        If obj.Description.Trim <> "" Then
            HTML.AppendLine($"              <div class='itemdesc'>{obj.Description}</div>")
        End If


        For Each en In obj.EnumerationItems
            HTML.AppendLine($"         <div class='rulelistitem'>{en}</div>")
        Next

        HTML.Append($"</div>")


        'HTML.AppendLine($"         <div class='field'>📇 {obj.Name}<sup class='fieldtype'>(Enum)</sup>:<span class='itemdesc'>{obj.Description}</span></div>")

        Return HTML.ToString
    End Function
    Private Function CreateDefParamater(obj As DefItem) As String
        Dim HTML As New StringBuilder
        HTML.AppendLine($"         <div class='field'>")
        HTML.AppendLine($"              <div>🏷️ {obj.Name}<sup class='fieldtype'>(Definition)</sup></div>")


        If obj.ListOption IsNot Nothing Then
            If obj.IsOptional Then
                HTML.AppendLine($"              <div class='optionaltagline'>Optional List</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required List</div>")
            End If
        Else
            If obj.IsOptional Then
                HTML.AppendLine($"              <div  class='optionaltagline'>Optional</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required</div>")
            End If
        End If

        If obj.Description.Trim <> "" Then
            HTML.AppendLine($"              <div class='itemdesc'>{obj.Description}</div>")
        End If

        HTML.AppendLine($"              <div class='itemdesc'>One of the fellowing rules:</div>")
        Dim I As Integer = 1
        For Each en In obj.Rules
            HTML.AppendLine($"         <div class='rulelistitem'>📑 <a href='#{en.Name}'>{en.Name}</a>{If(en.Description.Trim = "", "", $"<span style='color:gray'> : {en.Description}</span>")}</div>")
            I += 1
        Next
        HTML.AppendLine($"         </div>")
        Return HTML.ToString
    End Function
    Private Function CreateRefParamater(obj As RefItem) As String
        Dim HTML As New StringBuilder
        HTML.AppendLine($"         <div class='field'>")
        HTML.AppendLine($"              <div>🏷️ {obj.Name}<sup class='fieldtype'>(Reference)</sup></div>")


        If obj.ListOption IsNot Nothing Then
            If obj.IsOptional Then
                HTML.AppendLine($"              <div class='optionaltagline'>Optional List</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required List</div>")
            End If
        Else
            If obj.IsOptional Then
                HTML.AppendLine($"              <div  class='optionaltagline'>Optional</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required</div>")
            End If
        End If

        If obj.Description.Trim <> "" Then
            HTML.AppendLine($"              <div class='itemdesc'>{obj.Description}</div>")
        End If

        HTML.AppendLine($"              <div class='itemdesc'>A reference to:</div>")
        HTML.AppendLine($"                  <div class='rulelistitem'>📑 <a href='#{obj.RuleRef.Name}'>{obj.RuleRef.Name}</a> {If(obj.RuleRef.Description.Trim = "", "", $"<span style='color:gray'> : {obj.RuleRef.Description}</span>")}</div>")
        HTML.AppendLine($"         </div>")
        Return HTML.ToString
    End Function
    Private Function CreateRegexParamater(obj As RegexItem) As String
        Dim HTML As New StringBuilder
        HTML.Append($"<div class='field'>")
        HTML.Append($"  <div>🏷️ {obj.Name} <sup class='fieldtype'>(Regular Expression)</sup></div>")
        If obj.ListOption IsNot Nothing Then
            If obj.IsOptional Then
                HTML.AppendLine($"              <div class='optionaltagline'>Optional List</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required List</div>")
            End If
        Else
            If obj.IsOptional Then
                HTML.AppendLine($"              <div class='optionaltagline'>Optional</div>")
            Else
                HTML.AppendLine($"              <div class='requiredtagline'>Required</div>")
            End If
        End If
        If obj.Description.Trim <> "" Then
            HTML.AppendLine($"              <div class='itemdesc'>{obj.Description}</div>")
        End If
        HTML.AppendLine($"              <div class='itemdesc'>Pattern : <span class='strings'>""{obj.Pattern}""</span></div>")
        HTML.Append($"</div>")

        Return HTML.ToString
    End Function

End Class